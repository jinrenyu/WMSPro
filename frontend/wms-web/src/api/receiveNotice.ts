import request from '../utils/request'

// 收料通知单明细行
export interface ReceiveNoticeEntry {
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
    fcheckincoming?: boolean
    flot?: string
    fsupplylot?: string
    factreceiveqty: number
    finstockqty?: number
    fgodqty?: number
    fscrapqty?: number
    funitid: string
    funitNumber?: string
    funitName?: string
    fbaseunitid?: string
    fbaseunitqty?: number
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
    ftaxamount?: number
    famount?: number
    fallamount?: number
    fpredeliverydate?: string | null
    fkfdate?: string | null
    fexpiredate?: string | null
    forderbillno?: string
    forderentryid?: number
    forderinterid?: string
    forderdetailid?: string
    frepnote?: string
    _key?: number
}

// 收料通知单详情（主表 + 名称 + 明细）
export interface ReceiveNoticeDetail {
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
    fsrcformid?: string
    fsrcbillno?: string
    fdemandorgid?: string
    fdemandorgName?: string
    fpurorgid?: string
    fpurorgName?: string
    freceivedeptid?: string
    freceivedeptName?: string
    fpurdeptid?: string
    fpurdeptName?: string
    freceiverid?: string
    freceiverName?: string
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
    entries?: ReceiveNoticeEntry[]
}

// GET /api/receivenotice - 分页列表（按明细行展开）
export const getReceiveNotices = (params?: any) => {
    return request({
        url: '/receivenotice',
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

// GET /api/receivenotice/{id}
export const getReceiveNotice = (id: string) => {
    return request({ url: `/receivenotice/${id}`, method: 'get' })
}

// POST /api/receivenotice
export const createReceiveNotice = (data: any) => {
    return request({ url: '/receivenotice', method: 'post', data })
}

// PUT /api/receivenotice/{id}
export const updateReceiveNotice = (id: string, data: any) => {
    return request({ url: `/receivenotice/${id}`, method: 'put', data })
}

// DELETE /api/receivenotice/{id}
export const deleteReceiveNotice = (id: string) => {
    return request({ url: `/receivenotice/${id}`, method: 'delete' })
}

// POST /api/receivenotice/{id}/approve - 审核
export const approveReceiveNotice = (id: string) => {
    return request({ url: `/receivenotice/${id}/approve`, method: 'post' })
}

// POST /api/receivenotice/{id}/unapprove - 反审核
export const unapproveReceiveNotice = (id: string) => {
    return request({ url: `/receivenotice/${id}/unapprove`, method: 'post' })
}

// 物料是否启用辅助属性（复用采购订单已有的通用物料端点）
export { getMaterialAuxEnabled } from './purchaseOrder'
