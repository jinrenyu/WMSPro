<template>
  <div :class="selectMode ? 'mo-list-embedded' : 'production-order-list-container'">
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

      <div class="toolbar-actions" v-if="!selectMode">
        <el-button type="primary" @click="handleAdd" v-permission="'productionorder:add'">
          <el-icon><Plus /></el-icon> 新增
        </el-button>
        <el-button @click="handleEditSelected" :disabled="selectedOrderIds.length !== 1" v-permission="'productionorder:edit'">
          <el-icon><Edit /></el-icon> 修改
        </el-button>
        <el-button type="success" @click="handleBatchApprove" :disabled="selectedOrderIds.length === 0" :loading="actionLoading" v-permission="'productionorder:approve'">
          审核{{ selectedOrderIds.length ? ` (${selectedOrderIds.length})` : '' }}
        </el-button>
        <el-button type="warning" @click="handleBatchUnapprove" :disabled="selectedOrderIds.length === 0" :loading="actionLoading" v-permission="'productionorder:approve'">
          反审核{{ selectedOrderIds.length ? ` (${selectedOrderIds.length})` : '' }}
        </el-button>
        <el-button type="danger" @click="handleBatchDelete" :disabled="selectedOrderIds.length === 0" :loading="actionLoading" v-permission="'productionorder:delete'">
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
        :highlight-current-row="selectMode"
        @selection-change="handleSelectionChange"
        @row-dblclick="handleRowDblClick"
      >
        <el-table-column v-if="!selectMode" type="selection" width="45" fixed="left" />
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
  getProductionOrders, deleteProductionOrder,
  approveProductionOrder, unapproveProductionOrder
} from '../../api/productionOrder'
import { formatDate, formatDateOnly } from '../../utils/format'
import ColumnSetting from '../../components/ColumnSetting.vue'
import DynamicFilter, { type DynamicFilterInfo } from '../../components/DynamicFilter.vue'
import { useColumnConfig, type ColumnDef } from '../../composables/useColumnConfig'

// 双模式：默认管理页；selectMode 时作为"生产订单选择器"嵌入弹窗（隐藏管理工具栏，双击=选中回填）
const props = withDefaults(defineProps<{
  selectMode?: boolean
  onlyApproved?: boolean
}>(), { selectMode: false, onlyApproved: false })
const emit = defineEmits<{ 'select': [row: any] }>()

const router = useRouter()
const tableRef = ref()

// 列表按明细行展开（一条明细一行）
const columns: ColumnDef[] = [
  { key: 'fdate', label: '单据日期', prop: 'fdate', width: 110, slotName: 'dateOnly' },
  { key: 'fbillno', label: '生产工单编号', prop: 'fbillno', width: 170 },
  { key: 'fentryid', label: '行号', prop: 'fentryid', width: 60, align: 'center' },
  { key: 'fworkshopNumber', label: '车间代码', prop: 'fworkshopNumber', width: 110, defaultVisible: false },
  { key: 'fworkshopName', label: '车间名称', prop: 'fworkshopName', width: 120 },
  { key: 'fmaterialNumber', label: '产品代码', prop: 'fmaterialNumber', width: 140 },
  { key: 'fmaterialName', label: '产品名称', prop: 'fmaterialName', minWidth: 150 },
  { key: 'fchartNumber', label: '产品图号', prop: 'fchartNumber', width: 120, defaultVisible: false },
  { key: 'fSpecification', label: '规格型号', prop: 'fSpecification', minWidth: 120 },
  { key: 'fprorouteName', label: '产品工艺名称', prop: 'fprorouteName', width: 130, defaultVisible: false },
  { key: 'flot', label: '批次', prop: 'flot', width: 100 },
  { key: 'fbaseunitName', label: '单位', prop: 'fbaseunitName', width: 80 },
  { key: 'fqty', label: '计划数量', prop: 'fqty', width: 100, align: 'right' },
  { key: 'ffcqty', label: '派工数量', prop: 'ffcqty', width: 100, align: 'right' },
  { key: 'funfcqty', label: '未派工数量', prop: 'funfcqty', width: 100, align: 'right' },
  { key: 'fbaseunitqty', label: '基本单位计划数量', prop: 'fbaseunitqty', width: 130, align: 'right', defaultVisible: false },
  { key: 'fbstatusName', label: '业务状态', prop: 'fbstatusName', width: 90, align: 'center' },
  { key: 'fissuspend', label: '挂起', prop: 'fissuspend', width: 60, align: 'center', slotName: 'bool', defaultVisible: false },
  { key: 'fmachinemodel', label: '生产机型', prop: 'fmachinemodel', width: 110, defaultVisible: false },
  { key: 'fplanstartdate', label: '计划开工时间', prop: 'fplanstartdate', width: 150, slotName: 'date' },
  { key: 'fplanfinishdate', label: '计划完工时间', prop: 'fplanfinishdate', width: 150, slotName: 'date' },
  { key: 'factualstartdate', label: '实际开工时间', prop: 'factualstartdate', width: 150, slotName: 'date', defaultVisible: false },
  { key: 'factualfinishdate', label: '实际完工时间', prop: 'factualfinishdate', width: 150, slotName: 'date', defaultVisible: false },
  { key: 'fauxpropName', label: '辅助属性', prop: 'fauxpropName', width: 110, defaultVisible: false },
  { key: 'finhighlimit', label: '超产比例(%)', prop: 'finhighlimit', width: 100, align: 'right', defaultVisible: false },
  { key: 'fschedulestatus', label: '排产状态', prop: 'fschedulestatus', width: 90, defaultVisible: false },
  { key: 'fplannerName', label: '计划员', prop: 'fplannerName', width: 100, defaultVisible: false },
  { key: 'cuserName', label: '制单人', prop: 'cuserName', width: 100, defaultVisible: false },
  { key: 'cYmd', label: '制单日期', prop: 'cYmd', width: 160, slotName: 'date', defaultVisible: false },
  { key: 'muserName', label: '修改人', prop: 'muserName', width: 100, defaultVisible: false },
  { key: 'mYmd', label: '修改日期', prop: 'mYmd', width: 160, slotName: 'date', defaultVisible: false },
  { key: 'fcheckerName', label: '审核人', prop: 'fcheckerName', width: 100, defaultVisible: false },
  { key: 'fcheckdate', label: '审核日期', prop: 'fcheckdate', width: 160, slotName: 'date', defaultVisible: false },
  { key: 'fcompanyName', label: '组织', prop: 'fcompanyName', width: 120, defaultVisible: false },
  { key: 'fStatus', label: '审核状态', prop: 'fStatus', width: 90, align: 'center', slotName: 'status', fixed: 'right' },
]

// 高级筛选仅开放可在服务端过滤的真实列
const filterColumns: ColumnDef[] = [
  { key: 'fbillno', label: '单据编号', prop: 'fbillno' },
  { key: 'fdate', label: '单据日期', prop: 'fdate' },
  { key: 'fqty', label: '计划数量', prop: 'fqty' },
  { key: 'flot', label: '批次', prop: 'flot' },
  { key: 'fStatus', label: '审核状态', prop: 'fStatus', slotName: 'status' },
]

const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig(props.selectMode ? 'productionOrder-picker' : 'productionOrder', columns)

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

// 明细行可能属于同一订单；操作按去重后的订单 Uid 进行
const selectedOrderIds = computed(() =>
  Array.from(new Set(selectedRows.value.map(r => r.uid).filter(Boolean)))
)

// 业务日期：纯业务日期（时间恒 00:00:00），只显示到天
const fmtDateOnly = (d?: string) => {
  if (!d) return ''
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() <= 1900) return ''
  return formatDateOnly(d)
}
// 时间戳/计划时间：保留时分秒
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
    const res: any = await getProductionOrders(queryParams)
    dataList.value = res.data.items
    total.value = res.data.totalCount
  } catch (e) {
    console.error('加载生产订单失败:', e)
  } finally {
    loading.value = false
  }
}

const handleAdd = () => router.push({ name: 'ProductionOrderEdit' })
const handleEdit = (uid: string) => router.push({ name: 'ProductionOrderEdit', query: { uid } })
const handleEditSelected = () => {
  if (selectedOrderIds.value.length === 1) handleEdit(selectedOrderIds.value[0])
}
const handleRowDblClick = (row: any) => {
  if (props.selectMode) { emit('select', row); return }
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

const handleBatchApprove = () => confirmBatch('审核', `确认审核选中的 ${selectedOrderIds.value.length} 张生产订单？`, selectedOrderIds.value, approveProductionOrder)
const handleBatchUnapprove = () => confirmBatch('反审核', `确认反审核选中的 ${selectedOrderIds.value.length} 张生产订单？`, selectedOrderIds.value, unapproveProductionOrder)
const handleBatchDelete = () => confirmBatch('删除', `确认删除选中的 ${selectedOrderIds.value.length} 张生产订单？删除后不可恢复。`, selectedOrderIds.value, deleteProductionOrder)

onMounted(() => {
  queryParams.onlyApproved = props.onlyApproved
  fetchData()
})
</script>

<style scoped>
.production-order-list-container {
  padding: 20px;
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
}

/* 选择器嵌入弹窗时去掉整页卡片样式 */
.mo-list-embedded {
  padding: 0;
  background-color: transparent;
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
