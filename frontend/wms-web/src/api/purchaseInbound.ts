import request from '../utils/request'

// 物料汇总明细行（T_STK_INSTOCKENTRY）
export interface InStockMaterialEntry {
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
    frealqty: number
    fmustqty?: number
    funitid?: string
    funitNumber?: string
    funitName?: string
    fbaseunitid?: string
    fbaseunitqty?: number
    fsecunitid?: string
    fsecunitqty?: number
    fwwintype?: string
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

// 录入明细行（T_STK_INSTOCKENTRY1，条码级）
export interface InStockBarcodeEntry {
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
    fwwintype?: string
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
    // 前端携带：该录入行展开的底阶条码（提交时随行回传）
    bottoms?: InStockBottomEntry[]
    _key?: number
}

// 底阶条码明细行（T_STK_INSTOCKENTRY2）
export interface InStockBottomEntry {
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

// 采购入库单详情（主表 + 名称 + 三套明细）
export interface InStockDetail {
    uid?: string
    fbillno?: string
    fbilltypeid?: string
    fbilltypeName?: string
    fdate?: string | null
    fStatus?: number
    fstatusName?: string
    ftypeid?: number
    fbusinesstype?: string
    fsrcformid?: string
    fsrcformName?: string
    fsrcbillno?: string
    fdemandorgid?: string
    fdemandorgName?: string
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
    fempid?: string
    fempName?: string
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
    entries?: InStockMaterialEntry[]
    barcodeEntries?: InStockBarcodeEntry[]
    bottomEntries?: InStockBottomEntry[]
}

export interface SourceBillType {
    value: string
    label: string
}

// GET /api/instock - 分页列表（按物料汇总明细行展开）
export const getInStocks = (params?: any) => {
    return request({
        url: '/instock',
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

// GET /api/instock/{id}
export const getInStock = (id: string) => request({ url: `/instock/${id}`, method: 'get' })

// POST /api/instock
export const createInStock = (data: any) => request({ url: '/instock', method: 'post', data })

// PUT /api/instock/{id}
export const updateInStock = (id: string, data: any) => request({ url: `/instock/${id}`, method: 'put', data })

// DELETE /api/instock/{id}
export const deleteInStock = (id: string) => request({ url: `/instock/${id}`, method: 'delete' })

// POST /api/instock/{id}/approve - 审核
export const approveInStock = (id: string) => request({ url: `/instock/${id}/approve`, method: 'post' })

// POST /api/instock/{id}/unapprove - 反审核
export const unapproveInStock = (id: string) => request({ url: `/instock/${id}/unapprove`, method: 'post' })

// POST /api/instock/scan - 扫码解析（fsrcformid/fsrcbillno 为表头源单，用于校验条码归属）
export const scanBarcode = (data: { fbarcode: string; fstockid?: string; fstocklocid?: string; fstockstatusid?: string; fsrcformid?: string; fsrcbillno?: string }) =>
    request({ url: '/instock/scan', method: 'post', data })

// GET /api/instock/source-bill-types - 源单类型（数据驱动）
export const getSourceBillTypes = () => request({ url: '/instock/source-bill-types', method: 'get' })

// 物料是否启用辅助属性（复用采购订单已有的通用物料端点）
export { getMaterialAuxEnabled } from './purchaseOrder'
