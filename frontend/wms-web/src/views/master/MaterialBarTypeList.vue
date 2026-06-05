<template>
  <div class="material-bar-type-list-container">
    <div class="list-layout">
      <GroupPanel
        ref="groupPanelRef"
        prg-key="MaterialBarType"
        title="物料条码类型分组"
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
      <el-input
        v-model="queryParams.keyword"
        placeholder="搜索物料编码/名称"
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
        :api-fields-func="getMaterialBarTypesFields"
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
      <el-button type="primary" @click="handleAdd" v-permission="'materialbartype:add'">
        <el-icon><Plus /></el-icon> 新增
      </el-button>
      <el-button @click="handleEditSelected" :disabled="!canEdit" v-permission="'materialbartype:edit'">
        <el-icon><Edit /></el-icon> 编辑
      </el-button>
      <el-button type="success" @click="handleBatchApprove" :disabled="!canApprove" :loading="batchLoading" v-permission="'materialbartype:approve'">
        审核{{ canApprove ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="warning" @click="handleBatchUnapprove" :disabled="!canUnapprove" :loading="batchLoading" v-permission="'materialbartype:approve'">
        反审核{{ canUnapprove ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="danger" @click="handleBatchDelete" :disabled="!canDelete" :loading="batchLoading" v-permission="'materialbartype:delete'">
        删除{{ canDelete ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="info" @click="handleBatchDisable" :disabled="!canDisable" :loading="batchLoading" v-permission="'materialbartype:disable'">
        禁用{{ canDisable ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button @click="handleBatchEnable" :disabled="!canEnable" :loading="batchLoading" v-permission="'materialbartype:disable'">
        反禁用{{ canEnable ? ` (${selectedCount})` : '' }}
      </el-button>
    </div>

    <el-table
      ref="tableRef"
      v-loading="loading"
      :data="dataList"
      style="width: 100%"
      border
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
            <template v-if="col.slotName === 'fbartype'">
              <el-tag v-if="scope.row.fbartype === 1">单品条码</el-tag>
              <el-tag v-else-if="scope.row.fbartype === 2" type="success">最小包装量条码</el-tag>
              <el-tag v-else-if="scope.row.fbartype === 3" type="warning">批次条码</el-tag>
              <span v-else>{{ scope.row.fbartype }}</span>
            </template>
            <template v-else-if="col.slotName === 'fcheckdate'">
              {{ fmtAuditDate(scope.row.fcheckdate) }}
            </template>
            <template v-else-if="col.slotName === 'fdisabledate'">
              {{ fmtAuditDate(scope.row.fdisabledate) }}
            </template>
            <template v-else-if="col.slotName === 'fstatus'">
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
import { getMaterialBarTypes, deleteMaterialBarType, approveMaterialBarType, unapproveMaterialBarType, disableMaterialBarType, enableMaterialBarType, getMaterialBarTypesFields, type MaterialBarType } from '../../api/materialBarType'
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
  { key: 'fmaterialnumber', label: '物料编码', prop: 'fmaterialnumber', width: 150, sortable: 'custom' },
  { key: 'fmaterialname', label: '物料名称', prop: 'fmaterialname', minWidth: 150, sortable: 'custom' },
  { key: 'fbartype', label: '条码类型', prop: 'fbartype', width: 140, align: 'center', slotName: 'fbartype', sortable: 'custom' },
  { key: 'fcheckdate', label: '审核日期', prop: 'fcheckdate', width: 160, slotName: 'fcheckdate', sortable: 'custom' },
  { key: 'fcheckerid', label: '审核人', prop: 'fcheckerid', width: 120, sortable: 'custom' },
  { key: 'fdisabledate', label: '禁用日期', prop: 'fdisabledate', width: 160, slotName: 'fdisabledate', defaultVisible: false, sortable: 'custom' },
  { key: 'fdisableid', label: '禁用人', prop: 'fdisableid', width: 120, defaultVisible: false, sortable: 'custom' },
  { key: 'fStatus', label: '审核状态', prop: 'fStatus', width: 100, align: 'center', slotName: 'fstatus', sortable: 'custom' },
  { key: 'fDisabled', label: '禁用状态', prop: 'fDisabled', width: 100, align: 'center', slotName: 'disabled', sortable: 'custom' },
  { key: 'cYmd', label: '创建时间', prop: 'cYmd', width: 180, slotName: 'createTime', sortable: 'custom' },
]

const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig('materialBarType', columns)

const loading = ref(false)
const dataList = ref<MaterialBarType[]>([])
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
} = useTableSelection<MaterialBarType>({
  entityName: '物料条码类型',
  approveFn: approveMaterialBarType,
  unapproveFn: unapproveMaterialBarType,
  deleteFn: deleteMaterialBarType,
  disableFn: disableMaterialBarType,
  enableFn: enableMaterialBarType,
  onSuccess: fetchData,
})

// 审核/禁用日期：未审核/未禁用记录为哨兵值（SqlSugar 把 DateTime.MinValue 落库为 1900-01-01），过滤为空（与 Edit 页口径一致）
const fmtAuditDate = (d?: string) => {
  if (!d) return ''
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() <= 1900) return ''
  return formatDate(d)
}

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
    const res: any = await getMaterialBarTypes(queryParams)
    dataList.value = res.data.items
    total.value = res.data.totalCount
  } catch (error) {
    console.error('Fetch material bar types failed:', error)
  } finally {
    loading.value = false
  }
}

const handleAdd = () => {
  router.push({ name: 'MaterialBarTypeEdit', query: queryParams.groupId ? { groupId: queryParams.groupId } : {} })
}

const handleEdit = (row: MaterialBarType) => {
  router.push({ name: 'MaterialBarTypeEdit', query: { uid: row.uid } })
}

const handleEditSelected = () => {
  const rows = tableRef.value?.getSelectionRows() as MaterialBarType[]
  if (rows?.length === 1) handleEdit(rows[0])
}

const handleRowDblClick = (row: MaterialBarType) => {
  handleEdit(row)
}

onMounted(() => {
  fetchData()
})
</script>

<style scoped>
.material-bar-type-list-container {
  padding: 20px;
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
}

.list-layout {
  display: flex;
  gap: 16px;
}

.list-panel {
  flex: 1;
  min-width: 0;
}

.toggle-group-btn {
  margin-right: 8px;
  flex-shrink: 0;
}

.header-actions {
  display: flex;
  justify-content: space-between;
  margin-bottom: 12px;
}

.header-right {
  display: flex;
  align-items: center;
  gap: 8px;
}

.search-input {
  width: 300px;
}

.toolbar-actions {
  display: flex;
  align-items: center;
  gap: 8px;
  margin-bottom: 12px;
}

.pagination-container {
  margin-top: 20px;
  display: flex;
  justify-content: flex-end;
}
</style>
