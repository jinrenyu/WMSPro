<template>
  <div class="user-list-container">
    <div class="user-layout">
      <!-- 左侧用户分组树 -->
      <GroupPanel
        ref="groupPanelRef"
        prg-key="User"
        title="用户分组"
        @select="handleGroupSelect"
      />

      <!-- 右侧用户列表 -->
      <div class="user-panel">
        <div class="header-actions">
          <el-button
            v-show="!groupPanelRef?.visible"
            class="toggle-group-btn"
            @click="groupPanelRef!.visible = true"
          >
            <el-icon><DArrowRight /></el-icon>
            <span>分组</span>
          </el-button>
          <el-input
            v-model="queryParams.keyword"
            placeholder="搜索用户帐号 / 用户名称"
            class="search-input"
            clearable
            @clear="fetchData"
            @keyup.enter="fetchData"
          >
            <template #append>
              <el-button @click="fetchData"><el-icon><Search /></el-icon></el-button>
            </template>
          </el-input>
        </div>

        <div class="toolbar-actions">
          <el-button type="primary" @click="handleAdd" v-permission="'user:add'">
            <el-icon><Plus /></el-icon> 新增
          </el-button>
          <el-button @click="handleEditSelected" :disabled="!canEdit" v-permission="'user:edit'">
            <el-icon><Edit /></el-icon> 修改
          </el-button>
          <el-button type="danger" @click="handleBatchDelete" :disabled="!canDelete" :loading="batchLoading" v-permission="'user:delete'">
            删除{{ canDelete ? ` (${selectedCount})` : '' }}
          </el-button>
          <el-button type="info" @click="handleBatchDisable" :disabled="!canDisable" :loading="batchLoading" v-permission="'user:toggle-status'">
            禁用{{ canDisable ? ` (${selectedCount})` : '' }}
          </el-button>
          <el-button @click="handleBatchEnable" :disabled="!canEnable" :loading="batchLoading" v-permission="'user:toggle-status'">
            反禁用{{ canEnable ? ` (${selectedCount})` : '' }}
          </el-button>
          <el-button type="warning" @click="handleResetPwd" :disabled="!canResetPwd" v-permission="'user:reset-pwd'">
            重置密码
          </el-button>
          <el-button @click="fetchData"><el-icon><Refresh /></el-icon> 刷新</el-button>
          <el-button @click="handleExport"><el-icon><Download /></el-icon> 导出</el-button>
          <div class="toolbar-spacer" />
          <el-button @click="handleExit"><el-icon><SwitchButton /></el-icon> 退出</el-button>
        </div>

        <el-table
          ref="tableRef"
          v-loading="loading"
          :data="userList"
          style="width: 100%"
          border
          class="user-table"
          @selection-change="handleSelectionChange"
          @row-dblclick="handleRowDblClick"
        >
          <el-table-column type="selection" width="45" fixed="left" :selectable="(row: any) => row.userId !== 'superadmin'" />
          <el-table-column prop="userId" label="用户帐号" width="140" fixed="left">
            <template #default="scope">
              {{ scope.row.userId }}
              <el-tag v-if="scope.row.userId === 'superadmin'" type="info" size="small">内置</el-tag>
            </template>
          </el-table-column>
          <el-table-column prop="userName" label="用户名称" min-width="140" />
          <el-table-column prop="fGroupName" label="用户分组" width="120" />
          <el-table-column prop="domainUser" label="域用户帐号" width="130" />
          <el-table-column prop="paId" label="关联代码" width="130" show-overflow-tooltip />
          <el-table-column prop="email" label="邮箱地址" min-width="160" show-overflow-tooltip />
          <el-table-column label="智能设备用户" width="110" align="center">
            <template #default="scope">
              <el-tag :type="scope.row.isPda ? 'success' : 'info'" size="small">{{ scope.row.isPda ? '是' : '否' }}</el-tag>
            </template>
          </el-table-column>
          <el-table-column label="禁用" width="90" align="center">
            <template #default="scope">
              <el-tag v-if="scope.row.fStatus !== 0" type="danger" size="small">已禁用</el-tag>
              <el-tag v-else type="success" size="small">正常</el-tag>
            </template>
          </el-table-column>
          <el-table-column label="系统预置" width="90" align="center">
            <template #default="scope">
              <el-tag :type="scope.row.isDefault ? 'warning' : 'info'" size="small">{{ scope.row.isDefault ? '是' : '否' }}</el-tag>
            </template>
          </el-table-column>
          <el-table-column label="锁定" width="90" align="center">
            <template #default="scope">
              <el-tag v-if="scope.row.lockStatus === 1" type="danger" size="small">已锁定</el-tag>
              <span v-else>—</span>
            </template>
          </el-table-column>
          <el-table-column label="创建日期" width="170">
            <template #default="scope">{{ fmtDate(scope.row.cYmd) }}</template>
          </el-table-column>
          <el-table-column label="最后登录时间" width="170">
            <template #default="scope">{{ fmtDate(scope.row.lastLoginTime) }}</template>
          </el-table-column>
          <el-table-column label="操作" width="130" fixed="right">
            <template #default="scope">
              <el-button
                v-if="scope.row.userId !== 'superadmin'"
                link
                type="primary"
                size="small"
                @click="handleEdit(scope.row)"
                v-permission="'user:edit'"
              >编辑</el-button>
              <el-tag v-else type="info" size="small">内置账号</el-tag>
              <el-button
                v-if="scope.row.lockStatus === 1"
                link
                type="success"
                size="small"
                @click="handleUnlock(scope.row)"
                v-permission="'user:unlock'"
              >解锁</el-button>
            </template>
          </el-table-column>
        </el-table>

        <div class="pagination-container">
          <el-pagination
            v-model:current-page="queryParams.page"
            v-model:page-size="queryParams.pageSize"
            :total="total"
            layout="total, prev, pager, next"
            @current-change="fetchData"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { getUsers, deleteUser, toggleUserStatus, unlockUser, resetPassword, type User } from '../../api/user'
import { formatDate } from '../../utils/format'
import { ElMessage, ElMessageBox } from 'element-plus'
import { Search, Plus, Edit, Refresh, Download, SwitchButton, DArrowRight } from '@element-plus/icons-vue'
import GroupPanel from '../../components/GroupPanel.vue'

const router = useRouter()
const groupPanelRef = ref<InstanceType<typeof GroupPanel>>()
const tableRef = ref()

const loading = ref(false)
const batchLoading = ref(false)
const userList = ref<User[]>([])
const total = ref(0)
const queryParams = reactive({
  page: 1,
  pageSize: 10,
  keyword: '',
  groupId: ''
})

const selectedRows = ref<User[]>([])
const selectedCount = computed(() => selectedRows.value.length)
const isProtected = (row: User) => row.userId === 'superadmin' || row.userId === 'admin'

const canEdit = computed(() => selectedCount.value === 1)
const canResetPwd = computed(() => selectedCount.value === 1 && selectedRows.value[0].userId !== 'superadmin')
const canDelete = computed(() => selectedCount.value >= 1 && selectedRows.value.every(r => !isProtected(r)))
const canDisable = computed(() => selectedCount.value >= 1 && selectedRows.value.every(r => !isProtected(r) && r.fStatus === 0))
const canEnable = computed(() => selectedCount.value >= 1 && selectedRows.value.every(r => r.fStatus !== 0))

const fmtDate = (d?: string) => {
  if (!d) return ''
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() < 1900) return ''
  return formatDate(d)
}

const handleSelectionChange = (rows: User[]) => {
  selectedRows.value = rows
}

const handleGroupSelect = (groupId: string) => {
  queryParams.groupId = groupId
  queryParams.page = 1
  fetchData()
}

async function fetchData() {
  loading.value = true
  try {
    const res: any = await getUsers(queryParams)
    userList.value = res.data.items
    total.value = res.data.totalCount
  } catch (error) {
    console.error('Fetch users failed:', error)
  } finally {
    loading.value = false
  }
}

const handleAdd = () => {
  router.push({ name: 'UserEdit', query: queryParams.groupId ? { groupId: queryParams.groupId } : {} })
}

const handleEdit = (row: User) => {
  if (row.userId === 'superadmin') {
    ElMessage.info('内置账号 superadmin 不可编辑')
    return
  }
  router.push({ name: 'UserEdit', query: { uid: row.uid } })
}

const handleEditSelected = () => {
  if (selectedRows.value.length === 1) handleEdit(selectedRows.value[0])
}

const handleRowDblClick = (row: User) => {
  handleEdit(row)
}

async function runBatch(rows: User[], action: (id: string) => Promise<any>, actionName: string) {
  batchLoading.value = true
  try {
    const results = await Promise.allSettled(rows.map(r => action(r.uid!)))
    const failed = results.filter(r => r.status === 'rejected').length
    if (failed === 0) ElMessage.success(`${actionName}成功`)
    else ElMessage.warning(`${actionName}完成，${rows.length - failed} 成功，${failed} 失败`)
    await fetchData()
  } finally {
    batchLoading.value = false
  }
}

const handleBatchDelete = () => {
  const rows = selectedRows.value.filter(r => !isProtected(r))
  if (rows.length === 0) return
  ElMessageBox.confirm(`确定要删除选中的 ${rows.length} 个用户吗?`, '警告', {
    confirmButtonText: '确定', cancelButtonText: '取消', type: 'warning'
  }).then(() => runBatch(rows, deleteUser, '删除')).catch(() => {})
}

const handleBatchDisable = () => {
  const rows = selectedRows.value.filter(r => !isProtected(r) && r.fStatus === 0)
  if (rows.length === 0) return
  ElMessageBox.confirm(`确定要禁用选中的 ${rows.length} 个用户吗?`, '提示', {
    confirmButtonText: '确定', cancelButtonText: '取消', type: 'warning'
  }).then(() => runBatch(rows, toggleUserStatus, '禁用')).catch(() => {})
}

const handleBatchEnable = () => {
  const rows = selectedRows.value.filter(r => r.fStatus !== 0)
  if (rows.length === 0) return
  runBatch(rows, toggleUserStatus, '反禁用')
}

const handleUnlock = (row: User) => {
  ElMessageBox.confirm(`确定要解锁用户 "${row.userName}" 吗?`, '提示', {
    confirmButtonText: '确定', cancelButtonText: '取消', type: 'info'
  }).then(async () => {
    try {
      await unlockUser(row.uid!)
      ElMessage.success('解锁成功')
      fetchData()
    } catch (e: any) {
      ElMessage.error(e?.response?.data?.message || '解锁失败')
    }
  }).catch(() => {})
}

const handleResetPwd = () => {
  if (selectedRows.value.length !== 1) return
  const row = selectedRows.value[0]
  ElMessageBox.prompt(`为用户 "${row.userName}" 设置新密码`, '重置密码', {
    confirmButtonText: '确定', cancelButtonText: '取消',
    inputType: 'password',
    inputValidator: (val) => (!val || val.length < 6 ? '密码长度不能少于6位' : true)
  }).then(async ({ value }) => {
    try {
      await resetPassword({ userId: row.userId, newPassword: value })
      ElMessage.success('密码重置成功')
    } catch (e: any) {
      ElMessage.error(e?.response?.data?.message || '重置失败')
    }
  }).catch(() => {})
}

// 导出当前列表为 CSV
const handleExport = () => {
  if (userList.value.length === 0) { ElMessage.warning('暂无数据可导出'); return }
  const headers = ['用户帐号', '用户名称', '用户分组', '域用户帐号', '关联代码', '邮箱地址', '智能设备用户', '禁用', '系统预置', '创建日期', '最后登录时间']
  const escape = (v: any) => `"${String(v ?? '').replace(/"/g, '""')}"`
  const rows = userList.value.map(u => [
    u.userId, u.userName, u.fGroupName, u.domainUser, u.paId, u.email,
    u.isPda ? '是' : '否', u.fStatus !== 0 ? '已禁用' : '正常', u.isDefault ? '是' : '否',
    fmtDate(u.cYmd), fmtDate(u.lastLoginTime)
  ].map(escape).join(','))
  const csv = '﻿' + [headers.map(escape).join(','), ...rows].join('\r\n')
  const blob = new Blob([csv], { type: 'text/csv;charset=utf-8' })
  const link = document.createElement('a')
  link.href = URL.createObjectURL(blob)
  link.download = `用户列表_${formatDate(new Date().toISOString()).slice(0, 10)}.csv`
  link.click()
  URL.revokeObjectURL(link.href)
}

const handleExit = () => {
  router.push('/')
}

onMounted(() => {
  fetchData()
})
</script>

<style scoped>
.user-list-container {
  padding: 20px;
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
}

.user-layout {
  display: flex;
  gap: 16px;
}

.user-panel {
  flex: 1;
  min-width: 0;
}

.header-actions {
  display: flex;
  align-items: center;
  margin-bottom: 12px;
}

.toolbar-actions {
  display: flex;
  align-items: center;
  gap: 8px;
  margin-bottom: 12px;
}

.toolbar-spacer {
  flex: 1;
}

.toggle-group-btn {
  margin-right: 8px;
  flex-shrink: 0;
}

.search-input {
  width: 300px;
}

.pagination-container {
  margin-top: 20px;
  display: flex;
  justify-content: flex-end;
}
</style>
