import { createRouter, createWebHistory } from 'vue-router'
import Login from '../views/Login.vue'
import { usePermissionStore } from '../stores/permission'

const routes = [
    {
        path: '/login',
        name: 'Login',
        component: Login
    },
    {
        path: '/',
        name: 'Home',
        component: () => import('../views/Home.vue'),
        redirect: '/dashboard',
        meta: { requiresAuth: true },
        children: [
            {
                path: 'dashboard',
                name: 'Dashboard',
                component: () => import('../views/Dashboard.vue'),
                meta: { title: '仪表盘' }
            },
            {
                path: 'system/users',
                name: 'UserList',
                component: () => import('../views/system/UserList.vue'),
                meta: { title: '用户管理' }
            },
            {
                path: 'system/roles',
                name: 'RoleList',
                component: () => import('../views/system/RoleList.vue'),
                meta: { title: '角色管理' }
            },
            {
                path: 'system/depts',
                name: 'DeptList',
                component: () => import('../views/system/DeptList.vue'),
                meta: { title: '部门管理' }
            },
            {
                path: 'system/menus',
                name: 'MenuList',
                component: () => import('../views/system/MenuList.vue'),
                meta: { title: '菜单管理' }
            },
            {
                path: 'system/logs/request',
                name: 'RequestLogList',
                component: () => import('../views/system/RequestLogList.vue'),
                meta: { title: '请求日志' }
            },
            {
                path: 'master/materials',
                name: 'MaterialList',
                component: () => import('../views/master/MaterialList.vue'),
                meta: { title: '物料管理' }
            },
            {
                path: 'master/customers',
                name: 'CustomerList',
                component: () => import('../views/master/CustomerList.vue'),
                meta: { title: '客户管理' }
            },
            {
                path: 'master/suppliers',
                name: 'SupplierList',
                component: () => import('../views/master/SupplierList.vue'),
                meta: { title: '供应商管理' }
            },
            {
                path: 'master/currencies',
                name: 'CurrencyList',
                component: () => import('../views/master/CurrencyList.vue'),
                meta: { title: '币种管理' }
            },
            {
                path: 'master/warehouses',
                name: 'WarehouseList',
                component: () => import('../views/master/WarehouseList.vue'),
                meta: { title: '仓库管理' }
            },
            {
                path: 'master/units',
                name: 'UnitList',
                component: () => import('../views/master/UnitList.vue'),
                meta: { title: '单位管理' }
            },
            {
                path: 'master/stockplaces',
                name: 'StockPlaceList',
                component: () => import('../views/master/StockPlaceList.vue'),
                meta: { title: '仓位管理' }
            },
            {
                path: 'master/assistantdata',
                name: 'AssistantDataList',
                component: () => import('../views/master/AssistantDataList.vue'),
                meta: { title: '辅助资料' }
            },
            {
                path: 'master/employees',
                name: 'EmployeeList',
                component: () => import('../views/master/EmployeeList.vue'),
                meta: { title: '职员管理' }
            },
            {
                path: 'master/materialbartypes',
                name: 'MaterialBarTypeList',
                component: () => import('../views/master/MaterialBarTypeList.vue'),
                meta: { title: '物料条码类型' }
            }
        ]
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

router.beforeEach(async (to, from, next) => {
    const token = localStorage.getItem('token')
    if (to.path === '/login') {
        if (token) {
            next('/')
        } else {
            next()
        }
    } else {
        if (token) {
            const permissionStore = usePermissionStore()
            if (permissionStore.permissions.length === 0) {
                await permissionStore.loadPermissions()
            }
            next()
        } else {
            next('/login')
        }
    }
})

export default router
