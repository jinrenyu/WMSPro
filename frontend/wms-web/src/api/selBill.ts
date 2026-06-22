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
