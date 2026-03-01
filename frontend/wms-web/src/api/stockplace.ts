import request from '../utils/request'

export interface StockPlace {
    uid?: string
    fStatus?: number
    fNumber: string
    fName: string
    fStockId?: string
    fSpProperty?: string
    fAllowMix?: boolean
    cYmd?: string
    fDescription?: string
    fMaxCapacity?: number
}

export const getStockPlaces = (params?: any) => {
    return request({ url: '/stockplace', method: 'get', params: { pageIndex: params?.page || 1, pageSize: params?.pageSize || 10, keyword: params?.keyword || '', groupId: params?.groupId || '', dynamicFilters: params?.dynamicFilters || [] } })
}
export const getStockPlace = (id: string) => {
    return request({ url: `/stockplace/${id}`, method: 'get' })
}
export const createStockPlace = (data: any) => {
    return request({ url: '/stockplace', method: 'post', data })
}
export const updateStockPlace = (id: string, data: any) => {
    return request({ url: `/stockplace/${id}`, method: 'put', data })
}
export const deleteStockPlace = (id: string) => {
    return request({ url: `/stockplace/${id}`, method: 'delete' })
}

export const approveStockPlace = (id: string) => {
    return request({ url: `/stockplace/${id}/approve`, method: 'put' })
}

export const unapproveStockPlace = (id: string) => {
    return request({ url: `/stockplace/${id}/unapprove`, method: 'put' })
}

// PUT /api/stockplace/{id}/disable - 禁用
export const disableStockPlace = (id: string) => {
    return request({ url: `/stockplace/${id}/disable`, method: 'put' })
}

// PUT /api/stockplace/{id}/enable - 反禁用
export const enableStockPlace = (id: string) => {
    return request({ url: `/stockplace/${id}/enable`, method: 'put' })
}

// GET /api/stockplace/fields - 获取模型字段数据类型
export const getStockPlacesFields = () => {
    return request({ url: '/stockplace/fields', method: 'get' })
}
