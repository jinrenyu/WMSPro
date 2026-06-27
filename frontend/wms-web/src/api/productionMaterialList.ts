import request from '../utils/request'

// 生产用料清单明细行
export interface ProductionMaterialListEntry {
    uid?: string
    fentryid?: number
    freplacegroup?: number
    fmaterialtype?: string
    fmaterialid?: string
    fmaterialNumber?: string
    fmaterialName?: string
    fSpecification?: string
    fisBatchManage?: boolean
    fisAuxEnabled?: boolean
    foperid?: string
    foperNumber?: string
    foperName?: string
    fbaseunitid?: string
    fbaseunitNumber?: string
    fbaseunitName?: string
    fbaseqty?: number
    fcommonunitid?: string
    fcommonunitNumber?: string
    fcommonunitName?: string
    flot?: string
    fauxpropid?: string
    fauxpropName?: string
    fuserate?: number
    fnumerator?: number
    fdenominator?: number
    fscraprate?: number
    ffixscrapqty?: number
    fmustqty?: number
    fpickedqty?: number
    fnopickedqty?: number
    fsenditemdate?: string | null
    fstockid?: string
    fstockNumber?: string
    fstockName?: string
    fisOpenLocation?: boolean
    fstocklocid?: string
    fstocklocName?: string
    fbackflush?: number
    ffeedsn?: boolean
    fothersn?: boolean
    fnote?: string
    _key?: number
}

// 生产用料清单详情（主表 + 名称 + 明细）
export interface ProductionMaterialListDetail {
    uid?: string
    fbillno?: string
    fdate?: string | null
    fStatus?: number
    fstatusName?: string
    fmobillno?: string
    fmoid?: string
    fmoentryid?: string
    fmoentryseq?: number
    fmaterialid?: string
    fmaterialNumber?: string
    fmaterialName?: string
    fSpecification?: string
    fisAuxEnabled?: boolean
    fauxpropid?: string
    fauxpropName?: string
    fmotype?: string
    fmotypeNumber?: string
    fmotypeName?: string
    fqty?: number
    funitid?: string
    funitNumber?: string
    funitName?: string
    fcompanyid?: string
    fcompanyName?: string
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
    entries?: ProductionMaterialListEntry[]
}

// GET /api/prodmateriallist - 分页列表（一行一张主单）
export const getProductionMaterialLists = (params?: any) => {
    return request({
        url: '/prodmateriallist',
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

// GET /api/prodmateriallist/{id}
export const getProductionMaterialList = (id: string) => {
    return request({ url: `/prodmateriallist/${id}`, method: 'get' })
}

// POST /api/prodmateriallist
export const createProductionMaterialList = (data: any) => {
    return request({ url: '/prodmateriallist', method: 'post', data })
}

// PUT /api/prodmateriallist/{id}
export const updateProductionMaterialList = (id: string, data: any) => {
    return request({ url: `/prodmateriallist/${id}`, method: 'put', data })
}

// DELETE /api/prodmateriallist/{id}
export const deleteProductionMaterialList = (id: string) => {
    return request({ url: `/prodmateriallist/${id}`, method: 'delete' })
}

// POST /api/prodmateriallist/{id}/approve - 审核
export const approveProductionMaterialList = (id: string) => {
    return request({ url: `/prodmateriallist/${id}/approve`, method: 'post' })
}

// POST /api/prodmateriallist/{id}/unapprove - 反审核
export const unapproveProductionMaterialList = (id: string) => {
    return request({ url: `/prodmateriallist/${id}/unapprove`, method: 'post' })
}

// GET /api/material/{id}/aux-enabled - 物料是否启用辅助属性
export const getMaterialAuxEnabled = (materialId: string) => {
    return request({ url: `/material/${materialId}/aux-enabled`, method: 'get' })
}
