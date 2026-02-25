<template>
  <div class="warehouse-list-container">
    <div class="list-layout">
      <GroupPanel
        ref="groupPanelRef"
        prg-key="BD_Stock"
        title="仓库分组"
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
      <el-input
        v-model="queryParams.keyword"
        placeholder="搜索仓库编码/名称"
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
      <el-button type="primary" @click="handleAdd" v-permission="'warehouse:add'">
        <el-icon><Plus /></el-icon> 新增
      </el-button>
      <el-button @click="handleEditSelected" :disabled="!canEdit" v-permission="'warehouse:edit'">
        <el-icon><Edit /></el-icon> 编辑
      </el-button>
      <el-button type="success" @click="handleBatchApprove" :disabled="!canApprove" :loading="batchLoading" v-permission="'warehouse:approve'">
        审核{{ canApprove ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="warning" @click="handleBatchUnapprove" :disabled="!canUnapprove" :loading="batchLoading" v-permission="'warehouse:approve'">
        反审核{{ canUnapprove ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="danger" @click="handleBatchDelete" :disabled="!canDelete" :loading="batchLoading" v-permission="'warehouse:delete'">
        删除{{ canDelete ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="info" @click="handleBatchDisable" :disabled="!canDisable" :loading="batchLoading" v-permission="'warehouse:disable'">
        禁用{{ canDisable ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button @click="handleBatchEnable" :disabled="!canEnable" :loading="batchLoading" v-permission="'warehouse:disable'">
        反禁用{{ canEnable ? ` (${selectedCount})` : '' }}
      </el-button>
    </div>

    <el-table
      ref="tableRef"
      v-loading="loading"
      :data="warehouseList"
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
            <template v-if="col.slotName === 'boolTag'">
              <el-tag :type="scope.row[col.key] ? 'success' : 'info'" size="small">
                {{ scope.row[col.key] ? '是' : '否' }}
              </el-tag>
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
      <el-pagination
        v-model:current-page="queryParams.page"
        v-model:page-size="queryParams.pageSize"
        :total="total"
        layout="total, prev, pager, next"
        @current-change="fetchData"
      />
    </div>

    <!-- Create/Edit Dialog -->
    <el-dialog
      v-model="dialogVisible"
      :title="dialogType === 'create' ? '新增仓库' : isReadonly ? '查看仓库' : '编辑仓库'"
      width="700px"
    >
      <el-form :model="form" label-width="110px" :disabled="isReadonly">
        <el-tabs v-model="activeTab">
          <!-- 基本信息 -->
          <el-tab-pane label="基本信息" name="basic">
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="仓库编码" required>
                  <el-input v-model="form.fNumber" :disabled="dialogType === 'edit'" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="仓库名称" required>
                  <el-input v-model="form.fName" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="仓库类型">
                  <el-input v-model="form.fType" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="仓库属性">
                  <el-input v-model="form.fStockProperty" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="负责人">
                  <el-input v-model="form.fPrincipal" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="联系电话">
                  <el-input v-model="form.fTel" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-form-item label="地址">
              <el-input v-model="form.fAddress" />
            </el-form-item>
            <el-form-item label="分组">
              <el-tree-select v-model="form.fGroupId" :data="groupPanelRef?.treeData || []" :props="{ label: 'fName', children: 'children', value: 'uid' }" placeholder="请选择分组" clearable check-strictly style="width: 100%" />
            </el-form-item>
            <el-form-item label="描述">
              <el-input v-model="form.fDescription" type="textarea" :rows="2" />
            </el-form-item>
          </el-tab-pane>

          <!-- 仓库设置 -->
          <el-tab-pane label="仓库设置" name="settings">
            <el-row :gutter="20">
              <el-col :span="8">
                <el-form-item label="允许负库存">
                  <el-switch v-model="form.fAllowMinusQty" />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="启用仓位">
                  <el-switch v-model="form.fIsOpenLocation" />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="是否保税">
                  <el-switch v-model="form.fBonded" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="8">
                <el-form-item label="允许MRP">
                  <el-switch v-model="form.fAllowMrpPlan" />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="允许锁库">
                  <el-switch v-model="form.fAllowLock" />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="是否虚仓">
                  <el-switch v-model="form.fIsVirtual" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="8">
                <el-form-item label="参与预警">
                  <el-switch v-model="form.fAvailableAlert" />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="参与拣货">
                  <el-switch v-model="form.fAvailablePicking" />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="拣货优先级">
                  <el-input-number v-model="form.fSortingPriority" :min="0" style="width: 100%" />
                </el-form-item>
              </el-col>
            </el-row>
          </el-tab-pane>
        </el-tabs>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="dialogVisible = false">{{ isReadonly ? '关闭' : '取消' }}</el-button>
          <el-button v-if="!isReadonly" type="primary" @click="submitForm">确定</el-button>
        </span>
      </template>
    </el-dialog>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted } from 'vue'
import { getWarehouses, getWarehouse, createWarehouse, updateWarehouse, deleteWarehouse, approveWarehouse, unapproveWarehouse, disableWarehouse, enableWarehouse, type Warehouse } from '../../api/warehouse'
import { formatDate } from '../../utils/format'
import { ElMessage } from 'element-plus'
import { Search, Plus, Edit, DArrowRight } from '@element-plus/icons-vue'
import ColumnSetting from '../../components/ColumnSetting.vue'
import GroupPanel from '../../components/GroupPanel.vue'
import { useColumnConfig, type ColumnDef } from '../../composables/useColumnConfig'
import { useTableSelection } from '../../composables/useTableSelection'

const groupPanelRef = ref<InstanceType<typeof GroupPanel>>()
const tableRef = ref()

const columns: ColumnDef[] = [
  // 基本信息
  { key: 'fNumber', label: '仓库编码', prop: 'fNumber', width: 130 },
  { key: 'fName', label: '仓库名称', prop: 'fName', minWidth: 180 },
  { key: 'fType', label: '仓库类型', prop: 'fType', width: 120 },
  { key: 'fStockProperty', label: '仓库属性', prop: 'fStockProperty', width: 120, defaultVisible: false },
  { key: 'fPrincipal', label: '负责人', prop: 'fPrincipal', width: 120 },
  { key: 'fTel', label: '电话', prop: 'fTel', width: 140 },
  { key: 'fAddress', label: '地址', prop: 'fAddress', minWidth: 200 },
  { key: 'fDescription', label: '描述', prop: 'fDescription', minWidth: 200, defaultVisible: false },
  // 仓库设置
  { key: 'fAllowMinusQty', label: '允许负库存', width: 110, align: 'center', slotName: 'boolTag', defaultVisible: false },
  { key: 'fIsOpenLocation', label: '启用仓位', width: 100, align: 'center', slotName: 'boolTag', defaultVisible: false },
  { key: 'fBonded', label: '是否保税', width: 100, align: 'center', slotName: 'boolTag', defaultVisible: false },
  { key: 'fAllowMrpPlan', label: '允许MRP', width: 100, align: 'center', slotName: 'boolTag', defaultVisible: false },
  { key: 'fAllowLock', label: '允许锁库', width: 100, align: 'center', slotName: 'boolTag', defaultVisible: false },
  { key: 'fIsVirtual', label: '是否虚仓', width: 100, align: 'center', slotName: 'boolTag', defaultVisible: false },
  { key: 'fAvailableAlert', label: '参与预警', width: 100, align: 'center', slotName: 'boolTag', defaultVisible: false },
  { key: 'fAvailablePicking', label: '参与拣货', width: 100, align: 'center', slotName: 'boolTag', defaultVisible: false },
  { key: 'fSortingPriority', label: '拣货优先级', prop: 'fSortingPriority', width: 110, defaultVisible: false },
  // 系统字段
  { key: 'fStatus', label: '审核状态', width: 100, align: 'center', slotName: 'status' },
  { key: 'fDisabled', label: '禁用状态', width: 100, align: 'center', slotName: 'disabled' },
  { key: 'cYmd', label: '创建时间', width: 180, slotName: 'createTime' },
]

const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig('warehouse', columns)

const loading = ref(false)
const warehouseList = ref<Warehouse[]>([])
const total = ref(0)
const queryParams = reactive({
  page: 1,
  pageSize: 10,
  keyword: '',
  groupId: ''
})

const {
  selectedCount, canEdit, canApprove, canUnapprove, canDelete, canDisable, canEnable, batchLoading,
  handleSelectionChange, handleBatchApprove, handleBatchUnapprove, handleBatchDelete, handleBatchDisable, handleBatchEnable, clearSelection
} = useTableSelection<Warehouse>({
  entityName: '仓库',
  approveFn: approveWarehouse,
  unapproveFn: unapproveWarehouse,
  deleteFn: deleteWarehouse,
  disableFn: disableWarehouse,
  enableFn: enableWarehouse,
  onSuccess: fetchData,
})

const handleGroupSelect = (groupId: string) => {
  queryParams.groupId = groupId
  queryParams.page = 1
  fetchData()
}

const dialogVisible = ref(false)
const dialogType = ref<'create' | 'edit'>('create')
const activeTab = ref('basic')

const isReadonly = computed(() => dialogType.value === 'edit' && (form.fStatus === 40 || form.fDisabled))

const defaultForm = {
  uid: undefined as string | undefined,
  fStatus: 0,
  fNumber: '',
  fName: '',
  fDescription: '',
  fPrincipal: '',
  fTel: '',
  fType: '',
  fAddress: '',
  fStockProperty: '',
  fAllowMinusQty: false,
  fIsOpenLocation: false,
  fBonded: false,
  fAllowMrpPlan: false,
  fAllowLock: false,
  fAvailableAlert: false,
  fAvailablePicking: false,
  fSortingPriority: 0,
  fIsVirtual: false,
  fGroupId: '',
  fDisabled: false
}

const form = reactive({ ...defaultForm })

async function fetchData() {
  loading.value = true
  try {
    const res: any = await getWarehouses(queryParams)
    warehouseList.value = res.data.items
    total.value = res.data.totalCount
  } catch (error) {
    console.error('Fetch warehouses failed:', error)
  } finally {
    loading.value = false
  }
}

const handleAdd = () => {
  dialogType.value = 'create'
  activeTab.value = 'basic'
  Object.assign(form, { ...defaultForm })
  dialogVisible.value = true
}

const handleEdit = async (row: Warehouse) => {
  dialogType.value = 'edit'
  activeTab.value = 'basic'
  try {
    const res: any = await getWarehouse(row.uid!)
    const d = res.data
    Object.assign(form, {
      uid: d.uid,
      fStatus: d.fStatus || 0,
      fNumber: d.fNumber || '',
      fName: d.fName || '',
      fDescription: d.fDescription || '',
      fPrincipal: d.fPrincipal || '',
      fTel: d.fTel || '',
      fType: d.fType || '',
      fAddress: d.fAddress || '',
      fStockProperty: d.fStockProperty || '',
      fAllowMinusQty: d.fAllowMinusQty || false,
      fIsOpenLocation: d.fIsOpenLocation || false,
      fBonded: d.fBonded || false,
      fAllowMrpPlan: d.fAllowMrpPlan || false,
      fAllowLock: d.fAllowLock || false,
      fAvailableAlert: d.fAvailableAlert || false,
      fAvailablePicking: d.fAvailablePicking || false,
      fSortingPriority: d.fSortingPriority || 0,
      fIsVirtual: d.fIsVirtual || false,
      fGroupId: d.fGroupId || '',
      fDisabled: d.fDisabled || false
    })
  } catch (error) {
    console.error('Fetch warehouse detail failed:', error)
  }
  dialogVisible.value = true
}

const handleEditSelected = () => {
  const rows = tableRef.value?.getSelectionRows() as Warehouse[]
  if (rows?.length === 1) handleEdit(rows[0])
}

const handleRowDblClick = (row: Warehouse) => {
  handleEdit(row)
}

const submitForm = async () => {
  if (!form.fNumber || !form.fName) {
    ElMessage.warning('请输入仓库编码和名称')
    return
  }

  const data = {
    fNumber: form.fNumber,
    fName: form.fName,
    fDescription: form.fDescription,
    fPrincipal: form.fPrincipal,
    fTel: form.fTel,
    fType: form.fType,
    fAddress: form.fAddress,
    fAllowMinusQty: form.fAllowMinusQty,
    fIsOpenLocation: form.fIsOpenLocation,
    fStockProperty: form.fStockProperty,
    fGroupId: form.fGroupId
  }

  try {
    if (dialogType.value === 'create') {
      await createWarehouse(data)
      ElMessage.success('创建成功')
    } else {
      const { fNumber, ...updateData } = data
      await updateWarehouse(form.uid!, updateData)
      ElMessage.success('更新成功')
    }
    dialogVisible.value = false
    fetchData()
  } catch (error: any) {
    console.error('Submit warehouse failed:', error)
    if (error.response && error.response.data) {
      const errorMsg = error.response.data.message || JSON.stringify(error.response.data)
      ElMessage.error(errorMsg)
    } else {
      ElMessage.error('提交失败')
    }
  }
}

onMounted(() => {
  fetchData()
})
</script>

<style scoped>
.warehouse-list-container {
  padding: 20px;
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
}

.list-layout {
  display: flex;
  gap: 16px;
}

.list-panel {
  flex: 1;
  min-width: 0;
}

.toggle-group-btn {
  margin-right: 8px;
  flex-shrink: 0;
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
