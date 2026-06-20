<template>
  <div class="rn-edit-container">
    <!-- 顶部工具栏 -->
    <div class="edit-toolbar">
      <el-button type="primary" :disabled="isReadonly" @click="handleSave"
                 v-permission="isEdit ? 'receivenotice:edit' : 'receivenotice:add'">
        <el-icon><Check /></el-icon> 保存
      </el-button>
      <el-button v-if="isEdit && form.fStatus === 10" type="success" @click="handleApprove" v-permission="'receivenotice:approve'">
        <el-icon><CircleCheck /></el-icon> 审核
      </el-button>
      <el-button v-if="isEdit && form.fStatus === 40" type="warning" @click="handleUnapprove" v-permission="'receivenotice:approve'">
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
              <LookupSelect v-model="form.fbilltypeid" module="billtype" parent-id="PUR_ReceiveBill" placeholder="请选择单据类型" preload />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="单据编号">
              <el-input v-model="form.fbillno" :disabled="isEdit" placeholder="保存后自动生成" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="收料日期" prop="fdate">
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
                  <el-option v-for="t in businessTypes" :key="t" :label="t" :value="t" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="源单类型">
                <el-select v-model="form.fsrcformid" placeholder="源单类型" clearable style="width:100%" @change="onSrcFormChange">
                  <el-option v-for="t in srcFormTypes" :key="t.value" :label="t.label" :value="t.value" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="源单编号">
                <PurchaseOrderLookup v-if="form.fsrcformid === 'PUR_PurchaseOrder'"
                                     v-model="form.fsrcbillno" :display-text="form.fsrcbillno"
                                     :disabled="isReadonly" placeholder="选择采购订单"
                                     @change="onSrcOrderChange" />
                <el-input v-else v-model="form.fsrcbillno" placeholder="源单编号" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="8">
              <el-form-item label="需求组织">
                <el-select v-model="form.fdemandorgid" placeholder="需求组织" style="width:100%">
                  <el-option v-for="o in demandOrgOptions" :key="o.orgId" :label="o.orgName" :value="o.orgId" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="采购组织">
                <el-select v-model="form.fpurorgid" placeholder="采购组织" style="width:100%">
                  <el-option v-for="o in purOrgOptions" :key="o.orgId" :label="o.orgName" :value="o.orgId" />
                </el-select>
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
              <el-form-item label="收料部门">
                <LookupSelect v-model="form.freceivedeptid" module="department" placeholder="请选择收料部门" preload />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="收料员">
                <LookupSelect v-model="form.freceiverid" module="employee" placeholder="请选择收料员" preload />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="交易币别" prop="fsettlecurrid">
                <LookupSelect v-model="form.fsettlecurrid" module="currency" placeholder="请选择币别" preload />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="8">
              <el-form-item label="采购部门">
                <LookupSelect v-model="form.fpurdeptid" module="department" placeholder="请选择采购部门" preload />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="采购员">
                <LookupSelect v-model="form.fpurchaserid" module="employee" placeholder="请选择采购员" preload />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="汇率类型">
                <el-select v-model="form.fexchangetypeid" placeholder="汇率类型" style="width:100%">
                  <el-option v-for="t in exchangeTypes" :key="t" :label="t" :value="t" />
                </el-select>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="8">
              <el-form-item label="汇率">
                <el-input-number v-model="form.fexchangerate" :min="0" :precision="4" :controls="false" style="width:100%" />
              </el-form-item>
            </el-col>
            <el-col :span="16">
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

      <!-- 收料通知单明细 -->
      <div class="grid-section">
        <div class="grid-toolbar">
          <span class="grid-title">收料通知单明细</span>
          <el-button type="primary" size="small" :disabled="isReadonly" @click="addLine"><el-icon><Plus /></el-icon> 行新增</el-button>
          <el-button size="small" :disabled="isReadonly" @click="insertLine">行插入</el-button>
          <el-button size="small" type="danger" :disabled="isReadonly || selectedLineIndex < 0" @click="deleteLine">行删除</el-button>
        </div>

        <el-table :data="lineItems" border size="small" row-key="_key" highlight-current-row
                  :row-class-name="lineRowClass" @row-click="onLineRowClick" empty-text="暂无明细，点击“行新增”添加">
          <el-table-column type="index" label="行号" width="55" align="center" fixed="left" />
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
          <el-table-column label="来料检验" width="80" align="center">
            <template #default="{ row }"><el-checkbox v-model="row.fcheckincoming" :disabled="isReadonly" /></template>
          </el-table-column>
          <el-table-column label="启用批次管理" width="100" align="center">
            <template #default="{ row }"><el-checkbox :model-value="!!row.fisBatchManage" disabled /></template>
          </el-table-column>
          <el-table-column label="批次" width="120">
            <template #default="{ row }"><el-input v-model="row.flot" :disabled="isReadonly" /></template>
          </el-table-column>
          <el-table-column label="供应商批号" width="120">
            <template #default="{ row }"><el-input v-model="row.fsupplylot" :disabled="isReadonly" /></template>
          </el-table-column>
          <el-table-column label="交货数量" width="110">
            <template #default="{ row }">
              <el-input-number v-model="row.factreceiveqty" :min="0" :precision="4" :controls="false" :disabled="isReadonly" style="width:100%" @change="() => recalc(row)" />
            </template>
          </el-table-column>
          <el-table-column prop="finstockqty" label="累计入库" width="90" align="right" />
          <el-table-column label="检验合格数量" width="110">
            <template #default="{ row }">
              <el-input-number v-model="row.fgodqty" :min="0" :precision="4" :controls="false" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column label="样本破坏数" width="100">
            <template #default="{ row }">
              <el-input-number v-model="row.fscrapqty" :min="0" :precision="4" :controls="false" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column label="采购单位" width="120">
            <template #default="{ row }">
              <LookupSelect :key="'u'+row._key" v-model="row.funitid" module="unit" placeholder="单位" :disabled="isReadonly" preload
                            @change="(it: any) => onUnitChange(row, it)" />
            </template>
          </el-table-column>
          <el-table-column prop="funitName" label="采购单位名称" width="110" />
          <el-table-column label="仓库" width="150">
            <template #default="{ row }">
              <LookupSelect :key="'s'+row._key" v-model="row.fstockid" module="warehouse" placeholder="仓库" :disabled="isReadonly" preload
                            @change="(it: any) => onStockChange(row, it)" />
            </template>
          </el-table-column>
          <el-table-column prop="fstockName" label="仓库名称" width="110" />
          <el-table-column label="仓位" width="150">
            <template #default="{ row }">
              <LookupSelect :key="'l'+row._key" v-model="row.fstocklocid" module="stockplace" :parent-id="row.fstockid"
                            placeholder="先选仓库" :disabled="isReadonly" preload
                            @change="(it: any) => onStockLocChange(row, it)" />
            </template>
          </el-table-column>
          <el-table-column label="货主类型" width="120">
            <template #default="{ row }">
              <el-select v-model="row.fownertypeid" placeholder="货主类型" :disabled="isReadonly" clearable
                         style="width:100%" @change="() => onOwnerTypeChange(row)">
                <el-option v-for="o in keeperOwnerTypeOptions" :key="o.value" :label="o.label" :value="o.value" />
              </el-select>
            </template>
          </el-table-column>
          <el-table-column label="货主" width="160">
            <template #default="{ row }">
              <LookupSelect :key="'own'+row._key+row.fownertypeid" v-model="row.fownerid"
                            :module="typeToModule(row.fownertypeid)" placeholder="先选货主类型"
                            :disabled="isReadonly || !row.fownertypeid" preload />
            </template>
          </el-table-column>
          <el-table-column label="保管者类型" width="120">
            <template #default="{ row }">
              <el-select v-model="row.fkeepertypeid" placeholder="保管者类型" :disabled="isReadonly" clearable
                         style="width:100%" @change="() => onKeeperTypeChange(row)">
                <el-option v-for="o in keeperOwnerTypeOptions" :key="o.value" :label="o.label" :value="o.value" />
              </el-select>
            </template>
          </el-table-column>
          <el-table-column label="保管者" width="160">
            <template #default="{ row }">
              <LookupSelect :key="'kp'+row._key+row.fkeepertypeid" v-model="row.fkeeperid"
                            :module="typeToModule(row.fkeepertypeid)" placeholder="先选保管者类型"
                            :disabled="isReadonly || !row.fkeepertypeid" preload />
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
          <el-table-column label="税额" width="110" align="right">
            <template #default="{ row }">{{ fmtNum(row.ftaxamount) }}</template>
          </el-table-column>
          <el-table-column label="金额" width="110" align="right">
            <template #default="{ row }">{{ fmtNum(row.famount) }}</template>
          </el-table-column>
          <el-table-column label="价税合计" width="120" align="right">
            <template #default="{ row }">{{ fmtNum(row.fallamount) }}</template>
          </el-table-column>
          <el-table-column label="预计到货日期" width="140">
            <template #default="{ row }">
              <el-date-picker v-model="row.fpredeliverydate" type="date" value-format="YYYY-MM-DD" placeholder="日期" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column label="采购日期" width="140">
            <template #default="{ row }">
              <el-date-picker v-model="row.fkfdate" type="date" value-format="YYYY-MM-DD" placeholder="日期" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column label="有效期至" width="140">
            <template #default="{ row }">
              <el-date-picker v-model="row.fexpiredate" type="date" value-format="YYYY-MM-DD" placeholder="日期" :disabled="isReadonly" style="width:100%" />
            </template>
          </el-table-column>
          <el-table-column label="启用保质期" width="90" align="center">
            <template #default="{ row }"><el-checkbox :model-value="!!row.fisKfPeriod" disabled /></template>
          </el-table-column>
          <el-table-column prop="fKfPeriod" label="保质期限" width="90" align="right" />
          <el-table-column label="保质期单位" width="90" align="center">
            <template #default="{ row }">{{ kfUnitLabel(row.fKfUnit) }}</template>
          </el-table-column>
          <el-table-column label="订单编号" width="150">
            <template #default="{ row }"><el-input v-model="row.forderbillno" :disabled="isReadonly" /></template>
          </el-table-column>
          <el-table-column label="备注" min-width="140">
            <template #default="{ row }"><el-input v-model="row.frepnote" :disabled="isReadonly" /></template>
          </el-table-column>
        </el-table>
      </div>
    </el-form>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import type { FormInstance, FormRules } from 'element-plus'
import { Check, Back, Plus, CircleCheck, RefreshLeft } from '@element-plus/icons-vue'
import {
  getReceiveNotice, createReceiveNotice, updateReceiveNotice,
  approveReceiveNotice, unapproveReceiveNotice, getMaterialAuxEnabled, type ReceiveNoticeEntry
} from '../../api/receiveNotice'
import { getPurchaseOrder } from '../../api/purchaseOrder'
import { formatDate } from '../../utils/format'
import { useOrgStore } from '../../stores/org'
import LookupSelect from '../../components/LookupSelect.vue'
import MaterialLookup from '../../components/MaterialLookup.vue'
import PurchaseOrderLookup from '../../components/PurchaseOrderLookup.vue'

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
const businessTypes = ['标准采购', '委外采购', '直运采购', '资产采购', '费用采购', '现购']
const exchangeTypes = ['固定汇率', '浮动汇率']
// 源单类型：目前仅采购订单；选中采购订单时源单编号用弹窗选择采购订单
const srcFormTypes = [{ label: '采购订单', value: 'PUR_PurchaseOrder' }]

const defaultForm = {
  uid: '',
  fStatus: 0,
  fbillno: '',
  fbilltypeid: '',
  fdate: formatDate(new Date().toISOString()).slice(0, 10),
  fbusinesstype: '标准采购',
  fsrcformid: '',
  fsrcbillno: '',
  fdemandorgid: '',
  fdemandorgName: '',
  fpurorgid: '',
  fpurorgName: '',
  freceivedeptid: '',
  fpurdeptid: '',
  freceiverid: '',
  fpurchaserid: '',
  fsupplyid: '',
  fsettlecurrid: '',
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
  fdate: [{ required: true, message: '请选择收料日期', trigger: 'change' }],
  fsupplyid: [{ required: true, message: '请选择供应商', trigger: 'change' }],
  fsettlecurrid: [{ required: true, message: '请选择交易币别', trigger: 'change' }]
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
// 服务端日期回填：1900 哨兵视为空，否则取 YYYY-MM-DD
const toDateInput = (v?: string | null) => (fmtAuditDate(v || '') ? String(v).slice(0, 10) : null)

// 组织下拉：编辑态若单据组织不在当前用户可选组织里，补一项避免显示空白
const buildOrgOptions = (id: string, name: string) => {
  const list: any[] = [...orgStore.orgs]
  if (id && !list.some(o => o.orgId === id)) list.unshift({ orgId: id, orgName: name || id })
  return list
}
const demandOrgOptions = computed(() => buildOrgOptions(form.fdemandorgid, form.fdemandorgName))
const purOrgOptions = computed(() => buildOrgOptions(form.fpurorgid, form.fpurorgName))

const lineRowClass = ({ rowIndex }: { rowIndex: number }) => (rowIndex === selectedLineIndex.value ? 'cur-row' : '')
const onLineRowClick = (row: any) => { selectedLineIndex.value = lineItems.value.indexOf(row) }

const newLine = () => ({
  fmaterialid: '', fmaterialNumber: '', fmaterialName: '', fSpecification: '',
  fisBatchManage: false, fisKfPeriod: false, fKfPeriod: 0, fKfUnit: 0,
  fauxpropid: '', fauxpropName: '', fisAuxEnabled: false,
  fcheckincoming: false, flot: '', fsupplylot: '',
  factreceiveqty: 0, finstockqty: 0, fgodqty: 0, fscrapqty: 0,
  funitid: '', funitNumber: '', funitName: '', fbaseunitqty: 0,
  fstockid: '', fstockNumber: '', fstockName: '', fisOpenLocation: false,
  fstocklocid: '', fstocklocName: '',
  fkeepertypeid: '', fkeeperid: '', fownertypeid: '', fownerid: '',
  fprice: 0, ftaxrate: 0, ftaxprice: 0, fdiscountrate: 0, ftaxamount: 0, famount: 0, fallamount: 0,
  fpredeliverydate: null as any, fkfdate: null as any, fexpiredate: null as any,
  forderbillno: '', forderentryid: 0, forderinterid: '', forderdetailid: '',
  frepnote: '', _key: nextKey()
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
  const qty = Number(row.factreceiveqty) || 0
  const price = Number(row.fprice) || 0
  const rate = Number(row.ftaxrate) || 0
  const disc = Number(row.fdiscountrate) || 0
  row.famount = +(qty * price * (1 - disc / 100)).toFixed(2)   // 金额 = 数量×单价×(1-折扣率%)
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
const onStockChange = (row: any, item: any) => {
  row.fstockName = item?.fName || ''
  row.fstockNumber = item?.fNumber || ''
  // 切换仓库后清空仓位（仓位隶属仓库）
  row.fstocklocid = ''
  row.fstocklocName = ''
}
const onStockLocChange = (row: any, item: any) => { row.fstocklocName = item?.fName || '' }
// 货主/保管者类型：业务组织(BD_OwnerOrg)/供应商(BD_Supplier)/客户(BD_Customer)；货主与保管者同一套类型
// 类型决定取值来源表：BD_OwnerOrg→SYS_ORGSTRUCTURE(org)、BD_Supplier→T_BD_SUPPLIER(supplier)、BD_Customer→T_BD_CUSTOMER(customer)
const keeperOwnerTypeOptions = [
  { label: '业务组织', value: 'BD_OwnerOrg' },
  { label: '供应商', value: 'BD_Supplier' },
  { label: '客户', value: 'BD_Customer' },
]
const typeToModule = (t?: string) => (t === 'BD_Supplier' ? 'supplier' : t === 'BD_Customer' ? 'customer' : 'org')
const onOwnerTypeChange = (row: any) => { row.fownerid = '' }
const onKeeperTypeChange = (row: any) => { row.fkeeperid = '' }
// 切换源单类型后清空已选源单编号，避免残留不匹配的旧值
const onSrcFormChange = () => { form.fsrcbillno = '' }

// 选中采购订单后下推：拉取该采购订单全部明细，映射成收料通知单明细带入（并回写源单追溯字段）
const onSrcOrderChange = async (order: any) => {
  if (!order?.uid) return
  // 若已有录入明细，先确认是否用采购订单明细替换
  if (lineItems.value.some(it => it.fmaterialid)) {
    const ok = await ElMessageBox.confirm('选择采购订单将用其明细替换当前收料明细，是否继续？', '下推采购订单', { type: 'warning' })
      .then(() => true).catch(() => false)
    if (!ok) return
  }
  loading.value = true
  try {
    const res: any = await getPurchaseOrder(order.uid)
    const po = res.data
    if (!po) return
    // 表头带入：供应商/币别/采购员/采购部门仅当为空时补入（不覆盖用户已填）；业务类型/汇率随采购订单
    if (!form.fsupplyid && po.fsupplyid) form.fsupplyid = po.fsupplyid
    if (!form.fsettlecurrid && po.fsettlecurrid) form.fsettlecurrid = po.fsettlecurrid
    if (!form.fpurchaserid && po.fpurchaserid) form.fpurchaserid = po.fpurchaserid
    if (!form.fpurdeptid && po.fpurchasedeptid) form.fpurdeptid = po.fpurchasedeptid
    if (po.fbusinesstype) form.fbusinesstype = po.fbusinesstype
    if (po.fexchangetypeid) form.fexchangetypeid = po.fexchangetypeid
    if (po.fexchangerate) form.fexchangerate = po.fexchangerate

    const poEntries = po.entries || []
    lineItems.value = poEntries.map((e: any) => {
      const line: any = {
        ...newLine(),
        fmaterialid: e.fmaterialid || '', fmaterialNumber: e.fmaterialNumber || '', fmaterialName: e.fmaterialName || '', fSpecification: e.fSpecification || '',
        fisBatchManage: !!e.fisBatchManage, fisKfPeriod: !!e.fisKfPeriod, fKfPeriod: e.fKfPeriod || 0, fKfUnit: e.fKfUnit || 0,
        fauxpropid: e.fauxpropid || '', fauxpropName: e.fauxpropName || '', fisAuxEnabled: !!e.fisAuxEnabled,
        flot: e.flot || '', fsupplylot: e.fsupplierlot || '',
        funitid: e.funitid || '', funitNumber: e.funitNumber || '', funitName: e.funitName || '',
        factreceiveqty: e.fqty || 0,
        fprice: e.fprice || 0, ftaxrate: e.ftaxrate || 0, fdiscountrate: e.fdiscountrate || 0,
        fpredeliverydate: toDateInput(e.fdeliverydate),
        // 源单追溯
        forderbillno: po.fbillno || '', forderentryid: e.fentryid || 0, forderinterid: po.uid || '', forderdetailid: e.uid || ''
      }
      recalc(line)
      return line
    })
    selectedLineIndex.value = lineItems.value.length > 0 ? 0 : -1
    if (lineItems.value.length) ElMessage.success(`已带入 ${lineItems.value.length} 条采购订单明细`)
    else ElMessage.warning('该采购订单无可下推的明细')
  } catch (e) {
    console.error('下推采购订单失败:', e)
    ElMessage.error('带入采购订单明细失败')
  } finally {
    loading.value = false
  }
}

async function loadDetail(id: string) {
  loading.value = true
  try {
    const res: any = await getReceiveNotice(id)
    const d = res.data
    Object.keys(defaultForm).forEach(k => { if (d[k] !== undefined && d[k] !== null) (form as any)[k] = d[k] })
    form.uid = d.uid
    form.fStatus = d.fStatus
    // 日期框需 YYYY-MM-DD（服务端返回 ISO 串，截取日期部分）
    form.fdate = toDateInput(d.fdate) || ''
    lineItems.value = (d.entries || []).map((e: ReceiveNoticeEntry) => ({
      ...e,
      fpredeliverydate: toDateInput(e.fpredeliverydate),
      fkfdate: toDateInput(e.fkfdate),
      fexpiredate: toDateInput(e.fexpiredate),
      _key: nextKey()
    }))
    selectedLineIndex.value = lineItems.value.length > 0 ? 0 : -1
  } catch (e) {
    console.error('加载收料通知单失败:', e)
    ElMessage.error('加载收料通知单失败')
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
    fsrcformid: form.fsrcformid,
    fsrcbillno: form.fsrcbillno,
    fdemandorgid: form.fdemandorgid,
    fpurorgid: form.fpurorgid,
    freceivedeptid: form.freceivedeptid,
    fpurdeptid: form.fpurdeptid,
    freceiverid: form.freceiverid,
    fpurchaserid: form.fpurchaserid,
    fsupplyid: form.fsupplyid,
    fsettlecurrid: form.fsettlecurrid,
    fexchangetypeid: form.fexchangetypeid,
    fexchangerate: form.fexchangerate,
    fnote: form.fnote,
    entries: lineItems.value.filter(it => it.fmaterialid).map(it => ({
      fmaterialid: it.fmaterialid,
      fauxpropid: it.fauxpropid || '',
      fcheckincoming: !!it.fcheckincoming,
      flot: it.flot || '',
      fsupplylot: it.fsupplylot || '',
      factreceiveqty: it.factreceiveqty || 0,
      fgodqty: it.fgodqty || 0,
      fscrapqty: it.fscrapqty || 0,
      funitid: it.funitid || '',
      fbaseunitqty: it.fbaseunitqty || 0,
      fstockid: it.fstockid || '',
      fstocklocid: it.fstocklocid || '',
      fkeepertypeid: it.fkeepertypeid || '',
      fkeeperid: it.fkeeperid || '',
      fownertypeid: it.fownertypeid || '',
      fownerid: it.fownerid || '',
      fprice: it.fprice || 0,
      ftaxrate: it.ftaxrate || 0,
      ftaxprice: it.ftaxprice || 0,
      fdiscountrate: it.fdiscountrate || 0,
      ftaxamount: it.ftaxamount || 0,
      famount: it.famount || 0,
      fallamount: it.fallamount || 0,
      fpredeliverydate: it.fpredeliverydate || null,
      fkfdate: it.fkfdate || null,
      fexpiredate: it.fexpiredate || null,
      forderbillno: it.forderbillno || '',
      forderentryid: it.forderentryid || 0,
      forderinterid: it.forderinterid || '',
      forderdetailid: it.forderdetailid || '',
      frepnote: it.frepnote || ''
    }))
  }
}

async function handleSave() {
  if (!formRef.value) return
  try { await formRef.value.validate() } catch { activeTab.value = 'basic'; return }
  // 提交前对每行重算金额，避免漏触发 @change 导致脏值（后端也会以服务端公式为准重算）
  lineItems.value.forEach(it => recalc(it))
  if (buildPayload().entries.length === 0) { ElMessage.warning('请至少添加一条明细'); return }
  loading.value = true
  try {
    if (isEdit.value) {
      await updateReceiveNotice(uid.value, buildPayload())
      ElMessage.success('保存成功')
      await loadDetail(uid.value)
    } else {
      const res: any = await createReceiveNotice(buildPayload())
      ElMessage.success('创建成功')
      const newUid = res?.data?.uid
      if (newUid) {
        uid.value = newUid
        router.replace({ name: 'ReceiveNoticeEdit', query: { uid: newUid } })
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
const handleApprove = () => runStatus(approveReceiveNotice, '审核成功')
const handleUnapprove = () => runStatus(unapproveReceiveNotice, '反审核成功')
const handleBack = () => router.push({ name: 'ReceiveNoticeList' })

onMounted(async () => {
  if (!orgStore.loaded) await orgStore.loadOrgs()
  if (isEdit.value) await loadDetail(uid.value)
  else {
    if (!form.fdemandorgid) form.fdemandorgid = orgStore.currentOrgId
    if (!form.fpurorgid) form.fpurorgid = orgStore.currentOrgId
  }
})
</script>

<style scoped>
.rn-edit-container {
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
