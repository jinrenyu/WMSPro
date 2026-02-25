<template>
  <el-popover placement="bottom-end" :width="360" trigger="click">
    <template #reference>
      <el-button :icon="Setting" circle size="small" title="列设置" />
    </template>
    <div class="column-setting">
      <div class="column-setting-header">
        <el-checkbox
          v-model="checkAll"
          :indeterminate="indeterminate"
          @change="handleCheckAll"
        >全选</el-checkbox>
        <el-button link type="primary" size="small" @click="handleReset">重置</el-button>
      </div>
      <el-divider style="margin: 8px 0" />
      <div class="column-setting-list">
        <div
          v-for="col in configurableColumns"
          :key="col.key"
          class="column-setting-item"
        >
          <el-checkbox
            :model-value="isColumnVisible(col)"
            @change="(val: any) => toggleColumn(col.key, !!val)"
          >{{ col.label }}</el-checkbox>
        </div>
      </div>
    </div>
  </el-popover>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { Setting } from '@element-plus/icons-vue'
import type { ColumnDef } from '../composables/useColumnConfig'

const props = defineProps<{
  configurableColumns: ColumnDef[]
  visibleKeys: string[]
  isColumnVisible: (col: ColumnDef) => boolean
  toggleColumn: (key: string, visible: boolean) => void
  resetColumns: () => void
}>()

const checkAll = computed({
  get: () => props.configurableColumns.every(c => props.visibleKeys.includes(c.key)),
  set: () => {}
})

const indeterminate = computed(() => {
  const count = props.configurableColumns.filter(c => props.visibleKeys.includes(c.key)).length
  return count > 0 && count < props.configurableColumns.length
})

const handleCheckAll = (val: any) => {
  props.configurableColumns.forEach(c => {
    props.toggleColumn(c.key, !!val)
  })
}

const handleReset = () => {
  props.resetColumns()
}
</script>

<style scoped>
.column-setting-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.column-setting-list {
  max-height: 300px;
  overflow-y: auto;
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 0 8px;
}
.column-setting-item {
  padding: 4px 0;
}
</style>
