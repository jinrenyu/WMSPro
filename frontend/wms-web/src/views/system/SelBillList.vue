<template>
  <div class="selbill-list-container">
    <div class="list-panel">
      <div class="header-actions">
        <el-input
          v-model="queryParams.keyword"
          placeholder="搜索单据编号 / 单据名称"
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
            :columns="filterColumns"
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
        <el-button type="primary" @click="handleAdd" v-permission="'selbill:add'">
          <el-icon><Plus /></el-icon> 新增
        </el-button>
        <el-button @click="handleEditSelected" :disabled="selectedIds.length !== 1" v-permission="'selbill:edit'">
          <el-icon><Edit /></el-icon> 修改
        </el-button>
        <el-button type="success" @click="handleBatchApprove" :disabled="selectedIds.length === 0" :loading="actionLoading" v-permission="'selbill:approve'">
          审核{{ selectedIds.length ? ` (${selectedIds.length})` : '' }}
        </el-button>
        <el-button type="warning" @click="handleBatchUnapprove" :disabled="selectedIds.length === 0" :loading="actionLoading" v-permission="'selbill:approve'">
          反审核{{ selectedIds.length ? ` (${selectedIds.length})` : '' }}
        </el-button>
        <el-button type="info" @click="handleBatchDisable" :disabled="selectedIds.length === 0" :loading="actionLoading" v-permission="'selbill:disable'">
          禁用{{ selectedIds.length ? ` (${selectedIds.length})` : '' }}
        </el-button>
        <el-button @click="handleBatchEnable" :disabled="selectedIds.length === 0" :loading="actionLoading" v-permission="'selbill:disable'">
          反禁用{{ selectedIds.length ? ` (${selectedIds.length})` : '' }}
        </el-button>
        <el-button type="danger" @click="handleBatchDelete" :disabled="selectedIds.length === 0" :loading="actionLoading" v-permission="'selbill:delete'">
          删除{{ selectedIds.length ? ` (${selectedIds.length})` : '' }}
        </el-button>
      </div>

      <el-table
        ref="tableRef"
        v-loading="loading"
        :data="dataList"
        style="width: 100%"
        border
        size="small"
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
              <template v-if="col.slotName === 'bool'">
                <el-checkbox :model-value="!!scope.row[col.prop!]" disabled />
              </template>
              <template v-else-if="col.slotName === 'datetime'">
                {{ fmtDateTime(scope.row[col.prop!]) }}
              </template>
              <template v-else-if="col.slotName === 'date'">
                {{ fmtDate(scope.row[col.prop!]) }}
              </template>
              <template v-else-if="col.slotName === 'status'">
                <el-tag :type="statusTagType(scope.row.fStatus)" size="small">{{ scope.row.fstatusName || statusText(scope.row.fStatus) }}</el-tag>
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
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import { Search, Plus, Edit } from '@element-plus/icons-vue'
import {
  getSelBills, deleteSelBill,
  approveSelBill, unapproveSelBill, disableSelBill, enableSelBill
} from '../../api/selBill'
import { formatDate, formatDateOnly } from '../../utils/format'
import ColumnSetting from '../../components/ColumnSetting.vue'
import DynamicFilter, { type DynamicFilterInfo } from '../../components/DynamicFilter.vue'
import { useColumnConfig, type ColumnDef } from '../../composables/useColumnConfig'

const router = useRouter()
const tableRef = ref()

// 列表按主表一行
const columns: ColumnDef[] = [
  { key: 'fnumber', label: '单据编号', prop: 'fnumber', width: 170, fixed: 'left' },
  { key: 'fname', label: '单据名称', prop: 'fname', minWidth: 140 },
  { key: 'fsourcetypeName', label: '源单类型', prop: 'fsourcetypeName', width: 130 },
  { key: 'fdesttranName', label: '目标单据类型', prop: 'fdesttranName', width: 130 },
  { key: 'fiscontrolqty', label: '控制数量', prop: 'fiscontrolqty', width: 90, align: 'center', slotName: 'bool' },
  { key: 'fisopensource', label: '开放源单', prop: 'fisopensource', width: 90, align: 'center', slotName: 'bool' },
  { key: 'fdefault', label: '默认源单', prop: 'fdefault', width: 90, align: 'center', slotName: 'bool' },
  { key: 'fisuse', label: '启用', prop: 'fisuse', width: 70, align: 'center', slotName: 'bool' },
  { key: 'fcheck', label: '是否审核', prop: 'fcheck', width: 90, align: 'center', slotName: 'bool' },
  { key: 'fisdefaultstock', label: '带出仓库仓位', prop: 'fisdefaultstock', width: 110, align: 'center', slotName: 'bool' },
  { key: 'fStatus', label: '数据状态', prop: 'fStatus', width: 90, align: 'center', slotName: 'status' },
  { key: 'fDisabled', label: '禁用状态', prop: 'fDisabled', width: 90, align: 'center', slotName: 'disabled' },
  { key: 'cuserName', label: '制单人', prop: 'cuserName', width: 100 },
  { key: 'cYmd', label: '制单日期', prop: 'cYmd', width: 160, slotName: 'datetime' },
  { key: 'fcheckerName', label: '审核人', prop: 'fcheckerName', width: 100 },
  { key: 'fcheckdate', label: '审核日期', prop: 'fcheckdate', width: 110, slotName: 'date' },
  { key: 'fdisableName', label: '禁用人', prop: 'fdisableName', width: 100, defaultVisible: false },
  { key: 'fdisabledate', label: '禁用日期', prop: 'fdisabledate', width: 110, slotName: 'date', defaultVisible: false },
]

// 高级筛选仅开放可在服务端过滤的真实列（名称解析列不参与服务端过滤）
const filterColumns: ColumnDef[] = [
  { key: 'fnumber', label: '单据编号', prop: 'fnumber' },
  { key: 'fname', label: '单据名称', prop: 'fname' },
  { key: 'fStatus', label: '数据状态', prop: 'fStatus', slotName: 'status' },
]

const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig('selBill', columns)

const loading = ref(false)
const actionLoading = ref(false)
const dataList = ref<any[]>([])
const total = ref(0)
const selectedRows = ref<any[]>([])

const queryParams = reactive({
  page: 1,
  pageSize: 10,
  keyword: '',
  dynamicFilters: [] as DynamicFilterInfo[]
})

const selectedIds = computed(() => Array.from(new Set(selectedRows.value.map(r => r.uid).filter(Boolean))))

const fmtDateTime = (d?: string) => {
  if (!d) return ''
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() <= 1900) return ''
  return formatDate(d)
}
const fmtDate = (d?: string) => {
  if (!d) return ''
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() <= 1900) return ''
  return formatDateOnly(d)
}
const statusText = (s?: number) => (s === 40 ? '审核' : s === 70 ? '关闭' : '暂存')
const statusTagType = (s?: number) => (s === 40 ? 'success' : s === 70 ? 'info' : 'warning')

const handleSelectionChange = (rows: any[]) => { selectedRows.value = rows }

async function fetchData() {
  loading.value = true
  try {
    const res: any = await getSelBills(queryParams)
    dataList.value = res.data.items
    total.value = res.data.totalCount
  } catch (e) {
    console.error('加载出入库流程配置失败:', e)
  } finally {
    loading.value = false
  }
}

const handleAdd = () => router.push({ name: 'SelBillEdit' })
const handleEdit = (uid: string) => router.push({ name: 'SelBillEdit', query: { uid } })
const handleEditSelected = () => { if (selectedIds.value.length === 1) handleEdit(selectedIds.value[0]) }
const handleRowDblClick = (row: any) => { if (row.uid) handleEdit(row.uid) }

async function runBatch(ids: string[], fn: (id: string) => Promise<any>, label: string) {
  actionLoading.value = true
  try {
    let ok = 0, fail = 0
    for (const id of ids) {
      try { await fn(id); ok++ } catch { fail++ }
    }
    if (fail === 0) ElMessage.success(`${label}成功 (${ok})`)
    else ElMessage.warning(`${label}完成：成功 ${ok}，失败 ${fail}`)
    await fetchData()
  } finally {
    actionLoading.value = false
  }
}

const confirmBatch = async (title: string, msg: string, ids: string[], fn: (id: string) => Promise<any>) => {
  const r = await ElMessageBox.confirm(msg, title, { type: 'warning' }).catch(() => 'cancel')
  if (r !== 'cancel') await runBatch(ids, fn, title)
}

const handleBatchApprove = () => confirmBatch('审核', `确认审核选中的 ${selectedIds.value.length} 条流程配置？`, selectedIds.value, approveSelBill)
const handleBatchUnapprove = () => confirmBatch('反审核', `确认反审核选中的 ${selectedIds.value.length} 条流程配置？`, selectedIds.value, unapproveSelBill)
const handleBatchDisable = () => confirmBatch('禁用', `确认禁用选中的 ${selectedIds.value.length} 条流程配置？禁用后不再作为源单类型可选项。`, selectedIds.value, disableSelBill)
const handleBatchEnable = () => confirmBatch('反禁用', `确认反禁用选中的 ${selectedIds.value.length} 条流程配置？`, selectedIds.value, enableSelBill)
const handleBatchDelete = () => confirmBatch('删除', `确认删除选中的 ${selectedIds.value.length} 条流程配置？删除后不可恢复。`, selectedIds.value, deleteSelBill)

onMounted(() => { fetchData() })
</script>

<style scoped>
.selbill-list-container {
  padding: 20px;
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
}
.list-panel { min-width: 0; }
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
.search-input { width: 320px; }
.toolbar-actions {
  display: flex;
  align-items: center;
  gap: 8px;
  margin-bottom: 12px;
  flex-wrap: wrap;
}
.pagination-container {
  margin-top: 20px;
  display: flex;
  justify-content: flex-end;
}
</style>
