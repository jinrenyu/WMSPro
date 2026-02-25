import { defineStore } from 'pinia'
import { ref } from 'vue'
import { getSidebarMenus, type Menu } from '../api/menu'
import { usePermissionStore } from './permission'

export interface SidebarMenu {
    path: string
    name: string
    icon?: string
    children?: SidebarMenu[]
}

export const useMenuStore = defineStore('menu', () => {
    const sidebarMenus = ref<SidebarMenu[]>([])
    const loaded = ref(false)

    /**
     * 从后端加载菜单树，根据用户权限过滤后转换为侧边栏格式
     */
    const loadMenus = async () => {
        try {
            const res: any = await getSidebarMenus()
            const tree: Menu[] = res.data || []
            const permissionStore = usePermissionStore()
            sidebarMenus.value = convertToSidebar(tree, permissionStore)
            loaded.value = true
        } catch (error) {
            console.error('Failed to load menus from API:', error)
            sidebarMenus.value = getDefaultMenus()
            loaded.value = true
        }
    }

    const resetMenus = () => {
        sidebarMenus.value = []
        loaded.value = false
    }

    return { sidebarMenus, loaded, loadMenus, resetMenus }
})

/**
 * 将后端菜单树转换为侧边栏菜单格式，根据权限过滤
 * - 菜单(M)：用户拥有其下任意按钮权限时才显示
 * - 目录(D)：其下有可见子菜单时才显示
 * - 按钮(B)：不在侧边栏显示
 * - 没有子按钮的菜单(M)（如 Dashboard）：直接显示
 */
function convertToSidebar(menus: Menu[], permissionStore: { hasPermission: (code: string) => boolean }): SidebarMenu[] {
    return menus
        .filter(m => m.menuType !== 'B' && m.fStatus === 0)
        .map(m => {
            // 菜单类型(M)：检查用户是否拥有其下任意按钮权限
            if (m.menuType === 'M') {
                const buttons = (m.children || []).filter(c => c.menuType === 'B' && c.permCode)
                // 如果菜单下有按钮定义，则需要至少拥有一个按钮权限才显示
                if (buttons.length > 0) {
                    const hasAny = buttons.some(b => permissionStore.hasPermission(b.permCode!))
                    if (!hasAny) return null
                }
                // 没有按钮的菜单（如 Dashboard）直接显示
            }

            const item: SidebarMenu = {
                path: m.routePath || `/${m.uid}`,
                name: m.menuName,
                icon: m.icon || undefined
            }

            // 目录类型(D)：递归处理子菜单
            if (m.children && m.children.length > 0) {
                const children = convertToSidebar(m.children, permissionStore)
                if (children.length > 0) {
                    item.children = children
                } else if (m.menuType === 'D') {
                    // 目录下没有可见子菜单，不显示该目录
                    return null
                }
            }

            return item
        })
        .filter((item): item is SidebarMenu => item !== null)
}

function getDefaultMenus(): SidebarMenu[] {
    return [
        { path: '/dashboard', name: 'Dashboard', icon: 'Odometer' },
        {
            path: '/system',
            name: '系统管理',
            icon: 'Setting',
            children: [
                { path: '/system/users', name: '用户管理' },
                { path: '/system/roles', name: '角色管理' },
                { path: '/system/depts', name: '部门管理' },
                { path: '/system/menus', name: '菜单管理' },
                {
                    path: '/system/logs',
                    name: '日志管理',
                    children: [
                        { path: '/system/logs/request', name: '请求日志' }
                    ]
                }
            ]
        }
    ]
}
