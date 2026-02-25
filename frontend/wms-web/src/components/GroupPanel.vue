<template>
  <!-- 分组树面板 -->
  <transition name="slide-group">
    <div v-show="visible" class="group-panel">
      <div class="group-panel-header">
        <span>{{ title }}</span>
        <el-button size="small" link @click="visible = false">
          <el-icon><DArrowLeft /></el-icon>
        </el-button>
      </div>
      <div class="group-tree-wrapper">
        <el-tree
          ref="treeRef"
          :data="treeData"
          :props="{ label: 'fName', children: 'children' }"
          node-key="uid"
          highlight-current
          default-expand-all
          @node-click="handleNodeClick"
          @node-contextmenu="handleContextMenu"
        >
          <template #default="{ data }">
            <span class="group-tree-node">
              <span>{{ data.fName }}</span>
            </span>
          </template>
        </el-tree>
      </div>
      <div class="group-panel-footer">
        <el-button size="small" type="primary" link @click="handleAdd()">
          <el-icon><Plus /></el-icon> 新增分组
        </el-button>
      </div>
    </div>
  </transition>

  <!-- 右键菜单 -->
  <div
    v-show="contextMenu.visible"
    class="group-context-menu"
    :style="{ left: contextMenu.x + 'px', top: contextMenu.y + 'px' }"
  >
    <div class="context-menu-item" @click="handleAdd(contextMenu.data)">新增下级</div>
    <div class="context-menu-item" @click="handleEdit(contextMenu.data)">编辑</div>
    <div class="context-menu-item danger" @click="handleDelete(contextMenu.data)">删除</div>
  </div>

  <!-- 分组 CRUD 弹窗 -->
  <el-dialog
    v-model="dialogVisible"
    :title="dialogType === 'create' ? '新增分组' : '编辑分组'"
    width="500px"
  >
    <el-form :model="form" label-width="80px">
      <el-form-item label="编码" required>
        <el-input v-model="form.fNumber" />
      </el-form-item>
      <el-form-item label="名称" required>
        <el-input v-model="form.fName" />
      </el-form-item>
      <el-form-item label="上级分组">
        <el-tree-select
          v-model="form.fParentId"
          :data="treeData"
          :props="{ label: 'fName', children: 'children', value: 'uid' }"
          placeholder="无（顶级分组）"
          clearable
          check-strictly
          style="width: 100%"
        />
      </el-form-item>
      <el-form-item label="备注">
        <el-input v-model="form.fNote" type="textarea" :rows="2" />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="dialogVisible = false">取消</el-button>
      <el-button type="primary" @click="submitForm">确定</el-button>
    </template>
  </el-dialog>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted, onBeforeUnmount } from 'vue'
import { getGroupTree, createGroup, updateGroup, deleteGroup, buildTree, type BaseDataGroup } from '../api/baseDataGroup'
import { ElMessage, ElMessageBox } from 'element-plus'
import { Plus, DArrowLeft } from '@element-plus/icons-vue'

const props = withDefaults(defineProps<{
  prgKey: string
  title?: string
}>(), {
  title: '分组'
})

const emit = defineEmits<{
  select: [groupId: string]
}>()

const visible = ref(false)
const treeRef = ref()
const treeData = ref<BaseDataGroup[]>([])

// --- 树数据 ---
const fetchTree = async () => {
  try {
    const res: any = await getGroupTree(props.prgKey)
    const list = res.data || []
    treeData.value = [
      { uid: '', fNumber: '', fName: '全部', children: buildTree(list) }
    ]
  } catch (error) {
    console.error('Fetch group tree failed:', error)
  }
}

const handleNodeClick = (data: BaseDataGroup) => {
  contextMenu.visible = false
  emit('select', data.uid || '')
}

// --- 右键菜单 ---
const contextMenu = reactive({
  visible: false,
  x: 0,
  y: 0,
  data: null as BaseDataGroup | null
})

const handleContextMenu = (event: MouseEvent, data: BaseDataGroup) => {
  if (!data.uid) return
  event.preventDefault()
  contextMenu.visible = true
  contextMenu.x = event.clientX
  contextMenu.y = event.clientY
  contextMenu.data = data
}

const closeContextMenu = () => {
  contextMenu.visible = false
}

// --- CRUD 弹窗 ---
const dialogVisible = ref(false)
const dialogType = ref<'create' | 'edit'>('create')
const form = reactive({
  uid: undefined as string | undefined,
  fNumber: '',
  fName: '',
  fParentId: '' as string | undefined,
  fNote: ''
})

const handleAdd = (parent?: BaseDataGroup | null) => {
  contextMenu.visible = false
  dialogType.value = 'create'
  form.uid = undefined
  form.fNumber = ''
  form.fName = ''
  form.fParentId = parent?.uid || undefined
  form.fNote = ''
  dialogVisible.value = true
}

const handleEdit = (data: BaseDataGroup | null) => {
  if (!data) return
  contextMenu.visible = false
  dialogType.value = 'edit'
  form.uid = data.uid
  form.fNumber = data.fNumber
  form.fName = data.fName
  form.fParentId = data.fParentId || undefined
  form.fNote = data.fNote || ''
  dialogVisible.value = true
}

const handleDelete = (data: BaseDataGroup | null) => {
  if (!data?.uid) return
  contextMenu.visible = false
  ElMessageBox.confirm('确定要删除该分组吗?', '警告', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    await deleteGroup(props.prgKey, data.uid!)
    ElMessage.success('删除成功')
    fetchTree()
  }).catch(() => {})
}

const submitForm = async () => {
  if (!form.fNumber || !form.fName) {
    ElMessage.warning('请输入编码和名称')
    return
  }
  try {
    const payload = {
      fNumber: form.fNumber,
      fName: form.fName,
      fParentId: form.fParentId || undefined,
      fNote: form.fNote
    }
    if (dialogType.value === 'create') {
      await createGroup(props.prgKey, payload)
      ElMessage.success('创建成功')
    } else {
      await updateGroup(props.prgKey, form.uid!, payload)
      ElMessage.success('更新成功')
    }
    dialogVisible.value = false
    fetchTree()
  } catch (error: any) {
    console.error('Submit group failed:', error)
    if (error.response?.data) {
      ElMessage.error(error.response.data.message || '提交失败')
    } else {
      ElMessage.error('提交失败')
    }
  }
}

// 暴露给父组件
defineExpose({ treeData, fetchTree, visible })

onMounted(() => {
  fetchTree()
  document.addEventListener('click', closeContextMenu)
})

onBeforeUnmount(() => {
  document.removeEventListener('click', closeContextMenu)
})
</script>

<style scoped>
.group-panel {
  width: 220px;
  min-width: 220px;
  border: 1px solid var(--el-border-color-light, #e4e7ed);
  border-radius: 4px;
  display: flex;
  flex-direction: column;
}

.group-panel-header {
  padding: 12px 16px;
  font-weight: 600;
  border-bottom: 1px solid var(--el-border-color-light, #e4e7ed);
  background-color: var(--el-fill-color-light, #f5f7fa);
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.group-tree-wrapper {
  flex: 1;
  overflow-y: auto;
  padding: 8px 0;
  min-height: 200px;
  max-height: calc(100vh - 280px);
}

.group-tree-node {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: space-between;
  font-size: 14px;
  padding-right: 8px;
}

.group-panel-footer {
  padding: 8px 16px;
  border-top: 1px solid var(--el-border-color-light, #e4e7ed);
}

.group-context-menu {
  position: fixed;
  z-index: 3000;
  background: #fff;
  border: 1px solid var(--el-border-color-light, #e4e7ed);
  border-radius: 4px;
  box-shadow: 0 2px 12px rgba(0, 0, 0, 0.1);
  padding: 4px 0;
}

.context-menu-item {
  padding: 8px 16px;
  font-size: 14px;
  cursor: pointer;
  white-space: nowrap;
}

.context-menu-item:hover {
  background-color: var(--el-fill-color-light, #f5f7fa);
}

.context-menu-item.danger {
  color: var(--el-color-danger, #f56c6c);
}

/* 展开/收起动画 */
.slide-group-enter-active,
.slide-group-leave-active {
  transition: all 0.3s ease;
  overflow: hidden;
}

.slide-group-enter-from,
.slide-group-leave-to {
  width: 0 !important;
  min-width: 0 !important;
  opacity: 0;
  margin-right: 0;
  padding: 0;
  border: none;
}
</style>
