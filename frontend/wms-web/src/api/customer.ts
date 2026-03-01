import request from '../utils/request'

export interface Customer {
    uid?: string
    fNumber: string
    fName: string
    fStatus?: number
    fShortName?: string
    fContact?: string
    fPhone?: string
    fAddress?: string
    cYmd?: string
    // detail fields
    fSeller?: string
    fSalDeptId?: string
    fTradingCurrId?: string
    fCountry?: string
    fProvincial?: string
    fCity?: string
    fZip?: string
    fWebSite?: string
    fTel?: string
    fFax?: string
    fEmail?: string
    fBank?: string
    fAccount?: string
    fLegalPerson?: string
    fTaxRegisterCode?: string
    fNameEn?: string
    fAddressEn?: string
    fNote?: string
    fEmpId?: string
}

// GET /api/customer - paged list
export const getCustomers = (params?: any) => {
    return request({
        url: '/customer',
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

// GET /api/customer/{id}
export const getCustomer = (id: string) => {
    return request({
        url: `/customer/${id}`,
        method: 'get'
    })
}

// POST /api/customer
export const createCustomer = (data: any) => {
    return request({
        url: '/customer',
        method: 'post',
        data
    })
}

// PUT /api/customer/{id}
export const updateCustomer = (id: string, data: any) => {
    return request({
        url: `/customer/${id}`,
        method: 'put',
        data
    })
}

// DELETE /api/customer/{id}
export const deleteCustomer = (id: string) => {
    return request({
        url: `/customer/${id}`,
        method: 'delete'
    })
}

// PUT /api/customer/{id}/approve - 审核
export const approveCustomer = (id: string) => {
    return request({
        url: `/customer/${id}/approve`,
        method: 'put'
    })
}

// PUT /api/customer/{id}/unapprove - 反审核
export const unapproveCustomer = (id: string) => {
    return request({
        url: `/customer/${id}/unapprove`,
        method: 'put'
    })
}

// PUT /api/customer/{id}/disable - 禁用
export const disableCustomer = (id: string) => {
    return request({
        url: `/customer/${id}/disable`,
        method: 'put'
    })
}

// PUT /api/customer/{id}/enable - 反禁用
export const enableCustomer = (id: string) => {
    return request({
        url: `/customer/${id}/enable`,
        method: 'put'
    })
}

// GET /api/customer/fields - 获取模型字段数据类型
export const getCustomersFields = () => {
    return request({
        url: '/customer/fields',
        method: 'get'
    })
}
