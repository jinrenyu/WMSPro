import request from '../utils/request'

// 物料汇总明细行（T_PUR_MRBENTRY）
export interface PurchaseReturnMaterialEntry {
    uid?: string
    fentryid?: number
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
    fstockid?: string
    fstockNumber?: string
    fstockName?: string
    fisOpenLocation?: boolean
    fstocklocid?: string
    fstocklocName?: string
    fstockstatusid?: string
    fstockstatusName?: string
    frmrealqty: number
    freplenishqty?: number
    fkeapamtqty?: number
    funitid?: string
    funitNumber?: string
    funitName?: string
    fbaseunitid?: string
    fbaseunitqty?: number
    fcoefficient?: number
    fsecunitid?: string
    fsecunitqty?: number
    fkfdate?: string | null
    fusefuldate?: string | null
    fprice?: number
    ftaxprice?: number
    ftaxrate?: number
    fdiscountrate?: number
    fdiscount?: number
    ftaxamount?: number
    famount?: number
    fallamount?: number
    fsrcformid?: string
    fsrcbillno?: string
    fsrcentryid?: number
    forderbillno?: string
    forderentryid?: number
    forderinterid?: string
    forderdetailid?: string
    _key?: number
}

// 录入明细行（T_PUR_MRBENTRY1，条码级）
export interface PurchaseReturnBarcodeEntry {
    uid?: string
    fentryid?: number
    ftypeid?: number
    fisbox?: boolean
    fboxbarcode?: string
    fbarcode?: string
    fbartype?: number
    fmaterialid?: string
    fmaterialNumber?: string
    fmaterialName?: string
    fSpecification?: string
    fisBatchManage?: boolean
    fisKfPeriod?: boolean
    fKfPeriod?: number
    fKfUnit?: number
    fisSecUnit?: boolean
    fauxpropid?: string
    fauxpropName?: string
    flot?: string
    fstockid?: string
    fstockNumber?: string
    fstockName?: string
    fisOpenLocation?: boolean
    fstocklocid?: string
    fstocklocName?: string
    fqty?: number
    funitid?: string
    funitNumber?: string
    funitName?: string
    fbaseunitid?: string
    fbaseunitqty?: number
    fsecunitid?: string
    fsecunitNumber?: string
    fsecunitName?: string
    fsecunitqty?: number
    fstockstatusid?: string
    fstockstatusName?: string
    fsupplyid?: string
    fkfdate?: string | null
    fusefuldate?: string | null
    ftaxprice?: number
    ftaxrate?: number
    fdiscountrate?: number
    fsrcformid?: string
    fsrcbillno?: string
    fsrcentryid?: number
    fsrcdetailid?: string
    fordertypeid?: string
    forderinterid?: string
    forderbillno?: string
    forderdetailid?: string
    forderentryid?: number
    _key?: number
}

// 底阶条码明细行（T_PUR_MRBENTRY2）
export interface PurchaseReturnBottomEntry {
    uid?: string
    fentryid?: number
    fboxbarcode?: string
    fbarcode?: string
    fmaterialid?: string
    fmaterialNumber?: string
    fmaterialName?: string
    fauxpropid?: string
    flot?: string
    fkfdate?: string | null
    fusefuldate?: string | null
    fqty?: number
    fstockid?: string
    fstocklocid?: string
    fsupplyid?: string
    funitid?: string
    fbaseunitid?: string
    fbaseunitqty?: number
    ftaxprice?: number
    ftaxrate?: number
    fdiscountrate?: number
    fstockstatusid?: string
    fsrcformid?: string
    fsrcbillno?: string
    fsrcentryid?: number
    fsrcdetailid?: string
    fordertypeid?: string
    forderinterid?: string
    forderbillno?: string
    forderdetailid?: string
    forderentryid?: number
}

// 采购退料单详情（主表 + 名称 + 三套明细）
export interface PurchaseReturnDetail {
    uid?: string
    fbillno?: string
    fbilltypeid?: string
    fbilltypeName?: string
    fdate?: string | null
    fStatus?: number
    fstatusName?: string
    ftypeid?: number
    fbusinesstype?: string
    fmrtype?: string
    fmrmode?: string
    freplenishmode?: string
    fmrreason?: string
    fsrcformid?: string
    fsrcformName?: string
    fsrcbillno?: string
    frequireorgid?: string
    frequireorgName?: string
    fpurchaseorgid?: string
    fpurchaseorgName?: string
    fcompanyid?: string
    fcompanyName?: string
    fpurchasedeptid?: string
    fpurchasedeptName?: string
    fmrdeptid?: string
    fmrdeptName?: string
    fpurchaserid?: string
    fpurchaserName?: string
    fstockerid?: string
    fstockerName?: string
    fsupplyid?: string
    fsupplyNumber?: string
    fsupplyName?: string
    fcurrencyid?: string
    fcurrencyNumber?: string
    fcurrencyName?: string
    fexchangetypeid?: string
    fexchangerate?: string
    fstockid?: string
    fstockNumber?: string
    fstockName?: string
    fisOpenLocation?: boolean
    fstocklocid?: string
    fstocklocName?: string
    fstockstatusid?: string
    fstockstatusName?: string
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
    entries?: PurchaseReturnMaterialEntry[]
    barcodeEntries?: PurchaseReturnBarcodeEntry[]
    bottomEntries?: PurchaseReturnBottomEntry[]
}

export interface SourceBillType {
    value: string
    label: string
}

// GET /api/purchasereturn - 分页列表（按物料汇总明细行展开）
export const getPurchaseReturns = (params?: any) => {
    return request({
        url: '/purchasereturn',
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

// GET /api/purchasereturn/{id}
export const getPurchaseReturn = (id: string) => request({ url: `/purchasereturn/${id}`, method: 'get' })

// POST /api/purchasereturn
export const createPurchaseReturn = (data: any) => request({ url: '/purchasereturn', method: 'post', data })

// PUT /api/purchasereturn/{id}
export const updatePurchaseReturn = (id: string, data: any) => request({ url: `/purchasereturn/${id}`, method: 'put', data })

// DELETE /api/purchasereturn/{id}
export const deletePurchaseReturn = (id: string) => request({ url: `/purchasereturn/${id}`, method: 'delete' })

// POST /api/purchasereturn/{id}/approve - 审核（退料出库过账）
export const approvePurchaseReturn = (id: string) => request({ url: `/purchasereturn/${id}/approve`, method: 'post' })

// POST /api/purchasereturn/{id}/unapprove - 反审核（冲回）
export const unapprovePurchaseReturn = (id: string) => request({ url: `/purchasereturn/${id}/unapprove`, method: 'post' })

// POST /api/purchasereturn/scan - 扫码解析（仅已入库条码可退料；fsrcformid/fsrcbillno 为表头源单，用于校验条码归属）
export const scanBarcode = (data: { fbarcode: string; fstockid?: string; fstocklocid?: string; fstockstatusid?: string; fsrcformid?: string; fsrcbillno?: string }) =>
    request({ url: '/purchasereturn/scan', method: 'post', data })

// GET /api/purchasereturn/source-bill-types - 源单类型（数据驱动）
export const getSourceBillTypes = () => request({ url: '/purchasereturn/source-bill-types', method: 'get' })

// 物料是否启用辅助属性（复用采购订单已有的通用物料端点）
export { getMaterialAuxEnabled } from './purchaseOrder'
