<template>
  <div class="material-list-container">
    <div class="material-layout">
      <!-- 左侧分组树面板（通用组件） -->
      <GroupPanel
        ref="groupPanelRef"
        prg-key="BD_Material"
        title="物料分组"
        @select="handleGroupSelect"
      />

      <!-- 右侧物料列表 -->
      <div class="material-panel">
        <div class="header-actions">
          <el-button
            v-show="!groupPanelRef?.visible"
            class="toggle-group-btn"
            @click="groupPanelRef!.visible = true"
          >
            <el-icon><DArrowRight /></el-icon>
            <span>分组</span>
          </el-button>
          <el-input
            v-model="queryParams.keyword"
            placeholder="搜索物料编码/名称/规格"
            class="search-input"
            clearable
            @clear="fetchData"
            @keyup.enter="fetchData"
          >
            <template #append>
              <el-button @click="fetchData"><el-icon><Search /></el-icon></el-button>
            </template>
          </el-input>
          
          
          <div class="header-right">
        <DynamicFilter
            v-model="queryParams.dynamicFilters"
            :columns="allColumns"
            :api-fields-func="getFields"
            @change="fetchData" style="margin-right: 8px;"
          />
            <ColumnSetting
              :configurable-columns="configurableColumns"
              :visible-keys="visibleKeys"
              :is-column-visible="isColumnVisible"
              :toggle-column="toggleColumn"
              :reset-columns="resetColumns"
            />
          </div>
        </div>

        <div class="toolbar-actions">
          <el-button type="primary" @click="handleAdd" v-permission="'material:add'">
            <el-icon><Plus /></el-icon> 新增
          </el-button>
          <el-button @click="handleEditSelected" :disabled="!canEdit" v-permission="'material:edit'">
            <el-icon><Edit /></el-icon> 编辑
          </el-button>
          <el-button type="success" @click="handleBatchApprove" :disabled="!canApprove" :loading="batchLoading" v-permission="'material:approve'">
            审核{{ canApprove ? ` (${selectedCount})` : '' }}
          </el-button>
          <el-button type="warning" @click="handleBatchUnapprove" :disabled="!canUnapprove" :loading="batchLoading" v-permission="'material:approve'">
            反审核{{ canUnapprove ? ` (${selectedCount})` : '' }}
          </el-button>
          <el-button type="danger" @click="handleBatchDelete" :disabled="!canDelete" :loading="batchLoading" v-permission="'material:delete'">
            删除{{ canDelete ? ` (${selectedCount})` : '' }}
          </el-button>
          <el-button type="info" @click="handleBatchDisable" :disabled="!canDisable" :loading="batchLoading" v-permission="'material:disable'">
            禁用{{ canDisable ? ` (${selectedCount})` : '' }}
          </el-button>
          <el-button @click="handleBatchEnable" :disabled="!canEnable" :loading="batchLoading" v-permission="'material:disable'">
            反禁用{{ canEnable ? ` (${selectedCount})` : '' }}
          </el-button>
        </div>

        <el-table
          ref="tableRef"
          v-loading="loading"
          :data="materialList"
          style="width: 100%"
          border
          class="material-table"
          @selection-change="handleSelectionChange"
          @row-dblclick="handleRowDblClick"
        >
          <el-table-column type="selection" width="45" fixed="left" />
          <template v-for="col in allColumns" :key="col.key">
            <el-table-column
              v-if="isColumnVisible(col)"
              :prop="col.prop"
              :label="col.label"
              :width="col.width"
              :min-width="col.minWidth"
              :align="col.align"
              :fixed="col.fixed"
            >
              <template v-if="col.slotName" #default="scope">
                <template v-if="col.slotName === 'boolTag'">
                  <el-tag :type="scope.row[col.key] ? 'success' : 'info'" size="small">
                    {{ scope.row[col.key] ? '是' : '否' }}
                  </el-tag>
                </template>
                <template v-else-if="col.slotName === 'kfUnit'">
                  {{ scope.row.fKfUnit === 0 ? '天' : scope.row.fKfUnit === 1 ? '月' : scope.row.fKfUnit === 2 ? '年' : '' }}
                </template>
                <template v-else-if="col.slotName === 'status'">
                  <el-tag :type="scope.row.fStatus === 40 ? 'success' : 'warning'" size="small">
                    {{ scope.row.fStatus === 40 ? '已审核' : '未审核' }}
                  </el-tag>
                </template>
                <template v-else-if="col.slotName === 'disabled'">
                  <el-tag v-if="scope.row.fDisabled" type="danger" size="small">已禁用</el-tag>
                  <el-tag v-else type="success" size="small">正常</el-tag>
                </template>
                <template v-else-if="col.slotName === 'createTime'">
                  {{ formatDate(scope.row.cYmd) }}
                </template>
              </template>
            </el-table-column>
          </template>
        </el-table>

        <div class="pagination-container">
          <el-pagination
            v-model:current-page="queryParams.page"
            v-model:page-size="queryParams.pageSize"
            :total="total"
            layout="total, prev, pager, next"
            @current-change="fetchData"
          />
        </div>
      </div>
    </div>

    <!-- Create/Edit Dialog -->
    <el-dialog
      v-model="dialogVisible"
      :title="dialogType === 'create' ? '新增物料' : isReadonly ? '查看物料' : '编辑物料'"
      width="750px"
    >
      <el-form :model="form" label-width="100px" :disabled="isReadonly">
        <el-tabs v-model="activeTab">
          <!-- 基本信息 -->
          <el-tab-pane label="基本信息" name="basic">
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="物料编码" required>
                  <el-input v-model="form.fNumber" :disabled="dialogType === 'edit'" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="物料名称" required>
                  <el-input v-model="form.fName" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="规格型号">
                  <el-input v-model="form.fSpecification" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="物料属性">
                  <el-input v-model="form.fErpClsId" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="物料类别">
                  <el-input v-model="form.fTypeId" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="ABC分类">
                  <el-input v-model="form.fAbc" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="物料分组">
                  <el-tree-select
                    v-model="form.fGroupId"
                    :data="groupPanelRef?.treeData || []"
                    :props="{ label: 'fName', children: 'children', value: 'uid' }"
                    placeholder="请选择分组"
                    clearable
                    check-strictly
                    style="width: 100%"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="条码">
                  <el-input v-model="form.fBarcode" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="旧料号">
                  <el-input v-model="form.fOldNumber" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-form-item label="描述">
              <el-input v-model="form.fDescription" type="textarea" :rows="2" />
            </el-form-item>
          </el-tab-pane>

          <!-- 单位信息 -->
          <el-tab-pane label="单位信息" name="unit">
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="基本单位">
                  <LookupSelect v-model="form.fBaseUnitId" module="unit" placeholder="请选择基本单位" preload />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="库存单位">
                  <LookupSelect v-model="form.fStoreUnitId" module="unit" placeholder="请选择库存单位" preload />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="销售单位">
                  <LookupSelect v-model="form.fSaleUnitId" module="unit" placeholder="请选择销售单位" preload />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="采购单位">
                  <LookupSelect v-model="form.fPurchaseUnitId" module="unit" placeholder="请选择采购单位" preload />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="净重">
                  <el-input-number v-model="form.fNetWeight" :min="0" :precision="4" style="width: 100%" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="毛重">
                  <el-input-number v-model="form.fGrossWeight" :min="0" :precision="4" style="width: 100%" />
                </el-form-item>
              </el-col>
            </el-row>
          </el-tab-pane>

          <!-- 库存信息 -->
          <el-tab-pane label="库存信息" name="stock">
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="最大库存">
                  <el-input-number v-model="form.fMaxQty" :min="0" :precision="4" style="width: 100%" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="安全库存">
                  <el-input-number v-model="form.fSafeQty" :min="0" :precision="4" style="width: 100%" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="默认仓库">
                  <LookupSelect v-model="form.fDeStockId" module="warehouse" placeholder="请选择默认仓库" preload @change="handleWarehouseChange" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="默认仓位">
                  <LookupSelect v-model="form.fDeSpId" module="stockplace" :parent-id="form.fDeStockId" placeholder="请先选择仓库" :disabled="!form.fDeStockId" preload />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="8">
                <el-form-item label="批号管理">
                  <el-switch v-model="form.fIsBatchManage" />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="来料检验">
                  <el-switch v-model="form.fCheckIncoming" />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="虚拟件">
                  <el-switch v-model="form.fVPart" />
                </el-form-item>
              </el-col>
            </el-row>
          </el-tab-pane>

          <!-- 采购与保质期 -->
          <el-tab-pane label="采购与保质期" name="purchase">
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="最小订货量">
                  <el-input-number v-model="form.fMinPoQty" :min="0" :precision="4" style="width: 100%" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="最小包装量">
                  <el-input-number v-model="form.fIncreaseQty" :min="0" :precision="4" style="width: 100%" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="8">
                <el-form-item label="启用保质期">
                  <el-switch v-model="form.fIsKfPeriod" />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="保质期限">
                  <el-input-number v-model="form.fKfPeriod" :min="0" :disabled="!form.fIsKfPeriod" style="width: 100%" />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="保质期单位">
                  <el-select v-model="form.fKfUnit" :disabled="!form.fIsKfPeriod" style="width: 100%">
                    <el-option :value="0" label="天" />
                    <el-option :value="1" label="月" />
                    <el-option :value="2" label="年" />
                  </el-select>
                </el-form-item>
              </el-col>
            </el-row>
          </el-tab-pane>
        </el-tabs>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="dialogVisible = false">{{ isReadonly ? '关闭' : '取消' }}</el-button>
          <el-button v-if="!isReadonly" type="primary" @click="submitForm">确定</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted } from 'vue'
import { getMaterials, getMaterial, createMaterial, updateMaterial, deleteMaterial, approveMaterial, unapproveMaterial, disableMaterial, enableMaterial, getFields, type Material } from '../../api/material'
import { formatDate } from '../../utils/format'
import { ElMessage } from 'element-plus'
import { Search, Plus, Edit, DArrowRight } from '@element-plus/icons-vue'
import LookupSelect from '../../components/LookupSelect.vue'
import ColumnSetting from '../../components/ColumnSetting.vue'
import GroupPanel from '../../components/GroupPanel.vue'
import DynamicFilter, { type DynamicFilterInfo } from '../../components/DynamicFilter.vue'
import { useColumnConfig, type ColumnDef } from '../../composables/useColumnConfig'
import { useTableSelection } from '../../composables/useTableSelection'

const groupPanelRef = ref<InstanceType<typeof GroupPanel>>()
const tableRef = ref()

const columns: ColumnDef[] = [
  // 基本信息
  { key: 'fNumber', label: '物料编码', prop: 'fNumber', width: 150 },
  { key: 'fName', label: '物料名称', prop: 'fName', minWidth: 180 },
  { key: 'fSpecification', label: '规格型号', prop: 'fSpecification', minWidth: 150 },
  { key: 'fErpClsId', label: '物料属性', prop: 'fErpClsId', width: 120 },
  { key: 'fTypeId', label: '物料类别', prop: 'fTypeId', width: 120 },
  { key: 'fGroupName', label: '物料分组', prop: 'fGroupName', width: 120 },
  { key: 'fAbc', label: 'ABC分类', prop: 'fAbc', width: 100, defaultVisible: false },
  { key: 'fBarcode', label: '条码', prop: 'fBarcode', width: 150, defaultVisible: false },
  { key: 'fOldNumber', label: '旧料号', prop: 'fOldNumber', width: 130, defaultVisible: false },
  { key: 'fDescription', label: '描述', prop: 'fDescription', minWidth: 200, defaultVisible: false },
  // 单位信息
  { key: 'fBaseUnitName', label: '基本单位', prop: 'fBaseUnitName', width: 100 },
  { key: 'fStoreUnitId', label: '库存单位', prop: 'fStoreUnitId', width: 100, defaultVisible: false },
  { key: 'fSaleUnitId', label: '销售单位', prop: 'fSaleUnitId', width: 100, defaultVisible: false },
  { key: 'fPurchaseUnitId', label: '采购单位', prop: 'fPurchaseUnitId', width: 100, defaultVisible: false },
  { key: 'fNetWeight', label: '净重', prop: 'fNetWeight', width: 100, defaultVisible: false },
  { key: 'fGrossWeight', label: '毛重', prop: 'fGrossWeight', width: 100, defaultVisible: false },
  // 库存信息
  { key: 'fIsBatchManage', label: '批号管理', width: 100, align: 'center', slotName: 'boolTag' },
  { key: 'fCheckIncoming', label: '来料检验', width: 100, align: 'center', slotName: 'boolTag', defaultVisible: false },
  { key: 'fVPart', label: '虚拟件', width: 100, align: 'center', slotName: 'boolTag', defaultVisible: false },
  { key: 'fMaxQty', label: '最大库存', prop: 'fMaxQty', width: 110, defaultVisible: false },
  { key: 'fSafeQty', label: '安全库存', prop: 'fSafeQty', width: 110, defaultVisible: false },
  { key: 'fDeStockName', label: '默认仓库', prop: 'fDeStockName', width: 120, defaultVisible: false },
  { key: 'fDeSpName', label: '默认仓位', prop: 'fDeSpName', width: 120, defaultVisible: false },
  // 采购与保质期
  { key: 'fMinPoQty', label: '最小订货量', prop: 'fMinPoQty', width: 110, defaultVisible: false },
  { key: 'fIncreaseQty', label: '最小包装量', prop: 'fIncreaseQty', width: 110, defaultVisible: false },
  { key: 'fIsKfPeriod', label: '启用保质期', width: 110, align: 'center', slotName: 'boolTag', defaultVisible: false },
  { key: 'fKfPeriod', label: '保质期限', prop: 'fKfPeriod', width: 100, defaultVisible: false },
  { key: 'fKfUnit', label: '保质期单位', width: 110, slotName: 'kfUnit', defaultVisible: false },
  // 系统字段
  { key: 'fStatus', label: '审核状态', width: 100, align: 'center', slotName: 'status' },
  { key: 'fDisabled', label: '禁用状态', width: 100, align: 'center', slotName: 'disabled' },
  { key: 'cYmd', label: '创建时间', width: 180, slotName: 'createTime' },
]

const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig('material', columns)

const loading = ref(false)
const materialList = ref<Material[]>([])
const total = ref(0)
const queryParams = reactive({
  page: 1,
  pageSize: 10,
  keyword: '',
  groupId: '',
  dynamicFilters: [] as DynamicFilterInfo[]
})

const {
  selectedCount, canEdit, canApprove, canUnapprove, canDelete, canDisable, canEnable, batchLoading,
  handleSelectionChange, handleBatchApprove, handleBatchUnapprove, handleBatchDelete, handleBatchDisable, handleBatchEnable, clearSelection
} = useTableSelection<Material>({
  entityName: '物料',
  approveFn: approveMaterial,
  unapproveFn: unapproveMaterial,
  deleteFn: deleteMaterial,
  disableFn: disableMaterial,
  enableFn: enableMaterial,
  onSuccess: fetchData,
})

const handleGroupSelect = (groupId: string) => {
  queryParams.groupId = groupId
  queryParams.page = 1
  fetchData()
}

// --- 物料 ---
const dialogVisible = ref(false)
const dialogType = ref<'create' | 'edit'>('create')
const activeTab = ref('basic')

const isReadonly = computed(() => dialogType.value === 'edit' && (form.fStatus === 40 || form.fDisabled))

const defaultForm = {
  uid: undefined as string | undefined,
  fStatus: 0,
  fNumber: '',
  fName: '',
  fSpecification: '',
  fDescription: '',
  fBarcode: '',
  fErpClsId: '',
  fAbc: '',
  fMaxQty: 0,
  fSafeQty: 0,
  fNetWeight: 0,
  fGrossWeight: 0,
  fBaseUnitId: '',
  fStoreUnitId: '',
  fSaleUnitId: '',
  fPurchaseUnitId: '',
  fIsKfPeriod: false,
  fKfUnit: 0,
  fKfPeriod: 0,
  fIsBatchManage: false,
  fMinPoQty: 0,
  fIncreaseQty: 0,
  fCheckIncoming: false,
  fOldNumber: '',
  fDeStockId: '',
  fDeSpId: '',
  fVPart: false,
  fTypeId: '',
  fGroupId: '',
  fDisabled: false
}

const form = reactive({ ...defaultForm })

async function fetchData() {
  loading.value = true
  try {
    const res: any = await getMaterials(queryParams)
    materialList.value = res.data.items
    total.value = res.data.totalCount
  } catch (error) {
    console.error('Fetch materials failed:', error)
  } finally {
    loading.value = false
  }
}

const handleAdd = () => {
  dialogType.value = 'create'
  activeTab.value = 'basic'
  Object.assign(form, { ...defaultForm })
  dialogVisible.value = true
}

const handleEdit = async (row: Material) => {
  dialogType.value = 'edit'
  activeTab.value = 'basic'
  try {
    const res: any = await getMaterial(row.uid!)
    const d = res.data
    Object.assign(form, {
      uid: d.uid,
      fStatus: d.fStatus || 0,
      fNumber: d.fNumber || '',
      fName: d.fName || '',
      fSpecification: d.fSpecification || '',
      fDescription: d.fDescription || '',
      fBarcode: d.fBarcode || '',
      fErpClsId: d.fErpClsId || '',
      fAbc: d.fAbc || '',
      fMaxQty: d.fMaxQty || 0,
      fSafeQty: d.fSafeQty || 0,
      fNetWeight: d.fNetWeight || 0,
      fGrossWeight: d.fGrossWeight || 0,
      fBaseUnitId: d.fBaseUnitId || '',
      fStoreUnitId: d.fStoreUnitId || '',
      fSaleUnitId: d.fSaleUnitId || '',
      fPurchaseUnitId: d.fPurchaseUnitId || '',
      fIsKfPeriod: d.fIsKfPeriod || false,
      fKfUnit: d.fKfUnit || 0,
      fKfPeriod: d.fKfPeriod || 0,
      fIsBatchManage: d.fIsBatchManage || false,
      fMinPoQty: d.fMinPoQty || 0,
      fIncreaseQty: d.fIncreaseQty || 0,
      fCheckIncoming: d.fCheckIncoming || false,
      fOldNumber: d.fOldNumber || '',
      fDeStockId: d.fDeStockId || '',
      fDeSpId: d.fDeSpId || '',
      fVPart: d.fVPart || false,
      fTypeId: d.fTypeId || '',
      fGroupId: d.fGroupId || '',
      fDisabled: d.fDisabled || false
    })
  } catch (error) {
    console.error('Fetch material detail failed:', error)
  }
  dialogVisible.value = true
}

const handleEditSelected = () => {
  const rows = tableRef.value?.getSelectionRows() as Material[]
  if (rows?.length === 1) handleEdit(rows[0])
}

const handleRowDblClick = (row: Material) => {
  handleEdit(row)
}

const handleWarehouseChange = () => {
  form.fDeSpId = ''
}

const submitForm = async () => {
  if (!form.fNumber || !form.fName) {
    ElMessage.warning('请输入物料编码和名称')
    return
  }

  const data = {
    fNumber: form.fNumber,
    fName: form.fName,
    fSpecification: form.fSpecification,
    fDescription: form.fDescription,
    fBarcode: form.fBarcode,
    fErpClsId: form.fErpClsId,
    fAbc: form.fAbc,
    fMaxQty: form.fMaxQty,
    fSafeQty: form.fSafeQty,
    fNetWeight: form.fNetWeight,
    fGrossWeight: form.fGrossWeight,
    fBaseUnitId: form.fBaseUnitId,
    fStoreUnitId: form.fStoreUnitId,
    fSaleUnitId: form.fSaleUnitId,
    fPurchaseUnitId: form.fPurchaseUnitId,
    fIsKfPeriod: form.fIsKfPeriod,
    fKfUnit: form.fKfUnit,
    fKfPeriod: form.fKfPeriod,
    fIsBatchManage: form.fIsBatchManage,
    fMinPoQty: form.fMinPoQty,
    fIncreaseQty: form.fIncreaseQty,
    fCheckIncoming: form.fCheckIncoming,
    fOldNumber: form.fOldNumber,
    fDeStockId: form.fDeStockId,
    fDeSpId: form.fDeSpId,
    fVPart: form.fVPart,
    fTypeId: form.fTypeId,
    fGroupId: form.fGroupId
  }

  try {
    if (dialogType.value === 'create') {
      await createMaterial(data)
      ElMessage.success('创建成功')
    } else {
      const { fNumber, ...updateData } = data
      await updateMaterial(form.uid!, updateData)
      ElMessage.success('更新成功')
    }
    dialogVisible.value = false
    fetchData()
  } catch (error: any) {
    console.error('Submit material failed:', error)
    if (error.response && error.response.data) {
      const errorMsg = error.response.data.message || JSON.stringify(error.response.data)
      ElMessage.error(errorMsg)
    } else {
      ElMessage.error('提交失败')
    }
  }
}

onMounted(() => {
  fetchData()
})
</script>

<style scoped>
.material-list-container {
  padding: 20px;
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
}

.material-layout {
  display: flex;
  gap: 16px;
}

.material-panel {
  flex: 1;
  min-width: 0;
}

.header-actions {
  display: flex;
  justify-content: space-between;
  margin-bottom: 12px;
}

.toolbar-actions {
  display: flex;
  align-items: center;
  gap: 8px;
  margin-bottom: 12px;
}

.header-right {
  display: flex;
  align-items: center;
  gap: 8px;
}

.toggle-group-btn {
  margin-right: 8px;
  flex-shrink: 0;
}

.search-input {
  width: 300px;
}

.pagination-container {
  margin-top: 20px;
  display: flex;
  justify-content: flex-end;
}
</style>
