<template>
  <div class="material-list-container">
    <div class="material-layout">
      <!-- 左侧分组树面板（通用组件） -->
      <GroupPanel
        ref="groupPanelRef"
        prg-key="Material"
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
          @sort-change="handleSortChange"
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
              :sortable="col.sortable"
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
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { getMaterials, deleteMaterial, approveMaterial, unapproveMaterial, disableMaterial, enableMaterial, getFields, type Material } from '../../api/material'
import { formatDate } from '../../utils/format'
import { Search, Plus, Edit, DArrowRight } from '@element-plus/icons-vue'
import ColumnSetting from '../../components/ColumnSetting.vue'
import GroupPanel from '../../components/GroupPanel.vue'
import DynamicFilter, { type DynamicFilterInfo } from '../../components/DynamicFilter.vue'
import { useColumnConfig, type ColumnDef } from '../../composables/useColumnConfig'
import { useTableSelection } from '../../composables/useTableSelection'

const router = useRouter()
const groupPanelRef = ref<InstanceType<typeof GroupPanel>>()
const tableRef = ref()

const columns: ColumnDef[] = [
  // 基本信息
  { key: 'fNumber', label: '物料编码', prop: 'fNumber', width: 150, sortable: 'custom' },
  { key: 'fName', label: '物料名称', prop: 'fName', minWidth: 180, sortable: 'custom' },
  { key: 'fSpecification', label: '规格型号', prop: 'fSpecification', minWidth: 150, sortable: 'custom' },
  { key: 'fErpClsId', label: '物料属性', prop: 'fErpClsId', width: 120, sortable: 'custom' },
  { key: 'fTypeName', label: '物料类别', prop: 'fTypeName', width: 120 },
  { key: 'fGroupName', label: '物料分组', prop: 'fGroupName', width: 120, sortable: 'custom' },
  { key: 'fAbc', label: 'ABC分类', prop: 'fAbc', width: 100, defaultVisible: false, sortable: 'custom' },
  { key: 'fBarcode', label: '条码', prop: 'fBarcode', width: 150, defaultVisible: false, sortable: 'custom' },
  { key: 'fOldNumber', label: '旧料号', prop: 'fOldNumber', width: 130, defaultVisible: false, sortable: 'custom' },
  { key: 'fDescription', label: '描述', prop: 'fDescription', minWidth: 200, defaultVisible: false, sortable: 'custom' },
  // 单位信息
  { key: 'fBaseUnitName', label: '基本单位', prop: 'fBaseUnitName', width: 100, sortable: 'custom' },
  { key: 'fStoreUnitId', label: '库存单位', prop: 'fStoreUnitId', width: 100, defaultVisible: false, sortable: 'custom' },
  { key: 'fSaleUnitId', label: '销售单位', prop: 'fSaleUnitId', width: 100, defaultVisible: false, sortable: 'custom' },
  { key: 'fPurchaseUnitId', label: '采购单位', prop: 'fPurchaseUnitId', width: 100, defaultVisible: false, sortable: 'custom' },
  { key: 'fNetWeight', label: '净重', prop: 'fNetWeight', width: 100, defaultVisible: false, sortable: 'custom' },
  { key: 'fGrossWeight', label: '毛重', prop: 'fGrossWeight', width: 100, defaultVisible: false, sortable: 'custom' },
  // 库存信息
  { key: 'fIsBatchManage', label: '批号管理', prop: 'fIsBatchManage', width: 100, align: 'center', slotName: 'boolTag', sortable: 'custom' },
  { key: 'fCheckIncoming', label: '来料检验', prop: 'fCheckIncoming', width: 100, align: 'center', slotName: 'boolTag', defaultVisible: false, sortable: 'custom' },
  { key: 'fVPart', label: '虚拟件', prop: 'fVPart', width: 100, align: 'center', slotName: 'boolTag', defaultVisible: false, sortable: 'custom' },
  { key: 'fMaxQty', label: '最大库存', prop: 'fMaxQty', width: 110, defaultVisible: false, sortable: 'custom' },
  { key: 'fSafeQty', label: '安全库存', prop: 'fSafeQty', width: 110, defaultVisible: false, sortable: 'custom' },
  { key: 'fDeStockName', label: '默认仓库', prop: 'fDeStockName', width: 120, defaultVisible: false, sortable: 'custom' },
  { key: 'fDeSpName', label: '默认仓位', prop: 'fDeSpName', width: 120, defaultVisible: false, sortable: 'custom' },
  // 采购与保质期
  { key: 'fMinPoQty', label: '最小订货量', prop: 'fMinPoQty', width: 110, defaultVisible: false, sortable: 'custom' },
  { key: 'fIncreaseQty', label: '最小包装量', prop: 'fIncreaseQty', width: 110, defaultVisible: false, sortable: 'custom' },
  { key: 'fIsKfPeriod', label: '启用保质期', prop: 'fIsKfPeriod', width: 110, align: 'center', slotName: 'boolTag', defaultVisible: false, sortable: 'custom' },
  { key: 'fKfPeriod', label: '保质期限', prop: 'fKfPeriod', width: 100, defaultVisible: false, sortable: 'custom' },
  { key: 'fKfUnit', label: '保质期单位', prop: 'fKfUnit', width: 110, slotName: 'kfUnit', defaultVisible: false, sortable: 'custom' },
  // 系统字段
  { key: 'fStatus', label: '审核状态', prop: 'fStatus', width: 100, align: 'center', slotName: 'status', sortable: 'custom' },
  { key: 'fDisabled', label: '禁用状态', prop: 'fDisabled', width: 100, align: 'center', slotName: 'disabled', sortable: 'custom' },
  { key: 'cYmd', label: '创建时间', prop: 'cYmd', width: 180, slotName: 'createTime', sortable: 'custom' },
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
  dynamicFilters: [] as DynamicFilterInfo[],
  sortField: undefined as string | undefined,
  isAsc: undefined as boolean | undefined
})

const {
  selectedCount, canEdit, canApprove, canUnapprove, canDelete, canDisable, canEnable, batchLoading,
  handleSelectionChange, handleBatchApprove, handleBatchUnapprove, handleBatchDelete, handleBatchDisable, handleBatchEnable
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

const handleSortChange = ({ prop, order }: { prop: string, order: string | null }) => {
  queryParams.sortField = prop || undefined
  if (order === 'ascending') {
    queryParams.isAsc = true
  } else if (order === 'descending') {
    queryParams.isAsc = false
  } else {
    queryParams.isAsc = undefined
  }
  queryParams.page = 1
  fetchData()
}

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
  router.push({ name: 'MaterialEdit', query: queryParams.groupId ? { groupId: queryParams.groupId } : {} })
}

const handleEdit = (row: Material) => {
  router.push({ name: 'MaterialEdit', query: { uid: row.uid } })
}

const handleEditSelected = () => {
  const rows = tableRef.value?.getSelectionRows() as Material[]
  if (rows?.length === 1) handleEdit(rows[0])
}

const handleRowDblClick = (row: Material) => {
  handleEdit(row)
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
