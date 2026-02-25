<template>
  <div class="user-list-container">
    <div class="header-actions">
      <el-input
        v-model="queryParams.keyword"
        placeholder="搜索用户名或邮箱"
        class="search-input"
        clearable
        @clear="fetchData"
        @keyup.enter="fetchData"
      >
        <template #append>
          <el-button @click="fetchData"><el-icon><Search /></el-icon></el-button>
        </template>
      </el-input>
      <el-button type="primary" @click="handleAdd" v-permission="'user:add'">
        <el-icon><Plus /></el-icon> 新增用户
      </el-button>
    </div>

    <el-table
      v-loading="loading"
      :data="userList"
      style="width: 100%"
      border
      class="user-table"
    >
      <el-table-column prop="userId" label="用户ID" width="150" />
      <el-table-column prop="userName" label="用户名" width="150" />
      <el-table-column prop="email" label="邮箱" width="200" />
      <el-table-column label="角色" min-width="150">
        <template #default="scope">
          {{ (scope.row.roles || []).join(', ') }}
        </template>
      </el-table-column>
      <el-table-column prop="fStatus" label="状态" width="100">
        <template #default="scope">
          <el-tag :type="scope.row.fStatus === 0 ? 'success' : 'danger'">
            {{ scope.row.fStatus === 0 ? '启用' : '禁用' }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column label="创建时间" width="180">
        <template #default="scope">
          {{ formatDate(scope.row.cYmd) }}
        </template>
      </el-table-column>
      <el-table-column label="操作" min-width="380">
        <template #default="scope">
          <template v-if="scope.row.userId === 'superadmin'">
            <el-tag type="info" size="small">内置账号，不可操作</el-tag>
          </template>
          <template v-else>
            <el-button size="small" @click="handleEdit(scope.row)" v-permission="'user:edit'">编辑</el-button>
            <el-button size="small" type="warning" @click="handleRole(scope.row)" v-permission="'user:assign'">角色</el-button>
            <el-button size="small" type="info" @click="handleResetPwd(scope.row)" v-permission="'user:reset-pwd'">重置密码</el-button>
            <el-button
              v-if="scope.row.lockStatus === 1"
              size="small"
              type="success"
              @click="handleUnlock(scope.row)"
              v-permission="'user:unlock'"
            >解锁</el-button>
            <el-button
              size="small"
              :type="scope.row.fStatus === 0 ? 'warning' : 'success'"
              plain
              @click="handleToggleStatus(scope.row)"
              v-permission="'user:toggle-status'"
            >{{ scope.row.fStatus === 0 ? '禁用' : '启用' }}</el-button>
            <el-button
              size="small"
              type="danger"
              @click="handleDelete(scope.row)"
              v-permission="'user:delete'"
            >删除</el-button>
          </template>
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

    <!-- Create/Edit Dialog -->
    <el-dialog
      v-model="dialogVisible"
      :title="dialogType === 'create' ? '新增用户' : '编辑用户'"
      width="500px"
    >
      <el-form :model="form" label-width="80px">
        <el-form-item label="用户ID" v-if="dialogType === 'create'">
          <el-input v-model="form.userId" />
        </el-form-item>
        <el-form-item label="用户ID" v-else>
          <el-input v-model="form.userId" disabled />
        </el-form-item>
        <el-form-item label="用户名">
          <el-input v-model="form.userName" />
        </el-form-item>
        <el-form-item label="密码" v-if="dialogType === 'create'">
          <el-input
            v-model="form.password"
            type="password"
            placeholder="请输入密码"
            show-password
          />
        </el-form-item>
        <el-form-item label="邮箱">
          <el-input v-model="form.email" />
        </el-form-item>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="dialogVisible = false">取消</el-button>
          <el-button type="primary" @click="submitForm">确定</el-button>
        </span>
      </template>
    </el-dialog>

    <!-- Role Dialog -->
    <el-dialog
      v-model="roleDialogVisible"
      title="分配角色"
      width="500px"
    >
      <el-checkbox-group v-model="selectedRoleIds">
        <el-checkbox v-for="role in allRoles" :key="role.uid" :value="role.uid!">
          {{ role.roleName }}
        </el-checkbox>
      </el-checkbox-group>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="roleDialogVisible = false">取消</el-button>
          <el-button type="primary" @click="submitRole">确定</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { getUsers, getUser, createUser, updateUser, deleteUser, assignUserRoles, resetPassword, unlockUser, toggleUserStatus, type User } from '../../api/user'
import { getRoles, type Role } from '../../api/role'
import { formatDate } from '../../utils/format'
import { ElMessage, ElMessageBox } from 'element-plus'
import { Search, Plus } from '@element-plus/icons-vue'

const loading = ref(false)
const userList = ref<User[]>([])
const total = ref(0)
const queryParams = reactive({
  page: 1,
  pageSize: 10,
  keyword: ''
})

const dialogVisible = ref(false)
const dialogType = ref<'create' | 'edit'>('create')
const form = reactive({
  uid: undefined as string | undefined,
  userId: '',
  userName: '',
  password: '',
  email: ''
})

const fetchData = async () => {
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
  dialogType.value = 'create'
  Object.assign(form, {
    uid: undefined,
    userId: '',
    userName: '',
    password: '',
    email: ''
  })
  dialogVisible.value = true
}

const handleEdit = (row: User) => {
  dialogType.value = 'edit'
  Object.assign(form, {
    uid: row.uid,
    userId: row.userId,
    userName: row.userName,
    password: '',
    email: row.email || ''
  })
  dialogVisible.value = true
}

// Role Assignment
const roleDialogVisible = ref(false)
const allRoles = ref<Role[]>([])
const selectedRoleIds = ref<string[]>([])
const currentUserUid = ref<string>('')

const handleRole = async (row: User) => {
  currentUserUid.value = row.uid!
  roleDialogVisible.value = true

  // Load all roles
  if (allRoles.value.length === 0) {
    const res: any = await getRoles({ page: 1, pageSize: 100 })
    allRoles.value = res.data.items
  }

  // Load user's current roles from user detail
  try {
    const res: any = await getUser(row.uid!)
    const userDetail = res.data
    if (userDetail.roleDetails && userDetail.roleDetails.length > 0) {
      selectedRoleIds.value = userDetail.roleDetails.map((r: any) => r.roleId)
    } else {
      selectedRoleIds.value = []
    }
  } catch (error) {
    console.error('Fetch user roles failed:', error)
    selectedRoleIds.value = []
  }
}

const submitRole = async () => {
  try {
    await assignUserRoles(currentUserUid.value, selectedRoleIds.value)
    ElMessage.success('角色分配成功')
    roleDialogVisible.value = false
    fetchData()
  } catch (error) {
    console.error('Assign roles failed:', error)
  }
}

const handleDelete = (row: User) => {
  ElMessageBox.confirm('确定要删除该用户吗?', '警告', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    await deleteUser(row.uid!)
    ElMessage.success('删除成功')
    fetchData()
  })
}

const handleResetPwd = (row: User) => {
  ElMessageBox.prompt('请输入新密码', '重置密码', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    inputType: 'password',
    inputValidator: (val) => {
      if (!val || val.length < 6) return '密码长度不能少于6位'
      return true
    }
  }).then(async ({ value }) => {
    try {
      await resetPassword({ userId: row.userId, newPassword: value })
      ElMessage.success('密码重置成功')
    } catch (error: any) {
      ElMessage.error(error.response?.data?.message || '重置失败')
    }
  })
}

const handleUnlock = (row: User) => {
  ElMessageBox.confirm(`确定要解锁用户 "${row.userName}" 吗?`, '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'info'
  }).then(async () => {
    try {
      await unlockUser(row.uid!)
      ElMessage.success('解锁成功')
      fetchData()
    } catch (error: any) {
      ElMessage.error(error.response?.data?.message || '解锁失败')
    }
  })
}

const handleToggleStatus = (row: User) => {
  const action = row.fStatus === 0 ? '禁用' : '启用'
  ElMessageBox.confirm(`确定要${action}用户 "${row.userName}" 吗?`, '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await toggleUserStatus(row.uid!)
      ElMessage.success(`${action}成功`)
      fetchData()
    } catch (error: any) {
      ElMessage.error(error.response?.data?.message || `${action}失败`)
    }
  })
}

const submitForm = async () => {
  if (dialogType.value === 'create') {
    if (!form.userId || !form.userName || !form.password) {
      ElMessage.warning('请输入用户ID、用户名和密码')
      return
    }
    if (form.password.length < 6) {
      ElMessage.warning('密码长度不能少于6位')
      return
    }
  } else {
    if (!form.userName) {
      ElMessage.warning('请输入用户名')
      return
    }
  }

  try {
    if (dialogType.value === 'create') {
      await createUser({
        userId: form.userId,
        userName: form.userName,
        password: form.password,
        email: form.email
      })
      ElMessage.success('创建成功')
    } else {
      await updateUser(form.uid!, {
        userName: form.userName,
        email: form.email
      })
      ElMessage.success('更新成功')
    }
    dialogVisible.value = false
    fetchData()
  } catch (error: any) {
    console.error('Submit user failed:', error)
    if (error.response && error.response.data) {
      const data = error.response.data
      const errorMsg = data.message || data.title || JSON.stringify(data.errors || data)
      ElMessage.error(errorMsg)
    } else {
      ElMessage.error('提交失败')
    }
  }
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

.header-actions {
  display: flex;
  justify-content: space-between;
  margin-bottom: 20px;
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
