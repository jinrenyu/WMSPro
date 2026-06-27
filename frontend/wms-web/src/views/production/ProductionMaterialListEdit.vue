<template>
  <div class="ppbom-edit-container">
    <!-- 顶部工具栏 -->
    <div class="edit-toolbar">
      <el-button type="primary" :disabled="isReadonly" @click="handleSave"
                 v-permission="isEdit ? 'prodmateriallist:edit' : 'prodmateriallist:add'">
        <el-icon><Check /></el-icon> 保存
      </el-button>
      <el-button v-if="isEdit && form.fStatus !== 40" type="success" @click="handleApprove" v-permission="'prodmateriallist:approve'">
        <el-icon><CircleCheck /></el-icon> 审核
      </el-button>
      <el-button v-if="isEdit && form.fStatus === 40" type="warning" @click="handleUnapprove" v-permission="'prodmateriallist:approve'">
        <el-icon><RefreshLeft /></el-icon> 反审核
      </el-button>
      <div class="toolbar-spacer" />
      <el-tag v-if="isEdit" :type="form.fStatus === 40 ? 'success' : 'warning'" size="large">
        {{ form.fStatus === 40 ? '已审核' : '未审核' }}
      </el-tag>
      <el-button class="back-btn" @click="handleBack"><el-icon><Back /></el-icon> 退出</el-button>
    </div>

    <el-form ref="formRef" :model="form" :rules="rules" label-width="110px"
             :disabled="isReadonly" class="edit-form" v-loading="loading">
      <!-- 单据头：单据编号 / 单据日期 -->
      <div class="form-header">
        <el-row :gutter="16">
          <el-col :span="12">
            <el-form-item label="单据编号">
              <el-input v-model="form.fbillno" :disabled="isEdit" placeholder="保存后自动生成" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="单据日期" prop="fdate">
              <el-date-picker v-model="form.fdate" type="date" value-format="YYYY-MM-DD" placeholder="选择日期" style="width:100%" />
            </el-form-item>
          </el-col>
        </el-row>
      </div>

      <el-tabs v-model="activeTab" class="edit-tabs">
        <el-tab-pane label="基本" name="basic">
          <el-row :gutter="16">
            <el-col :span="12">
              <el-form-item label="生产工单编号" prop="fmoentryid">
                <ProductionOrderLookup v-model="form.fmoentryid" :display-text="form.fmobillno" placeholder="点击选择生产订单"
                              @change="onMoChange" />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="生产工单行号">
                <el-input :model-value="form.fmoentryseq" disabled />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="12">
              <el-form-item label="产品代码" prop="fmaterialid">
                <MaterialLookup v-model="form.fmaterialid" :display-text="form.fmaterialNumber" placeholder="点击选择产品"
                              @change="onProductChange" />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="产品名称">
                <el-input :model-value="form.fmaterialName" disabled />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="12">
              <el-form-item label="规格型号">
                <el-input :model-value="form.fSpecification" disabled />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="启用辅助属性">
                <el-checkbox :model-value="!!form.fisAuxEnabled" disabled />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="12">
              <el-form-item label="辅助属性">
                <LookupSelect v-if="form.fisAuxEnabled" v-model="form.fauxpropid"
                              module="material/aux-properties" placeholder="请选择辅助属性" preload
                              @change="onAuxChange" />
                <el-input v-else model-value="" disabled placeholder="未启用辅助属性" />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="生产类型">
                <LookupSelect v-model="form.fmotype" module="producetype" placeholder="请选择生产类型" preload
                              @change="onMotypeChange" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="12">
              <el-form-item label="生产数量" prop="fqty">
                <el-input-number v-model="form.fqty" :min="0" :precision="6" :controls="false" style="width:100%" />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="单位" prop="funitid">
                <LookupSelect v-model="form.funitid" module="unit" placeholder="请选择单位" preload
                              @change="onUnitChange" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="12">
              <el-form-item label="组织">
                <el-select v-model="form.fcompanyid" placeholder="组织" style="width:100%">
                  <el-option v-for="o in orgOptions" :key="o.orgId" :label="o.orgName" :value="o.orgId" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="备注">
                <el-input v-model="form.fnote" placeholder="备注" />
              </el-form-item>
            </el-col>
          </el-row>
        </el-tab-pane>

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

      <!-- 生产用料清单明细 -->
      <div class="grid-section">
        <div class="grid-toolbar">
          <span class="grid-title">生产用料清单</span>
          <el-button type="primary" size="small" :disabled="isReadonly" @click="addLine"><el-icon><Plus /></el-icon> 行新增</el-button>
          <el-button size="small" :disabled="isReadonly" @click="insertLine">行插入</el-button>
          <el-button size="small" type="danger" :disabled="isReadonly || selectedLineIndex < 0" @click="deleteLine">行删除</el-button>
        </div>

        <el-table :data="lineItems" border size="small" row-key="_key" highlight-current-row
                  :row-class-name="lineRowClass" @row-click="onLineRowClick" empty-text="暂无明细，点击“行新增”添加">
          <el-table-column type="index" label="行号" width="55" align="center" fixed="left" />
          <el-table-column label="项次" width="80">
            <template #default="{ row }">
              <el-input-number v-model="row.freplacegroup" :min="0" :precision="0" :controls="false" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column label="子项类型" width="110" fixed="left">
            <template #default="{ row }">
              <el-select v-model="row.fmaterialtype" :disabled="isReadonly" placeholder="类型" style="width:100%">
                <el-option v-for="t in materialTypes" :key="t.v" :label="t.l" :value="t.v" />
              </el-select>
            </template>
          </el-table-column>
          <el-table-column label="物料代码" min-width="190">
            <template #default="{ row }">
              <MaterialLookup :key="'m'+row._key" v-model="row.fmaterialid" :display-text="row.fmaterialNumber" placeholder="选择物料" :disabled="isReadonly" @change="(it: any) => onLineMaterialChange(row, it)" />
            </template>
          </el-table-column>
          <el-table-column prop="fmaterialName" label="物料名称" min-width="150" />
          <el-table-column prop="fSpecification" label="规格型号" min-width="120" />
          <el-table-column label="工序代码" width="170">
            <template #default="{ row }">
              <LookupSelect :key="'op'+row._key" v-model="row.foperid" module="process" placeholder="选择工序" :disabled="isReadonly" preload
                            @change="(it: any) => onOperChange(row, it)" />
            </template>
          </el-table-column>
          <el-table-column prop="foperName" label="工序名称" width="130" />
          <el-table-column prop="fbaseUnitNumber" label="基本单位" width="100" />
          <el-table-column prop="fbaseUnitName" label="基本单位名称" width="110" />
          <el-table-column label="常用单位" width="130">
            <template #default="{ row }">
              <LookupSelect :key="'cu'+row._key" v-model="row.fcommonunitid" module="unit" placeholder="常用单位" :disabled="isReadonly" preload
                            @change="(it: any) => onCommonUnitChange(row, it)" />
            </template>
          </el-table-column>
          <el-table-column prop="fcommonunitName" label="常用单位名称" width="110" />
          <el-table-column label="批次" width="120">
            <template #default="{ row }"><el-input v-model="row.flot" :disabled="isReadonly" /></template>
          </el-table-column>
          <el-table-column label="启用辅助属性" width="100" align="center">
            <template #default="{ row }"><el-checkbox :model-value="!!row.fisAuxEnabled" disabled /></template>
          </el-table-column>
          <el-table-column label="辅助属性" width="170">
            <template #default="{ row }">
              <LookupSelect v-if="row.fisAuxEnabled" :key="'aux'+row._key" v-model="row.fauxpropid"
                            module="material/aux-properties" placeholder="选择辅助属性" :disabled="isReadonly" preload
                            @change="(it: any) => onLineAuxChange(row, it)" />
              <el-input v-else model-value="" disabled placeholder="未启用" />
            </template>
          </el-table-column>
          <el-table-column label="使用比例" width="110">
            <template #default="{ row }">
              <el-input-number v-model="row.fuserate" :min="0" :precision="6" :controls="false" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column label="分子" width="100">
            <template #default="{ row }">
              <el-input-number v-model="row.fnumerator" :min="0" :precision="6" :controls="false" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column label="分母" width="100">
            <template #default="{ row }">
              <el-input-number v-model="row.fdenominator" :min="0" :precision="6" :controls="false" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column label="变动损耗率%" width="120">
            <template #default="{ row }">
              <el-input-number v-model="row.fscraprate" :min="0" :precision="2" :controls="false" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column label="固定损耗" width="110">
            <template #default="{ row }">
              <el-input-number v-model="row.ffixscrapqty" :min="0" :precision="6" :controls="false" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column label="应发数量" width="120">
            <template #default="{ row }">
              <el-input-number v-model="row.fmustqty" :min="0" :precision="6" :controls="false" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column prop="fpickedqty" label="已领数量" width="100" align="right" />
          <el-table-column prop="fnopickedqty" label="未领数量" width="100" align="right" />
          <el-table-column label="计划发料日期" width="150">
            <template #default="{ row }">
              <el-date-picker v-model="row.fsenditemdate" type="date" value-format="YYYY-MM-DD" placeholder="日期" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column label="仓库代码" width="160">
            <template #default="{ row }">
              <LookupSelect :key="'s'+row._key" v-model="row.fstockid" module="warehouse" placeholder="仓库" :disabled="isReadonly" preload
                            @change="(it: any) => onLineStockChange(row, it)" />
            </template>
          </el-table-column>
          <el-table-column prop="fstockName" label="仓库名称" width="120" />
          <el-table-column label="启用仓位管理" width="100" align="center">
            <template #default="{ row }"><el-checkbox :model-value="!!row.fisOpenLocation" disabled /></template>
          </el-table-column>
          <el-table-column label="仓位" width="160">
            <template #default="{ row }">
              <LookupSelect :key="'l'+row._key" v-model="row.fstocklocid" module="stockplace" :parent-id="row.fstockid"
                            :placeholder="row.fisOpenLocation ? '请选择仓位' : '未启用仓位管理'"
                            :disabled="isReadonly || !row.fisOpenLocation" preload
                            @change="(it: any) => onLineStockLocChange(row, it)" />
            </template>
          </el-table-column>
          <el-table-column label="倒冲" width="70" align="center">
            <template #default="{ row }">
              <el-checkbox :model-value="row.fbackflush === 1" :disabled="isReadonly" @change="(v: any) => row.fbackflush = v ? 1 : 0" />
            </template>
          </el-table-column>
          <el-table-column label="是否启用序列号" width="120" align="center">
            <template #default="{ row }"><el-checkbox v-model="row.ffeedsn" :disabled="isReadonly" /></template>
          </el-table-column>
          <el-table-column label="是否第三方序列号" width="130" align="center">
            <template #default="{ row }"><el-checkbox v-model="row.fothersn" :disabled="isReadonly" /></template>
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
  getProductionMaterialList, createProductionMaterialList, updateProductionMaterialList,
  approveProductionMaterialList, unapproveProductionMaterialList, getMaterialAuxEnabled,
  type ProductionMaterialListEntry
} from '../../api/productionMaterialList'
import { formatDate } from '../../utils/format'
import { useOrgStore } from '../../stores/org'
import LookupSelect from '../../components/LookupSelect.vue'
import MaterialLookup from '../../components/MaterialLookup.vue'
import ProductionOrderLookup from '../../components/ProductionOrderLookup.vue'

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
const materialTypes = [{ v: '1', l: '标准件' }, { v: '2', l: '返还件' }, { v: '3', l: '替代件' }]

const defaultForm = {
  uid: '',
  fStatus: 0,
  fbillno: '',
  fdate: formatDate(new Date().toISOString()).slice(0, 10),
  // 生产工单
  fmoentryid: '',
  fmoid: '',
  fmobillno: '',
  fmoentryseq: 0,
  // 产品
  fmaterialid: '',
  fmaterialNumber: '',
  fmaterialName: '',
  fSpecification: '',
  fisAuxEnabled: false,
  fauxpropid: '',
  fauxpropName: '',
  // 生产类型
  fmotype: '',
  fmotypeNumber: '',
  fmotypeName: '',
  // 数量/单位
  fqty: 0,
  funitid: '',
  funitNumber: '',
  funitName: '',
  // 组织/备注
  fcompanyid: '',
  fcompanyName: '',
  fnote: '',
  // 只读
  fstatusName: '',
  cuserName: '', cYmd: '',
  fcheckerName: '', fcheckdate: '',
  muserName: '', mYmd: '',
  fdisableName: '', fdisabledate: ''
}

const form = reactive({ ...defaultForm })
const isReadonly = computed(() => isEdit.value && form.fStatus === 40)

const rules: FormRules = {
  fdate: [{ required: true, message: '请选择单据日期', trigger: 'change' }],
  fmaterialid: [{ required: true, message: '请选择产品代码', trigger: 'change' }],
  funitid: [{ required: true, message: '请选择单位', trigger: 'change' }],
  fqty: [{ required: true, message: '请输入生产数量', trigger: 'change' }]
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
  freplacegroup: 0, fmaterialtype: '1',
  fmaterialid: '', fmaterialNumber: '', fmaterialName: '', fSpecification: '', fisBatchManage: false, fisAuxEnabled: false,
  foperid: '', foperNumber: '', foperName: '',
  fbaseUnitId: '', fbaseUnitNumber: '', fbaseUnitName: '',
  fcommonunitid: '', fcommonunitNumber: '', fcommonunitName: '',
  flot: '', fauxpropid: '', fauxpropName: '',
  fuserate: 0, fnumerator: 0, fdenominator: 0, fscraprate: 0, ffixscrapqty: 0,
  fmustqty: 0, fpickedqty: 0, fnopickedqty: 0,
  fsenditemdate: null as any,
  fstockid: '', fstockNumber: '', fstockName: '', fisOpenLocation: false,
  fstocklocid: '', fstocklocName: '',
  fbackflush: 0, ffeedsn: false, fothersn: false,
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

// ===== 主表变更 =====
// 生产工单编号选中：带出行号 + 产品 + 数量 + 单位（fmoentryid 由 v-model 自动赋值）
const onMoChange = async (item: any) => {
  if (!item) {
    form.fmoid = ''; form.fmobillno = ''; form.fmoentryseq = 0
    return
  }
  form.fmoid = item.uid || ''
  form.fmobillno = item.fbillno || ''
  form.fmoentryseq = item.fentryid || 0
  if (item.fmaterialid) {
    form.fmaterialid = item.fmaterialid
    form.fmaterialNumber = item.fmaterialNumber || ''
    form.fmaterialName = item.fmaterialName || ''
    form.fSpecification = item.fSpecification || ''
    form.fqty = item.fqty || 0
    if (item.fbaseUnitId) { form.funitid = item.fbaseUnitId; form.funitName = item.fbaseUnitName || '' }
    await refreshHeaderAux(item.fmaterialid)
  }
}

// 产品代码（物料扩展）选中：带出名称/规格/基本单位 + 启用辅助属性（fmaterialid 由 v-model 自动赋值）
const onProductChange = async (item: any) => {
  form.fmaterialNumber = item?.fNumber || ''
  form.fmaterialName = item?.fName || ''
  form.fSpecification = item?.fSpecification || ''
  if (item?.fbaseUnitId) { form.funitid = item.fbaseUnitId; form.funitName = item.fbaseUnitName || '' }
  form.fauxpropid = ''
  form.fauxpropName = ''
  await refreshHeaderAux(item?.uid || '')
}

const refreshHeaderAux = async (materialId: string) => {
  form.fisAuxEnabled = false
  if (!materialId) return
  try {
    const res: any = await getMaterialAuxEnabled(materialId)
    form.fisAuxEnabled = !!res.data
  } catch { form.fisAuxEnabled = false }
}

const onMotypeChange = (item: any) => { form.fmotypeNumber = item?.fNumber || ''; form.fmotypeName = item?.fName || '' }
const onUnitChange = (item: any) => { form.funitName = item?.fName || ''; form.funitNumber = item?.fNumber || '' }
const onAuxChange = (item: any) => { form.fauxpropName = item?.fName || '' }

// ===== 明细变更 =====
const onLineMaterialChange = async (row: any, item: any) => {
  row.fmaterialNumber = item?.fNumber || ''
  row.fmaterialName = item?.fName || ''
  row.fSpecification = item?.fSpecification || ''
  row.fisBatchManage = !!item?.fisBatchManage
  row.fbaseUnitId = item?.fbaseUnitId || ''
  row.fbaseUnitNumber = item?.fbaseUnitNumber || ''
  row.fbaseUnitName = item?.fbaseUnitName || ''
  // 常用单位默认回落基本单位
  if (!row.fcommonunitid && item?.fbaseUnitId) {
    row.fcommonunitid = item.fbaseUnitId
    row.fcommonunitNumber = item.fbaseUnitNumber || ''
    row.fcommonunitName = item.fbaseUnitName || ''
  }
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
const onOperChange = (row: any, item: any) => { row.foperNumber = item?.fNumber || ''; row.foperName = item?.fName || '' }
const onCommonUnitChange = (row: any, item: any) => { row.fcommonunitNumber = item?.fNumber || ''; row.fcommonunitName = item?.fName || '' }
const onLineAuxChange = (row: any, item: any) => { row.fauxpropName = item?.fName || '' }
const onLineStockChange = (row: any, item: any) => {
  row.fstockNumber = item?.fNumber || ''
  row.fstockName = item?.fName || ''
  row.fisOpenLocation = !!item?.fIsOpenLocation
  row.fstocklocid = ''
  row.fstocklocName = ''
}
const onLineStockLocChange = (row: any, item: any) => { row.fstocklocName = item?.fName || '' }

const dateOrNull = (d?: string | null) => (fmtAuditDate(d || '') ? (d as string).slice(0, 10) : null)

async function loadDetail(id: string) {
  loading.value = true
  try {
    const res: any = await getProductionMaterialList(id)
    const d = res.data
    Object.keys(defaultForm).forEach(k => { if (d[k] !== undefined && d[k] !== null) (form as any)[k] = d[k] })
    form.uid = d.uid
    form.fStatus = d.fStatus
    form.fdate = d.fdate ? String(d.fdate).slice(0, 10) : ''
    lineItems.value = (d.entries || []).map((e: ProductionMaterialListEntry) => ({
      ...e,
      fsenditemdate: dateOrNull(e.fsenditemdate),
      _key: nextKey()
    }))
    selectedLineIndex.value = lineItems.value.length > 0 ? 0 : -1
  } catch (e) {
    console.error('加载生产用料清单失败:', e)
    ElMessage.error('加载生产用料清单失败')
  } finally {
    loading.value = false
  }
}

function buildPayload() {
  return {
    fbillno: form.fbillno,
    fdate: form.fdate,
    fmobillno: form.fmobillno,
    fmoid: form.fmoid,
    fmoentryid: form.fmoentryid,
    fmoentryseq: form.fmoentryseq || 0,
    fmaterialid: form.fmaterialid,
    fauxpropid: form.fauxpropid,
    fmotype: form.fmotype,
    fqty: form.fqty || 0,
    funitid: form.funitid,
    fcompanyid: form.fcompanyid,
    fnote: form.fnote,
    entries: lineItems.value.filter(it => it.fmaterialid).map(it => ({
      freplacegroup: it.freplacegroup || 0,
      fmaterialtype: it.fmaterialtype || '1',
      fmaterialid: it.fmaterialid || '',
      foperid: it.foperid || '',
      fbaseUnitId: it.fbaseUnitId || '',
      fcommonunitid: it.fcommonunitid || '',
      flot: it.flot || '',
      fauxpropid: it.fauxpropid || '',
      fuserate: it.fuserate || 0,
      fnumerator: it.fnumerator || 0,
      fdenominator: it.fdenominator || 0,
      fscraprate: it.fscraprate || 0,
      ffixscrapqty: it.ffixscrapqty || 0,
      fmustqty: it.fmustqty || 0,
      fsenditemdate: it.fsenditemdate || null,
      fstockid: it.fstockid || '',
      fstocklocid: it.fstocklocid || '',
      fbackflush: it.fbackflush || 0,
      ffeedsn: !!it.ffeedsn,
      fothersn: !!it.fothersn,
      fnote: it.fnote || ''
    }))
  }
}

async function handleSave() {
  if (!formRef.value) return
  try { await formRef.value.validate() } catch { return }
  const payload = buildPayload()
  if (payload.entries.length === 0) { ElMessage.warning('请至少添加一条用料明细'); return }
  loading.value = true
  try {
    if (isEdit.value) {
      await updateProductionMaterialList(uid.value, payload)
      ElMessage.success('保存成功')
      await loadDetail(uid.value)
    } else {
      const res: any = await createProductionMaterialList(payload)
      ElMessage.success('创建成功')
      const newUid = res?.data?.uid
      if (newUid) {
        uid.value = newUid
        router.replace({ name: 'ProductionMaterialListEdit', query: { uid: newUid } })
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
const handleApprove = () => runStatus(approveProductionMaterialList, '审核成功')
const handleUnapprove = () => runStatus(unapproveProductionMaterialList, '反审核成功')
const handleBack = () => router.push({ name: 'ProductionMaterialListList' })

onMounted(async () => {
  if (!orgStore.loaded) await orgStore.loadOrgs()
  if (isEdit.value) await loadDetail(uid.value)
  else if (!form.fcompanyid) form.fcompanyid = orgStore.currentOrgId
})
</script>

<style scoped>
.ppbom-edit-container {
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
/* 表单标签不换行（避免“生产工单编号”等 6 字标签折成两行） */
.edit-form :deep(.el-form-item__label) {
  white-space: nowrap;
}
</style>
