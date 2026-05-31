<template>
  <div class="unit-list-container">
    <div class="list-layout">
      <GroupPanel
        ref="groupPanelRef"
        prg-key="Unit"
        title="单位分组"
        @select="handleGroupSelect"
      />
      <div class="list-panel">
    <div class="header-actions">
      <el-button
        v-show="!groupPanelRef?.visible"
        class="toggle-group-btn"
        @click="groupPanelRef!.visible = true"
      >
        <el-icon><DArrowRight /></el-icon>
        <span>分组</span>
      </el-button>
      <el-input v-model="queryParams.keyword" placeholder="搜索单位代码/名称" class="search-input" clearable @clear="fetchData" @keyup.enter="fetchData">
        <template #append><el-button @click="fetchData"><el-icon><Search /></el-icon></el-button></template>
      </el-input>
      
      
      
      <div class="header-right">
        <DynamicFilter
        v-model="queryParams.dynamicFilters"
        :columns="allColumns"
        :api-fields-func="getUnitsFields"
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
      <el-button type="primary" @click="handleAdd" v-permission="'unit:add'"><el-icon><Plus /></el-icon> 新增</el-button>
      <el-button @click="handleEditSelected" :disabled="!canEdit" v-permission="'unit:edit'"><el-icon><Edit /></el-icon> 编辑</el-button>
      <el-button type="success" @click="handleBatchApprove" :disabled="!canApprove" :loading="batchLoading" v-permission="'unit:approve'">
        审核{{ canApprove ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="warning" @click="handleBatchUnapprove" :disabled="!canUnapprove" :loading="batchLoading" v-permission="'unit:approve'">
        反审核{{ canUnapprove ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="danger" @click="handleBatchDelete" :disabled="!canDelete" :loading="batchLoading" v-permission="'unit:delete'">
        删除{{ canDelete ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="info" @click="handleBatchDisable" :disabled="!canDisable" :loading="batchLoading" v-permission="'unit:disable'">
        禁用{{ canDisable ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button @click="handleBatchEnable" :disabled="!canEnable" :loading="batchLoading" v-permission="'unit:disable'">
        反禁用{{ canEnable ? ` (${selectedCount})` : '' }}
      </el-button>
    </div>

    <el-table ref="tableRef" v-loading="loading" :data="list" style="width: 100%" border @selection-change="handleSelectionChange" @row-dblclick="handleRowDblClick" @sort-change="handleSortChange">
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
            <template v-if="col.slotName === 'baseUnit'">
              <el-tag :type="scope.row.fIsBaseUnit ? 'success' : 'info'" size="small">{{ scope.row.fIsBaseUnit ? '是' : '否' }}</el-tag>
            </template>
            <template v-else-if="col.slotName === 'createTime'">
              {{ formatDate(scope.row.cYmd) }}
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
          </template>
        </el-table-column>
      </template>
    </el-table>
    <div class="pagination-container">
      <el-pagination v-model:current-page="queryParams.page" v-model:page-size="queryParams.pageSize" :total="total" layout="total, prev, pager, next" @current-change="fetchData" />
    </div>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { getUnits, deleteUnit, approveUnit, unapproveUnit, disableUnit, enableUnit, getUnitsFields, type Unit } from '../../api/unit'
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
  { key: 'fNumber', label: '单位代码', prop: 'fNumber', width: 130, sortable: 'custom' },
  { key: 'fName', label: '单位名称', prop: 'fName', minWidth: 150, sortable: 'custom' },
  { key: 'fUnitGroupId', label: '单位组', prop: 'fUnitGroupId', width: 120, sortable: 'custom' },
  { key: 'fIsBaseUnit', label: '基准单位', prop: 'fIsBaseUnit', width: 100, align: 'center', slotName: 'baseUnit', sortable: 'custom' },
  { key: 'fPrecision', label: '精度', prop: 'fPrecision', width: 80, align: 'center', sortable: 'custom' },
  { key: 'fCoefficient', label: '换算率', prop: 'fCoefficient', width: 100, sortable: 'custom' },
  { key: 'fRoundType', label: '舍入类型', prop: 'fRoundType', width: 100, defaultVisible: false, sortable: 'custom' },
  { key: 'fConvertType', label: '转换类型', prop: 'fConvertType', width: 100, defaultVisible: false, sortable: 'custom' },
  { key: 'fDescription', label: '描述', prop: 'fDescription', minWidth: 200, defaultVisible: false, sortable: 'custom' },
  { key: 'fStatus', label: '审核状态', prop: 'fStatus', width: 100, align: 'center', slotName: 'status', sortable: 'custom' },
  { key: 'fDisabled', label: '禁用状态', prop: 'fDisabled', width: 100, align: 'center', slotName: 'disabled', sortable: 'custom' },
  { key: 'cYmd', label: '创建时间', prop: 'cYmd', width: 180, slotName: 'createTime', sortable: 'custom' },
]

const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig('unit', columns)

const loading = ref(false)
const list = ref<Unit[]>([])
const total = ref(0)
const queryParams = reactive({ page: 1, pageSize: 10, keyword: '', groupId: '', dynamicFilters: [] as DynamicFilterInfo[], sortField: undefined as string | undefined, isAsc: undefined as boolean | undefined })

const {
  selectedCount, canEdit, canApprove, canUnapprove, canDelete, canDisable, canEnable, batchLoading,
  handleSelectionChange, handleBatchApprove, handleBatchUnapprove, handleBatchDelete, handleBatchDisable, handleBatchEnable
} = useTableSelection<Unit>({
  entityName: '单位',
  approveFn: approveUnit,
  unapproveFn: unapproveUnit,
  deleteFn: deleteUnit,
  disableFn: disableUnit,
  enableFn: enableUnit,
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
async function fetchData() { loading.value = true; try { const res: any = await getUnits(queryParams); list.value = res.data.items; total.value = res.data.totalCount } catch (e) { console.error(e) } finally { loading.value = false } }
const handleAdd = () => { router.push({ name: 'UnitEdit', query: queryParams.groupId ? { groupId: queryParams.groupId } : {} }) }
const handleEdit = (row: Unit) => { router.push({ name: 'UnitEdit', query: { uid: row.uid } }) }
const handleEditSelected = () => { const rows = tableRef.value?.getSelectionRows() as Unit[]; if (rows?.length === 1) handleEdit(rows[0]) }
const handleRowDblClick = (row: Unit) => { handleEdit(row) }
onMounted(() => { fetchData() })
</script>
<style scoped>
.unit-list-container { padding: 20px; background-color: var(--bg-card); border-radius: 8px; box-shadow: var(--shadow-card); }
.list-layout { display: flex; gap: 16px; }
.list-panel { flex: 1; min-width: 0; }
.toggle-group-btn { margin-right: 8px; flex-shrink: 0; }
.header-actions { display: flex; justify-content: space-between; margin-bottom: 12px; }
.header-right { display: flex; align-items: center; gap: 8px; }
.search-input { width: 300px; }
.toolbar-actions { display: flex; align-items: center; gap: 8px; margin-bottom: 12px; }
.pagination-container { margin-top: 20px; display: flex; justify-content: flex-end; }
</style>
