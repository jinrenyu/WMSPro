<template>
  <div class="stock-inventory-list-container">
    <div class="list-panel">
      <div class="header-actions">
        <el-input
          v-model="queryParams.keyword"
          placeholder="搜索物料代码/名称/批次"
          class="search-input"
          clearable
          @clear="handleSearch"
          @keyup.enter="handleSearch"
        >
          <template #append>
            <el-button @click="handleSearch"><el-icon><Search /></el-icon></el-button>
          </template>
        </el-input>

        <div class="header-right">
          <el-button @click="fetchData">
            <el-icon><Refresh /></el-icon> 刷新
          </el-button>
          <DynamicFilter
            v-model="queryParams.dynamicFilters"
            :columns="filterColumns"
            @change="handleSearch"
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

      <el-table
        v-loading="loading"
        :data="dataList"
        style="width: 100%"
        border
        size="small"
      >
        <template v-for="col in allColumns" :key="col.key">
          <el-table-column
            v-if="isColumnVisible(col)"
            :prop="col.prop"
            :label="col.label"
            :width="col.width"
            :min-width="col.minWidth"
            :align="col.align"
            :fixed="col.fixed"
            show-overflow-tooltip
          >
            <template v-if="col.slotName" #default="scope">
              <template v-if="col.slotName === 'date'">
                {{ fmtDate(scope.row[col.prop!]) }}
              </template>
              <template v-else-if="col.slotName === 'bool'">
                <el-tag :type="scope.row[col.prop!] ? 'success' : 'info'" size="small">
                  {{ scope.row[col.prop!] ? '是' : '否' }}
                </el-tag>
              </template>
              <template v-else-if="col.slotName === 'kfunit'">
                {{ fmtKfUnit(scope.row[col.prop!]) }}
              </template>
              <template v-else-if="col.slotName === 'num'">
                {{ fmtNum(scope.row[col.prop!]) }}
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
          :page-sizes="[20, 50, 100]"
          layout="total, sizes, prev, pager, next"
          @current-change="fetchData"
          @size-change="handleSizeChange"
        />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { Search, Refresh } from '@element-plus/icons-vue'
import { getInventories } from '../../api/inventory'
import { formatDate } from '../../utils/format'
import ColumnSetting from '../../components/ColumnSetting.vue'
import DynamicFilter, { type DynamicFilterInfo } from '../../components/DynamicFilter.vue'
import { useColumnConfig, type ColumnDef } from '../../composables/useColumnConfig'

// 即时库存：一行一条库存维度，只读列表（字段顺序参考 designpictures/即时库存）
const columns: ColumnDef[] = [
  { key: 'fmaterialNumber', label: '物料代码', prop: 'fmaterialNumber', width: 140 },
  { key: 'fmaterialName', label: '物料名称', prop: 'fmaterialName', minWidth: 160 },
  { key: 'fSpecification', label: '规格型号', prop: 'fSpecification', minWidth: 120 },
  { key: 'flot', label: '批次', prop: 'flot', width: 120 },
  { key: 'fauxpropName', label: '辅助属性', prop: 'fauxpropName', width: 110 },
  { key: 'fqty', label: '数量', prop: 'fqty', width: 110, align: 'right', slotName: 'num' },
  { key: 'fstockunitName', label: '单位', prop: 'fstockunitName', width: 90 },
  { key: 'fbaseunitqty', label: '基本单位数量', prop: 'fbaseunitqty', width: 120, align: 'right', slotName: 'num' },
  { key: 'fbaseunitName', label: '基本单位', prop: 'fbaseunitName', width: 90 },
  { key: 'fsecunitqty', label: '辅助单位数量', prop: 'fsecunitqty', width: 120, align: 'right', slotName: 'num' },
  { key: 'fsecunitName', label: '辅助单位', prop: 'fsecunitName', width: 90 },
  { key: 'fstockNumber', label: '仓库代码', prop: 'fstockNumber', width: 120 },
  { key: 'fstockName', label: '仓库', prop: 'fstockName', minWidth: 130 },
  { key: 'fstocklocNumber', label: '仓位代码', prop: 'fstocklocNumber', width: 120 },
  { key: 'fstocklocName', label: '仓位', prop: 'fstocklocName', minWidth: 120 },
  { key: 'fisKfPeriod', label: '是否启用保质期', prop: 'fisKfPeriod', width: 130, align: 'center', slotName: 'bool' },
  { key: 'fkfdate', label: '生产日期/采购日期', prop: 'fkfdate', width: 150, slotName: 'date' },
  { key: 'fKfPeriod', label: '保质期限', prop: 'fKfPeriod', width: 100, align: 'right' },
  { key: 'fKfUnit', label: '保质期单位', prop: 'fKfUnit', width: 100, align: 'center', slotName: 'kfunit' },
  { key: 'fusefuldate', label: '有效期至', prop: 'fusefuldate', width: 130, slotName: 'date' },
  { key: 'fstockstatusName', label: '库存状态', prop: 'fstockstatusName', width: 100 },
  { key: 'fstockorgName', label: '库存组织', prop: 'fstockorgName', width: 140 },
  { key: 'fcompanyName', label: '组织', prop: 'fcompanyName', width: 140 },
]

// 高级筛选可选字段：名称类字段（物料/仓库/仓位/库存状态/组织/辅助属性）由后端先解析目标资料 Uid 再约束库存外键列，
// 批次/数量为库存表真实列；多个条件 AND 组合，可组合查"某仓库+某仓位+某物料+某批次"的库存。
const filterColumns: ColumnDef[] = [
  { key: 'fmaterialNumber', label: '物料代码', prop: 'fmaterialNumber' },
  { key: 'fmaterialName', label: '物料名称', prop: 'fmaterialName' },
  { key: 'fSpecification', label: '规格型号', prop: 'fSpecification' },
  { key: 'flot', label: '批次', prop: 'flot' },
  { key: 'fstockNumber', label: '仓库代码', prop: 'fstockNumber' },
  { key: 'fstockName', label: '仓库', prop: 'fstockName' },
  { key: 'fstocklocNumber', label: '仓位代码', prop: 'fstocklocNumber' },
  { key: 'fstocklocName', label: '仓位', prop: 'fstocklocName' },
  { key: 'fauxpropName', label: '辅助属性', prop: 'fauxpropName' },
  { key: 'fstockstatusName', label: '库存状态', prop: 'fstockstatusName' },
  { key: 'fstockorgName', label: '库存组织', prop: 'fstockorgName' },
  { key: 'fcompanyName', label: '组织', prop: 'fcompanyName' },
  { key: 'fqty', label: '数量', prop: 'fqty' },
]

const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig('stockInventory', columns)

const loading = ref(false)
const dataList = ref<any[]>([])
const total = ref(0)

const queryParams = reactive({
  page: 1,
  pageSize: 20,
  keyword: '',
  dynamicFilters: [] as DynamicFilterInfo[],
})

const fmtDate = (d?: string) => {
  if (!d) return ''
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() <= 1900) return ''
  return formatDate(d).slice(0, 10)
}

const fmtKfUnit = (u?: number) => (u === 1 ? '月' : u === 2 ? '年' : u === 0 ? '日' : '')

const fmtNum = (n?: number | null) => {
  if (n === null || n === undefined) return ''
  const v = Number(n)
  if (isNaN(v)) return ''
  // 去掉无意义的尾随 0（最多 6 位小数）
  return String(parseFloat(v.toFixed(6)))
}

async function fetchData() {
  loading.value = true
  try {
    const res: any = await getInventories(queryParams)
    dataList.value = res.data.items
    total.value = res.data.totalCount
  } catch (e) {
    console.error('加载即时库存失败:', e)
  } finally {
    loading.value = false
  }
}

// 搜索/清空/筛选变更：先回到第 1 页再查，避免处于非首页时过滤后越界出现空白
const handleSearch = () => {
  queryParams.page = 1
  fetchData()
}

const handleSizeChange = () => {
  queryParams.page = 1
  fetchData()
}

onMounted(() => {
  fetchData()
})
</script>

<style scoped>
.stock-inventory-list-container {
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

.pagination-container {
  margin-top: 20px;
  display: flex;
  justify-content: flex-end;
}
</style>
