import { jwtDecode } from 'jwt-decode'

interface JwtPayload {
    exp: number
    sub: string
    userId: string
    userName: string
}

export function getAccessToken(): string | null {
    return localStorage.getItem('token')
}

export function setAccessToken(token: string): void {
    localStorage.setItem('token', token)
}

export function getRefreshToken(): string | null {
    return localStorage.getItem('refreshToken')
}

export function setRefreshToken(token: string): void {
    localStorage.setItem('refreshToken', token)
}

export function clearTokens(): void {
    localStorage.removeItem('token')
    localStorage.removeItem('refreshToken')
    localStorage.removeItem('username')
    localStorage.removeItem('permissions')
}

/**
 * 检查 access token 是否即将过期（默认剩余 5 分钟以内）
 */
export function isTokenExpiringSoon(token: string, thresholdMinutes = 5): boolean {
    try {
        const decoded = jwtDecode<JwtPayload>(token)
        const expiresAt = decoded.exp * 1000
        const now = Date.now()
        return expiresAt - now < thresholdMinutes * 60 * 1000
    } catch {
        return true
    }
}

/**
 * 检查 access token 是否已过期
 */
export function isTokenExpired(token: string): boolean {
    try {
        const decoded = jwtDecode<JwtPayload>(token)
        return decoded.exp * 1000 < Date.now()
    } catch {
        return true
    }
}
