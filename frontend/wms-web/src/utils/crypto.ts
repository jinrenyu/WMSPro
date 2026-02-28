import JSEncrypt from 'jsencrypt'
import request from './request'

let publicKey: string | null = null
let fetchingPromise: Promise<string> | null = null

/**
 * 从后端获取 RSA 公钥（仅获取一次，之后缓存）
 */
export const fetchPublicKey = async (): Promise<string> => {
    if (publicKey) return publicKey

    // 防止并发重复请求
    if (fetchingPromise) return fetchingPromise

    fetchingPromise = (async () => {
        try {
            const res: any = await request.get('/auth/public-key')
            publicKey = res.data.publicKey
            return publicKey!
        } catch (error) {
            console.error('Failed to fetch RSA public key:', error)
            throw error
        } finally {
            fetchingPromise = null
        }
    })()

    return fetchingPromise
}

/**
 * 使用 RSA 公钥加密密码
 * @param password 明文密码
 * @returns Base64 编码的加密密码
 */
export const encryptPassword = async (password: string): Promise<string> => {
    const key = await fetchPublicKey()
    const encryptor = new JSEncrypt()
    encryptor.setPublicKey(key)
    const encrypted = encryptor.encrypt(password)
    if (!encrypted) {
        throw new Error('RSA encryption failed')
    }
    return encrypted
}
