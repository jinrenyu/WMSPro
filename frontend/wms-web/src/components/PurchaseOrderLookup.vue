<template>
  <div class="po-lookup">
    <el-input
      :model-value="innerDisplay"
      :placeholder="placeholder"
      :disabled="disabled"
      readonly
      @click="openDialog"
    >
      <template #suffix>
        <el-icon v-if="innerDisplay && !disabled" class="po-ico po-clear" @click.stop="handleClear"><CircleClose /></el-icon>
        <el-icon class="po-ico" @click.stop="openDialog"><Search /></el-icon>
      </template>
    </el-input>

    <!-- 直接复用“采购订单”列表页作为选择器：自带列设置 / 高级筛选，仅展示已审核非禁用 -->
    <el-dialog
      v-model="dialogVisible"
      title="选择采购订单（双击行选择）"
      width="80%"
      top="6vh"
      append-to-body
      destroy-on-close
      class="po-lookup-dialog"
    >
      <PurchaseOrderList select-mode :only-approved="true" @select="pick" />
    </el-dialog>
  </div>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { Search, CircleClose } from '@element-plus/icons-vue'
import PurchaseOrderList from '../views/purchase/PurchaseOrderList.vue'

const props = withDefaults(defineProps<{
  /** 已选采购订单的单据编号（源单编号即存编号本身） */
  modelValue: string
  /** 显示文本（一般同 modelValue=单据编号，编辑回填用） */
  displayText?: string
  disabled?: boolean
  placeholder?: string
}>(), {
  displayText: '',
  disabled: false,
  placeholder: '点击选择采购订单'
})

const emit = defineEmits<{
  'update:modelValue': [value: string]
  'change': [order: any | null]
}>()

const innerDisplay = ref(props.displayText || props.modelValue || '')
watch(() => props.displayText, (v) => { if (v) innerDisplay.value = v })
watch(() => props.modelValue, (v) => { innerDisplay.value = v || props.displayText || '' })

const dialogVisible = ref(false)
const openDialog = () => { if (!props.disabled) dialogVisible.value = true }

const pick = (row: any) => {
  if (!row) return
  // 源单编号存的是采购订单的单据编号
  innerDisplay.value = row.fbillno || ''
  emit('update:modelValue', row.fbillno || '')
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
.po-lookup { width: 100%; }
.po-ico { cursor: pointer; }
.po-clear { margin-right: 4px; color: var(--el-text-color-placeholder); }
.po-clear:hover { color: var(--el-color-danger); }
</style>

<style>
.po-lookup-dialog .el-dialog__body { padding-top: 10px; }
</style>
