import request from '../utils/request'
import { getLookup, type LookupParams } from './lookup'

export interface Warehouse {
    uid?: string
    fNumber: string
    fName: string
    fStatus?: number
    fDisabled?: boolean
    fPrincipal?: string
    fTel?: string
    fType?: string
    fAddress?: string
    cYmd?: string
    // detail fields
    fDescription?: string
    fStockProperty?: string
    fStockStatusType?: string
    fDefStockStatusId?: string
    fDefReceiveStatusId?: string
    fWorkshopId?: string
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
    fGroupId?: string
    fGroupName?: string
    // 使用组织 / 审计（只读）
    fCompanyId?: string
    fCompanyName?: string
    cUser?: string
    mUser?: string
    mYmd?: string
    fCheckerId?: string
    fCheckDate?: string
    fdisableid?: string
    fdisabledate?: string
}

// 库存状态下拉（默认库存状态、默认收料状态）
export const getStockStatusLookup = (params?: LookupParams) => getLookup('stockstatus', params)

// ===== 仓位信息（启用仓位管理时维护：仓位集 + 仓位集值）=====
export interface WarehouseFlexDetail {
    uid?: string
    fFlexEntryId: string
    fNumber?: string
    fName?: string
    fDescription?: string
    _key?: number  // 前端行标识（稳定 key，不入库）
}
export interface WarehouseFlexItem {
    uid?: string
    fFlexId: string
    fNumber?: string
    fName?: string
    fDescription?: string
    fIsMustInput?: boolean
    details: WarehouseFlexDetail[]
    _key?: number  // 前端行标识（稳定 key，不入库）
}

// GET /api/warehouse/{id}/flex - 仓位信息（仓位集含其值）
export const getWarehouseFlex = (id: string) => {
    return request({ url: `/warehouse/${id}/flex`, method: 'get' })
}

// PUT /api/warehouse/{id}/flex - 保存仓位信息（整组替换）
export const saveWarehouseFlex = (id: string, items: any[]) => {
    return request({ url: `/warehouse/${id}/flex`, method: 'put', data: { items } })
}

export const getWarehouses = (params?: any) => {
    return request({
        url: '/warehouse',
        method: 'get',
        params: {
            pageIndex: params?.page || 1,
            pageSize: params?.pageSize || 10,
            keyword: params?.keyword || '',
            groupId: params?.groupId || '',
            dynamicFilters: params?.dynamicFilters || [],
            sortField: params?.sortField,
            isAsc: params?.isAsc
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

export const enableWarehouse = (id: string) => {
    return request({
        url: `/warehouse/${id}/enable`,
        method: 'put'
    })
}

// GET /api/warehouse/fields - 获取模型字段数据类型
export const getWarehousesFields = () => {
    return request({
        url: '/warehouse/fields',
        method: 'get'
    })
}
