<template>
  <div class="tpl-edit">
    <div class="toolbar">
      <el-button type="primary" :loading="saving" @click="handleSave" v-permission="isEdit ? 'labeltemplate:edit' : 'labeltemplate:add'">
        <el-icon><Check /></el-icon> 保存
      </el-button>
      <el-button @click="applyPaper">应用纸张尺寸</el-button>
      <el-button @click="clearDesign">清空画布</el-button>
      <el-button @click="doPreview">预览打印</el-button>
      <div class="spacer"></div>
      <el-button @click="goBack">退出</el-button>
    </div>

    <el-form ref="formRef" :model="form" :rules="rules" inline class="head-form">
      <el-form-item label="模板编码" prop="fNumber">
        <el-input v-model="form.fNumber" :disabled="isEdit" placeholder="如 LBL001" style="width: 150px" />
      </el-form-item>
      <el-form-item label="模板名称" prop="fName">
        <el-input v-model="form.fName" placeholder="模板名称" style="width: 180px" />
      </el-form-item>
      <el-form-item label="适用单据" prop="fBillSource">
        <el-select v-model="form.fBillSource" style="width: 170px">
          <el-option v-for="s in billSources" :key="s.value" :label="s.label" :value="s.value" />
        </el-select>
      </el-form-item>
      <el-form-item label="纸张(mm)">
        <el-input-number v-model="form.fPaperWidth" :min="1" :precision="1" controls-position="right" style="width: 110px" />
        <span style="margin: 0 6px">×</span>
        <el-input-number v-model="form.fPaperHeight" :min="1" :precision="1" controls-position="right" style="width: 110px" />
      </el-form-item>
      <el-form-item label="默认">
        <el-switch v-model="form.fIsDefault" />
      </el-form-item>
      <el-form-item label="备注">
        <el-input v-model="form.fDescription" placeholder="备注" style="width: 180px" />
      </el-form-item>
    </el-form>

    <div class="designer">
      <div class="left-panel">
        <el-divider content-position="left">标签字段（拖入画布即自动绑定）</el-divider>
        <div id="label-field-provider" class="hiprint-provider"></div>
        <el-divider content-position="left">通用元素</el-divider>
        <div id="default-provider" class="hiprint-provider"></div>
      </div>
      <div class="center-panel">
        <div id="hiprint-printTemplate" class="hiprint-canvas"></div>
      </div>
      <div class="right-panel">
        <div id="PrintElementOptionSetting"></div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted, nextTick } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { ElMessage } from 'element-plus'
import { Check } from '@element-plus/icons-vue'
import { getPrintTemplate, createPrintTemplate, updatePrintTemplate } from '../../api/labelTemplate'

const route = useRoute()
const router = useRouter()
const uid = String(route.query.uid || '')
const isEdit = computed(() => !!uid)

const formRef = ref()
const saving = ref(false)

const form = reactive({
  fNumber: '',
  fName: '',
  fBillSource: 'PUR_PurchaseOrder',
  fPaperWidth: 60,
  fPaperHeight: 40,
  fIsDefault: false,
  fDescription: ''
})

const rules = {
  fNumber: [{ required: true, message: '请输入模板编码', trigger: 'blur' }],
  fName: [{ required: true, message: '请输入模板名称', trigger: 'blur' }],
  fBillSource: [{ required: true, message: '请选择适用单据', trigger: 'change' }]
}

const billSources = [
  { value: 'PUR_PurchaseOrder', label: '采购订单标签' },
  { value: 'PUR_ReceiveBill', label: '收料通知单标签' }
]

// 可绑定数据字段（来自 BarcodeLine，两单据共用；fbarcode 额外生成条码/二维码元素）
const labelFields: Array<{ field: string; title: string; barcode?: boolean }> = [
  { field: 'fbarcode', title: '条码', barcode: true },
  { field: 'fmaterialNumber', title: '物料代码' },
  { field: 'fmaterialName', title: '物料名称' },
  { field: 'fSpecification', title: '规格型号' },
  { field: 'flot', title: '批次' },
  { field: 'fqty', title: '数量' },
  { field: 'funitName', title: '单位' },
  { field: 'fbaseqty', title: '基本单位数量' },
  { field: 'fbaseunitName', title: '基本单位' },
  { field: 'fauxpropName', title: '辅助属性' },
  { field: 'fcompanyName', title: '组织' },
  { field: 'fkfdate', title: '采购/生产日期' },
  { field: 'fusefuldate', title: '有效期至' },
  { field: 'fkfperiod', title: '保质期限' },
  { field: 'fpobillno', title: '采购订单编号' },
  { field: 'fpurbillno', title: '收料单编号' },
  { field: 'finspectName', title: 'IQC检验员' },
  { field: 'finspectdate', title: 'IQC检验日期' },
  { field: 'fbarcodestatusName', title: '条码状态' },
  { field: 'fstockstatusName', title: '库存状态' }
]

let hiprint: any = null
let hiprintTemplate: any = null
let templateJson: any = {}

// 自定义元素 provider：把业务字段做成可拖拽元素，拖入即带 field 绑定
function buildFieldProvider() {
  return {
    addElementTypes(context: any) {
      context.removePrintElementTypes('labelFieldModule')
      const eles: any[] = []
      for (const f of labelFields) {
        if (f.barcode) {
          eles.push({
            tid: `labelFieldModule.${f.field}_bar`, title: f.title + '(条码)', data: '123456789012', type: 'text',
            options: { field: f.field, testData: '123456789012', textType: 'barcode', height: 30, width: 130, fontSize: 10 }
          })
          eles.push({
            tid: `labelFieldModule.${f.field}_qr`, title: f.title + '(二维码)', data: '123456789012', type: 'text',
            options: { field: f.field, testData: '123456789012', textType: 'qrcode', height: 60, width: 60 }
          })
        } else {
          eles.push({
            tid: `labelFieldModule.${f.field}`, title: f.title, data: f.title, type: 'text',
            options: { field: f.field, testData: f.title, height: 16, fontSize: 12 }
          })
        }
      }
      context.addPrintElementTypes('labelFieldModule', [new hiprint.PrintElementTypeGroup('标签字段', eles)])
    }
  }
}

async function initHiprint() {
  const jq = (await import('jquery')).default
  ;(window as any).jQuery = (window as any).$ = jq
  const mod: any = await import('vue-plugin-hiprint')
  hiprint = mod.hiprint
  // 没装 electron-hiprint 客户端时关闭自动连，避免控制台反复报错
  try { mod.hiPrintPlugin?.disAutoConnect?.() } catch { /* ignore */ }
  hiprint.init({ providers: [new mod.defaultElementTypeProvider(), buildFieldProvider()] })
  await nextTick()
  const $ = (window as any).$
  $('#label-field-provider').empty()
  hiprint.PrintElementTypeManager.build($('#label-field-provider'), 'labelFieldModule')
  $('#default-provider').empty()
  hiprint.PrintElementTypeManager.build($('#default-provider'), 'defaultModule')
}

function buildDesigner(json: any) {
  const $ = (window as any).$
  $('#hiprint-printTemplate').empty()
  hiprintTemplate = new hiprint.PrintTemplate({ template: json || {}, settingContainer: '#PrintElementOptionSetting' })
  hiprintTemplate.design('#hiprint-printTemplate')
  applyPaper()
}

function applyPaper() {
  if (hiprintTemplate && form.fPaperWidth && form.fPaperHeight) {
    try { hiprintTemplate.setPaper(form.fPaperWidth, form.fPaperHeight) } catch { /* ignore */ }
  }
}

function clearDesign() {
  if (hiprintTemplate) {
    try { hiprintTemplate.clear() } catch { buildDesigner({}) }
  }
}

function doPreview() {
  if (!hiprintTemplate) return
  // 用每个字段的 testData 占位预览打印
  hiprintTemplate.print({}, { leftOffset: -1, topOffset: -1 })
}

async function loadDetail() {
  const res: any = await getPrintTemplate(uid)
  const d = res.data
  if (!d) return
  form.fNumber = d.fNumber || ''
  form.fName = d.fName || ''
  form.fBillSource = d.fBillSource || 'PUR_PurchaseOrder'
  form.fPaperWidth = d.fPaperWidth || 60
  form.fPaperHeight = d.fPaperHeight || 40
  form.fIsDefault = !!d.fIsDefault
  form.fDescription = d.fDescription || ''
  if (d.fTemplate) {
    try { templateJson = JSON.parse(d.fTemplate) } catch { templateJson = {} }
  }
}

async function handleSave() {
  try {
    await formRef.value.validate()
  } catch { return }
  if (!hiprintTemplate) { ElMessage.warning('设计器尚未就绪，请稍候'); return }
  saving.value = true
  try {
    const json = hiprintTemplate.getJson()
    const payload = { ...form, fTemplate: JSON.stringify(json) }
    if (isEdit.value) {
      await updatePrintTemplate(uid, payload)
      ElMessage.success('保存成功')
    } else {
      const res: any = await createPrintTemplate(payload)
      ElMessage.success('创建成功')
      const newUid = res.data?.uid
      if (newUid) router.replace({ name: 'LabelTemplateEdit', query: { uid: newUid } })
    }
  } catch (e: any) {
    ElMessage.error(e?.response?.data?.message || '保存失败')
  } finally {
    saving.value = false
  }
}

const goBack = () => router.push({ name: 'LabelTemplateList' })

onMounted(async () => {
  if (uid) await loadDetail()
  await initHiprint()
  buildDesigner(templateJson)
})
</script>

<style scoped>
.tpl-edit {
  padding: 16px;
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
  display: flex;
  flex-direction: column;
  height: calc(100vh - 120px);
}

.toolbar {
  display: flex;
  align-items: center;
  gap: 8px;
  margin-bottom: 10px;
}

.toolbar .spacer {
  flex: 1;
}

.head-form {
  margin-bottom: 8px;
}

.head-form :deep(.el-form-item) {
  margin-bottom: 8px;
}

.designer {
  flex: 1;
  display: flex;
  gap: 8px;
  min-height: 0;
}

.left-panel {
  width: 230px;
  overflow: auto;
  border: 1px solid var(--el-border-color-light);
  border-radius: 4px;
  padding: 6px;
}

.center-panel {
  flex: 1;
  overflow: auto;
  background: #f5f5f5;
  border: 1px solid var(--el-border-color-light);
  border-radius: 4px;
  padding: 16px;
}

.right-panel {
  width: 260px;
  overflow: auto;
  border: 1px solid var(--el-border-color-light);
  border-radius: 4px;
  padding: 6px;
}

.hiprint-canvas {
  background: #fff;
}
</style>
