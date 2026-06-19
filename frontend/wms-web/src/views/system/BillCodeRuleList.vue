<template>
  <div class="bill-code-rule-container">
    <div class="header-actions">
      <div class="page-tip">按业务表单配置单据编号与条码编号的生成规则（常量 / 文本字段 / 日期字段 / 流水号）。可「登记新单据」把新单据纳入此列表后直接配置；纯常量/日期/流水规则无需开发改代码。</div>
      <div class="toolbar-btns">
        <el-button type="primary" @click="openRegister" v-permission="'billcoderule:edit'">
          <el-icon><Plus /></el-icon> 登记新单据
        </el-button>
        <el-button @click="fetchData">
          <el-icon><Refresh /></el-icon> 刷新
        </el-button>
      </div>
    </div>

    <el-table v-loading="loading" :data="rows" style="width: 100%" row-key="formKey" border size="small">
      <el-table-column label="业务表单" min-width="160">
        <template #default="scope">
          <div>{{ scope.row.formName }}</div>
          <div class="form-key">{{ scope.row.formKey }}</div>
        </template>
      </el-table-column>
      <el-table-column label="单据编号规则" min-width="260">
        <template #default="scope">
          <template v-if="scope.row.billConfigured">
            <div>{{ scope.row.billRuleName }} <el-tag v-if="scope.row.billDisabled" type="danger" size="small">已禁用</el-tag></div>
            <div class="summary">{{ scope.row.billSummary }}</div>
          </template>
          <el-tag v-else type="info" size="small">未配置</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="条码编号规则" min-width="260">
        <template #default="scope">
          <template v-if="scope.row.barcodeConfigured">
            <div>{{ scope.row.barcodeRuleName }} <el-tag v-if="scope.row.barcodeDisabled" type="danger" size="small">已禁用</el-tag></div>
            <div class="summary">{{ scope.row.barcodeSummary }}</div>
          </template>
          <el-tag v-else type="info" size="small">未配置</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="最后修改" width="170">
        <template #default="scope">{{ fmtDateTime(scope.row.mYmd) }}</template>
      </el-table-column>
      <el-table-column label="操作" width="210" fixed="right">
        <template #default="scope">
          <div class="op-btns">
            <el-button size="small" type="primary" @click="handleEdit(scope.row)" v-permission="'billcoderule:edit'">配置</el-button>
            <el-button
              v-if="scope.row.billConfigured || scope.row.barcodeConfigured"
              size="small"
              type="danger"
              plain
              @click="handleClear(scope.row)"
              v-permission="'billcoderule:edit'"
            >清除</el-button>
            <el-button size="small" type="warning" plain @click="handleUnregister(scope.row)" v-permission="'billcoderule:edit'">注销</el-button>
          </div>
        </template>
      </el-table-column>
    </el-table>

    <el-dialog v-model="registerVisible" title="登记业务表单" width="560px">
      <el-form :model="registerForm" label-width="110px">
        <el-form-item label="选择单据">
          <el-select v-model="selectedEntity" placeholder="从系统已注册单据中选择" style="width: 100%" filterable @change="onPickDocument">
            <el-option
              v-for="d in documentTypes"
              :key="d.entityName"
              :label="d.formName"
              :value="d.entityName"
              :disabled="d.registered"
            >
              <span>{{ d.formName }}</span>
              <span class="opt-hint">{{ d.entityName }}{{ d.registered ? ' · 已登记' : '' }}</span>
            </el-option>
          </el-select>
          <div class="field-hint">从系统已有单据里选，选中后自动带出标识与实体类名，无需手填；已登记的单据不可重复登记。</div>
        </el-form-item>
        <el-form-item label="表单名称">
          <el-input v-model="registerForm.formName" placeholder="界面显示名（可改）" :disabled="!selectedEntity" />
        </el-form-item>
        <el-form-item label="表单标识">
          <el-input v-model="registerForm.formKey" disabled placeholder="选择单据后自动带出" />
        </el-form-item>
        <el-form-item label="实体类名">
          <el-input v-model="registerForm.entityName" disabled placeholder="选择单据后自动带出" />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="registerVisible = false">取消</el-button>
        <el-button type="primary" :loading="registerSaving" :disabled="!selectedEntity" @click="submitRegister">确定</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import {
  getBillCodeRules,
  deleteBillCodeRule,
  registerBillCodeForm,
  unregisterBillCodeForm,
  getBillCodeDocumentTypes,
  type BillCodeRuleRow,
  type BillCodeDocument,
} from '../../api/billCodeRule'

const router = useRouter()
const loading = ref(false)
const rows = ref<BillCodeRuleRow[]>([])

const registerVisible = ref(false)
const registerSaving = ref(false)
const registerForm = reactive({ formKey: '', formName: '', entityName: '' })
const documentTypes = ref<BillCodeDocument[]>([])
const selectedEntity = ref('')

const openRegister = async () => {
  registerForm.formKey = ''
  registerForm.formName = ''
  registerForm.entityName = ''
  selectedEntity.value = ''
  registerVisible.value = true
  try {
    const res: any = await getBillCodeDocumentTypes()
    documentTypes.value = res?.data || []
  } catch {
    documentTypes.value = []
  }
}

const onPickDocument = (entityName: string) => {
  const d = documentTypes.value.find((x) => x.entityName === entityName)
  if (!d) return
  registerForm.formKey = d.formKey
  registerForm.formName = d.formName
  registerForm.entityName = d.entityName
}

const submitRegister = async () => {
  if (!selectedEntity.value || !registerForm.formKey.trim()) {
    ElMessage.warning('请先选择单据')
    return
  }
  if (!registerForm.formName.trim()) {
    ElMessage.warning('请填写表单名称')
    return
  }
  registerSaving.value = true
  try {
    await registerBillCodeForm({
      formKey: registerForm.formKey.trim(),
      formName: registerForm.formName.trim(),
      entityName: registerForm.entityName.trim(),
    })
    ElMessage.success('业务表单已登记')
    registerVisible.value = false
    fetchData()
  } catch {
    // 全局拦截器已统一提示错误
  } finally {
    registerSaving.value = false
  }
}

const handleUnregister = async (row: BillCodeRuleRow) => {
  try {
    await ElMessageBox.confirm(
      `确定注销「${row.formName}」吗？注销后该单据将从本列表移除（已配置的规则与流水计数不会被删除，重新登记同标识可恢复显示）。`,
      '注销业务表单',
      { type: 'warning', confirmButtonText: '注销', cancelButtonText: '取消' }
    )
  } catch {
    return
  }
  try {
    await unregisterBillCodeForm(row.formKey)
    ElMessage.success('已注销该业务表单')
    fetchData()
  } catch {
    // 全局拦截器已统一提示错误
  }
}

const fetchData = async () => {
  loading.value = true
  try {
    const res: any = await getBillCodeRules()
    rows.value = res?.data || []
  } finally {
    loading.value = false
  }
}

const handleEdit = (row: BillCodeRuleRow) => {
  router.push({ path: '/system/code-rules/edit', query: { formKey: row.formKey } })
}

const handleClear = async (row: BillCodeRuleRow) => {
  try {
    await ElMessageBox.confirm(
      `确定清除「${row.formName}」的编码规则配置吗？清除后该单据将无法自动取号（流水计数不会被清除，可在配置页单独「重置流水」）。`,
      '清除配置',
      { type: 'warning', confirmButtonText: '清除', cancelButtonText: '取消' }
    )
  } catch {
    return
  }
  try {
    await deleteBillCodeRule(row.formKey)
    ElMessage.success('已清除该表单的编码规则配置')
    fetchData()
  } catch {
    // 全局拦截器已统一提示错误，无需重复
  }
}

const fmtDateTime = (v?: string | null) => {
  if (!v) return ''
  const d = new Date(v)
  if (isNaN(d.getTime()) || d.getFullYear() <= 1900) return ''
  return v.slice(0, 19).replace('T', ' ')
}

onMounted(fetchData)
</script>

<style scoped>
.bill-code-rule-container {
  padding: 20px;
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
}
.header-actions {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 12px;
}
.page-tip {
  color: #909399;
  font-size: 13px;
}
.toolbar-btns {
  display: flex;
  align-items: center;
  gap: 8px;
  flex-shrink: 0;
}
.toolbar-btns :deep(.el-button) {
  margin-left: 0;
}
.op-btns {
  display: flex;
  align-items: center;
  flex-wrap: nowrap;
  gap: 6px;
}
.op-btns :deep(.el-button) {
  margin-left: 0;
}
.form-key {
  color: #909399;
  font-size: 12px;
}
.summary {
  color: #909399;
  font-size: 12px;
}
.field-hint {
  color: #909399;
  font-size: 12px;
  line-height: 1.5;
  margin-top: 4px;
}
.opt-hint {
  float: right;
  color: #a8abb2;
  font-size: 12px;
  margin-left: 16px;
}
</style>
