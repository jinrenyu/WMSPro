<template>
  <div class="rn-label-list-container">
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
        <el-button type="primary" :disabled="selectedRows.length !== 1" @click="handlePrint" v-permission="'labelreceivenotice:generate'">
          <el-icon><PriceTag /></el-icon> 条码打印
        </el-button>
        <el-button @click="fetchData">
          <el-icon><Refresh /></el-icon> 刷新
        </el-button>
      </div>

      <el-table
        ref="tableRef"
        v-loading="loading"
        :data="dataList"
        style="width: 100%"
        border
        size="small"
        highlight-current-row
        @selection-change="handleSelectionChange"
        @row-dblclick="handlePrintRow"
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
                <el-tag :type="scope.row.fStatus === 40 ? 'success' : (scope.row.fStatus === 70 ? 'info' : 'warning')" size="small">
                  {{ scope.row.fstatusName || (scope.row.fStatus === 40 ? '已审核' : scope.row.fStatus === 70 ? '已关闭' : '未审核') }}
                </el-tag>
              </template>
              <template v-else-if="col.slotName === 'bool'">
                <el-checkbox :model-value="!!scope.row[col.prop!]" disabled />
              </template>
              <template v-else-if="col.slotName === 'kfunit'">
                {{ kfUnitLabel(scope.row[col.prop!]) }}
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
import { ref, reactive, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage } from 'element-plus'
import { Search, PriceTag, Refresh } from '@element-plus/icons-vue'
import { getReceiveNoticeLabels } from '../../api/receiveNoticeLabel'
import { formatDate } from '../../utils/format'
import ColumnSetting from '../../components/ColumnSetting.vue'
import DynamicFilter, { type DynamicFilterInfo } from '../../components/DynamicFilter.vue'
import { useColumnConfig, type ColumnDef } from '../../composables/useColumnConfig'

const router = useRouter()
const tableRef = ref()

// 列表按收料通知单明细行展开（一条明细一行），列对应设计图
const columns: ColumnDef[] = [
  { key: 'fdate', label: '收料日期', prop: 'fdate', width: 110, slotName: 'date' },
  { key: 'fpurorgName', label: '收料组织', prop: 'fpurorgName', width: 120 },
  { key: 'fsupplyName', label: '供应商', prop: 'fsupplyName', minWidth: 150 },
  { key: 'fbillno', label: '单据编号', prop: 'fbillno', width: 180 },
  { key: 'fentryid', label: '单据行号', prop: 'fentryid', width: 80, align: 'center' },
  { key: 'fmaterialNumber', label: '物料编码', prop: 'fmaterialNumber', width: 140 },
  { key: 'fmaterialName', label: '物料名称', prop: 'fmaterialName', minWidth: 150 },
  { key: 'fSpecification', label: '规格型号', prop: 'fSpecification', minWidth: 120 },
  { key: 'flot', label: '批次', prop: 'flot', width: 110 },
  { key: 'fauxpropName', label: '辅助属性', prop: 'fauxpropName', width: 110 },
  { key: 'factreceiveqty', label: '交货数量', prop: 'factreceiveqty', width: 100, align: 'right' },
  { key: 'funitName', label: '单位', prop: 'funitName', width: 90 },
  { key: 'fkfdate', label: '采购日期', prop: 'fkfdate', width: 110, slotName: 'date' },
  { key: 'fisKfPeriod', label: '是否启用保质期', prop: 'fisKfPeriod', width: 120, align: 'center', slotName: 'bool' },
  { key: 'fKfPeriod', label: '保质期限', prop: 'fKfPeriod', width: 90, align: 'right' },
  { key: 'fKfUnit', label: '保质期单位', prop: 'fKfUnit', width: 100, align: 'center', slotName: 'kfunit' },
  { key: 'fsupplyNumber', label: '供应商编码', prop: 'fsupplyNumber', width: 120, defaultVisible: false },
  { key: 'finstockqty', label: '累计入库数量', prop: 'finstockqty', width: 120, align: 'right', defaultVisible: false },
  { key: 'fincreaseqty', label: '最小包装量', prop: 'fincreaseqty', width: 110, align: 'right', defaultVisible: false },
  { key: 'fStatus', label: '数据状态', prop: 'fStatus', width: 90, align: 'center', slotName: 'status', defaultVisible: false },
  { key: 'cuserName', label: '制单人', prop: 'cuserName', width: 100 },
  { key: 'cYmd', label: '创建日期', prop: 'cYmd', width: 150, slotName: 'date' },
  { key: 'fcheckerName', label: '审核人', prop: 'fcheckerName', width: 100 },
  { key: 'fcheckdate', label: '审核日期', prop: 'fcheckdate', width: 150, slotName: 'date' },
]

// 高级筛选仅开放可在服务端过滤的真实列（表头：单据编号/收料日期；明细：交货数量/累计入库数量）
const filterColumns: ColumnDef[] = [
  { key: 'fbillno', label: '单据编号', prop: 'fbillno' },
  { key: 'fdate', label: '收料日期', prop: 'fdate' },
  { key: 'factreceiveqty', label: '交货数量', prop: 'factreceiveqty' },
  { key: 'finstockqty', label: '累计入库数量', prop: 'finstockqty' },
]

const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig('receiveNoticeLabel', columns)

const loading = ref(false)
const dataList = ref<any[]>([])
const total = ref(0)
const selectedRows = ref<any[]>([])

const queryParams = reactive({
  page: 1,
  pageSize: 10,
  keyword: '',
  dynamicFilters: [] as DynamicFilterInfo[]
})

const fmtDate = (d?: string) => {
  if (!d) return ''
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() <= 1900) return ''
  return formatDate(d).slice(0, 10)
}
const kfUnitLabel = (v?: number) => (v === 1 ? '月' : v === 2 ? '年' : '日')

const handleSelectionChange = (rows: any[]) => { selectedRows.value = rows }

async function fetchData() {
  loading.value = true
  try {
    const res: any = await getReceiveNoticeLabels(queryParams)
    dataList.value = res.data.items
    total.value = res.data.totalCount
  } catch (e) {
    console.error('加载收料通知单标签列表失败:', e)
  } finally {
    loading.value = false
  }
}

const openGenerate = (row: any) => {
  if (!row?.entryUid) { ElMessage.warning('该行缺少明细标识，无法生成条码'); return }
  router.push({ name: 'ReceiveNoticeLabelGenerate', query: { entryUid: row.entryUid } })
}
const handlePrint = () => { if (selectedRows.value.length === 1) openGenerate(selectedRows.value[0]) }
const handlePrintRow = (row: any) => openGenerate(row)

onMounted(fetchData)
</script>

<style scoped>
.rn-label-list-container {
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
.header-right { display: flex; align-items: center; gap: 8px; }
.search-input { width: 320px; }
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
