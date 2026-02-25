import request from '../utils/request'

export interface Menu {
    uid?: string
    parentId: string
    menuName: string
    menuType: string
    routePath?: string
    icon?: string
    permCode?: string
    sortOrder: number
    fStatus: number
    cYmd?: string
    children?: Menu[]
}

// GET /api/menu/tree (菜单管理用，包含所有菜单)
export const getMenuTree = () => {
    return request({
        url: '/menu/tree',
        method: 'get'
    })
}

// GET /api/menu/sidebar (侧边栏用，仅启用的菜单)
export const getSidebarMenus = () => {
    return request({
        url: '/menu/sidebar',
        method: 'get'
    })
}

// GET /api/menu/{id}
export const getMenu = (id: string) => {
    return request({
        url: `/menu/${id}`,
        method: 'get'
    })
}

// POST /api/menu
export const createMenu = (data: any) => {
    return request({
        url: '/menu',
        method: 'post',
        data
    })
}

// PUT /api/menu/{id}
export const updateMenu = (id: string, data: any) => {
    return request({
        url: `/menu/${id}`,
        method: 'put',
        data
    })
}

// DELETE /api/menu/{id}
export const deleteMenu = (id: string) => {
    return request({
        url: `/menu/${id}`,
        method: 'delete'
    })
}
