import request from '../utils/request'

export interface Unit {
    uid?: string
    fNumber: string
    fName: string
    fStatus?: number
    fUnitGroupId?: string
    fIsBaseUnit?: boolean
    fPrecision?: number
    fCoefficient?: number
    cYmd?: string
    fDescription?: string
    fRoundType?: string
    fConvertType?: string
}

export const getUnits = (params?: any) => {
    return request({ url: '/unit', method: 'get', params: { pageIndex: params?.page || 1, pageSize: params?.pageSize || 10, keyword: params?.keyword || '', groupId: params?.groupId || '' } })
}
export const getUnit = (id: string) => {
    return request({ url: `/unit/${id}`, method: 'get' })
}
export const createUnit = (data: any) => {
    return request({ url: '/unit', method: 'post', data })
}
export const updateUnit = (id: string, data: any) => {
    return request({ url: `/unit/${id}`, method: 'put', data })
}
export const deleteUnit = (id: string) => {
    return request({ url: `/unit/${id}`, method: 'delete' })
}

// PUT /api/unit/{id}/approve - 审核
export const approveUnit = (id: string) => {
    return request({ url: `/unit/${id}/approve`, method: 'put' })
}

// PUT /api/unit/{id}/unapprove - 反审核
export const unapproveUnit = (id: string) => {
    return request({ url: `/unit/${id}/unapprove`, method: 'put' })
}

// PUT /api/unit/{id}/disable - 禁用
export const disableUnit = (id: string) => {
    return request({ url: `/unit/${id}/disable`, method: 'put' })
}

// PUT /api/unit/{id}/enable - 反禁用
export const enableUnit = (id: string) => {
    return request({ url: `/unit/${id}/enable`, method: 'put' })
}
