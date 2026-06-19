import request from '../utils/request'

// 编码规则可用动态字段（白名单项）
export interface BillCodeField {
    fieldId: string
    fieldName: string
    // text=文本字段(段类型2) date=日期字段(段类型3)
    kind: string
    sample?: string
}

// 编码规则分段（SYS_LISTCODEENTRY / SYS_BARCODEENTRY 同构）
export interface BillCodeSegment {
    // 段类型：1=常量 2=文本字段 3=日期字段 4=流水号
    ftype: string
    fieldid: string
    fieldName: string
    fvalue: string
    // 长度：流水段=位数补零；字段段=截取/补位长度（0=不限）
    flenght: number
    fmin: number
    fmax: number
    fstep: number
    // 字段段 U=大写 L=小写；日期段为日期格式（如 yyyyMMdd）
    formatString: string
    fchar: string
    fcharAlignment: string
    // 编码依据：日期段勾选=按日重置；字段段勾选=按字段值分组取号
    isSerial: boolean
    isMember: boolean
    fnote: string
}

export interface BillCodeRuleSection {
    fname: string
    ismodify: boolean
    // 是否禁用该规则（禁用后取号被拦截）
    disabled?: boolean
    fnote: string
    segments: BillCodeSegment[]
}

export interface BillCodeRuleRow {
    formKey: string
    formName: string
    billConfigured: boolean
    billRuleName: string
    billSummary: string
    barcodeConfigured: boolean
    barcodeRuleName: string
    barcodeSummary: string
    billDisabled?: boolean
    barcodeDisabled?: boolean
    mYmd?: string | null
}

export interface BillCodeRuleDetail {
    formKey: string
    formName: string
    bill: BillCodeRuleSection
    barcode: BillCodeRuleSection
    billFields: BillCodeField[]
    barcodeFields: BillCodeField[]
}

// 取号预览（按已保存规则读当前流水算下一个号，不占号）
export interface BillCodePreview {
    configured: boolean
    disabled: boolean
    basisKey: string
    currentSeq: number
    nextNo: string
    byteLength: number
    message: string
}

export interface BillCodeRulePreview {
    bill: BillCodePreview
    barcode: BillCodePreview
}

// 登记业务表单请求（数据驱动目录）
export interface BillCodeFormRegister {
    formKey: string
    formName: string
    // 单据表头实体类名（选填，填了基类即可按实体反射自动取号）
    entityName?: string
}

// 可登记单据花名册项（反射系统已注册单据 + 登记状态）
export interface BillCodeDocument {
    formKey: string
    formName: string
    entityName: string
    tableName: string
    registered: boolean
}

// GET /api/billcoderule/document-types - 可登记单据花名册
export const getBillCodeDocumentTypes = () => {
    return request({ url: '/billcoderule/document-types', method: 'get' })
}

// GET /api/billcoderule - 已注册业务表单 + 两套规则配置概览
export const getBillCodeRules = () => {
    return request({ url: '/billcoderule', method: 'get' })
}

// POST /api/billcoderule/forms - 登记/更新一个可配置业务表单
export const registerBillCodeForm = (data: BillCodeFormRegister) => {
    return request({ url: '/billcoderule/forms', method: 'post', data })
}

// DELETE /api/billcoderule/forms/{formKey} - 注销业务表单（移出目录，不动规则与流水）
export const unregisterBillCodeForm = (formKey: string) => {
    return request({ url: `/billcoderule/forms/${formKey}`, method: 'delete' })
}

// GET /api/billcoderule/{formKey} - 规则详情（含可用字段白名单）
export const getBillCodeRuleDetail = (formKey: string) => {
    return request({ url: `/billcoderule/${formKey}`, method: 'get' })
}

// PUT /api/billcoderule/{formKey} - 保存（单据编号 + 条码编号）
export const saveBillCodeRule = (formKey: string, data: { bill: BillCodeRuleSection; barcode: BillCodeRuleSection }) => {
    return request({ url: `/billcoderule/${formKey}`, method: 'put', data })
}

// GET /api/billcoderule/{formKey}/preview - 取号预览（不占号）
export const getBillCodeRulePreview = (formKey: string) => {
    return request({ url: `/billcoderule/${formKey}/preview`, method: 'get' })
}

// DELETE /api/billcoderule/{formKey} - 清除配置（软删规则头+分段，不动流水）
export const deleteBillCodeRule = (formKey: string) => {
    return request({ url: `/billcoderule/${formKey}`, method: 'delete' })
}

// POST /api/billcoderule/{formKey}/reset-seq - 重置流水（下次取号从起始号重新计）
export const resetBillCodeSeq = (formKey: string) => {
    return request({ url: `/billcoderule/${formKey}/reset-seq`, method: 'post' })
}
