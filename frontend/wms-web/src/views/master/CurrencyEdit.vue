<template>
  <div class="currency-edit-container">
    <!-- 顶部工具栏 -->
    <div class="edit-toolbar">
      <el-button type="primary" :disabled="isReadonly" @click="handleSave" v-permission="isEdit ? 'currency:edit' : 'currency:add'">
        <el-icon><Check /></el-icon> 保存
      </el-button>
      <el-button v-if="showApprove" type="success" @click="handleApprove" v-permission="'currency:approve'">审核</el-button>
      <el-button v-if="showUnapprove" type="warning" @click="handleUnapprove" v-permission="'currency:approve'">反审核</el-button>
      <el-button v-if="showDisable" type="info" @click="handleDisable" v-permission="'currency:disable'">禁用</el-button>
      <el-button v-if="showEnable" @click="handleEnable" v-permission="'currency:disable'">反禁用</el-button>
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
      label-width="90px"
      :disabled="isReadonly"
      class="edit-form"
      v-loading="loading"
    >
      <!-- 公共头部 -->
      <div class="form-header">
        <el-row :gutter="20">
          <el-col :span="6">
            <el-form-item label="币别代码" prop="fNumber">
              <el-input v-model="form.fNumber" :disabled="isEdit" placeholder="编辑时不可修改" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="币别名称" prop="fName">
              <el-input v-model="form.fName" placeholder="必填" />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="组织">
              <el-input :model-value="companyDisplayName" disabled placeholder="当前组织" />
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
              <el-form-item label="货币代码">
                <el-input v-model="form.fCode" />
              </el-form-item>
            </el-col>
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
              <el-form-item label="单价精度">
                <el-input-number v-model="form.fPriceDigits" :min="0" :max="10" :precision="0" :controls="false" style="width: 100%" />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="记账汇率" prop="fExchangeRate">
                <el-input-number v-model="form.fExchangeRate" :min="0" :precision="6" :controls="false" :value-on-clear="1" style="width: 100%" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="折算方式" prop="fFixRate">
                <el-select v-model="form.fFixRate" style="width: 100%">
                  <el-option v-for="o in fixRateOptions" :key="o.value" :label="o.label" :value="o.value" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="金额精度">
                <el-input-number v-model="form.fAmountDigits" :min="0" :max="10" :precision="0" :controls="false" style="width: 100%" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="24">
              <el-form-item label="币别描述">
                <el-input v-model="form.fDescription" />
              </el-form-item>
            </el-col>
          </el-row>
        </el-tab-pane>

        <!-- ============ 其他（只读系统信息） ============ -->
        <el-tab-pane label="其他" name="other">
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="制单人">
                <el-input :model-value="form.cUser" disabled />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="制单日期">
                <el-input :model-value="fmtAuditDate(form.cYmd)" disabled />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="修改人">
                <el-input :model-value="form.mUser" disabled />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="修改日期">
                <el-input :model-value="fmtAuditDate(form.mYmd)" disabled />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="审核人">
                <el-input :model-value="form.fCheckerId" disabled />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="审核日期">
                <el-input :model-value="fmtAuditDate(form.fCheckDate)" disabled />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="禁用人">
                <el-input :model-value="form.fdisableid" disabled />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="禁用日期">
                <el-input :model-value="fmtAuditDate(form.fdisabledate)" disabled />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="状态">
                <el-input :model-value="form.fStatus === 40 ? '已审核' : '未审核'" disabled />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="禁用">
                <el-checkbox :model-value="form.fDisabled" disabled />
              </el-form-item>
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
  getCurrency, createCurrency, updateCurrency,
  approveCurrency, unapproveCurrency, disableCurrency, enableCurrency,
} from '../../api/currency'
import { getGroupTree, type BaseDataGroup } from '../../api/baseDataGroup'
import { formatDate } from '../../utils/format'
import { useOrgStore } from '../../stores/org'

const router = useRouter()
const route = useRoute()
const orgStore = useOrgStore()

const formRef = ref<FormInstance>()
const loading = ref(false)
const activeTab = ref('basic')
const groupTree = ref<BaseDataGroup[]>([])

const uid = ref<string>((route.query.uid as string) || '')
const isEdit = computed(() => !!uid.value)
const isReadonly = computed(() => isEdit.value && (form.fStatus === 40 || form.fDisabled))

// 折算方式：1=原币×汇率＝本位币；2=原币÷汇率＝本位币
const fixRateOptions = [
  { label: '原币×汇率＝本位币', value: 1 },
  { label: '原币÷汇率＝本位币', value: 2 },
]

// 组织显示名：FCompanyId 记录所选组织的上级(公司)，故显示上级组织名。
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
  fNumber: [{ required: true, message: '请输入币别代码', trigger: 'blur' }],
  fName: [{ required: true, message: '请输入币别名称', trigger: 'blur' }],
  fFixRate: [{ required: true, message: '请选择折算方式', trigger: 'change' }],
  fExchangeRate: [{
    validator: (_rule: any, value: any, cb: any) =>
      (value == null || value <= 0) ? cb(new Error('记账汇率必须大于0')) : cb(),
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
  fCode: '',
  fGroupId: '',
  fPriceDigits: 0,
  fExchangeRate: 1,
  fFixRate: 1,
  fAmountDigits: 0,
  fDescription: '',
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

async function loadGroupTree() {
  try {
    const res: any = await getGroupTree('Currency')
    groupTree.value = res.data || []
  } catch (e) {
    console.error('加载币种分组失败:', e)
  }
}

async function loadDetail(id: string) {
  loading.value = true
  try {
    const res: any = await getCurrency(id)
    const d = res.data
    Object.keys(defaultForm).forEach((k) => {
      if (d[k] !== undefined && d[k] !== null) {
        ;(form as any)[k] = d[k]
      }
    })
    form.uid = d.uid
    // 历史/异常数据 fFixRate 不在 1/2 内时回落为默认 1，避免下拉空白且 required 误判通过
    if (form.fFixRate !== 1 && form.fFixRate !== 2) form.fFixRate = 1
  } catch (e) {
    console.error('加载币别详情失败:', e)
    ElMessage.error('加载币别详情失败')
  } finally {
    loading.value = false
  }
}

function buildPayload() {
  return {
    fNumber: form.fNumber,
    fName: form.fName,
    fCompanyId: form.fCompanyId,
    fCode: form.fCode,
    fDescription: form.fDescription,
    fPriceDigits: form.fPriceDigits,
    fAmountDigits: form.fAmountDigits,
    fFixRate: form.fFixRate,
    fExchangeRate: form.fExchangeRate,
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
      await updateCurrency(uid.value, updateData)
      ElMessage.success('保存成功')
      await loadDetail(uid.value)
    } else {
      const res: any = await createCurrency(payload)
      ElMessage.success('创建成功')
      const newUid = res?.data?.uid
      if (newUid) {
        uid.value = newUid
        router.replace({ name: 'CurrencyEdit', query: { uid: newUid } })
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

const handleApprove = () => runStatusAction(approveCurrency, '审核成功')
const handleUnapprove = () => runStatusAction(unapproveCurrency, '反审核成功')
const handleDisable = () => runStatusAction(disableCurrency, '禁用成功')
const handleEnable = () => runStatusAction(enableCurrency, '反禁用成功')

const handleBack = () => {
  router.push({ name: 'CurrencyList' })
}

onMounted(async () => {
  if (!orgStore.loaded) orgStore.loadOrgs()
  await loadGroupTree()
  if (isEdit.value) {
    await loadDetail(uid.value)
  } else {
    // 新增：默认使用当前切换的组织
    form.fCompanyId = orgStore.currentOrgId
    if (route.query.groupId) form.fGroupId = route.query.groupId as string
  }
})
</script>

<style scoped>
.currency-edit-container {
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

/* 标签保持单行 */
.edit-form :deep(.el-form-item__label) {
  white-space: nowrap;
}
</style>
