import axios, { type InternalAxiosRequestConfig } from 'axios'
import { ElMessage } from 'element-plus'
import router from '../router'
import {
    getAccessToken, setAccessToken,
    getRefreshToken, setRefreshToken,
    clearTokens, isTokenExpiringSoon, isTokenExpired
} from './token'

const service = axios.create({
    baseURL: import.meta.env.DEV ? 'http://localhost:5180/api' : '/api',
    timeout: 10000
})

// --- Refresh token 并发控制 ---
let refreshPromise: Promise<string | null> | null = null

async function doRefreshToken(): Promise<string | null> {
    const refreshToken = getRefreshToken()
    if (!refreshToken) return null

    try {
        // 使用独立 axios 实例避免拦截器循环
        const res = await axios.post(
            `${service.defaults.baseURL}/auth/refresh`,
            { refreshToken },
            { timeout: 10000 }
        )

        const data = res.data
        if (data.code === 200 && data.data) {
            setAccessToken(data.data.token)
            setRefreshToken(data.data.refreshToken)
            return data.data.token
        }
        return null
    } catch {
        return null
    }
}

function refreshAccessToken(): Promise<string | null> {
    if (!refreshPromise) {
        refreshPromise = doRefreshToken().finally(() => {
            refreshPromise = null
        })
    }
    return refreshPromise
}

// --- Request interceptor: 主动刷新 ---
service.interceptors.request.use(
    async (config: InternalAxiosRequestConfig) => {
        let token = getAccessToken()
        if (token) {
            // 主动检查：token 即将过期时先刷新
            if (isTokenExpiringSoon(token, 5) && !isTokenExpired(token) && getRefreshToken()) {
                const newToken = await refreshAccessToken()
                if (newToken) {
                    token = newToken
                }
            }
            config.headers.Authorization = `Bearer ${token}`
        }
        return config
    },
    (error) => {
        return Promise.reject(error)
    }
)

// --- Response interceptor: 被动刷新（401） ---
service.interceptors.response.use(
    (response) => {
        const res = response.data
        // Backend wraps all responses in ApiResponse<T> { code, message, data }
        if (res.code && res.code !== 200) {
            ElMessage.error(res.message || '请求失败')
            return Promise.reject(new Error(res.message || '请求失败'))
        }
        return res
    },
    async (error) => {
        const originalRequest = error.config
        if (error.response?.status === 401 && !originalRequest._retry && getRefreshToken()) {
            originalRequest._retry = true
            const newToken = await refreshAccessToken()
            if (newToken) {
                originalRequest.headers.Authorization = `Bearer ${newToken}`
                return service(originalRequest)
            }
        }

        // 刷新也失败了，或非 401 错误
        if (error.response) {
            switch (error.response.status) {
                case 400: {
                    const data = error.response.data
                    if (data?.errors) {
                        const firstError = Object.values(data.errors).flat()[0]
                        ElMessage.error(firstError as string || '请求参数错误')
                    } else {
                        ElMessage.error(data?.message || '请求参数错误')
                    }
                    break
                }
                case 401:
                    ElMessage.error('登录已过期，请重新登录')
                    clearTokens()
                    router.push('/login')
                    break
                case 403:
                    ElMessage.error('没有权限访问')
                    break
                case 500:
                    ElMessage.error('服务器内部错误')
                    break
                default:
                    ElMessage.error(error.response.data?.message || '请求失败')
            }
        } else {
            ElMessage.error('网络连接失败')
        }
        return Promise.reject(error)
    }
)

export default service
