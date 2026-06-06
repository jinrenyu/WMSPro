import request from '../utils/request'

export interface Material {
    uid?: string
    fNumber: string
    fName: string
    fSpecification?: string
    fErpClsId?: string
    fBaseUnitId?: string
    fBaseUnitName?: string
    fTypeId?: string
    fIsBatchManage?: boolean
    fStatus?: number
    fCheckerId?: string
    fCheckDate?: string
    cYmd?: string
    // detail fields
    fMasterId?: string
    fDescription?: string
    fBarcode?: string
    fAbc?: string
    fMaxQty?: number
    fSafeQty?: number
    fNetWeight?: number
    fGrossWeight?: number
    fStoreUnitId?: string
    fStoreUnitName?: string
    fSaleUnitId?: string
    fSaleUnitName?: string
    fPurchaseUnitId?: string
    fPurchaseUnitName?: string
    fIsKfPeriod?: boolean
    fKfUnit?: number
    fKfPeriod?: number
    fMinPoQty?: number
    fIncreaseQty?: number
    fCheckIncoming?: boolean
    fOldNumber?: string
    fDeStockId?: string
    fDeStockName?: string
    fDeSpId?: string
    fDeSpName?: string
    fVPart?: boolean
    fGroupId?: string
    fGroupName?: string
    // ---- 扩展字段（物料维护表单）----
    fChartNumber?: string       // 图号
    fTaxRate?: number           // 税率
    fCompanyId?: string         // 使用组织
    fCompanyName?: string       // 使用组织名称
    fTypeName?: string          // 物料类别名称
    fBarType?: number           // 条码类型
    fSubConUnitId?: string      // 委外单位
    fProduceUnitId?: string     // 生产单位
    fAuxUnitId?: string         // 辅助单位
    fLength?: number            // 长
    fWidth?: number             // 宽
    fHeight?: number            // 高
    fVolume?: number            // 体积
    fLowLimit?: number          // 最低库存量
    fFeedSn?: boolean           // 启用序列号
    fOtherSn?: boolean          // 第三方序列号
    fIsSecUnit?: boolean        // 启用辅助单位
    fIsFeed?: boolean           // 不投料
    fIsPinal?: boolean          // 终检
    fSuite?: boolean            // 套件
    fPrice?: number             // 单价
    // ---- 只读系统信息 ----
    cUser?: string              // 制单人
    mUser?: string              // 修改人
    mYmd?: string               // 修改日期
    fdisableid?: string         // 禁用人
    fdisabledate?: string       // 禁用日期
    fDisabled?: boolean         // 禁用状态
    // ---- 图片（Base64）----
    fPictureBase64?: string | null
}

// 物料维度（辅助属性配置）
export interface MaterialDimension {
    uid?: string
    fAuxPropertyId: string
    fAuxPropertyNumber?: string
    fAuxPropertyName?: string
    fIsEnable: boolean
    fIsComControl: boolean
    fIsMustInput: boolean
    fIsAffectPrice: boolean
    fIsAffectPlan: boolean
    fIsAffectCost: boolean
    fValueSet: boolean
    fEntryId?: number
}

// 辅助单位换算
export interface MaterialSecUnit {
    uid?: string
    fConvertType: string
    fBaseUnitId: string
    fBaseUnitName?: string
    fSecUnitId: string
    fSecUnitName?: string
    fConvertNumerator: number
    fConvertDenominator: number
    fEntryId?: number
}

// GET /api/material - paged list
export const getMaterials = (params?: any) => {
    return request({
        url: '/material',
        method: 'get',
        params: {
            pageIndex: params?.page || 1,
            pageSize: params?.pageSize || 10,
            keyword: params?.keyword || '',
            groupId: params?.groupId || '',
            dynamicFilters: params?.dynamicFilters || [],
            sortField: params?.sortField,
            isAsc: params?.isAsc,
            onlyApproved: params?.onlyApproved || false
        }
    })
}

// GET /api/material/{id}
export const getMaterial = (id: string) => {
    return request({
        url: `/material/${id}`,
        method: 'get'
    })
}

// POST /api/material
export const createMaterial = (data: any) => {
    return request({
        url: '/material',
        method: 'post',
        data
    })
}

// PUT /api/material/{id}
export const updateMaterial = (id: string, data: any) => {
    return request({
        url: `/material/${id}`,
        method: 'put',
        data
    })
}

// DELETE /api/material/{id}
export const deleteMaterial = (id: string) => {
    return request({
        url: `/material/${id}`,
        method: 'delete'
    })
}

// PUT /api/material/{id}/approve - 审核
export const approveMaterial = (id: string) => {
    return request({
        url: `/material/${id}/approve`,
        method: 'put'
    })
}

// PUT /api/material/{id}/unapprove - 反审核
export const unapproveMaterial = (id: string) => {
    return request({
        url: `/material/${id}/unapprove`,
        method: 'put'
    })
}

// PUT /api/material/{id}/disable - 禁用
export const disableMaterial = (id: string) => {
    return request({
        url: `/material/${id}/disable`,
        method: 'put'
    })
}

// PUT /api/material/{id}/enable - 反禁用
export const enableMaterial = (id: string) => {
    return request({
        url: `/material/${id}/enable`,
        method: 'put'
    })
}

// GET /api/material/fields - 获取模型字段数据类型
export const getFields = () => {
    return request({
        url: '/material/fields',
        method: 'get'
    })
}

// GET /api/material/{id}/dimensions - 物料维度列表
export const getMaterialDimensions = (id: string) => {
    return request({ url: `/material/${id}/dimensions`, method: 'get' })
}

// PUT /api/material/{id}/dimensions - 保存物料维度（整组替换）
export const saveMaterialDimensions = (id: string, items: any[]) => {
    return request({ url: `/material/${id}/dimensions`, method: 'put', data: { items } })
}

// GET /api/material/{id}/sec-units - 辅助单位换算列表
export const getMaterialSecUnits = (id: string) => {
    return request({ url: `/material/${id}/sec-units`, method: 'get' })
}

// PUT /api/material/{id}/sec-units - 保存辅助单位换算（整组替换）
export const saveMaterialSecUnits = (id: string, items: any[]) => {
    return request({ url: `/material/${id}/sec-units`, method: 'put', data: { items } })
}
