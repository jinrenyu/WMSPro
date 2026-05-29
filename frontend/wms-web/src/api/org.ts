import request from '../utils/request'

export interface MyOrg {
    orgId: string
    orgNumber: string
    orgName: string
    parentOrgId: string
    parentOrgName: string
    isDefault: boolean
}

// GET /api/org/my - 当前用户可访问的组织列表
export const getMyOrgs = () => {
    return request({ url: '/org/my', method: 'get' })
}
