<template>
  <div class="material-lookup">
    <el-input
      :model-value="innerDisplay"
      :placeholder="placeholder"
      :disabled="disabled"
      readonly
      @click="openDialog"
    >
      <template #suffix>
        <el-icon v-if="innerDisplay && !disabled" class="ml-ico ml-clear" @click.stop="handleClear"><CircleClose /></el-icon>
        <el-icon class="ml-ico" @click.stop="openDialog"><Search /></el-icon>
      </template>
    </el-input>

    <!-- 直接复用“物料管理”列表页作为选择器：自带列设置 / 高级筛选 / 分组 / 全字段 -->
    <el-dialog
      v-model="dialogVisible"
      title="选择物料（双击行选择）"
      width="80%"
      top="6vh"
      append-to-body
      destroy-on-close
      class="material-lookup-dialog"
    >
      <MaterialList select-mode :only-approved="true" @select="pick" />
    </el-dialog>
  </div>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { Search, CircleClose } from '@element-plus/icons-vue'
import MaterialList from '../views/master/MaterialList.vue'

const props = withDefaults(defineProps<{
  modelValue: string
  /** 已选物料的显示文本（一般传物料代码，编辑回填用） */
  displayText?: string
  disabled?: boolean
  placeholder?: string
}>(), {
  displayText: '',
  disabled: false,
  placeholder: '点击选择物料'
})

const emit = defineEmits<{
  'update:modelValue': [value: string]
  'change': [material: any | null]
}>()

const innerDisplay = ref(props.displayText || '')
watch(() => props.displayText, (v) => { innerDisplay.value = v || '' })
watch(() => props.modelValue, (v) => { if (!v) innerDisplay.value = props.displayText || '' })

const dialogVisible = ref(false)
const openDialog = () => { if (!props.disabled) dialogVisible.value = true }

const pick = (row: any) => {
  if (!row) return
  innerDisplay.value = row.fNumber || ''
  emit('update:modelValue', row.uid)
  emit('change', row)
  dialogVisible.value = false
}

const handleClear = () => {
  innerDisplay.value = ''
  emit('update:modelValue', '')
  emit('change', null)
}
</script>

<style scoped>
.material-lookup { width: 100%; }
.ml-ico { cursor: pointer; }
.ml-clear { margin-right: 4px; color: var(--el-text-color-placeholder); }
.ml-clear:hover { color: var(--el-color-danger); }
</style>

<style>
.material-lookup-dialog .el-dialog__body { padding-top: 10px; }
</style>
