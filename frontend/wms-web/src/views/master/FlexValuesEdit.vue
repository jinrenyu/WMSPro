<template>
  <div class="flexvalues-edit-container">
    <!-- 顶部工具栏 -->
    <div class="edit-toolbar">
      <el-button type="primary" :disabled="isReadonly" @click="handleSave" v-permission="isEdit ? 'flexvalues:edit' : 'flexvalues:add'">
        <el-icon><Check /></el-icon> 保存
      </el-button>
      <el-button v-if="showApprove" type="success" @click="handleApprove" v-permission="'flexvalues:approve'">审核</el-button>
      <el-button v-if="showUnapprove" type="warning" @click="handleUnapprove" v-permission="'flexvalues:approve'">反审核</el-button>
      <el-button v-if="showDisable" type="info" @click="handleDisable" v-permission="'flexvalues:disable'">禁用</el-button>
      <el-button v-if="showEnable" @click="handleEnable" v-permission="'flexvalues:disable'">反禁用</el-button>
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
          <el-col :span="12">
            <el-form-item label="代码" prop="fNumber">
              <el-input v-model="form.fNumber" :disabled="isEdit" placeholder="编辑时不可修改" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="名称" prop="fName">
              <el-input v-model="form.fName" placeholder="必填" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="类型">
              <el-input model-value="仓位" disabled />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="组织">
              <el-input :model-value="companyDisplayName" disabled placeholder="当前组织" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="24">
            <el-form-item label="描述">
              <el-input v-model="form.fDescription" type="textarea" :rows="2" />
            </el-form-item>
          </el-col>
        </el-row>
      </div>

      <!-- Tab 区 -->
      <el-tabs v-model="activeTab" class="edit-tabs">
        <!-- ============ 值列表 ============ -->
        <el-tab-pane label="值列表" name="entries">
          <div class="grid-toolbar">
            <el-button type="primary" size="small" :disabled="isReadonly" @click="addEntry">行新增</el-button>
            <el-button size="small" :disabled="isReadonly" @click="insertEntry">行插入</el-button>
            <el-button size="small" type="danger" :disabled="isReadonly || selectedEntryIndex < 0" @click="deleteEntry">行删除</el-button>
          </div>
          <el-table
            :data="entries"
            border
            size="small"
            row-key="_key"
            :row-class-name="entryRowClass"
            empty-text="暂无值，点击“行新增”添加"
            @row-click="onEntryRowClick"
          >
            <el-table-column type="index" label="行号" width="60" align="center" />
            <el-table-column label="代码" min-width="160">
              <template #default="{ row }">
                <el-input v-model="row.fNumber" size="small" :disabled="isReadonly" placeholder="代码" />
              </template>
            </el-table-column>
            <el-table-column label="名称" min-width="160">
              <template #default="{ row }">
                <el-input v-model="row.fName" size="small" :disabled="isReadonly" placeholder="名称" />
              </template>
            </el-table-column>
            <el-table-column label="说明" min-width="200">
              <template #default="{ row }">
                <el-input v-model="row.fDescription" size="small" :disabled="isReadonly" placeholder="说明" />
              </template>
            </el-table-column>
            <el-table-column label="禁用" width="80" align="center">
              <template #default="{ row }">
                <el-checkbox v-model="row.fForbid" :disabled="isReadonly" />
              </template>
            </el-table-column>
          </el-table>
        </el-tab-pane>

        <!-- ============ 其他（只读系统信息） ============ -->
        <el-tab-pane label="其他" name="other">
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="创建人"><el-input :model-value="form.cUser" disabled /></el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="创建日期"><el-input :model-value="fmtAuditDate(form.cYmd)" disabled /></el-form-item>
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
              <el-form-item label="审核人"><el-input :model-value="form.fCheckerId" disabled /></el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="审核日期"><el-input :model-value="fmtAuditDate(form.fCheckDate)" disabled /></el-form-item>
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
              <el-form-item label="禁用状态"><el-checkbox :model-value="form.fDisabled" disabled /></el-form-item>
            </el-col>
          </el-row>
        </el-tab-pane>

        <!-- ============ 仓位信息详情 ============ -->
        <el-tab-pane label="仓位信息详情" name="detail">
          <div class="section-block">
            <div class="section-title">尺寸</div>
            <el-row :gutter="20">
              <el-col :span="6">
                <el-form-item label="长"><el-input-number v-model="form.fLength" :min="0" :precision="2" :controls="false" style="width: 100%" /></el-form-item>
              </el-col>
              <el-col :span="6">
                <el-form-item label="宽"><el-input-number v-model="form.fWidth" :min="0" :precision="2" :controls="false" style="width: 100%" /></el-form-item>
              </el-col>
              <el-col :span="6">
                <el-form-item label="高"><el-input-number v-model="form.fHeight" :min="0" :precision="2" :controls="false" style="width: 100%" /></el-form-item>
              </el-col>
              <el-col :span="6">
                <el-form-item label="容积"><el-input-number v-model="form.fVolume" :min="0" :precision="4" :controls="false" style="width: 100%" /></el-form-item>
              </el-col>
            </el-row>
          </div>
          <div class="section-block">
            <div class="section-title">限制</div>
            <el-row :gutter="20">
              <el-col :span="8">
                <el-form-item label="体积限制"><el-input-number v-model="form.fVolumeLimit" :min="0" :precision="2" :controls="false" style="width: 100%" /></el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="重量限制"><el-input-number v-model="form.fWeightLimit" :min="0" :precision="2" :controls="false" style="width: 100%" /></el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="混放种类限制"><el-input v-model="form.fMixTypeLimit" /></el-form-item>
              </el-col>
            </el-row>
          </div>
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
  getFlexValue, createFlexValue, updateFlexValue,
  approveFlexValue, unapproveFlexValue, disableFlexValue, enableFlexValue,
  saveFlexValuesEntries, type FlexValueEntry,
} from '../../api/flexValues'
import { formatDate } from '../../utils/format'
import { useOrgStore } from '../../stores/org'

const router = useRouter()
const route = useRoute()
const orgStore = useOrgStore()

const formRef = ref<FormInstance>()
const loading = ref(false)
const activeTab = ref('entries')

const uid = ref<string>((route.query.uid as string) || '')
const isEdit = computed(() => !!uid.value)
const isReadonly = computed(() => isEdit.value && (form.fStatus === 40 || form.fDisabled))

// ---- 值列表（仓位集值）----
const entries = ref<FlexValueEntry[]>([])
const selectedEntryIndex = ref(-1)
let rowKeySeq = 0
const nextKey = () => ++rowKeySeq

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
  fNumber: [{ required: true, message: '请输入代码', trigger: 'blur' }],
  fName: [{ required: true, message: '请输入名称', trigger: 'blur' }],
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
  fDescription: '',
  // 仓位信息详情
  fLength: 0,
  fWidth: 0,
  fHeight: 0,
  fVolume: 0,
  fVolumeLimit: 0,
  fWeightLimit: 0,
  fMixTypeLimit: '',
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

// ---- 值列表 行操作 ----
const entryRowClass = ({ rowIndex }: { rowIndex: number }) => (rowIndex === selectedEntryIndex.value ? 'cur-row' : '')
const onEntryRowClick = (row: FlexValueEntry) => { selectedEntryIndex.value = entries.value.indexOf(row) }
const newEntry = (): FlexValueEntry => ({ uid: '', fNumber: '', fName: '', fDescription: '', fForbid: false, _key: nextKey() })
const addEntry = () => {
  entries.value.push(newEntry())
  selectedEntryIndex.value = entries.value.length - 1
}
const insertEntry = () => {
  const at = selectedEntryIndex.value >= 0 ? selectedEntryIndex.value : entries.value.length
  entries.value.splice(at, 0, newEntry())
  selectedEntryIndex.value = at
}
const deleteEntry = () => {
  if (selectedEntryIndex.value < 0) return
  entries.value.splice(selectedEntryIndex.value, 1)
  selectedEntryIndex.value = entries.value.length > 0 ? Math.min(selectedEntryIndex.value, entries.value.length - 1) : -1
}

function buildPayload() {
  return {
    fNumber: form.fNumber,
    fName: form.fName,
    fCompanyId: form.fCompanyId,
    fDescription: form.fDescription,
    fLength: form.fLength,
    fWidth: form.fWidth,
    fHeight: form.fHeight,
    fVolume: form.fVolume,
    fVolumeLimit: form.fVolumeLimit,
    fWeightLimit: form.fWeightLimit,
    fMixTypeLimit: form.fMixTypeLimit,
    fGroupId: form.fGroupId,
  }
}

function buildEntriesPayload() {
  return entries.value
    .filter((e) => (e.fNumber && e.fNumber.trim()) || (e.fName && e.fName.trim()))
    .map((e) => ({
      uid: e.uid || '',
      fNumber: e.fNumber,
      fName: e.fName,
      fDescription: e.fDescription || '',
      fForbid: e.fForbid || false,
    }))
}

async function loadDetail(id: string) {
  loading.value = true
  try {
    const res: any = await getFlexValue(id)
    const d = res.data
    Object.keys(defaultForm).forEach((k) => {
      if (d[k] !== undefined && d[k] !== null) {
        ;(form as any)[k] = d[k]
      }
    })
    form.uid = d.uid
    entries.value = (d.entries || []).map((e: any) => ({
      uid: e.uid,
      fEntryId: e.fEntryId,
      fNumber: e.fNumber || '',
      fName: e.fName || '',
      fDescription: e.fDescription || '',
      fForbid: e.fForbid || false,
      _key: nextKey(),
    }))
    selectedEntryIndex.value = -1
  } catch (e) {
    console.error('加载仓位详情失败:', e)
    ElMessage.error('加载仓位详情失败')
  } finally {
    loading.value = false
  }
}

async function handleSave() {
  if (!formRef.value) return
  try {
    await formRef.value.validate()
  } catch {
    return
  }
  const payload = buildPayload()
  loading.value = true
  try {
    if (isEdit.value) {
      const { fNumber, fCompanyId, ...updateData } = payload
      await updateFlexValue(uid.value, updateData)
      await saveFlexValuesEntries(uid.value, buildEntriesPayload())
      ElMessage.success('保存成功')
      await loadDetail(uid.value)
    } else {
      const res: any = await createFlexValue(payload)
      const newUid = res?.data?.uid
      if (newUid) {
        await saveFlexValuesEntries(newUid, buildEntriesPayload())
        ElMessage.success('创建成功')
        uid.value = newUid
        router.replace({ name: 'FlexValuesEdit', query: { uid: newUid } })
        await loadDetail(newUid)
      } else {
        ElMessage.success('创建成功')
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

const handleApprove = () => runStatusAction(approveFlexValue, '审核成功')
const handleUnapprove = () => runStatusAction(unapproveFlexValue, '反审核成功')
const handleDisable = () => runStatusAction(disableFlexValue, '禁用成功')
const handleEnable = () => runStatusAction(enableFlexValue, '反禁用成功')

const handleBack = () => {
  router.push({ name: 'FlexValuesList' })
}

onMounted(async () => {
  if (!orgStore.loaded) orgStore.loadOrgs()
  if (isEdit.value) {
    await loadDetail(uid.value)
  } else {
    form.fCompanyId = orgStore.currentOrgId
  }
})
</script>

<style scoped>
.flexvalues-edit-container {
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

.grid-toolbar {
  display: flex;
  align-items: center;
  gap: 8px;
  margin-bottom: 8px;
}

.section-block {
  margin-top: 8px;
  padding: 12px 16px 0;
  border: 1px solid var(--border-color);
  border-radius: 6px;
}

.section-title {
  font-weight: 600;
  margin-bottom: 12px;
  color: var(--el-text-color-primary);
}

.edit-form :deep(.el-form-item__label) { white-space: nowrap; }

/* 选中行高亮 */
.edit-form :deep(.el-table__body tr.cur-row > td.el-table__cell) {
  background-color: var(--el-color-primary-light-9);
}
</style>
