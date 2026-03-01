<template>
  <div class="stockplace-list-container">
    <div class="list-layout">
      <GroupPanel
        ref="groupPanelRef"
        prg-key="BD_StockPlace"
        title="仓位分组"
        @select="handleGroupSelect"
      />
      <div class="list-panel">
    <div class="header-actions">
      <el-button
        v-show="!groupPanelRef?.visible"
        class="toggle-group-btn"
        @click="groupPanelRef!.visible = true"
      >
        <el-icon><DArrowRight /></el-icon>
        <span>分组</span>
      </el-button>
      <el-input v-model="queryParams.keyword" placeholder="搜索仓位代码/名称" class="search-input" clearable @clear="fetchData" @keyup.enter="fetchData">
        <template #append><el-button @click="fetchData"><el-icon><Search /></el-icon></el-button></template>
      </el-input>
      
      
      
      <div class="header-right">
        <DynamicFilter
        v-model="queryParams.dynamicFilters"
        :columns="allColumns"
        :api-fields-func="getStockPlacesFields"
        @change="fetchData" style="margin-right: 8px;"
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
      <el-button type="primary" @click="handleAdd" v-permission="'stockplace:add'"><el-icon><Plus /></el-icon> 新增</el-button>
      <el-button @click="handleEditSelected" :disabled="!canEdit" v-permission="'stockplace:edit'"><el-icon><Edit /></el-icon> 编辑</el-button>
      <el-button type="success" @click="handleBatchApprove" :disabled="!canApprove" :loading="batchLoading" v-permission="'stockplace:approve'">
        审核{{ canApprove ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="warning" @click="handleBatchUnapprove" :disabled="!canUnapprove" :loading="batchLoading" v-permission="'stockplace:approve'">
        反审核{{ canUnapprove ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="danger" @click="handleBatchDelete" :disabled="!canDelete" :loading="batchLoading" v-permission="'stockplace:delete'">
        删除{{ canDelete ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="info" @click="handleBatchDisable" :disabled="!canDisable" :loading="batchLoading" v-permission="'stockplace:disable'">
        禁用{{ canDisable ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button @click="handleBatchEnable" :disabled="!canEnable" :loading="batchLoading" v-permission="'stockplace:disable'">
        反禁用{{ canEnable ? ` (${selectedCount})` : '' }}
      </el-button>
    </div>

    <el-table ref="tableRef" v-loading="loading" :data="list" style="width: 100%" border @selection-change="handleSelectionChange" @row-dblclick="handleRowDblClick">
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
            <template v-if="col.slotName === 'allowMix'">
              <el-tag :type="scope.row.fAllowMix ? 'success' : 'info'" size="small">{{ scope.row.fAllowMix ? '是' : '否' }}</el-tag>
            </template>
            <template v-else-if="col.slotName === 'createTime'">
              {{ formatDate(scope.row.cYmd) }}
            </template>
            <template v-else-if="col.slotName === 'status'">
              <el-tag :type="scope.row.fStatus === 40 ? 'success' : 'warning'" size="small">
                {{ scope.row.fStatus === 40 ? '已审核' : '未审核' }}
              </el-tag>
            </template>
            <template v-else-if="col.slotName === 'disabled'">
              <el-tag v-if="scope.row.fDisabled" type="danger" size="small">已禁用</el-tag>
              <el-tag v-else type="success" size="small">正常</el-tag>
            </template>
          </template>
        </el-table-column>
      </template>
    </el-table>
    <div class="pagination-container">
      <el-pagination v-model:current-page="queryParams.page" v-model:page-size="queryParams.pageSize" :total="total" layout="total, prev, pager, next" @current-change="fetchData" />
    </div>
    <el-dialog v-model="dialogVisible" :title="dialogType === 'create' ? '新增仓位' : isReadonly ? '查看仓位' : '编辑仓位'" width="550px">
      <el-form :model="form" label-width="100px" :disabled="isReadonly">
        <el-row :gutter="20">
          <el-col :span="12"><el-form-item label="仓位代码" required><el-input v-model="form.fNumber" :disabled="dialogType === 'edit'" /></el-form-item></el-col>
          <el-col :span="12"><el-form-item label="仓位名称" required><el-input v-model="form.fName" /></el-form-item></el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="12"><el-form-item label="所属仓库" required><LookupSelect v-model="form.fStockId" module="warehouse" placeholder="请选择仓库" preload /></el-form-item></el-col>
          <el-col :span="12"><el-form-item label="仓位属性"><el-input v-model="form.fSpProperty" /></el-form-item></el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="12"><el-form-item label="最大容量"><el-input-number v-model="form.fMaxCapacity" :min="0" :precision="4" style="width:100%" /></el-form-item></el-col>
          <el-col :span="12"><el-form-item label="允许混放"><el-switch v-model="form.fAllowMix" /></el-form-item></el-col>
        </el-row>
        <el-form-item label="分组">
          <el-tree-select v-model="form.fGroupId" :data="groupPanelRef?.treeData || []" :props="{ label: 'fName', children: 'children', value: 'uid' }" placeholder="请选择分组" clearable check-strictly style="width: 100%" />
        </el-form-item>
        <el-form-item label="描述"><el-input v-model="form.fDescription" type="textarea" :rows="2" /></el-form-item>
      </el-form>
      <template #footer><el-button @click="dialogVisible = false">{{ isReadonly ? '关闭' : '取消' }}</el-button><el-button v-if="!isReadonly" type="primary" @click="submitForm">确定</el-button></template>
    </el-dialog>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { ref, reactive, computed, onMounted } from 'vue'
import { getStockPlaces, getStockPlace, createStockPlace, updateStockPlace, deleteStockPlace, approveStockPlace, unapproveStockPlace, disableStockPlace, enableStockPlace, getStockPlacesFields, type StockPlace } from '../../api/stockplace'
import { formatDate } from '../../utils/format'
import { ElMessage } from 'element-plus'
import { Search, Plus, Edit, DArrowRight } from '@element-plus/icons-vue'
import LookupSelect from '../../components/LookupSelect.vue'
import ColumnSetting from '../../components/ColumnSetting.vue'
import GroupPanel from '../../components/GroupPanel.vue'
import DynamicFilter, { type DynamicFilterInfo } from '../../components/DynamicFilter.vue'
import { useColumnConfig, type ColumnDef } from '../../composables/useColumnConfig'
import { useTableSelection } from '../../composables/useTableSelection'

const groupPanelRef = ref<InstanceType<typeof GroupPanel>>()
const tableRef = ref()

const columns: ColumnDef[] = [
  { key: 'fNumber', label: '仓位代码', prop: 'fNumber', width: 130 },
  { key: 'fName', label: '仓位名称', prop: 'fName', minWidth: 180 },
  { key: 'fStockName', label: '所属仓库', prop: 'fStockName', width: 150 },
  { key: 'fSpProperty', label: '仓位属性', prop: 'fSpProperty', width: 120 },
  { key: 'fAllowMix', label: '允许混放', width: 100, align: 'center', slotName: 'allowMix' },
  { key: 'fMaxCapacity', label: '最大容量', prop: 'fMaxCapacity', width: 110, defaultVisible: false },
  { key: 'fDescription', label: '描述', prop: 'fDescription', minWidth: 200, defaultVisible: false },
  { key: 'fStatus', label: '审核状态', width: 100, align: 'center', slotName: 'status' },
  { key: 'fDisabled', label: '禁用状态', width: 100, align: 'center', slotName: 'disabled' },
  { key: 'cYmd', label: '创建时间', width: 180, slotName: 'createTime' },
]

const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig('stockplace', columns)

const loading = ref(false)
const list = ref<StockPlace[]>([])
const total = ref(0)
const queryParams = reactive({ page: 1, pageSize: 10, keyword: '', groupId: '', dynamicFilters: [] as DynamicFilterInfo[] })

const {
  selectedCount, canEdit, canApprove, canUnapprove, canDelete, canDisable, canEnable, batchLoading,
  handleSelectionChange, handleBatchApprove, handleBatchUnapprove, handleBatchDelete, handleBatchDisable, handleBatchEnable, clearSelection
} = useTableSelection<StockPlace>({
  entityName: '仓位',
  approveFn: approveStockPlace,
  unapproveFn: unapproveStockPlace,
  deleteFn: deleteStockPlace,
  disableFn: disableStockPlace,
  enableFn: enableStockPlace,
  onSuccess: fetchData,
})

const handleGroupSelect = (groupId: string) => {
  queryParams.groupId = groupId
  queryParams.page = 1
  fetchData()
}
const dialogVisible = ref(false)
const dialogType = ref<'create' | 'edit'>('create')
const defaultForm = { uid: undefined as string | undefined, fNumber: '', fName: '', fDescription: '', fStockId: '', fSpProperty: '', fAllowMix: false, fMaxCapacity: 0, fGroupId: '', fStatus: 0, fDisabled: false }
const form = reactive({ ...defaultForm })
const isReadonly = computed(() => dialogType.value === 'edit' && (form.fStatus === 40 || form.fDisabled))

async function fetchData() { loading.value = true; try { const res: any = await getStockPlaces(queryParams); list.value = res.data.items; total.value = res.data.totalCount } catch (e) { console.error(e) } finally { loading.value = false } }
const handleAdd = () => { dialogType.value = 'create'; Object.assign(form, { ...defaultForm }); dialogVisible.value = true }
const handleEdit = async (row: StockPlace) => { dialogType.value = 'edit'; try { const res: any = await getStockPlace(row.uid!); const d = res.data; Object.assign(form, { uid: d.uid, fNumber: d.fNumber||'', fName: d.fName||'', fDescription: d.fDescription||'', fStockId: d.fStockId||'', fSpProperty: d.fSpProperty||'', fAllowMix: d.fAllowMix||false, fMaxCapacity: d.fMaxCapacity||0, fGroupId: d.fGroupId||'', fStatus: d.fStatus||0, fDisabled: d.fDisabled||false }) } catch (e) { console.error(e) }; dialogVisible.value = true }
const handleEditSelected = () => { const rows = tableRef.value?.getSelectionRows() as StockPlace[]; if (rows?.length === 1) handleEdit(rows[0]) }
const handleRowDblClick = (row: StockPlace) => { handleEdit(row) }
const submitForm = async () => { if (!form.fNumber || !form.fName || !form.fStockId) { ElMessage.warning('请输入仓位代码、名称和所属仓库'); return }; try { if (dialogType.value === 'create') { await createStockPlace({ fNumber: form.fNumber, fName: form.fName, fDescription: form.fDescription, fStockId: form.fStockId, fSpProperty: form.fSpProperty, fAllowMix: form.fAllowMix, fMaxCapacity: form.fMaxCapacity, fGroupId: form.fGroupId }); ElMessage.success('创建成功') } else { await updateStockPlace(form.uid!, { fName: form.fName, fDescription: form.fDescription, fStockId: form.fStockId, fSpProperty: form.fSpProperty, fAllowMix: form.fAllowMix, fMaxCapacity: form.fMaxCapacity, fGroupId: form.fGroupId }); ElMessage.success('更新成功') }; dialogVisible.value = false; fetchData() } catch (error: any) { const msg = error.response?.data?.message || '提交失败'; ElMessage.error(msg) } }
onMounted(() => { fetchData() })
</script>
<style scoped>
.stockplace-list-container { padding: 20px; background-color: var(--bg-card); border-radius: 8px; box-shadow: var(--shadow-card); }
.list-layout { display: flex; gap: 16px; }
.list-panel { flex: 1; min-width: 0; }
.toggle-group-btn { margin-right: 8px; flex-shrink: 0; }
.header-actions { display: flex; justify-content: space-between; margin-bottom: 12px; }
.header-right { display: flex; align-items: center; gap: 8px; }
.search-input { width: 300px; }
.toolbar-actions { display: flex; align-items: center; gap: 8px; margin-bottom: 12px; }
.pagination-container { margin-top: 20px; display: flex; justify-content: flex-end; }
</style>
