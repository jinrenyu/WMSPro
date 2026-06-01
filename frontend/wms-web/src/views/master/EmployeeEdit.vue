<template>
  <div class="employee-edit-container">
    <!-- 顶部工具栏 -->
    <div class="edit-toolbar">
      <el-button type="primary" :disabled="isReadonly" @click="handleSave" v-permission="isEdit ? 'employee:edit' : 'employee:add'">
        <el-icon><Check /></el-icon> 保存
      </el-button>
      <el-button v-if="showApprove" type="success" @click="handleApprove" v-permission="'employee:approve'">审核</el-button>
      <el-button v-if="showUnapprove" type="warning" @click="handleUnapprove" v-permission="'employee:approve'">反审核</el-button>
      <el-button v-if="showDisable" type="info" @click="handleDisable" v-permission="'employee:disable'">禁用</el-button>
      <el-button v-if="showEnable" @click="handleEnable" v-permission="'employee:disable'">反禁用</el-button>
      <div class="toolbar-spacer" />
      <el-tag v-if="isEdit" :type="form.fStatus === 40 ? 'success' : 'warning'" size="large">
        {{ form.fStatus === 40 ? '已审核' : '未审核' }}
      </el-tag>
      <el-tag v-if="form.fDisabled" type="danger" size="large" style="margin-left: 8px;">已禁用</el-tag>
      <el-button class="back-btn" @click="handleBack">
        <el-icon><Back /></el-icon> 退出
      </el-button>
    </div>

    <el-form
      ref="formRef"
      :model="form"
      :rules="rules"
      label-width="92px"
      :disabled="isReadonly"
      class="edit-form"
      v-loading="loading"
    >
      <!-- 公共头部 -->
      <div class="form-header">
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="职员代码" prop="fNumber">
              <el-input v-model="form.fNumber" :disabled="isEdit" placeholder="编辑时不可修改" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="职员姓名" prop="fName">
              <el-input v-model="form.fName" placeholder="必填" />
            </el-form-item>
          </el-col>
        </el-row>
      </div>

      <el-tabs v-model="activeTab" class="edit-tabs">
        <!-- ============ 基本 ============ -->
        <el-tab-pane label="基本" name="basic">
          <el-row :gutter="16">
            <el-col :span="6">
              <el-form-item label="入职日期" prop="fEntryDate">
                <el-date-picker v-model="form.fEntryDate" type="date" value-format="YYYY-MM-DD" placeholder="入职日期" style="width: 100%" />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="性别">
                <el-select v-model="form.fSex" style="width: 100%">
                  <el-option :value="0" label="男" />
                  <el-option :value="1" label="女" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="电子邮箱"><el-input v-model="form.fMail" /></el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="联系地址"><el-input v-model="form.fAddress" /></el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="6">
              <el-form-item label="联系电话"><el-input v-model="form.fTel" /></el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="部门名称" prop="fSalDeptId">
                <LookupSelect v-model="form.fSalDeptId" module="department" placeholder="请选择部门" preload />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="成本中心">
                <el-select v-model="form.fCostCenterId" filterable clearable placeholder="请选择成本中心" style="width: 100%" @change="onCostCenterChange">
                  <el-option v-for="c in costCenters" :key="c.uid" :label="`${c.fNumber} - ${c.fName}`" :value="c.uid" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="学历"><el-input v-model="form.fEducation" /></el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="6">
              <el-form-item label="QQ"><el-input v-model="form.fQq" /></el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="微信"><el-input v-model="form.fWechat" /></el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="出生日期">
                <el-date-picker v-model="form.fBirthDate" type="date" value-format="YYYY-MM-DD" placeholder="出生日期" style="width: 100%" />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="调岗日期">
                <el-date-picker v-model="form.fTransferDate" type="date" value-format="YYYY-MM-DD" placeholder="调岗日期" style="width: 100%" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="6">
              <el-form-item label="离职"><el-checkbox v-model="form.fIsDeparture" /></el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="离职日期">
                <el-date-picker v-model="form.fDepartureDate" type="date" value-format="YYYY-MM-DD" placeholder="离职日期" style="width: 100%" />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="职员分组">
                <el-tree-select
                  v-model="form.fGroupId"
                  :data="groupTree"
                  :props="{ label: 'fName', children: 'children', value: 'uid' }"
                  placeholder="请选择分组"
                  clearable
                  check-strictly
                  style="width: 100%"
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="职务">
                <el-select v-model="form.fDutyId" filterable clearable placeholder="请选择职务" style="width: 100%" @change="onDutyChange">
                  <el-option v-for="d in duties" :key="d.uid" :label="`${d.fNumber} - ${d.fName}`" :value="d.uid" />
                </el-select>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="6">
              <el-form-item label="人员系数">
                <el-input-number v-model="form.fPersonnelCoefficient" :min="0" :precision="2" :controls="false" :value-on-clear="0" style="width: 100%" />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="职务等级">
                <el-input-number v-model="form.fDutyLevel" :min="0" :precision="0" :controls="false" :value-on-clear="0" style="width: 100%" />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="紧急联系人"><el-input v-model="form.emergency" /></el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="紧急联系电话"><el-input v-model="form.fEmergencyTel" /></el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="6">
              <el-form-item label="组织">
                <el-input :model-value="companyDisplayName" disabled placeholder="当前组织" />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="职员描述"><el-input v-model="form.fNote" /></el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="钉钉UserID"><el-input v-model="form.fDingTalkUserId" /></el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="24">
              <el-form-item label="图片">
                <div class="picture-uploader">
                  <el-upload :auto-upload="false" :show-file-list="false" accept="image/*" :disabled="isReadonly" @change="handlePictureChange">
                    <el-button :disabled="isReadonly"><el-icon><Picture /></el-icon> 选择图片</el-button>
                  </el-upload>
                  <el-button v-if="form.fPictureBase64" link type="danger" :disabled="isReadonly" @click="form.fPictureBase64 = null">移除</el-button>
                  <div v-if="pictureSrc" class="picture-preview"><img :src="pictureSrc" alt="职员图片" /></div>
                </div>
              </el-form-item>
            </el-col>
          </el-row>
        </el-tab-pane>

        <!-- ============ 其他（只读系统信息） ============ -->
        <el-tab-pane label="其他" name="other">
          <el-row :gutter="20">
            <el-col :span="12"><el-form-item label="制单人"><el-input :model-value="form.cUser" disabled /></el-form-item></el-col>
            <el-col :span="12"><el-form-item label="制单日期"><el-input :model-value="fmtAuditDate(form.cYmd)" disabled /></el-form-item></el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12"><el-form-item label="修改人"><el-input :model-value="form.mUser" disabled /></el-form-item></el-col>
            <el-col :span="12"><el-form-item label="修改日期"><el-input :model-value="fmtAuditDate(form.mYmd)" disabled /></el-form-item></el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12"><el-form-item label="审核人"><el-input :model-value="form.fCheckerId" disabled /></el-form-item></el-col>
            <el-col :span="12"><el-form-item label="审核日期"><el-input :model-value="fmtAuditDate(form.fCheckDate)" disabled /></el-form-item></el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12"><el-form-item label="禁用人"><el-input :model-value="form.fdisableid" disabled /></el-form-item></el-col>
            <el-col :span="12"><el-form-item label="禁用日期"><el-input :model-value="fmtAuditDate(form.fdisabledate)" disabled /></el-form-item></el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12"><el-form-item label="数据状态"><el-input :model-value="form.fStatus === 40 ? '已审核' : '未审核'" disabled /></el-form-item></el-col>
            <el-col :span="12"><el-form-item label="禁用"><el-checkbox :model-value="form.fDisabled" disabled /></el-form-item></el-col>
          </el-row>
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
import { Check, Back, Picture } from '@element-plus/icons-vue'
import {
  getEmployee, createEmployee, updateEmployee,
  approveEmployee, unapproveEmployee, disableEmployee, enableEmployee,
  getCostCenters, getDuties, type EmployeeLookupOption,
} from '../../api/employee'
import { getGroupTree, type BaseDataGroup } from '../../api/baseDataGroup'
import { formatDate } from '../../utils/format'
import { useOrgStore } from '../../stores/org'
import LookupSelect from '../../components/LookupSelect.vue'

const router = useRouter()
const route = useRoute()
const orgStore = useOrgStore()

const formRef = ref<FormInstance>()
const loading = ref(false)
const activeTab = ref('basic')
const groupTree = ref<BaseDataGroup[]>([])
const costCenters = ref<EmployeeLookupOption[]>([])
const duties = ref<EmployeeLookupOption[]>([])

const uid = ref<string>((route.query.uid as string) || '')
const isEdit = computed(() => !!uid.value)
const isReadonly = computed(() => isEdit.value && (form.fStatus === 40 || form.fDisabled))

const companyDisplayName = computed(() => {
  if (isEdit.value && form.fCompanyName) return form.fCompanyName
  if (!isEdit.value) {
    const cur = orgStore.currentOrg
    if (cur) return cur.parentOrgName || cur.orgName
  }
  const org = orgStore.orgs.find(o => o.orgId === form.fCompanyId)
  if (org) return org.parentOrgName || org.orgName
  return form.fCompanyName || form.fCompanyId
})

const rules: FormRules = {
  fNumber: [{ required: true, message: '请输入职员代码', trigger: 'blur' }],
  fName: [{ required: true, message: '请输入职员姓名', trigger: 'blur' }],
  fEntryDate: [{ required: true, message: '请选择入职日期', trigger: 'change' }],
  fSalDeptId: [{ required: true, message: '请选择部门', trigger: 'change' }],
}

const defaultForm = {
  uid: '' as string,
  fStatus: 0,
  fDisabled: false,
  fNumber: '',
  fName: '',
  fCompanyId: '',
  fCompanyName: '',
  // 基本
  fEntryDate: '',
  fSex: 0,
  fMail: '',
  fAddress: '',
  fTel: '',
  fSalDeptId: '',
  fCostCenterId: '',
  fCostCenterName: '',
  fEducation: '',
  fQq: '',
  fWechat: '',
  fBirthDate: '',
  fTransferDate: '',
  fIsDeparture: false,
  fDepartureDate: '',
  fGroupId: '',
  fDutyId: '',
  fDutyName: '',
  fPersonnelCoefficient: 0,
  fDutyLevel: 0,
  emergency: '',
  fEmergencyTel: '',
  fNote: '',
  fDingTalkUserId: '',
  fPictureBase64: null as string | null,
  // 只读系统信息
  cUser: '',
  cYmd: '',
  mUser: '',
  mYmd: '',
  fCheckerId: '',
  fCheckDate: '',
  fdisableid: '',
  fdisabledate: '',
}

const form = reactive({ ...defaultForm })

const showApprove = computed(() => isEdit.value && form.fStatus !== 40 && !form.fDisabled)
const showUnapprove = computed(() => isEdit.value && form.fStatus === 40)
const showDisable = computed(() => isEdit.value && !form.fDisabled)
const showEnable = computed(() => isEdit.value && form.fDisabled)

const fmtAuditDate = (d?: string) => {
  if (!d) return ''
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() < 1900) return ''
  return formatDate(d)
}
// 业务日期：MinValue/空 → ''，否则取 yyyy-MM-dd
const normDate = (d?: string) => {
  if (!d) return ''
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() < 1900) return ''
  return d.slice(0, 10)
}

// ---- 图片 ----
const pictureSrc = computed(() => {
  const b64 = form.fPictureBase64
  if (!b64) return ''
  let mime = 'image/png'
  if (b64.startsWith('/9j/')) mime = 'image/jpeg'
  else if (b64.startsWith('iVBOR')) mime = 'image/png'
  else if (b64.startsWith('R0lGOD')) mime = 'image/gif'
  else if (b64.startsWith('UklGR')) mime = 'image/webp'
  return `data:${mime};base64,${b64}`
})
const handlePictureChange = (uploadFile: any) => {
  const raw = uploadFile?.raw
  if (!raw) return
  if (raw.size > 5 * 1024 * 1024) { ElMessage.warning('图片大小不能超过 5MB'); return }
  const reader = new FileReader()
  reader.onload = (e) => {
    const dataUrl = (e.target?.result as string) || ''
    const comma = dataUrl.indexOf(',')
    form.fPictureBase64 = comma >= 0 ? dataUrl.slice(comma + 1) : dataUrl
  }
  reader.readAsDataURL(raw)
}

const onCostCenterChange = (val: string) => {
  const c = costCenters.value.find(x => x.uid === val)
  form.fCostCenterName = c?.fName || ''
}
const onDutyChange = (val: string) => {
  const d = duties.value.find(x => x.uid === val)
  form.fDutyName = d?.fName || ''
}

async function loadCostCenters() {
  try { const res: any = await getCostCenters(); costCenters.value = res.data || [] } catch (e) { console.error('加载成本中心失败:', e) }
}
async function loadDuties() {
  try { const res: any = await getDuties(); duties.value = res.data || [] } catch (e) { console.error('加载职务失败:', e) }
}
async function loadGroupTree() {
  try { const res: any = await getGroupTree('Employee'); groupTree.value = res.data || [] } catch (e) { console.error('加载职员分组失败:', e) }
}

async function loadDetail(id: string) {
  loading.value = true
  try {
    const res: any = await getEmployee(id)
    const d = res.data
    Object.keys(defaultForm).forEach((k) => {
      if (d[k] !== undefined && d[k] !== null) { ;(form as any)[k] = d[k] }
    })
    form.uid = d.uid
    // 业务日期归一（MinValue → ''）
    form.fEntryDate = normDate(d.fEntryDate)
    form.fBirthDate = normDate(d.fBirthDate)
    form.fDepartureDate = normDate(d.fDepartureDate)
    form.fTransferDate = normDate(d.fTransferDate)
    // 回显兜底：所选成本中心/职务若已被禁用/删除/反审核而不在下拉列表，合并进选项以正常显示
    if (form.fCostCenterId && !costCenters.value.some(c => c.uid === form.fCostCenterId)) {
      costCenters.value = [{ uid: form.fCostCenterId, fNumber: d.fCostCenterNumber || '', fName: d.fCostCenterName || '' }, ...costCenters.value]
    }
    if (form.fDutyId && !duties.value.some(x => x.uid === form.fDutyId)) {
      duties.value = [{ uid: form.fDutyId, fNumber: d.fDutyNumber || '', fName: d.fDutyName || '' }, ...duties.value]
    }
  } catch (e) {
    console.error('加载职员详情失败:', e)
    ElMessage.error('加载职员详情失败')
  } finally {
    loading.value = false
  }
}

function buildPayload() {
  return {
    fNumber: form.fNumber,
    fName: form.fName,
    fCompanyId: form.fCompanyId,
    fSex: form.fSex,
    fEducation: form.fEducation,
    fBirthDate: form.fBirthDate || null,
    fEntryDate: form.fEntryDate || null,
    fTransferDate: form.fTransferDate || null,
    fDepartureDate: form.fDepartureDate || null,
    fIsDeparture: form.fIsDeparture,
    fTel: form.fTel,
    fSalDeptId: form.fSalDeptId,
    fCostCenterId: form.fCostCenterId,
    fDutyId: form.fDutyId,
    fDutyLevel: form.fDutyLevel,
    fPersonnelCoefficient: form.fPersonnelCoefficient,
    fMail: form.fMail,
    fNote: form.fNote,
    fAddress: form.fAddress,
    fQq: form.fQq,
    fWechat: form.fWechat,
    fEmergencyTel: form.fEmergencyTel,
    emergency: form.emergency,
    fDingTalkUserId: form.fDingTalkUserId,
    fPictureBase64: form.fPictureBase64 || '',
    fGroupId: form.fGroupId,
  }
}

async function handleSave() {
  if (!formRef.value) return
  try { await formRef.value.validate() } catch { activeTab.value = 'basic'; return }
  const payload = buildPayload()
  loading.value = true
  try {
    if (isEdit.value) {
      const { fNumber, fCompanyId, ...updateData } = payload
      await updateEmployee(uid.value, updateData)
      ElMessage.success('保存成功')
      await loadDetail(uid.value)
    } else {
      const res: any = await createEmployee(payload)
      ElMessage.success('创建成功')
      const newUid = res?.data?.uid
      if (newUid) {
        uid.value = newUid
        router.replace({ name: 'EmployeeEdit', query: { uid: newUid } })
        await loadDetail(newUid)
      } else { handleBack() }
    }
  } catch (error: any) {
    ElMessage.error(error?.response?.data?.message || '提交失败')
  } finally {
    loading.value = false
  }
}

async function runStatusAction(action: (id: string) => Promise<any>, msg: string) {
  if (!uid.value) return
  try { await action(uid.value); ElMessage.success(msg); await loadDetail(uid.value) }
  catch (error: any) { ElMessage.error(error?.response?.data?.message || '操作失败') }
}
const handleApprove = () => runStatusAction(approveEmployee, '审核成功')
const handleUnapprove = () => runStatusAction(unapproveEmployee, '反审核成功')
const handleDisable = () => runStatusAction(disableEmployee, '禁用成功')
const handleEnable = () => runStatusAction(enableEmployee, '反禁用成功')
const handleBack = () => router.push({ name: 'EmployeeList' })

onMounted(async () => {
  if (!orgStore.loaded) orgStore.loadOrgs()
  await Promise.all([loadCostCenters(), loadDuties(), loadGroupTree()])
  if (isEdit.value) {
    await loadDetail(uid.value)
  } else {
    form.fCompanyId = orgStore.currentOrgId
    if (route.query.groupId) form.fGroupId = route.query.groupId as string
  }
})
</script>

<style scoped>
.employee-edit-container {
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
  display: flex;
  flex-direction: column;
  height: 100%;
}
.edit-toolbar { display: flex; align-items: center; gap: 8px; padding: 12px 16px; border-bottom: 1px solid var(--border-color); }
.toolbar-spacer { flex: 1; }
.back-btn { margin-left: 8px; }
.edit-form { padding: 16px 24px 24px; overflow-y: auto; }
.form-header { padding-bottom: 8px; border-bottom: 1px dashed var(--border-color); margin-bottom: 8px; }
.edit-tabs { margin-top: 8px; }
.edit-form :deep(.el-form-item__label) { white-space: nowrap; }
.picture-uploader { display: flex; align-items: center; gap: 12px; }
.picture-preview { width: 120px; height: 120px; border: 1px solid var(--border-color); border-radius: 4px; overflow: hidden; }
.picture-preview img { width: 100%; height: 100%; object-fit: cover; }
</style>
