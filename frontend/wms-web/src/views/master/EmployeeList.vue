<template>
  <div class="employee-list-container">
    <div class="list-layout">
      <GroupPanel
        ref="groupPanelRef"
        prg-key="BD_Employee"
        title="职员分组"
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
      <el-input v-model="queryParams.keyword" placeholder="搜索员工编码/姓名/电话" class="search-input" clearable @clear="fetchData" @keyup.enter="fetchData">
        <template #append><el-button @click="fetchData"><el-icon><Search /></el-icon></el-button></template>
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
      <el-button type="primary" @click="handleAdd" v-permission="'employee:add'"><el-icon><Plus /></el-icon> 新增</el-button>
      <el-button @click="handleEditSelected" :disabled="!canEdit" v-permission="'employee:edit'"><el-icon><Edit /></el-icon> 编辑</el-button>
      <el-button type="success" @click="handleBatchApprove" :disabled="!canApprove" :loading="batchLoading" v-permission="'employee:approve'">
        审核{{ canApprove ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="warning" @click="handleBatchUnapprove" :disabled="!canUnapprove" :loading="batchLoading" v-permission="'employee:approve'">
        反审核{{ canUnapprove ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="danger" @click="handleBatchDelete" :disabled="!canDelete" :loading="batchLoading" v-permission="'employee:delete'">
        删除{{ canDelete ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="info" @click="handleBatchDisable" :disabled="!canDisable" :loading="batchLoading" v-permission="'employee:disable'">
        禁用{{ canDisable ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button @click="handleBatchEnable" :disabled="!canEnable" :loading="batchLoading" v-permission="'employee:disable'">
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
            <template v-if="col.slotName === 'sex'">
              {{ scope.row.fSex === 1 ? '男' : scope.row.fSex === 2 ? '女' : '未知' }}
            </template>
            <template v-else-if="col.slotName === 'departure'">
              <el-tag :type="scope.row.fIsDeparture ? 'danger' : 'success'" size="small">{{ scope.row.fIsDeparture ? '已离职' : '在职' }}</el-tag>
            </template>
            <template v-else-if="col.slotName === 'dateField'">
              {{ scope.row[col.key] ? formatDate(scope.row[col.key]) : '' }}
            </template>
            <template v-else-if="col.slotName === 'createTime'">
              {{ formatDate(scope.row.cYmd) }}
            </template>
            <template v-else-if="col.slotName === 'status'">
              <el-tag :type="scope.row.fStatus === 40 ? 'success' : 'warning'" size="small">{{ scope.row.fStatus === 40 ? '已审核' : '未审核' }}</el-tag>
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
    <el-dialog v-model="dialogVisible" :title="dialogType === 'create' ? '新增职员' : isReadonly ? '查看职员' : '编辑职员'" width="700px">
      <el-form :model="form" label-width="110px" :disabled="isReadonly">
        <el-tabs v-model="activeTab">
          <el-tab-pane label="基本信息" name="basic">
            <el-row :gutter="20">
              <el-col :span="12"><el-form-item label="员工编码" required><el-input v-model="form.fNumber" :disabled="dialogType === 'edit'" /></el-form-item></el-col>
              <el-col :span="12"><el-form-item label="姓名" required><el-input v-model="form.fName" /></el-form-item></el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12"><el-form-item label="性别"><el-select v-model="form.fSex" style="width:100%"><el-option :value="1" label="男" /><el-option :value="2" label="女" /></el-select></el-form-item></el-col>
              <el-col :span="12"><el-form-item label="学历"><el-input v-model="form.fEducation" /></el-form-item></el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12"><el-form-item label="电话"><el-input v-model="form.fTel" /></el-form-item></el-col>
              <el-col :span="12"><el-form-item label="部门"><LookupSelect v-model="form.fSalDeptId" module="department" placeholder="请选择部门" preload /></el-form-item></el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12"><el-form-item label="出生日期"><el-date-picker v-model="form.fBirthDate" type="date" style="width:100%" /></el-form-item></el-col>
              <el-col :span="12"><el-form-item label="入职日期"><el-date-picker v-model="form.fEntryDate" type="date" style="width:100%" /></el-form-item></el-col>
            </el-row>
            <el-row :gutter="20" v-if="dialogType === 'edit'">
              <el-col :span="12"><el-form-item label="离职日期"><el-date-picker v-model="form.fDepartureDate" type="date" style="width:100%" /></el-form-item></el-col>
              <el-col :span="12"><el-form-item label="已离职"><el-switch v-model="form.fIsDeparture" /></el-form-item></el-col>
            </el-row>
            <el-form-item label="邮箱"><el-input v-model="form.fMail" /></el-form-item>
            <el-form-item label="分组">
              <el-tree-select v-model="form.fGroupId" :data="groupPanelRef?.treeData || []" :props="{ label: 'fName', children: 'children', value: 'uid' }" placeholder="请选择分组" clearable check-strictly style="width: 100%" />
            </el-form-item>
            <el-form-item label="备注"><el-input v-model="form.fNote" type="textarea" :rows="2" /></el-form-item>
          </el-tab-pane>
          <el-tab-pane label="联系信息" name="contact">
            <el-form-item label="家庭住址"><el-input v-model="form.fAddress" /></el-form-item>
            <el-row :gutter="20">
              <el-col :span="12"><el-form-item label="微信"><el-input v-model="form.fWechat" /></el-form-item></el-col>
              <el-col :span="12"><el-form-item label="紧急联系人"><el-input v-model="form.emergency" /></el-form-item></el-col>
            </el-row>
            <el-form-item label="紧急联系电话"><el-input v-model="form.fEmergencyTel" /></el-form-item>
          </el-tab-pane>
        </el-tabs>
      </el-form>
      <template #footer><el-button @click="dialogVisible = false">{{ isReadonly ? '关闭' : '取消' }}</el-button><el-button v-if="!isReadonly" type="primary" @click="submitForm">确定</el-button></template>
    </el-dialog>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { ref, reactive, computed, onMounted } from 'vue'
import { getEmployees, getEmployee, createEmployee, updateEmployee, deleteEmployee, approveEmployee, unapproveEmployee, disableEmployee, enableEmployee, type Employee } from '../../api/employee'
import { formatDate } from '../../utils/format'
import { ElMessage } from 'element-plus'
import { Search, Plus, Edit, DArrowRight } from '@element-plus/icons-vue'
import LookupSelect from '../../components/LookupSelect.vue'
import ColumnSetting from '../../components/ColumnSetting.vue'
import GroupPanel from '../../components/GroupPanel.vue'
import { useColumnConfig, type ColumnDef } from '../../composables/useColumnConfig'
import { useTableSelection } from '../../composables/useTableSelection'

const groupPanelRef = ref<InstanceType<typeof GroupPanel>>()
const tableRef = ref()

const columns: ColumnDef[] = [
  { key: 'fNumber', label: '员工编码', prop: 'fNumber', width: 130 },
  { key: 'fName', label: '姓名', prop: 'fName', minWidth: 120 },
  { key: 'fSex', label: '性别', width: 80, align: 'center', slotName: 'sex' },
  { key: 'fTel', label: '电话', prop: 'fTel', width: 140 },
  { key: 'fSalDeptName', label: '部门', prop: 'fSalDeptName', width: 120 },
  { key: 'fEducation', label: '学历', prop: 'fEducation', width: 100, defaultVisible: false },
  { key: 'fBirthDate', label: '出生日期', width: 120, slotName: 'dateField', defaultVisible: false },
  { key: 'fEntryDate', label: '入职日期', width: 120, slotName: 'dateField', defaultVisible: false },
  { key: 'fDepartureDate', label: '离职日期', width: 120, slotName: 'dateField', defaultVisible: false },
  { key: 'fIsDeparture', label: '在职状态', width: 100, align: 'center', slotName: 'departure' },
  { key: 'fMail', label: '邮箱', prop: 'fMail', width: 180, defaultVisible: false },
  { key: 'fNote', label: '备注', prop: 'fNote', minWidth: 200, defaultVisible: false },
  { key: 'fAddress', label: '家庭住址', prop: 'fAddress', minWidth: 200, defaultVisible: false },
  { key: 'fWechat', label: '微信', prop: 'fWechat', width: 130, defaultVisible: false },
  { key: 'emergency', label: '紧急联系人', prop: 'emergency', width: 120, defaultVisible: false },
  { key: 'fEmergencyTel', label: '紧急联系电话', prop: 'fEmergencyTel', width: 140, defaultVisible: false },
  { key: 'fStatus', label: '审核状态', width: 100, align: 'center', slotName: 'status' },
  { key: 'fDisabled', label: '禁用状态', width: 100, align: 'center', slotName: 'disabled' },
  { key: 'cYmd', label: '创建时间', width: 180, slotName: 'createTime' },
]

const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig('employee', columns)

const loading = ref(false)
const list = ref<Employee[]>([])
const total = ref(0)
const queryParams = reactive({ page: 1, pageSize: 10, keyword: '', groupId: '' })

const {
  selectedCount, canEdit, canApprove, canUnapprove, canDelete, canDisable, canEnable, batchLoading,
  handleSelectionChange, handleBatchApprove, handleBatchUnapprove, handleBatchDelete, handleBatchDisable, handleBatchEnable, clearSelection
} = useTableSelection<Employee>({
  entityName: '职员',
  approveFn: approveEmployee,
  unapproveFn: unapproveEmployee,
  deleteFn: deleteEmployee,
  disableFn: disableEmployee,
  enableFn: enableEmployee,
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
const defaultForm = { uid: undefined as string | undefined, fNumber: '', fName: '', fSex: 1, fEducation: '', fBirthDate: undefined as string | undefined, fEntryDate: undefined as string | undefined, fDepartureDate: undefined as string | undefined, fIsDeparture: false, fTel: '', fSalDeptId: '', fMail: '', fNote: '', fAddress: '', fWechat: '', fEmergencyTel: '', emergency: '', fGroupId: '', fStatus: 0, fDisabled: false }
const form = reactive({ ...defaultForm })
const isReadonly = computed(() => dialogType.value === 'edit' && (form.fStatus === 40 || form.fDisabled))

async function fetchData() { loading.value = true; try { const res: any = await getEmployees(queryParams); list.value = res.data.items; total.value = res.data.totalCount } catch (e) { console.error(e) } finally { loading.value = false } }
const handleAdd = () => { dialogType.value = 'create'; activeTab.value = 'basic'; Object.assign(form, { ...defaultForm }); dialogVisible.value = true }
const handleEdit = async (row: Employee) => { dialogType.value = 'edit'; activeTab.value = 'basic'; try { const res: any = await getEmployee(row.uid!); const d = res.data; Object.assign(form, { uid: d.uid, fNumber: d.fNumber||'', fName: d.fName||'', fSex: d.fSex??1, fEducation: d.fEducation||'', fBirthDate: d.fBirthDate||undefined, fEntryDate: d.fEntryDate||undefined, fDepartureDate: d.fDepartureDate||undefined, fIsDeparture: d.fIsDeparture||false, fTel: d.fTel||'', fSalDeptId: d.fSalDeptId||'', fMail: d.fMail||'', fNote: d.fNote||'', fAddress: d.fAddress||'', fWechat: d.fWechat||'', fEmergencyTel: d.fEmergencyTel||'', emergency: d.emergency||'', fGroupId: d.fGroupId||'', fStatus: d.fStatus||0, fDisabled: d.fDisabled||false }) } catch (e) { console.error(e) }; dialogVisible.value = true }
const handleEditSelected = () => { const rows = tableRef.value?.getSelectionRows() as Employee[]; if (rows?.length === 1) handleEdit(rows[0]) }
const handleRowDblClick = (row: Employee) => { handleEdit(row) }
const submitForm = async () => { if (!form.fNumber || !form.fName) { ElMessage.warning('请输入员工编码和姓名'); return }; try { if (dialogType.value === 'create') { await createEmployee({ fNumber: form.fNumber, fName: form.fName, fSex: form.fSex, fEducation: form.fEducation, fBirthDate: form.fBirthDate, fEntryDate: form.fEntryDate, fTel: form.fTel, fSalDeptId: form.fSalDeptId, fMail: form.fMail, fNote: form.fNote, fAddress: form.fAddress, fWechat: form.fWechat, fEmergencyTel: form.fEmergencyTel, emergency: form.emergency, fGroupId: form.fGroupId }); ElMessage.success('创建成功') } else { await updateEmployee(form.uid!, { fName: form.fName, fSex: form.fSex, fEducation: form.fEducation, fBirthDate: form.fBirthDate, fEntryDate: form.fEntryDate, fDepartureDate: form.fDepartureDate, fIsDeparture: form.fIsDeparture, fTel: form.fTel, fSalDeptId: form.fSalDeptId, fMail: form.fMail, fNote: form.fNote, fAddress: form.fAddress, fWechat: form.fWechat, fEmergencyTel: form.fEmergencyTel, emergency: form.emergency, fGroupId: form.fGroupId }); ElMessage.success('更新成功') }; dialogVisible.value = false; fetchData() } catch (error: any) { const msg = error.response?.data?.message || '提交失败'; ElMessage.error(msg) } }
onMounted(() => { fetchData() })
</script>
<style scoped>
.employee-list-container { padding: 20px; background-color: var(--bg-card); border-radius: 8px; box-shadow: var(--shadow-card); }
.list-layout { display: flex; gap: 16px; }
.list-panel { flex: 1; min-width: 0; }
.toggle-group-btn { margin-right: 8px; flex-shrink: 0; }
.header-actions { display: flex; justify-content: space-between; margin-bottom: 12px; }
.header-right { display: flex; align-items: center; gap: 8px; }
.search-input { width: 300px; }
.toolbar-actions { display: flex; align-items: center; gap: 8px; margin-bottom: 12px; }
.pagination-container { margin-top: 20px; display: flex; justify-content: flex-end; }
</style>
