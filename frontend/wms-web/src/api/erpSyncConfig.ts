import request from '../utils/request'

// 字段映射行（从2 T_SYN_INFOENTRY1）
export interface ErpSyncFieldMap {
    uid?: string
    fentryid?: number
    fdetailid?: string
    fbodyid?: string
    /** 源数据字段（首字段） */
    fsrcfield: string
    fsecondsrcfield?: string
    fthirdsrcfield?: string
    fsrcfielddes?: string
    fissort?: boolean
    fissrcfieldkey?: boolean
    /** 目标字段 */
    faimfield: string
    faimfieldtype?: string
    faimfielddes?: string
    faimfieldkey?: boolean
    fmustinput?: boolean
    fdefaultvalue?: string
    fissetfixed?: boolean
    ffixedvalue?: string
    flookupclassid?: string
    flookupfield?: string
    flookupsavefield?: string
    fisinnerid?: boolean
    fislogshow?: boolean
    /** 字段类型 0=标准/1=辅助属性/2=仓位/4=仓库/3=自定义 */
    ffieldtype?: number
    ferpseq?: string
    _key?: number
}

// 实体信息行（从1 T_SYN_INFOENTRY）+ 内嵌字段映射
export interface ErpSyncEntity {
    uid?: string
    fentryid?: number
    fdetailid?: string
    /** 是否主表 */
    fismain?: boolean
    /** 源数据类型 0=自定义/1=表/2=视图 */
    fsrcdatatype?: number
    fsrcdataname?: string
    fsrcdatades?: string
    fsrcdatakey?: string
    ftimestampname?: string
    filter?: string
    /** 目标数据类型 1=表 */
    faimdatatype?: number
    /** O3目标实体（表名） */
    faimdataname?: string
    faimdatades?: string
    /** 目标唯一标识（逗号分隔） */
    faimdatakey?: string
    frelationfield?: string
    fiscover?: boolean
    /** ERP查询标识脚本 */
    fieldkeys?: string
    /** ERP表单代码 */
    ferpbillid?: string
    forderstring?: string
    fruleid?: string
    fieldMaps?: ErpSyncFieldMap[]
    _key?: number
}

// ERP集成配置详情（主表 + 实体信息 内嵌字段映射）
export interface ErpSyncConfigDetail {
    uid?: string
    fnumber?: string
    fname?: string
    ferpbillid?: string
    fstart?: number
    fsynrate?: number
    ftimetype?: string
    fruleid?: string
    fisenable?: boolean
    isdefault?: boolean
    fnote?: string
    ftimestamp?: string | null
    fStatus?: number
    fstatusName?: string
    cYmd?: string
    cUser?: string
    cuserName?: string
    mYmd?: string
    mUser?: string
    entities?: ErpSyncEntity[]
}

// GET /api/erpsyncconfig - 分页列表（按主表一行）
export const getErpSyncConfigs = (params?: any) => {
    return request({
        url: '/erpsyncconfig',
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

export const getErpSyncConfig = (id: string) => {
    return request({ url: `/erpsyncconfig/${id}`, method: 'get' })
}

export const createErpSyncConfig = (data: any) => {
    return request({ url: '/erpsyncconfig', method: 'post', data })
}

export const updateErpSyncConfig = (id: string, data: any) => {
    return request({ url: `/erpsyncconfig/${id}`, method: 'put', data })
}

export const deleteErpSyncConfig = (id: string) => {
    return request({ url: `/erpsyncconfig/${id}`, method: 'delete' })
}

export const approveErpSyncConfig = (id: string) => {
    return request({ url: `/erpsyncconfig/${id}/approve`, method: 'post' })
}

export const unapproveErpSyncConfig = (id: string) => {
    return request({ url: `/erpsyncconfig/${id}/unapprove`, method: 'post' })
}

// POST /api/erpsyncconfig/{id}/sync - 手动执行同步
// 同步是长耗时操作（从 ERP 分页取数 + 逐条 upsert，可能上千条），单独放宽超时到 30 分钟，
// 避免默认 10s 超时导致前端报 "timeout of 10000ms exceeded"（后端其实仍在执行）。
export const syncErpSyncConfig = (id: string) => {
    return request({ url: `/erpsyncconfig/${id}/sync`, method: 'post', timeout: 1800000 })
}

// 目标目录 / 功能代码 下拉
export const getErpTargetTables = (keyword?: string) => {
    return request({ url: '/erpsyncconfig/target-tables', method: 'get', params: { keyword } })
}

export const getErpTargetFields = (table: string) => {
    return request({ url: '/erpsyncconfig/target-fields', method: 'get', params: { table } })
}

export const getErpBillTemplates = (keyword?: string) => {
    return request({ url: '/erpsyncconfig/bill-templates', method: 'get', params: { keyword } })
}
