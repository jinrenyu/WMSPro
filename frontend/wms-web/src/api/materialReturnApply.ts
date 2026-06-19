import request from '../utils/request'

// 退料申请单明细行
export interface MaterialReturnApplyEntry {
    uid?: string
    fentryid?: number
    frowtype?: string
    fmaterialid: string
    fmaterialNumber?: string
    fmaterialName?: string
    fSpecification?: string
    fisBatchManage?: boolean
    fisKfPeriod?: boolean
    fKfPeriod?: number
    fKfUnit?: number
    fauxpropid?: string
    fauxpropName?: string
    fisAuxEnabled?: boolean
    flot?: string
    fmrappqty: number
    fmrqty?: number
    fmrbqty?: number
    freplenishqty?: number
    fstockqty?: number
    fbaseunitqty?: number
    funitid: string
    funitNumber?: string
    funitName?: string
    fbaseunitid?: string
    fstockunitid?: string
    fstockunitName?: string
    fstockid?: string
    fstockNumber?: string
    fstockName?: string
    fisOpenLocation?: boolean
    fstocklocid?: string
    fstocklocName?: string
    fprice?: number
    ftaxrate?: number
    ftaxprice?: number
    fdiscountrate?: number
    fdiscount?: number
    ftaxamount?: number
    famount?: number
    fallamount?: number
    fsrcformid?: string
    fsrcbillno?: string
    forderbillno?: string
    forderentryid?: number
    forderinterid?: string
    forderdetailid?: string
    fnote?: string
    _key?: number
}

// 退料申请单详情（主表 + 名称 + 明细）
export interface MaterialReturnApplyDetail {
    uid?: string
    fbillno?: string
    fbilltypeid?: string
    fbilltypeName?: string
    fdate?: string | null
    fStatus?: number
    fstatusName?: string
    foastatus?: string
    foaresult?: string
    fbusinesstype?: string
    frmtype?: string
    frmmode?: string
    freplenishmode?: string
    frequireorgid?: string
    frequireorgName?: string
    fpurchaseorgid?: string
    fpurchaseorgName?: string
    fstockorgid?: string
    fstockorgName?: string
    fappdeptid?: string
    fappdeptName?: string
    fsupplyid?: string
    fsupplyNumber?: string
    fsupplyName?: string
    fcurrencyid?: string
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
    entries?: MaterialReturnApplyEntry[]
}

// GET /api/materialreturnapply - 分页列表（按明细行展开）
export const getMaterialReturnApplies = (params?: any) => {
    return request({
        url: '/materialreturnapply',
        method: 'get',
        params: {
            pageIndex: params?.page || 1,
            pageSize: params?.pageSize || 10,
            keyword: params?.keyword || '',
            dynamicFilters: params?.dynamicFilters || [],
            sortField: params?.sortField,
            isAsc: params?.isAsc,
            onlyApproved: params?.onlyApproved || false
        }
    })
}

// GET /api/materialreturnapply/{id}
export const getMaterialReturnApply = (id: string) => {
    return request({ url: `/materialreturnapply/${id}`, method: 'get' })
}

// POST /api/materialreturnapply
export const createMaterialReturnApply = (data: any) => {
    return request({ url: '/materialreturnapply', method: 'post', data })
}

// PUT /api/materialreturnapply/{id}
export const updateMaterialReturnApply = (id: string, data: any) => {
    return request({ url: `/materialreturnapply/${id}`, method: 'put', data })
}

// DELETE /api/materialreturnapply/{id}
export const deleteMaterialReturnApply = (id: string) => {
    return request({ url: `/materialreturnapply/${id}`, method: 'delete' })
}

// POST /api/materialreturnapply/{id}/approve - 审核
export const approveMaterialReturnApply = (id: string) => {
    return request({ url: `/materialreturnapply/${id}/approve`, method: 'post' })
}

// POST /api/materialreturnapply/{id}/unapprove - 反审核
export const unapproveMaterialReturnApply = (id: string) => {
    return request({ url: `/materialreturnapply/${id}/unapprove`, method: 'post' })
}

// 物料是否启用辅助属性（复用采购订单已有的通用物料端点）
export { getMaterialAuxEnabled } from './purchaseOrder'
