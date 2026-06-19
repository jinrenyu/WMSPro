<template>
  <div class="template-list-container">
    <div class="header-actions">
      <el-input
        v-model="queryParams.keyword"
        placeholder="搜索模板编码/名称"
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
          :columns="allColumns"
          :api-fields-func="getPrintTemplateFields"
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
      <el-button type="primary" @click="handleAdd" v-permission="'labeltemplate:add'">
        <el-icon><Plus /></el-icon> 新增
      </el-button>
      <el-button @click="handleEditSelected" :disabled="selectedRows.length !== 1" v-permission="'labeltemplate:edit'">
        <el-icon><Edit /></el-icon> 编辑
      </el-button>
      <el-button type="danger" @click="handleBatchDelete" :disabled="selectedRows.length === 0" :loading="deleteLoading" v-permission="'labeltemplate:delete'">
        删除{{ selectedRows.length ? ` (${selectedRows.length})` : '' }}
      </el-button>
      <el-button @click="fetchData">
        <el-icon><Refresh /></el-icon> 刷新
      </el-button>
    </div>

    <el-table
      ref="tableRef"
      v-loading="loading"
      :data="templateList"
      style="width: 100%"
      border
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
            <template v-if="col.slotName === 'createTime'">
              {{ formatDate(scope.row.cYmd) }}
            </template>
            <template v-else-if="col.slotName === 'paper'">
              {{ scope.row.fPaperWidth || 0 }} × {{ scope.row.fPaperHeight || 0 }} mm
            </template>
            <template v-else-if="col.slotName === 'isDefault'">
              <el-tag v-if="scope.row.fIsDefault" type="success" size="small">默认</el-tag>
              <span v-else>—</span>
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
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import { getPrintTemplates, deletePrintTemplate, getPrintTemplateFields, type PrintTemplate } from '../../api/labelTemplate'
import { formatDate } from '../../utils/format'
import { Search, Plus, Edit, Refresh } from '@element-plus/icons-vue'
import ColumnSetting from '../../components/ColumnSetting.vue'
import DynamicFilter, { type DynamicFilterInfo } from '../../components/DynamicFilter.vue'
import { useColumnConfig, type ColumnDef } from '../../composables/useColumnConfig'

const router = useRouter()
const tableRef = ref()

const columns: ColumnDef[] = [
  { key: 'fNumber', label: '模板编码', prop: 'fNumber', width: 140 },
  { key: 'fName', label: '模板名称', prop: 'fName', minWidth: 180 },
  { key: 'fBillSourceName', label: '适用单据', prop: 'fBillSourceName', width: 160 },
  { key: 'paper', label: '纸张尺寸', prop: 'fPaperWidth', width: 140, align: 'center', slotName: 'paper' },
  { key: 'fIsDefault', label: '默认', prop: 'fIsDefault', width: 90, align: 'center', slotName: 'isDefault' },
  { key: 'fDescription', label: '备注', prop: 'fDescription', minWidth: 160, defaultVisible: false },
  { key: 'cYmd', label: '创建时间', prop: 'cYmd', width: 180, slotName: 'createTime' },
]

const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig('labelTemplate', columns)

const loading = ref(false)
const deleteLoading = ref(false)
const templateList = ref<PrintTemplate[]>([])
const total = ref(0)
const selectedRows = ref<PrintTemplate[]>([])

const queryParams = reactive({
  page: 1,
  pageSize: 10,
  keyword: '',
  dynamicFilters: [] as DynamicFilterInfo[],
  sortField: undefined as string | undefined,
  isAsc: undefined as boolean | undefined
})

const handleSelectionChange = (rows: PrintTemplate[]) => { selectedRows.value = rows }

async function fetchData() {
  loading.value = true
  try {
    const res: any = await getPrintTemplates(queryParams)
    templateList.value = res.data.items
    total.value = res.data.totalCount
  } catch (error) {
    console.error('加载标签模板失败:', error)
  } finally {
    loading.value = false
  }
}

const handleAdd = () => {
  router.push({ name: 'LabelTemplateEdit', query: {} })
}

const handleEdit = (row: PrintTemplate) => {
  router.push({ name: 'LabelTemplateEdit', query: { uid: row.uid } })
}

const handleEditSelected = () => {
  if (selectedRows.value.length === 1) handleEdit(selectedRows.value[0])
}

const handleRowDblClick = (row: PrintTemplate) => handleEdit(row)

async function handleBatchDelete() {
  if (selectedRows.value.length === 0) return
  try {
    await ElMessageBox.confirm(`确认删除选中的 ${selectedRows.value.length} 个模板？`, '提示', { type: 'warning' })
  } catch { return }
  deleteLoading.value = true
  try {
    for (const row of selectedRows.value) {
      if (row.uid) await deletePrintTemplate(row.uid)
    }
    ElMessage.success('删除成功')
    await fetchData()
  } catch (e: any) {
    ElMessage.error(e?.response?.data?.message || '删除失败')
  } finally {
    deleteLoading.value = false
  }
}

onMounted(() => {
  fetchData()
})
</script>

<style scoped>
.template-list-container {
  padding: 20px;
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
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
  width: 300px;
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
