<template>
  <div class="material-bar-type-list-container">
    <div class="list-layout">
      <GroupPanel
        ref="groupPanelRef"
        prg-key="BD_MaterialBarType"
        title="物料条码类型分组"
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
        placeholder="搜索物料编码/名称"
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
      <el-button type="primary" @click="handleAdd" v-permission="'materialbartype:add'">
        <el-icon><Plus /></el-icon> 新增
      </el-button>
      <el-button @click="handleEditSelected" :disabled="!canEdit" v-permission="'materialbartype:edit'">
        <el-icon><Edit /></el-icon> 编辑
      </el-button>
      <el-button type="success" @click="handleBatchApprove" :disabled="!canApprove" :loading="batchLoading" v-permission="'materialbartype:approve'">
        审核{{ canApprove ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="warning" @click="handleBatchUnapprove" :disabled="!canUnapprove" :loading="batchLoading" v-permission="'materialbartype:approve'">
        反审核{{ canUnapprove ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="danger" @click="handleBatchDelete" :disabled="!canDelete" :loading="batchLoading" v-permission="'materialbartype:delete'">
        删除{{ canDelete ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="info" @click="handleBatchDisable" :disabled="!canDisable" :loading="batchLoading" v-permission="'materialbartype:disable'">
        禁用{{ canDisable ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button @click="handleBatchEnable" :disabled="!canEnable" :loading="batchLoading" v-permission="'materialbartype:disable'">
        反禁用{{ canEnable ? ` (${selectedCount})` : '' }}
      </el-button>
    </div>

    <el-table
      ref="tableRef"
      v-loading="loading"
      :data="dataList"
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
            <template v-if="col.slotName === 'fbartype'">
              <el-tag v-if="scope.row.fbartype === 1">单品条码</el-tag>
              <el-tag v-else-if="scope.row.fbartype === 2" type="success">最小包装量条码</el-tag>
              <el-tag v-else-if="scope.row.fbartype === 3" type="warning">批次条码</el-tag>
              <span v-else>{{ scope.row.fbartype }}</span>
            </template>
            <template v-else-if="col.slotName === 'fcheckdate'">
              {{ formatDate(scope.row.fcheckdate) }}
            </template>
            <template v-else-if="col.slotName === 'fdisabledate'">
              {{ formatDate(scope.row.fdisabledate) }}
            </template>
            <template v-else-if="col.slotName === 'fstatus'">
              <el-tag :type="scope.row.fStatus === 40 ? 'success' : 'warning'" size="small">
                {{ scope.row.fStatus === 40 ? '已审核' : '未审核' }}
              </el-tag>
            </template>
            <template v-else-if="col.slotName === 'disabled'">
              <el-tag v-if="scope.row.fDisabled" type="danger" size="small">已禁用</el-tag>
              <el-tag v-else type="success" size="small">正常</el-tag>
            </template>
            <template v-else-if="col.slotName === 'createTime'">
              {{ formatDate(scope.row.cYmd) }}
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
      :title="dialogType === 'create' ? '新增物料条码类型' : isReadonly ? '查看物料条码类型' : '编辑物料条码类型'"
      width="550px"
    >
      <el-form :model="form" label-width="100px" :disabled="isReadonly">
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="物料" required>
              <LookupSelect v-model="form.fmaterialid" module="material" placeholder="请选择物料" :disabled="dialogType === 'edit'" preload @change="handleMaterialChange" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="条码类型" required>
              <el-select v-model="form.fbartype" style="width: 100%">
                <el-option :value="1" label="单品条码" />
                <el-option :value="2" label="最小包装量条码" />
                <el-option :value="3" label="批次条码" />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="审核日期">
              <el-date-picker v-model="form.fcheckdate" type="date" style="width: 100%" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="审核人">
              <el-input v-model="form.fcheckerid" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="禁用日期">
              <el-date-picker v-model="form.fdisabledate" type="date" style="width: 100%" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="禁用人">
              <el-input v-model="form.fdisableid" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="分组">
          <el-tree-select v-model="form.fGroupId" :data="groupPanelRef?.treeData || []" :props="{ label: 'fName', children: 'children', value: 'uid' }" placeholder="请选择分组" clearable check-strictly style="width: 100%" />
        </el-form-item>
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
import { getMaterialBarTypes, getMaterialBarType, createMaterialBarType, updateMaterialBarType, deleteMaterialBarType, approveMaterialBarType, unapproveMaterialBarType, disableMaterialBarType, enableMaterialBarType, type MaterialBarType } from '../../api/materialBarType'
import { formatDate } from '../../utils/format'
import { ElMessage } from 'element-plus'
import { Search, Plus, Edit, DArrowRight } from '@element-plus/icons-vue'
import LookupSelect from '../../components/LookupSelect.vue'
import type { LookupItem } from '../../api/lookup'
import ColumnSetting from '../../components/ColumnSetting.vue'
import GroupPanel from '../../components/GroupPanel.vue'
import { useColumnConfig, type ColumnDef } from '../../composables/useColumnConfig'
import { useTableSelection } from '../../composables/useTableSelection'

const groupPanelRef = ref<InstanceType<typeof GroupPanel>>()
const tableRef = ref()

const columns: ColumnDef[] = [
  { key: 'fmaterialnumber', label: '物料编码', prop: 'fmaterialnumber', width: 150 },
  { key: 'fmaterialname', label: '物料名称', prop: 'fmaterialname', minWidth: 150 },
  { key: 'fbartype', label: '条码类型', width: 140, align: 'center', slotName: 'fbartype' },
  { key: 'fcheckdate', label: '审核日期', width: 160, slotName: 'fcheckdate' },
  { key: 'fcheckerid', label: '审核人', prop: 'fcheckerid', width: 120 },
  { key: 'fdisabledate', label: '禁用日期', width: 160, slotName: 'fdisabledate', defaultVisible: false },
  { key: 'fdisableid', label: '禁用人', prop: 'fdisableid', width: 120, defaultVisible: false },
  { key: 'fStatus', label: '审核状态', width: 100, align: 'center', slotName: 'fstatus' },
  { key: 'fDisabled', label: '禁用状态', width: 100, align: 'center', slotName: 'disabled' },
  { key: 'cYmd', label: '创建时间', width: 180, slotName: 'createTime' },
]

const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig('materialBarType', columns)

const loading = ref(false)
const dataList = ref<MaterialBarType[]>([])
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
} = useTableSelection<MaterialBarType>({
  entityName: '物料条码类型',
  approveFn: approveMaterialBarType,
  unapproveFn: unapproveMaterialBarType,
  deleteFn: deleteMaterialBarType,
  disableFn: disableMaterialBarType,
  enableFn: enableMaterialBarType,
  onSuccess: fetchData,
})

const handleGroupSelect = (groupId: string) => {
  queryParams.groupId = groupId
  queryParams.page = 1
  fetchData()
}

const handleMaterialChange = (item: LookupItem | null) => {
  if (item) {
    form.fmaterialnumber = item.fNumber
    form.fmaterialname = item.fName
  } else {
    form.fmaterialnumber = ''
    form.fmaterialname = ''
  }
}

const dialogVisible = ref(false)
const dialogType = ref<'create' | 'edit'>('create')

const defaultForm = {
  uid: undefined as string | undefined,
  fmaterialid: '',
  fbartype: 1,
  fmaterialnumber: '',
  fmaterialname: '',
  fcheckdate: undefined as string | undefined,
  fcheckerid: '',
  fdisabledate: undefined as string | undefined,
  fdisableid: '',
  fGroupId: '',
  fStatus: 0,
  fDisabled: false
}

const form = reactive({ ...defaultForm })
const isReadonly = computed(() => dialogType.value === 'edit' && (form.fStatus === 40 || form.fDisabled))

async function fetchData() {
  loading.value = true
  try {
    const res: any = await getMaterialBarTypes(queryParams)
    dataList.value = res.data.items
    total.value = res.data.totalCount
  } catch (error) {
    console.error('Fetch material bar types failed:', error)
  } finally {
    loading.value = false
  }
}

const handleAdd = () => {
  dialogType.value = 'create'
  Object.assign(form, { ...defaultForm })
  dialogVisible.value = true
}

const handleEdit = async (row: MaterialBarType) => {
  dialogType.value = 'edit'
  try {
    const res: any = await getMaterialBarType(row.uid!)
    const d = res.data
    Object.assign(form, {
      uid: d.uid,
      fmaterialid: d.fmaterialid || '',
      fbartype: d.fbartype ?? 1,
      fmaterialnumber: d.fmaterialnumber || '',
      fmaterialname: d.fmaterialname || '',
      fcheckdate: d.fcheckdate,
      fcheckerid: d.fcheckerid || '',
      fdisabledate: d.fdisabledate,
      fdisableid: d.fdisableid || '',
      fGroupId: d.fGroupId || '',
      fStatus: d.fStatus || 0,
      fDisabled: d.fDisabled || false
    })
  } catch (error) {
    console.error('Fetch material bar type detail failed:', error)
  }
  dialogVisible.value = true
}

const handleEditSelected = () => {
  const rows = tableRef.value?.getSelectionRows() as MaterialBarType[]
  if (rows?.length === 1) handleEdit(rows[0])
}

const handleRowDblClick = (row: MaterialBarType) => {
  handleEdit(row)
}

const submitForm = async () => {
  if (!form.fmaterialid) {
    ElMessage.warning('请选择物料')
    return
  }

  try {
    if (dialogType.value === 'create') {
      await createMaterialBarType({
        fmaterialid: form.fmaterialid,
        fbartype: form.fbartype,
        fmaterialnumber: form.fmaterialnumber,
        fmaterialname: form.fmaterialname,
        fcheckdate: form.fcheckdate,
        fcheckerid: form.fcheckerid,
        fdisabledate: form.fdisabledate,
        fdisableid: form.fdisableid,
        fGroupId: form.fGroupId
      })
      ElMessage.success('创建成功')
    } else {
      await updateMaterialBarType(form.uid!, {
        fmaterialid: form.fmaterialid,
        fbartype: form.fbartype,
        fmaterialnumber: form.fmaterialnumber,
        fmaterialname: form.fmaterialname,
        fcheckdate: form.fcheckdate,
        fcheckerid: form.fcheckerid,
        fdisabledate: form.fdisabledate,
        fdisableid: form.fdisableid,
        fGroupId: form.fGroupId
      })
      ElMessage.success('更新成功')
    }
    dialogVisible.value = false
    fetchData()
  } catch (error: any) {
    console.error('Submit material bar type failed:', error)
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
.material-bar-type-list-container {
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
