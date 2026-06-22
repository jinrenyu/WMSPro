<template>
  <div class="rn-label-generate" v-loading="loading">
    <div class="edit-toolbar">
      <el-button type="primary" @click="handleGenerate" :loading="generating" v-permission="'labelreceivenotice:generate'">
        <el-icon><PriceTag /></el-icon> 条码生成
      </el-button>
      <el-button @click="goBack"><el-icon><Back /></el-icon> 返回</el-button>
      <span class="toolbar-sep" />
      <el-button type="danger" plain :disabled="!canVoid" @click="handleVoid" v-permission="'labelreceivenotice:void'">条码作废</el-button>
      <el-button type="warning" plain :disabled="!canUnvoid" @click="handleUnvoid" v-permission="'labelreceivenotice:void'">条码反作废</el-button>
      <span class="toolbar-sep" />
      <span class="tpl-label">模板</span>
      <el-select v-model="selectedTemplateUid" placeholder="选择标签模板" style="width: 200px" :disabled="templates.length === 0">
        <el-option v-for="t in templates" :key="t.uid" :label="t.fName + (t.fIsDefault ? '（默认）' : '')" :value="t.uid" />
      </el-select>
      <el-button plain :disabled="selectedBarcodes.length === 0" @click="handlePrint" v-permission="'labelreceivenotice:print'">打印</el-button>
      <el-button plain :disabled="selectedBarcodes.length === 0" @click="handleExportPdf" v-permission="'labelreceivenotice:print'">导出PDF</el-button>
    </div>

    <el-form label-width="92px" class="head-form" size="small">
      <!-- 来源收料通知单信息（只读） -->
      <el-row :gutter="16">
        <el-col :span="8"><el-form-item label="收料单号"><el-input :model-value="head.fbillno" disabled /></el-form-item></el-col>
        <el-col :span="8"><el-form-item label="收料组织"><el-input :model-value="head.fpurorgName" disabled /></el-form-item></el-col>
        <el-col :span="8"><el-form-item label="检验人员"><LookupSelect v-model="form.finspectid" module="employee" placeholder="检验人员" /></el-form-item></el-col>
      </el-row>
      <el-row :gutter="16">
        <el-col :span="8"><el-form-item label="收料日期"><el-input :model-value="fmtDate(head.fdate)" disabled /></el-form-item></el-col>
        <el-col :span="8"><el-form-item label="供应商"><el-input :model-value="head.fsupplyName" disabled /></el-form-item></el-col>
        <el-col :span="8"><el-form-item label="物料代码"><el-input :model-value="head.fmaterialNumber" disabled /></el-form-item></el-col>
      </el-row>
      <el-row :gutter="16">
        <el-col :span="8"><el-form-item label="物料名称"><el-input :model-value="head.fmaterialName" disabled /></el-form-item></el-col>
        <el-col :span="8"><el-form-item label="规格型号"><el-input :model-value="head.fSpecification" disabled /></el-form-item></el-col>
        <el-col :span="8"><el-form-item label="辅助属性"><el-input :model-value="head.fauxpropName" disabled /></el-form-item></el-col>
      </el-row>
      <el-row :gutter="16">
        <el-col :span="8"><el-form-item label="交货数量"><el-input :model-value="fmtNum(head.factreceiveqty)" disabled /></el-form-item></el-col>
        <el-col :span="8"><el-form-item label="采购单位"><el-input :model-value="head.funitName" disabled /></el-form-item></el-col>
        <el-col :span="8"><el-form-item label="基本单位"><el-input :model-value="head.fbaseunitName" disabled /></el-form-item></el-col>
      </el-row>
      <el-row :gutter="16">
        <el-col :span="8"><el-form-item label="行号"><el-input :model-value="head.fentryid != null ? String(head.fentryid) : ''" disabled /></el-form-item></el-col>
        <el-col :span="8">
          <el-form-item label="启用批次">
            <el-checkbox :model-value="!!head.fisBatchManage" disabled />
            <span class="inline-label">启用保质期</span>
            <el-checkbox :model-value="!!head.fisKfPeriod" disabled />
          </el-form-item>
        </el-col>
      </el-row>

      <el-divider content-position="left">条码生成参数</el-divider>

      <!-- 生成参数（可编辑） -->
      <el-row :gutter="16">
        <el-col :span="8"><el-form-item label="订单批次"><el-input v-model="form.flot" placeholder="批次" /></el-form-item></el-col>
        <el-col :span="8"><el-form-item label="采购日期"><el-date-picker v-model="form.fkfdate" type="date" value-format="YYYY-MM-DD" placeholder="采购日期" style="width:100%" @change="recalcUseful" /></el-form-item></el-col>
        <el-col :span="8"><el-form-item label="有效期至"><el-date-picker v-model="form.fusefuldate" type="date" value-format="YYYY-MM-DD" placeholder="有效期至" style="width:100%" /></el-form-item></el-col>
      </el-row>
      <el-row :gutter="16">
        <el-col :span="8"><el-form-item label="保质期限"><el-input-number v-model="form.fkfperiod" :min="0" :controls="false" style="width:100%" @change="recalcUseful" /></el-form-item></el-col>
        <el-col :span="8"><el-form-item label="保质期单位"><el-input :model-value="kfUnitLabel(form.fkfunit)" disabled /></el-form-item></el-col>
        <el-col :span="8"><el-form-item label="条码类型"><el-select v-model="form.fbartype" style="width:100%"><el-option v-for="o in barTypeOptions" :key="o.value" :label="o.label" :value="o.value" /></el-select></el-form-item></el-col>
      </el-row>
      <el-row :gutter="16">
        <el-col :span="8"><el-form-item label="本次打印数量"><el-input-number v-model="form.printQty" :min="0" :precision="6" :controls="false" style="width:100%" @change="recalcCount" /></el-form-item></el-col>
        <el-col :span="8"><el-form-item label="包装数量"><el-input-number v-model="form.packageQty" :min="0" :precision="6" :controls="false" style="width:100%" :disabled="form.fbartype === 1" @change="recalcCount" /></el-form-item></el-col>
        <el-col :span="8"><el-form-item label="条码数量"><el-input :model-value="String(barcodeCount)" disabled /></el-form-item></el-col>
      </el-row>
      <el-row :gutter="16">
        <el-col :span="8">
          <el-form-item label="启用辅助数量"><el-checkbox v-model="form.enableAuxQty" @change="recalcAuxQty" /></el-form-item>
        </el-col>
        <el-col :span="8" v-if="form.enableAuxQty"><el-form-item label="辅助单位"><LookupSelect v-model="form.fsecunitid" module="unit" placeholder="辅助单位" /></el-form-item></el-col>
        <el-col :span="8" v-if="form.enableAuxQty"><el-form-item label="辅助单位数量"><el-input-number v-model="form.fsecqty" :min="0" :precision="6" :controls="false" style="width:100%" /></el-form-item></el-col>
      </el-row>
      <el-row :gutter="16">
        <el-col :span="8"><el-form-item label="打印日期"><el-input :model-value="nowText" disabled /></el-form-item></el-col>
        <el-col :span="16"><el-form-item label="备注"><el-input v-model="form.fnote" placeholder="备注" /></el-form-item></el-col>
      </el-row>
    </el-form>

    <el-divider content-position="left">条码明细（已生成 {{ barcodes.length }} 个）</el-divider>

    <el-table
      :data="barcodes"
      border
      size="small"
      height="360"
      @selection-change="handleBarcodeSelection"
    >
      <el-table-column type="selection" width="45" fixed="left" />
      <el-table-column type="index" label="#" width="50" align="center" />
      <el-table-column prop="fbarcode" label="条码" width="160" fixed="left" />
      <el-table-column label="条码状态" width="90" align="center">
        <template #default="s">
          <el-tag :type="barcodeStatusType(s.row.fbarcodestatus)" size="small">{{ s.row.fbarcodestatusName || '初始' }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="fstockstatusName" label="库存状态" width="90" align="center" />
      <el-table-column prop="fcompanyName" label="收料组织" width="110" />
      <el-table-column label="打印日期" width="110">
        <template #default="s">{{ fmtDate(s.row.fdate) }}</template>
      </el-table-column>
      <el-table-column prop="fmaterialNumber" label="物料代码" width="130" />
      <el-table-column prop="fmaterialName" label="物料名称" min-width="140" />
      <el-table-column prop="fSpecification" label="规格型号" min-width="110" />
      <el-table-column prop="flot" label="批次" width="100" />
      <el-table-column prop="fauxpropName" label="辅助属性" width="100" />
      <el-table-column prop="fqty" label="数量" width="90" align="right" />
      <el-table-column prop="funitName" label="单位" width="80" />
      <el-table-column prop="fbaseqty" label="基本单位数量" width="110" align="right" />
      <el-table-column prop="fbaseunitName" label="基本单位" width="90" />
      <el-table-column label="采购日期" width="110">
        <template #default="s">{{ fmtDate(s.row.fkfdate) }}</template>
      </el-table-column>
      <el-table-column label="有效期至" width="110">
        <template #default="s">{{ fmtDate(s.row.fusefuldate) }}</template>
      </el-table-column>
      <el-table-column prop="fsecunitqty" label="辅助单位数量" width="110" align="right" />
      <el-table-column prop="fpurbillno" label="收料单编号" width="170" />
      <el-table-column prop="fpobillno" label="采购订单编号" width="170" />
      <el-table-column prop="finspectName" label="IQC检验员" width="110" />
      <el-table-column label="IQC检验日期" width="110">
        <template #default="s">{{ fmtDate(s.row.finspectdate) }}</template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import { PriceTag, Back } from '@element-plus/icons-vue'
import LookupSelect from '../../components/LookupSelect.vue'
import { formatDate } from '../../utils/format'
import {
  getLabelHead, getLabelBarcodes, generateBarcodes,
  voidBarcodes, unvoidBarcodes, printBarcodes,
  type ReceiveNoticeLabelHead, type BarcodeLine
} from '../../api/receiveNoticeLabel'
import { getPrintTemplatesBySource } from '../../api/labelTemplate'
import { getMaterialSecUnits } from '../../api/material'
import { loadHiprint } from '../../utils/hiprintLoader'

const route = useRoute()
const router = useRouter()

const entryUid = String(route.query.entryUid || '')
const loading = ref(false)
const generating = ref(false)

const head = ref<ReceiveNoticeLabelHead>({} as any)
const barcodes = ref<BarcodeLine[]>([])
const selectedRows = ref<BarcodeLine[]>([])

// 标签模板（适用单据来源固定为收料通知单标签）
const BILL_SOURCE = 'PUR_ReceiveBill'
const templates = ref<any[]>([])
const selectedTemplateUid = ref('')
const currentTemplate = computed(() => templates.value.find(t => t.uid === selectedTemplateUid.value))

const barTypeOptions = [
  { label: '单品条码', value: 1 },
  { label: '最小包装量条码', value: 2 },
  { label: '批次条码', value: 3 },
]

const form = reactive({
  flot: '',
  fkfdate: null as string | null,
  fusefuldate: null as string | null,
  fkfperiod: 0,
  fkfunit: 0,
  fbartype: 1,
  printQty: 0,
  packageQty: 0,
  finspectid: '',
  enableAuxQty: false,
  fsecunitid: '',
  fsecqty: 0,
  fnote: '',
})

const nowText = formatDate(new Date().toISOString())

const fmtDate = (d?: string | null) => {
  if (!d) return ''
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() <= 1900) return ''
  return formatDate(d).slice(0, 10)
}
const fmtNum = (n?: number) => (n == null ? '0' : Number(n).toFixed(2))
const kfUnitLabel = (v?: number) => (v === 1 ? '月' : v === 2 ? '年' : '日')

const barcodeCount = computed(() => {
  const p = Number(form.printQty)
  if (!p || p <= 0) return 0
  // 单品条码：每个数量恒为 1，共 ceil(打印数量) 个
  if (form.fbartype === 1) return Math.ceil(p)
  // 最小包装量/批次条码：每个数量=包装数量，共 ceil(打印数量/包装数量) 个
  const pkg = Number(form.packageQty)
  if (!pkg || pkg <= 0) return 0
  return Math.ceil(p / pkg)
})
const recalcCount = () => { recalcAuxQty() }  // barcodeCount 为计算属性；打印数量变化时同步辅助数量

// 有效期至 = 采购日期 + 保质期（启用保质期时）
const recalcUseful = () => {
  if (!head.value.fisKfPeriod || !form.fkfdate || !form.fkfperiod) return
  const d = new Date(form.fkfdate)
  if (isNaN(d.getTime())) return
  if (form.fkfunit === 2) d.setFullYear(d.getFullYear() + form.fkfperiod)
  else if (form.fkfunit === 1) d.setMonth(d.getMonth() + form.fkfperiod)
  else d.setDate(d.getDate() + form.fkfperiod)
  form.fusefuldate = `${d.getFullYear()}-${String(d.getMonth() + 1).padStart(2, '0')}-${String(d.getDate()).padStart(2, '0')}`
}

// 物料第一条辅助单位换算（用于辅助数量自动换算）
const firstSecConv = ref<any>(null)
async function loadSecConv() {
  firstSecConv.value = null
  const mid = head.value?.fmaterialid
  if (!mid) return
  try {
    const res: any = await getMaterialSecUnits(mid)
    firstSecConv.value = (res.data || [])[0] || null
  } catch { firstSecConv.value = null }
}
// 启用辅助数量时：取物料第一条换算，自动带辅助单位并按打印数量算辅助数量（辅助数量=数量×辅助单位数量/基本单位数量）
const recalcAuxQty = () => {
  if (!form.enableAuxQty) return
  const c = firstSecConv.value
  if (!c || !Number(c.fConvertNumerator) || !Number(c.fConvertDenominator)) return
  if (!form.fsecunitid) form.fsecunitid = c.fSecUnitId || ''
  form.fsecqty = +(Number(form.printQty || 0) * Number(c.fConvertNumerator) / Number(c.fConvertDenominator)).toFixed(6)
}

const selectedBarcodes = computed(() => selectedRows.value.map(r => r.fbarcode))
// 条码状态 FBARCODESTATUS：1=初始 2=收料 3=已拆分 4=已合并 5=已使用 10=已废弃
const BC_INIT = 1, BC_VOID = 10
const canVoid = computed(() => selectedRows.value.length > 0 && selectedRows.value.some(r => r.fbarcodestatus === BC_INIT))
const canUnvoid = computed(() => selectedRows.value.length > 0 && selectedRows.value.some(r => r.fbarcodestatus === BC_VOID))
const barcodeStatusType = (s?: number) => (s === BC_VOID ? 'danger' : s === BC_INIT ? 'info' : 'success')

const handleBarcodeSelection = (rows: BarcodeLine[]) => { selectedRows.value = rows }

async function loadHead() {
  if (!entryUid) { ElMessage.error('缺少明细标识'); return }
  loading.value = true
  try {
    const res: any = await getLabelHead(entryUid)
    const h: ReceiveNoticeLabelHead = res.data
    if (!h) { ElMessage.error('收料通知单明细不存在'); return }
    head.value = h
    form.flot = h.flot || ''
    form.fkfperiod = h.fkfperiod || 0
    form.fkfunit = h.fkfunit || 0
    form.fbartype = h.fbartype || 1
    form.printQty = h.factreceiveqty || 0
    form.packageQty = (h.fincreaseqty && h.fincreaseqty > 0) ? h.fincreaseqty : (h.factreceiveqty || 0)
    form.fkfdate = toDateInput(h.fkfdate)
    form.fusefuldate = toDateInput(h.fexpiredate)
    await loadBarcodes()
    await loadSecConv()
  } catch (e) {
    console.error('加载条码生成页失败:', e)
    ElMessage.error('加载失败')
  } finally {
    loading.value = false
  }
}

// 后端 1900 哨兵日期过滤为 null，否则带入 YYYY-MM-DD
const toDateInput = (d?: string | null): string | null => {
  if (!d) return null
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() <= 1900) return null
  return `${date.getFullYear()}-${String(date.getMonth() + 1).padStart(2, '0')}-${String(date.getDate()).padStart(2, '0')}`
}

async function loadBarcodes() {
  const res: any = await getLabelBarcodes(entryUid)
  barcodes.value = res.data || []
}

async function loadTemplates() {
  try {
    const res: any = await getPrintTemplatesBySource(BILL_SOURCE)
    templates.value = res.data || []
    const def = templates.value.find((t: any) => t.fIsDefault) || templates.value[0]
    selectedTemplateUid.value = def?.uid || ''
  } catch (e) {
    console.error('加载标签模板失败:', e)
  }
}

async function handleGenerate() {
  if (!form.printQty || form.printQty <= 0) { ElMessage.warning('请填写本次打印数量'); return }
  if (head.value?.fisBatchManage && !String(form.flot || '').trim()) { ElMessage.warning('该物料启用了批号管理，批次（订单批次）必填'); return }
  if (form.fbartype !== 1 && (!form.packageQty || form.packageQty <= 0)) { ElMessage.warning('请填写包装数量'); return }
  if (barcodeCount.value <= 0) { ElMessage.warning('条码数量必须大于 0'); return }
  generating.value = true
  try {
    await generateBarcodes({
      receiveEntryUid: entryUid,
      fbartype: form.fbartype,
      printQty: form.printQty,
      packageQty: form.packageQty,
      flot: form.flot,
      fkfdate: form.fkfdate,
      fusefuldate: form.fusefuldate,
      fkfperiod: form.fkfperiod,
      fkfunit: form.fkfunit,
      finspectid: form.finspectid,
      enableAuxQty: form.enableAuxQty,
      fsecunitid: form.enableAuxQty ? form.fsecunitid : '',
      fsecqty: form.enableAuxQty ? form.fsecqty : 0,
      fnote: form.fnote,
    })
    ElMessage.success(`已生成 ${barcodeCount.value} 个条码`)
    await loadBarcodes()
  } catch (e: any) {
    ElMessage.error(e?.response?.data?.message || '条码生成失败')
  } finally {
    generating.value = false
  }
}

async function handleVoid() {
  const codes = selectedRows.value.filter(r => r.fbarcodestatus === BC_INIT).map(r => r.fbarcode)
  if (codes.length === 0) { ElMessage.warning('仅“初始”状态的条码可作废'); return }
  const ok = await ElMessageBox.confirm(`确认作废选中的 ${codes.length} 个条码？`, '条码作废', { type: 'warning' }).then(() => true).catch(() => false)
  if (!ok) return
  try {
    const res: any = await voidBarcodes(codes)
    if (res.data > 0) { ElMessage.success(`已作废 ${res.data} 个条码`); await loadBarcodes() }
    else { ElMessage.warning('没有可作废的条码（可能状态已变更），请刷新后重试'); await loadBarcodes() }
  } catch (e: any) { ElMessage.error(e?.response?.data?.message || '作废失败') }
}

async function handleUnvoid() {
  const codes = selectedRows.value.filter(r => r.fbarcodestatus === BC_VOID).map(r => r.fbarcode)
  if (codes.length === 0) { ElMessage.warning('仅“作废”状态的条码可反作废'); return }
  try {
    const res: any = await unvoidBarcodes(codes)
    if (res.data > 0) { ElMessage.success(`已反作废 ${res.data} 个条码`); await loadBarcodes() }
    else { ElMessage.warning('没有可反作废的条码（可能状态已变更），请刷新后重试'); await loadBarcodes() }
  } catch (e: any) { ElMessage.error(e?.response?.data?.message || '反作废失败') }
}

// 用所选模板渲染选中条码：mode=print 走系统打印对话框（可另存PDF/选打印机），mode=pdf 直接生成PDF文件
async function renderLabels(mode: 'print' | 'pdf') {
  const rows = selectedRows.value.filter(r => r.fbarcodestatus !== BC_VOID)
  if (rows.length === 0) { ElMessage.warning('作废的条码不可打印'); return }
  const tpl = currentTemplate.value
  if (!tpl || !tpl.fTemplate) { ElMessage.warning('请先选择标签模板（可在“标签模板设计”中创建并设为默认）'); return }
  let json: any
  try { json = JSON.parse(tpl.fTemplate) } catch { ElMessage.error('模板内容损坏，无法打印'); return }
  try {
    const mod = await loadHiprint()
    const ht = new mod.hiprint.PrintTemplate({ template: json })
    if (mode === 'pdf') {
      ht.toPdf(rows, head.value.fbillno || '标签')
    } else {
      ht.print(rows, { leftOffset: -1, topOffset: -1 })
    }
    // 渲染成功后回写“已打印”状态（沿用后端 print 端点记录打印日期）
    const codes = rows.map(r => r.fbarcode)
    const res: any = await printBarcodes(codes)
    if (res.data > 0) ElMessage.success(`已打印 ${res.data} 个条码`)
    await loadBarcodes()
  } catch (e: any) {
    ElMessage.error(e?.response?.data?.message || '打印失败')
  }
}
const handlePrint = () => renderLabels('print')
const handleExportPdf = () => renderLabels('pdf')

const goBack = () => router.push({ name: 'ReceiveNoticeLabelList' })

onMounted(() => { loadHead(); loadTemplates() })
</script>

<style scoped>
.rn-label-generate {
  padding: 16px 20px;
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
}
.edit-toolbar {
  display: flex;
  align-items: center;
  gap: 8px;
  margin-bottom: 16px;
  padding-bottom: 12px;
  border-bottom: 1px solid var(--el-border-color-lighter);
}
.toolbar-sep {
  width: 1px;
  height: 20px;
  background: var(--el-border-color);
  margin: 0 4px;
}
.head-form { margin-top: 4px; }
.inline-label { margin: 0 8px 0 16px; color: var(--el-text-color-regular); }
</style>
