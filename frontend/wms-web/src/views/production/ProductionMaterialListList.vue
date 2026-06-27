<template>
  <div class="prod-mtrl-list-container">
    <div class="list-panel">
      <div class="header-actions">
        <el-input
          v-model="queryParams.keyword"
          placeholder="搜索单据编号 / 生产工单编号"
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
        <el-button type="primary" @click="handleAdd" v-permission="'prodmateriallist:add'">
          <el-icon><Plus /></el-icon> 新增
        </el-button>
        <el-button @click="handleEditSelected" :disabled="selectedIds.length !== 1" v-permission="'prodmateriallist:edit'">
          <el-icon><Edit /></el-icon> 修改
        </el-button>
        <el-button type="success" @click="handleBatchApprove" :disabled="selectedIds.length === 0" :loading="actionLoading" v-permission="'prodmateriallist:approve'">
          审核{{ selectedIds.length ? ` (${selectedIds.length})` : '' }}
        </el-button>
        <el-button type="warning" @click="handleBatchUnapprove" :disabled="selectedIds.length === 0" :loading="actionLoading" v-permission="'prodmateriallist:approve'">
          反审核{{ selectedIds.length ? ` (${selectedIds.length})` : '' }}
        </el-button>
        <el-button type="danger" @click="handleBatchDelete" :disabled="selectedIds.length === 0" :loading="actionLoading" v-permission="'prodmateriallist:delete'">
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
              <template v-if="col.slotName === 'dateOnly'">
                {{ fmtDateOnly(scope.row[col.prop!]) }}
              </template>
              <template v-else-if="col.slotName === 'date'">
                {{ fmtDate(scope.row[col.prop!]) }}
              </template>
              <template v-else-if="col.slotName === 'bool'">
                <el-checkbox :model-value="!!scope.row[col.prop!]" disabled />
              </template>
              <template v-else-if="col.slotName === 'status'">
                <el-tag :type="scope.row.fStatus === 40 ? 'success' : 'warning'" size="small">
                  {{ scope.row.fStatus === 40 ? '已审核' : '未审核' }}
                </el-tag>
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
  getProductionMaterialLists, deleteProductionMaterialList,
  approveProductionMaterialList, unapproveProductionMaterialList
} from '../../api/productionMaterialList'
import { formatDate, formatDateOnly } from '../../utils/format'
import ColumnSetting from '../../components/ColumnSetting.vue'
import DynamicFilter, { type DynamicFilterInfo } from '../../components/DynamicFilter.vue'
import { useColumnConfig, type ColumnDef } from '../../composables/useColumnConfig'

const router = useRouter()
const tableRef = ref()

const columns: ColumnDef[] = [
  { key: 'fmobillno', label: '生产工单编号', prop: 'fmobillno', width: 170 },
  { key: 'fmoentryseq', label: '生产工单行号', prop: 'fmoentryseq', width: 110, align: 'center' },
  { key: 'fbillno', label: '单据编号', prop: 'fbillno', width: 170 },
  { key: 'fdate', label: '单据日期', prop: 'fdate', width: 110, slotName: 'dateOnly' },
  { key: 'fmaterialNumber', label: '产品代码', prop: 'fmaterialNumber', width: 140 },
  { key: 'fmaterialName', label: '产品名称', prop: 'fmaterialName', minWidth: 150 },
  { key: 'fSpecification', label: '产品规格型号', prop: 'fSpecification', minWidth: 120 },
  { key: 'fqty', label: '生产数量', prop: 'fqty', width: 100, align: 'right' },
  { key: 'fnote', label: '备注', prop: 'fnote', minWidth: 120, defaultVisible: false },
  { key: 'fDisabled', label: '禁用', prop: 'fDisabled', width: 60, align: 'center', slotName: 'bool', defaultVisible: false },
  { key: 'cuserName', label: '制单人', prop: 'cuserName', width: 100 },
  { key: 'cYmd', label: '制单日期', prop: 'cYmd', width: 160, slotName: 'date' },
  { key: 'muserName', label: '修改人', prop: 'muserName', width: 100, defaultVisible: false },
  { key: 'mYmd', label: '修改日期', prop: 'mYmd', width: 160, slotName: 'date', defaultVisible: false },
  { key: 'fcheckerName', label: '审核人', prop: 'fcheckerName', width: 100, defaultVisible: false },
  { key: 'fcheckdate', label: '审核日期', prop: 'fcheckdate', width: 160, slotName: 'date', defaultVisible: false },
  { key: 'fcompanyName', label: '组织', prop: 'fcompanyName', width: 120, defaultVisible: false },
  { key: 'fStatus', label: '数据状态', prop: 'fStatus', width: 90, align: 'center', slotName: 'status', fixed: 'right' },
]

const filterColumns: ColumnDef[] = [
  { key: 'fbillno', label: '单据编号', prop: 'fbillno' },
  { key: 'fmobillno', label: '生产工单编号', prop: 'fmobillno' },
  { key: 'fdate', label: '单据日期', prop: 'fdate' },
  { key: 'fqty', label: '生产数量', prop: 'fqty' },
  { key: 'fStatus', label: '数据状态', prop: 'fStatus', slotName: 'status' },
]

const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig('productionMaterialList', columns)

const loading = ref(false)
const actionLoading = ref(false)
const dataList = ref<any[]>([])
const total = ref(0)
const selectedRows = ref<any[]>([])

const queryParams = reactive({
  page: 1,
  pageSize: 10,
  keyword: '',
  dynamicFilters: [] as DynamicFilterInfo[],
  onlyApproved: false
})

const selectedIds = computed(() =>
  Array.from(new Set(selectedRows.value.map(r => r.uid).filter(Boolean)))
)

// 业务日期：纯业务日期（时间恒 00:00:00），只显示到天
const fmtDateOnly = (d?: string) => {
  if (!d) return ''
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() <= 1900) return ''
  return formatDateOnly(d)
}
// 时间戳：保留时分秒
const fmtDate = (d?: string) => {
  if (!d) return ''
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() <= 1900) return ''
  return formatDate(d)
}

const handleSelectionChange = (rows: any[]) => { selectedRows.value = rows }

async function fetchData() {
  loading.value = true
  try {
    const res: any = await getProductionMaterialLists(queryParams)
    dataList.value = res.data.items
    total.value = res.data.totalCount
  } catch (e) {
    console.error('加载生产用料清单失败:', e)
  } finally {
    loading.value = false
  }
}

const handleAdd = () => router.push({ name: 'ProductionMaterialListEdit' })
const handleEdit = (uid: string) => router.push({ name: 'ProductionMaterialListEdit', query: { uid } })
const handleEditSelected = () => {
  if (selectedIds.value.length === 1) handleEdit(selectedIds.value[0])
}
const handleRowDblClick = (row: any) => {
  if (row.uid) handleEdit(row.uid)
}

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

const handleBatchApprove = () => confirmBatch('审核', `确认审核选中的 ${selectedIds.value.length} 张生产用料清单？`, selectedIds.value, approveProductionMaterialList)
const handleBatchUnapprove = () => confirmBatch('反审核', `确认反审核选中的 ${selectedIds.value.length} 张生产用料清单？`, selectedIds.value, unapproveProductionMaterialList)
const handleBatchDelete = () => confirmBatch('删除', `确认删除选中的 ${selectedIds.value.length} 张生产用料清单？删除后不可恢复。`, selectedIds.value, deleteProductionMaterialList)

onMounted(() => {
  fetchData()
})
</script>

<style scoped>
.prod-mtrl-list-container {
  padding: 20px;
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
}

.list-panel {
  min-width: 0;
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
  width: 320px;
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
