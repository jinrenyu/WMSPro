<template>
  <div :class="selectMode ? 'mra-list-embedded' : 'material-return-apply-list-container'">
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
        <el-button type="primary" @click="handleAdd" v-permission="'returnreq:add'">
          <el-icon><Plus /></el-icon> 新增
        </el-button>
        <el-button @click="handleEditSelected" :disabled="selectedOrderIds.length !== 1" v-permission="'returnreq:edit'">
          <el-icon><Edit /></el-icon> 修改
        </el-button>
        <el-button type="success" @click="handleBatchApprove" :disabled="selectedOrderIds.length === 0" :loading="actionLoading" v-permission="'returnreq:approve'">
          审核{{ selectedOrderIds.length ? ` (${selectedOrderIds.length})` : '' }}
        </el-button>
        <el-button type="warning" @click="handleBatchUnapprove" :disabled="selectedOrderIds.length === 0" :loading="actionLoading" v-permission="'returnreq:approve'">
          反审核{{ selectedOrderIds.length ? ` (${selectedOrderIds.length})` : '' }}
        </el-button>
        <el-button type="danger" @click="handleBatchDelete" :disabled="selectedOrderIds.length === 0" :loading="actionLoading" v-permission="'returnreq:delete'">
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
              <template v-if="col.slotName === 'date'">
                {{ fmtDate(scope.row[col.prop!]) }}
              </template>
              <template v-else-if="col.slotName === 'bool'">
                <el-checkbox :model-value="!!scope.row[col.prop!]" disabled />
              </template>
              <template v-else-if="col.slotName === 'rmtype'">
                {{ rmTypeLabel(scope.row.frmtype) }}
              </template>
              <template v-else-if="col.slotName === 'bizType'">
                {{ bizTypeLabel(scope.row.fbusinesstype) }}
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
import { ref, reactive, onMounted, computed } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import { Search, Plus, Edit } from '@element-plus/icons-vue'
import {
  getMaterialReturnApplies, deleteMaterialReturnApply,
  approveMaterialReturnApply, unapproveMaterialReturnApply
} from '../../api/materialReturnApply'
import { formatDate } from '../../utils/format'
import ColumnSetting from '../../components/ColumnSetting.vue'
import DynamicFilter, { type DynamicFilterInfo } from '../../components/DynamicFilter.vue'
import { useColumnConfig, type ColumnDef } from '../../composables/useColumnConfig'

// 双模式：默认管理页；selectMode 时作为"退料申请单选择器"嵌入弹窗（隐藏管理工具栏，双击=选中回填）
const props = withDefaults(defineProps<{
  selectMode?: boolean
  onlyApproved?: boolean
}>(), { selectMode: false, onlyApproved: false })
const emit = defineEmits<{ 'select': [row: any] }>()

const router = useRouter()
const tableRef = ref()

// 列表按明细行展开（一条明细一行），参照设计列表
const columns: ColumnDef[] = [
  { key: 'fdate', label: '单据日期', prop: 'fdate', width: 110, slotName: 'date' },
  { key: 'fbillno', label: '单据编号', prop: 'fbillno', width: 170 },
  { key: 'fentryid', label: '行号', prop: 'fentryid', width: 60, align: 'center' },
  { key: 'fbusinesstype', label: '业务类型', prop: 'fbusinesstype', width: 100, slotName: 'bizType' },
  { key: 'frmtype', label: '退料类型', prop: 'frmtype', width: 90, slotName: 'rmtype', defaultVisible: false },
  { key: 'frequireorgName', label: '需求组织', prop: 'frequireorgName', width: 120, defaultVisible: false },
  { key: 'fappdeptName', label: '退料部门', prop: 'fappdeptName', width: 120 },
  { key: 'fpurchaseorgName', label: '采购组织', prop: 'fpurchaseorgName', width: 120, defaultVisible: false },
  { key: 'fmaterialNumber', label: '物料代码', prop: 'fmaterialNumber', width: 140 },
  { key: 'fmaterialName', label: '物料名称', prop: 'fmaterialName', minWidth: 150 },
  { key: 'fSpecification', label: '规格型号', prop: 'fSpecification', minWidth: 120 },
  { key: 'flot', label: '批次', prop: 'flot', width: 110, defaultVisible: false },
  { key: 'fauxpropName', label: '辅助属性', prop: 'fauxpropName', width: 110, defaultVisible: false },
  { key: 'fmrappqty', label: '申请退料数量', prop: 'fmrappqty', width: 110, align: 'right' },
  { key: 'fmrbqty', label: '累计退料数量', prop: 'fmrbqty', width: 110, align: 'right', defaultVisible: false },
  { key: 'freplenishqty', label: '补料数量', prop: 'freplenishqty', width: 100, align: 'right', defaultVisible: false },
  { key: 'fstockunitNumber', label: '库存单位代码', prop: 'fstockunitNumber', width: 110, defaultVisible: false },
  { key: 'fstockunitName', label: '库存单位名称', prop: 'fstockunitName', width: 110 },
  { key: 'fstockNumber', label: '仓库代码', prop: 'fstockNumber', width: 110, defaultVisible: false },
  { key: 'fstockName', label: '仓库名称', prop: 'fstockName', width: 120 },
  { key: 'fstocklocName', label: '仓位名称', prop: 'fstocklocName', width: 110, defaultVisible: false },
  { key: 'famount', label: '退料金额', prop: 'famount', width: 110, align: 'right', defaultVisible: false },
  { key: 'fsupplyName', label: '供应商', prop: 'fsupplyName', minWidth: 150 },
  { key: 'fsrcbillno', label: '源单编号', prop: 'fsrcbillno', width: 150, defaultVisible: false },
  { key: 'cuserName', label: '制单人', prop: 'cuserName', width: 100, defaultVisible: false },
  { key: 'cYmd', label: '制单日期', prop: 'cYmd', width: 110, slotName: 'date', defaultVisible: false },
  { key: 'fcheckerName', label: '审核人', prop: 'fcheckerName', width: 100, defaultVisible: false },
  { key: 'fcheckdate', label: '审核日期', prop: 'fcheckdate', width: 110, slotName: 'date', defaultVisible: false },
  { key: 'fStatus', label: '数据状态', prop: 'fStatus', width: 90, align: 'center', slotName: 'status', fixed: 'right' },
]

// 高级筛选仅开放可在服务端过滤的真实列（表头：单据编号/单据日期/审核状态；明细：申请退料数量/批次）
const filterColumns: ColumnDef[] = [
  { key: 'fbillno', label: '单据编号', prop: 'fbillno' },
  { key: 'fdate', label: '单据日期', prop: 'fdate' },
  { key: 'fmrappqty', label: '申请退料数量', prop: 'fmrappqty' },
  { key: 'flot', label: '批次', prop: 'flot' },
  { key: 'fStatus', label: '审核状态', prop: 'fStatus', slotName: 'status' },
]

// 选择器用独立的列配置 key，避免与"退料申请单管理"页相互影响
const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig(props.selectMode ? 'materialReturnApply-picker' : 'materialReturnApply', columns)

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
const bizTypeMap: Record<string, string> = {
  CG: '标准采购', WW: '标准委外', ZCCG: '资产采购', ZYCG: '直运采购', FYCG: '费用采购', VMICG: 'VMI采购', CSCG: '测试采购'
}
const bizTypeLabel = (v?: string) => (v ? (bizTypeMap[v] || v) : '')
const rmTypeLabel = (v?: string) => (v === 'A' ? '检验退料' : v === 'B' ? '库存退料' : '')

const handleSelectionChange = (rows: any[]) => { selectedRows.value = rows }

async function fetchData() {
  loading.value = true
  try {
    const res: any = await getMaterialReturnApplies(queryParams)
    dataList.value = res.data.items
    total.value = res.data.totalCount
  } catch (e) {
    console.error('加载退料申请单失败:', e)
  } finally {
    loading.value = false
  }
}

const handleAdd = () => router.push({ name: 'MaterialReturnApplyEdit' })
const handleEdit = (uid: string) => router.push({ name: 'MaterialReturnApplyEdit', query: { uid } })
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

const handleBatchApprove = () => confirmBatch('审核', `确认审核选中的 ${selectedOrderIds.value.length} 张退料申请单？`, selectedOrderIds.value, approveMaterialReturnApply)
const handleBatchUnapprove = () => confirmBatch('反审核', `确认反审核选中的 ${selectedOrderIds.value.length} 张退料申请单？`, selectedOrderIds.value, unapproveMaterialReturnApply)
const handleBatchDelete = () => confirmBatch('删除', `确认删除选中的 ${selectedOrderIds.value.length} 张退料申请单？删除后不可恢复。`, selectedOrderIds.value, deleteMaterialReturnApply)

onMounted(() => {
  queryParams.onlyApproved = props.onlyApproved
  fetchData()
})
</script>

<style scoped>
.material-return-apply-list-container {
  padding: 20px;
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
}

/* 选择器嵌入弹窗时去掉整页卡片样式 */
.mra-list-embedded {
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
