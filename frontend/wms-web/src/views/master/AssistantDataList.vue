<template>
  <div class="assistantdata-list-container">
    <div class="list-layout">
      <!-- 左侧：辅助资料类别 -->
      <div class="category-panel">
        <div class="category-header">
          <span class="category-title">辅助资料类别</span>
          <el-button type="primary" size="small" @click="handleAddCategory" v-permission="'assistantdata:add'">
            <el-icon><Plus /></el-icon>
          </el-button>
        </div>
        <el-input
          v-model="categoryKeyword"
          placeholder="搜索类别"
          class="category-search"
          clearable
          size="small"
        >
          <template #prefix><el-icon><Search /></el-icon></template>
        </el-input>
        <div class="category-list" v-loading="categoryLoading">
          <div
            v-for="cat in filteredCategories"
            :key="cat.uid"
            class="category-item"
            :class="{ active: selectedCategory?.uid === cat.uid }"
            @click="selectCategory(cat)"
          >
            <div class="category-item-info">
              <span class="category-name">{{ cat.fname }}</span>
              <span class="category-number">{{ cat.fnumber }}</span>
            </div>
            <div class="category-item-actions">
              <el-icon class="action-icon" @click.stop="handleEditCategory(cat)" v-permission="'assistantdata:edit'"><Edit /></el-icon>
              <el-icon class="action-icon danger" @click.stop="handleDeleteCategory(cat)" v-permission="'assistantdata:delete'"><Delete /></el-icon>
            </div>
          </div>
          <el-empty v-if="filteredCategories.length === 0 && !categoryLoading" description="暂无类别" :image-size="60" />
        </div>
      </div>

      <!-- 右侧：辅助资料明细 -->
      <div class="list-panel">
        <div class="header-actions">
          <el-input
            v-model="queryParams.keyword"
            placeholder="搜索代码/名称"
            class="search-input"
            clearable
            @clear="fetchEntries"
            @keyup.enter="fetchEntries"
          >
            <template #append>
              <el-button @click="fetchEntries"><el-icon><Search /></el-icon></el-button>
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

        <div v-if="!selectedCategory" class="empty-hint">
          <el-empty description="请先选择左侧的辅助资料类别" :image-size="100" />
        </div>

        <template v-else>
          <div class="toolbar-actions">
            <el-button type="primary" @click="handleAddEntry" :disabled="!selectedCategory" v-permission="'assistantdata:add'">
              <el-icon><Plus /></el-icon> 新增
            </el-button>
            <el-button @click="handleEditSelected" :disabled="!canEdit" v-permission="'assistantdata:edit'">
              <el-icon><Edit /></el-icon> 编辑
            </el-button>
            <el-button type="success" @click="handleBatchApprove" :disabled="!canApprove" :loading="batchLoading" v-permission="'assistantdata:approve'">
              审核{{ canApprove ? ` (${selectedCount})` : '' }}
            </el-button>
            <el-button type="warning" @click="handleBatchUnapprove" :disabled="!canUnapprove" :loading="batchLoading" v-permission="'assistantdata:approve'">
              反审核{{ canUnapprove ? ` (${selectedCount})` : '' }}
            </el-button>
            <el-button type="danger" @click="handleBatchDelete" :disabled="!canDelete" :loading="batchLoading" v-permission="'assistantdata:delete'">
              删除{{ canDelete ? ` (${selectedCount})` : '' }}
            </el-button>
            <el-button type="info" @click="handleBatchDisable" :disabled="!canDisable" :loading="batchLoading" v-permission="'assistantdata:disable'">
              禁用{{ canDisable ? ` (${selectedCount})` : '' }}
            </el-button>
            <el-button @click="handleBatchEnable" :disabled="!canEnable" :loading="batchLoading" v-permission="'assistantdata:disable'">
              反禁用{{ canEnable ? ` (${selectedCount})` : '' }}
            </el-button>
          </div>

          <el-table ref="tableRef" v-loading="entryLoading" :data="entryList" style="width: 100%" border @selection-change="handleSelectionChange" @row-dblclick="handleRowDblClick">
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
                  <template v-if="col.slotName === 'isdefault'">
                    <el-tag :type="scope.row.isdefault ? 'warning' : 'info'" size="small">{{ scope.row.isdefault ? '是' : '否' }}</el-tag>
                  </template>
                  <template v-else-if="col.slotName === 'fisbuildself'">
                    <el-tag :type="scope.row.fisbuildself ? 'success' : 'info'" size="small">{{ scope.row.fisbuildself ? '是' : '否' }}</el-tag>
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
              :total="entryTotal"
              layout="total, prev, pager, next"
              @current-change="fetchEntries"
            />
          </div>
        </template>
      </div>
    </div>

    <!-- 类别弹窗 -->
    <el-dialog v-model="categoryDialogVisible" :title="categoryDialogType === 'create' ? '新增辅助资料类别' : '编辑辅助资料类别'" width="450px">
      <el-form :model="categoryForm" label-width="100px">
        <el-form-item label="类别代码" required>
          <el-input v-model="categoryForm.fnumber" :disabled="categoryDialogType === 'edit'" />
        </el-form-item>
        <el-form-item label="类别名称" required>
          <el-input v-model="categoryForm.fname" />
        </el-form-item>
        <el-form-item label="描述">
          <el-input v-model="categoryForm.fdescription" type="textarea" :rows="2" />
        </el-form-item>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="系统预设">
              <el-switch v-model="categoryForm.isdefault" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="O3自用">
              <el-switch v-model="categoryForm.fiso3use" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <el-button @click="categoryDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="submitCategory">确定</el-button>
      </template>
    </el-dialog>

    <!-- 明细弹窗 -->
    <el-dialog v-model="entryDialogVisible" :title="entryDialogType === 'create' ? '新增辅助资料' : isReadonly ? '查看辅助资料' : '编辑辅助资料'" width="500px">
      <el-form :model="entryForm" label-width="100px" :disabled="isReadonly">
        <el-form-item label="所属类别">
          <el-input :model-value="selectedCategory?.fname" disabled />
        </el-form-item>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="代码" required>
              <el-input v-model="entryForm.fnumber" :disabled="entryDialogType === 'edit'" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="名称" required>
              <el-input v-model="entryForm.fdatavalue" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="显示顺序">
              <el-input-number v-model="entryForm.fseq" :min="0" style="width: 100%" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="系统预设">
              <el-switch v-model="entryForm.isdefault" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="备注">
          <el-input v-model="entryForm.fdescription" type="textarea" :rows="2" />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="entryDialogVisible = false">{{ isReadonly ? '关闭' : '取消' }}</el-button>
        <el-button v-if="!isReadonly" type="primary" @click="submitEntry">确定</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted } from 'vue'
import {
  getAssistantDataList, getAssistantData, createAssistantData, updateAssistantData, deleteAssistantData, approveAssistantData, unapproveAssistantData,
  getAssistantDataEntries, getAssistantDataEntry, createAssistantDataEntry, updateAssistantDataEntry, deleteAssistantDataEntry, approveAssistantDataEntry, unapproveAssistantDataEntry, disableAssistantDataEntry, enableAssistantDataEntry,
  type AssistantData, type AssistantDataEntry
} from '../../api/assistantdata'
import { formatDate } from '../../utils/format'
import { ElMessage, ElMessageBox } from 'element-plus'
import { Search, Plus, Edit, Delete } from '@element-plus/icons-vue'
import ColumnSetting from '../../components/ColumnSetting.vue'
import { useColumnConfig, type ColumnDef } from '../../composables/useColumnConfig'
import { useTableSelection } from '../../composables/useTableSelection'

const tableRef = ref()

// ═══════════════════════════════════════════════════════
// 类别
// ═══════════════════════════════════════════════════════
const categoryLoading = ref(false)
const categoryList = ref<AssistantData[]>([])
const selectedCategory = ref<AssistantData | null>(null)
const categoryKeyword = ref('')

const filteredCategories = computed(() => {
  if (!categoryKeyword.value) return categoryList.value
  const kw = categoryKeyword.value.toLowerCase()
  return categoryList.value.filter(c => c.fname.toLowerCase().includes(kw) || c.fnumber.toLowerCase().includes(kw))
})

const fetchCategories = async () => {
  categoryLoading.value = true
  try {
    const res: any = await getAssistantDataList({ page: 1, pageSize: 200 })
    categoryList.value = res.data.items
  } catch (e) {
    console.error(e)
  } finally {
    categoryLoading.value = false
  }
}

const selectCategory = (cat: AssistantData) => {
  selectedCategory.value = cat
  queryParams.page = 1
  fetchEntries()
}

// 类别弹窗
const categoryDialogVisible = ref(false)
const categoryDialogType = ref<'create' | 'edit'>('create')
const defaultCategoryForm = { uid: undefined as string | undefined, fnumber: '', fname: '', fdescription: '', isdefault: false, fiso3use: false }
const categoryForm = reactive({ ...defaultCategoryForm })

const handleAddCategory = () => {
  categoryDialogType.value = 'create'
  Object.assign(categoryForm, { ...defaultCategoryForm })
  categoryDialogVisible.value = true
}

const handleEditCategory = async (row: AssistantData) => {
  categoryDialogType.value = 'edit'
  try {
    const res: any = await getAssistantData(row.uid!)
    const d = res.data
    Object.assign(categoryForm, { uid: d.uid, fnumber: d.fnumber || '', fname: d.fname || '', fdescription: d.fdescription || '', isdefault: d.isdefault || false, fiso3use: d.fiso3use || false })
  } catch (e) { console.error(e) }
  categoryDialogVisible.value = true
}

const handleDeleteCategory = (row: AssistantData) => {
  ElMessageBox.confirm('删除类别将同时影响其下的辅助资料，确定删除吗？', '警告', { confirmButtonText: '确定', cancelButtonText: '取消', type: 'warning' })
    .then(async () => {
      await deleteAssistantData(row.uid!)
      ElMessage.success('删除成功')
      if (selectedCategory.value?.uid === row.uid) selectedCategory.value = null
      fetchCategories()
    })
}

const submitCategory = async () => {
  if (!categoryForm.fnumber || !categoryForm.fname) { ElMessage.warning('请输入类别代码和名称'); return }
  try {
    if (categoryDialogType.value === 'create') {
      await createAssistantData({ fnumber: categoryForm.fnumber, fname: categoryForm.fname, fdescription: categoryForm.fdescription, isdefault: categoryForm.isdefault, fiso3use: categoryForm.fiso3use })
      ElMessage.success('创建成功')
    } else {
      await updateAssistantData(categoryForm.uid!, { fname: categoryForm.fname, fdescription: categoryForm.fdescription, isdefault: categoryForm.isdefault, fiso3use: categoryForm.fiso3use })
      ElMessage.success('更新成功')
    }
    categoryDialogVisible.value = false
    fetchCategories()
  } catch (error: any) {
    const msg = error.response?.data?.message || '提交失败'
    ElMessage.error(msg)
  }
}

// ═══════════════════════════════════════════════════════
// 明细
// ═══════════════════════════════════════════════════════
const columns: ColumnDef[] = [
  { key: 'fnumber', label: '代码', prop: 'fnumber', width: 130 },
  { key: 'fdatavalue', label: '名称', prop: 'fdatavalue', minWidth: 180 },
  { key: 'fseq', label: '顺序', prop: 'fseq', width: 80, align: 'center' },
  { key: 'isdefault', label: '系统预设', width: 100, align: 'center', slotName: 'isdefault' },
  { key: 'fisbuildself', label: '自建', width: 80, align: 'center', slotName: 'fisbuildself', defaultVisible: false },
  { key: 'fdescription', label: '备注', prop: 'fdescription', minWidth: 200, defaultVisible: false },
  { key: 'fStatus', label: '审核状态', width: 100, align: 'center', slotName: 'status' },
  { key: 'fDisabled', label: '禁用状态', width: 100, align: 'center', slotName: 'disabled' },
  { key: 'cYmd', label: '创建时间', width: 180, slotName: 'createTime' },
]

const { allColumns, visibleKeys, configurableColumns, toggleColumn, resetColumns, isColumnVisible } = useColumnConfig('assistantdata', columns)

const entryLoading = ref(false)
const entryList = ref<AssistantDataEntry[]>([])
const entryTotal = ref(0)
const queryParams = reactive({ page: 1, pageSize: 10, keyword: '' })

const {
  selectedCount, canEdit, canApprove, canUnapprove, canDelete, canDisable, canEnable, batchLoading,
  handleSelectionChange, handleBatchApprove, handleBatchUnapprove, handleBatchDelete, handleBatchDisable, handleBatchEnable, clearSelection
} = useTableSelection<AssistantDataEntry>({
  entityName: '辅助资料',
  approveFn: approveAssistantDataEntry,
  unapproveFn: unapproveAssistantDataEntry,
  deleteFn: deleteAssistantDataEntry,
  disableFn: disableAssistantDataEntry,
  enableFn: enableAssistantDataEntry,
  onSuccess: fetchEntries,
})

const handleEditSelected = () => {
  const rows = tableRef.value?.getSelectionRows() as AssistantDataEntry[]
  if (rows?.length === 1) handleEditEntry(rows[0])
}

const handleRowDblClick = (row: AssistantDataEntry) => {
  handleEditEntry(row)
}

async function fetchEntries() {
  if (!selectedCategory.value) return
  entryLoading.value = true
  try {
    const res: any = await getAssistantDataEntries({ ...queryParams, fid: selectedCategory.value.uid })
    entryList.value = res.data.items
    entryTotal.value = res.data.totalCount
  } catch (e) {
    console.error(e)
  } finally {
    entryLoading.value = false
  }
}

// 明细弹窗
const entryDialogVisible = ref(false)
const entryDialogType = ref<'create' | 'edit'>('create')
const defaultEntryForm = { uid: undefined as string | undefined, fnumber: '', fdatavalue: '', fdescription: '', fseq: 0, isdefault: false, fStatus: 0, fDisabled: false }
const entryForm = reactive({ ...defaultEntryForm })
const isReadonly = computed(() => entryDialogType.value === 'edit' && (entryForm.fStatus === 40 || entryForm.fDisabled))

const handleAddEntry = () => {
  entryDialogType.value = 'create'
  Object.assign(entryForm, { ...defaultEntryForm })
  entryDialogVisible.value = true
}

const handleEditEntry = async (row: AssistantDataEntry) => {
  entryDialogType.value = 'edit'
  try {
    const res: any = await getAssistantDataEntry(row.uid!)
    const d = res.data
    Object.assign(entryForm, { uid: d.uid, fnumber: d.fnumber || '', fdatavalue: d.fdatavalue || '', fdescription: d.fdescription || '', fseq: d.fseq || 0, isdefault: d.isdefault || false, fStatus: d.fStatus || 0, fDisabled: d.fDisabled || false })
  } catch (e) { console.error(e) }
  entryDialogVisible.value = true
}

const submitEntry = async () => {
  if (!entryForm.fnumber || !entryForm.fdatavalue) { ElMessage.warning('请输入代码和名称'); return }
  try {
    if (entryDialogType.value === 'create') {
      await createAssistantDataEntry({ fnumber: entryForm.fnumber, fdatavalue: entryForm.fdatavalue, fid: selectedCategory.value!.uid, fdescription: entryForm.fdescription, fseq: entryForm.fseq, isdefault: entryForm.isdefault })
      ElMessage.success('创建成功')
    } else {
      await updateAssistantDataEntry(entryForm.uid!, { fdatavalue: entryForm.fdatavalue, fdescription: entryForm.fdescription, fseq: entryForm.fseq, isdefault: entryForm.isdefault })
      ElMessage.success('更新成功')
    }
    entryDialogVisible.value = false
    fetchEntries()
  } catch (error: any) {
    const msg = error.response?.data?.message || '提交失败'
    ElMessage.error(msg)
  }
}

onMounted(() => { fetchCategories() })
</script>

<style scoped>
.assistantdata-list-container {
  padding: 20px;
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
}

.list-layout {
  display: flex;
  gap: 16px;
}

.category-panel {
  width: 280px;
  flex-shrink: 0;
  border-right: 1px solid var(--el-border-color-lighter);
  padding-right: 16px;
}

.category-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 12px;
}

.category-title {
  font-size: 15px;
  font-weight: 600;
  color: var(--el-text-color-primary);
}

.category-search {
  margin-bottom: 12px;
}

.category-list {
  max-height: calc(100vh - 260px);
  overflow-y: auto;
}

.category-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px 12px;
  border-radius: 6px;
  cursor: pointer;
  transition: all 0.2s;
  margin-bottom: 4px;
}

.category-item:hover {
  background-color: var(--el-fill-color-light);
}

.category-item.active {
  background-color: var(--el-color-primary-light-9);
  color: var(--el-color-primary);
}

.category-item-info {
  display: flex;
  flex-direction: column;
  gap: 2px;
  min-width: 0;
}

.category-name {
  font-size: 14px;
  font-weight: 500;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.category-number {
  font-size: 12px;
  color: var(--el-text-color-secondary);
}

.category-item-actions {
  display: none;
  gap: 8px;
  flex-shrink: 0;
}

.category-item:hover .category-item-actions {
  display: flex;
}

.action-icon {
  font-size: 14px;
  cursor: pointer;
  color: var(--el-text-color-secondary);
}

.action-icon:hover {
  color: var(--el-color-primary);
}

.action-icon.danger:hover {
  color: var(--el-color-danger);
}

.list-panel {
  flex: 1;
  min-width: 0;
}

.header-actions {
  display: flex;
  justify-content: space-between;
  margin-bottom: 12px;
}

.toolbar-actions {
  display: flex;
  align-items: center;
  gap: 8px;
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

.pagination-container {
  margin-top: 20px;
  display: flex;
  justify-content: flex-end;
}

.empty-hint {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 300px;
}
</style>
