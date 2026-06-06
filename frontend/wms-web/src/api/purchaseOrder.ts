import request from '../utils/request'

// 采购订单明细行
export interface PurchaseOrderEntry {
    uid?: string
    fentryid?: number
    fmaterialid: string
    fmaterialNumber?: string
    fmaterialName?: string
    fSpecification?: string
    fisBatchManage?: boolean
    flot?: string
    fauxpropid?: string
    fauxpropName?: string
    fisAuxEnabled?: boolean
    fisKfPeriod?: boolean
    fKfPeriod?: number
    fKfUnit?: number
    fqty: number
    finstockqty?: number
    funitid: string
    funitNumber?: string
    funitName?: string
    ftaxprice?: number
    ftaxrate?: number
    fprice?: number
    fdiscountrate?: number
    ftaxamount?: number
    famount?: number
    fallamount?: number
    fdeliverydate?: string | null
    fsupplierlot?: string
    fnote?: string
    _key?: number
}

// 采购订单详情（主表 + 名称 + 明细）
export interface PurchaseOrderDetail {
    uid?: string
    fbillno?: string
    fbilltypeid?: string
    fbilltypeName?: string
    fdate?: string | null
    fStatus?: number
    fstatusName?: string
    fbusinesstype?: string
    fcompanyid?: string
    fcompanyName?: string
    fpurchasedeptid?: string
    fpurchasedeptName?: string
    fpurchaserid?: string
    fpurchaserName?: string
    fsupplyid?: string
    fsupplyNumber?: string
    fsupplyName?: string
    fsettlecurrid?: string
    fcurrencyNumber?: string
    fcurrencyName?: string
    fexchangetypeid?: string
    fexchangerate?: number
    fnote?: string
    cUser?: string
    cuserName?: string
    cYmd?: string
    fcheckerid?: string
    fcheckerName?: string
    fcheckdate?: string
    mUser?: string
    muserName?: string
    mYmd?: string
    fdisableid?: string
    fdisableName?: string
    fdisabledate?: string
    fDisabled?: boolean
    entries?: PurchaseOrderEntry[]
}

// GET /api/purchaseorder - 分页列表（按明细行展开）
export const getPurchaseOrders = (params?: any) => {
    return request({
        url: '/purchaseorder',
        method: 'get',
        params: {
            pageIndex: params?.page || 1,
            pageSize: params?.pageSize || 10,
            keyword: params?.keyword || '',
            dynamicFilters: params?.dynamicFilters || [],
            sortField: params?.sortField,
            isAsc: params?.isAsc
        }
    })
}

// GET /api/purchaseorder/{id}
export const getPurchaseOrder = (id: string) => {
    return request({ url: `/purchaseorder/${id}`, method: 'get' })
}

// POST /api/purchaseorder
export const createPurchaseOrder = (data: any) => {
    return request({ url: '/purchaseorder', method: 'post', data })
}

// PUT /api/purchaseorder/{id}
export const updatePurchaseOrder = (id: string, data: any) => {
    return request({ url: `/purchaseorder/${id}`, method: 'put', data })
}

// DELETE /api/purchaseorder/{id}
export const deletePurchaseOrder = (id: string) => {
    return request({ url: `/purchaseorder/${id}`, method: 'delete' })
}

// POST /api/purchaseorder/{id}/approve - 审核
export const approvePurchaseOrder = (id: string) => {
    return request({ url: `/purchaseorder/${id}/approve`, method: 'post' })
}

// POST /api/purchaseorder/{id}/unapprove - 反审核
export const unapprovePurchaseOrder = (id: string) => {
    return request({ url: `/purchaseorder/${id}/unapprove`, method: 'post' })
}

// GET /api/material/{id}/aux-enabled - 物料是否启用辅助属性
export const getMaterialAuxEnabled = (materialId: string) => {
    return request({ url: `/material/${materialId}/aux-enabled`, method: 'get' })
}
