import request from '../utils/request'

export interface Role {
    uid?: string
    roleNumber: string
    roleName: string
    roleType?: number
    roleTypeName?: string
    isDefault?: boolean
    note?: string
    cYmd?: string
    permissions?: string[]
}

// GET /api/role - paged list
export const getRoles = (params: any) => {
    return request({
        url: '/role',
        method: 'get',
        params: {
            pageIndex: params.page || 1,
            pageSize: params.pageSize || 10,
            keyword: params.keyword || ''
        }
    })
}

// GET /api/role/{id}
export const getRole = (id: string) => {
    return request({
        url: `/role/${id}`,
        method: 'get'
    })
}

// POST /api/role
export const createRole = (data: any) => {
    return request({
        url: '/role',
        method: 'post',
        data
    })
}

// PUT /api/role/{id}
export const updateRole = (id: string, data: any) => {
    return request({
        url: `/role/${id}`,
        method: 'put',
        data
    })
}

// DELETE /api/role/{id}
export const deleteRole = (id: string) => {
    return request({
        url: `/role/${id}`,
        method: 'delete'
    })
}

// GET /api/role/{id}/permissions
export const getRolePermissions = (roleId: string) => {
    return request({
        url: `/role/${roleId}/permissions`,
        method: 'get'
    })
}

// POST /api/role/{id}/permissions
export const assignRolePermissions = (roleId: string, permissionCodes: string[]) => {
    return request({
        url: `/role/${roleId}/permissions`,
        method: 'post',
        data: { permissionCodes }
    })
}

// GET /api/permission/definitions
export interface PermissionItem {
    code: string
    name: string
}

export interface PermissionModule {
    moduleId: string
    moduleName: string
    isDirectory: boolean
    permissions: PermissionItem[]
    children: PermissionModule[]
}

export interface PermissionGroup {
    groupName: string
    icon: string
    modules: PermissionModule[]
}

export const getPermissionDefinitions = () => {
    return request({
        url: '/permission/definitions',
        method: 'get'
    })
}
