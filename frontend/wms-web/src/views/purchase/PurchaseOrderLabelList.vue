<template>
  <div class="po-label-list-container">
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
        <el-button type="primary" :disabled="selectedRows.length !== 1" @click="handlePrint" v-permission="'labelpurchaseorder:generate'">
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
                <el-tag :type="scope.row.fStatus === 40 ? 'success' : 'warning'" size="small">
                  {{ scope.row.fstatusName || (scope.row.fStatus === 40 ? '已审核' : '未审核') }}
                </el-tag>
              </template>
              <template v-else-if="col.slotName === 'bool'">
                <el-checkbox :model-value="!!scope.row[col.prop!]" disabled />
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
import { getPurchaseOrderLabels } from '../../api/purchaseOrderLabel'
import { formatDate } from '../../utils/format'
import ColumnSetting from '../../components/ColumnSetting.vue'
import DynamicFilter, { type DynamicFilterInfo } from '../../components/DynamicFilter.vue'
import { useColumnConfig, type ColumnDef } from '../../composables/useColumnConfig'

const router = useRouter()
const tableRef = ref()

// 列表按采购订单明细行展开（一条明细一行），列对应设计图
const columns: ColumnDef[] = [
  { key: 'fdate', label: '订单日期', prop: 'fdate', width: 110, slotName: 'date' },
  { key: 'fbillno', label: '单据编号', prop: 'fbillno', width: 180 },
  { key: 'fentryid', label: '行号', prop: 'fentryid', width: 60, align: 'center', defaultVisible: false },
  { key: 'fmaterialNumber', label: '物料代码', prop: 'fmaterialNumber', width: 140 },
  { key: 'fmaterialName', label: '物料名称', prop: 'fmaterialName', minWidth: 150 },
  { key: 'fcompanyName', label: '采购组织', prop: 'fcompanyName', width: 120 },
  { key: 'fSpecification', label: '规格型号', prop: 'fSpecification', minWidth: 120 },
  { key: 'fauxpropName', label: '辅助属性', prop: 'fauxpropName', width: 110, defaultVisible: false },
  { key: 'funitName', label: '单位', prop: 'funitName', width: 90 },
  { key: 'fsupplyNumber', label: '供应商编码', prop: 'fsupplyNumber', width: 120 },
  { key: 'fsupplyName', label: '供应商名称', prop: 'fsupplyName', minWidth: 150 },
  { key: 'fqty', label: '数量', prop: 'fqty', width: 100, align: 'right' },
  { key: 'finstockqty', label: '累计入库数量', prop: 'finstockqty', width: 120, align: 'right' },
  { key: 'fincreaseqty', label: '最小包装量', prop: 'fincreaseqty', width: 110, align: 'right' },
  { key: 'fkfperiod', label: '保质期限', prop: 'fkfperiod', width: 90, align: 'right', defaultVisible: false },
  { key: 'fStatus', label: '数据状态', prop: 'fStatus', width: 90, align: 'center', slotName: 'status' },
  { key: 'fDisabled', label: '禁用', prop: 'fDisabled', width: 70, align: 'center', slotName: 'bool', defaultVisible: false },
  { key: 'cuserName', label: '制单人', prop: 'cuserName', width: 100, defaultVisible: false },
  { key: 'cYmd', label: '制单日期', prop: 'cYmd', width: 110, slotName: 'date', defaultVisible: false },
  { key: 'fcheckerName', label: '审核人', prop: 'fcheckerName', width: 100, defaultVisible: false },
  { key: 'fcheckdate', label: '审核日期', prop: 'fcheckdate', width: 110, slotName: 'date', defaultVisible: false },
]

// 高级筛选仅开放可在服务端过滤的真实列（表头：单据编号/订单日期；明细：数量/累计入库数量）
const filterColumns: ColumnDef[] = [
  { key: 'fbillno', label: '单据编号', prop: 'fbillno' },
  { key: 'fdate', label: '订单日期', prop: 'fdate' },
  { key: 'fqty', label: '数量', prop: 'fqty' },
  { key: 'finstockqty', label: '累计入库数量', prop: 'finstockqty' },
]

const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig('purchaseOrderLabel', columns)

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

const handleSelectionChange = (rows: any[]) => { selectedRows.value = rows }

async function fetchData() {
  loading.value = true
  try {
    const res: any = await getPurchaseOrderLabels(queryParams)
    dataList.value = res.data.items
    total.value = res.data.totalCount
  } catch (e) {
    console.error('加载采购订单标签列表失败:', e)
  } finally {
    loading.value = false
  }
}

const openGenerate = (row: any) => {
  if (!row?.entryUid) { ElMessage.warning('该行缺少明细标识，无法生成条码'); return }
  router.push({ name: 'PurchaseOrderLabelGenerate', query: { entryUid: row.entryUid } })
}
const handlePrint = () => { if (selectedRows.value.length === 1) openGenerate(selectedRows.value[0]) }
const handlePrintRow = (row: any) => openGenerate(row)

onMounted(fetchData)
</script>

<style scoped>
.po-label-list-container {
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
