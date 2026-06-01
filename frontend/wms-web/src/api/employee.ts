import request from '../utils/request'

export interface Employee {
    uid?: string
    fStatus?: number
    fDisabled?: boolean
    fNumber: string
    fName: string
    fSex?: number
    fTel?: string
    fSalDeptId?: string
    fSalDeptName?: string
    fIsDeparture?: boolean
    cYmd?: string
    fEducation?: string
    fBirthDate?: string
    fEntryDate?: string
    fDepartureDate?: string
    fTransferDate?: string
    fMail?: string
    fNote?: string
    fAddress?: string
    fQq?: string
    fWechat?: string
    fEmergencyTel?: string
    emergency?: string
    fGroupId?: string
    // 扩展
    fCostCenterId?: string
    fCostCenterName?: string
    fDutyId?: string
    fDutyName?: string
    fPersonnelCoefficient?: number
    fDutyLevel?: number
    fDingTalkUserId?: string
    fPictureBase64?: string | null
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
}

export interface EmployeeLookupOption {
    uid: string
    fNumber: string
    fName: string
}

// GET /api/employee/cost-centers - 成本中心下拉
export const getCostCenters = (keyword?: string) => request({ url: '/employee/cost-centers', method: 'get', params: { keyword } })
// GET /api/employee/duties - 职务下拉（辅助资料类别 FNUMBER='D'）
export const getDuties = (keyword?: string) => request({ url: '/employee/duties', method: 'get', params: { keyword } })

export const getEmployees = (params?: any) => {
    return request({ url: '/employee', method: 'get', params: { pageIndex: params?.page || 1, pageSize: params?.pageSize || 10, keyword: params?.keyword || '', groupId: params?.groupId || '', dynamicFilters: params?.dynamicFilters || [], sortField: params?.sortField, isAsc: params?.isAsc } })
}
export const getEmployee = (id: string) => {
    return request({ url: `/employee/${id}`, method: 'get' })
}
export const createEmployee = (data: any) => {
    return request({ url: '/employee', method: 'post', data })
}
export const updateEmployee = (id: string, data: any) => {
    return request({ url: `/employee/${id}`, method: 'put', data })
}
export const deleteEmployee = (id: string) => {
    return request({ url: `/employee/${id}`, method: 'delete' })
}

export const approveEmployee = (id: string) => {
    return request({ url: `/employee/${id}/approve`, method: 'put' })
}

export const unapproveEmployee = (id: string) => {
    return request({ url: `/employee/${id}/unapprove`, method: 'put' })
}

// PUT /api/employee/{id}/disable - 禁用
export const disableEmployee = (id: string) => {
    return request({ url: `/employee/${id}/disable`, method: 'put' })
}

// PUT /api/employee/{id}/enable - 反禁用
export const enableEmployee = (id: string) => {
    return request({ url: `/employee/${id}/enable`, method: 'put' })
}

// GET /api/employee/fields - 获取模型字段数据类型
export const getEmployeesFields = () => {
    return request({ url: '/employee/fields', method: 'get' })
}
