<template>
  <div class="mo-edit-container">
    <!-- 顶部工具栏 -->
    <div class="edit-toolbar">
      <el-button type="primary" :disabled="isReadonly" @click="handleSave"
                 v-permission="isEdit ? 'productionorder:edit' : 'productionorder:add'">
        <el-icon><Check /></el-icon> 保存
      </el-button>
      <el-button v-if="isEdit && form.fStatus !== 40" type="success" @click="handleApprove" v-permission="'productionorder:approve'">
        <el-icon><CircleCheck /></el-icon> 审核
      </el-button>
      <el-button v-if="isEdit && form.fStatus === 40" type="warning" @click="handleUnapprove" v-permission="'productionorder:approve'">
        <el-icon><RefreshLeft /></el-icon> 反审核
      </el-button>
      <div class="toolbar-spacer" />
      <el-tag v-if="isEdit" :type="form.fStatus === 40 ? 'success' : 'warning'" size="large">
        {{ form.fStatus === 40 ? '已审核' : '未审核' }}
      </el-tag>
      <el-button class="back-btn" @click="handleBack"><el-icon><Back /></el-icon> 退出</el-button>
    </div>

    <el-form ref="formRef" :model="form" :rules="rules" label-width="84px"
             :disabled="isReadonly" class="edit-form" v-loading="loading">
      <!-- 单据头 -->
      <div class="form-header">
        <el-row :gutter="16">
          <el-col :span="8">
            <el-form-item label="单据类型" prop="fbilltype">
              <LookupSelect v-model="form.fbilltype" module="billtype" parent-id="PRD_MO" placeholder="请选择单据类型" preload />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="单据编号">
              <el-input v-model="form.fbillno" :disabled="isEdit" placeholder="保存后自动生成" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="单据日期" prop="fdate">
              <el-date-picker v-model="form.fdate" type="date" value-format="YYYY-MM-DD" placeholder="选择日期" style="width:100%" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="16">
          <el-col :span="8">
            <el-form-item label="审批状态">
              <el-input :model-value="form.foastatus" disabled placeholder="—" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="审批结果">
              <el-input :model-value="form.foaresult" disabled placeholder="—" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="组织">
              <el-select v-model="form.fcompanyid" placeholder="组织" style="width:100%">
                <el-option v-for="o in orgOptions" :key="o.orgId" :label="o.orgName" :value="o.orgId" />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="16">
          <el-col :span="8">
            <el-form-item label="计划员">
              <LookupSelect v-model="form.fplannerid" module="employee" placeholder="请选择计划员" preload />
            </el-form-item>
          </el-col>
          <el-col :span="16">
            <el-form-item label="备注">
              <el-input v-model="form.fnote" placeholder="备注" />
            </el-form-item>
          </el-col>
        </el-row>
      </div>

      <el-tabs v-model="activeTab" class="edit-tabs">
        <el-tab-pane label="其他" name="other">
          <el-row :gutter="16">
            <el-col :span="8"><el-form-item label="制单人"><el-input :model-value="form.cuserName" disabled /></el-form-item></el-col>
            <el-col :span="8"><el-form-item label="制单日期"><el-input :model-value="fmtAuditDate(form.cYmd)" disabled /></el-form-item></el-col>
            <el-col :span="8"><el-form-item label="数据状态"><el-input :model-value="form.fstatusName" disabled /></el-form-item></el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="8"><el-form-item label="审核人"><el-input :model-value="form.fcheckerName" disabled /></el-form-item></el-col>
            <el-col :span="8"><el-form-item label="审核日期"><el-input :model-value="fmtAuditDate(form.fcheckdate)" disabled /></el-form-item></el-col>
            <el-col :span="8"></el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="8"><el-form-item label="修改人"><el-input :model-value="form.muserName" disabled /></el-form-item></el-col>
            <el-col :span="8"><el-form-item label="修改日期"><el-input :model-value="fmtAuditDate(form.mYmd)" disabled /></el-form-item></el-col>
            <el-col :span="8"></el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="8"><el-form-item label="禁用人"><el-input :model-value="form.fdisableName" disabled /></el-form-item></el-col>
            <el-col :span="8"><el-form-item label="禁用日期"><el-input :model-value="fmtAuditDate(form.fdisabledate)" disabled /></el-form-item></el-col>
            <el-col :span="8"></el-col>
          </el-row>
        </el-tab-pane>
      </el-tabs>

      <!-- 任务订单明细 -->
      <div class="grid-section">
        <div class="grid-toolbar">
          <span class="grid-title">任务订单明细</span>
          <el-button type="primary" size="small" :disabled="isReadonly" @click="addLine"><el-icon><Plus /></el-icon> 行新增</el-button>
          <el-button size="small" :disabled="isReadonly" @click="insertLine">行插入</el-button>
          <el-button size="small" type="danger" :disabled="isReadonly || selectedLineIndex < 0" @click="deleteLine">行删除</el-button>
          <el-button size="small" :disabled="isReadonly || selectedLineIndex < 0" @click="notImpl('带入默认工艺')" v-permission="'productionorder:default-process'">带入默认工艺</el-button>
          <el-button size="small" :disabled="selectedLineIndex < 0" @click="notImpl('下达')" v-permission="'productionorder:release'">下达</el-button>
          <el-button size="small" :disabled="selectedLineIndex < 0" @click="notImpl('反下达')" v-permission="'productionorder:unrelease'">反下达</el-button>
          <el-button size="small" :disabled="selectedLineIndex < 0" @click="notImpl('生成工序流转卡')" v-permission="'productionorder:gen-routecard'">生成工序流转卡</el-button>
          <el-divider direction="vertical" />
          <el-button size="small" :disabled="selectedLineIndex < 0" @click="notImpl('变更')" v-permission="'productionorder:change'">变更</el-button>
          <el-button size="small" :disabled="selectedLineIndex < 0" @click="notImpl('结案')" v-permission="'productionorder:close'">结案</el-button>
          <el-button size="small" :disabled="selectedLineIndex < 0" @click="notImpl('反结案')" v-permission="'productionorder:unclose'">反结案</el-button>
          <el-button size="small" :disabled="selectedLineIndex < 0" @click="notImpl('挂起')" v-permission="'productionorder:suspend'">挂起</el-button>
          <el-button size="small" :disabled="selectedLineIndex < 0" @click="notImpl('反挂起')" v-permission="'productionorder:unsuspend'">反挂起</el-button>
        </div>

        <el-table :data="lineItems" border size="small" row-key="_key" highlight-current-row
                  :row-class-name="lineRowClass" @row-click="onLineRowClick" empty-text="暂无明细，点击“行新增”添加">
          <el-table-column type="index" label="行号" width="55" align="center" fixed="left" />
          <el-table-column label="生产车间" width="170" fixed="left">
            <template #default="{ row }">
              <LookupSelect :key="'ws'+row._key" v-model="row.fworkshopid" module="department" placeholder="选择车间" :disabled="isReadonly" preload
                            @change="(it: any) => onWorkshopChange(row, it)" />
            </template>
          </el-table-column>
          <el-table-column label="产品类型" width="110">
            <template #default="{ row }">
              <el-select v-model="row.fproducttype" :disabled="isReadonly" placeholder="类型" style="width:100%">
                <el-option v-for="t in productTypes" :key="t.v" :label="t.l" :value="t.v" />
              </el-select>
            </template>
          </el-table-column>
          <el-table-column label="产品代码" min-width="200">
            <template #default="{ row }">
              <LookupSelect :key="'pr'+row._key" v-model="row.fprorouteid" module="proroute"
                            placeholder="选择产品（工艺）" :disabled="isReadonly" preload
                            @change="(it: any) => onProductChange(row, it)" />
            </template>
          </el-table-column>
          <el-table-column prop="fmaterialName" label="产品名称" min-width="150" />
          <el-table-column prop="fchartNumber" label="产品图号" width="120" />
          <el-table-column label="生产机型" width="120">
            <template #default="{ row }"><el-input v-model="row.fmachinemodel" :disabled="isReadonly" /></template>
          </el-table-column>
          <el-table-column label="启用批次管理" width="100" align="center">
            <template #default="{ row }"><el-checkbox :model-value="!!row.fisBatchManage" disabled /></template>
          </el-table-column>
          <el-table-column label="批次" width="120">
            <template #default="{ row }"><el-input v-model="row.flot" :disabled="isReadonly" /></template>
          </el-table-column>
          <el-table-column label="基本单位" width="120">
            <template #default="{ row }">
              <LookupSelect :key="'bu'+row._key" v-model="row.fbaseunitid" module="unit" placeholder="基本单位" :disabled="isReadonly" preload
                            @change="(it: any) => onBaseUnitChange(row, it)" />
            </template>
          </el-table-column>
          <el-table-column label="基本单位数量" width="120">
            <template #default="{ row }">
              <el-input-number v-model="row.fbaseunitqty" :min="0" :precision="6" :controls="false" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column label="常用单位" width="120">
            <template #default="{ row }">
              <LookupSelect :key="'cu'+row._key" v-model="row.fcommonunitid" module="unit" placeholder="常用单位" :disabled="isReadonly" preload
                            @change="(it: any) => onCommonUnitChange(row, it)" />
            </template>
          </el-table-column>
          <el-table-column label="数量" width="120">
            <template #default="{ row }">
              <el-input-number v-model="row.fqty" :min="0" :precision="6" :controls="false" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column label="启用辅助属性" width="100" align="center">
            <template #default="{ row }"><el-checkbox :model-value="!!row.fisAuxEnabled" disabled /></template>
          </el-table-column>
          <el-table-column label="辅助属性" width="170">
            <template #default="{ row }">
              <LookupSelect v-if="row.fisAuxEnabled" :key="'aux'+row._key" v-model="row.fauxpropid"
                            module="material/aux-properties" placeholder="选择辅助属性" :disabled="isReadonly" preload
                            @change="(it: any) => onAuxChange(row, it)" />
              <el-input v-else model-value="" disabled placeholder="未启用辅助属性" />
            </template>
          </el-table-column>
          <el-table-column prop="fprorouteNumber" label="产品工艺流程" width="130" />
          <el-table-column prop="fprorouteName" label="产品工艺名称" width="130" />
          <el-table-column label="条码规则" width="170">
            <template #default="{ row }">
              <LookupSelect :key="'bc'+row._key" v-model="row.fbarcoderuleid" module="barcode" placeholder="选择条码规则" :disabled="isReadonly" preload
                            @change="(it: any) => onBarcodeChange(row, it)" />
            </template>
          </el-table-column>
          <el-table-column prop="ffcqty" label="派工数量" width="90" align="right" />
          <el-table-column prop="ftotalfinishqty" label="累计完工数量" width="110" align="right" />
          <el-table-column prop="fbstatusName" label="业务状态" width="90" align="center" />
          <el-table-column label="挂起" width="60" align="center">
            <template #default="{ row }"><el-checkbox :model-value="!!row.fissuspend" disabled /></template>
          </el-table-column>
          <el-table-column label="超产比例(%)" width="110">
            <template #default="{ row }">
              <el-input-number v-model="row.finhighlimit" :min="0" :precision="2" :controls="false" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column label="换型时长(分)" width="110">
            <template #default="{ row }">
              <el-input-number v-model="row.fretime" :min="0" :precision="2" :controls="false" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column label="BOM单" width="170">
            <template #default="{ row }">
              <LookupSelect :key="'bom'+row._key" v-model="row.fbomid" module="bom" placeholder="选择BOM单" :disabled="isReadonly" preload
                            @change="(it: any) => onBomChange(row, it)" />
            </template>
          </el-table-column>
          <el-table-column label="下达日期" width="120">
            <template #default="{ row }">{{ fmtAuditDate(row.fconveydate) }}</template>
          </el-table-column>
          <el-table-column label="计划开工时间" width="150">
            <template #default="{ row }">
              <el-date-picker v-model="row.fplanstartdate" type="date" value-format="YYYY-MM-DD" placeholder="日期" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column label="计划完工时间" width="150">
            <template #default="{ row }">
              <el-date-picker v-model="row.fplanfinishdate" type="date" value-format="YYYY-MM-DD" placeholder="日期" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column label="实际开工时间" width="130">
            <template #default="{ row }">{{ fmtAuditDate(row.factualstartdate) }}</template>
          </el-table-column>
          <el-table-column label="实际完工时间" width="130">
            <template #default="{ row }">{{ fmtAuditDate(row.factualfinishdate) }}</template>
          </el-table-column>
          <el-table-column prop="ffinalName" label="结案人" width="100" />
          <el-table-column label="结案日期" width="120">
            <template #default="{ row }">{{ fmtAuditDate(row.ffinaldate) }}</template>
          </el-table-column>
          <el-table-column label="备注" min-width="140">
            <template #default="{ row }"><el-input v-model="row.fnote" :disabled="isReadonly" /></template>
          </el-table-column>
          <el-table-column prop="fstockqty" label="已入库数量" width="90" align="right" />
          <el-table-column prop="fstockbaseqty" label="已入库基本数量" width="110" align="right" />
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
import { Check, Back, Plus, CircleCheck, RefreshLeft } from '@element-plus/icons-vue'
import {
  getProductionOrder, createProductionOrder, updateProductionOrder,
  approveProductionOrder, unapproveProductionOrder, getMaterialAuxEnabled, type ProductionOrderEntry
} from '../../api/productionOrder'
import { formatDate } from '../../utils/format'
import { useOrgStore } from '../../stores/org'
import LookupSelect from '../../components/LookupSelect.vue'

const router = useRouter()
const route = useRoute()
const orgStore = useOrgStore()

const formRef = ref<FormInstance>()
const loading = ref(false)
const activeTab = ref('other')
const lineItems = ref<any[]>([])
const selectedLineIndex = ref(-1)

const uid = ref<string>((route.query.uid as string) || '')
const isEdit = computed(() => !!uid.value)
const productTypes = [{ v: '1', l: '主产品' }, { v: '2', l: '联产品' }, { v: '3', l: '副产品' }]

const defaultForm = {
  uid: '',
  fStatus: 0,
  fbillno: '',
  fbilltype: '',
  fdate: formatDate(new Date().toISOString()).slice(0, 10),
  fcompanyid: '',
  fcompanyName: '',
  fplannerid: '',
  fnote: '',
  // 只读
  foastatus: '',
  foaresult: '',
  fstatusName: '',
  cuserName: '', cYmd: '',
  fcheckerName: '', fcheckdate: '',
  muserName: '', mYmd: '',
  fdisableName: '', fdisabledate: ''
}

const form = reactive({ ...defaultForm })
const isReadonly = computed(() => isEdit.value && form.fStatus === 40)

const rules: FormRules = {
  fbilltype: [{ required: true, message: '请选择单据类型', trigger: 'change' }],
  fdate: [{ required: true, message: '请选择单据日期', trigger: 'change' }]
}

let rowKeySeq = 0
const nextKey = () => ++rowKeySeq

const fmtAuditDate = (d?: string) => {
  if (!d) return ''
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() <= 1900) return ''
  return formatDate(d)
}

// 组织下拉：编辑态若单据组织不在当前用户可选组织里，补一项避免显示空白
const orgOptions = computed(() => {
  const list: any[] = [...orgStore.orgs]
  if (form.fcompanyid && !list.some(o => o.orgId === form.fcompanyid)) {
    list.unshift({ orgId: form.fcompanyid, orgName: form.fcompanyName || form.fcompanyid })
  }
  return list
})

const lineRowClass = ({ rowIndex }: { rowIndex: number }) => (rowIndex === selectedLineIndex.value ? 'cur-row' : '')
const onLineRowClick = (row: any) => { selectedLineIndex.value = lineItems.value.indexOf(row) }

const newLine = () => ({
  fproducttype: '1', fworkshopid: '', fworkshopNumber: '', fworkshopName: '',
  fmaterialid: '', fmaterialNumber: '', fmaterialName: '', fchartNumber: '', fSpecification: '', fisBatchManage: false,
  fmachinemodel: '', flot: '',
  fbaseunitid: '', fbaseunitName: '', fbaseunitqty: 0,
  fcommonunitid: '', fcommonunitName: '', fqty: 0,
  fisAuxEnabled: false, fauxpropid: '', fauxpropName: '',
  fprorouteid: '', fprorouteNumber: '', fprorouteName: '',
  fbarcoderuleid: '', fbarcoderuleName: '',
  ffcqty: 0, ftotalfinishqty: 0, fbstatus: '1', fbstatusName: '计划', fissuspend: false,
  finhighlimit: 0, fretime: 0, fbomid: '', fbomBillno: '',
  fconveydate: null as any, fplanstartdate: null as any, fplanfinishdate: null as any,
  factualstartdate: null as any, factualfinishdate: null as any,
  ffinalid: '', ffinalName: '', ffinaldate: null as any,
  fnote: '', fstockqty: 0, fstockbaseqty: 0, _key: nextKey()
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

// 明细高级动作占位（带入默认工艺/下达/反下达/生成工序流转卡/变更/结案/反结案/挂起/反挂起）——功能后续实现
const notImpl = (name: string) => ElMessage.info(`「${name}」功能开发中，敬请期待`)

// 产品代码（工艺流程）选中：带出产品物料 + 工艺信息；按物料是否启用辅助属性决定该格可选/只读
const onProductChange = async (row: any, item: any) => {
  row.fmaterialid = item?.fmaterialid || ''
  row.fmaterialNumber = item?.fmaterialNumber || ''
  row.fmaterialName = item?.fmaterialName || ''
  row.fchartNumber = item?.fchartNumber || ''
  row.fSpecification = item?.fSpecification || ''
  row.fisBatchManage = !!item?.fisBatchManage
  row.fprorouteid = item?.fprorouteid || item?.uid || ''
  row.fprorouteNumber = item?.fprorouteNumber || ''
  row.fprorouteName = item?.fprorouteName || ''
  row.fauxpropid = ''
  row.fauxpropName = ''
  row.fisAuxEnabled = false
  if (item?.fmaterialid) {
    try {
      const res: any = await getMaterialAuxEnabled(item.fmaterialid)
      row.fisAuxEnabled = !!res.data
    } catch { row.fisAuxEnabled = false }
  }
}
const onWorkshopChange = (row: any, item: any) => { row.fworkshopNumber = item?.fNumber || ''; row.fworkshopName = item?.fName || '' }
const onBaseUnitChange = (row: any, item: any) => { row.fbaseunitName = item?.fName || '' }
const onCommonUnitChange = (row: any, item: any) => { row.fcommonunitName = item?.fName || '' }
const onAuxChange = (row: any, item: any) => { row.fauxpropName = item?.fName || '' }
const onBarcodeChange = (row: any, item: any) => { row.fbarcoderuleName = item?.fName || '' }
const onBomChange = (row: any, item: any) => { row.fbomBillno = item?.fNumber || '' }

const dateOrNull = (d?: string | null) => (fmtAuditDate(d || '') ? (d as string).slice(0, 10) : null)

async function loadDetail(id: string) {
  loading.value = true
  try {
    const res: any = await getProductionOrder(id)
    const d = res.data
    Object.keys(defaultForm).forEach(k => { if (d[k] !== undefined && d[k] !== null) (form as any)[k] = d[k] })
    form.uid = d.uid
    form.fStatus = d.fStatus
    form.fdate = d.fdate ? String(d.fdate).slice(0, 10) : ''
    lineItems.value = (d.entries || []).map((e: ProductionOrderEntry) => ({
      ...e,
      fplanstartdate: dateOrNull(e.fplanstartdate),
      fplanfinishdate: dateOrNull(e.fplanfinishdate),
      _key: nextKey()
    }))
    selectedLineIndex.value = lineItems.value.length > 0 ? 0 : -1
  } catch (e) {
    console.error('加载生产订单失败:', e)
    ElMessage.error('加载生产订单失败')
  } finally {
    loading.value = false
  }
}

function buildPayload() {
  return {
    fbillno: form.fbillno,
    fbilltype: form.fbilltype,
    fdate: form.fdate,
    fcompanyid: form.fcompanyid,
    fplannerid: form.fplannerid,
    fnote: form.fnote,
    entries: lineItems.value.filter(it => it.fmaterialid || it.fprorouteid).map(it => ({
      fproducttype: it.fproducttype || '1',
      fworkshopid: it.fworkshopid || '',
      fmaterialid: it.fmaterialid || '',
      fmachinemodel: it.fmachinemodel || '',
      flot: it.flot || '',
      fbaseunitid: it.fbaseunitid || '',
      fbaseunitqty: it.fbaseunitqty || 0,
      fcommonunitid: it.fcommonunitid || '',
      fqty: it.fqty || 0,
      fauxpropid: it.fauxpropid || '',
      fprorouteid: it.fprorouteid || '',
      fbarcoderuleid: it.fbarcoderuleid || '',
      finhighlimit: it.finhighlimit || 0,
      fretime: it.fretime || 0,
      fbomid: it.fbomid || '',
      fplanstartdate: it.fplanstartdate || null,
      fplanfinishdate: it.fplanfinishdate || null,
      fnote: it.fnote || ''
    }))
  }
}

async function handleSave() {
  if (!formRef.value) return
  try { await formRef.value.validate() } catch { return }
  if (buildPayload().entries.length === 0) { ElMessage.warning('请至少添加一条明细'); return }
  // 启用批次管理的产品，批次必录
  const noBatch = lineItems.value.find(it => (it.fmaterialid || it.fprorouteid) && it.fisBatchManage && !String(it.flot || '').trim())
  if (noBatch) { ElMessage.warning(`产品「${noBatch.fmaterialName || noBatch.fmaterialNumber}」启用了批次管理，批次必填`); return }
  loading.value = true
  try {
    if (isEdit.value) {
      await updateProductionOrder(uid.value, buildPayload())
      ElMessage.success('保存成功')
      await loadDetail(uid.value)
    } else {
      const res: any = await createProductionOrder(buildPayload())
      ElMessage.success('创建成功')
      const newUid = res?.data?.uid
      if (newUid) {
        uid.value = newUid
        router.replace({ name: 'ProductionOrderEdit', query: { uid: newUid } })
        await loadDetail(newUid)
      } else { handleBack() }
    }
  } catch (error: any) {
    ElMessage.error(error?.response?.data?.message || '提交失败')
  } finally {
    loading.value = false
  }
}

async function runStatus(fn: (id: string) => Promise<any>, msg: string) {
  if (!uid.value) return
  try { await fn(uid.value); ElMessage.success(msg); await loadDetail(uid.value) }
  catch (error: any) { ElMessage.error(error?.response?.data?.message || '操作失败') }
}
const handleApprove = () => runStatus(approveProductionOrder, '审核成功')
const handleUnapprove = () => runStatus(unapproveProductionOrder, '反审核成功')
const handleBack = () => router.push({ name: 'ProductionOrderList' })

onMounted(async () => {
  if (!orgStore.loaded) await orgStore.loadOrgs()
  if (isEdit.value) await loadDetail(uid.value)
  else if (!form.fcompanyid) form.fcompanyid = orgStore.currentOrgId
})
</script>

<style scoped>
.mo-edit-container {
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
.toolbar-spacer { flex: 1; }
.back-btn { margin-left: 8px; }
.edit-form { padding: 16px 20px 24px; overflow-y: auto; }
.form-header { padding-bottom: 4px; border-bottom: 1px dashed var(--border-color, #ebeef5); margin-bottom: 4px; }
.edit-tabs { margin-top: 4px; }
.grid-section { margin-top: 16px; }
.grid-toolbar { display: flex; align-items: center; gap: 8px; margin-bottom: 8px; }
.grid-title { font-weight: 600; margin-right: 8px; }
.edit-form :deep(.el-table__body tr.cur-row > td.el-table__cell) {
  background-color: var(--el-color-primary-light-9);
}
</style>
