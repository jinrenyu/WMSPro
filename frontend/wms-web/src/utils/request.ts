import axios from 'axios'
import { ElMessage } from 'element-plus'
import router from '../router'

const service = axios.create({
    baseURL: import.meta.env.DEV ? 'http://localhost:5180/api' : '/api',
    timeout: 10000
})

// Request interceptor
service.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('token')
        if (token) {
            config.headers.Authorization = `Bearer ${token}`
        }
        return config
    },
    (error) => {
        return Promise.reject(error)
    }
)

// Response interceptor - unwrap ApiResponse<T>
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
    (error) => {
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
                    localStorage.removeItem('token')
                    localStorage.removeItem('username')
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
