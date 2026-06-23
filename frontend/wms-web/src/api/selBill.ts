import request from '../utils/request'

// 出入库流程配置-单据类型映射明细行
export interface SelBillEntry {
    uid?: string
    fentryid?: number
    /** 源单类型（T_BAS_BILLTYPE.Uid） */
    fsourceid: string
    fsourceNumber?: string
    fsourceName?: string
    /** 目标单据类型（T_BAS_BILLTYPE.Uid） */
    fdestid: string
    fdestNumber?: string
    fdestName?: string
    fdefault?: boolean
    _key?: number
}

// 出入库流程配置详情（主表 + 名称 + 明细）
export interface SelBillDetail {
    uid?: string
    fnumber?: string
    fname?: string
    fsourcetrantype?: string
    fsourcetypeName?: string
    fdesttrantype?: string
    fdesttranName?: string
    // 基本页签开关
    fisuse?: boolean
    fdefault?: boolean
    fisopensource?: boolean
    fcheck?: boolean
    fiscontrolqty?: boolean
    fisdefaultstock?: boolean
    fcansynerp?: boolean
    fcheckerpstock?: boolean
    fischecklot?: boolean
    fischeckaux?: boolean
    fischeckkfdate?: boolean
    fisusecoderule?: boolean
    fcanpushdown?: boolean
    fiswholeout?: boolean
    fkind?: string
    // Wise配置
    fproname?: string
    // 状态
    fStatus?: number
    fstatusName?: string
    fDisabled?: boolean
    // 其他页签
    cUser?: string
    cuserName?: string
    cYmd?: string
    mUser?: string
    muserName?: string
    mYmd?: string
    fcheckerid?: string
    fcheckerName?: string
    fcheckdate?: string
    fdisableid?: string
    fdisableName?: string
    fdisabledate?: string
    entries?: SelBillEntry[]
}

// GET /api/selbill - 分页列表（按主表一行）
export const getSelBills = (params?: any) => {
    return request({
        url: '/selbill',
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

// GET /api/selbill/{id}
export const getSelBill = (id: string) => {
    return request({ url: `/selbill/${id}`, method: 'get' })
}

// POST /api/selbill
export const createSelBill = (data: any) => {
    return request({ url: '/selbill', method: 'post', data })
}

// PUT /api/selbill/{id}
export const updateSelBill = (id: string, data: any) => {
    return request({ url: `/selbill/${id}`, method: 'put', data })
}

// DELETE /api/selbill/{id}
export const deleteSelBill = (id: string) => {
    return request({ url: `/selbill/${id}`, method: 'delete' })
}

// POST /api/selbill/{id}/approve - 审核
export const approveSelBill = (id: string) => {
    return request({ url: `/selbill/${id}/approve`, method: 'post' })
}

// POST /api/selbill/{id}/unapprove - 反审核
export const unapproveSelBill = (id: string) => {
    return request({ url: `/selbill/${id}/unapprove`, method: 'post' })
}

// POST /api/selbill/{id}/disable - 禁用
export const disableSelBill = (id: string) => {
    return request({ url: `/selbill/${id}/disable`, method: 'post' })
}

// POST /api/selbill/{id}/enable - 反禁用
export const enableSelBill = (id: string) => {
    return request({ url: `/selbill/${id}/enable`, method: 'post' })
}

// 下推目标项：某源单类型可下推到的目标单据类型
export interface PushDownTarget {
    value: string   // 目标单据模板编号（SYS_BILLTEMPLATE.FNUMBER）
    label: string   // 目标单据名称
}

// GET /api/selbill/push-targets?sourceType=xxx - 按源单类型查可下推目标（仅启用流程）
export const getPushTargets = (sourceType: string) => {
    return request({ url: '/selbill/push-targets', method: 'get', params: { sourceType } })
}

// GET /api/selbill/trace-targets?sourceType=xxx - 下查取可追溯目标类型（不看启用/禁用，反映历史既成事实）
export const getTraceTargets = (sourceType: string) => {
    return request({ url: '/selbill/trace-targets', method: 'get', params: { sourceType } })
}

// 目标单据模板编号 -> 目标维护页路由名。
// 下推(PushDownDialog)/下查(DrillDownDialog) 共用此映射，后端 SelBillService.GetDownstreamDocsAsync 的 switch 须与之保持一致。
// 新增第三种目标单据时三处同步补一项即可。
export const DOC_TARGET_ROUTE: Record<string, string> = {
    STK_InStock: 'PurchaseInboundEdit',   // 采购入库单
    PUR_MRB: 'PurchaseReturnEdit',        // 采购退料单
}

// 下查结果项：由某源单生成的某一类下游单据（已审核且未禁用）
export interface DownstreamDoc {
    uid: string
    fbillno: string
    fdate?: string | null
    fStatus: number
}

// GET /api/selbill/downstream - 下查（正向追溯）：列出某源单生成的指定类型下游单据
export const getDownstreamDocs = (sourceType: string, sourceBillNo: string, targetType: string) => {
    return request({ url: '/selbill/downstream', method: 'get', params: { sourceType, sourceBillNo, targetType } })
}
