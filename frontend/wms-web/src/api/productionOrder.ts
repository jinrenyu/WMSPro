import request from '../utils/request'

// 生产订单明细行
export interface ProductionOrderEntry {
    uid?: string
    fentryid?: number
    fproducttype?: string
    fworkshopid?: string
    fworkshopNumber?: string
    fworkshopName?: string
    fmaterialid?: string
    fmaterialNumber?: string
    fmaterialName?: string
    fchartNumber?: string
    fSpecification?: string
    fisBatchManage?: boolean
    fmachinemodel?: string
    flot?: string
    fbaseunitid?: string
    fbaseunitNumber?: string
    fbaseunitName?: string
    fbaseunitqty?: number
    fcommonunitid?: string
    fcommonunitNumber?: string
    fcommonunitName?: string
    fqty?: number
    fisAuxEnabled?: boolean
    fauxpropid?: string
    fauxpropName?: string
    fprorouteid?: string
    fprorouteNumber?: string
    fprorouteName?: string
    fbarcoderuleid?: string
    fbarcoderuleName?: string
    ffcqty?: number
    ftotalfinishqty?: number
    fbstatus?: string
    fbstatusName?: string
    fissuspend?: boolean
    finhighlimit?: number
    fretime?: number
    fbomid?: string
    fbomBillno?: string
    fconveydate?: string | null
    fplanstartdate?: string | null
    fplanfinishdate?: string | null
    factualstartdate?: string | null
    factualfinishdate?: string | null
    ffinalid?: string
    ffinalName?: string
    ffinaldate?: string | null
    fnote?: string
    fstockqty?: number
    fstockbaseqty?: number
    _key?: number
}

// 生产订单详情（主表 + 名称 + 明细）
export interface ProductionOrderDetail {
    uid?: string
    fbillno?: string
    fbilltype?: string
    fbilltypeName?: string
    fdate?: string | null
    fStatus?: number
    fstatusName?: string
    foastatus?: string
    foaresult?: string
    fcompanyid?: string
    fcompanyName?: string
    fplannerid?: string
    fplannerName?: string
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
    entries?: ProductionOrderEntry[]
}

// GET /api/productionorder - 分页列表（按明细行展开）
export const getProductionOrders = (params?: any) => {
    return request({
        url: '/productionorder',
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

// GET /api/productionorder/{id}
export const getProductionOrder = (id: string) => {
    return request({ url: `/productionorder/${id}`, method: 'get' })
}

// POST /api/productionorder
export const createProductionOrder = (data: any) => {
    return request({ url: '/productionorder', method: 'post', data })
}

// PUT /api/productionorder/{id}
export const updateProductionOrder = (id: string, data: any) => {
    return request({ url: `/productionorder/${id}`, method: 'put', data })
}

// DELETE /api/productionorder/{id}
export const deleteProductionOrder = (id: string) => {
    return request({ url: `/productionorder/${id}`, method: 'delete' })
}

// POST /api/productionorder/{id}/approve - 审核
export const approveProductionOrder = (id: string) => {
    return request({ url: `/productionorder/${id}/approve`, method: 'post' })
}

// POST /api/productionorder/{id}/unapprove - 反审核
export const unapproveProductionOrder = (id: string) => {
    return request({ url: `/productionorder/${id}/unapprove`, method: 'post' })
}

// 下达 / 反下达结果
export interface ProductionReleaseResult {
    messages: string[]
    successCount: number
    failCount: number
    billNos: string[]
}

// POST /api/productionorder/{id}/release - 下达（按选中明细行的 BOM 生成生产用料清单）
export const releaseProductionOrder = (id: string, entryUids: string[]) => {
    return request({ url: `/productionorder/${id}/release`, method: 'post', data: { entryUids } })
}

// POST /api/productionorder/{id}/unrelease - 反下达（删除选中明细行对应的生产用料清单）
export const unreleaseProductionOrder = (id: string, entryUids: string[]) => {
    return request({ url: `/productionorder/${id}/unrelease`, method: 'post', data: { entryUids } })
}

// GET /api/material/{id}/aux-enabled - 物料是否启用辅助属性
export const getMaterialAuxEnabled = (materialId: string) => {
    return request({ url: `/material/${materialId}/aux-enabled`, method: 'get' })
}
