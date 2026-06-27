<template>
  <div class="mo-lookup">
    <el-input
      :model-value="innerDisplay"
      :placeholder="placeholder"
      :disabled="disabled"
      readonly
      @click="openDialog"
    >
      <template #suffix>
        <el-icon v-if="innerDisplay && !disabled" class="mo-ico mo-clear" @click.stop="handleClear"><CircleClose /></el-icon>
        <el-icon class="mo-ico" @click.stop="openDialog"><Search /></el-icon>
      </template>
    </el-input>

    <!-- 复用“生产订单”列表页作为选择器（按明细行展开，仅已审核），双击行选择 -->
    <el-dialog
      v-model="dialogVisible"
      title="选择生产订单（双击行选择）"
      width="80%"
      top="6vh"
      append-to-body
      destroy-on-close
      class="mo-lookup-dialog"
    >
      <ProductionOrderList select-mode :only-approved="true" @select="pick" />
    </el-dialog>
  </div>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { Search, CircleClose } from '@element-plus/icons-vue'
import ProductionOrderList from '../views/production/ProductionOrderList.vue'

const props = withDefaults(defineProps<{
  modelValue: string
  displayText?: string
  disabled?: boolean
  placeholder?: string
}>(), {
  displayText: '',
  disabled: false,
  placeholder: '点击选择生产订单'
})

const emit = defineEmits<{
  'update:modelValue': [value: string]
  'change': [row: any | null]
}>()

const innerDisplay = ref(props.displayText || '')
watch(() => props.displayText, (v) => { innerDisplay.value = v || '' })
watch(() => props.modelValue, (v) => { if (!v) innerDisplay.value = props.displayText || '' })

const dialogVisible = ref(false)
const openDialog = () => { if (!props.disabled) dialogVisible.value = true }

const pick = (row: any) => {
  if (!row) return
  innerDisplay.value = row.fbillno || ''
  emit('update:modelValue', row.entryUid || row.uid || '')
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
.mo-lookup { width: 100%; }
.mo-ico { cursor: pointer; }
.mo-clear { margin-right: 4px; color: var(--el-text-color-placeholder); }
.mo-clear:hover { color: var(--el-color-danger); }
</style>

<style>
.mo-lookup-dialog .el-dialog__body { padding-top: 10px; }
</style>