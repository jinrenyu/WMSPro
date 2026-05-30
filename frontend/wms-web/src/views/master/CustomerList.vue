<template>
  <div class="customer-list-container">
    <div class="list-layout">
      <GroupPanel
        ref="groupPanelRef"
        prg-key="Customer"
        title="客户分组"
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
        placeholder="搜索客户编码/名称/简称"
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
        :api-fields-func="getCustomersFields"
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
      <el-button type="primary" @click="handleAdd" v-permission="'customer:add'">
        <el-icon><Plus /></el-icon> 新增
      </el-button>
      <el-button @click="handleEditSelected" :disabled="!canEdit" v-permission="'customer:edit'">
        <el-icon><Edit /></el-icon> 编辑
      </el-button>
      <el-button type="success" @click="handleBatchApprove" :disabled="!canApprove" :loading="batchLoading" v-permission="'customer:approve'">
        审核{{ canApprove ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="warning" @click="handleBatchUnapprove" :disabled="!canUnapprove" :loading="batchLoading" v-permission="'customer:approve'">
        反审核{{ canUnapprove ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="danger" @click="handleBatchDelete" :disabled="!canDelete" :loading="batchLoading" v-permission="'customer:delete'">
        删除{{ canDelete ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="info" @click="handleBatchDisable" :disabled="!canDisable" :loading="batchLoading" v-permission="'customer:disable'">
        禁用{{ canDisable ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button @click="handleBatchEnable" :disabled="!canEnable" :loading="batchLoading" v-permission="'customer:disable'">
        反禁用{{ canEnable ? ` (${selectedCount})` : '' }}
      </el-button>
    </div>

    <el-table
      ref="tableRef"
      v-loading="loading"
      :data="customerList"
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
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { getCustomers, deleteCustomer, approveCustomer, unapproveCustomer, disableCustomer, enableCustomer, getCustomersFields, type Customer } from '../../api/customer'
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
  { key: 'fNumber', label: '客户编码', prop: 'fNumber', width: 150, sortable: 'custom' },
  { key: 'fName', label: '客户名称', prop: 'fName', minWidth: 200, sortable: 'custom' },
  { key: 'fShortName', label: '简称', prop: 'fShortName', width: 150, sortable: 'custom' },
  { key: 'fContact', label: '联系人', prop: 'fContact', width: 120, sortable: 'custom' },
  { key: 'fPhone', label: '联系电话', prop: 'fPhone', width: 150, sortable: 'custom' },
  { key: 'fAddress', label: '地址', prop: 'fAddress', minWidth: 200, sortable: 'custom' },
  { key: 'fNote', label: '备注', prop: 'fNote', minWidth: 200, defaultVisible: false, sortable: 'custom' },
  // 联系信息
  { key: 'fProvincial', label: '省份', prop: 'fProvincial', width: 100, defaultVisible: false, sortable: 'custom' },
  { key: 'fCity', label: '城市', prop: 'fCity', width: 100, defaultVisible: false, sortable: 'custom' },
  { key: 'fZip', label: '邮政区号', prop: 'fZip', width: 100, defaultVisible: false, sortable: 'custom' },
  { key: 'fFax', label: '传真', prop: 'fFax', width: 140, defaultVisible: false, sortable: 'custom' },
  { key: 'fEmail', label: '邮箱', prop: 'fEmail', width: 180, defaultVisible: false, sortable: 'custom' },
  { key: 'fWebSite', label: '公司网址', prop: 'fWebSite', width: 180, defaultVisible: false, sortable: 'custom' },
  { key: 'fNameEn', label: '英文简称', prop: 'fNameEn', width: 150, defaultVisible: false, sortable: 'custom' },
  { key: 'fAddressEn', label: '英文地址', prop: 'fAddressEn', minWidth: 200, defaultVisible: false, sortable: 'custom' },
  // 财务信息
  { key: 'fBank', label: '银行', prop: 'fBank', width: 150, defaultVisible: false, sortable: 'custom' },
  { key: 'fAccount', label: '账户', prop: 'fAccount', width: 180, defaultVisible: false, sortable: 'custom' },
  { key: 'fLegalPerson', label: '法人', prop: 'fLegalPerson', width: 100, defaultVisible: false, sortable: 'custom' },
  { key: 'fTaxRegisterCode', label: '税号', prop: 'fTaxRegisterCode', width: 150, defaultVisible: false, sortable: 'custom' },
  // 系统字段
  { key: 'fStatus', label: '审核状态', prop: 'fStatus', width: 100, align: 'center', slotName: 'status', sortable: 'custom' },
  { key: 'fDisabled', label: '禁用状态', prop: 'fDisabled', width: 100, align: 'center', slotName: 'disabled', sortable: 'custom' },
  { key: 'cYmd', label: '创建时间', prop: 'cYmd', width: 180, slotName: 'createTime', sortable: 'custom' },
]

const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig('customer', columns)

const loading = ref(false)
const customerList = ref<Customer[]>([])
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
} = useTableSelection<Customer>({
  entityName: '客户',
  approveFn: approveCustomer,
  unapproveFn: unapproveCustomer,
  deleteFn: deleteCustomer,
  disableFn: disableCustomer,
  enableFn: enableCustomer,
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
    const res: any = await getCustomers(queryParams)
    customerList.value = res.data.items
    total.value = res.data.totalCount
  } catch (error) {
    console.error('Fetch customers failed:', error)
  } finally {
    loading.value = false
  }
}

const handleAdd = () => {
  router.push({ name: 'CustomerEdit', query: queryParams.groupId ? { groupId: queryParams.groupId } : {} })
}

const handleEdit = (row: Customer) => {
  router.push({ name: 'CustomerEdit', query: { uid: row.uid } })
}

const handleEditSelected = () => {
  const rows = tableRef.value?.getSelectionRows() as Customer[]
  if (rows?.length === 1) handleEdit(rows[0])
}

const handleRowDblClick = (row: Customer) => {
  handleEdit(row)
}

onMounted(() => {
  fetchData()
})
</script>

<style scoped>
.customer-list-container {
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
