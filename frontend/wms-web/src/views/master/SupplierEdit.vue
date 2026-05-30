<template>
  <div class="supplier-edit-container">
    <!-- 顶部工具栏 -->
    <div class="edit-toolbar">
      <el-button type="primary" :disabled="isReadonly" @click="handleSave" v-permission="isEdit ? 'supplier:edit' : 'supplier:add'">
        <el-icon><Check /></el-icon> 保存
      </el-button>
      <el-button v-if="showApprove" type="success" @click="handleApprove" v-permission="'supplier:approve'">审核</el-button>
      <el-button v-if="showUnapprove" type="warning" @click="handleUnapprove" v-permission="'supplier:approve'">反审核</el-button>
      <el-button v-if="showDisable" type="info" @click="handleDisable" v-permission="'supplier:disable'">禁用</el-button>
      <el-button v-if="showEnable" @click="handleEnable" v-permission="'supplier:disable'">反禁用</el-button>
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
          <el-col :span="8">
            <el-form-item label="供应商代码" prop="fNumber">
              <el-input v-model="form.fNumber" :disabled="isEdit" placeholder="编辑时不可修改" />
            </el-form-item>
          </el-col>
          <el-col :span="16">
            <el-form-item label="供应商名称" prop="fName">
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
            <el-col :span="8">
              <el-form-item label="供应商简称">
                <el-input v-model="form.fShortName" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="供应商分组">
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
            <el-col :span="8">
              <el-form-item label="组织">
                <el-input :model-value="companyDisplayName" disabled placeholder="当前组织" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="8">
              <el-form-item label="税率">
                <el-input-number v-model="form.fTaxRate" :min="0" :max="100" :precision="2" :controls="false" placeholder="0" style="width: 100%" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="国家">
                <el-input v-model="form.fCountry" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="地区">
                <el-input v-model="form.fProvincial" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="8">
              <el-form-item label="通讯地址">
                <el-input v-model="form.fAddress" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="联系人">
                <el-input v-model="form.fContact" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="联系电话">
                <el-input v-model="form.fPhone" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="8">
              <el-form-item label="传真">
                <el-input v-model="form.fFax" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="电子邮件">
                <el-input v-model="form.fEmail" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="银行">
                <el-input v-model="form.fBank" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="8">
              <el-form-item label="账户">
                <el-input v-model="form.fAccount" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="业务员">
                <LookupSelect v-model="form.fEmpId" module="employee" placeholder="请选择业务员" preload />
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
              <el-form-item label="数据状态">
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
  getSupplier, createSupplier, updateSupplier,
  approveSupplier, unapproveSupplier, disableSupplier, enableSupplier,
} from '../../api/supplier'
import { getGroupTree, type BaseDataGroup } from '../../api/baseDataGroup'
import LookupSelect from '../../components/LookupSelect.vue'
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

// 组织显示名：FCompanyId 记录所选组织的上级(公司)，故显示上级组织名。
// 编辑态用后端解析的 FCompanyName；新增态用当前所选组织的上级名。
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
  fNumber: [{ required: true, message: '请输入供应商代码', trigger: 'blur' }],
  fName: [{ required: true, message: '请输入供应商名称', trigger: 'blur' }],
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
  fShortName: '',
  fGroupId: '',
  // 税率初值留空：避免把数据库原为 NULL 的税率在未修改时静默改写为 0
  fTaxRate: undefined as number | undefined,
  fCountry: '',
  fProvincial: '',
  fAddress: '',
  fContact: '',
  fPhone: '',
  fFax: '',
  fEmail: '',
  fBank: '',
  fAccount: '',
  fEmpId: '',
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
    const res: any = await getGroupTree('Supplier')
    groupTree.value = res.data || []
  } catch (e) {
    console.error('加载供应商分组失败:', e)
  }
}

async function loadDetail(id: string) {
  loading.value = true
  try {
    const res: any = await getSupplier(id)
    const d = res.data
    Object.keys(defaultForm).forEach((k) => {
      if (d[k] !== undefined && d[k] !== null) {
        ;(form as any)[k] = d[k]
      }
    })
    form.uid = d.uid
  } catch (e) {
    console.error('加载供应商详情失败:', e)
    ElMessage.error('加载供应商详情失败')
  } finally {
    loading.value = false
  }
}

function buildPayload() {
  return {
    fNumber: form.fNumber,
    fName: form.fName,
    fCompanyId: form.fCompanyId,
    fGroupId: form.fGroupId,
    fShortName: form.fShortName,
    fTaxRate: form.fTaxRate,
    fCountry: form.fCountry,
    fProvincial: form.fProvincial,
    fAddress: form.fAddress,
    fContact: form.fContact,
    fPhone: form.fPhone,
    fFax: form.fFax,
    fEmail: form.fEmail,
    fBank: form.fBank,
    fAccount: form.fAccount,
    fEmpId: form.fEmpId,
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
      await updateSupplier(uid.value, updateData)
      ElMessage.success('保存成功')
      await loadDetail(uid.value)
    } else {
      const res: any = await createSupplier(payload)
      ElMessage.success('创建成功')
      const newUid = res?.data?.uid
      if (newUid) {
        uid.value = newUid
        router.replace({ name: 'SupplierEdit', query: { uid: newUid } })
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

const handleApprove = () => runStatusAction(approveSupplier, '审核成功')
const handleUnapprove = () => runStatusAction(unapproveSupplier, '反审核成功')
const handleDisable = () => runStatusAction(disableSupplier, '禁用成功')
const handleEnable = () => runStatusAction(enableSupplier, '反禁用成功')

const handleBack = () => {
  router.push({ name: 'SupplierList' })
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
.supplier-edit-container {
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

/* 标签保持单行，避免"供应商代码/名称"等5字标签换行 */
.edit-form :deep(.el-form-item__label) {
  white-space: nowrap;
}
</style>
