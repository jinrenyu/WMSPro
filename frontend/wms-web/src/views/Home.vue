<template>
  <el-container class="layout-container">
    <el-aside width="220px" class="aside-menu">
      <div class="logo">
        <el-icon class="logo-icon"><Box /></el-icon>
        <span>WMS Pro</span>
      </div>
      <el-menu
        active-text-color="#ffffff"
        background-color="transparent"
        class="el-menu-vertical"
        :default-active="activeMenu"
        text-color="#94a3b8"
        router
      >
        <template v-for="menu in menuList" :key="menu.path">
          <el-sub-menu v-if="menu.children && menu.children.length > 0" :index="menu.path">
            <template #title>
              <el-icon v-if="menu.icon"><component :is="menu.icon" /></el-icon>
              <span>{{ menu.name }}</span>
            </template>
            <template v-for="child in menu.children" :key="child.path">
              <el-sub-menu v-if="child.children && child.children.length > 0" :index="child.path">
                <template #title>
                  <el-icon v-if="child.icon"><component :is="child.icon" /></el-icon>
                  <span>{{ child.name }}</span>
                </template>
                <el-menu-item v-for="grandchild in child.children" :key="grandchild.path" :index="grandchild.path">
                  <el-icon v-if="grandchild.icon"><component :is="grandchild.icon" /></el-icon>
                  <span>{{ grandchild.name }}</span>
                </el-menu-item>
              </el-sub-menu>
              <el-menu-item v-else :index="child.path">
                <el-icon v-if="child.icon"><component :is="child.icon" /></el-icon>
                <span>{{ child.name }}</span>
              </el-menu-item>
            </template>
          </el-sub-menu>
          <el-menu-item v-else :index="menu.path">
            <el-icon v-if="menu.icon"><component :is="menu.icon" /></el-icon>
            <span>{{ menu.name }}</span>
          </el-menu-item>
        </template>
      </el-menu>
    </el-aside>

    <el-container>
      <el-header class="header">
        <div class="header-left">
          <h2 class="page-title">{{ pageTitle }}</h2>
        </div>
        <div class="header-right">
          <el-select
            v-if="orgStore.orgs.length"
            :model-value="orgStore.currentOrgId"
            class="org-switcher"
            placeholder="选择组织"
            @change="orgStore.setCurrentOrg"
          >
            <template #prefix><el-icon><OfficeBuilding /></el-icon></template>
            <el-option v-for="o in orgStore.orgs" :key="o.orgId" :label="o.orgName" :value="o.orgId" />
          </el-select>
          <ThemeToggle />
          <div class="user-info">
            <el-avatar :size="32" class="user-avatar">A</el-avatar>
            <span class="username">{{ username }}</span>
          </div>
          <el-button type="danger" link @click="handleLogout">
            <el-icon><SwitchButton /></el-icon>
          </el-button>
        </div>
      </el-header>

      <el-main class="main-content">
        <router-view v-slot="{ Component }">
          <transition name="fade" mode="out-in">
            <component :is="Component" />
          </transition>
        </router-view>
      </el-main>
    </el-container>
  </el-container>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { ElMessage } from 'element-plus'
import ThemeToggle from '../components/ThemeToggle.vue'
import { usePermissionStore } from '../stores/permission'
import { useMenuStore } from '../stores/menu'
import { useOrgStore } from '../stores/org'
import { getRefreshToken, clearTokens } from '../utils/token'
import request from '../utils/request'

const router = useRouter()
const route = useRoute()
const username = ref(localStorage.getItem('username') || 'User')
const menuStore = useMenuStore()
const orgStore = useOrgStore()

const activeMenu = computed(() => route.path)
const menuList = computed(() => menuStore.sidebarMenus)

// 根据当前路由动态获取页面标题
const pageTitle = computed(() => {
  const findMenuByPath = (menus: any[], path: string): any => {
    for (const menu of menus) {
      if (menu.path === path) return menu
      if (menu.children) {
        const found = findMenuByPath(menu.children, path)
        if (found) return found
      }
    }
    return null
  }

  const currentMenu = findMenuByPath(menuStore.sidebarMenus, route.path)
  return currentMenu?.name || route.meta?.title || '仪表盘'
})

onMounted(async () => {
  if (!menuStore.loaded) {
    await menuStore.loadMenus()
  }
  if (!orgStore.loaded) {
    orgStore.loadOrgs()
  }
})

const handleLogout = async () => {
  try {
    const refreshToken = getRefreshToken()
    await request.post('/auth/logout', refreshToken ? { refreshToken } : {})
  } catch {
    // 即使后端调用失败也继续登出
  }
  clearTokens()
  const permissionStore = usePermissionStore()
  permissionStore.resetPermissions()
  menuStore.resetMenus()
  orgStore.reset()
  ElMessage.success('已退出登录')
  router.push('/login')
}
</script>

<style scoped>
.layout-container {
  height: 100vh;
  background-color: var(--bg-body);
}

.aside-menu {
  background-color: var(--bg-sidebar);
  border-right: none;
  display: flex;
  flex-direction: column;
  box-shadow: 2px 0 12px rgba(0, 0, 0, 0.18);
  z-index: 10;
  overflow: hidden;
}

.logo {
  height: 64px;
  display: flex;
  align-items: center;
  padding: 0 22px;
  color: #ffffff;
  font-size: 19px;
  font-weight: 700;
  letter-spacing: 0.3px;
  border-bottom: 1px solid rgba(255, 255, 255, 0.08);
}

.logo-icon {
  margin-right: 12px;
  font-size: 22px;
  color: var(--accent-color);
  display: flex;
  align-items: center;
  justify-content: center;
  width: 34px;
  height: 34px;
  border-radius: 9px;
  background: rgba(217, 119, 6, 0.14);
}

.el-menu-vertical {
  border-right: none;
  flex: 1;
  padding-top: 8px;
  overflow-y: auto;
}

:deep(.el-menu-item),
:deep(.el-sub-menu__title) {
  color: var(--sidebar-text);
  height: 44px;
  line-height: 44px;
  transition: background-color 0.2s, color 0.2s;
}

:deep(.el-menu-item) {
  margin: 3px 12px;
  border-radius: 7px;
  height: 44px;
}

:deep(.el-menu-item.is-active) {
  background-color: rgba(59, 130, 246, 0.16);
  color: #ffffff;
  font-weight: 600;
  position: relative;
}

:deep(.el-menu-item.is-active)::before {
  content: '';
  position: absolute;
  left: 0;
  top: 9px;
  bottom: 9px;
  width: 3px;
  border-radius: 0 3px 3px 0;
  background: var(--accent-color);
}

:deep(.el-menu-item.is-active .el-icon) {
  color: #60a5fa;
}

:deep(.el-menu-item:hover:not(.is-active)),
:deep(.el-sub-menu__title:hover) {
  background-color: var(--bg-sidebar-hover);
  color: #ffffff;
}

/* 让二级 sub-menu（如日志管理）的标题与同级 menu-item 对齐 */
:deep(.el-sub-menu .el-sub-menu .el-sub-menu__title) {
  padding-left: 52px !important;
}

.header {
  background-color: var(--header-bg);
  border-bottom: 1px solid var(--border-color);
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 28px;
  height: 64px;
  box-shadow: 0 1px 2px rgba(15, 23, 42, 0.04);
}

.page-title {
  margin: 0;
  font-size: 19px;
  font-weight: 600;
  color: var(--text-primary);
  letter-spacing: -0.2px;
}

.header-right {
  display: flex;
  align-items: center;
  gap: 16px;
}

.org-switcher {
  width: 168px;
}

.user-info {
  display: flex;
  align-items: center;
  gap: 10px;
  padding-left: 16px;
  border-left: 1px solid var(--border-color);
  color: var(--text-primary);
  font-weight: 500;
}

.user-avatar {
  background: linear-gradient(135deg, var(--primary-color), #2563eb);
  color: #ffffff;
  font-weight: 600;
  border: none;
}

.main-content {
  padding: 24px 28px;
  background-color: var(--bg-body);
}
</style>
