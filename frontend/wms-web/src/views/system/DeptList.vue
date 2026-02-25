<template>
  <div class="dept-list-container">
    <div class="header-actions">
      <el-input
        v-model="queryParams.keyword"
        placeholder="搜索部门名称"
        class="search-input"
        clearable
        @clear="fetchData"
        @keyup.enter="fetchData"
      >
        <template #append>
          <el-button @click="fetchData"><el-icon><Search /></el-icon></el-button>
        </template>
      </el-input>
      <el-button type="primary" @click="handleAdd()" v-permission="'dept:add'">
        <el-icon><Plus /></el-icon> 新增部门
      </el-button>
    </div>

    <el-table
      v-loading="loading"
      :data="deptList"
      style="width: 100%"
      row-key="uid"
      border
      default-expand-all
      :tree-props="{ children: 'children', hasChildren: 'hasChildren' }"
    >
      <el-table-column prop="fName" label="部门名称" min-width="200" />
      <el-table-column prop="fNumber" label="部门代码" width="150" />
      <el-table-column prop="fFullName" label="全称" width="200" />
      <el-table-column prop="fManager" label="负责人" width="120" />
      <el-table-column prop="fIsLine" label="产线" width="80" align="center">
        <template #default="scope">
          <el-tag :type="scope.row.fIsLine ? 'success' : 'info'" size="small">
            {{ scope.row.fIsLine ? '是' : '否' }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column label="创建时间" width="180">
        <template #default="scope">
          {{ formatDate(scope.row.cYmd) }}
        </template>
      </el-table-column>
      <el-table-column label="操作" width="250" fixed="right">
        <template #default="scope">
          <el-button size="small" @click="handleEdit(scope.row)" v-permission="'dept:edit'">编辑</el-button>
          <el-button size="small" type="primary" plain @click="handleAdd(scope.row)" v-permission="'dept:add'">新增下级</el-button>
          <el-button
            size="small"
            type="danger"
            @click="handleDelete(scope.row)"
            v-permission="'dept:delete'"
          >删除</el-button>
        </template>
      </el-table-column>
    </el-table>

    <!-- Dialog -->
    <el-dialog
      v-model="dialogVisible"
      :title="dialogType === 'create' ? '新增部门' : '编辑部门'"
      width="600px"
    >
      <el-form :model="form" label-width="100px">
        <el-form-item label="上级部门">
          <el-tree-select
            v-model="form.fParentId"
            :data="deptTree"
            :props="{ label: 'fName', children: 'children', value: 'uid' }"
            check-strictly
            placeholder="选择上级部门"
            clearable
            style="width: 100%"
          />
        </el-form-item>
        <el-form-item label="部门代码" required>
          <el-input v-model="form.fNumber" :disabled="dialogType === 'edit'" />
        </el-form-item>
        <el-form-item label="部门名称" required>
          <el-input v-model="form.fName" />
        </el-form-item>
        <el-form-item label="全称">
          <el-input v-model="form.fFullName" />
        </el-form-item>
        <el-form-item label="负责人">
          <el-input v-model="form.fManager" />
        </el-form-item>
        <el-form-item label="描述">
          <el-input v-model="form.fDescription" type="textarea" />
        </el-form-item>
        <el-form-item label="助记码">
          <el-input v-model="form.fHelpCode" />
        </el-form-item>
        <el-form-item label="部门属性">
          <el-input v-model="form.fDeptProperty" />
        </el-form-item>
        <el-form-item label="是否产线">
          <el-switch v-model="form.fIsLine" />
        </el-form-item>
        <el-form-item label="成本核算">
          <el-switch v-model="form.fCostAccountType" />
        </el-form-item>
        <el-form-item label="生效日期">
          <el-date-picker v-model="form.fEffectDate" type="date" placeholder="选择生效日期" style="width: 100%" />
        </el-form-item>
        <el-form-item label="失效日期">
          <el-date-picker v-model="form.fLapseDate" type="date" placeholder="选择失效日期" style="width: 100%" />
        </el-form-item>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="dialogVisible = false">取消</el-button>
          <el-button type="primary" @click="submitForm">确定</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { getDeptTree, getDept, createDept, updateDept, deleteDept, type Department } from '../../api/dept'
import { formatDate } from '../../utils/format'
import { ElMessage, ElMessageBox } from 'element-plus'
import { Search, Plus } from '@element-plus/icons-vue'

const loading = ref(false)
const deptList = ref<Department[]>([])
const deptTree = ref<Department[]>([])

const queryParams = reactive({
  keyword: ''
})

const dialogVisible = ref(false)
const dialogType = ref<'create' | 'edit'>('create')
const form = reactive({
  uid: undefined as string | undefined,
  fNumber: '',
  fName: '',
  fFullName: '',
  fParentId: '' as string | undefined,
  fManager: '',
  fDescription: '',
  fHelpCode: '',
  fIsLine: false,
  fDeptProperty: '',
  fCostAccountType: false,
  fEffectDate: undefined as string | undefined,
  fLapseDate: undefined as string | undefined
})

const fetchData = async () => {
  loading.value = true
  try {
    const res: any = await getDeptTree(queryParams)
    deptList.value = res.data
    deptTree.value = res.data
  } catch (error) {
    console.error('Fetch depts failed:', error)
  } finally {
    loading.value = false
  }
}

const handleAdd = (row?: Department) => {
  dialogType.value = 'create'
  Object.assign(form, {
    uid: undefined,
    fNumber: '',
    fName: '',
    fFullName: '',
    fParentId: row ? row.uid : undefined,
    fManager: '',
    fDescription: '',
    fHelpCode: '',
    fIsLine: false,
    fDeptProperty: '',
    fCostAccountType: false,
    fEffectDate: undefined,
    fLapseDate: undefined
  })
  dialogVisible.value = true
}

const handleEdit = async (row: Department) => {
  dialogType.value = 'edit'
  try {
    const res: any = await getDept(row.uid!)
    const detail = res.data
    Object.assign(form, {
      uid: detail.uid,
      fNumber: detail.fNumber,
      fName: detail.fName,
      fFullName: detail.fFullName || '',
      fParentId: detail.fParentId || undefined,
      fManager: detail.fManager || '',
      fDescription: detail.fDescription || '',
      fHelpCode: detail.fHelpCode || '',
      fIsLine: detail.fIsLine || false,
      fDeptProperty: detail.fDeptProperty || '',
      fCostAccountType: detail.fCostAccountType || false,
      fEffectDate: detail.fEffectDate || undefined,
      fLapseDate: detail.fLapseDate || undefined
    })
  } catch (error) {
    console.error('Fetch dept detail failed:', error)
    Object.assign(form, {
      uid: row.uid,
      fNumber: row.fNumber,
      fName: row.fName,
      fFullName: row.fFullName || '',
      fParentId: row.fParentId || undefined,
      fManager: row.fManager || '',
      fDescription: row.fDescription || '',
      fHelpCode: row.fHelpCode || '',
      fIsLine: row.fIsLine || false,
      fDeptProperty: row.fDeptProperty || '',
      fCostAccountType: row.fCostAccountType || false,
      fEffectDate: row.fEffectDate || undefined,
      fLapseDate: row.fLapseDate || undefined
    })
  }
  dialogVisible.value = true
}

const handleDelete = (row: Department) => {
  ElMessageBox.confirm('确定要删除该部门吗?', '警告', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    await deleteDept(row.uid!)
    ElMessage.success('删除成功')
    fetchData()
  })
}

const submitForm = async () => {
  if (!form.fName || !form.fNumber) {
    ElMessage.warning('请输入部门名称和代码')
    return
  }

  try {
    if (dialogType.value === 'create') {
      await createDept({
        fNumber: form.fNumber,
        fName: form.fName,
        fFullName: form.fFullName,
        fParentId: form.fParentId || '',
        fManager: form.fManager,
        fDescription: form.fDescription,
        fHelpCode: form.fHelpCode,
        fIsLine: form.fIsLine,
        fDeptProperty: form.fDeptProperty,
        fCostAccountType: form.fCostAccountType,
        fEffectDate: form.fEffectDate,
        fLapseDate: form.fLapseDate
      })
      ElMessage.success('创建成功')
    } else {
      await updateDept(form.uid!, {
        fName: form.fName,
        fFullName: form.fFullName,
        fParentId: form.fParentId || '',
        fManager: form.fManager,
        fDescription: form.fDescription,
        fHelpCode: form.fHelpCode,
        fIsLine: form.fIsLine,
        fDeptProperty: form.fDeptProperty,
        fCostAccountType: form.fCostAccountType,
        fEffectDate: form.fEffectDate,
        fLapseDate: form.fLapseDate
      })
      ElMessage.success('更新成功')
    }
    dialogVisible.value = false
    fetchData()
  } catch (error: any) {
    console.error('Submit dept failed:', error)
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
.dept-list-container {
  padding: 20px;
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
}

.header-actions {
  display: flex;
  justify-content: space-between;
  margin-bottom: 20px;
}

.search-input {
  width: 300px;
}
</style>
