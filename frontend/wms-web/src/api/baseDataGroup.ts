import request from '../utils/request'

export interface BaseDataGroup {
    uid?: string
    fPrgKey?: string
    fNumber: string
    fName: string
    fCName?: string
    fTName?: string
    fEName?: string
    fParentId?: string
    fFullNumber?: string
    fNote?: string
    cYmd?: string
    children?: BaseDataGroup[]
}

// GET /api/base-data-group/{prgKey}/tree
export const getGroupTree = (prgKey: string) => {
    return request({
        url: `/base-data-group/${prgKey}/tree`,
        method: 'get'
    })
}

// GET /api/base-data-group/{prgKey}/{id}
export const getGroup = (prgKey: string, id: string) => {
    return request({
        url: `/base-data-group/${prgKey}/${id}`,
        method: 'get'
    })
}

// POST /api/base-data-group/{prgKey}
export const createGroup = (prgKey: string, data: Partial<BaseDataGroup>) => {
    return request({
        url: `/base-data-group/${prgKey}`,
        method: 'post',
        data
    })
}

// PUT /api/base-data-group/{prgKey}/{id}
export const updateGroup = (prgKey: string, id: string, data: Partial<BaseDataGroup>) => {
    return request({
        url: `/base-data-group/${prgKey}/${id}`,
        method: 'put',
        data
    })
}

// DELETE /api/base-data-group/{prgKey}/{id}
export const deleteGroup = (prgKey: string, id: string) => {
    return request({
        url: `/base-data-group/${prgKey}/${id}`,
        method: 'delete'
    })
}

// Build flat list into tree structure
export function buildTree(list: BaseDataGroup[]): BaseDataGroup[] {
    const map = new Map<string, BaseDataGroup>()
    const roots: BaseDataGroup[] = []
    list.forEach(item => { item.children = []; map.set(item.uid!, item) })
    list.forEach(item => {
        if (item.fParentId && map.has(item.fParentId))
            map.get(item.fParentId)!.children!.push(item)
        else
            roots.push(item)
    })
    return roots
}
