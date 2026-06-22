<template>
  <div class="sb-edit-container">
    <!-- 顶部工具栏 -->
    <div class="edit-toolbar">
      <el-button :icon="CirclePlus" @click="handleNew">新增</el-button>
      <el-button :icon="DocumentCopy" :disabled="!isEdit" @click="handleCopy">复制</el-button>
      <el-button type="primary" :icon="Check" :disabled="isReadonly" @click="handleSave"
                 v-permission="isEdit ? 'selbill:edit' : 'selbill:add'">保存</el-button>
      <div class="toolbar-divider" />
      <el-button v-if="isEdit && !form.fDisabled" type="info" :icon="Lock" @click="handleDisable" v-permission="'selbill:disable'">禁用</el-button>
      <el-button v-if="isEdit && form.fDisabled" :icon="Unlock" @click="handleEnable" v-permission="'selbill:disable'">反禁用</el-button>
      <el-button v-if="isEdit && form.fStatus !== 40 && form.fStatus !== 70" type="success" :icon="CircleCheck" @click="handleApprove" v-permission="'selbill:approve'">审核</el-button>
      <el-button v-if="isEdit && form.fStatus === 40" type="warning" :icon="RefreshLeft" @click="handleUnapprove" v-permission="'selbill:approve'">反审核</el-button>
      <div class="toolbar-spacer" />
      <el-tag v-if="isEdit" :type="form.fStatus === 40 ? 'success' : (form.fStatus === 70 ? 'info' : 'warning')" size="large">
        {{ statusText(form.fStatus) }}
      </el-tag>
      <el-tag v-if="isEdit && form.fDisabled" type="danger" size="large" style="margin-left:6px;">已禁用</el-tag>
      <el-button class="back-btn" :icon="Back" @click="handleBack">退出</el-button>
    </div>

    <el-form ref="formRef" :model="form" :rules="rules" label-width="92px"
             :disabled="isReadonly" class="edit-form" v-loading="loading">
      <!-- 单据头 -->
      <div class="form-header">
        <el-row :gutter="16">
          <el-col :span="12">
            <el-form-item label="单据编号">
              <el-input v-model="form.fnumber" disabled placeholder="保存后自动生成" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="单据名称" prop="fname">
              <el-input v-model="form.fname" placeholder="请输入单据名称" maxlength="50" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="16">
          <el-col :span="12">
            <el-form-item label="源单类型">
              <LookupSelect v-model="form.fsourcetrantype" module="billtemplate" placeholder="请选择源单类型" preload />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="目标单据类型" prop="fdesttrantype">
              <LookupSelect v-model="form.fdesttrantype" module="billtemplate" placeholder="请选择目标单据类型" preload />
            </el-form-item>
          </el-col>
        </el-row>
      </div>

      <el-tabs v-model="activeTab" class="edit-tabs">
        <!-- 基本 -->
        <el-tab-pane label="基本" name="basic">
          <el-row :gutter="16" class="cb-row">
            <el-col :span="8"><el-checkbox v-model="form.fisuse">启用流程</el-checkbox></el-col>
            <el-col :span="8"><el-checkbox v-model="form.fdefault">默认源单</el-checkbox></el-col>
            <el-col :span="8"><el-checkbox v-model="form.fisopensource">开放源单</el-checkbox></el-col>
          </el-row>
          <el-row :gutter="16" class="cb-row">
            <el-col :span="8"><el-checkbox v-model="form.fcheck">提交审核</el-checkbox></el-col>
            <el-col :span="8"><el-checkbox v-model="form.fiscontrolqty">控制数量</el-checkbox></el-col>
            <el-col :span="8"><el-checkbox v-model="form.fisdefaultstock">带出仓库仓位</el-checkbox></el-col>
          </el-row>
          <el-row :gutter="16" class="cb-row">
            <el-col :span="8"><el-checkbox v-model="form.fcansynerp">同步到ERP</el-checkbox></el-col>
            <el-col :span="8"><el-checkbox v-model="form.fcheckerpstock">实时同步到ERP</el-checkbox></el-col>
            <el-col :span="8"><el-checkbox v-model="form.fischecklot">检查批次</el-checkbox></el-col>
          </el-row>
          <el-row :gutter="16" class="cb-row">
            <el-col :span="8"><el-checkbox v-model="form.fischeckaux">检查辅助属性</el-checkbox></el-col>
            <el-col :span="8"><el-checkbox v-model="form.fischeckkfdate">检查保质期</el-checkbox></el-col>
            <el-col :span="8"><el-checkbox v-model="form.fisusecoderule">启用条码解析</el-checkbox></el-col>
          </el-row>
        </el-tab-pane>

        <!-- Wise配置 -->
        <el-tab-pane label="Wise配置" name="wise">
          <el-row :gutter="16">
            <el-col :span="16">
              <el-form-item label="ERP存储过程">
                <el-input v-model="form.fproname" placeholder="ERP 同步存储过程名称" maxlength="50" />
              </el-form-item>
            </el-col>
          </el-row>
        </el-tab-pane>

        <!-- 其他 -->
        <el-tab-pane label="其他" name="other">
          <el-row :gutter="16">
            <el-col :span="8"><el-form-item label="制单人"><el-input :model-value="form.cuserName" disabled /></el-form-item></el-col>
            <el-col :span="8"><el-form-item label="制单日期"><el-input :model-value="fmtDateTime(form.cYmd)" disabled /></el-form-item></el-col>
            <el-col :span="8"><el-form-item label="数据状态"><el-input :model-value="form.fstatusName || statusText(form.fStatus)" disabled /></el-form-item></el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="8"><el-form-item label="修改人"><el-input :model-value="form.muserName" disabled /></el-form-item></el-col>
            <el-col :span="8"><el-form-item label="修改日期"><el-input :model-value="fmtDateTime(form.mYmd)" disabled /></el-form-item></el-col>
            <el-col :span="8"><el-form-item label="禁用"><el-checkbox :model-value="!!form.fDisabled" disabled /></el-form-item></el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="8"><el-form-item label="审核人"><el-input :model-value="form.fcheckerName" disabled /></el-form-item></el-col>
            <el-col :span="8"><el-form-item label="审核日期"><el-input :model-value="fmtAuditDate(form.fcheckdate)" disabled /></el-form-item></el-col>
            <el-col :span="8"></el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="8"><el-form-item label="禁用人"><el-input :model-value="form.fdisableName" disabled /></el-form-item></el-col>
            <el-col :span="8"><el-form-item label="禁用日期"><el-input :model-value="fmtAuditDate(form.fdisabledate)" disabled /></el-form-item></el-col>
            <el-col :span="8"></el-col>
          </el-row>
        </el-tab-pane>
      </el-tabs>

      <!-- 单据类型映射 -->
      <div class="grid-section">
        <div class="grid-toolbar">
          <span class="grid-title">单据类型映射</span>
          <el-button type="primary" size="small" :disabled="isReadonly" @click="addLine"><el-icon><Plus /></el-icon> 行新增</el-button>
          <el-button size="small" :disabled="isReadonly" @click="insertLine">行插入</el-button>
          <el-button size="small" type="danger" :disabled="isReadonly || selectedLineIndex < 0" @click="deleteLine">行删除</el-button>
        </div>

        <el-table :data="lineItems" border size="small" row-key="_key" highlight-current-row
                  :row-class-name="lineRowClass" @row-click="onLineRowClick" empty-text="暂无映射，点击“行新增”添加">
          <el-table-column type="index" label="行号" width="55" align="center" />
          <el-table-column label="源单编号" min-width="220">
            <template #default="{ row }">
              <LookupSelect :key="'s'+row._key" v-model="row.fsourceid" module="billtype" :parent-id="form.fsourcetrantype"
                            placeholder="选择源单编号" :disabled="isReadonly || !form.fsourcetrantype" preload
                            @change="(it: any) => onSourceChange(row, it)" />
            </template>
          </el-table-column>
          <el-table-column prop="fsourceName" label="源单名称" min-width="150" />
          <el-table-column label="目标编号" min-width="220">
            <template #default="{ row }">
              <LookupSelect :key="'d'+row._key" v-model="row.fdestid" module="billtype" :parent-id="form.fdesttrantype"
                            placeholder="选择目标编号" :disabled="isReadonly || !form.fdesttrantype" preload
                            @change="(it: any) => onDestChange(row, it)" />
            </template>
          </el-table-column>
          <el-table-column prop="fdestName" label="目标名称" min-width="150" />
          <el-table-column label="是否默认值" width="100" align="center">
            <template #default="{ row }"><el-checkbox v-model="row.fdefault" :disabled="isReadonly" /></template>
          </el-table-column>
        </el-table>
      </div>
    </el-form>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { ElMessage } from 'element-plus'
import type { FormInstance, FormRules } from 'element-plus'
import { Check, Back, Plus, CircleCheck, RefreshLeft, CirclePlus, DocumentCopy, Lock, Unlock } from '@element-plus/icons-vue'
import {
  getSelBill, createSelBill, updateSelBill,
  approveSelBill, unapproveSelBill, disableSelBill, enableSelBill, type SelBillEntry
} from '../../api/selBill'
import { formatDate, formatDateOnly } from '../../utils/format'
import LookupSelect from '../../components/LookupSelect.vue'

const router = useRouter()
const route = useRoute()

const formRef = ref<FormInstance>()
const loading = ref(false)
const activeTab = ref('basic')
const lineItems = ref<any[]>([])
const selectedLineIndex = ref(-1)

const uid = ref<string>((route.query.uid as string) || '')
const isEdit = computed(() => !!uid.value)

const defaultForm = {
  uid: '',
  fStatus: 0,
  fnumber: '',
  fname: '',
  fsourcetrantype: '',
  fdesttrantype: '',
  // 基本页签开关
  fisuse: true,
  fdefault: false,
  fisopensource: false,
  fcheck: false,
  fiscontrolqty: false,
  fisdefaultstock: false,
  fcansynerp: false,
  fcheckerpstock: false,
  fischecklot: false,
  fischeckaux: false,
  fischeckkfdate: false,
  fisusecoderule: false,
  // Wise配置
  fproname: '',
  // 只读
  fstatusName: '',
  fDisabled: false,
  cuserName: '', cYmd: '',
  muserName: '', mYmd: '',
  fcheckerName: '', fcheckdate: '',
  fdisableName: '', fdisabledate: ''
}

const form = reactive({ ...defaultForm })
const isReadonly = computed(() => isEdit.value && (form.fStatus === 40 || form.fStatus === 70))

const rules: FormRules = {
  fname: [{ required: true, message: '请输入单据名称', trigger: 'blur' }],
  fdesttrantype: [{ required: true, message: '请选择目标单据类型', trigger: 'change' }]
}

let rowKeySeq = 0
const nextKey = () => ++rowKeySeq

const statusText = (s?: number) => (s === 40 ? '审核' : s === 70 ? '关闭' : '暂存')
const fmtDateTime = (d?: string) => {
  if (!d) return ''
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() <= 1900) return ''
  return formatDate(d)
}
const fmtAuditDate = (d?: string) => {
  if (!d) return ''
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() <= 1900) return ''
  return formatDateOnly(d)
}

const lineRowClass = ({ rowIndex }: { rowIndex: number }) => (rowIndex === selectedLineIndex.value ? 'cur-row' : '')
const onLineRowClick = (row: any) => { selectedLineIndex.value = lineItems.value.indexOf(row) }

const newLine = () => ({
  fsourceid: '', fsourceNumber: '', fsourceName: '',
  fdestid: '', fdestNumber: '', fdestName: '',
  fdefault: false, _key: nextKey()
})

const addLine = () => { lineItems.value.push(newLine()); selectedLineIndex.value = lineItems.value.length - 1 }
const insertLine = () => {
  const at = selectedLineIndex.value >= 0 ? selectedLineIndex.value : lineItems.value.length
  lineItems.value.splice(at, 0, newLine()); selectedLineIndex.value = at
}
const deleteLine = () => {
  if (selectedLineIndex.value < 0) return
  lineItems.value.splice(selectedLineIndex.value, 1)
  selectedLineIndex.value = lineItems.value.length > 0 ? Math.min(selectedLineIndex.value, lineItems.value.length - 1) : -1
}

const onSourceChange = (row: any, item: any) => { row.fsourceNumber = item?.fNumber || ''; row.fsourceName = item?.fName || '' }
const onDestChange = (row: any, item: any) => { row.fdestNumber = item?.fNumber || ''; row.fdestName = item?.fName || '' }

async function loadDetail(id: string) {
  loading.value = true
  try {
    const res: any = await getSelBill(id)
    const d = res.data
    // 先回填表头（含源单/目标类型），保证明细 LookupSelect 挂载时 parentId 已就绪
    Object.keys(defaultForm).forEach(k => { if (d[k] !== undefined && d[k] !== null) (form as any)[k] = d[k] })
    form.uid = d.uid
    form.fStatus = d.fStatus
    lineItems.value = (d.entries || []).map((e: SelBillEntry) => ({ ...e, _key: nextKey() }))
    selectedLineIndex.value = lineItems.value.length > 0 ? 0 : -1
  } catch (e) {
    console.error('加载出入库流程配置失败:', e)
    ElMessage.error('加载出入库流程配置失败')
  } finally {
    loading.value = false
  }
}

function buildPayload() {
  return {
    fnumber: form.fnumber,
    fname: form.fname,
    fsourcetrantype: form.fsourcetrantype,
    fdesttrantype: form.fdesttrantype,
    fisuse: form.fisuse,
    fdefault: form.fdefault,
    fisopensource: form.fisopensource,
    fcheck: form.fcheck,
    fiscontrolqty: form.fiscontrolqty,
    fisdefaultstock: form.fisdefaultstock,
    fcansynerp: form.fcansynerp,
    fcheckerpstock: form.fcheckerpstock,
    fischecklot: form.fischecklot,
    fischeckaux: form.fischeckaux,
    fischeckkfdate: form.fischeckkfdate,
    fisusecoderule: form.fisusecoderule,
    fproname: form.fproname,
    entries: lineItems.value.filter(it => it.fsourceid || it.fdestid).map(it => ({
      fsourceid: it.fsourceid || '',
      fdestid: it.fdestid || '',
      fdefault: !!it.fdefault
    }))
  }
}

async function handleSave() {
  if (!formRef.value) return
  try { await formRef.value.validate() } catch { activeTab.value = 'basic'; return }
  loading.value = true
  try {
    if (isEdit.value) {
      await updateSelBill(uid.value, buildPayload())
      ElMessage.success('保存成功')
      await loadDetail(uid.value)
    } else {
      const res: any = await createSelBill(buildPayload())
      ElMessage.success('创建成功')
      const newUid = res?.data?.uid
      if (newUid) {
        uid.value = newUid
        router.replace({ name: 'SelBillEdit', query: { uid: newUid } })
        await loadDetail(newUid)
      } else { handleBack() }
    }
  } catch (error: any) {
    ElMessage.error(error?.response?.data?.message || '提交失败')
  } finally {
    loading.value = false
  }
}

function resetForm() {
  Object.assign(form, defaultForm)
  lineItems.value = []
  selectedLineIndex.value = -1
  activeTab.value = 'basic'
  formRef.value?.clearValidate()
}

const handleNew = () => {
  uid.value = ''
  resetForm()
  if (route.query.uid) router.replace({ name: 'SelBillEdit' })
}

const handleCopy = () => {
  // 以当前内容为蓝本另存为新配置：清空主键/编号/状态/审计，明细保留但去除主键
  uid.value = ''
  form.uid = ''
  form.fnumber = ''
  form.fStatus = 0
  form.fDisabled = false
  form.fstatusName = ''
  form.cuserName = ''; form.cYmd = ''
  form.muserName = ''; form.mYmd = ''
  form.fcheckerName = ''; form.fcheckdate = ''
  form.fdisableName = ''; form.fdisabledate = ''
  lineItems.value = lineItems.value.map(it => ({ ...it, uid: undefined, fentryid: undefined, _key: nextKey() }))
  if (route.query.uid) router.replace({ name: 'SelBillEdit' })
  ElMessage.info('已复制为新配置，请修改后保存')
}

async function runStatus(fn: (id: string) => Promise<any>, msg: string) {
  if (!uid.value) return
  try { await fn(uid.value); ElMessage.success(msg); await loadDetail(uid.value) }
  catch (error: any) { ElMessage.error(error?.response?.data?.message || '操作失败') }
}
const handleApprove = () => runStatus(approveSelBill, '审核成功')
const handleUnapprove = () => runStatus(unapproveSelBill, '反审核成功')
const handleDisable = () => runStatus(disableSelBill, '禁用成功')
const handleEnable = () => runStatus(enableSelBill, '反禁用成功')
const handleBack = () => router.push({ name: 'SelBillList' })

onMounted(async () => {
  if (isEdit.value) await loadDetail(uid.value)
})
</script>

<style scoped>
.sb-edit-container {
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
  border-bottom: 1px solid var(--border-color, #ebeef5);
}
.toolbar-divider { width: 1px; height: 22px; background: var(--border-color, #ebeef5); margin: 0 4px; }
.toolbar-spacer { flex: 1; }
.back-btn { margin-left: 8px; }
.edit-form { padding: 16px 20px 24px; overflow-y: auto; }
.form-header { padding-bottom: 4px; border-bottom: 1px dashed var(--border-color, #ebeef5); margin-bottom: 4px; }
.edit-tabs { margin-top: 4px; }
.cb-row { margin-bottom: 14px; }
.grid-section { margin-top: 16px; }
.grid-toolbar { display: flex; align-items: center; gap: 8px; margin-bottom: 8px; }
.grid-title { font-weight: 600; margin-right: 8px; }
.edit-form :deep(.el-table__body tr.cur-row > td.el-table__cell) {
  background-color: var(--el-color-primary-light-9);
}
</style>
