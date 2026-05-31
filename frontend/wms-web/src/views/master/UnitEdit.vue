<template>
  <div class="unit-edit-container">
    <!-- 顶部工具栏 -->
    <div class="edit-toolbar">
      <el-button type="primary" :disabled="isReadonly" @click="handleSave" v-permission="isEdit ? 'unit:edit' : 'unit:add'">
        <el-icon><Check /></el-icon> 保存
      </el-button>
      <el-button v-if="showApprove" type="success" @click="handleApprove" v-permission="'unit:approve'">审核</el-button>
      <el-button v-if="showUnapprove" type="warning" @click="handleUnapprove" v-permission="'unit:approve'">反审核</el-button>
      <el-button v-if="showDisable" type="info" @click="handleDisable" v-permission="'unit:disable'">禁用</el-button>
      <el-button v-if="showEnable" @click="handleEnable" v-permission="'unit:disable'">反禁用</el-button>
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
      label-width="100px"
      :disabled="isReadonly"
      class="edit-form"
      v-loading="loading"
    >
      <!-- 公共头部 -->
      <div class="form-header">
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="单位代码" prop="fNumber">
              <el-input v-model="form.fNumber" :disabled="isEdit" placeholder="编辑时不可修改" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="单位名称" prop="fName">
              <el-input v-model="form.fName" placeholder="必填" />
            </el-form-item>
          </el-col>
        </el-row>
      </div>

      <!-- Tab 区 -->
      <el-tabs v-model="activeTab" class="edit-tabs">
        <!-- ============ 基本 ============ -->
        <el-tab-pane label="基本" name="basic">
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="组织">
                <el-input :model-value="companyDisplayName" disabled placeholder="当前组织" />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="单位描述">
                <el-input v-model="form.fDescription" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="分组">
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
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="所属分组" prop="fUnitGroupId">
                <el-select v-model="form.fUnitGroupId" filterable clearable placeholder="请选择所属分组" style="width: 100%" @change="onUnitGroupChange">
                  <el-option v-for="g in unitGroups" :key="g.uid" :label="`${g.fNumber} - ${g.fName}`" :value="g.uid" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="基准计量单位">
                <el-checkbox v-model="form.fIsBaseUnit" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="舍入类型">
                <el-select v-model="form.fRoundType" placeholder="请选择舍入类型" style="width: 100%">
                  <el-option v-for="o in roundTypeOptions" :key="o.value" :label="o.label" :value="o.value" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="精度">
                <el-input-number v-model="form.fPrecision" :min="0" :max="10" :precision="0" :controls="false" style="width: 100%" />
              </el-form-item>
            </el-col>
          </el-row>

          <!-- 换算关系：换算分母 当前单位 = 换算分子 基准单位 -->
          <div class="section-title">换算关系</div>
          <el-row :gutter="12" align="middle">
            <el-col :span="6">
              <el-form-item label="换算分母" prop="fConvertDenominator">
                <el-input-number v-model="form.fConvertDenominator" :min="0" :precision="6" :controls="false" style="width: 100%" />
              </el-form-item>
            </el-col>
            <el-col :span="5">
              <el-form-item label="当前单位">
                <el-input :model-value="form.fName" disabled placeholder="当前单位" />
              </el-form-item>
            </el-col>
            <el-col :span="1" class="eq-col">=</el-col>
            <el-col :span="6">
              <el-form-item label="换算分子" prop="fConvertNumerator">
                <el-input-number v-model="form.fConvertNumerator" :min="0" :precision="6" :controls="false" style="width: 100%" />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="基准单位">
                <el-input :model-value="form.fBaseUnitName" disabled placeholder="基准单位" />
              </el-form-item>
            </el-col>
          </el-row>
        </el-tab-pane>

        <!-- ============ 其他（只读系统信息） ============ -->
        <el-tab-pane label="其他" name="other">
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="制单人"><el-input :model-value="form.cUser" disabled /></el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="制单日期"><el-input :model-value="fmtAuditDate(form.cYmd)" disabled /></el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="审核人"><el-input :model-value="form.fCheckerId" disabled /></el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="审核日期"><el-input :model-value="fmtAuditDate(form.fCheckDate)" disabled /></el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="修改人"><el-input :model-value="form.mUser" disabled /></el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="修改日期"><el-input :model-value="fmtAuditDate(form.mYmd)" disabled /></el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="禁用人"><el-input :model-value="form.fdisableid" disabled /></el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="禁用日期"><el-input :model-value="fmtAuditDate(form.fdisabledate)" disabled /></el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="数据状态"><el-input :model-value="form.fStatus === 40 ? '已审核' : '未审核'" disabled /></el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="禁用"><el-checkbox :model-value="form.fDisabled" disabled /></el-form-item>
            </el-col>
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
import { Check, Back } from '@element-plus/icons-vue'
import {
  getUnit, createUnit, updateUnit,
  approveUnit, unapproveUnit, disableUnit, enableUnit,
  getUnitGroups, type UnitGroupOption,
} from '../../api/unit'
import { getGroupTree, type BaseDataGroup } from '../../api/baseDataGroup'
import { formatDate } from '../../utils/format'
import { useOrgStore } from '../../stores/org'

const router = useRouter()
const route = useRoute()
const orgStore = useOrgStore()

const formRef = ref<FormInstance>()
const loading = ref(false)
const activeTab = ref('basic')
const unitGroups = ref<UnitGroupOption[]>([])
const groupTree = ref<BaseDataGroup[]>([])

const uid = ref<string>((route.query.uid as string) || '')
const isEdit = computed(() => !!uid.value)
const isReadonly = computed(() => isEdit.value && (form.fStatus === 40 || form.fDisabled))

// 舍入类型：1=四舍五入;2=进位;3=舍位
const roundTypeOptions = [
  { value: '1', label: '四舍五入' },
  { value: '2', label: '进位' },
  { value: '3', label: '舍位' },
]
const roundTypeLabelToCode: Record<string, string> = { '四舍五入': '1', '进位': '2', '舍位': '3' }

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
  fNumber: [{ required: true, message: '请输入单位代码', trigger: 'blur' }],
  fName: [{ required: true, message: '请输入单位名称', trigger: 'blur' }],
  fUnitGroupId: [{ required: true, message: '请选择所属分组', trigger: 'change' }],
  fConvertDenominator: [{
    validator: (_r: any, v: any, cb: any) => (v == null || v <= 0) ? cb(new Error('换算分母必须大于0')) : cb(),
    trigger: 'blur',
  }],
  fConvertNumerator: [{
    validator: (_r: any, v: any, cb: any) => (v == null || v <= 0) ? cb(new Error('换算分子必须大于0')) : cb(),
    trigger: 'blur',
  }],
}

const defaultForm = {
  uid: '' as string,
  fStatus: 0,
  fDisabled: false,
  // 头部
  fNumber: '',
  fName: '',
  fCompanyId: '',
  fCompanyName: '',
  // 基本
  fDescription: '',
  fUnitGroupId: '',
  fBaseUnitNumber: '',
  fBaseUnitName: '',
  fIsBaseUnit: false,
  fRoundType: '1',
  fPrecision: 0,
  fConvertType: '',
  fConvertNumerator: 1,
  fConvertDenominator: 1,
  fGroupId: '',
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

const onUnitGroupChange = (val: string) => {
  const g = unitGroups.value.find(x => x.uid === val)
  form.fBaseUnitNumber = g?.fBaseUnitNumber || ''
  form.fBaseUnitName = g?.fBaseUnitName || ''
}

async function loadUnitGroups() {
  try {
    const res: any = await getUnitGroups()
    unitGroups.value = res.data || []
  } catch (e) {
    console.error('加载单位组失败:', e)
  }
}

async function loadGroupTree() {
  try {
    const res: any = await getGroupTree('Unit')
    groupTree.value = res.data || []
  } catch (e) {
    console.error('加载单位分组失败:', e)
  }
}

async function loadDetail(id: string) {
  loading.value = true
  try {
    const res: any = await getUnit(id)
    const d = res.data
    Object.keys(defaultForm).forEach((k) => {
      if (d[k] !== undefined && d[k] !== null) {
        ;(form as any)[k] = d[k]
      }
    })
    form.uid = d.uid
    // 历史数据 fRoundType 存中文标签时映射为码；不在 1/2/3 内则回落为四舍五入
    if (roundTypeLabelToCode[form.fRoundType]) form.fRoundType = roundTypeLabelToCode[form.fRoundType]
    else if (!['1', '2', '3'].includes(form.fRoundType)) form.fRoundType = '1'
  } catch (e) {
    console.error('加载单位详情失败:', e)
    ElMessage.error('加载单位详情失败')
  } finally {
    loading.value = false
  }
}

function buildPayload() {
  return {
    fNumber: form.fNumber,
    fName: form.fName,
    fCompanyId: form.fCompanyId,
    fDescription: form.fDescription,
    fUnitGroupId: form.fUnitGroupId,
    fIsBaseUnit: form.fIsBaseUnit,
    fPrecision: form.fPrecision,
    fRoundType: form.fRoundType,
    fConvertType: form.fConvertType,
    fConvertNumerator: form.fConvertNumerator,
    fConvertDenominator: form.fConvertDenominator,
    fGroupId: form.fGroupId,
  }
}

async function handleSave() {
  if (!formRef.value) return
  try {
    await formRef.value.validate()
  } catch {
    activeTab.value = 'basic'
    return
  }
  const payload = buildPayload()
  loading.value = true
  try {
    if (isEdit.value) {
      const { fNumber, fCompanyId, ...updateData } = payload
      await updateUnit(uid.value, updateData)
      ElMessage.success('保存成功')
      await loadDetail(uid.value)
    } else {
      const res: any = await createUnit(payload)
      ElMessage.success('创建成功')
      const newUid = res?.data?.uid
      if (newUid) {
        uid.value = newUid
        router.replace({ name: 'UnitEdit', query: { uid: newUid } })
        await loadDetail(newUid)
      } else {
        handleBack()
      }
    }
  } catch (error: any) {
    const msg = error?.response?.data?.message || '提交失败'
    ElMessage.error(msg)
  } finally {
    loading.value = false
  }
}

async function runStatusAction(action: (id: string) => Promise<any>, successMsg: string) {
  if (!uid.value) return
  try {
    await action(uid.value)
    ElMessage.success(successMsg)
    await loadDetail(uid.value)
  } catch (error: any) {
    const msg = error?.response?.data?.message || '操作失败'
    ElMessage.error(msg)
  }
}

const handleApprove = () => runStatusAction(approveUnit, '审核成功')
const handleUnapprove = () => runStatusAction(unapproveUnit, '反审核成功')
const handleDisable = () => runStatusAction(disableUnit, '禁用成功')
const handleEnable = () => runStatusAction(enableUnit, '反禁用成功')

const handleBack = () => {
  router.push({ name: 'UnitList' })
}

onMounted(async () => {
  if (!orgStore.loaded) orgStore.loadOrgs()
  await Promise.all([loadUnitGroups(), loadGroupTree()])
  if (isEdit.value) {
    await loadDetail(uid.value)
  } else {
    form.fCompanyId = orgStore.currentOrgId
    if (route.query.groupId) form.fGroupId = route.query.groupId as string
  }
})
</script>

<style scoped>
.unit-edit-container {
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

.toolbar-spacer { flex: 1; }
.back-btn { margin-left: 8px; }

.edit-form {
  padding: 16px 24px 24px;
  overflow-y: auto;
}

.form-header {
  padding-bottom: 8px;
  border-bottom: 1px dashed var(--border-color);
  margin-bottom: 8px;
}

.edit-tabs { margin-top: 8px; }

.section-title {
  font-weight: 600;
  margin: 8px 0 12px;
  color: var(--el-text-color-primary);
}

.eq-col {
  text-align: center;
  font-weight: 600;
  padding-bottom: 18px;
}

.edit-form :deep(.el-form-item__label) { white-space: nowrap; }
</style>
