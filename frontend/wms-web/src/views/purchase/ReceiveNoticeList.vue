<template>
  <div :class="selectMode ? 'rn-list-embedded' : 'receive-notice-list-container'">
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
        <el-button type="primary" @click="handleAdd" v-permission="'receivenotice:add'">
          <el-icon><Plus /></el-icon> 新增
        </el-button>
        <el-button @click="handleEditSelected" :disabled="selectedOrderIds.length !== 1" v-permission="'receivenotice:edit'">
          <el-icon><Edit /></el-icon> 修改
        </el-button>
        <el-button type="success" @click="handleBatchApprove" :disabled="selectedOrderIds.length === 0" :loading="actionLoading" v-permission="'receivenotice:approve'">
          审核{{ selectedOrderIds.length ? ` (${selectedOrderIds.length})` : '' }}
        </el-button>
        <el-button type="warning" @click="handleBatchUnapprove" :disabled="selectedOrderIds.length === 0" :loading="actionLoading" v-permission="'receivenotice:approve'">
          反审核{{ selectedOrderIds.length ? ` (${selectedOrderIds.length})` : '' }}
        </el-button>
        <el-button type="danger" @click="handleBatchDelete" :disabled="selectedOrderIds.length === 0" :loading="actionLoading" v-permission="'receivenotice:delete'">
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
  getReceiveNotices, deleteReceiveNotice,
  approveReceiveNotice, unapproveReceiveNotice
} from '../../api/receiveNotice'
import { formatDateOnly } from '../../utils/format'
import ColumnSetting from '../../components/ColumnSetting.vue'
import DynamicFilter, { type DynamicFilterInfo } from '../../components/DynamicFilter.vue'
import { useColumnConfig, type ColumnDef } from '../../composables/useColumnConfig'

// 双模式：默认管理页；selectMode 时作为"收料通知单选择器"嵌入弹窗（隐藏管理工具栏，双击=选中回填）
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
  { key: 'fbilltypeName', label: '单据类型', prop: 'fbilltypeName', width: 110 },
  { key: 'fentryid', label: '单据行号', prop: 'fentryid', width: 70, align: 'center' },
  { key: 'fmaterialNumber', label: '物料编码', prop: 'fmaterialNumber', width: 140 },
  { key: 'fmaterialName', label: '物料名称', prop: 'fmaterialName', minWidth: 150 },
  { key: 'fSpecification', label: '规格型号', prop: 'fSpecification', minWidth: 120, defaultVisible: false },
  { key: 'factreceiveqty', label: '收料数量', prop: 'factreceiveqty', width: 100, align: 'right' },
  { key: 'fgodqty', label: '检验合格数量', prop: 'fgodqty', width: 110, align: 'right' },
  { key: 'fscrapqty', label: '样本破坏数', prop: 'fscrapqty', width: 100, align: 'right', defaultVisible: false },
  { key: 'finstockqty', label: '累计入库数量', prop: 'finstockqty', width: 110, align: 'right' },
  { key: 'funitName', label: '单位名称', prop: 'funitName', width: 90 },
  { key: 'fbaseunitqty', label: '基本单位数量', prop: 'fbaseunitqty', width: 110, align: 'right', defaultVisible: false },
  { key: 'fbaseunitName', label: '基本单位名称', prop: 'fbaseunitName', width: 110, defaultVisible: false },
  { key: 'fauxpropName', label: '辅助属性', prop: 'fauxpropName', width: 110, defaultVisible: false },
  { key: 'fpredeliverydate', label: '预计到货日期', prop: 'fpredeliverydate', width: 120, slotName: 'date', defaultVisible: false },
  { key: 'fisBatchManage', label: '启用批号管理', prop: 'fisBatchManage', width: 100, align: 'center', slotName: 'bool', defaultVisible: false },
  { key: 'flot', label: '批次', prop: 'flot', width: 110, defaultVisible: false },
  { key: 'fprice', label: '单价', prop: 'fprice', width: 100, align: 'right', defaultVisible: false },
  { key: 'fsupplyNumber', label: '供应商编码', prop: 'fsupplyNumber', width: 120, defaultVisible: false },
  { key: 'fsupplyName', label: '供应商名称', prop: 'fsupplyName', minWidth: 150 },
  { key: 'fpurchaserName', label: '业务员名称', prop: 'fpurchaserName', width: 100, defaultVisible: false },
  { key: 'freceivedeptNumber', label: '收料部门代码', prop: 'freceivedeptNumber', width: 110, defaultVisible: false },
  { key: 'freceivedeptName', label: '收料部门名称', prop: 'freceivedeptName', width: 120, defaultVisible: false },
  { key: 'fstockNumber', label: '仓库代码', prop: 'fstockNumber', width: 110, defaultVisible: false },
  { key: 'fstockName', label: '仓库名称', prop: 'fstockName', width: 120 },
  { key: 'fisOpenLocation', label: '启用仓位管理', prop: 'fisOpenLocation', width: 100, align: 'center', slotName: 'bool', defaultVisible: false },
  { key: 'fstocklocName', label: '仓位名称', prop: 'fstocklocName', width: 110, defaultVisible: false },
  { key: 'forderbillno', label: '订单编号', prop: 'forderbillno', width: 150, defaultVisible: false },
  { key: 'forderentryid', label: '订单明细行号', prop: 'forderentryid', width: 100, align: 'center', defaultVisible: false },
  { key: 'fpurorgName', label: '采购组织', prop: 'fpurorgName', width: 120, defaultVisible: false },
  { key: 'fStatus', label: '审核状态', prop: 'fStatus', width: 90, align: 'center', slotName: 'status', fixed: 'right' },
]

// 高级筛选仅开放可在服务端过滤的真实列（表头：单据编号/单据日期/审核状态；明细：收料数量/批次）
const filterColumns: ColumnDef[] = [
  { key: 'fbillno', label: '单据编号', prop: 'fbillno' },
  { key: 'fdate', label: '单据日期', prop: 'fdate' },
  { key: 'factreceiveqty', label: '收料数量', prop: 'factreceiveqty' },
  { key: 'flot', label: '批次', prop: 'flot' },
  { key: 'fStatus', label: '审核状态', prop: 'fStatus', slotName: 'status' },
]

// 选择器用独立的列配置 key，避免与"收料通知单管理"页相互影响
const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig(props.selectMode ? 'receiveNotice-picker' : 'receiveNotice', columns)

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
  // 单据日期/预计到货日期为纯业务日期（无时分秒），只显示到天
  return formatDateOnly(d)
}

const handleSelectionChange = (rows: any[]) => { selectedRows.value = rows }

async function fetchData() {
  loading.value = true
  try {
    const res: any = await getReceiveNotices(queryParams)
    dataList.value = res.data.items
    total.value = res.data.totalCount
  } catch (e) {
    console.error('加载收料通知单失败:', e)
  } finally {
    loading.value = false
  }
}

const handleAdd = () => router.push({ name: 'ReceiveNoticeEdit' })
const handleEdit = (uid: string) => router.push({ name: 'ReceiveNoticeEdit', query: { uid } })
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

const handleBatchApprove = () => confirmBatch('审核', `确认审核选中的 ${selectedOrderIds.value.length} 张收料通知单？`, selectedOrderIds.value, approveReceiveNotice)
const handleBatchUnapprove = () => confirmBatch('反审核', `确认反审核选中的 ${selectedOrderIds.value.length} 张收料通知单？`, selectedOrderIds.value, unapproveReceiveNotice)
const handleBatchDelete = () => confirmBatch('删除', `确认删除选中的 ${selectedOrderIds.value.length} 张收料通知单？删除后不可恢复。`, selectedOrderIds.value, deleteReceiveNotice)

onMounted(() => {
  queryParams.onlyApproved = props.onlyApproved
  fetchData()
})
</script>

<style scoped>
.receive-notice-list-container {
  padding: 20px;
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
}

/* 选择器嵌入弹窗时去掉整页卡片样式 */
.rn-list-embedded {
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
