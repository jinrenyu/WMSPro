<template>
  <div class="menu-list-container">
    <div class="header-actions">
      <el-input
        v-model="queryParams.keyword"
        placeholder="搜索菜单名称"
        class="search-input"
        clearable
        @clear="fetchData"
        @keyup.enter="fetchData"
      >
        <template #append>
          <el-button @click="fetchData"><el-icon><Search /></el-icon></el-button>
        </template>
      </el-input>
      <el-button type="primary" @click="handleAdd()" v-permission="'menu:add'">
        <el-icon><Plus /></el-icon> 新增菜单
      </el-button>
    </div>

    <el-table
      v-loading="loading"
      :data="filteredMenuList"
      style="width: 100%"
      row-key="uid"
      border
      default-expand-all
      :tree-props="{ children: 'children', hasChildren: 'hasChildren' }"
    >
      <el-table-column prop="menuName" label="菜单名称" min-width="200" />
      <el-table-column prop="icon" label="图标" width="80" align="center">
        <template #default="scope">
          <el-icon v-if="scope.row.icon"><component :is="scope.row.icon" /></el-icon>
        </template>
      </el-table-column>
      <el-table-column label="类型" width="90" align="center">
        <template #default="scope">
          <el-tag v-if="scope.row.menuType === 'D'" type="warning" size="small">目录</el-tag>
          <el-tag v-else-if="scope.row.menuType === 'M'" type="success" size="small">菜单</el-tag>
          <el-tag v-else-if="scope.row.menuType === 'B'" type="info" size="small">按钮</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="permCode" label="权限代码" width="160" />
      <el-table-column prop="routePath" label="路由路径" width="180" />
      <el-table-column prop="sortOrder" label="排序" width="80" align="center" />
      <el-table-column label="状态" width="80" align="center">
        <template #default="scope">
          <el-tag :type="scope.row.fStatus === 0 ? 'success' : 'danger'" size="small">
            {{ scope.row.fStatus === 0 ? '启用' : '禁用' }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column label="操作" width="280" fixed="right">
        <template #default="scope">
          <el-button size="small" @click="handleEdit(scope.row)" v-permission="'menu:edit'">编辑</el-button>
          <el-button
            v-if="scope.row.menuType !== 'B'"
            size="small"
            type="primary"
            plain
            @click="handleAdd(scope.row)"
            v-permission="'menu:add'"
          >新增下级</el-button>
          <el-button
            size="small"
            type="danger"
            @click="handleDelete(scope.row)"
            v-permission="'menu:delete'"
          >删除</el-button>
        </template>
      </el-table-column>
    </el-table>

    <!-- Dialog -->
    <el-dialog
      v-model="dialogVisible"
      :title="dialogType === 'create' ? '新增菜单' : '编辑菜单'"
      width="600px"
    >
      <el-form :model="form" label-width="100px">
        <el-form-item label="上级菜单">
          <el-tree-select
            v-model="form.parentId"
            :data="parentOptions"
            :props="{ label: 'menuName', children: 'children', value: 'uid' }"
            check-strictly
            placeholder="选择上级菜单（空为顶级）"
            clearable
            style="width: 100%"
          />
        </el-form-item>
        <el-form-item label="菜单类型" required>
          <el-radio-group v-model="form.menuType">
            <el-radio value="D">目录</el-radio>
            <el-radio value="M">菜单</el-radio>
            <el-radio value="B">按钮</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="菜单名称" required>
          <el-input v-model="form.menuName" placeholder="请输入菜单名称" />
        </el-form-item>
        <el-form-item label="图标" v-if="form.menuType !== 'B'">
          <el-input v-model="form.icon" placeholder="Element Plus 图标名称" />
        </el-form-item>
        <el-form-item label="路由路径" v-if="form.menuType === 'M'">
          <el-input v-model="form.routePath" placeholder="如 /system/users" />
        </el-form-item>
        <el-form-item label="权限代码" v-if="form.menuType === 'B'" required>
          <el-input v-model="form.permCode" placeholder="如 user:add" />
        </el-form-item>
        <el-form-item label="排序号">
          <el-input-number v-model="form.sortOrder" :min="0" :max="999" />
        </el-form-item>
        <el-form-item label="状态">
          <el-radio-group v-model="form.fStatus">
            <el-radio :value="0">启用</el-radio>
            <el-radio :value="1">禁用</el-radio>
          </el-radio-group>
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
import { ref, reactive, computed, onMounted } from 'vue'
import { getMenuTree, getMenu, createMenu, updateMenu, deleteMenu, type Menu } from '../../api/menu'
import { ElMessage, ElMessageBox } from 'element-plus'
import { Search, Plus } from '@element-plus/icons-vue'
import { useMenuStore } from '../../stores/menu'

const menuStore = useMenuStore()

const loading = ref(false)
const menuList = ref<Menu[]>([])

const queryParams = reactive({
  keyword: ''
})

// 根据关键字过滤树形数据
const filteredMenuList = computed(() => {
  if (!queryParams.keyword) return menuList.value
  return filterTree(menuList.value, queryParams.keyword)
})

const filterTree = (nodes: Menu[], keyword: string): Menu[] => {
  return nodes.reduce<Menu[]>((acc, node) => {
    const children = node.children ? filterTree(node.children, keyword) : []
    if (node.menuName.includes(keyword) || children.length > 0) {
      acc.push({ ...node, children: children.length > 0 ? children : node.children })
    }
    return acc
  }, [])
}

// 父级选项（排除按钮类型）
const parentOptions = computed(() => {
  return filterNonButtons(menuList.value)
})

const filterNonButtons = (nodes: Menu[]): Menu[] => {
  return nodes
    .filter(n => n.menuType !== 'B')
    .map(n => ({
      ...n,
      children: n.children ? filterNonButtons(n.children) : []
    }))
}

const dialogVisible = ref(false)
const dialogType = ref<'create' | 'edit'>('create')
const form = reactive({
  uid: undefined as string | undefined,
  parentId: '' as string | undefined,
  menuName: '',
  menuType: 'M',
  routePath: '',
  icon: '',
  permCode: '',
  sortOrder: 0,
  fStatus: 0
})

const resetForm = () => {
  Object.assign(form, {
    uid: undefined,
    parentId: undefined,
    menuName: '',
    menuType: 'M',
    routePath: '',
    icon: '',
    permCode: '',
    sortOrder: 0,
    fStatus: 0
  })
}

const fetchData = async () => {
  loading.value = true
  try {
    const res: any = await getMenuTree()
    menuList.value = res.data
    // 同步刷新侧边栏菜单
    await menuStore.loadMenus()
  } catch (error) {
    console.error('Fetch menus failed:', error)
  } finally {
    loading.value = false
  }
}

const handleAdd = (row?: Menu) => {
  dialogType.value = 'create'
  resetForm()
  if (row) {
    form.parentId = row.uid
    // 如果父级是菜单，默认新增按钮
    if (row.menuType === 'M') {
      form.menuType = 'B'
    } else {
      form.menuType = 'M'
    }
  }
  dialogVisible.value = true
}

const handleEdit = async (row: Menu) => {
  dialogType.value = 'edit'
  try {
    const res: any = await getMenu(row.uid!)
    const detail = res.data
    Object.assign(form, {
      uid: detail.uid,
      parentId: detail.parentId || undefined,
      menuName: detail.menuName,
      menuType: detail.menuType,
      routePath: detail.routePath || '',
      icon: detail.icon || '',
      permCode: detail.permCode || '',
      sortOrder: detail.sortOrder || 0,
      fStatus: detail.fStatus || 0
    })
  } catch (error) {
    console.error('Fetch menu detail failed:', error)
    Object.assign(form, {
      uid: row.uid,
      parentId: row.parentId || undefined,
      menuName: row.menuName,
      menuType: row.menuType,
      routePath: row.routePath || '',
      icon: row.icon || '',
      permCode: row.permCode || '',
      sortOrder: row.sortOrder || 0,
      fStatus: row.fStatus || 0
    })
  }
  dialogVisible.value = true
}

const handleDelete = (row: Menu) => {
  ElMessageBox.confirm('确定要删除该菜单吗？如有子节点将无法删除。', '警告', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await deleteMenu(row.uid!)
      ElMessage.success('删除成功')
      fetchData()
    } catch (error: any) {
      if (error.response && error.response.data) {
        ElMessage.error(error.response.data.message || '删除失败')
      } else {
        ElMessage.error('删除失败')
      }
    }
  })
}

const submitForm = async () => {
  if (!form.menuName) {
    ElMessage.warning('请输入菜单名称')
    return
  }
  if (!form.menuType) {
    ElMessage.warning('请选择菜单类型')
    return
  }
  if (form.menuType === 'B' && !form.permCode) {
    ElMessage.warning('按钮类型必须填写权限代码')
    return
  }

  const data = {
    parentId: form.parentId || '',
    menuName: form.menuName,
    menuType: form.menuType,
    routePath: form.routePath,
    icon: form.icon,
    permCode: form.permCode,
    sortOrder: form.sortOrder,
    fStatus: form.fStatus
  }

  try {
    if (dialogType.value === 'create') {
      await createMenu(data)
      ElMessage.success('创建成功')
    } else {
      await updateMenu(form.uid!, data)
      ElMessage.success('更新成功')
    }
    dialogVisible.value = false
    fetchData()
  } catch (error: any) {
    console.error('Submit menu failed:', error)
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
.menu-list-container {
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
