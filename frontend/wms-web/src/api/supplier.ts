import request from '../utils/request'

export interface Supplier {
    uid?: string
    fStatus?: number
    fNumber: string
    fName: string
    fContact?: string
    fPhone?: string
    fAddress?: string
    fNote?: string
    cYmd?: string
}

// GET /api/supplier - paged list
export const getSuppliers = (params?: any) => {
    return request({
        url: '/supplier',
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

// GET /api/supplier/{id}
export const getSupplier = (id: string) => {
    return request({
        url: `/supplier/${id}`,
        method: 'get'
    })
}

// POST /api/supplier
export const createSupplier = (data: any) => {
    return request({
        url: '/supplier',
        method: 'post',
        data
    })
}

// PUT /api/supplier/{id}
export const updateSupplier = (id: string, data: any) => {
    return request({
        url: `/supplier/${id}`,
        method: 'put',
        data
    })
}

// DELETE /api/supplier/{id}
export const deleteSupplier = (id: string) => {
    return request({
        url: `/supplier/${id}`,
        method: 'delete'
    })
}

// PUT /api/supplier/{id}/approve
export const approveSupplier = (id: string) => {
    return request({
        url: `/supplier/${id}/approve`,
        method: 'put'
    })
}

// PUT /api/supplier/{id}/unapprove
export const unapproveSupplier = (id: string) => {
    return request({
        url: `/supplier/${id}/unapprove`,
        method: 'put'
    })
}

// PUT /api/supplier/{id}/disable - 禁用
export const disableSupplier = (id: string) => {
    return request({
        url: `/supplier/${id}/disable`,
        method: 'put'
    })
}

// PUT /api/supplier/{id}/enable - 反禁用
export const enableSupplier = (id: string) => {
    return request({
        url: `/supplier/${id}/enable`,
        method: 'put'
    })
}

// GET /api/supplier/fields - 获取模型字段数据类型
export const getSuppliersFields = () => {
    return request({
        url: '/supplier/fields',
        method: 'get'
    })
}
