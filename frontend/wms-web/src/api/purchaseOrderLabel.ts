import request from '../utils/request'

// 列表行（已审非禁用采购订单按明细行展开）
export interface PurchaseOrderLabelRow {
    uid: string
    entryUid: string
    fentryid?: number
    fdate?: string | null
    fbillno?: string
    fcompanyName?: string
    fmaterialid?: string
    fmaterialNumber?: string
    fmaterialName?: string
    fSpecification?: string
    fauxpropName?: string
    funitName?: string
    fsupplyNumber?: string
    fsupplyName?: string
    fqty?: number
    finstockqty?: number
    fincreaseqty?: number
    fkfperiod?: number
    fStatus?: number
    fstatusName?: string
    fDisabled?: boolean
    cuserName?: string
    cYmd?: string | null
    fcheckerName?: string
    fcheckdate?: string | null
}

// 条码生成页单据头
export interface PurchaseOrderLabelHead {
    uid: string
    entryUid: string
    fentryid?: number
    fbillno?: string
    fdate?: string | null
    fcompanyName?: string
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
    fqty?: number
    funitid?: string
    funitName?: string
    fbaseunitid?: string
    fisBatchManage?: boolean
    flot?: string
    fisKfPeriod?: boolean
    fkfperiod?: number
    fkfunit?: number
    fbartype?: number
    fincreaseqty?: number
    generatedCount?: number
}

// 条码明细行
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
}

// GET /api/purchaseorderlabel - 列表（按明细行展开）
export const getPurchaseOrderLabels = (params?: any) => {
    return request({
        url: '/purchaseorderlabel',
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

// GET /api/purchaseorderlabel/head/{entryUid}
export const getLabelHead = (entryUid: string) => {
    return request({ url: `/purchaseorderlabel/head/${entryUid}`, method: 'get' })
}

// GET /api/purchaseorderlabel/barcodes/{poEntryUid}
export const getLabelBarcodes = (poEntryUid: string) => {
    return request({ url: `/purchaseorderlabel/barcodes/${poEntryUid}`, method: 'get' })
}

// POST /api/purchaseorderlabel/generate
export const generateBarcodes = (data: any) => {
    return request({ url: '/purchaseorderlabel/generate', method: 'post', data })
}

// POST /api/purchaseorderlabel/void
export const voidBarcodes = (barcodes: string[]) => {
    return request({ url: '/purchaseorderlabel/void', method: 'post', data: { barcodes } })
}

// POST /api/purchaseorderlabel/unvoid
export const unvoidBarcodes = (barcodes: string[]) => {
    return request({ url: '/purchaseorderlabel/unvoid', method: 'post', data: { barcodes } })
}

// POST /api/purchaseorderlabel/print
export const printBarcodes = (barcodes: string[]) => {
    return request({ url: '/purchaseorderlabel/print', method: 'post', data: { barcodes } })
}
