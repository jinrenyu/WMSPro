<template>
  <div class="rule-edit-container">
    <div class="edit-toolbar">
      <el-button type="primary" :loading="saving" @click="handleSave" v-permission="'billcoderule:edit'">
        <el-icon><Check /></el-icon> 保存
      </el-button>
      <el-button type="warning" plain @click="handleResetSeq" v-permission="'billcoderule:edit'">
        <el-icon><RefreshLeft /></el-icon> 重置流水
      </el-button>
      <div class="toolbar-spacer" />
      <span class="toolbar-title">编码规则配置 —— {{ detail.formName || formKey }}</span>
      <el-button class="back-btn" @click="goBack"><el-icon><Back /></el-icon> 返回</el-button>
    </div>

    <div class="edit-body">
      <el-tabs v-model="activeTab" v-loading="loading">
      <el-tab-pane v-for="t in tabs" :key="t.key" :label="t.label" :name="t.key">
        <el-form label-width="110px" class="sec-head">
          <el-row :gutter="16">
            <el-col :span="7">
              <el-form-item label="规则名称">
                <el-input v-model="secOf(t.key).fname" maxlength="200" />
              </el-form-item>
            </el-col>
            <el-col :span="5" v-if="t.key === 'bill'">
              <el-form-item label="允许手工改号">
                <el-switch v-model="secOf(t.key).ismodify" />
              </el-form-item>
            </el-col>
            <el-col :span="4">
              <el-form-item label="禁用此规则">
                <el-switch v-model="secOf(t.key).disabled" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="描述">
                <el-input v-model="secOf(t.key).fnote" maxlength="255" />
              </el-form-item>
            </el-col>
          </el-row>
        </el-form>

        <div class="grid-toolbar">
          <el-button size="small" type="primary" plain @click="addSeg(t.key)">新增分段</el-button>
          <span class="grid-tip">「拼入编号」决定该段是否出现在最终编号；「依据」的段值变化时流水重新计数：日期段勾依据 = 按日/按月重置，字段段勾依据 = 按字段值分组取号</span>
        </div>
        <div v-if="fieldSegHint(t.key)" class="grid-warn">提示：{{ fieldSegHint(t.key) }}</div>

        <el-table :data="secOf(t.key).segments" border size="small" row-key="_key">
          <el-table-column type="index" label="#" width="44" align="center" />
          <el-table-column label="段类型" width="118">
            <template #default="scope">
              <el-select v-model="scope.row.ftype" @change="onTypeChange(scope.row)">
                <el-option label="常量" value="1" />
                <el-option label="文本字段" value="2" />
                <el-option label="日期字段" value="3" />
                <el-option label="流水号" value="4" />
              </el-select>
            </template>
          </el-table-column>
          <el-table-column label="来源字段" width="140">
            <template #default="scope">
              <el-select
                v-if="scope.row.ftype === '2' || scope.row.ftype === '3'"
                v-model="scope.row.fieldid"
                clearable
                placeholder="选择字段"
                @change="(v: string) => onFieldChange(scope.row, t.key, v)"
              >
                <el-option v-for="f in fieldOptions(t.key, scope.row.ftype)" :key="f.fieldId" :label="f.fieldName" :value="f.fieldId" />
              </el-select>
              <span v-else class="muted">—</span>
            </template>
          </el-table-column>
          <el-table-column label="设置值 / 格式" min-width="160">
            <template #default="scope">
              <el-input v-if="scope.row.ftype === '1'" v-model="scope.row.fvalue" placeholder="固定文本" />
              <el-select
                v-else-if="scope.row.ftype === '3'"
                v-model="scope.row.formatString"
                filterable
                allow-create
                default-first-option
                placeholder="yyyyMMdd"
              >
                <el-option v-for="f in dateFormats" :key="f" :label="f" :value="f" />
              </el-select>
              <el-select v-else-if="scope.row.ftype === '2'" v-model="scope.row.formatString" clearable placeholder="原样">
                <el-option label="转大写 (U)" value="U" />
                <el-option label="转小写 (L)" value="L" />
              </el-select>
              <span v-else class="muted">自动流水</span>
            </template>
          </el-table-column>
          <el-table-column label="长度" width="100">
            <template #default="scope">
              <el-input-number
                v-if="scope.row.ftype !== '1' && scope.row.ftype !== '3'"
                v-model="scope.row.flenght"
                :min="0"
                :max="scope.row.ftype === '4' ? 9 : 50"
                value-on-clear="min"
                controls-position="right"
                style="width: 100%"
              />
              <span v-else class="muted">—</span>
            </template>
          </el-table-column>
          <el-table-column label="补位" width="130">
            <template #default="scope">
              <div v-if="scope.row.ftype === '2'" class="pad-cell">
                <el-input v-model="scope.row.fchar" maxlength="1" placeholder="符" class="pad-char" />
                <el-select v-model="scope.row.fcharAlignment" clearable placeholder="向" class="pad-align">
                  <el-option label="左补" value="L" />
                  <el-option label="右补" value="R" />
                </el-select>
              </div>
              <span v-else class="muted">{{ scope.row.ftype === '4' ? '左补 0' : '—' }}</span>
            </template>
          </el-table-column>
          <el-table-column label="起始" width="92">
            <template #default="scope">
              <el-input-number v-if="scope.row.ftype === '4'" v-model="scope.row.fmin" :min="1" :max="999999999" value-on-clear="min" controls-position="right" style="width: 100%" />
              <span v-else class="muted">—</span>
            </template>
          </el-table-column>
          <el-table-column label="步长" width="92">
            <template #default="scope">
              <el-input-number v-if="scope.row.ftype === '4'" v-model="scope.row.fstep" :min="1" :max="1000" value-on-clear="min" controls-position="right" style="width: 100%" />
              <span v-else class="muted">—</span>
            </template>
          </el-table-column>
          <el-table-column label="最大(0不限)" width="110">
            <template #default="scope">
              <el-input-number v-if="scope.row.ftype === '4'" v-model="scope.row.fmax" :min="0" :max="999999999" value-on-clear="min" controls-position="right" style="width: 100%" />
              <span v-else class="muted">—</span>
            </template>
          </el-table-column>
          <el-table-column label="拼入编号" width="76" align="center">
            <template #default="scope">
              <el-checkbox v-model="scope.row.isMember" :disabled="scope.row.ftype === '4'" />
            </template>
          </el-table-column>
          <el-table-column label="依据" width="58" align="center">
            <template #default="scope">
              <el-checkbox v-model="scope.row.isSerial" :disabled="scope.row.ftype === '4'" />
            </template>
          </el-table-column>
          <el-table-column label="操作" width="160" fixed="right">
            <template #default="scope">
              <div class="op-btns">
                <el-button size="small" text :disabled="scope.$index === 0" @click="moveSeg(t.key, scope.$index, -1)">上移</el-button>
                <el-button size="small" text :disabled="scope.$index === secOf(t.key).segments.length - 1" @click="moveSeg(t.key, scope.$index, 1)">下移</el-button>
                <el-button size="small" text type="danger" @click="secOf(t.key).segments.splice(scope.$index, 1)">删除</el-button>
              </div>
            </template>
          </el-table-column>
        </el-table>

        <div class="preview-bar">
          <span class="preview-label">格式预览：</span>
          <span v-if="previewOf(t.key)" class="preview-no">{{ previewOf(t.key) }}</span>
          <span v-else class="muted">（未配置分段，生成编号时将提示先配置规则）</span>
          <span v-if="previewOf(t.key)" class="preview-len" :class="{ over: byteLen(previewOf(t.key)) > 50 }">
            约 {{ byteLen(previewOf(t.key)) }} 字节/50
          </span>
        </div>
        <div v-if="dateNoBasisWarn(t.key)" class="preview-warn">⚠ {{ dateNoBasisWarn(t.key) }}</div>
        <div class="preview-bar server">
          <span class="preview-label">下一个实际编号：</span>
          <template v-if="serverPreviewOf(t.key)?.configured && !serverPreviewOf(t.key)?.message">
            <span class="preview-no">{{ serverPreviewOf(t.key)?.nextNo }}</span>
            <span class="preview-len">
              当前流水 {{ serverPreviewOf(t.key)?.currentSeq }} · {{ serverPreviewOf(t.key)?.byteLength }}/50 字节
              <span v-if="serverPreviewOf(t.key)?.disabled" class="warn-text"> · 规则已禁用</span>
            </span>
          </template>
          <span v-else class="muted">{{ serverPreviewOf(t.key)?.message || '（按已保存规则计算，保存后可见）' }}</span>
          <el-button size="small" text type="primary" @click="loadPreview">刷新试取号</el-button>
        </div>
      </el-tab-pane>
      </el-tabs>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import {
  getBillCodeRuleDetail, saveBillCodeRule, getBillCodeRulePreview, resetBillCodeSeq,
  type BillCodeField, type BillCodeRuleDetail, type BillCodeRuleSection, type BillCodeSegment,
  type BillCodePreview
} from '../../api/billCodeRule'

const route = useRoute()
const router = useRouter()
const formKey = (route.query.formKey as string) || ''

const tabs = [
  { key: 'bill', label: '单据编号规则' },
  { key: 'barcode', label: '条码编号规则' }
] as const
type TabKey = typeof tabs[number]['key']

const dateFormats = ['yyyyMMdd', 'yyMMdd', 'yyyyMM', 'yyMM', 'yyyy']

const emptySection = (): BillCodeRuleSection => ({ fname: '', ismodify: true, disabled: false, fnote: '', segments: [] })
const detail = reactive<BillCodeRuleDetail>({
  formKey: '', formName: '', bill: emptySection(), barcode: emptySection(), billFields: [], barcodeFields: []
})
const activeTab = ref<TabKey>('bill')
const loading = ref(false)
const saving = ref(false)
const serverPreview = reactive<{ bill?: BillCodePreview; barcode?: BillCodePreview }>({})

const secOf = (k: TabKey): BillCodeRuleSection => (k === 'bill' ? detail.bill : detail.barcode)
const serverPreviewOf = (k: TabKey): BillCodePreview | undefined => (k === 'bill' ? serverPreview.bill : serverPreview.barcode)
const fieldsOf = (k: TabKey): BillCodeField[] => (k === 'bill' ? detail.billFields : detail.barcodeFields)
const fieldOptions = (k: TabKey, ftype: string) =>
  fieldsOf(k).filter(f => (ftype === '3' ? f.kind === 'date' : f.kind === 'text'))

const loadDetail = async () => {
  if (!formKey) {
    ElMessage.warning('缺少表单标识，已返回列表')
    router.replace('/system/code-rules')
    return
  }
  loading.value = true
  try {
    const res: any = await getBillCodeRuleDetail(formKey)
    const d = res?.data
    if (d) {
      detail.formKey = d.formKey
      detail.formName = d.formName
      detail.bill = { ...emptySection(), ...d.bill, segments: d.bill?.segments || [] }
      detail.barcode = { ...emptySection(), ...d.barcode, segments: d.barcode?.segments || [] }
      // 旧"数值字段(5)"已并入文本字段(2)，载入时统一
      for (const s of [...detail.bill.segments, ...detail.barcode.segments]) if (s.ftype === '5') s.ftype = '2'
      detail.billFields = d.billFields || []
      detail.barcodeFields = d.barcodeFields || []
    }
  } finally {
    loading.value = false
  }
}

// 服务端试取号（按已保存规则读当前流水算下一个号，不占号；反映的是上次保存的规则）
const loadPreview = async () => {
  if (!formKey) return
  try {
    const res: any = await getBillCodeRulePreview(formKey)
    const d = res?.data
    serverPreview.bill = d?.bill
    serverPreview.barcode = d?.barcode
  } catch {
    // 预览失败不阻断配置
  }
}

const newSegment = (): BillCodeSegment => ({
  ftype: '1', fieldid: '', fieldName: '', fvalue: '',
  flenght: 0, fmin: 0, fmax: 0, fstep: 0,
  formatString: '', fchar: '', fcharAlignment: '',
  isSerial: false, isMember: true, fnote: ''
})

const addSeg = (k: TabKey) => {
  secOf(k).segments.push(newSegment())
}

const moveSeg = (k: TabKey, idx: number, dir: number) => {
  const segs = secOf(k).segments
  const to = idx + dir
  if (to < 0 || to >= segs.length) return
  const [row] = segs.splice(idx, 1)
  segs.splice(to, 0, row)
}

// 切换段类型时清掉不相关属性，流水段给常用默认值
const onTypeChange = (row: BillCodeSegment) => {
  row.fieldid = ''
  row.fieldName = ''
  row.fvalue = ''
  row.fchar = ''
  row.fcharAlignment = ''
  row.formatString = row.ftype === '3' ? 'yyyyMMdd' : ''
  if (row.ftype === '4') {
    row.flenght = row.flenght > 0 ? row.flenght : 4
    row.fmin = 1
    row.fstep = 1
    row.fmax = 0
    row.isSerial = false
    row.isMember = true // 流水段恒拼入编号
  } else {
    row.flenght = 0
    row.fmin = 0
    row.fmax = 0
    row.fstep = 0
  }
}

const onFieldChange = (row: BillCodeSegment, k: TabKey, v: string) => {
  row.fieldName = fieldsOf(k).find(f => f.fieldId === v)?.fieldName || ''
}

// ===== 实时预览（与后端取号引擎同口径）=====

const pad2 = (n: number) => String(n).padStart(2, '0')
const fmtNow = (fmt: string) => {
  const d = new Date()
  return (fmt || 'yyyyMMdd')
    .replace(/yyyy/g, String(d.getFullYear()))
    .replace(/yy/g, String(d.getFullYear()).slice(-2))
    .replace(/MM/g, pad2(d.getMonth() + 1))
    .replace(/dd/g, pad2(d.getDate()))
    .replace(/HH/g, pad2(d.getHours()))
    .replace(/mm/g, pad2(d.getMinutes()))
    .replace(/ss/g, pad2(d.getSeconds()))
}

// 与后端 DbByteLen 同口径：逐 UTF-16 码元，非 ASCII（CJK 等）按 2 字节计
const byteLen = (s: string): number => {
  let n = 0
  for (let i = 0; i < s.length; i++) n += s.charCodeAt(i) <= 0x7F ? 1 : 2
  return n
}

const previewOf = (k: TabKey): string => {
  const segs = secOf(k).segments.filter(s => s.isMember !== false)
  if (segs.length === 0) return ''
  return segs.map(s => {
    if (s.ftype === '1') return s.fvalue || ''
    if (s.ftype === '3') return fmtNow(s.formatString)
    if (s.ftype === '4') return String(s.fmin || 1).padStart(Math.max(s.flenght, 1), '0')
    let v = fieldsOf(k).find(f => f.fieldId === s.fieldid)?.sample || 'XXXX'
    if (s.formatString === 'U') v = v.toUpperCase()
    if (s.formatString === 'L') v = v.toLowerCase()
    if (s.flenght > 0) {
      // 与后端 BuildSample 同口径：无补位符也按声明长度用 'X' 占位补满（最坏长度估计），
      // 否则前端 50 位校验放行、后端保存时拒绝，预览与服务端结论矛盾
      if (v.length > s.flenght) v = v.slice(0, s.flenght)
      else v = s.fcharAlignment === 'L' ? v.padStart(s.flenght, s.fchar || 'X') : v.padEnd(s.flenght, s.fchar || 'X')
    }
    return v
  }).join('')
}

// 日期已拼入编号但无任何依据 → 跨日不重置，可能与历史编号重复
const dateNoBasisWarn = (k: TabKey): string => {
  const segs = secOf(k).segments
  const hasDateMember = segs.some(s => s.ftype === '3' && s.isMember !== false)
  const hasBasis = segs.some(s => s.isSerial)
  if (hasDateMember && !hasBasis)
    return '日期已拼入编号但未勾任何「依据」，流水不会按日期重置，可能与历史编号重复；建议勾选日期段的「依据」实现按日重置'
  return ''
}

// 字段段所选字段会成为建单时的必填项（缺值无法取号）
const fieldSegHint = (k: TabKey): string => {
  const names = secOf(k).segments
    .filter(s => s.ftype === '2' && s.fieldid)
    .map(s => fieldsOf(k).find(f => f.fieldId === s.fieldid)?.fieldName || s.fieldid)
  return names.length ? `字段段【${names.join('、')}】将成为建单时的必填项，缺值会导致无法取号` : ''
}

// ===== 保存 =====

const validateSection = (label: string, k: TabKey): string | null => {
  const segs = secOf(k).segments
  if (segs.length === 0) return null // 允许清空 = 未配置
  const serialCount = segs.filter(s => s.ftype === '4').length
  if (serialCount !== 1) return `${label}：必须且只能配置一个流水号段（当前 ${serialCount} 个）`
  for (let i = 0; i < segs.length; i++) {
    const s = segs[i]
    if (s.ftype === '1' && !s.fvalue.trim()) return `${label}第 ${i + 1} 段：常量段必须填写设置值`
    if (s.ftype === '2' && !s.fieldid) return `${label}第 ${i + 1} 段：请选择来源字段`
    if (s.ftype !== '4' && s.isMember === false && !s.isSerial) return `${label}第 ${i + 1} 段：既未勾「拼入编号」也未勾「依据」，该段无意义`
  }
  const sample = previewOf(k)
  if (byteLen(sample) > 50) return `${label}：编号预览约 ${byteLen(sample)} 字节，超过 50 字节上限（CJK 字符按 2 字节计）`
  return null
}

const handleSave = async () => {
  if (!formKey) {
    ElMessage.warning('缺少表单标识')
    return
  }
  for (const t of tabs) {
    const err = validateSection(t.label, t.key)
    if (err) {
      activeTab.value = t.key
      ElMessage.warning(err)
      return
    }
  }
  saving.value = true
  try {
    await saveBillCodeRule(formKey, { bill: detail.bill, barcode: detail.barcode })
    ElMessage.success('编码规则已保存')
    await loadDetail()
    await loadPreview()
  } catch {
    // 全局拦截器已统一提示错误，无需重复
  } finally {
    saving.value = false
  }
}

const handleResetSeq = async () => {
  if (!formKey) return
  try {
    await ElMessageBox.confirm(
      '重置后该单据的编号/条码流水将从起始号重新计数，可能与历史已生成的编号重复。仅建议测试期使用，确定重置？',
      '重置流水',
      { type: 'warning', confirmButtonText: '重置', cancelButtonText: '取消' }
    )
  } catch {
    return
  }
  try {
    await resetBillCodeSeq(formKey)
    ElMessage.success('编码流水已重置')
    await loadPreview()
  } catch {
    // 全局拦截器已统一提示错误，无需重复
  }
}

const goBack = () => router.push('/system/code-rules')

onMounted(async () => {
  await loadDetail()
  await loadPreview()
})
</script>

<style scoped>
.rule-edit-container {
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
  display: flex;
  flex-direction: column;
}
.edit-toolbar {
  display: flex;
  align-items: center;
  gap: 8px;
  padding: 12px 16px;
  border-bottom: 1px solid var(--border-color, #ebeef5);
}
.toolbar-spacer {
  flex: 1;
}
.toolbar-title {
  font-size: 15px;
  font-weight: 600;
  color: var(--el-text-color-primary);
  margin-right: 8px;
}
.back-btn {
  margin-left: 8px;
}
.edit-body {
  padding: 16px 20px 24px;
}
.sec-head {
  margin-top: 4px;
}
.grid-toolbar {
  display: flex;
  align-items: center;
  gap: 12px;
  margin-bottom: 8px;
}
.grid-tip {
  color: #909399;
  font-size: 12px;
}
.op-btns {
  display: flex;
  align-items: center;
  flex-wrap: nowrap;
  gap: 4px;
}
.op-btns :deep(.el-button) {
  margin-left: 0;
}
.muted {
  color: #c0c4cc;
}
.pad-cell {
  display: flex;
  gap: 4px;
}
.pad-char {
  width: 44px;
}
.pad-align {
  flex: 1;
}
.preview-bar {
  margin-top: 10px;
  padding: 10px 12px;
  background: #f5f7fa;
  border-radius: 4px;
  display: flex;
  align-items: center;
  gap: 8px;
}
.preview-label {
  color: #606266;
}
.preview-no {
  font-family: Consolas, Monaco, monospace;
  font-size: 15px;
  font-weight: 600;
  color: #409eff;
}
.preview-len {
  color: #909399;
  font-size: 12px;
}
.preview-len.over {
  color: #f56c6c;
  font-weight: 600;
}
.preview-bar.server {
  margin-top: 8px;
  background: #ecf5ff;
}
.preview-warn {
  margin-top: 6px;
  padding: 6px 12px;
  background: #fdf6ec;
  color: #e6a23c;
  border-radius: 4px;
  font-size: 12px;
}
.grid-warn {
  margin-bottom: 8px;
  color: #e6a23c;
  font-size: 12px;
}
.warn-text {
  color: #f56c6c;
}
</style>
