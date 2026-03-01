<template>
  <el-container class="layout-container">
    <el-aside width="220px" class="aside-menu">
      <div class="logo">
        <el-icon class="logo-icon"><Box /></el-icon>
        <span>WMS Pro</span>
      </div>
      <el-menu
        active-text-color="#06b6d4"
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
import { getRefreshToken, clearTokens } from '../utils/token'
import request from '../utils/request'

const router = useRouter()
const route = useRoute()
const username = ref(localStorage.getItem('username') || 'User')
const menuStore = useMenuStore()

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
  box-shadow: 4px 0 10px rgba(0,0,0,0.05);
  z-index: 10;
}

.logo {
  height: 64px;
  display: flex;
  align-items: center;
  padding: 0 24px;
  color: #ffffff;
  font-size: 20px;
  font-weight: 600;
  border-bottom: 1px solid rgba(255,255,255,0.1);
}

.logo-icon {
  margin-right: 12px;
  font-size: 24px;
  color: var(--accent-color);
}

.el-menu-vertical {
  border-right: none;
  flex: 1;
  padding-top: 10px;
}

:deep(.el-menu-item) {
  color: #94a3b8;
  margin: 4px 12px;
  border-radius: 6px;
  height: 48px;
}

:deep(.el-menu-item.is-active) {
  background-color: var(--primary-color);
  color: #ffffff;
}

:deep(.el-menu-item:hover:not(.is-active)) {
  background-color: rgba(255, 255, 255, 0.05);
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
  padding: 0 32px;
  height: 64px;
  box-shadow: 0 1px 2px rgba(0,0,0,0.03);
}

.page-title {
  margin: 0;
  font-size: 20px;
  font-weight: 600;
  color: var(--text-primary);
}

.header-right {
  display: flex;
  align-items: center;
  gap: 20px;
}

.user-info {
  display: flex;
  align-items: center;
  gap: 12px;
  color: var(--text-primary);
  font-weight: 500;
}

.user-avatar {
  background-color: var(--bg-body);
  color: var(--text-primary);
  border: 1px solid var(--border-color);
}

.main-content {
  padding: 32px;
  background-color: var(--bg-body);
}
</style>
