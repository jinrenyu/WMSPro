import request from '../utils/request'

export interface Warehouse {
    uid?: string
    fNumber: string
    fName: string
    fStatus?: number
    fPrincipal?: string
    fTel?: string
    fType?: string
    fAddress?: string
    cYmd?: string
    // detail fields
    fDescription?: string
    fStockProperty?: string
    fAllowMinusQty?: boolean
    fIsOpenLocation?: boolean
    fBonded?: boolean
    fAllowMrpPlan?: boolean
    fAllowLock?: boolean
    fAvailableAlert?: boolean
    fAvailablePicking?: boolean
    fSortingPriority?: number
    fIsVirtual?: boolean
    erpNumber?: string
}

// GET /api/warehouse - paged list
export const getWarehouses = (params?: any) => {
    return request({
        url: '/warehouse',
        method: 'get',
        params: {
            pageIndex: params?.page || 1,
            pageSize: params?.pageSize || 10,
            keyword: params?.keyword || '',
            groupId: params?.groupId || ''
        }
    })
}

// GET /api/warehouse/{id}
export const getWarehouse = (id: string) => {
    return request({
        url: `/warehouse/${id}`,
        method: 'get'
    })
}

// POST /api/warehouse
export const createWarehouse = (data: any) => {
    return request({
        url: '/warehouse',
        method: 'post',
        data
    })
}

// PUT /api/warehouse/{id}
export const updateWarehouse = (id: string, data: any) => {
    return request({
        url: `/warehouse/${id}`,
        method: 'put',
        data
    })
}

// DELETE /api/warehouse/{id}
export const deleteWarehouse = (id: string) => {
    return request({
        url: `/warehouse/${id}`,
        method: 'delete'
    })
}

// PUT /api/warehouse/{id}/approve - 审核
export const approveWarehouse = (id: string) => {
    return request({
        url: `/warehouse/${id}/approve`,
        method: 'put'
    })
}

// PUT /api/warehouse/{id}/unapprove - 反审核
export const unapproveWarehouse = (id: string) => {
    return request({
        url: `/warehouse/${id}/unapprove`,
        method: 'put'
    })
}

// PUT /api/warehouse/{id}/disable - 禁用
export const disableWarehouse = (id: string) => {
    return request({
        url: `/warehouse/${id}/disable`,
        method: 'put'
    })
}

// PUT /api/warehouse/{id}/enable - 反禁用
export const enableWarehouse = (id: string) => {
    return request({
        url: `/warehouse/${id}/enable`,
        method: 'put'
    })
}
