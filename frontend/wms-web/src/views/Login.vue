<template>
  <div class="login-container">
    <div class="theme-switch-wrapper">
      <ThemeToggle />
    </div>
    <div class="login-card glass-card">
      <div class="card-header">
        <h2>WMS 智能仓储系统</h2>
      </div>
      <el-form :model="loginForm" label-width="0" size="large">
        <el-form-item>
          <el-input
            v-model="loginForm.username"
            placeholder="用户名 / Username"
            prefix-icon="User"
          />
        </el-form-item>
        <el-form-item>
          <el-input
            v-model="loginForm.password"
            type="password"
            placeholder="密码 / Password"
            show-password
            prefix-icon="Lock"
          />
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleLogin" :loading="loading" style="width: 100%">
            登 录 / LOGIN
          </el-button>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive } from 'vue'
import { useRouter } from 'vue-router'
import request from '../utils/request'
import { User, Lock } from '@element-plus/icons-vue'
import { ElMessage } from 'element-plus'
import ThemeToggle from '../components/ThemeToggle.vue'
import { usePermissionStore } from '../stores/permission'

const router = useRouter()
const loading = ref(false)

const loginForm = reactive({
  username: '',
  password: ''
})

const handleLogin = async () => {
  if (!loginForm.username || !loginForm.password) {
    ElMessage.warning('请输入用户名和密码')
    return
  }

  loading.value = true
  const permissionStore = usePermissionStore()
  permissionStore.resetPermissions()
  try {
    const data: any = await request.post('/auth/login', {
      userId: loginForm.username,
      password: loginForm.password
    })

    const token = data.data.token
    const userInfo = data.data.userInfo

    localStorage.setItem('token', token)
    localStorage.setItem('username', userInfo.userName)
    localStorage.setItem('permissions', JSON.stringify(userInfo.permissions || []))

    // Load permissions into store
    permissionStore.loadPermissions()

    ElMessage.success('登录成功')
    router.push('/')
  } catch (error) {
    console.error(error)
    ElMessage.error('登录失败，请检查用户名或密码')
  } finally {
    loading.value = false
  }
}
</script>

<style scoped>
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: var(--bg-body);
  background-image: radial-gradient(var(--border-color) 1px, transparent 1px);
  background-size: 20px 20px;
  position: relative;
}

.theme-switch-wrapper {
  position: absolute;
  top: 20px;
  right: 20px;
}

.login-card {
  width: 400px;
  padding: 40px;
  border-radius: 8px;
  background: var(--bg-card);
  border: 1px solid var(--border-color);
  box-shadow: var(--shadow-card);
}

.card-header {
  text-align: center;
  margin-bottom: 30px;
}

.card-header h2 {
  color: var(--primary-color);
  font-weight: 600;
  font-size: 24px;
  letter-spacing: -0.5px;
}

/* Element Plus Customization for Login */
:deep(.el-form-item__label) {
  color: var(--text-primary);
  font-weight: 500;
}

:deep(.el-input__wrapper) {
  background-color: var(--bg-body);
  box-shadow: 0 0 0 1px var(--border-color) inset;
  padding: 8px 12px;
}

:deep(.el-input__wrapper.is-focus) {
  box-shadow: 0 0 0 2px var(--primary-color) inset;
  background-color: var(--bg-card);
}

:deep(.el-button--primary) {
  height: 40px;
  font-weight: 500;
  letter-spacing: 0.5px;
}
</style>
