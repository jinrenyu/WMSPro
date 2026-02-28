import request from '../utils/request'
import { encryptPassword } from '../utils/crypto'

export interface User {
    uid?: string
    userId: string
    userName: string
    password?: string
    email?: string
    paType?: string
    fStatus?: number
    lockStatus?: number
    lastLoginTime?: string
    cYmd?: string
    companyId?: string
    roles?: string[]
    roleDetails?: { roleId: string; roleNumber: string; roleName: string }[]
    organizations?: { orgId: string; isDefault: boolean }[]
}

// GET /api/user - paged list
export const getUsers = (params: any) => {
    return request({
        url: '/user',
        method: 'get',
        params: {
            pageIndex: params.page || 1,
            pageSize: params.pageSize || 10,
            keyword: params.keyword || ''
        }
    })
}

// GET /api/user/{id}
export const getUser = (id: string) => {
    return request({
        url: `/user/${id}`,
        method: 'get'
    })
}

// GET /api/user/me
export const getCurrentUser = () => {
    return request({
        url: '/user/me',
        method: 'get'
    })
}

// POST /api/user
export const createUser = async (data: any) => {
    const payload = { ...data }
    if (payload.password) {
        payload.password = await encryptPassword(payload.password)
    }
    return request({
        url: '/user',
        method: 'post',
        data: payload
    })
}

// PUT /api/user/{id}
export const updateUser = (id: string, data: any) => {
    return request({
        url: `/user/${id}`,
        method: 'put',
        data
    })
}

// DELETE /api/user/{id}
export const deleteUser = (id: string) => {
    return request({
        url: `/user/${id}`,
        method: 'delete'
    })
}

// POST /api/user/{id}/assign-roles
export const assignUserRoles = (userId: string, roleIds: string[]) => {
    return request({
        url: `/user/${userId}/assign-roles`,
        method: 'post',
        data: { roleIds }
    })
}

// POST /api/user/{id}/unlock
export const unlockUser = (id: string) => {
    return request({
        url: `/user/${id}/unlock`,
        method: 'post'
    })
}

// POST /api/user/{id}/toggle-status
export const toggleUserStatus = (id: string) => {
    return request({
        url: `/user/${id}/toggle-status`,
        method: 'post'
    })
}

// POST /api/user/reset-password
export const resetPassword = async (data: { userId: string; newPassword: string }) => {
    const encryptedNewPwd = await encryptPassword(data.newPassword)
    return request({
        url: '/user/reset-password',
        method: 'post',
        data: { userId: data.userId, newPassword: encryptedNewPwd }
    })
}
