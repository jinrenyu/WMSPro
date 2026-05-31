<template>
  <div class="flexvalues-list-container">
    <div class="header-actions">
      <el-input
        v-model="queryParams.keyword"
        placeholder="搜索代码/名称"
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
          :api-fields-func="getFlexValuesFields"
          @change="fetchData"
          style="margin-right: 8px;"
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
      <el-button type="primary" @click="handleAdd" v-permission="'flexvalues:add'">
        <el-icon><Plus /></el-icon> 新增
      </el-button>
      <el-button @click="handleEditSelected" :disabled="!canEdit" v-permission="'flexvalues:edit'">
        <el-icon><Edit /></el-icon> 编辑
      </el-button>
      <el-button type="success" @click="handleBatchApprove" :disabled="!canApprove" :loading="batchLoading" v-permission="'flexvalues:approve'">
        审核{{ canApprove ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="warning" @click="handleBatchUnapprove" :disabled="!canUnapprove" :loading="batchLoading" v-permission="'flexvalues:approve'">
        反审核{{ canUnapprove ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="danger" @click="handleBatchDelete" :disabled="!canDelete" :loading="batchLoading" v-permission="'flexvalues:delete'">
        删除{{ canDelete ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="info" @click="handleBatchDisable" :disabled="!canDisable" :loading="batchLoading" v-permission="'flexvalues:disable'">
        禁用{{ canDisable ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button @click="handleBatchEnable" :disabled="!canEnable" :loading="batchLoading" v-permission="'flexvalues:disable'">
        反禁用{{ canEnable ? ` (${selectedCount})` : '' }}
      </el-button>
    </div>

    <el-table
      ref="tableRef"
      v-loading="loading"
      :data="list"
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
            <template v-if="col.slotName === 'createTime'">
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
      <el-pagination
        v-model:current-page="queryParams.page"
        v-model:page-size="queryParams.pageSize"
        :total="total"
        layout="total, prev, pager, next"
        @current-change="fetchData"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import {
  getFlexValuesList, deleteFlexValue, approveFlexValue, unapproveFlexValue,
  disableFlexValue, enableFlexValue, getFlexValuesFields, type FlexValue,
} from '../../api/flexValues'
import { formatDate } from '../../utils/format'
import { Search, Plus, Edit } from '@element-plus/icons-vue'
import ColumnSetting from '../../components/ColumnSetting.vue'
import DynamicFilter, { type DynamicFilterInfo } from '../../components/DynamicFilter.vue'
import { useColumnConfig, type ColumnDef } from '../../composables/useColumnConfig'
import { useTableSelection } from '../../composables/useTableSelection'

const router = useRouter()
const tableRef = ref()

const columns: ColumnDef[] = [
  { key: 'fNumber', label: '代码', prop: 'fNumber', width: 160, sortable: 'custom' },
  { key: 'fName', label: '名称', prop: 'fName', minWidth: 180, sortable: 'custom' },
  { key: 'fDescription', label: '描述', prop: 'fDescription', minWidth: 200, defaultVisible: false, sortable: 'custom' },
  { key: 'fStatus', label: '审核状态', prop: 'fStatus', width: 100, align: 'center', slotName: 'status', sortable: 'custom' },
  { key: 'fDisabled', label: '禁用状态', prop: 'fDisabled', width: 100, align: 'center', slotName: 'disabled', sortable: 'custom' },
  { key: 'cYmd', label: '创建时间', prop: 'cYmd', width: 180, slotName: 'createTime', sortable: 'custom' },
]

const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig('flexvalues', columns)

const loading = ref(false)
const list = ref<FlexValue[]>([])
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
} = useTableSelection<FlexValue>({
  entityName: '仓位',
  approveFn: approveFlexValue,
  unapproveFn: unapproveFlexValue,
  deleteFn: deleteFlexValue,
  disableFn: disableFlexValue,
  enableFn: enableFlexValue,
  onSuccess: fetchData,
})

const handleSortChange = ({ prop, order }: { prop: string, order: string | null }) => {
  queryParams.sortField = prop || undefined
  if (order === 'ascending') queryParams.isAsc = true
  else if (order === 'descending') queryParams.isAsc = false
  else queryParams.isAsc = undefined
  queryParams.page = 1
  fetchData()
}

async function fetchData() {
  loading.value = true
  try {
    const res: any = await getFlexValuesList(queryParams)
    list.value = res.data.items
    total.value = res.data.totalCount
  } catch (error) {
    console.error('Fetch flex values failed:', error)
  } finally {
    loading.value = false
  }
}

const handleAdd = () => {
  router.push({ name: 'FlexValuesEdit', query: {} })
}

const handleEdit = (row: FlexValue) => {
  router.push({ name: 'FlexValuesEdit', query: { uid: row.uid } })
}

const handleEditSelected = () => {
  const rows = tableRef.value?.getSelectionRows() as FlexValue[]
  if (rows?.length === 1) handleEdit(rows[0])
}

const handleRowDblClick = (row: FlexValue) => {
  handleEdit(row)
}

onMounted(() => {
  fetchData()
})
</script>

<style scoped>
.flexvalues-list-container {
  padding: 20px;
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
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
