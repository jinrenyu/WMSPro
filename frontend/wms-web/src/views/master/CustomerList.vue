<template>
  <div class="customer-list-container">
    <div class="list-layout">
      <GroupPanel
        ref="groupPanelRef"
        prg-key="BD_Customer"
        title="客户分组"
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
        placeholder="搜索客户编码/名称/联系人"
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
        :api-fields-func="getCustomersFields"
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
      <el-button type="primary" @click="handleAdd" v-permission="'customer:add'">
        <el-icon><Plus /></el-icon> 新增
      </el-button>
      <el-button @click="handleEditSelected" :disabled="!canEdit" v-permission="'customer:edit'">
        <el-icon><Edit /></el-icon> 编辑
      </el-button>
      <el-button type="success" @click="handleBatchApprove" :disabled="!canApprove" :loading="batchLoading" v-permission="'customer:approve'">
        审核{{ canApprove ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="warning" @click="handleBatchUnapprove" :disabled="!canUnapprove" :loading="batchLoading" v-permission="'customer:approve'">
        反审核{{ canUnapprove ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="danger" @click="handleBatchDelete" :disabled="!canDelete" :loading="batchLoading" v-permission="'customer:delete'">
        删除{{ canDelete ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button type="info" @click="handleBatchDisable" :disabled="!canDisable" :loading="batchLoading" v-permission="'customer:disable'">
        禁用{{ canDisable ? ` (${selectedCount})` : '' }}
      </el-button>
      <el-button @click="handleBatchEnable" :disabled="!canEnable" :loading="batchLoading" v-permission="'customer:disable'">
        反禁用{{ canEnable ? ` (${selectedCount})` : '' }}
      </el-button>
    </div>

    <el-table
      ref="tableRef"
      v-loading="loading"
      :data="customerList"
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
      :title="dialogType === 'create' ? '新增客户' : isReadonly ? '查看客户' : '编辑客户'"
      width="700px"
    >
      <el-form :model="form" label-width="100px" :disabled="isReadonly">
        <el-tabs v-model="activeTab">
          <!-- 基本信息 -->
          <el-tab-pane label="基本信息" name="basic">
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="客户编码" required>
                  <el-input v-model="form.fNumber" :disabled="dialogType === 'edit'" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="客户名称" required>
                  <el-input v-model="form.fName" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="简称">
                  <el-input v-model="form.fShortName" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="联系人">
                  <el-input v-model="form.fContact" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="电话">
                  <el-input v-model="form.fPhone" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="销售员">
                  <el-input v-model="form.fSeller" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-form-item label="地址">
              <el-input v-model="form.fAddress" />
            </el-form-item>
            <el-form-item label="分组">
              <el-tree-select v-model="form.fGroupId" :data="groupPanelRef?.treeData || []" :props="{ label: 'fName', children: 'children', value: 'uid' }" placeholder="请选择分组" clearable check-strictly style="width: 100%" />
            </el-form-item>
            <el-form-item label="备注">
              <el-input v-model="form.fNote" type="textarea" :rows="2" />
            </el-form-item>
          </el-tab-pane>

          <!-- 联系信息 -->
          <el-tab-pane label="联系信息" name="contact">
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="座机">
                  <el-input v-model="form.fTel" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="传真">
                  <el-input v-model="form.fFax" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="邮箱">
                  <el-input v-model="form.fEmail" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="网站">
                  <el-input v-model="form.fWebSite" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="国家">
                  <el-input v-model="form.fCountry" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="交易币种">
                  <LookupSelect v-model="form.fTradingCurrId" module="currency" placeholder="请选择币种" preload />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="省份">
                  <el-input v-model="form.fProvincial" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="城市">
                  <el-input v-model="form.fCity" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="邮编">
                  <el-input v-model="form.fZip" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="英文名称">
                  <el-input v-model="form.fNameEn" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-form-item label="英文地址">
              <el-input v-model="form.fAddressEn" />
            </el-form-item>
          </el-tab-pane>

          <!-- 财务信息 -->
          <el-tab-pane label="财务信息" name="finance">
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="开户银行">
                  <el-input v-model="form.fBank" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="银行账号">
                  <el-input v-model="form.fAccount" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="法人">
                  <el-input v-model="form.fLegalPerson" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="税务登记号">
                  <el-input v-model="form.fTaxRegisterCode" />
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
import { getCustomers, getCustomer, createCustomer, updateCustomer, deleteCustomer, approveCustomer, unapproveCustomer, disableCustomer, enableCustomer, getCustomersFields, type Customer } from '../../api/customer'
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
  // 基本信息
  { key: 'fNumber', label: '客户编码', prop: 'fNumber', width: 150 },
  { key: 'fName', label: '客户名称', prop: 'fName', minWidth: 200 },
  { key: 'fShortName', label: '简称', prop: 'fShortName', width: 150 },
  { key: 'fContact', label: '联系人', prop: 'fContact', width: 120 },
  { key: 'fPhone', label: '电话', prop: 'fPhone', width: 150 },
  { key: 'fSeller', label: '销售员', prop: 'fSeller', width: 120, defaultVisible: false },
  { key: 'fAddress', label: '地址', prop: 'fAddress', minWidth: 200 },
  { key: 'fNote', label: '备注', prop: 'fNote', minWidth: 200, defaultVisible: false },
  // 联系信息
  { key: 'fTel', label: '座机', prop: 'fTel', width: 140, defaultVisible: false },
  { key: 'fFax', label: '传真', prop: 'fFax', width: 140, defaultVisible: false },
  { key: 'fEmail', label: '邮箱', prop: 'fEmail', width: 180, defaultVisible: false },
  { key: 'fWebSite', label: '网站', prop: 'fWebSite', width: 180, defaultVisible: false },
  { key: 'fCountry', label: '国家', prop: 'fCountry', width: 100, defaultVisible: false },
  { key: 'fProvincial', label: '省份', prop: 'fProvincial', width: 100, defaultVisible: false },
  { key: 'fCity', label: '城市', prop: 'fCity', width: 100, defaultVisible: false },
  { key: 'fZip', label: '邮编', prop: 'fZip', width: 100, defaultVisible: false },
  { key: 'fTradingCurrId', label: '交易币种', prop: 'fTradingCurrId', width: 120, defaultVisible: false },
  { key: 'fNameEn', label: '英文名称', prop: 'fNameEn', width: 150, defaultVisible: false },
  { key: 'fAddressEn', label: '英文地址', prop: 'fAddressEn', minWidth: 200, defaultVisible: false },
  // 财务信息
  { key: 'fBank', label: '开户银行', prop: 'fBank', width: 150, defaultVisible: false },
  { key: 'fAccount', label: '银行账号', prop: 'fAccount', width: 180, defaultVisible: false },
  { key: 'fLegalPerson', label: '法人', prop: 'fLegalPerson', width: 100, defaultVisible: false },
  { key: 'fTaxRegisterCode', label: '税务登记号', prop: 'fTaxRegisterCode', width: 150, defaultVisible: false },
  // 系统字段
  { key: 'fStatus', label: '审核状态', width: 100, align: 'center', slotName: 'status' },
  { key: 'fDisabled', label: '禁用状态', width: 100, align: 'center', slotName: 'disabled' },
  { key: 'cYmd', label: '创建时间', width: 180, slotName: 'createTime' },
]

const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig('customer', columns)

const loading = ref(false)
const customerList = ref<Customer[]>([])
const total = ref(0)
const queryParams = reactive({
  page: 1,
  pageSize: 10,
  keyword: '',
  groupId: '',
  dynamicFilters: [] as DynamicFilterInfo[]
})

const {
  selectedCount, canEdit, canApprove, canUnapprove, canDelete, canDisable, canEnable, batchLoading,
  handleSelectionChange, handleBatchApprove, handleBatchUnapprove, handleBatchDelete, handleBatchDisable, handleBatchEnable, clearSelection
} = useTableSelection<Customer>({
  entityName: '客户',
  approveFn: approveCustomer,
  unapproveFn: unapproveCustomer,
  deleteFn: deleteCustomer,
  disableFn: disableCustomer,
  enableFn: enableCustomer,
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
  fShortName: '',
  fContact: '',
  fPhone: '',
  fAddress: '',
  fSeller: '',
  fTradingCurrId: '',
  fCountry: '',
  fProvincial: '',
  fCity: '',
  fZip: '',
  fWebSite: '',
  fTel: '',
  fFax: '',
  fEmail: '',
  fBank: '',
  fAccount: '',
  fLegalPerson: '',
  fTaxRegisterCode: '',
  fNameEn: '',
  fAddressEn: '',
  fNote: '',
  fGroupId: '',
  fDisabled: false
}

const form = reactive({ ...defaultForm })

async function fetchData() {
  loading.value = true
  try {
    const res: any = await getCustomers(queryParams)
    customerList.value = res.data.items
    total.value = res.data.totalCount
  } catch (error) {
    console.error('Fetch customers failed:', error)
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

const handleEdit = async (row: Customer) => {
  dialogType.value = 'edit'
  activeTab.value = 'basic'
  try {
    const res: any = await getCustomer(row.uid!)
    const d = res.data
    Object.assign(form, {
      uid: d.uid,
      fStatus: d.fStatus || 0,
      fNumber: d.fNumber || '',
      fName: d.fName || '',
      fShortName: d.fShortName || '',
      fContact: d.fContact || '',
      fPhone: d.fPhone || '',
      fAddress: d.fAddress || '',
      fSeller: d.fSeller || '',
      fTradingCurrId: d.fTradingCurrId || '',
      fCountry: d.fCountry || '',
      fProvincial: d.fProvincial || '',
      fCity: d.fCity || '',
      fZip: d.fZip || '',
      fWebSite: d.fWebSite || '',
      fTel: d.fTel || '',
      fFax: d.fFax || '',
      fEmail: d.fEmail || '',
      fBank: d.fBank || '',
      fAccount: d.fAccount || '',
      fLegalPerson: d.fLegalPerson || '',
      fTaxRegisterCode: d.fTaxRegisterCode || '',
      fNameEn: d.fNameEn || '',
      fAddressEn: d.fAddressEn || '',
      fNote: d.fNote || '',
      fGroupId: d.fGroupId || '',
      fDisabled: d.fDisabled || false
    })
  } catch (error) {
    console.error('Fetch customer detail failed:', error)
  }
  dialogVisible.value = true
}

const handleEditSelected = () => {
  const rows = tableRef.value?.getSelectionRows() as Customer[]
  if (rows?.length === 1) handleEdit(rows[0])
}

const handleRowDblClick = (row: Customer) => {
  handleEdit(row)
}

const submitForm = async () => {
  if (!form.fNumber || !form.fName) {
    ElMessage.warning('请输入客户编码和名称')
    return
  }

  const data = {
    fNumber: form.fNumber,
    fName: form.fName,
    fShortName: form.fShortName,
    fContact: form.fContact,
    fPhone: form.fPhone,
    fAddress: form.fAddress,
    fSeller: form.fSeller,
    fTradingCurrId: form.fTradingCurrId,
    fCountry: form.fCountry,
    fTel: form.fTel,
    fEmail: form.fEmail,
    fNote: form.fNote,
    fGroupId: form.fGroupId
  }

  try {
    if (dialogType.value === 'create') {
      await createCustomer(data)
      ElMessage.success('创建成功')
    } else {
      const { fNumber, ...updateData } = data
      await updateCustomer(form.uid!, updateData)
      ElMessage.success('更新成功')
    }
    dialogVisible.value = false
    fetchData()
  } catch (error: any) {
    console.error('Submit customer failed:', error)
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
.customer-list-container {
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
