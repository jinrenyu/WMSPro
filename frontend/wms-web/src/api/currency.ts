import request from '../utils/request'

export interface Currency {
    uid?: string
    fNumber: string
    fCode: string
    fName: string
    fStatus?: number
    fExchangeRate?: number
    fPriceDigits?: number
    fAmountDigits?: number
    cYmd?: string
    // detail fields
    fDescription?: string
    fFixRate?: number
    fUseOrgId?: string
}

export const getCurrencies = (params?: any) => {
    return request({
        url: '/currency',
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

// GET /api/currency/{id}
export const getCurrency = (id: string) => {
    return request({
        url: `/currency/${id}`,
        method: 'get'
    })
}

// POST /api/currency
export const createCurrency = (data: any) => {
    return request({
        url: '/currency',
        method: 'post',
        data
    })
}

// PUT /api/currency/{id}
export const updateCurrency = (id: string, data: any) => {
    return request({
        url: `/currency/${id}`,
        method: 'put',
        data
    })
}

// DELETE /api/currency/{id}
export const deleteCurrency = (id: string) => {
    return request({
        url: `/currency/${id}`,
        method: 'delete'
    })
}

// PUT /api/currency/{id}/approve - 审核
export const approveCurrency = (id: string) => {
    return request({
        url: `/currency/${id}/approve`,
        method: 'put'
    })
}

// PUT /api/currency/{id}/unapprove - 反审核
export const unapproveCurrency = (id: string) => {
    return request({
        url: `/currency/${id}/unapprove`,
        method: 'put'
    })
}

// PUT /api/currency/{id}/disable - 禁用
export const disableCurrency = (id: string) => {
    return request({
        url: `/currency/${id}/disable`,
        method: 'put'
    })
}

// PUT /api/currency/{id}/enable - 反禁用
export const enableCurrency = (id: string) => {
    return request({
        url: `/currency/${id}/enable`,
        method: 'put'
    })
}

// GET /api/currency/fields - 获取模型字段数据类型
export const getCurrenciesFields = () => {
    return request({
        url: '/currency/fields',
        method: 'get'
    })
}
