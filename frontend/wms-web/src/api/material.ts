import request from '../utils/request'

export interface Material {
    uid?: string
    fNumber: string
    fName: string
    fSpecification?: string
    fErpClsId?: string
    fBaseUnitId?: string
    fBaseUnitName?: string
    fTypeId?: string
    fIsBatchManage?: boolean
    fStatus?: number
    fCheckerId?: string
    fCheckDate?: string
    cYmd?: string
    // detail fields
    fMasterId?: string
    fDescription?: string
    fBarcode?: string
    fAbc?: string
    fMaxQty?: number
    fSafeQty?: number
    fNetWeight?: number
    fGrossWeight?: number
    fStoreUnitId?: string
    fStoreUnitName?: string
    fSaleUnitId?: string
    fSaleUnitName?: string
    fPurchaseUnitId?: string
    fPurchaseUnitName?: string
    fIsKfPeriod?: boolean
    fKfUnit?: number
    fKfPeriod?: number
    fMinPoQty?: number
    fIncreaseQty?: number
    fCheckIncoming?: boolean
    fOldNumber?: string
    fDeStockId?: string
    fDeStockName?: string
    fDeSpId?: string
    fDeSpName?: string
    fVPart?: boolean
    fGroupId?: string
    fGroupName?: string
}

// GET /api/material - paged list
export const getMaterials = (params?: any) => {
    return request({
        url: '/material',
        method: 'get',
        params: {
            pageIndex: params?.page || 1,
            pageSize: params?.pageSize || 10,
            keyword: params?.keyword || '',
            groupId: params?.groupId || ''
        }
    })
}

// GET /api/material/{id}
export const getMaterial = (id: string) => {
    return request({
        url: `/material/${id}`,
        method: 'get'
    })
}

// POST /api/material
export const createMaterial = (data: any) => {
    return request({
        url: '/material',
        method: 'post',
        data
    })
}

// PUT /api/material/{id}
export const updateMaterial = (id: string, data: any) => {
    return request({
        url: `/material/${id}`,
        method: 'put',
        data
    })
}

// DELETE /api/material/{id}
export const deleteMaterial = (id: string) => {
    return request({
        url: `/material/${id}`,
        method: 'delete'
    })
}

// PUT /api/material/{id}/approve - 审核
export const approveMaterial = (id: string) => {
    return request({
        url: `/material/${id}/approve`,
        method: 'put'
    })
}

// PUT /api/material/{id}/unapprove - 反审核
export const unapproveMaterial = (id: string) => {
    return request({
        url: `/material/${id}/unapprove`,
        method: 'put'
    })
}

// PUT /api/material/{id}/disable - 禁用
export const disableMaterial = (id: string) => {
    return request({
        url: `/material/${id}/disable`,
        method: 'put'
    })
}

// PUT /api/material/{id}/enable - 反禁用
export const enableMaterial = (id: string) => {
    return request({
        url: `/material/${id}/enable`,
        method: 'put'
    })
}
