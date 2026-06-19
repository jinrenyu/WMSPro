import request from '../utils/request'

export interface PrintTemplate {
    uid?: string
    fStatus?: number
    fDisabled?: boolean
    fNumber: string
    fName: string
    fBillSource: string          // 适用单据来源（表单键 PUR_PurchaseOrder / PUR_ReceiveBill）
    fBillSourceName?: string     // 适用单据中文名（服务端解析）
    fPaperWidth?: number
    fPaperHeight?: number
    fIsDefault?: boolean
    fDescription?: string
    cYmd?: string
    // ---- 详情字段 ----
    fTemplate?: string           // 模板 JSON（hiprint）
    fCompanyId?: string
    cUser?: string
    mUser?: string
    mYmd?: string
}

// GET /api/printtemplate
export const getPrintTemplates = (params?: any) => {
    return request({
        url: '/printtemplate',
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

// GET /api/printtemplate/{id}
export const getPrintTemplate = (id: string) => {
    return request({ url: `/printtemplate/${id}`, method: 'get' })
}

// POST /api/printtemplate
export const createPrintTemplate = (data: any) => {
    return request({ url: '/printtemplate', method: 'post', data })
}

// PUT /api/printtemplate/{id}
export const updatePrintTemplate = (id: string, data: any) => {
    return request({ url: `/printtemplate/${id}`, method: 'put', data })
}

// DELETE /api/printtemplate/{id}
export const deletePrintTemplate = (id: string) => {
    return request({ url: `/printtemplate/${id}`, method: 'delete' })
}

// GET /api/printtemplate/by-source/{billSource} - 按适用单据取启用模板（默认在前，含模板 JSON）
export const getPrintTemplatesBySource = (billSource: string) => {
    return request({ url: `/printtemplate/by-source/${billSource}`, method: 'get' })
}

// GET /api/printtemplate/fields - 获取模型字段数据类型
export const getPrintTemplateFields = () => {
    return request({ url: '/printtemplate/fields', method: 'get' })
}
