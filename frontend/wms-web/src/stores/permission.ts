import { defineStore } from 'pinia'
import { ref } from 'vue'
import request from '../utils/request'

export const usePermissionStore = defineStore('permission', () => {
    const permissions = ref<string[]>([])

    /**
     * 从后端实时加载当前用户权限，同时更新 localStorage
     */
    const loadPermissions = async () => {
        try {
            const res: any = await request({ url: '/auth/permissions', method: 'get' })
            permissions.value = res.data || []
            localStorage.setItem('permissions', JSON.stringify(permissions.value))
        } catch (error) {
            console.error('Failed to load permissions from API, fallback to localStorage:', error)
            // 接口失败时降级读 localStorage
            const stored = localStorage.getItem('permissions')
            permissions.value = stored ? JSON.parse(stored) : []
        }
    }

    const hasPermission = (code: string) => {
        if (permissions.value.includes('*:*')) return true
        return permissions.value.includes(code)
    }

    const resetPermissions = () => {
        permissions.value = []
        localStorage.removeItem('permissions')
    }

    return { permissions, loadPermissions, hasPermission, resetPermissions }
})
