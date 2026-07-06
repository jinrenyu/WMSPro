<template>
  <div class="erpsync-list-container">
    <div class="list-panel">
      <div class="header-actions">
        <el-input
          v-model="queryParams.keyword"
          placeholder="搜索功能代码 / 功能名称"
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
        <el-button type="primary" @click="handleAdd" v-permission="'erpsync:add'">
          <el-icon><Plus /></el-icon> 新增
        </el-button>
        <el-button @click="handleEditSelected" :disabled="selectedIds.length !== 1" v-permission="'erpsync:edit'">
          <el-icon><Edit /></el-icon> 修改
        </el-button>
        <el-button type="success" @click="handleBatchApprove" :disabled="selectedIds.length === 0" :loading="actionLoading" v-permission="'erpsync:approve'">
          审核{{ selectedIds.length ? ` (${selectedIds.length})` : '' }}
        </el-button>
        <el-button type="warning" @click="handleBatchUnapprove" :disabled="selectedIds.length === 0" :loading="actionLoading" v-permission="'erpsync:approve'">
          反审核{{ selectedIds.length ? ` (${selectedIds.length})` : '' }}
        </el-button>
        <el-button type="danger" @click="handleBatchDelete" :disabled="selectedIds.length === 0" :loading="actionLoading" v-permission="'erpsync:delete'">
          删除{{ selectedIds.length ? ` (${selectedIds.length})` : '' }}
        </el-button>
        <el-button type="primary" plain @click="handleSyncSelected" :disabled="selectedIds.length !== 1" :loading="syncLoading" v-permission="'erpsync:sync'">
          <el-icon><Refresh /></el-icon> 立即同步
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
              <template v-else-if="col.slotName === 'timetype'">
                {{ timeTypeText(scope.row.ftimetype) }}
              </template>
              <template v-else-if="col.slotName === 'datetime'">
                {{ fmtDateTime(scope.row[col.prop!]) }}
              </template>
              <template v-else-if="col.slotName === 'status'">
                <el-tag :type="statusTagType(scope.row.fStatus)" size="small">{{ scope.row.fstatusName || statusText(scope.row.fStatus) }}</el-tag>
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
import { Search, Plus, Edit, Refresh } from '@element-plus/icons-vue'
import {
  getErpSyncConfigs, deleteErpSyncConfig,
  approveErpSyncConfig, unapproveErpSyncConfig, syncErpSyncConfig
} from '../../api/erpSyncConfig'
import { formatDate } from '../../utils/format'
import ColumnSetting from '../../components/ColumnSetting.vue'
import DynamicFilter, { type DynamicFilterInfo } from '../../components/DynamicFilter.vue'
import { useColumnConfig, type ColumnDef } from '../../composables/useColumnConfig'

const router = useRouter()
const tableRef = ref()

// 列表按主表一行（对应设计图：功能代码/功能名称/开始时间点/同步频率/时间单位/同步过滤规则/启用同步/系统预设/备注）
const columns: ColumnDef[] = [
  { key: 'fnumber', label: '功能代码', prop: 'fnumber', width: 170, fixed: 'left' },
  { key: 'fname', label: '功能名称', prop: 'fname', minWidth: 140 },
  { key: 'ferpbillid', label: 'ERP表单ID', prop: 'ferpbillid', width: 150 },
  { key: 'fstart', label: '开始时间点', prop: 'fstart', width: 100, align: 'center' },
  { key: 'fsynrate', label: '同步频率', prop: 'fsynrate', width: 90, align: 'center' },
  { key: 'ftimetype', label: '时间单位', prop: 'ftimetype', width: 90, align: 'center', slotName: 'timetype' },
  { key: 'fruleid', label: '同步过滤规则', prop: 'fruleid', minWidth: 160 },
  { key: 'fisenable', label: '启用同步', prop: 'fisenable', width: 90, align: 'center', slotName: 'bool' },
  { key: 'isdefault', label: '系统预设', prop: 'isdefault', width: 90, align: 'center', slotName: 'bool' },
  { key: 'ftimestamp', label: '最后同步时间', prop: 'ftimestamp', width: 160, slotName: 'datetime' },
  { key: 'fStatus', label: '数据状态', prop: 'fStatus', width: 90, align: 'center', slotName: 'status' },
  { key: 'fnote', label: '备注', prop: 'fnote', minWidth: 140 },
]

const filterColumns: ColumnDef[] = [
  { key: 'fnumber', label: '功能代码', prop: 'fnumber' },
  { key: 'fname', label: '功能名称', prop: 'fname' },
  { key: 'fStatus', label: '数据状态', prop: 'fStatus', slotName: 'status' },
]

const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig('erpSyncConfig', columns)

const loading = ref(false)
const actionLoading = ref(false)
const syncLoading = ref(false)
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
const timeTypeText = (t?: string) => ({ '1': '秒', '2': '分', '3': '小时', '4': '天' } as Record<string, string>)[t || ''] || ''
const statusText = (s?: number) => (s === 40 ? '审核' : '暂存')
const statusTagType = (s?: number) => (s === 40 ? 'success' : 'warning')

const handleSelectionChange = (rows: any[]) => { selectedRows.value = rows }

async function fetchData() {
  loading.value = true
  try {
    const res: any = await getErpSyncConfigs(queryParams)
    dataList.value = res.data.items
    total.value = res.data.totalCount
  } catch (e) {
    console.error('加载ERP集成配置失败:', e)
  } finally {
    loading.value = false
  }
}

const handleAdd = () => router.push({ name: 'ErpSyncConfigEdit' })
const handleEdit = (uid: string) => router.push({ name: 'ErpSyncConfigEdit', query: { uid } })
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

const handleBatchApprove = () => confirmBatch('审核', `确认审核选中的 ${selectedIds.value.length} 条配置？`, selectedIds.value, approveErpSyncConfig)
const handleBatchUnapprove = () => confirmBatch('反审核', `确认反审核选中的 ${selectedIds.value.length} 条配置？`, selectedIds.value, unapproveErpSyncConfig)
const handleBatchDelete = () => confirmBatch('删除', `确认删除选中的 ${selectedIds.value.length} 条配置？删除后不可恢复。`, selectedIds.value, deleteErpSyncConfig)

async function handleSyncSelected() {
  if (selectedIds.value.length !== 1) return
  const r = await ElMessageBox.confirm('确认立即执行一次同步？（需配置已审核）', '立即同步', { type: 'warning' }).catch(() => 'cancel')
  if (r === 'cancel') return
  syncLoading.value = true
  try {
    const res: any = await syncErpSyncConfig(selectedIds.value[0])
    const data = res.data || {}
    if (data.success) ElMessage.success(data.message || `同步完成，成功 ${data.successCount} 条`)
    else ElMessage.warning(data.message || `同步完成：成功 ${data.successCount}，失败 ${data.failCount}`)
    await fetchData()
  } catch (e: any) {
    ElMessage.error(e?.message || '同步失败')
  } finally {
    syncLoading.value = false
  }
}

onMounted(() => { fetchData() })
</script>

<style scoped>
.erpsync-list-container {
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
