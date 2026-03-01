import request from '../utils/request'

export interface MaterialBarType {
    uid?: string
    fStatus?: number
    fmaterialid?: string
    fbartype?: number
    fmaterialnumber: string
    fmaterialname?: string
    fcheckdate?: string
    fcheckerid?: string
    fdisabledate?: string
    fdisableid?: string
    cYmd?: string
    fGroupId?: string
}

export const getMaterialBarTypes = (params?: any) => {
    return request({
        url: '/materialbartype',
        method: 'get',
        params: {
            pageIndex: params?.page || 1,
            pageSize: params?.pageSize || 10,
            keyword: params?.keyword || '',
            groupId: params?.groupId || '',
            dynamicFilters: params?.dynamicFilters || []
        }
    })
}

// GET /api/materialbartype/{id}
export const getMaterialBarType = (id: string) => {
    return request({
        url: `/materialbartype/${id}`,
        method: 'get'
    })
}

// POST /api/materialbartype
export const createMaterialBarType = (data: any) => {
    return request({
        url: '/materialbartype',
        method: 'post',
        data
    })
}

// PUT /api/materialbartype/{id}
export const updateMaterialBarType = (id: string, data: any) => {
    return request({
        url: `/materialbartype/${id}`,
        method: 'put',
        data
    })
}

// DELETE /api/materialbartype/{id}
export const deleteMaterialBarType = (id: string) => {
    return request({
        url: `/materialbartype/${id}`,
        method: 'delete'
    })
}

// PUT /api/materialbartype/{id}/approve
export const approveMaterialBarType = (id: string) => {
    return request({
        url: `/materialbartype/${id}/approve`,
        method: 'put'
    })
}

// PUT /api/materialbartype/{id}/unapprove
export const unapproveMaterialBarType = (id: string) => {
    return request({
        url: `/materialbartype/${id}/unapprove`,
        method: 'put'
    })
}

// PUT /api/materialbartype/{id}/disable - 禁用
export const disableMaterialBarType = (id: string) => {
    return request({
        url: `/materialbartype/${id}/disable`,
        method: 'put'
    })
}

// PUT /api/materialbartype/{id}/enable - 反禁用
export const enableMaterialBarType = (id: string) => {
    return request({
        url: `/materialbartype/${id}/enable`,
        method: 'put'
    })
}

// GET /api/materialbartype/fields - 获取模型字段数据类型
export const getMaterialBarTypesFields = () => {
    return request({
        url: '/materialbartype/fields',
        method: 'get'
    })
}
