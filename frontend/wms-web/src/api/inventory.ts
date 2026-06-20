import request from '../utils/request'

// 即时库存列表行（只读；真实表 T_STK_INVENTORY，解析关联名称）
export interface InventoryListItem {
    uid?: string
    fmaterialNumber?: string
    fmaterialName?: string
    fSpecification?: string
    flot?: string
    fauxpropName?: string
    fqty?: number
    fstockunitName?: string
    fbaseunitqty?: number
    fbaseunitName?: string
    fsecunitqty?: number
    fsecunitName?: string
    fstockNumber?: string
    fstockName?: string
    fstocklocNumber?: string
    fstocklocName?: string
    fisKfPeriod?: boolean
    fkfdate?: string | null
    fKfPeriod?: number
    fKfUnit?: number
    fusefuldate?: string | null
    fstockstatusName?: string
    fstockorgName?: string
    fcompanyName?: string
}

// GET /api/inventory - 即时库存分页列表
export const getInventories = (params?: any) => {
    return request({
        url: '/inventory',
        method: 'get',
        params: {
            pageIndex: params?.page || 1,
            pageSize: params?.pageSize || 20,
            keyword: params?.keyword || '',
            dynamicFilters: params?.dynamicFilters || []
        }
    })
}
