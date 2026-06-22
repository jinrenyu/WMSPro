<template>
  <div class="mra-edit-container">
    <!-- 顶部工具栏 -->
    <div class="edit-toolbar">
      <el-button type="primary" :disabled="isReadonly" @click="handleSave"
                 v-permission="isEdit ? 'returnreq:edit' : 'returnreq:add'">
        <el-icon><Check /></el-icon> 保存
      </el-button>
      <el-button v-if="isEdit && form.fStatus === 10" type="success" @click="handleApprove" v-permission="'returnreq:approve'">
        <el-icon><CircleCheck /></el-icon> 审核
      </el-button>
      <el-button v-if="isEdit && form.fStatus === 40" type="warning" @click="handleUnapprove" v-permission="'returnreq:approve'">
        <el-icon><RefreshLeft /></el-icon> 反审核
      </el-button>
      <div class="toolbar-spacer" />
      <el-tag v-if="isEdit" :type="form.fStatus === 40 ? 'success' : form.fStatus === 70 ? 'info' : 'warning'" size="large">
        {{ form.fStatus === 40 ? '已审核' : form.fStatus === 70 ? '已关闭' : '未审核' }}
      </el-tag>
      <el-button class="back-btn" @click="handleBack"><el-icon><Back /></el-icon> 退出</el-button>
    </div>

    <el-form ref="formRef" :model="form" :rules="rules" label-width="84px"
             :disabled="isReadonly" class="edit-form" v-loading="loading">
      <!-- 单据头 -->
      <div class="form-header">
        <el-row :gutter="16">
          <el-col :span="8">
            <el-form-item label="单据类型" prop="fbilltypeid">
              <LookupSelect v-model="form.fbilltypeid" module="billtype" parent-id="PUR_MRAPP" placeholder="请选择单据类型" preload />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="单据编号">
              <el-input v-model="form.fbillno" :disabled="isEdit" placeholder="保存后自动生成" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="申请日期" prop="fdate">
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
        </el-row>
      </div>

      <el-tabs v-model="activeTab" class="edit-tabs">
        <!-- 基本 -->
        <el-tab-pane label="基本" name="basic">
          <el-row :gutter="16">
            <el-col :span="8">
              <el-form-item label="业务类型">
                <el-select v-model="form.fbusinesstype" placeholder="业务类型" style="width:100%">
                  <el-option v-for="t in businessTypes" :key="t.value" :label="t.label" :value="t.value" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="退料类型" prop="frmtype">
                <el-select v-model="form.frmtype" placeholder="退料类型" style="width:100%">
                  <el-option v-for="t in rmTypes" :key="t.value" :label="t.label" :value="t.value" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="退料方式" prop="frmmode">
                <el-select v-model="form.frmmode" placeholder="退料方式" style="width:100%">
                  <el-option v-for="t in rmModes" :key="t.value" :label="t.label" :value="t.value" />
                </el-select>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="8">
              <el-form-item label="补料方式">
                <el-select v-model="form.freplenishmode" placeholder="补料方式" style="width:100%">
                  <el-option v-for="t in replenishModes" :key="t.value" :label="t.label" :value="t.value" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="需求组织">
                <el-select v-model="form.frequireorgid" placeholder="需求组织" style="width:100%">
                  <el-option v-for="o in requireOrgOptions" :key="o.orgId" :label="o.orgName" :value="o.orgId" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="采购组织" prop="fpurchaseorgid">
                <el-select v-model="form.fpurchaseorgid" placeholder="采购组织" style="width:100%">
                  <el-option v-for="o in purchaseOrgOptions" :key="o.orgId" :label="o.orgName" :value="o.orgId" />
                </el-select>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="8">
              <el-form-item label="退料组织" prop="fstockorgid">
                <el-select v-model="form.fstockorgid" placeholder="退料组织" style="width:100%">
                  <el-option v-for="o in stockOrgOptions" :key="o.orgId" :label="o.orgName" :value="o.orgId" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="退料部门">
                <LookupSelect v-model="form.fappdeptid" module="department" placeholder="请选择退料部门" preload />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="供应商" prop="fsupplyid">
                <LookupSelect v-model="form.fsupplyid" module="supplier" placeholder="请选择供应商" preload />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="8">
              <el-form-item label="币别">
                <LookupSelect v-model="form.fcurrencyid" module="currency" placeholder="请选择币别" preload />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="汇率类型">
                <el-select v-model="form.fexchangetypeid" placeholder="汇率类型" style="width:100%">
                  <el-option v-for="t in exchangeTypes" :key="t" :label="t" :value="t" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="汇率">
                <el-input-number v-model="form.fexchangerate" :min="0" :precision="4" :controls="false" style="width:100%" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="24">
              <el-form-item label="备注">
                <el-input v-model="form.fnote" placeholder="备注" />
              </el-form-item>
            </el-col>
          </el-row>
        </el-tab-pane>

        <!-- 其他 -->
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

      <!-- 退料通知单明细 -->
      <div class="grid-section">
        <div class="grid-toolbar">
          <span class="grid-title">退料通知单明细</span>
          <el-button type="primary" size="small" :disabled="isReadonly" @click="addLine"><el-icon><Plus /></el-icon> 行新增</el-button>
          <el-button size="small" :disabled="isReadonly" @click="insertLine">行插入</el-button>
          <el-button size="small" type="danger" :disabled="isReadonly || selectedLineIndex < 0" @click="deleteLine">行删除</el-button>
        </div>

        <el-table :data="lineItems" border size="small" row-key="_key" highlight-current-row
                  :row-class-name="lineRowClass" @row-click="onLineRowClick" empty-text="暂无明细，点击“行新增”添加">
          <el-table-column type="index" label="行号" width="55" align="center" fixed="left" />
          <el-table-column label="产品类型" width="120">
            <template #default="{ row }">
              <el-select v-model="row.frowtype" :disabled="isReadonly" placeholder="产品类型" style="width:100%">
                <el-option v-for="t in rowTypes" :key="t.value" :label="t.label" :value="t.value" />
              </el-select>
            </template>
          </el-table-column>
          <el-table-column label="物料代码" min-width="200" fixed="left">
            <template #default="{ row }">
              <MaterialLookup :key="row._key" v-model="row.fmaterialid" :display-text="row.fmaterialNumber"
                              placeholder="选择物料" :disabled="isReadonly"
                              @change="(m: any) => onMaterialChange(row, m)" />
            </template>
          </el-table-column>
          <el-table-column prop="fmaterialName" label="物料名称" min-width="150" />
          <el-table-column prop="fSpecification" label="规格型号" min-width="120" />
          <el-table-column label="启用辅助属性" width="100" align="center">
            <template #default="{ row }"><el-checkbox :model-value="!!row.fisAuxEnabled" disabled /></template>
          </el-table-column>
          <el-table-column label="辅助属性" width="170">
            <template #default="{ row }">
              <LookupSelect v-if="row.fisAuxEnabled" :key="'a'+row._key" v-model="row.fauxpropid"
                            module="material/aux-properties" placeholder="选择辅助属性" :disabled="isReadonly" preload
                            @change="(it: any) => onAuxChange(row, it)" />
              <el-input v-else model-value="" disabled placeholder="未启用辅助属性" />
            </template>
          </el-table-column>
          <el-table-column label="启用批次管理" width="100" align="center">
            <template #default="{ row }"><el-checkbox :model-value="!!row.fisBatchManage" disabled /></template>
          </el-table-column>
          <el-table-column label="批次" width="120">
            <template #default="{ row }"><el-input v-model="row.flot" :disabled="isReadonly" /></template>
          </el-table-column>
          <el-table-column label="申请退料数量" width="120">
            <template #default="{ row }">
              <el-input-number v-model="row.fmrappqty" :min="0" :precision="4" :controls="false" :disabled="isReadonly" style="width:100%" @change="() => recalc(row)" />
            </template>
          </el-table-column>
          <el-table-column label="退料数量" width="110">
            <template #default="{ row }">
              <el-input-number v-model="row.fmrqty" :min="0" :precision="4" :controls="false" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column prop="fmrbqty" label="累计退料数量" width="110" align="right" />
          <el-table-column label="补料数量" width="110">
            <template #default="{ row }">
              <el-input-number v-model="row.freplenishqty" :min="0" :precision="4" :controls="false" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column label="单位" width="120">
            <template #default="{ row }">
              <LookupSelect :key="'u'+row._key" v-model="row.funitid" module="unit" placeholder="单位" :disabled="isReadonly" preload
                            @change="(it: any) => onUnitChange(row, it)" />
            </template>
          </el-table-column>
          <el-table-column prop="funitName" label="单位名称" width="100" />
          <el-table-column label="库存单位" width="120">
            <template #default="{ row }">
              <LookupSelect :key="'su'+row._key" v-model="row.fstockunitid" module="unit" placeholder="库存单位" :disabled="isReadonly" preload
                            @change="(it: any) => onStockUnitChange(row, it)" />
            </template>
          </el-table-column>
          <el-table-column label="库存单位数量" width="120">
            <template #default="{ row }">
              <el-input-number v-model="row.fstockqty" :min="0" :precision="4" :controls="false" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column label="仓库" width="150">
            <template #default="{ row }">
              <LookupSelect :key="'s'+row._key" v-model="row.fstockid" module="warehouse" placeholder="仓库" :disabled="isReadonly" preload
                            @change="(it: any) => onStockChange(row, it)" />
            </template>
          </el-table-column>
          <el-table-column prop="fstockName" label="仓库名称" width="110" />
          <el-table-column label="启用仓位管理" width="100" align="center">
            <template #default="{ row }"><el-checkbox :model-value="!!row.fisOpenLocation" disabled /></template>
          </el-table-column>
          <el-table-column label="仓位" width="150">
            <template #default="{ row }">
              <LookupSelect :key="'l'+row._key" v-model="row.fstocklocid" module="stockplace" :parent-id="row.fstockid"
                            placeholder="先选仓库" :disabled="isReadonly" preload
                            @change="(it: any) => onStockLocChange(row, it)" />
            </template>
          </el-table-column>
          <el-table-column label="含税单价" width="110" align="right">
            <template #default="{ row }">{{ fmtNum(row.ftaxprice) }}</template>
          </el-table-column>
          <el-table-column label="税率%" width="90">
            <template #default="{ row }">
              <el-input-number v-model="row.ftaxrate" :min="0" :max="100" :precision="2" :controls="false" :disabled="isReadonly" style="width:100%" @change="() => recalc(row)" />
            </template>
          </el-table-column>
          <el-table-column label="单价" width="110">
            <template #default="{ row }">
              <el-input-number v-model="row.fprice" :min="0" :precision="6" :controls="false" :disabled="isReadonly" style="width:100%" @change="() => recalc(row)" />
            </template>
          </el-table-column>
          <el-table-column label="折扣率%" width="90">
            <template #default="{ row }">
              <el-input-number v-model="row.fdiscountrate" :min="0" :max="100" :precision="2" :controls="false" :disabled="isReadonly" style="width:100%" @change="() => recalc(row)" />
            </template>
          </el-table-column>
          <el-table-column label="折扣额" width="110" align="right">
            <template #default="{ row }">{{ fmtNum(row.fdiscount) }}</template>
          </el-table-column>
          <el-table-column label="税额" width="110" align="right">
            <template #default="{ row }">{{ fmtNum(row.ftaxamount) }}</template>
          </el-table-column>
          <el-table-column label="退料金额" width="110" align="right">
            <template #default="{ row }">{{ fmtNum(row.famount) }}</template>
          </el-table-column>
          <el-table-column label="价税合计" width="120" align="right">
            <template #default="{ row }">{{ fmtNum(row.fallamount) }}</template>
          </el-table-column>
          <el-table-column label="启用保质期" width="90" align="center">
            <template #default="{ row }"><el-checkbox :model-value="!!row.fisKfPeriod" disabled /></template>
          </el-table-column>
          <el-table-column prop="fKfPeriod" label="保质期限" width="90" align="right" />
          <el-table-column label="保质期单位" width="90" align="center">
            <template #default="{ row }">{{ kfUnitLabel(row.fKfUnit) }}</template>
          </el-table-column>
          <el-table-column label="备注" min-width="140">
            <template #default="{ row }"><el-input v-model="row.fnote" :disabled="isReadonly" /></template>
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
import { Check, Back, Plus, CircleCheck, RefreshLeft } from '@element-plus/icons-vue'
import {
  getMaterialReturnApply, createMaterialReturnApply, updateMaterialReturnApply,
  approveMaterialReturnApply, unapproveMaterialReturnApply, getMaterialAuxEnabled,
  type MaterialReturnApplyEntry
} from '../../api/materialReturnApply'
import { formatDate } from '../../utils/format'
import { useOrgStore } from '../../stores/org'
import LookupSelect from '../../components/LookupSelect.vue'
import MaterialLookup from '../../components/MaterialLookup.vue'

const router = useRouter()
const route = useRoute()
const orgStore = useOrgStore()

const formRef = ref<FormInstance>()
const loading = ref(false)
const activeTab = ref('basic')
const lineItems = ref<any[]>([])
const selectedLineIndex = ref(-1)

const uid = ref<string>((route.query.uid as string) || '')
const isEdit = computed(() => !!uid.value)

// 业务类型存码（CG/WW/...），下拉显示中文
const businessTypes = [
  { value: 'CG', label: '标准采购' },
  { value: 'WW', label: '标准委外' },
  { value: 'ZCCG', label: '资产采购' },
  { value: 'ZYCG', label: '直运采购' },
  { value: 'FYCG', label: '费用采购' },
  { value: 'VMICG', label: 'VMI采购' },
  { value: 'CSCG', label: '测试采购' }
]
const rmTypes = [{ value: 'A', label: '检验退料' }, { value: 'B', label: '库存退料' }]
const rmModes = [{ value: 'A', label: '退料补料' }, { value: 'B', label: '退料并扣款' }]
const replenishModes = [{ value: 'A', label: '按源单补料' }, { value: 'B', label: '创建补料订单' }]
const rowTypes = [
  { value: 'Standard', label: '标准产品' },
  { value: 'Parent', label: '套件父项' },
  { value: 'Son', label: '套件子项' },
  { value: 'Service', label: '服务' }
]
const exchangeTypes = ['固定汇率', '浮动汇率']

const defaultForm = {
  uid: '',
  fStatus: 0,
  fbillno: '',
  fbilltypeid: '',
  fdate: formatDate(new Date().toISOString()).slice(0, 10),
  fbusinesstype: 'CG',
  frmtype: 'B',
  frmmode: 'A',
  freplenishmode: 'B',
  frequireorgid: '',
  frequireorgName: '',
  fpurchaseorgid: '',
  fpurchaseorgName: '',
  fstockorgid: '',
  fstockorgName: '',
  fappdeptid: '',
  fsupplyid: '',
  fcurrencyid: '',
  fexchangetypeid: '固定汇率',
  fexchangerate: 1,
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
// 仅草稿(10)可编辑；已审核(40)/已关闭(70)整单只读
const isReadonly = computed(() => isEdit.value && form.fStatus !== 10)

const rules: FormRules = {
  fbilltypeid: [{ required: true, message: '请选择单据类型', trigger: 'change' }],
  fdate: [{ required: true, message: '请选择申请日期', trigger: 'change' }],
  frmtype: [{ required: true, message: '请选择退料类型', trigger: 'change' }],
  frmmode: [{ required: true, message: '请选择退料方式', trigger: 'change' }],
  fpurchaseorgid: [{ required: true, message: '请选择采购组织', trigger: 'change' }],
  fstockorgid: [{ required: true, message: '请选择退料组织', trigger: 'change' }],
  fsupplyid: [{ required: true, message: '请选择供应商', trigger: 'change' }]
}

let rowKeySeq = 0
const nextKey = () => ++rowKeySeq

const fmtAuditDate = (d?: string) => {
  if (!d) return ''
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() <= 1900) return ''
  return formatDate(d)
}
const fmtNum = (n?: number) => (n == null ? '0' : Number(n).toFixed(2))
const kfUnitLabel = (v?: number) => (v === 0 ? '日' : v === 1 ? '月' : v === 2 ? '年' : '')

// 组织下拉：编辑态若单据组织不在当前用户可选组织里，补一项避免显示空白
const buildOrgOptions = (id: string, name: string) => {
  const list: any[] = [...orgStore.orgs]
  if (id && !list.some(o => o.orgId === id)) list.unshift({ orgId: id, orgName: name || id })
  return list
}
const requireOrgOptions = computed(() => buildOrgOptions(form.frequireorgid, form.frequireorgName))
const purchaseOrgOptions = computed(() => buildOrgOptions(form.fpurchaseorgid, form.fpurchaseorgName))
const stockOrgOptions = computed(() => buildOrgOptions(form.fstockorgid, form.fstockorgName))

const lineRowClass = ({ rowIndex }: { rowIndex: number }) => (rowIndex === selectedLineIndex.value ? 'cur-row' : '')
const onLineRowClick = (row: any) => { selectedLineIndex.value = lineItems.value.indexOf(row) }

const newLine = () => ({
  frowtype: 'Standard',
  fmaterialid: '', fmaterialNumber: '', fmaterialName: '', fSpecification: '',
  fisBatchManage: false, fisKfPeriod: false, fKfPeriod: 0, fKfUnit: 0,
  fauxpropid: '', fauxpropName: '', fisAuxEnabled: false,
  flot: '',
  fmrappqty: 0, fmrqty: 0, fmrbqty: 0, freplenishqty: 0, fstockqty: 0, fbaseunitqty: 0,
  funitid: '', funitNumber: '', funitName: '', fbaseunitid: '',
  fstockunitid: '', fstockunitName: '',
  fstockid: '', fstockNumber: '', fstockName: '', fisOpenLocation: false,
  fstocklocid: '', fstocklocName: '',
  fprice: 0, ftaxrate: 0, ftaxprice: 0, fdiscountrate: 0, fdiscount: 0, ftaxamount: 0, famount: 0, fallamount: 0,
  fsrcformid: '', fsrcbillno: '',
  forderbillno: '', forderentryid: 0, forderinterid: '', forderdetailid: '',
  fnote: '', _key: nextKey()
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

const recalc = (row: any) => {
  const qty = Number(row.fmrappqty) || 0
  const price = Number(row.fprice) || 0
  const rate = Number(row.ftaxrate) || 0
  const disc = Number(row.fdiscountrate) || 0
  row.famount = +(qty * price * (1 - disc / 100)).toFixed(2)   // 退料金额 = 申请退料数量×单价×(1-折扣率%)
  row.fdiscount = +(qty * price * (disc / 100)).toFixed(2)     // 折扣额
  row.ftaxamount = +(row.famount * rate / 100).toFixed(2)
  row.fallamount = +(row.famount + row.ftaxamount).toFixed(2)
  row.ftaxprice = +(price * (1 + rate / 100)).toFixed(6)        // 含税单价 = 单价×(1+税率%)
}

const onMaterialChange = async (row: any, item: any) => {
  row.fmaterialName = item?.fName || ''
  row.fmaterialNumber = item?.fNumber || ''
  row.fSpecification = item?.fSpecification || ''
  // 物料带出：启用批次管理 / 保质期（只读展示）
  row.fisBatchManage = !!item?.fIsBatchManage
  row.fisKfPeriod = !!item?.fIsKfPeriod
  row.fKfPeriod = item?.fKfPeriod || 0
  row.fKfUnit = item?.fKfUnit || 0
  // 物料带出：库存单位（对应明细“库存单位”列 fstockunitid，参照 onStockUnitChange 填 name）
  if (item?.fStoreUnitId) { row.fstockunitid = item.fStoreUnitId; row.fstockunitName = item.fStoreUnitName || '' }
  // 物料带出：基本单位（无 UI 列，仅赋值参与保存）
  if (item?.fBaseUnitId) { row.fbaseunitid = item.fBaseUnitId }
  // 切换物料后重置辅助属性，并按物料是否启用辅助属性决定该格可选/只读
  row.fauxpropid = ''
  row.fauxpropName = ''
  row.fisAuxEnabled = false
  if (item?.uid) {
    try {
      const res: any = await getMaterialAuxEnabled(item.uid)
      row.fisAuxEnabled = !!res.data
    } catch { row.fisAuxEnabled = false }
  }
}
const onAuxChange = (row: any, item: any) => { row.fauxpropName = item?.fName || '' }
const onUnitChange = (row: any, item: any) => { row.funitName = item?.fName || ''; row.funitNumber = item?.fNumber || '' }
const onStockUnitChange = (row: any, item: any) => { row.fstockunitName = item?.fName || '' }
const onStockChange = (row: any, item: any) => {
  row.fstockName = item?.fName || ''
  row.fstockNumber = item?.fNumber || ''
  row.fisOpenLocation = !!item?.fIsOpenLocation
  // 切换仓库后清空仓位（仓位隶属仓库）
  row.fstocklocid = ''
  row.fstocklocName = ''
}
const onStockLocChange = (row: any, item: any) => { row.fstocklocName = item?.fName || '' }

async function loadDetail(id: string) {
  loading.value = true
  try {
    const res: any = await getMaterialReturnApply(id)
    const d = res.data
    Object.keys(defaultForm).forEach(k => { if (d[k] !== undefined && d[k] !== null) (form as any)[k] = d[k] })
    form.uid = d.uid
    form.fStatus = d.fStatus
    // 日期框需 YYYY-MM-DD（服务端返回 ISO 串，截取日期部分）
    form.fdate = (d.fdate ? String(d.fdate).slice(0, 10) : '') || ''
    lineItems.value = (d.entries || []).map((e: MaterialReturnApplyEntry) => ({ ...e, _key: nextKey() }))
    selectedLineIndex.value = lineItems.value.length > 0 ? 0 : -1
  } catch (e) {
    console.error('加载退料申请单失败:', e)
    ElMessage.error('加载退料申请单失败')
  } finally {
    loading.value = false
  }
}

function buildPayload() {
  return {
    fbillno: form.fbillno,
    fbilltypeid: form.fbilltypeid,
    fdate: form.fdate,
    fbusinesstype: form.fbusinesstype,
    frmtype: form.frmtype,
    frmmode: form.frmmode,
    freplenishmode: form.freplenishmode,
    frequireorgid: form.frequireorgid,
    fpurchaseorgid: form.fpurchaseorgid,
    fstockorgid: form.fstockorgid,
    fappdeptid: form.fappdeptid,
    fsupplyid: form.fsupplyid,
    fcurrencyid: form.fcurrencyid,
    fexchangetypeid: form.fexchangetypeid,
    fexchangerate: form.fexchangerate,
    fnote: form.fnote,
    entries: lineItems.value.filter(it => it.fmaterialid).map(it => ({
      frowtype: it.frowtype || 'Standard',
      fmaterialid: it.fmaterialid,
      fauxpropid: it.fauxpropid || '',
      flot: it.flot || '',
      fmrappqty: it.fmrappqty || 0,
      fmrqty: it.fmrqty || 0,
      freplenishqty: it.freplenishqty || 0,
      fstockqty: it.fstockqty || 0,
      fbaseunitqty: it.fbaseunitqty || 0,
      funitid: it.funitid || '',
      fbaseunitid: it.fbaseunitid || '',
      fstockunitid: it.fstockunitid || '',
      fstockid: it.fstockid || '',
      fstocklocid: it.fstocklocid || '',
      fprice: it.fprice || 0,
      ftaxrate: it.ftaxrate || 0,
      fdiscountrate: it.fdiscountrate || 0,
      fsrcformid: it.fsrcformid || '',
      fsrcbillno: it.fsrcbillno || '',
      forderbillno: it.forderbillno || '',
      forderentryid: it.forderentryid || 0,
      forderinterid: it.forderinterid || '',
      forderdetailid: it.forderdetailid || '',
      fnote: it.fnote || ''
    }))
  }
}

async function handleSave() {
  if (!formRef.value) return
  try { await formRef.value.validate() } catch { activeTab.value = 'basic'; return }
  // 提交前对每行重算金额，避免漏触发 @change 导致脏值（后端也会以服务端公式为准重算）
  lineItems.value.forEach(it => recalc(it))
  if (buildPayload().entries.length === 0) { ElMessage.warning('请至少添加一条明细'); return }
  // 批号必录：物料启用了批次管理则批次不能为空
  const noBatch = lineItems.value.find(it => it.fmaterialid && it.fisBatchManage && !String(it.flot || '').trim())
  if (noBatch) { ElMessage.warning(`物料「${noBatch.fmaterialName || noBatch.fmaterialNumber}」启用了批号管理，批次必填`); return }
  loading.value = true
  try {
    if (isEdit.value) {
      await updateMaterialReturnApply(uid.value, buildPayload())
      ElMessage.success('保存成功')
      await loadDetail(uid.value)
    } else {
      const res: any = await createMaterialReturnApply(buildPayload())
      ElMessage.success('创建成功')
      const newUid = res?.data?.uid
      if (newUid) {
        uid.value = newUid
        router.replace({ name: 'MaterialReturnApplyEdit', query: { uid: newUid } })
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
const handleApprove = () => runStatus(approveMaterialReturnApply, '审核成功')
const handleUnapprove = () => runStatus(unapproveMaterialReturnApply, '反审核成功')
const handleBack = () => router.push({ name: 'MaterialReturnApplyList' })

onMounted(async () => {
  if (!orgStore.loaded) await orgStore.loadOrgs()
  if (isEdit.value) await loadDetail(uid.value)
  else {
    if (!form.frequireorgid) form.frequireorgid = orgStore.currentOrgId
    if (!form.fpurchaseorgid) form.fpurchaseorgid = orgStore.currentOrgId
    if (!form.fstockorgid) form.fstockorgid = orgStore.currentOrgId
  }
})
</script>

<style scoped>
.mra-edit-container {
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
