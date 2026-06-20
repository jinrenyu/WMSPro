<template>
  <div class="login-page">
    <div class="theme-switch-wrapper">
      <ThemeToggle />
    </div>

    <div class="login-shell">
      <!-- 左：品牌墙（始终深色，含装饰与产品亮点） -->
      <aside class="brand-panel">
        <span class="bp-grid"></span>
        <span class="bp-glow bp-glow--blue"></span>
        <span class="bp-glow bp-glow--amber"></span>

        <div class="brand-content">
          <div class="brand-logo">
            <span class="logo-mark"><el-icon><Box /></el-icon></span>
            <span>WMS Pro</span>
          </div>

          <div class="brand-main">
            <h1 class="brand-title">智能仓储管理系统</h1>
            <p class="brand-tagline">入库 · 出库 · 采购协同 · 条码追溯，一体化的数字仓储平台。</p>

            <ul class="brand-features">
              <li class="feature">
                <span class="f-icon"><el-icon><Tickets /></el-icon></span>
                <span class="f-text">
                  <strong>全流程单据数字化</strong>
                  <span>采购、收料、入库、退料逐单流转</span>
                </span>
              </li>
              <li class="feature">
                <span class="f-icon"><el-icon><DataLine /></el-icon></span>
                <span class="f-text">
                  <strong>库存实时可视</strong>
                  <span>即时库存、批次与货位精准掌握</span>
                </span>
              </li>
              <li class="feature">
                <span class="f-icon"><el-icon><Connection /></el-icon></span>
                <span class="f-text">
                  <strong>条码标签全链路追溯</strong>
                  <span>从打印到出入库，来源去向可查</span>
                </span>
              </li>
            </ul>
          </div>

          <div class="brand-footer">© 2026 WMS Pro · 企业仓储数字化平台</div>
        </div>
      </aside>

      <!-- 右：登录表单 -->
      <section class="form-panel">
        <div class="form-inner">
          <div class="form-head">
            <span class="form-badge"><el-icon><Box /></el-icon></span>
            <h2 class="form-title">欢迎登录</h2>
            <p class="form-subtitle">请输入账号信息以继续</p>
          </div>

          <el-form :model="loginForm" label-width="0" size="large" class="login-form">
            <el-form-item>
              <el-input
                v-model="loginForm.username"
                placeholder="用户名 / Username"
                prefix-icon="User"
                aria-label="用户名"
              />
            </el-form-item>
            <el-form-item>
              <el-input
                v-model="loginForm.password"
                type="password"
                placeholder="密码 / Password"
                show-password
                prefix-icon="Lock"
                aria-label="密码"
              />
            </el-form-item>
            <el-form-item>
              <el-button type="primary" class="login-btn" @click="handleLogin" :loading="loading">
                登 录 / LOGIN
              </el-button>
            </el-form-item>
          </el-form>

          <p class="form-hint">登录即代表你已阅读并同意系统使用规范</p>
        </div>
      </section>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import request from '../utils/request'
import { setAccessToken, setRefreshToken, clearTokens } from '../utils/token'
import { encryptPassword, fetchPublicKey } from '../utils/crypto'
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

// 预获取 RSA 公钥
onMounted(() => {
  fetchPublicKey().catch(() => {})
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
    const encryptedPwd = await encryptPassword(loginForm.password)
    const data: any = await request.post('/auth/login', {
      userId: loginForm.username,
      password: encryptedPwd
    })

    const token = data.data.token
    const refreshToken = data.data.refreshToken
    const userInfo = data.data.userInfo

    setAccessToken(token)
    if (refreshToken) {
      setRefreshToken(refreshToken)
    }
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
.login-page {
  min-height: 100vh;
  display: flex;
  position: relative;
  background-color: var(--bg-card);
}

.theme-switch-wrapper {
  position: absolute;
  top: 20px;
  right: 20px;
  z-index: 2;
}

/* ---------- 双栏外壳：铺满全屏 ---------- */
.login-shell {
  width: 100%;
  min-height: 100vh;
  display: flex;
  background: var(--bg-card);
}

/* ---------- 左侧品牌墙（始终深色，撑满全高） ---------- */
.brand-panel {
  position: relative;
  flex: 1.05;
  padding: clamp(48px, 6vh, 96px) clamp(44px, 5vw, 88px);
  color: #ffffff;
  overflow: hidden;
  background: linear-gradient(155deg, #0f172a 0%, #14306e 56%, #1e40af 128%);
}

.bp-grid {
  position: absolute;
  inset: 0;
  background-image:
    linear-gradient(rgba(255, 255, 255, 0.05) 1px, transparent 1px),
    linear-gradient(90deg, rgba(255, 255, 255, 0.05) 1px, transparent 1px);
  background-size: 26px 26px;
  -webkit-mask-image: radial-gradient(circle at 28% 18%, #000, transparent 78%);
  mask-image: radial-gradient(circle at 28% 18%, #000, transparent 78%);
  opacity: 0.55;
}

.bp-glow {
  position: absolute;
  border-radius: 50%;
  filter: blur(64px);
  pointer-events: none;
}
.bp-glow--blue {
  width: 320px;
  height: 320px;
  top: -86px;
  right: -64px;
  background: rgba(59, 130, 246, 0.45);
}
.bp-glow--amber {
  width: 240px;
  height: 240px;
  bottom: -76px;
  left: -52px;
  background: rgba(217, 119, 6, 0.34);
}

.brand-content {
  position: relative;
  z-index: 1;
  height: 100%;
  width: 100%;
  max-width: 480px;
  margin: 0 auto;
  display: flex;
  flex-direction: column;
}

.brand-logo {
  display: flex;
  align-items: center;
  gap: 12px;
  font-size: 20px;
  font-weight: 700;
  letter-spacing: 0.3px;
}
.logo-mark {
  width: 42px;
  height: 42px;
  border-radius: 12px;
  display: grid;
  place-items: center;
  font-size: 23px;
  color: #fbbf24;
  background: rgba(255, 255, 255, 0.1);
  border: 1px solid rgba(255, 255, 255, 0.18);
}

.brand-main {
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: center;
}
.brand-title {
  margin: 0 0 12px;
  font-size: clamp(28px, 3.2vw, 38px);
  line-height: 1.25;
  font-weight: 700;
  letter-spacing: -0.3px;
}
.brand-tagline {
  margin: 0;
  max-width: 340px;
  font-size: 14px;
  line-height: 1.7;
  color: rgba(255, 255, 255, 0.72);
}

.brand-features {
  list-style: none;
  margin: 34px 0 0;
  padding: 0;
  display: flex;
  flex-direction: column;
  gap: 16px;
}
.feature {
  display: flex;
  gap: 13px;
  align-items: flex-start;
}
.f-icon {
  width: 38px;
  height: 38px;
  flex-shrink: 0;
  border-radius: 10px;
  display: grid;
  place-items: center;
  font-size: 19px;
  color: #93c5fd;
  background: rgba(255, 255, 255, 0.1);
  border: 1px solid rgba(255, 255, 255, 0.16);
}
.f-text {
  display: flex;
  flex-direction: column;
  gap: 2px;
  padding-top: 1px;
}
.f-text strong {
  font-size: 14px;
  font-weight: 600;
}
.f-text span {
  font-size: 12.5px;
  line-height: 1.5;
  color: rgba(255, 255, 255, 0.6);
}

.brand-footer {
  margin-top: 28px;
  font-size: 12px;
  color: rgba(255, 255, 255, 0.45);
}

/* ---------- 右侧表单区 ---------- */
.form-panel {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 48px 44px;
  background: var(--bg-card);
}
.form-inner {
  width: 100%;
  max-width: 336px;
}

.form-head {
  margin-bottom: 26px;
}
.form-badge {
  display: none;
}
.form-title {
  margin: 0;
  font-size: 24px;
  font-weight: 700;
  letter-spacing: -0.3px;
  color: var(--text-primary);
}
.form-subtitle {
  margin: 8px 0 0;
  font-size: 14px;
  color: var(--text-secondary);
}

.login-form :deep(.el-form-item) {
  margin-bottom: 20px;
}

:deep(.el-input__wrapper) {
  background-color: var(--bg-body);
  box-shadow: 0 0 0 1px var(--border-color) inset;
  padding: 9px 14px;
  border-radius: var(--radius-base);
}
:deep(.el-input__wrapper.is-focus) {
  box-shadow: 0 0 0 2px var(--primary-color) inset;
  background-color: var(--bg-card);
}

.login-btn {
  width: 100%;
  height: 46px;
  margin-top: 4px;
  font-weight: 600;
  letter-spacing: 1px;
  border: none;
  border-radius: var(--radius-base);
  background: linear-gradient(135deg, var(--primary-color), #2563eb);
}
.login-btn:hover {
  box-shadow: 0 8px 18px -6px rgba(30, 64, 175, 0.5);
}

.form-hint {
  margin: 22px 0 0;
  text-align: center;
  font-size: 12px;
  color: var(--text-placeholder);
}

/* ---------- 响应式：窄屏隐藏品牌墙 ---------- */
@media (max-width: 880px) {
  .brand-panel {
    display: none;
  }
  .form-panel {
    padding: 44px 34px;
  }
  .form-badge {
    display: grid;
    place-items: center;
    width: 52px;
    height: 52px;
    margin-bottom: 16px;
    border-radius: 14px;
    font-size: 26px;
    color: #ffffff;
    background: linear-gradient(135deg, var(--primary-color), #2563eb);
    box-shadow: 0 8px 18px -6px rgba(30, 64, 175, 0.5);
  }
}

@media (max-width: 480px) {
  .login-page {
    padding: 14px;
  }
  .form-panel {
    padding: 36px 24px;
  }
}
</style>
