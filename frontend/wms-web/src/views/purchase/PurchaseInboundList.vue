<template>
  <div class="inbound-list-container">
    <div class="list-panel">
      <div class="header-actions">
        <el-input
          v-model="queryParams.keyword"
          placeholder="搜索单据编号"
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
        <el-button type="primary" @click="handleAdd" v-permission="'purchasein:add'">
          <el-icon><Plus /></el-icon> 新增
        </el-button>
        <el-button @click="handleEditSelected" :disabled="selectedOrderIds.length !== 1" v-permission="'purchasein:edit'">
          <el-icon><Edit /></el-icon> 修改
        </el-button>
        <el-button type="success" @click="handleBatchApprove" :disabled="selectedOrderIds.length === 0" :loading="actionLoading" v-permission="'purchasein:approve'">
          审核{{ selectedOrderIds.length ? ` (${selectedOrderIds.length})` : '' }}
        </el-button>
        <el-button type="warning" @click="handleBatchUnapprove" :disabled="selectedOrderIds.length === 0" :loading="actionLoading" v-permission="'purchasein:approve'">
          反审核{{ selectedOrderIds.length ? ` (${selectedOrderIds.length})` : '' }}
        </el-button>
        <el-button type="danger" @click="handleBatchDelete" :disabled="selectedOrderIds.length === 0" :loading="actionLoading" v-permission="'purchasein:delete'">
          删除{{ selectedOrderIds.length ? ` (${selectedOrderIds.length})` : '' }}
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
              <template v-if="col.slotName === 'date'">
                {{ fmtDate(scope.row[col.prop!]) }}
              </template>
              <template v-else-if="col.slotName === 'status'">
                <el-tag :type="scope.row.fStatus === 40 ? 'success' : scope.row.fStatus === 70 ? 'info' : 'warning'" size="small">
                  {{ scope.row.fStatus === 40 ? '已审核' : scope.row.fStatus === 70 ? '已关闭' : '未审核' }}
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
  getInStocks, deleteInStock,
  approveInStock, unapproveInStock
} from '../../api/purchaseInbound'
import { formatDate } from '../../utils/format'
import ColumnSetting from '../../components/ColumnSetting.vue'
import DynamicFilter, { type DynamicFilterInfo } from '../../components/DynamicFilter.vue'
import { useColumnConfig, type ColumnDef } from '../../composables/useColumnConfig'

const router = useRouter()
const tableRef = ref()

// 列表按物料汇总明细行展开（一条明细一行），参照设计列表
const columns: ColumnDef[] = [
  { key: 'fdate', label: '入库日期', prop: 'fdate', width: 110, slotName: 'date' },
  { key: 'fbillno', label: '单据编号', prop: 'fbillno', width: 170 },
  { key: 'fsupplyName', label: '供应商名称', prop: 'fsupplyName', minWidth: 150 },
  { key: 'fentryid', label: '行号', prop: 'fentryid', width: 60, align: 'center' },
  { key: 'fmrdeptName', label: '收料部门', prop: 'fmrdeptName', width: 110, defaultVisible: false },
  { key: 'forderbillno', label: '订单编号', prop: 'forderbillno', width: 150, defaultVisible: false },
  { key: 'fempName', label: '业务员', prop: 'fempName', width: 100, defaultVisible: false },
  { key: 'ftypeName', label: '录入类型', prop: 'ftypeName', width: 90, align: 'center' },
  { key: 'fmaterialNumber', label: '物料代码', prop: 'fmaterialNumber', width: 140 },
  { key: 'fmaterialName', label: '物料名称', prop: 'fmaterialName', minWidth: 150 },
  { key: 'fSpecification', label: '规格型号', prop: 'fSpecification', minWidth: 120, defaultVisible: false },
  { key: 'flot', label: '批次', prop: 'flot', width: 110 },
  { key: 'frealqty', label: '实收数量', prop: 'frealqty', width: 100, align: 'right' },
  { key: 'funitName', label: '单位名称', prop: 'funitName', width: 90 },
  { key: 'fkfdate', label: '生产/采购日期', prop: 'fkfdate', width: 120, slotName: 'date', defaultVisible: false },
  { key: 'ferpno', label: 'ERP单据编号', prop: 'ferpno', width: 140, defaultVisible: false },
  { key: 'fbilltypeName', label: '单据类型', prop: 'fbilltypeName', width: 120, defaultVisible: false },
  { key: 'fcompanyName', label: '组织', prop: 'fcompanyName', width: 120, defaultVisible: false },
  { key: 'cuserName', label: '制单人', prop: 'cuserName', width: 100, defaultVisible: false },
  { key: 'cYmd', label: '制单日期', prop: 'cYmd', width: 110, slotName: 'date', defaultVisible: false },
  { key: 'fcheckerName', label: '审核人', prop: 'fcheckerName', width: 100, defaultVisible: false },
  { key: 'fcheckdate', label: '审核日期', prop: 'fcheckdate', width: 110, slotName: 'date', defaultVisible: false },
  { key: 'fStatus', label: '数据状态', prop: 'fStatus', width: 90, align: 'center', slotName: 'status', fixed: 'right' },
]

// 高级筛选仅开放可在服务端过滤的真实列（表头：单据编号/入库日期/数据状态；明细：实收数量/批次）
const filterColumns: ColumnDef[] = [
  { key: 'fbillno', label: '单据编号', prop: 'fbillno' },
  { key: 'fdate', label: '入库日期', prop: 'fdate' },
  { key: 'frealqty', label: '实收数量', prop: 'frealqty' },
  { key: 'flot', label: '批次', prop: 'flot' },
  { key: 'fStatus', label: '数据状态', prop: 'fStatus', slotName: 'status' },
]

const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig('purchaseInbound', columns)

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

// 明细行可能属于同一单据；操作按去重后的单据 Uid 进行
const selectedOrderIds = computed(() =>
  Array.from(new Set(selectedRows.value.map(r => r.uid).filter(Boolean)))
)

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
    const res: any = await getInStocks(queryParams)
    dataList.value = res.data.items
    total.value = res.data.totalCount
  } catch (e) {
    console.error('加载采购入库单失败:', e)
  } finally {
    loading.value = false
  }
}

const handleAdd = () => router.push({ name: 'PurchaseInboundEdit' })
const handleEdit = (uid: string) => router.push({ name: 'PurchaseInboundEdit', query: { uid } })
const handleEditSelected = () => {
  if (selectedOrderIds.value.length === 1) handleEdit(selectedOrderIds.value[0])
}
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

const handleBatchApprove = () => confirmBatch('审核', `确认审核选中的 ${selectedOrderIds.value.length} 张采购入库单？`, selectedOrderIds.value, approveInStock)
const handleBatchUnapprove = () => confirmBatch('反审核', `确认反审核选中的 ${selectedOrderIds.value.length} 张采购入库单？`, selectedOrderIds.value, unapproveInStock)
const handleBatchDelete = () => confirmBatch('删除', `确认删除选中的 ${selectedOrderIds.value.length} 张采购入库单？删除后不可恢复。`, selectedOrderIds.value, deleteInStock)

onMounted(() => { fetchData() })
</script>

<style scoped>
.inbound-list-container {
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
