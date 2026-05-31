import request from '../utils/request'

export interface FlexValue {
    uid?: string
    fNumber: string
    fName: string
    fFlexTypeId?: string
    fDescription?: string
    fStatus?: number
    fDisabled?: boolean
    cYmd?: string
    fGroupId?: string
    // 仓位信息详情
    fLength?: number
    fWidth?: number
    fHeight?: number
    fVolume?: number
    fVolumeLimit?: number
    fWeightLimit?: number
    fMixTypeLimit?: string
    // 使用组织 / 审计（只读）
    fCompanyId?: string
    fCompanyName?: string
    cUser?: string
    mUser?: string
    mYmd?: string
    fCheckerId?: string
    fCheckDate?: string
    fdisableid?: string
    fdisabledate?: string
    entries?: FlexValueEntry[]
}

export interface FlexValueEntry {
    uid?: string
    fEntryId?: number
    fNumber: string
    fName: string
    fDescription?: string
    fForbid?: boolean
    _key?: number  // 前端行标识（不入库）
}

export const getFlexValuesList = (params?: any) => {
    return request({
        url: '/flexvalues',
        method: 'get',
        params: {
            pageIndex: params?.page || 1,
            pageSize: params?.pageSize || 10,
            keyword: params?.keyword || '',
            groupId: params?.groupId || '',
            dynamicFilters: params?.dynamicFilters || [],
            sortField: params?.sortField,
            isAsc: params?.isAsc
        }
    })
}

export const getFlexValue = (id: string) => request({ url: `/flexvalues/${id}`, method: 'get' })
export const createFlexValue = (data: any) => request({ url: '/flexvalues', method: 'post', data })
export const updateFlexValue = (id: string, data: any) => request({ url: `/flexvalues/${id}`, method: 'put', data })
export const deleteFlexValue = (id: string) => request({ url: `/flexvalues/${id}`, method: 'delete' })
export const approveFlexValue = (id: string) => request({ url: `/flexvalues/${id}/approve`, method: 'put' })
export const unapproveFlexValue = (id: string) => request({ url: `/flexvalues/${id}/unapprove`, method: 'put' })
export const disableFlexValue = (id: string) => request({ url: `/flexvalues/${id}/disable`, method: 'put' })
export const enableFlexValue = (id: string) => request({ url: `/flexvalues/${id}/enable`, method: 'put' })
export const getFlexValuesFields = () => request({ url: '/flexvalues/fields', method: 'get' })

// 值列表（仓位集值）
export const getFlexValuesEntries = (id: string) => request({ url: `/flexvalues/${id}/entries`, method: 'get' })
export const saveFlexValuesEntries = (id: string, items: any[]) => request({ url: `/flexvalues/${id}/entries`, method: 'put', data: { items } })
