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
                path: 'system/users/edit',
                name: 'UserEdit',
                component: () => import('../views/system/UserEdit.vue'),
                meta: { title: '用户-维护' }
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
                path: 'system/code-rules',
                name: 'BillCodeRuleList',
                component: () => import('../views/system/BillCodeRuleList.vue'),
                meta: { title: '编码规则' }
            },
            {
                path: 'system/code-rules/edit',
                name: 'BillCodeRuleEdit',
                component: () => import('../views/system/BillCodeRuleEdit.vue'),
                meta: { title: '编码规则配置' }
            },
            {
                path: 'system/selbills',
                name: 'SelBillList',
                component: () => import('../views/system/SelBillList.vue'),
                meta: { title: '出入库流程配置' }
            },
            {
                path: 'system/selbills/edit',
                name: 'SelBillEdit',
                component: () => import('../views/system/SelBillEdit.vue'),
                meta: { title: '出入库流程配置-维护' }
            },
            {
                path: 'master/materials',
                name: 'MaterialList',
                component: () => import('../views/master/MaterialList.vue'),
                meta: { title: '物料管理' }
            },
            {
                path: 'master/materials/edit',
                name: 'MaterialEdit',
                component: () => import('../views/master/MaterialEdit.vue'),
                meta: { title: '物料-维护' }
            },
            {
                path: 'master/customers',
                name: 'CustomerList',
                component: () => import('../views/master/CustomerList.vue'),
                meta: { title: '客户管理' }
            },
            {
                path: 'master/customers/edit',
                name: 'CustomerEdit',
                component: () => import('../views/master/CustomerEdit.vue'),
                meta: { title: '客户-维护' }
            },
            {
                path: 'master/suppliers',
                name: 'SupplierList',
                component: () => import('../views/master/SupplierList.vue'),
                meta: { title: '供应商管理' }
            },
            {
                path: 'master/suppliers/edit',
                name: 'SupplierEdit',
                component: () => import('../views/master/SupplierEdit.vue'),
                meta: { title: '供应商-维护' }
            },
            {
                path: 'master/currencies',
                name: 'CurrencyList',
                component: () => import('../views/master/CurrencyList.vue'),
                meta: { title: '币种管理' }
            },
            {
                path: 'master/currencies/edit',
                name: 'CurrencyEdit',
                component: () => import('../views/master/CurrencyEdit.vue'),
                meta: { title: '币别-维护' }
            },
            {
                path: 'master/warehouses',
                name: 'WarehouseList',
                component: () => import('../views/master/WarehouseList.vue'),
                meta: { title: '仓库管理' }
            },
            {
                path: 'master/warehouses/edit',
                name: 'WarehouseEdit',
                component: () => import('../views/master/WarehouseEdit.vue'),
                meta: { title: '仓库-维护' }
            },
            {
                path: 'master/units',
                name: 'UnitList',
                component: () => import('../views/master/UnitList.vue'),
                meta: { title: '单位管理' }
            },
            {
                path: 'master/units/edit',
                name: 'UnitEdit',
                component: () => import('../views/master/UnitEdit.vue'),
                meta: { title: '计量单位-维护' }
            },
            {
                path: 'master/stockplaces',
                name: 'StockPlaceList',
                component: () => import('../views/master/StockPlaceList.vue'),
                meta: { title: '仓位管理' }
            },
            {
                path: 'master/flexvalues',
                name: 'FlexValuesList',
                component: () => import('../views/master/FlexValuesList.vue'),
                meta: { title: '仓位' }
            },
            {
                path: 'master/flexvalues/edit',
                name: 'FlexValuesEdit',
                component: () => import('../views/master/FlexValuesEdit.vue'),
                meta: { title: '仓位-维护' }
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
                path: 'master/employees/edit',
                name: 'EmployeeEdit',
                component: () => import('../views/master/EmployeeEdit.vue'),
                meta: { title: '职员-维护' }
            },
            {
                path: 'master/materialbartypes',
                name: 'MaterialBarTypeList',
                component: () => import('../views/master/MaterialBarTypeList.vue'),
                meta: { title: '物料条码类型' }
            },
            {
                path: 'master/materialbartypes/edit',
                name: 'MaterialBarTypeEdit',
                component: () => import('../views/master/MaterialBarTypeEdit.vue'),
                meta: { title: '物料条码类型-维护' }
            },
            // ── 采购管理（占位空页，后续逐张单据替换为真实页面）──
            {
                path: 'purchase/requests',
                name: 'PurchaseRequestList',
                component: () => import('../views/purchase/PlaceholderPage.vue'),
                meta: { title: '采购申请单' }
            },
            {
                path: 'purchase/orders',
                name: 'PurchaseOrderList',
                component: () => import('../views/purchase/PurchaseOrderList.vue'),
                meta: { title: '采购订单' }
            },
            {
                path: 'purchase/orders/edit',
                name: 'PurchaseOrderEdit',
                component: () => import('../views/purchase/PurchaseOrderEdit.vue'),
                meta: { title: '采购订单-维护' }
            },
            {
                path: 'purchase/receive-notices',
                name: 'ReceiveNoticeList',
                component: () => import('../views/purchase/ReceiveNoticeList.vue'),
                meta: { title: '收料通知单' }
            },
            {
                path: 'purchase/receive-notices/edit',
                name: 'ReceiveNoticeEdit',
                component: () => import('../views/purchase/ReceiveNoticeEdit.vue'),
                meta: { title: '收料通知单-维护' }
            },
            {
                path: 'purchase/inbounds',
                name: 'PurchaseInboundList',
                component: () => import('../views/purchase/PurchaseInboundList.vue'),
                meta: { title: '采购入库单' }
            },
            {
                path: 'purchase/inbounds/edit',
                name: 'PurchaseInboundEdit',
                component: () => import('../views/purchase/PurchaseInboundEdit.vue'),
                meta: { title: '采购入库单-维护' }
            },
            {
                path: 'purchase/return-requests',
                name: 'MaterialReturnApplyList',
                component: () => import('../views/purchase/MaterialReturnApplyList.vue'),
                meta: { title: '退料申请单' }
            },
            {
                path: 'purchase/return-requests/edit',
                name: 'MaterialReturnApplyEdit',
                component: () => import('../views/purchase/MaterialReturnApplyEdit.vue'),
                meta: { title: '退料申请单-维护' }
            },
            {
                path: 'purchase/returns',
                name: 'PurchaseReturnList',
                component: () => import('../views/purchase/PurchaseReturnList.vue'),
                meta: { title: '采购退料单' }
            },
            {
                path: 'purchase/returns/edit',
                name: 'PurchaseReturnEdit',
                component: () => import('../views/purchase/PurchaseReturnEdit.vue'),
                meta: { title: '采购退料单-维护' }
            },
            {
                path: 'purchase/labels/receive-notice',
                name: 'ReceiveNoticeLabelList',
                component: () => import('../views/purchase/ReceiveNoticeLabelList.vue'),
                meta: { title: '收料通知单标签' }
            },
            {
                path: 'purchase/labels/receive-notice/generate',
                name: 'ReceiveNoticeLabelGenerate',
                component: () => import('../views/purchase/ReceiveNoticeLabelGenerate.vue'),
                meta: { title: '收料通知单标签 - 条码生成' }
            },
            {
                path: 'purchase/labels/purchase-order',
                name: 'PurchaseOrderLabelList',
                component: () => import('../views/purchase/PurchaseOrderLabelList.vue'),
                meta: { title: '采购订单标签' }
            },
            {
                path: 'purchase/labels/purchase-order/generate',
                name: 'PurchaseOrderLabelGenerate',
                component: () => import('../views/purchase/PurchaseOrderLabelGenerate.vue'),
                meta: { title: '采购订单标签 - 条码生成' }
            },
            {
                path: 'purchase/labels/template',
                name: 'LabelTemplateList',
                component: () => import('../views/purchase/LabelTemplateList.vue'),
                meta: { title: '标签模板设计' }
            },
            {
                path: 'purchase/labels/template/edit',
                name: 'LabelTemplateEdit',
                component: () => import('../views/purchase/LabelTemplateEdit.vue'),
                meta: { title: '标签模板设计 - 编辑' }
            },
            // ── 库存管理 ──
            {
                path: 'purchase/inventory',
                name: 'StockInventoryList',
                component: () => import('../views/purchase/StockInventoryList.vue'),
                meta: { title: '即时库存' }
            },
            // ── 生产管理（占位空页，后续逐张单据替换为真实页面）──
            // 生产业务
            {
                path: 'production/orders',
                name: 'ProductionOrderList',
                component: () => import('../views/production/ProductionOrderList.vue'),
                meta: { title: '生产订单' }
            },
            {
                path: 'production/orders/edit',
                name: 'ProductionOrderEdit',
                component: () => import('../views/production/ProductionOrderEdit.vue'),
                meta: { title: '生产订单-维护' }
            },
            {
                path: 'production/material-lists',
                name: 'ProductionMaterialListList',
                component: () => import('../views/production/ProductionMaterialListList.vue'),
                meta: { title: '生产用料清单' }
            },
            {
                path: 'production/material-lists/edit',
                name: 'ProductionMaterialListEdit',
                component: () => import('../views/production/ProductionMaterialListEdit.vue'),
                meta: { title: '生产用料清单-维护' }
            },
            // 数据集成 - ERP集成配置
            {
                path: 'integration/erp-sync',
                name: 'ErpSyncConfigList',
                component: () => import('../views/integration/ErpSyncConfigList.vue'),
                meta: { title: 'ERP集成配置' }
            },
            {
                path: 'integration/erp-sync/edit',
                name: 'ErpSyncConfigEdit',
                component: () => import('../views/integration/ErpSyncConfigEdit.vue'),
                meta: { title: 'ERP集成配置-维护' }
            },
            // 生产领料
            {
                path: 'production/picks',
                name: 'ProductionPickList',
                component: () => import('../views/production/PlaceholderPage.vue'),
                meta: { title: '生产领料单' }
            },
            {
                path: 'production/returns',
                name: 'ProductionReturnList',
                component: () => import('../views/production/PlaceholderPage.vue'),
                meta: { title: '生产退料单' }
            },
            {
                path: 'production/supplements',
                name: 'ProductionSupplementList',
                component: () => import('../views/production/PlaceholderPage.vue'),
                meta: { title: '生产补料单' }
            },
            // 生产入库
            {
                path: 'production/inbounds',
                name: 'ProductionInboundList',
                component: () => import('../views/production/PlaceholderPage.vue'),
                meta: { title: '生产入库单' }
            },
            {
                path: 'production/stock-returns',
                name: 'ProductionStockReturnList',
                component: () => import('../views/production/PlaceholderPage.vue'),
                meta: { title: '生产退库单' }
            },
            // 简单生产
            {
                path: 'production/simple-picks',
                name: 'SimpleProductionPickList',
                component: () => import('../views/production/PlaceholderPage.vue'),
                meta: { title: '简单生产领料单' }
            },
            {
                path: 'production/simple-returns',
                name: 'SimpleProductionReturnList',
                component: () => import('../views/production/PlaceholderPage.vue'),
                meta: { title: '简单生产退料单' }
            },
            {
                path: 'production/simple-inbounds',
                name: 'SimpleProductionInboundList',
                component: () => import('../views/production/PlaceholderPage.vue'),
                meta: { title: '简单生产入库单' }
            },
            {
                path: 'production/simple-stock-returns',
                name: 'SimpleProductionStockReturnList',
                component: () => import('../views/production/PlaceholderPage.vue'),
                meta: { title: '简单生产退库单' }
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
