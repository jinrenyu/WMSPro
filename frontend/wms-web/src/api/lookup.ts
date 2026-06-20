import request from '../utils/request'

export interface LookupItem {
    uid: string
    fNumber: string
    fName: string
    /** 是否启用仓位管理（仅仓库 lookup 返回；用于判断明细行仓位是否可编辑） */
    fIsOpenLocation?: boolean
}

export interface LookupParams {
    keyword?: string
    parentId?: string
    maxCount?: number
}

/**
 * 通用下拉查询接口
 * @param module 模块名称，如 unit, warehouse, stock-place 等
 * @param params 查询参数
 */
export const getLookup = (module: string, params?: LookupParams) => {
    return request({
        url: `/${module}/lookup`,
        method: 'get',
        params
    })
}
