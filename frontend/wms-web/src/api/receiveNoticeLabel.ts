import request from '../utils/request'

// 列表行（已审非禁用收料通知单按明细行展开）
export interface ReceiveNoticeLabelRow {
    uid: string
    entryUid: string
    fentryid?: number
    fdate?: string | null
    fbillno?: string
    fpurorgName?: string
    fmaterialid?: string
    fmaterialNumber?: string
    fmaterialName?: string
    fSpecification?: string
    fauxpropName?: string
    flot?: string
    funitName?: string
    fsupplyNumber?: string
    fsupplyName?: string
    factreceiveqty?: number
    finstockqty?: number
    fkfdate?: string | null
    fisKfPeriod?: boolean
    fKfPeriod?: number
    fKfUnit?: number
    fincreaseqty?: number
    fStatus?: number
    fstatusName?: string
    fDisabled?: boolean
    cuserName?: string
    cYmd?: string | null
    fcheckerName?: string
    fcheckdate?: string | null
}

// 条码生成页单据头
export interface ReceiveNoticeLabelHead {
    uid: string
    entryUid: string
    fentryid?: number
    fbillno?: string
    fdate?: string | null
    fpurorgName?: string
    fbusinesstype?: string
    fsupplyid?: string
    fsupplyNumber?: string
    fsupplyName?: string
    fmaterialid?: string
    fmaterialNumber?: string
    fmaterialName?: string
    fSpecification?: string
    fauxpropid?: string
    fauxpropName?: string
    factreceiveqty?: number
    funitid?: string
    funitName?: string
    fbaseunitid?: string
    fbaseunitName?: string
    fisBatchManage?: boolean
    flot?: string
    fisKfPeriod?: boolean
    fkfperiod?: number
    fkfunit?: number
    fkfdate?: string | null
    fexpiredate?: string | null
    fbartype?: number
    fincreaseqty?: number
    generatedCount?: number
}

// 条码明细行（与采购订单标签共用结构，含 IQC 检验员/日期）
export interface BarcodeLine {
    uid: string
    fbarcode: string
    fbarcodestatus?: number
    fbarcodestatusName?: string
    fstockstatus?: number
    fstockstatusName?: string
    fcompanyName?: string
    fdate?: string | null
    fmaterialNumber?: string
    fmaterialName?: string
    fSpecification?: string
    flot?: string
    fauxpropName?: string
    fqty?: number
    funitName?: string
    fbaseqty?: number
    fbaseunitName?: string
    fkfdate?: string | null
    fkfperiod?: number
    fusefuldate?: string | null
    fsecunitqty?: number
    fpobillno?: string
    fpurbillno?: string
    fpoentreyid?: number
    finspectName?: string
    finspectdate?: string | null
}

// GET /api/receivenoticelabel - 列表（按明细行展开）
export const getReceiveNoticeLabels = (params?: any) => {
    return request({
        url: '/receivenoticelabel',
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

// GET /api/receivenoticelabel/head/{entryUid}
export const getLabelHead = (entryUid: string) => {
    return request({ url: `/receivenoticelabel/head/${entryUid}`, method: 'get' })
}

// GET /api/receivenoticelabel/barcodes/{receiveEntryUid}
export const getLabelBarcodes = (receiveEntryUid: string) => {
    return request({ url: `/receivenoticelabel/barcodes/${receiveEntryUid}`, method: 'get' })
}

// POST /api/receivenoticelabel/generate
export const generateBarcodes = (data: any) => {
    return request({ url: '/receivenoticelabel/generate', method: 'post', data })
}

// POST /api/receivenoticelabel/void
export const voidBarcodes = (barcodes: string[]) => {
    return request({ url: '/receivenoticelabel/void', method: 'post', data: { barcodes } })
}

// POST /api/receivenoticelabel/unvoid
export const unvoidBarcodes = (barcodes: string[]) => {
    return request({ url: '/receivenoticelabel/unvoid', method: 'post', data: { barcodes } })
}

// POST /api/receivenoticelabel/print
export const printBarcodes = (barcodes: string[]) => {
    return request({ url: '/receivenoticelabel/print', method: 'post', data: { barcodes } })
}
