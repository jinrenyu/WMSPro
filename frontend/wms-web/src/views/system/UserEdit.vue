<template>
  <div class="user-edit-container">
    <!-- 顶部工具栏 -->
    <div class="edit-toolbar">
      <el-button type="primary" :disabled="isReadonly" @click="handleSave" v-permission="isEdit ? 'user:edit' : 'user:add'">
        <el-icon><Check /></el-icon> 保存
      </el-button>
      <el-button v-if="isEdit" :disabled="isReadonly" @click="handleCopy" v-permission="'user:add'">
        <el-icon><DocumentCopy /></el-icon> 复制
      </el-button>
      <el-button v-if="showDisable" type="info" @click="handleToggleStatus" v-permission="'user:toggle-status'">禁用</el-button>
      <el-button v-if="showEnable" type="success" @click="handleToggleStatus" v-permission="'user:toggle-status'">反禁用</el-button>

      <el-divider direction="vertical" />
      <el-button-group v-if="isEdit">
        <el-button :disabled="!canPrev" @click="handleFirst">首张</el-button>
        <el-button :disabled="!canPrev" @click="handlePrev">前一</el-button>
        <el-button :disabled="!canNext" @click="handleNext">后一</el-button>
        <el-button :disabled="!canNext" @click="handleLast">末张</el-button>
      </el-button-group>

      <div class="toolbar-spacer" />
      <el-tag v-if="isEdit" :type="form.fStatus === 0 ? 'success' : 'danger'" size="large">
        {{ form.fStatus === 0 ? '正常' : '已禁用' }}
      </el-tag>
      <el-tag v-if="isEdit && form.lockStatus === 1" type="danger" size="large" style="margin-left: 8px;">已锁定</el-tag>
      <el-tag v-if="isSuperadmin" type="info" size="large" style="margin-left: 8px;">内置账号只读</el-tag>
      <el-button class="back-btn" @click="handleBack">
        <el-icon><Back /></el-icon> 退出
      </el-button>
    </div>

    <el-form
      ref="formRef"
      :model="form"
      :rules="rules"
      label-width="100px"
      :disabled="isReadonly"
      class="edit-form"
      v-loading="loading"
    >
      <div class="form-header">
        <el-row :gutter="20">
          <el-col :span="8">
            <el-form-item label="用户帐号" prop="userId">
              <el-input v-model="form.userId" :disabled="isEdit" placeholder="必填，编辑时不可修改" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="用户名称" prop="userName">
              <el-input v-model="form.userName" placeholder="必填" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="智能设备用户">
              <el-checkbox v-model="form.isPda" />
            </el-form-item>
          </el-col>
        </el-row>

        <!-- 密码：仅新增态录入 -->
        <el-row :gutter="20" v-if="!isEdit">
          <el-col :span="8">
            <el-form-item label="密码" prop="password">
              <el-input v-model="form.password" type="password" show-password placeholder="必填，至少6位" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="8">
            <el-form-item label="用户ID">
              <el-input v-model="form.cardId" placeholder="用户ID卡" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="邮箱地址">
              <el-input v-model="form.email" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="用户分组">
              <el-tree-select
                v-model="form.fGroupId"
                :data="groupTree"
                :props="{ label: 'fName', children: 'children', value: 'uid' }"
                placeholder="请选择用户分组"
                clearable
                check-strictly
                style="width: 100%"
              />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="8">
            <el-form-item label="关联对象类别">
              <el-select v-model="form.paType" clearable placeholder="请选择" style="width: 100%" @change="handlePaTypeChange">
                <el-option v-for="o in paTypeOptions" :key="o.value" :label="o.label" :value="o.value" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="关联代码">
              <el-input v-model="form.paId" placeholder="职员/厂商/客户内码" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="禁用">
              <el-checkbox :model-value="form.fStatus !== 0" disabled />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="8">
            <el-form-item label="域用户帐号">
              <el-input v-model="form.domainUser" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="第三方ERP帐号">
              <el-input v-model="form.erpId" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="最后登录时间">
              <el-input :model-value="fmtDate(form.lastLoginTime)" disabled />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="8">
            <el-form-item label="手机号">
              <el-input v-model="form.mobile" placeholder="用于短信验证码(MFA)" maxlength="11" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="短信验证登录">
              <el-switch v-model="form.mfaEnabled" />
              <span class="mfa-hint">开启后该用户登录需短信验证码（需全局开启且已填手机号）</span>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="24">
            <el-form-item label="用户照片">
              <div class="picture-uploader">
                <el-upload
                  :auto-upload="false"
                  :show-file-list="false"
                  accept="image/*"
                  :disabled="isReadonly"
                  @change="handlePhotoChange"
                >
                  <el-button :disabled="isReadonly">
                    <el-icon><Picture /></el-icon> 选择图片
                  </el-button>
                </el-upload>
                <el-button v-if="form.photoBase64" link type="danger" :disabled="isReadonly" @click="form.photoBase64 = null">移除</el-button>
                <div v-if="photoSrc" class="picture-preview">
                  <img :src="photoSrc" alt="用户照片" />
                </div>
              </div>
            </el-form-item>
          </el-col>
        </el-row>
      </div>

      <!-- 明细 Tab -->
      <el-tabs v-model="activeTab" class="edit-tabs">
        <!-- ============ 角色明细 ============ -->
        <el-tab-pane label="角色明细" name="roles">
          <div class="grid-toolbar">
            <el-button type="primary" size="small" :disabled="gridDisabled" @click="addRole">
              <el-icon><Plus /></el-icon> 行新增
            </el-button>
          </div>
          <el-table :data="roleRows" border size="small" empty-text="暂无角色，点击「行新增」分配">
            <el-table-column type="index" label="#" width="45" align="center" />
            <el-table-column label="角色代码" min-width="220">
              <template #default="{ row }">
                <LookupSelect
                  v-model="row.roleId"
                  module="role"
                  placeholder="选择角色"
                  preload
                  :disabled="gridDisabled"
                  @change="(item: any) => onRoleChange(row, item)"
                />
              </template>
            </el-table-column>
            <el-table-column label="角色名称" min-width="180">
              <template #default="{ row }">{{ row.roleName }}</template>
            </el-table-column>
            <el-table-column label="操作" width="70" align="center" fixed="right">
              <template #default="{ $index }">
                <el-button link type="danger" :disabled="gridDisabled" @click="roleRows.splice($index, 1)">行删除</el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-tab-pane>

        <!-- ============ 组织明细 ============ -->
        <el-tab-pane label="组织明细" name="orgs">
          <div class="grid-toolbar">
            <el-button type="primary" size="small" :disabled="gridDisabled" @click="addOrg">
              <el-icon><Plus /></el-icon> 行新增
            </el-button>
          </div>
          <el-table :data="orgRows" border size="small" empty-text="暂无组织，点击「行新增」分配">
            <el-table-column type="index" label="#" width="45" align="center" />
            <el-table-column label="组织代码" min-width="220">
              <template #default="{ row }">
                <LookupSelect
                  v-model="row.orgId"
                  module="org"
                  placeholder="选择组织"
                  preload
                  :disabled="gridDisabled"
                  @change="(item: any) => onOrgChange(row, item)"
                />
              </template>
            </el-table-column>
            <el-table-column label="组织名称" min-width="180">
              <template #default="{ row }">{{ row.orgName }}</template>
            </el-table-column>
            <el-table-column label="操作" width="70" align="center" fixed="right">
              <template #default="{ $index }">
                <el-button link type="danger" :disabled="gridDisabled" @click="orgRows.splice($index, 1)">行删除</el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-tab-pane>
      </el-tabs>
    </el-form>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { ElMessage } from 'element-plus'
import type { FormInstance, FormRules } from 'element-plus'
import { Check, Back, Plus, Picture, DocumentCopy } from '@element-plus/icons-vue'
import {
  getUsers, getUser, createUser, updateUser,
  assignUserRoles, assignUserOrgs, toggleUserStatus,
  type UserRoleRow, type UserOrgRow
} from '../../api/user'
import type { LookupItem } from '../../api/lookup'
import { getGroupTree, type BaseDataGroup } from '../../api/baseDataGroup'
import LookupSelect from '../../components/LookupSelect.vue'
import { formatDate } from '../../utils/format'
import { usePermissionStore } from '../../stores/permission'

const router = useRouter()
const route = useRoute()
const permissionStore = usePermissionStore()

const formRef = ref<FormInstance>()
const loading = ref(false)
const activeTab = ref('roles')
const groupTree = ref<BaseDataGroup[]>([])

const uid = ref<string>((route.query.uid as string) || '')
const isEdit = computed(() => !!uid.value)
const isSuperadmin = computed(() => form.userId === 'superadmin')
const isReadonly = computed(() => isSuperadmin.value)

// 编辑态维护角色/组织明细需要 user:assign 权限；新增态经 POST /user(user:add) 内联落库，不受此限
const canAssign = computed(() => permissionStore.hasPermission('user:assign'))
const gridDisabled = computed(() => isReadonly.value || (isEdit.value && !canAssign.value))

const showDisable = computed(() => isEdit.value && !isSuperadmin.value && form.userId !== 'admin' && form.fStatus === 0)
const showEnable = computed(() => isEdit.value && form.fStatus !== 0)

const paTypeOptions = [
  { value: '1', label: '职员' },
  { value: '2', label: '厂商' },
  { value: '3', label: '客户' },
]

const rules: FormRules = {
  userId: [{ required: true, message: '请输入用户帐号', trigger: 'blur' }],
  userName: [{ required: true, message: '请输入用户名称', trigger: 'blur' }],
  password: [
    { required: true, message: '请输入密码', trigger: 'blur' },
    { min: 6, message: '密码长度不能少于6位', trigger: 'blur' },
  ],
}

const defaultForm = {
  uid: '' as string,
  userId: '',
  userName: '',
  password: '',
  email: '',
  mobile: '',
  mfaEnabled: false,
  paType: '',
  paId: '',
  isPda: false,
  isDefault: false,
  domainUser: '',
  cardId: '',
  erpId: '',
  fGroupId: '',
  fStatus: 0,
  lockStatus: 0,
  lastLoginTime: '',
  companyId: '',
  photoBase64: null as string | null,
}

const form = reactive({ ...defaultForm })

const roleRows = ref<UserRoleRow[]>([])
const orgRows = ref<UserOrgRow[]>([])

// 导航用：全部用户 uid 有序列表
const allUids = ref<string[]>([])
const currentIndex = computed(() => allUids.value.indexOf(uid.value))
const canPrev = computed(() => currentIndex.value > 0)
const canNext = computed(() => currentIndex.value >= 0 && currentIndex.value < allUids.value.length - 1)

const fmtDate = (d?: string) => {
  if (!d) return ''
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() < 1900) return ''
  return formatDate(d)
}

// ---- 照片 ----
const photoSrc = computed(() => {
  const b64 = form.photoBase64
  if (!b64) return ''
  let mime = 'image/png'
  if (b64.startsWith('/9j/')) mime = 'image/jpeg'
  else if (b64.startsWith('iVBOR')) mime = 'image/png'
  else if (b64.startsWith('R0lGOD')) mime = 'image/gif'
  else if (b64.startsWith('UklGR')) mime = 'image/webp'
  return `data:${mime};base64,${b64}`
})

const handlePhotoChange = (uploadFile: any) => {
  const raw = uploadFile?.raw
  if (!raw) return
  if (raw.size > 5 * 1024 * 1024) {
    ElMessage.warning('图片大小不能超过 5MB')
    return
  }
  const reader = new FileReader()
  reader.onload = (e) => {
    const dataUrl = (e.target?.result as string) || ''
    const comma = dataUrl.indexOf(',')
    form.photoBase64 = comma >= 0 ? dataUrl.slice(comma + 1) : dataUrl
  }
  reader.readAsDataURL(raw)
}

const handlePaTypeChange = () => {
  if (!form.paType) form.paId = ''
}

// ---- 角色明细 ----
const addRole = () => {
  roleRows.value.push({ roleId: '', roleNumber: '', roleName: '' })
}
const onRoleChange = (row: UserRoleRow, item: LookupItem | null) => {
  if (item && roleRows.value.filter(r => r.roleId === item.uid).length > 1) {
    ElMessage.warning('该角色已存在')
    row.roleId = ''
    row.roleNumber = ''
    row.roleName = ''
    return
  }
  row.roleNumber = item?.fNumber || ''
  row.roleName = item?.fName || ''
}

// ---- 组织明细 ----
const addOrg = () => {
  orgRows.value.push({ orgId: '', orgNumber: '', orgName: '' })
}
const onOrgChange = (row: UserOrgRow, item: LookupItem | null) => {
  if (item && orgRows.value.filter(r => r.orgId === item.uid).length > 1) {
    ElMessage.warning('该组织已存在')
    row.orgId = ''
    row.orgNumber = ''
    row.orgName = ''
    return
  }
  row.orgNumber = item?.fNumber || ''
  row.orgName = item?.fName || ''
}

async function loadGroupTree() {
  try {
    const res: any = await getGroupTree('User')
    groupTree.value = res.data || []
  } catch (e) {
    console.error('加载用户分组失败:', e)
  }
}

async function loadNavList() {
  // 后端分页每页上限 100（PagedRequest 会把 pageSize 夹为 100），逐页累积全部 uid 以支持首张/末张导航
  try {
    const uids: string[] = []
    const pageSize = 100
    let page = 1
    while (page <= 200) { // 安全上限，避免异常时死循环
      const res: any = await getUsers({ page, pageSize, keyword: '' })
      const items = res.data.items || []
      for (const u of items) if (u.uid && !uids.includes(u.uid)) uids.push(u.uid)
      const totalCount = res.data.totalCount || 0
      if (items.length === 0 || uids.length >= totalCount) break
      page++
    }
    allUids.value = uids
  } catch (e) {
    console.error('加载导航列表失败:', e)
  }
}

async function loadDetail(id: string) {
  loading.value = true
  try {
    const res: any = await getUser(id)
    const d = res.data
    Object.keys(defaultForm).forEach((k) => {
      if (k === 'password') return
      if (d[k] !== undefined && d[k] !== null) {
        ;(form as any)[k] = d[k]
      }
    })
    form.uid = d.uid
    form.photoBase64 = d.photoBase64 ?? null
    roleRows.value = (d.roleDetails || []).map((r: any) => ({ roleId: r.roleId, roleNumber: r.roleNumber, roleName: r.roleName }))
    orgRows.value = (d.organizations || []).map((o: any) => ({ orgId: o.orgId, orgNumber: o.orgNumber, orgName: o.orgName, isDefault: o.isDefault }))
  } catch (e) {
    console.error('加载用户详情失败:', e)
    ElMessage.error('加载用户详情失败')
  } finally {
    loading.value = false
  }
}

function buildPayload() {
  return {
    userName: form.userName,
    email: form.email,
    mobile: form.mobile,
    mfaEnabled: form.mfaEnabled,
    paType: form.paType,
    paId: form.paId,
    isPda: form.isPda,
    erpId: form.erpId,
    cardId: form.cardId,
    domainUser: form.domainUser,
    fGroupId: form.fGroupId,
    photoBase64: form.photoBase64,
  }
}

async function handleSave() {
  if (!formRef.value) return
  try {
    await formRef.value.validate()
  } catch {
    return
  }
  loading.value = true
  try {
    if (isEdit.value) {
      await updateUser(uid.value, buildPayload())
      // 角色/组织明细写入需 user:assign 权限；无此权限时仅保存表头，避免半提交 403
      if (canAssign.value) {
        await assignUserRoles(uid.value, roleRows.value.map(r => r.roleId).filter(Boolean))
        await assignUserOrgs(uid.value, orgRows.value.map(r => r.orgId).filter(Boolean))
      }
      ElMessage.success('保存成功')
      await loadDetail(uid.value)
    } else {
      const createData = {
        userId: form.userId,
        password: form.password,
        ...buildPayload(),
        roleIds: roleRows.value.map(r => r.roleId).filter(Boolean),
        orgIds: orgRows.value.map(r => r.orgId).filter(Boolean),
      }
      const res: any = await createUser(createData)
      ElMessage.success('创建成功')
      const newUid = res?.data?.uid
      if (newUid) {
        uid.value = newUid
        router.replace({ name: 'UserEdit', query: { uid: newUid } })
        await loadNavList()
        await loadDetail(newUid)
      } else {
        handleBack()
      }
    }
  } catch (error: any) {
    ElMessage.error(error?.response?.data?.message || '提交失败')
  } finally {
    loading.value = false
  }
}

const handleCopy = () => {
  // 复制为新增：清空主键/帐号/密码，保留其余字段与明细
  uid.value = ''
  form.uid = ''
  form.userId = ''
  form.password = ''
  form.fStatus = 0
  form.lockStatus = 0
  form.lastLoginTime = ''
  form.isDefault = false
  router.replace({ name: 'UserEdit', query: {} })
  ElMessage.info('已复制为新用户，请填写用户帐号与密码后保存')
}

const handleToggleStatus = async () => {
  if (!uid.value) return
  try {
    await toggleUserStatus(uid.value)
    ElMessage.success(form.fStatus === 0 ? '禁用成功' : '反禁用成功')
    await loadDetail(uid.value)
  } catch (e: any) {
    ElMessage.error(e?.response?.data?.message || '操作失败')
  }
}

async function navTo(targetUid: string) {
  if (!targetUid || targetUid === uid.value) return
  uid.value = targetUid
  router.replace({ name: 'UserEdit', query: { uid: targetUid } })
  await loadDetail(targetUid)
}
const handleFirst = () => navTo(allUids.value[0])
const handlePrev = () => { if (canPrev.value) navTo(allUids.value[currentIndex.value - 1]) }
const handleNext = () => { if (canNext.value) navTo(allUids.value[currentIndex.value + 1]) }
const handleLast = () => navTo(allUids.value[allUids.value.length - 1])

const handleBack = () => {
  router.push({ name: 'UserList' })
}

onMounted(async () => {
  await loadGroupTree()
  if (isEdit.value) {
    await loadNavList()
    await loadDetail(uid.value)
  } else if (route.query.groupId) {
    form.fGroupId = route.query.groupId as string
  }
})
</script>

<style scoped>
.user-edit-container {
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
  display: flex;
  flex-direction: column;
  height: 100%;
}

.edit-toolbar {
  display: flex;
  align-items: center;
  gap: 8px;
  padding: 12px 16px;
  border-bottom: 1px solid var(--border-color);
}

.toolbar-spacer {
  flex: 1;
}

.back-btn {
  margin-left: 8px;
}

.edit-form {
  padding: 16px 24px 24px;
  overflow-y: auto;
}

.form-header {
  padding-bottom: 8px;
  border-bottom: 1px dashed var(--border-color);
  margin-bottom: 8px;
}

.edit-tabs {
  margin-top: 8px;
}

.grid-toolbar {
  margin-bottom: 8px;
}

.mfa-hint {
  margin-left: 10px;
  font-size: 12px;
  color: var(--text-secondary);
}

.picture-uploader {
  display: flex;
  align-items: flex-start;
  gap: 8px;
}

.picture-preview {
  margin-left: 12px;
}

.picture-preview img {
  max-width: 160px;
  max-height: 160px;
  border: 1px solid var(--border-color);
  border-radius: 4px;
  object-fit: contain;
}
</style>
