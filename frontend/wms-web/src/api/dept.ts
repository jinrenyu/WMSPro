import request from '../utils/request'

export interface Department {
    uid?: string
    fNumber: string
    fName: string
    fFullName?: string
    fParentId?: string
    fManager?: string
    fDescription?: string
    fHelpCode?: string
    fIsLine?: boolean
    fDeptProperty?: string
    fCostAccountType?: boolean
    fEffectDate?: string
    fLapseDate?: string
    fErpNumber?: string
    cYmd?: string
    children?: Department[]
}

// GET /api/department/tree
export const getDeptTree = (params?: any) => {
    return request({
        url: '/department/tree',
        method: 'get',
        params
    })
}

// GET /api/department - paged list
export const getDepts = (params?: any) => {
    return request({
        url: '/department',
        method: 'get',
        params: {
            pageIndex: params?.page || 1,
            pageSize: params?.pageSize || 100,
            keyword: params?.keyword || ''
        }
    })
}

// GET /api/department/{id}
export const getDept = (id: string) => {
    return request({
        url: `/department/${id}`,
        method: 'get'
    })
}

// POST /api/department
export const createDept = (data: any) => {
    return request({
        url: '/department',
        method: 'post',
        data
    })
}

// PUT /api/department/{id}
export const updateDept = (id: string, data: any) => {
    return request({
        url: `/department/${id}`,
        method: 'put',
        data
    })
}

// DELETE /api/department/{id}
export const deleteDept = (id: string) => {
    return request({
        url: `/department/${id}`,
        method: 'delete'
    })
}
