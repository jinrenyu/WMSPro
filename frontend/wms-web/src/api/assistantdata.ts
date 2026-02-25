import request from '../utils/request'

// 辅助资料类别
export interface AssistantData {
    uid?: string
    fnumber: string
    fname: string
    fdescription?: string
    fparentid?: string
    ftopclassid?: string
    isdefault?: boolean
    fiso3use?: boolean
    fStatus?: number
    cYmd?: string
}

export const getAssistantDataList = (params?: any) => {
    return request({ url: '/assistantdata', method: 'get', params: { pageIndex: params?.page || 1, pageSize: params?.pageSize || 100, keyword: params?.keyword || '', groupId: params?.groupId || '' } })
}
export const getAssistantData = (id: string) => {
    return request({ url: `/assistantdata/${id}`, method: 'get' })
}
export const createAssistantData = (data: any) => {
    return request({ url: '/assistantdata', method: 'post', data })
}
export const updateAssistantData = (id: string, data: any) => {
    return request({ url: `/assistantdata/${id}`, method: 'put', data })
}
export const deleteAssistantData = (id: string) => {
    return request({ url: `/assistantdata/${id}`, method: 'delete' })
}
export const approveAssistantData = (id: string) => {
    return request({ url: `/assistantdata/${id}/approve`, method: 'put' })
}
export const unapproveAssistantData = (id: string) => {
    return request({ url: `/assistantdata/${id}/unapprove`, method: 'put' })
}
export const disableAssistantData = (id: string) => {
    return request({ url: `/assistantdata/${id}/disable`, method: 'put' })
}
export const enableAssistantData = (id: string) => {
    return request({ url: `/assistantdata/${id}/enable`, method: 'put' })
}

// 辅助资料明细
export interface AssistantDataEntry {
    uid?: string
    fnumber: string
    fdatavalue: string
    fid: string
    fparentid?: string
    fdescription?: string
    fseq?: number
    isdefault?: boolean
    fuseorgid?: string
    fisbuildself?: boolean
    fStatus?: number
    cYmd?: string
}

export const getAssistantDataEntries = (params?: any) => {
    return request({ url: '/assistantdataentry', method: 'get', params: { pageIndex: params?.page || 1, pageSize: params?.pageSize || 10, keyword: params?.keyword || '', groupId: params?.groupId || '', fid: params?.fid || '' } })
}
export const getAssistantDataEntry = (id: string) => {
    return request({ url: `/assistantdataentry/${id}`, method: 'get' })
}
export const createAssistantDataEntry = (data: any) => {
    return request({ url: '/assistantdataentry', method: 'post', data })
}
export const updateAssistantDataEntry = (id: string, data: any) => {
    return request({ url: `/assistantdataentry/${id}`, method: 'put', data })
}
export const deleteAssistantDataEntry = (id: string) => {
    return request({ url: `/assistantdataentry/${id}`, method: 'delete' })
}
export const approveAssistantDataEntry = (id: string) => {
    return request({ url: `/assistantdataentry/${id}/approve`, method: 'put' })
}
export const unapproveAssistantDataEntry = (id: string) => {
    return request({ url: `/assistantdataentry/${id}/unapprove`, method: 'put' })
}
export const disableAssistantDataEntry = (id: string) => {
    return request({ url: `/assistantdataentry/${id}/disable`, method: 'put' })
}
export const enableAssistantDataEntry = (id: string) => {
    return request({ url: `/assistantdataentry/${id}/enable`, method: 'put' })
}
