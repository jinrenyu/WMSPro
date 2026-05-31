<template>
  <div class="warehouse-edit-container">
    <!-- 顶部工具栏 -->
    <div class="edit-toolbar">
      <el-button type="primary" :disabled="isReadonly" @click="handleSave" v-permission="isEdit ? 'warehouse:edit' : 'warehouse:add'">
        <el-icon><Check /></el-icon> 保存
      </el-button>
      <el-button v-if="showApprove" type="success" @click="handleApprove" v-permission="'warehouse:approve'">审核</el-button>
      <el-button v-if="showUnapprove" type="warning" @click="handleUnapprove" v-permission="'warehouse:approve'">反审核</el-button>
      <el-button v-if="showDisable" type="info" @click="handleDisable" v-permission="'warehouse:disable'">禁用</el-button>
      <el-button v-if="showEnable" @click="handleEnable" v-permission="'warehouse:disable'">反禁用</el-button>
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
      label-width="100px"
      :disabled="isReadonly"
      class="edit-form"
      v-loading="loading"
    >
      <!-- 公共头部 -->
      <div class="form-header">
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="组织">
              <el-input :model-value="companyDisplayName" disabled placeholder="当前组织" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="仓库代码" prop="fNumber">
              <el-input v-model="form.fNumber" :disabled="isEdit" placeholder="编辑时不可修改" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="仓库名称" prop="fName">
              <el-input v-model="form.fName" placeholder="必填" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="仓库负责人">
              <LookupSelect v-model="form.fPrincipal" module="employee" placeholder="请选择负责人" preload />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="所在车间">
              <LookupSelect v-model="form.fWorkshopId" module="department" placeholder="请选择车间(部门)" preload />
            </el-form-item>
          </el-col>
        </el-row>
      </div>

      <!-- Tab 区 -->
      <el-tabs v-model="activeTab" class="edit-tabs">
        <!-- ============ 基本 ============ -->
        <el-tab-pane label="基本" name="basic">
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="仓库属性" prop="fStockProperty">
                <el-select v-model="form.fStockProperty" placeholder="请选择仓库属性" style="width: 100%">
                  <el-option v-for="o in stockPropertyOptions" :key="o.value" :label="o.label" :value="o.value" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="仓库类型" prop="fType">
                <el-select v-model="form.fType" placeholder="请选择仓库类型" style="width: 100%">
                  <el-option v-for="o in typeOptions" :key="o.value" :label="o.label" :value="o.value" />
                </el-select>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="库存状态类型" prop="fStockStatusType">
                <el-select v-model="stockStatusTypeArray" multiple collapse-tags collapse-tags-tooltip placeholder="请选择库存状态类型" style="width: 100%">
                  <el-option v-for="o in stockStatusTypeOptions" :key="o.value" :label="o.label" :value="o.value" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="默认库存状态">
                <el-select v-model="form.fDefStockStatusId" clearable placeholder="请选择默认库存状态" style="width: 100%">
                  <el-option v-for="o in stockStatusOptions" :key="o.uid" :label="o.fName" :value="o.uid" />
                </el-select>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="默认收料状态">
                <el-select v-model="form.fDefReceiveStatusId" clearable placeholder="请选择默认收料状态" style="width: 100%">
                  <el-option v-for="o in stockStatusOptions" :key="o.uid" :label="o.fName" :value="o.uid" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="是否保税">
                <el-checkbox v-model="form.fBonded" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="24">
              <el-form-item label="仓库地址">
                <el-input v-model="form.fAddress" type="textarea" :rows="2" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="联系电话">
                <el-input v-model="form.fTel" />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="分组">
                <el-tree-select
                  v-model="form.fGroupId"
                  :data="groupTree"
                  :props="{ label: 'fName', children: 'children', value: 'uid' }"
                  placeholder="请选择分组"
                  clearable
                  check-strictly
                  style="width: 100%"
                />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="24">
              <el-form-item label="仓库描述">
                <el-input v-model="form.fDescription" type="textarea" :rows="2" />
              </el-form-item>
            </el-col>
          </el-row>

          <!-- 库存控制 -->
          <div class="section-block">
            <div class="section-title">库存控制</div>
            <el-row :gutter="20">
              <el-col :span="8">
                <el-form-item label="允许负库存">
                  <el-checkbox v-model="form.fAllowMinusQty" />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="允许锁库">
                  <el-checkbox v-model="form.fAllowLock" />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="启用仓位管理">
                  <el-checkbox v-model="form.fIsOpenLocation" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="8">
                <el-form-item label="允许MRP计划">
                  <el-checkbox v-model="form.fAllowMrpPlan" />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="参与预警">
                  <el-checkbox v-model="form.fAvailableAlert" />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="是否虚仓">
                  <el-checkbox v-model="form.fIsVirtual" />
                </el-form-item>
              </el-col>
            </el-row>
          </div>
        </el-tab-pane>

        <!-- ============ 仓位信息（启用仓位管理时显示） ============ -->
        <el-tab-pane v-if="form.fIsOpenLocation" label="仓位信息" name="flex">
          <el-empty v-if="!isEdit" description="保存仓库后可维护仓位信息" />
          <template v-else>
            <!-- 仓位集 -->
            <div class="grid-toolbar">
              <span class="grid-title">仓位集</span>
              <el-button type="primary" size="small" :disabled="isReadonly" @click="addFlexItem">行新增</el-button>
              <el-button size="small" :disabled="isReadonly" @click="insertFlexItem">行插入</el-button>
              <el-button size="small" type="danger" :disabled="isReadonly || selectedItemIndex < 0" @click="deleteFlexItem">行删除</el-button>
            </div>
            <el-table
              :data="flexItems"
              border
              size="small"
              row-key="_key"
              :row-class-name="itemRowClass"
              empty-text="暂无仓位集，点击“行新增”添加"
              @row-click="onFlexItemRowClick"
            >
              <el-table-column type="index" label="行号" width="60" align="center" />
              <el-table-column label="仓位集代码" min-width="220">
                <template #default="{ row }">
                  <LookupSelect
                    :key="row._key"
                    v-model="row.fFlexId"
                    module="warehouse/flex-sets"
                    placeholder="选择仓位集"
                    preload
                    :disabled="isReadonly"
                    @change="(item: any) => onFlexItemChange(row, item)"
                  />
                </template>
              </el-table-column>
              <el-table-column label="仓位集名称" min-width="160">
                <template #default="{ row }">{{ row.fName }}</template>
              </el-table-column>
              <el-table-column label="仓位集描述" min-width="200">
                <template #default="{ row }">{{ row.fDescription }}</template>
              </el-table-column>
            </el-table>

            <!-- 仓位集值（所选仓位集的值）-->
            <div class="grid-toolbar" style="margin-top: 16px;">
              <span class="grid-title">仓位集值</span>
              <el-button type="primary" size="small" :disabled="isReadonly || !canEditDetails" @click="addFlexDetail">行新增</el-button>
              <el-button size="small" :disabled="isReadonly || !canEditDetails" @click="insertFlexDetail">行插入</el-button>
              <el-button size="small" type="danger" :disabled="isReadonly || !canEditDetails || selectedDetailIndex < 0" @click="deleteFlexDetail">行删除</el-button>
              <span v-if="selectedItemIndex < 0" class="grid-hint">请先选择上方仓位集</span>
              <span v-else-if="!canEditDetails" class="grid-hint">请先为该行选择仓位集</span>
            </div>
            <el-table
              :data="currentDetails"
              border
              size="small"
              row-key="_key"
              :row-class-name="detailRowClass"
              empty-text="暂无仓位集值"
              @row-click="onFlexDetailRowClick"
            >
              <el-table-column type="index" label="行号" width="60" align="center" />
              <el-table-column label="仓位集值代码" min-width="220">
                <template #default="{ row }">
                  <LookupSelect
                    :key="row._key"
                    v-model="row.fFlexEntryId"
                    module="warehouse/flex-set-values"
                    :parent-id="currentFlexId"
                    placeholder="选择仓位集值"
                    preload
                    :disabled="isReadonly"
                    @change="(item: any) => onFlexDetailChange(row, item)"
                  />
                </template>
              </el-table-column>
              <el-table-column label="仓位集值名称" min-width="160">
                <template #default="{ row }">{{ row.fName }}</template>
              </el-table-column>
              <el-table-column label="仓位集值描述" min-width="200">
                <template #default="{ row }">{{ row.fDescription }}</template>
              </el-table-column>
            </el-table>
          </template>
        </el-tab-pane>

        <!-- ============ 其他（只读系统信息） ============ -->
        <el-tab-pane label="其他" name="other">
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="制单人">
                <el-input :model-value="form.cUser" disabled />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="制单日期">
                <el-input :model-value="fmtAuditDate(form.cYmd)" disabled />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="修改人">
                <el-input :model-value="form.mUser" disabled />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="修改日期">
                <el-input :model-value="fmtAuditDate(form.mYmd)" disabled />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="审核人">
                <el-input :model-value="form.fCheckerId" disabled />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="审核日期">
                <el-input :model-value="fmtAuditDate(form.fCheckDate)" disabled />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="禁用人">
                <el-input :model-value="form.fdisableid" disabled />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="禁用日期">
                <el-input :model-value="fmtAuditDate(form.fdisabledate)" disabled />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="数据状态">
                <el-input :model-value="form.fStatus === 40 ? '已审核' : '未审核'" disabled />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="禁用">
                <el-checkbox :model-value="form.fDisabled" disabled />
              </el-form-item>
            </el-col>
          </el-row>
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
  getWarehouse, createWarehouse, updateWarehouse,
  approveWarehouse, unapproveWarehouse, disableWarehouse, enableWarehouse,
  getStockStatusLookup, getWarehouseFlex, saveWarehouseFlex,
  type WarehouseFlexItem, type WarehouseFlexDetail,
} from '../../api/warehouse'
import { getGroupTree, type BaseDataGroup } from '../../api/baseDataGroup'
import type { LookupItem } from '../../api/lookup'
import { formatDate } from '../../utils/format'
import { useOrgStore } from '../../stores/org'
import LookupSelect from '../../components/LookupSelect.vue'

const router = useRouter()
const route = useRoute()
const orgStore = useOrgStore()

const formRef = ref<FormInstance>()
const loading = ref(false)
const activeTab = ref('basic')
const groupTree = ref<BaseDataGroup[]>([])
const stockStatusOptions = ref<LookupItem[]>([])

// ---- 仓位信息（仓位集 → 仓位集值）----
const flexItems = ref<WarehouseFlexItem[]>([])
const selectedItemIndex = ref(-1)
const selectedDetailIndex = ref(-1)
const selectedItem = computed(() => (selectedItemIndex.value >= 0 ? flexItems.value[selectedItemIndex.value] : null))
const currentDetails = computed(() => selectedItem.value?.details ?? [])
const currentFlexId = computed(() => selectedItem.value?.fFlexId ?? '')
const canEditDetails = computed(() => selectedItemIndex.value >= 0 && !!currentFlexId.value)

const uid = ref<string>((route.query.uid as string) || '')
const isEdit = computed(() => !!uid.value)
const isReadonly = computed(() => isEdit.value && (form.fStatus === 40 || form.fDisabled))

// 仓库属性：1=普通仓库;2=车间仓库;3=供应商仓库;4=客户仓库;5=第三方仓储
const stockPropertyOptions = [
  { value: '1', label: '普通仓库' },
  { value: '2', label: '车间仓库' },
  { value: '3', label: '供应商仓库' },
  { value: '4', label: '客户仓库' },
  { value: '5', label: '第三方仓储' },
]

// 仓库类型：1=物料类;2=TPM类
const typeOptions = [
  { value: '1', label: '物料类' },
  { value: '2', label: 'TPM类' },
]

// 库存状态类型（多选）：0=可用;1=待检;2=冻结;3=退回冻结;4=在途;5=收货冻结;6=废品;7=不良;8=不参与核算
const stockStatusTypeOptions = [
  { value: '0', label: '可用' },
  { value: '1', label: '待检' },
  { value: '2', label: '冻结' },
  { value: '3', label: '退回冻结' },
  { value: '4', label: '在途' },
  { value: '5', label: '收货冻结' },
  { value: '6', label: '废品' },
  { value: '7', label: '不良' },
  { value: '8', label: '不参与核算' },
]

// 组织显示名：FCompanyId 记录所选组织的上级(公司)，故显示上级组织名。
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

// 库存状态类型 多选数组 <-> 逗号分隔字符串
const stockStatusTypeArray = computed<string[]>({
  get: () => (form.fStockStatusType ? form.fStockStatusType.split(',').filter(Boolean) : []),
  set: (v) => { form.fStockStatusType = (v || []).join(',') },
})

const rules: FormRules = {
  fNumber: [{ required: true, message: '请输入仓库代码', trigger: 'blur' }],
  fName: [{ required: true, message: '请输入仓库名称', trigger: 'blur' }],
  fStockProperty: [{ required: true, message: '请选择仓库属性', trigger: 'change' }],
  fType: [{ required: true, message: '请选择仓库类型', trigger: 'change' }],
  fStockStatusType: [{ required: true, message: '请选择库存状态类型', trigger: 'change' }],
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
  fPrincipal: '',
  fWorkshopId: '',
  // 基本
  fStockProperty: '',
  fType: '',
  fStockStatusType: '',
  fDefStockStatusId: '',
  fDefReceiveStatusId: '',
  fBonded: false,
  fAddress: '',
  fTel: '',
  fGroupId: '',
  fDescription: '',
  // 库存控制
  fAllowMinusQty: false,
  fAllowLock: false,
  fIsOpenLocation: false,
  fAllowMrpPlan: false,
  fAvailableAlert: false,
  fIsVirtual: false,
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

async function loadGroupTree() {
  try {
    const res: any = await getGroupTree('Warehouse')
    groupTree.value = res.data || []
  } catch (e) {
    console.error('加载仓库分组失败:', e)
  }
}

async function loadStockStatus() {
  try {
    const res: any = await getStockStatusLookup({ maxCount: 200 })
    stockStatusOptions.value = res.data || []
  } catch (e) {
    console.error('加载库存状态失败:', e)
  }
}

// ---- 仓位信息 ----
// 客户端行 key（稳定唯一，仅用于 el-table row-key 与 LookupSelect :key，避免实例跨行复用引发的破坏性副作用；不入库）
let rowKeySeq = 0
const nextKey = () => ++rowKeySeq

async function loadFlex(id: string) {
  try {
    const res: any = await getWarehouseFlex(id)
    flexItems.value = (res.data || []).map((it: any) => ({
      uid: it.uid,
      fFlexId: it.fFlexId || '',
      fNumber: it.fNumber || '',
      fName: it.fName || '',
      fDescription: it.fDescription || '',
      fIsMustInput: it.fIsMustInput || false,
      _key: nextKey(),
      details: (it.details || []).map((d: any) => ({
        uid: d.uid,
        fFlexEntryId: d.fFlexEntryId || '',
        fNumber: d.fNumber || '',
        fName: d.fName || '',
        fDescription: d.fDescription || '',
        _key: nextKey(),
      })),
    }))
    selectedItemIndex.value = flexItems.value.length > 0 ? 0 : -1
    selectedDetailIndex.value = -1
  } catch (e) {
    console.error('加载仓位信息失败:', e)
  }
}

const newFlexItem = (): WarehouseFlexItem => ({ fFlexId: '', fNumber: '', fName: '', fDescription: '', fIsMustInput: false, details: [], _key: nextKey() })
const newFlexDetail = (): WarehouseFlexDetail => ({ fFlexEntryId: '', fNumber: '', fName: '', fDescription: '', _key: nextKey() })

// 选中高亮：以 selectedItemIndex/selectedDetailIndex 为唯一来源，行高亮由其派生（不用 el-table 内部 current-row，避免删除时 current-change(null) 覆盖索引）
const itemRowClass = ({ rowIndex }: { rowIndex: number }) => (rowIndex === selectedItemIndex.value ? 'cur-row' : '')
const detailRowClass = ({ rowIndex }: { rowIndex: number }) => (rowIndex === selectedDetailIndex.value ? 'cur-row' : '')
const onFlexItemRowClick = (row: WarehouseFlexItem) => {
  selectedItemIndex.value = flexItems.value.indexOf(row)
  selectedDetailIndex.value = -1
}
const onFlexDetailRowClick = (row: WarehouseFlexDetail) => {
  selectedDetailIndex.value = currentDetails.value.indexOf(row)
}

const addFlexItem = () => {
  flexItems.value.push(newFlexItem())
  selectedItemIndex.value = flexItems.value.length - 1
  selectedDetailIndex.value = -1
}
const insertFlexItem = () => {
  const at = selectedItemIndex.value >= 0 ? selectedItemIndex.value : flexItems.value.length
  flexItems.value.splice(at, 0, newFlexItem())
  selectedItemIndex.value = at
  selectedDetailIndex.value = -1
}
const deleteFlexItem = () => {
  if (selectedItemIndex.value < 0) return
  flexItems.value.splice(selectedItemIndex.value, 1)
  selectedItemIndex.value = flexItems.value.length > 0 ? Math.min(selectedItemIndex.value, flexItems.value.length - 1) : -1
  selectedDetailIndex.value = -1
}
const onFlexItemChange = (row: WarehouseFlexItem, item: LookupItem | null) => {
  row.fNumber = item?.fNumber || ''
  row.fName = item?.fName || ''
  row.fDescription = ''
  // 切换仓位集后原有值不再属于该集，清空其值明细
  row.details = []
  selectedDetailIndex.value = -1
}

const addFlexDetail = () => {
  if (!canEditDetails.value || !selectedItem.value) return
  selectedItem.value.details.push(newFlexDetail())
  selectedDetailIndex.value = selectedItem.value.details.length - 1
}
const insertFlexDetail = () => {
  if (!canEditDetails.value || !selectedItem.value) return
  const at = selectedDetailIndex.value >= 0 ? selectedDetailIndex.value : selectedItem.value.details.length
  selectedItem.value.details.splice(at, 0, newFlexDetail())
  selectedDetailIndex.value = at
}
const deleteFlexDetail = () => {
  if (!selectedItem.value || selectedDetailIndex.value < 0) return
  selectedItem.value.details.splice(selectedDetailIndex.value, 1)
  selectedDetailIndex.value = selectedItem.value.details.length > 0
    ? Math.min(selectedDetailIndex.value, selectedItem.value.details.length - 1) : -1
}
const onFlexDetailChange = (row: WarehouseFlexDetail, item: LookupItem | null) => {
  row.fNumber = item?.fNumber || ''
  row.fName = item?.fName || ''
  row.fDescription = ''
}

function buildFlexPayload() {
  return flexItems.value
    .filter((it) => it.fFlexId)
    .map((it) => ({
      fFlexId: it.fFlexId,
      fIsMustInput: it.fIsMustInput || false,
      details: (it.details || []).filter((d) => d.fFlexEntryId).map((d) => ({ fFlexEntryId: d.fFlexEntryId })),
    }))
}

async function loadDetail(id: string) {
  loading.value = true
  try {
    const res: any = await getWarehouse(id)
    const d = res.data
    Object.keys(defaultForm).forEach((k) => {
      if (d[k] !== undefined && d[k] !== null) {
        ;(form as any)[k] = d[k]
      }
    })
    form.uid = d.uid
    // 历史/种子数据枚举值不在选项集合内时清空/过滤，避免下拉空白却因“非空”使 required 误判通过，强制用户重选
    if (!stockPropertyOptions.some((o) => o.value === form.fStockProperty)) form.fStockProperty = ''
    if (!typeOptions.some((o) => o.value === form.fType)) form.fType = ''
    const validStatusTypes = new Set(stockStatusTypeOptions.map((o) => o.value))
    form.fStockStatusType = (form.fStockStatusType || '')
      .split(',')
      .filter((v) => v && validStatusTypes.has(v))
      .join(',')
  } catch (e) {
    console.error('加载仓库详情失败:', e)
    ElMessage.error('加载仓库详情失败')
  } finally {
    loading.value = false
  }
}

function buildPayload() {
  return {
    fNumber: form.fNumber,
    fName: form.fName,
    fCompanyId: form.fCompanyId,
    fPrincipal: form.fPrincipal,
    fWorkshopId: form.fWorkshopId,
    fStockProperty: form.fStockProperty,
    fType: form.fType,
    fStockStatusType: form.fStockStatusType,
    fDefStockStatusId: form.fDefStockStatusId,
    fDefReceiveStatusId: form.fDefReceiveStatusId,
    fBonded: form.fBonded,
    fAddress: form.fAddress,
    fTel: form.fTel,
    fGroupId: form.fGroupId,
    fDescription: form.fDescription,
    fAllowMinusQty: form.fAllowMinusQty,
    fAllowLock: form.fAllowLock,
    fIsOpenLocation: form.fIsOpenLocation,
    fAllowMrpPlan: form.fAllowMrpPlan,
    fAvailableAlert: form.fAvailableAlert,
    fIsVirtual: form.fIsVirtual,
  }
}

async function handleSave() {
  if (!formRef.value) return
  try {
    await formRef.value.validate()
  } catch {
    activeTab.value = 'basic'
    return
  }
  const payload = buildPayload()
  loading.value = true
  try {
    if (isEdit.value) {
      const { fNumber, fCompanyId, ...updateData } = payload
      await updateWarehouse(uid.value, updateData)
      if (form.fIsOpenLocation) await saveWarehouseFlex(uid.value, buildFlexPayload())
      ElMessage.success('保存成功')
      await loadDetail(uid.value)
      if (form.fIsOpenLocation) await loadFlex(uid.value)
    } else {
      const res: any = await createWarehouse(payload)
      ElMessage.success('创建成功')
      const newUid = res?.data?.uid
      if (newUid) {
        uid.value = newUid
        router.replace({ name: 'WarehouseEdit', query: { uid: newUid } })
        await loadDetail(newUid)
        if (form.fIsOpenLocation) await loadFlex(newUid)
      } else {
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

const handleApprove = () => runStatusAction(approveWarehouse, '审核成功')
const handleUnapprove = () => runStatusAction(unapproveWarehouse, '反审核成功')
const handleDisable = () => runStatusAction(disableWarehouse, '禁用成功')
const handleEnable = () => runStatusAction(enableWarehouse, '反禁用成功')

const handleBack = () => {
  router.push({ name: 'WarehouseList' })
}

onMounted(async () => {
  if (!orgStore.loaded) orgStore.loadOrgs()
  await Promise.all([loadGroupTree(), loadStockStatus()])
  if (isEdit.value) {
    await loadDetail(uid.value)
    if (form.fIsOpenLocation) await loadFlex(uid.value)
  } else {
    // 新增：默认使用当前切换的组织
    form.fCompanyId = orgStore.currentOrgId
    if (route.query.groupId) form.fGroupId = route.query.groupId as string
  }
})
</script>

<style scoped>
.warehouse-edit-container {
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

.toolbar-spacer {
  flex: 1;
}

.back-btn {
  margin-left: 8px;
}

.edit-form {
  padding: 16px 24px 24px;
  overflow-y: auto;
}

.form-header {
  padding-bottom: 8px;
  border-bottom: 1px dashed var(--border-color);
  margin-bottom: 8px;
}

.edit-tabs {
  margin-top: 8px;
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

/* 标签保持单行 */
.edit-form :deep(.el-form-item__label) {
  white-space: nowrap;
}

/* 仓位信息表格工具栏 */
.grid-toolbar {
  display: flex;
  align-items: center;
  gap: 8px;
  margin-bottom: 8px;
}

.grid-title {
  font-weight: 600;
  margin-right: 8px;
  color: var(--el-text-color-primary);
}

.grid-hint {
  color: var(--el-text-color-secondary);
  font-size: 12px;
}

/* 选中行高亮（由 selectedIndex 派生的 cur-row class） */
.edit-form :deep(.el-table__body tr.cur-row > td.el-table__cell) {
  background-color: var(--el-color-primary-light-9);
}
</style>
