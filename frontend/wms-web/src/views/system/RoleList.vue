<template>
  <div class="role-list-container">
    <div class="header-actions">
      <el-input
        v-model="queryParams.keyword"
        placeholder="搜索角色名称或代码"
        class="search-input"
        clearable
        @clear="fetchData"
        @keyup.enter="fetchData"
      >
        <template #append>
          <el-button @click="fetchData"><el-icon><Search /></el-icon></el-button>
        </template>
      </el-input>
      <el-button type="primary" @click="handleAdd" v-permission="'role:add'">
        <el-icon><Plus /></el-icon> 新增角色
      </el-button>
    </div>

    <el-table
      v-loading="loading"
      :data="roleList"
      style="width: 100%"
      border
      class="role-table"
    >
      <el-table-column prop="roleNumber" label="角色代码" width="150" />
      <el-table-column prop="roleName" label="角色名称" width="150" />
      <el-table-column prop="roleTypeName" label="角色类型" width="120" />
      <el-table-column prop="note" label="备注" />
      <el-table-column prop="isDefault" label="默认" width="80" align="center">
        <template #default="scope">
          <el-tag :type="scope.row.isDefault ? 'success' : 'info'" size="small">
            {{ scope.row.isDefault ? '是' : '否' }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column label="创建时间" width="180">
        <template #default="scope">
          {{ formatDate(scope.row.cYmd) }}
        </template>
      </el-table-column>
      <el-table-column label="操作" width="200">
        <template #default="scope">
          <el-button size="small" @click="handleEdit(scope.row)" v-permission="'role:edit'">编辑</el-button>
          <el-button size="small" type="success" @click="handlePerm(scope.row)" v-permission="'role:assign'">权限</el-button>
          <el-button
            size="small"
            type="danger"
            @click="handleDelete(scope.row)"
            v-permission="'role:delete'"
          >删除</el-button>
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
      :title="dialogType === 'create' ? '新增角色' : '编辑角色'"
      width="500px"
    >
      <el-form :model="form" label-width="80px">
        <el-form-item label="角色代码">
          <el-input v-model="form.roleNumber" :disabled="dialogType === 'edit'" />
        </el-form-item>
        <el-form-item label="角色名称">
          <el-input v-model="form.roleName" />
        </el-form-item>
        <el-form-item label="角色类型">
          <el-select v-model="form.roleType" placeholder="请选择角色类型">
            <el-option label="系统角色" :value="0" />
            <el-option label="业务角色" :value="1" />
          </el-select>
        </el-form-item>
        <el-form-item label="备注">
          <el-input v-model="form.note" type="textarea" />
        </el-form-item>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="dialogVisible = false">取消</el-button>
          <el-button type="primary" @click="submitForm">确定</el-button>
        </span>
      </template>
    </el-dialog>

    <!-- Permission Dialog -->
    <el-dialog
      v-model="permDialogVisible"
      title="分配权限"
      width="650px"
    >
      <div v-loading="permLoading" class="perm-tree-area">
        <el-collapse v-model="activeGroups">
          <el-collapse-item v-for="group in permissionGroups" :key="group.groupName" :name="group.groupName">
            <template #title>
              <div class="group-title">
                <el-checkbox
                  :model-value="isGroupAllChecked(group)"
                  :indeterminate="isGroupIndeterminate(group)"
                  @change="(val: boolean) => toggleGroup(group, val)"
                  @click.stop
                />
                <span class="group-name">{{ group.groupName }}</span>
              </div>
            </template>
            <div v-for="mod in group.modules" :key="mod.moduleId">
              <!-- 二级目录（如日志管理）：包含三级菜单 -->
              <div v-if="mod.isDirectory && mod.children.length > 0" class="perm-directory">
                <div class="perm-directory-header">
                  <el-checkbox
                    :model-value="isModuleAllChecked(mod)"
                    :indeterminate="isModuleIndeterminate(mod)"
                    @change="(val: boolean) => toggleModule(mod, val)"
                  >
                    {{ mod.moduleName }}
                  </el-checkbox>
                </div>
                <div class="perm-directory-body">
                  <div v-for="child in mod.children" :key="child.moduleId" class="perm-module">
                    <div class="perm-module-header">
                      <el-checkbox
                        :model-value="isLeafModuleAllChecked(child)"
                        :indeterminate="isLeafModuleIndeterminate(child)"
                        @change="(val: boolean) => toggleLeafModule(child, val)"
                      >
                        {{ child.moduleName }}
                      </el-checkbox>
                    </div>
                    <div class="perm-module-body">
                      <el-checkbox
                        v-for="perm in child.permissions"
                        :key="perm.code"
                        :model-value="selectedPermCodes.includes(perm.code)"
                        @change="(val: boolean) => togglePerm(perm.code, val)"
                      >
                        {{ perm.name }}
                        <span class="perm-code-tag">{{ perm.code }}</span>
                      </el-checkbox>
                    </div>
                  </div>
                </div>
              </div>
              <!-- 二级菜单（如用户管理）：直接包含按钮 -->
              <div v-else class="perm-module">
                <div class="perm-module-header">
                  <el-checkbox
                    :model-value="isLeafModuleAllChecked(mod)"
                    :indeterminate="isLeafModuleIndeterminate(mod)"
                    @change="(val: boolean) => toggleLeafModule(mod, val)"
                  >
                    {{ mod.moduleName }}
                  </el-checkbox>
                </div>
                <div class="perm-module-body">
                  <el-checkbox
                    v-for="perm in mod.permissions"
                    :key="perm.code"
                    :model-value="selectedPermCodes.includes(perm.code)"
                    @change="(val: boolean) => togglePerm(perm.code, val)"
                  >
                    {{ perm.name }}
                    <span class="perm-code-tag">{{ perm.code }}</span>
                  </el-checkbox>
                </div>
              </div>
            </div>
          </el-collapse-item>
        </el-collapse>
        <el-empty v-if="permissionGroups.length === 0 && !permLoading" description="暂无权限定义" />
      </div>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="permDialogVisible = false">取消</el-button>
          <el-button type="primary" @click="submitPerm" :loading="permSubmitting">确定</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { getRoles, getRole, createRole, updateRole, deleteRole, getRolePermissions, assignRolePermissions, getPermissionDefinitions, type Role, type PermissionGroup, type PermissionModule } from '../../api/role'
import { formatDate } from '../../utils/format'
import { ElMessage, ElMessageBox } from 'element-plus'
import { Search, Plus } from '@element-plus/icons-vue'

const loading = ref(false)
const roleList = ref<Role[]>([])
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
  roleNumber: '',
  roleName: '',
  roleType: 0,
  note: ''
})

// Permission related
const permDialogVisible = ref(false)
const permLoading = ref(false)
const permSubmitting = ref(false)
const currentRoleUid = ref<string>('')
const selectedPermCodes = ref<string[]>([])
const permissionGroups = ref<PermissionGroup[]>([])
const activeGroups = ref<string[]>([])

const loadPermDefinitions = async () => {
  if (permissionGroups.value.length > 0) return
  try {
    const res: any = await getPermissionDefinitions()
    permissionGroups.value = res.data || []
    activeGroups.value = permissionGroups.value.map(g => g.groupName)
  } catch (error) {
    console.error('Fetch permission definitions failed:', error)
  }
}

// 获取模块下所有权限代码（含 children）
const getAllModuleCodes = (mod: PermissionModule): string[] => {
  const own = mod.permissions.map(p => p.code)
  const childCodes = mod.children.flatMap(c => c.permissions.map(p => p.code))
  return [...own, ...childCodes]
}

const isGroupAllChecked = (group: PermissionGroup) => {
  const allCodes = group.modules.flatMap(m => getAllModuleCodes(m))
  return allCodes.length > 0 && allCodes.every(c => selectedPermCodes.value.includes(c))
}

const isGroupIndeterminate = (group: PermissionGroup) => {
  const allCodes = group.modules.flatMap(m => getAllModuleCodes(m))
  const checkedCount = allCodes.filter(c => selectedPermCodes.value.includes(c)).length
  return checkedCount > 0 && checkedCount < allCodes.length
}

const toggleGroup = (group: PermissionGroup, checked: boolean) => {
  const codes = group.modules.flatMap(m => getAllModuleCodes(m))
  if (checked) {
    const newCodes = codes.filter(c => !selectedPermCodes.value.includes(c))
    selectedPermCodes.value = [...selectedPermCodes.value, ...newCodes]
  } else {
    selectedPermCodes.value = selectedPermCodes.value.filter(c => !codes.includes(c))
  }
}

// 二级目录级别（含 children）
const isModuleAllChecked = (mod: PermissionModule) => {
  const allCodes = getAllModuleCodes(mod)
  return allCodes.length > 0 && allCodes.every(c => selectedPermCodes.value.includes(c))
}

const isModuleIndeterminate = (mod: PermissionModule) => {
  const allCodes = getAllModuleCodes(mod)
  const checkedCount = allCodes.filter(c => selectedPermCodes.value.includes(c)).length
  return checkedCount > 0 && checkedCount < allCodes.length
}

const toggleModule = (mod: PermissionModule, checked: boolean) => {
  const codes = getAllModuleCodes(mod)
  if (checked) {
    const newCodes = codes.filter(c => !selectedPermCodes.value.includes(c))
    selectedPermCodes.value = [...selectedPermCodes.value, ...newCodes]
  } else {
    selectedPermCodes.value = selectedPermCodes.value.filter(c => !codes.includes(c))
  }
}

// 叶子模块级别（仅自身 permissions）
const isLeafModuleAllChecked = (mod: PermissionModule) => {
  return mod.permissions.length > 0 && mod.permissions.every(p => selectedPermCodes.value.includes(p.code))
}

const isLeafModuleIndeterminate = (mod: PermissionModule) => {
  const checkedCount = mod.permissions.filter(p => selectedPermCodes.value.includes(p.code)).length
  return checkedCount > 0 && checkedCount < mod.permissions.length
}

const toggleLeafModule = (mod: PermissionModule, checked: boolean) => {
  const codes = mod.permissions.map(p => p.code)
  if (checked) {
    const newCodes = codes.filter(c => !selectedPermCodes.value.includes(c))
    selectedPermCodes.value = [...selectedPermCodes.value, ...newCodes]
  } else {
    selectedPermCodes.value = selectedPermCodes.value.filter(c => !codes.includes(c))
  }
}

const togglePerm = (code: string, checked: boolean) => {
  if (checked) {
    if (!selectedPermCodes.value.includes(code)) {
      selectedPermCodes.value = [...selectedPermCodes.value, code]
    }
  } else {
    selectedPermCodes.value = selectedPermCodes.value.filter(c => c !== code)
  }
}

const handlePerm = async (row: Role) => {
  currentRoleUid.value = row.uid!
  permDialogVisible.value = true
  permLoading.value = true

  try {
    await loadPermDefinitions()
    const res: any = await getRolePermissions(row.uid!)
    selectedPermCodes.value = res.data || []
  } catch (error) {
    console.error('Fetch permissions failed:', error)
    selectedPermCodes.value = []
  } finally {
    permLoading.value = false
  }
}

const submitPerm = async () => {
  permSubmitting.value = true
  try {
    await assignRolePermissions(currentRoleUid.value, selectedPermCodes.value)
    ElMessage.success('权限分配成功')
    permDialogVisible.value = false
  } catch (error) {
    console.error('Assign permissions failed:', error)
  } finally {
    permSubmitting.value = false
  }
}

const fetchData = async () => {
  loading.value = true
  try {
    const res: any = await getRoles(queryParams)
    roleList.value = res.data.items
    total.value = res.data.totalCount
  } catch (error) {
    console.error('Fetch roles failed:', error)
  } finally {
    loading.value = false
  }
}

const handleAdd = () => {
  dialogType.value = 'create'
  Object.assign(form, {
    uid: undefined,
    roleNumber: '',
    roleName: '',
    roleType: 0,
    note: ''
  })
  dialogVisible.value = true
}

const handleEdit = async (row: Role) => {
  dialogType.value = 'edit'
  try {
    const res: any = await getRole(row.uid!)
    const detail = res.data
    Object.assign(form, {
      uid: detail.uid,
      roleNumber: detail.roleNumber,
      roleName: detail.roleName,
      roleType: detail.roleType,
      note: detail.note
    })
  } catch (error) {
    console.error('Fetch role details failed:', error)
    Object.assign(form, {
      uid: row.uid,
      roleNumber: row.roleNumber,
      roleName: row.roleName,
      roleType: row.roleType,
      note: row.note || ''
    })
  }
  dialogVisible.value = true
}

const handleDelete = (row: Role) => {
  ElMessageBox.confirm('确定要删除该角色吗?', '警告', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    await deleteRole(row.uid!)
    ElMessage.success('删除成功')
    fetchData()
  })
}

const submitForm = async () => {
  if (!form.roleName || !form.roleNumber) {
    ElMessage.warning('请输入角色名称和代码')
    return
  }

  try {
    if (dialogType.value === 'create') {
      await createRole({
        roleNumber: form.roleNumber,
        roleName: form.roleName,
        roleType: form.roleType,
        note: form.note
      })
      ElMessage.success('创建成功')
    } else {
      await updateRole(form.uid!, {
        roleName: form.roleName,
        roleType: form.roleType,
        note: form.note
      })
      ElMessage.success('更新成功')
    }
    dialogVisible.value = false
    fetchData()
  } catch (error) {
    console.error('Submit role failed:', error)
  }
}

onMounted(() => {
  fetchData()
})
</script>

<style scoped>
.role-list-container {
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

.perm-tree-area {
  max-height: 500px;
  overflow-y: auto;
  padding: 4px 0;
}

.group-title {
  display: flex;
  align-items: center;
  gap: 8px;
}

.group-name {
  font-weight: 600;
  font-size: 14px;
}

.perm-directory {
  margin-bottom: 12px;
  border: 1px solid var(--border-color, #e4e7ed);
  border-radius: 6px;
  overflow: hidden;
}

.perm-directory-header {
  padding: 8px 16px;
  background-color: var(--bg-body, #f5f7fa);
  font-weight: 500;
  border-bottom: 1px solid var(--border-color, #e4e7ed);
}

.perm-directory-body {
  padding: 8px 12px;
}

.perm-module {
  margin-bottom: 12px;
  border: 1px solid var(--border-color, #e4e7ed);
  border-radius: 6px;
  overflow: hidden;
}

.perm-module:last-child {
  margin-bottom: 0;
}

.perm-module-header {
  padding: 8px 16px;
  background-color: var(--bg-body, #f5f7fa);
  font-weight: 500;
  border-bottom: 1px solid var(--border-color, #e4e7ed);
}

.perm-module-body {
  padding: 10px 16px;
  display: flex;
  flex-wrap: wrap;
  gap: 10px 24px;
}

.perm-code-tag {
  font-size: 11px;
  color: var(--text-secondary, #909399);
  margin-left: 4px;
}
</style>
