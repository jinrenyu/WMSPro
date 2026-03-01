<template>
  <el-popover
    placement="bottom"
    :width="500"
    trigger="click"
    v-model:visible="visible"
  >
    <template #reference>
      <el-button>
        <el-icon><Filter /></el-icon>
        高级筛选
        <el-badge v-if="filters.length > 0" :value="filters.length" class="filter-badge" type="primary" />
      </el-button>
    </template>

    <div class="dynamic-filter-container">
      <div class="filter-header">
        <span>高级筛选条件</span>
        <el-button type="primary" link @click="addFilter">
          <el-icon><Plus /></el-icon>添加条件
        </el-button>
      </div>

      <div class="filter-body">
        <template v-if="filters.length === 0">
          <el-empty description="暂无筛选条件，点击右上角添加" :image-size="60" />
        </template>
        
        <div v-for="(filter, index) in filters" :key="index" class="filter-row">
          <!-- 1. 选择字段 -->
          <el-select
            v-model="filter.field"
            placeholder="选择字段"
            class="filter-field"
            :teleported="false"
            @change="(val: string) => handleFieldChange(index, val)"
          >
            <el-option
              v-for="col in columns"
              :key="col.prop || col.key"
              :label="col.label"
              :value="col.prop || col.key"
            />
          </el-select>

          <!-- 2. 选择操作符 -->
          <el-select v-model="filter.operator" placeholder="操作符" class="filter-operator" :teleported="false">
            <el-option
              v-for="op in getOperators(filter.field)"
              :key="op.value"
              :label="op.label"
              :value="op.value"
            />
          </el-select>

          <!-- 3. 输入值 / 选择值 -->
          <div class="filter-value" @click.stop>
            <!-- 针对布尔类型，预置已禁用/审核状态等特性 -->
            <template v-if="isBooleanField(filter.field)">
              <el-select v-model="filter.value" placeholder="选择值" :teleported="false">
                <el-option label="是" value="true" />
                <el-option label="否" value="false" />
              </el-select>
            </template>
            <!-- 针对特定的状态枚举 (通用：审批状态) -->
            <template v-else-if="filter.field === 'fStatus'">
              <el-select v-model="filter.value" placeholder="选择状态" :teleported="false">
                <el-option label="未审核" value="0,10" />
                <el-option label="已审核" value="40" />
              </el-select>
            </template>
            <!-- 针对日期类型，使用日期选择器 -->
            <template v-else-if="isDateField(filter.field)">
              <el-date-picker
                v-model="filter.value"
                type="date"
                placeholder="选择日期"
                format="YYYY-MM-DD"
                value-format="YYYY-MM-DD"
                :teleported="false"
                style="width: 100%"
              />
            </template>
            <!-- 默认文本输入 -->
            <template v-else>
              <el-input v-model="filter.value" placeholder="输入值" />
            </template>
          </div>

          <!-- 移除该行 -->
          <el-button type="danger" link @click="removeFilter(index)" class="remove-btn">
            <el-icon><Delete /></el-icon>
          </el-button>
        </div>
      </div>

      <div class="filter-footer">
        <el-button @click="clearFilters">清空</el-button>
        <el-button type="primary" @click="applyFilters">应用</el-button>
      </div>
    </div>
  </el-popover>
</template>

<script setup lang="ts">
import { ref, watch, computed } from 'vue'
import { Filter, Plus, Delete } from '@element-plus/icons-vue'

export interface ColumnDef {
  key: string
  label: string
  prop?: string
  slotName?: string
  width?: number | string
  minWidth?: number | string
  align?: 'left' | 'center' | 'right'
  fixed?: string | boolean
  defaultVisible?: boolean
  hideable?: boolean
}

export interface DynamicFilterInfo {
  field: string
  operator: string
  value: string
}

const props = defineProps<{
  columns: ColumnDef[]
  modelValue: DynamicFilterInfo[]
  apiFieldsFunc?: () => Promise<any>
}>()

const emit = defineEmits<{
  (e: 'update:modelValue', value: DynamicFilterInfo[]): void
  (e: 'change', value: DynamicFilterInfo[]): void
}>()

const visible = ref(false)
const filters = ref<DynamicFilterInfo[]>([])

interface FieldMeta {
  field: string
  dataType: string
}
const fieldMetaList = ref<FieldMeta[]>([])

// 初始化时从 props 同步
watch(() => props.modelValue, (newVal) => {
  if (JSON.stringify(newVal) !== JSON.stringify(filters.value)) {
    filters.value = JSON.parse(JSON.stringify(newVal || []))
  }
}, { immediate: true, deep: true })

import { onMounted } from 'vue'

onMounted(async () => {
  if (props.apiFieldsFunc) {
    try {
      const res = await props.apiFieldsFunc()
      if (res && res.data) {
        fieldMetaList.value = res.data
      }
    } catch (e) {
      console.error('Failed to fetch field metadata', e)
    }
  }
})

const addFilter = () => {
  filters.value.push({ field: '', operator: 'eq', value: '' })
}

const removeFilter = (index: number) => {
  filters.value.splice(index, 1)
}

const clearFilters = () => {
  filters.value = []
  applyFilters()
}

const applyFilters = () => {
  // 过滤掉没选字段或者没填值（非空校验可根据业务调整）的条件
  const validFilters = filters.value.filter(f => f.field)
  emit('update:modelValue', validFilters)
  emit('change', validFilters)
  visible.value = false
}

const getFieldConfig = (fieldValue: string) => {
  return props.columns.find(c => (c.prop || c.key) === fieldValue)
}

const getFieldMeta = (fieldValue: string) => {
  return fieldMetaList.value.find(f => f.field.toLowerCase() === fieldValue.toLowerCase())
}

const isBooleanField = (fieldValue: string) => {
  const meta = getFieldMeta(fieldValue)
  if (meta) return meta.dataType === 'bool' || meta.dataType === 'boolean'
  
  const cfg = getFieldConfig(fieldValue)
  if (!cfg) return false
  // 通过自定义的slotName等特征来简单推断
  return cfg.slotName === 'boolTag' || cfg.slotName === 'disabled'
}

const isDateField = (fieldValue: string) => {
  if (!fieldValue) return false
  const meta = getFieldMeta(fieldValue)
  if (meta) return meta.dataType === 'datetime' || meta.dataType === 'date'

  const cfg = getFieldConfig(fieldValue)
  if (cfg && (cfg.slotName === 'createTime' || cfg.slotName === 'updateTime')) return true
  // 根据常见的日期字段名推断
  const lField = fieldValue.toLowerCase()
  return lField.includes('date') || lField.includes('time') || lField.includes('ymd')
}

const isNumericField = (fieldValue: string) => {
  if (!fieldValue) return false
  const meta = getFieldMeta(fieldValue)
  if (meta) {
    return ['int', 'long', 'short', 'decimal', 'double', 'float'].includes(meta.dataType.toLowerCase())
  }

  const lField = fieldValue.toLowerCase()
  return lField.includes('qty') || lField.includes('price') || lField.includes('amount') || lField.includes('min') || lField.includes('max') || lField.includes('volume') || lField.includes('weight')
}

const handleFieldChange = (index: number, newField: string) => {
  const filter = filters.value[index]
  filter.value = '' // 清空之前选的值
  if (isBooleanField(newField) || newField === 'fStatus') {
    filter.operator = 'eq' // 状态、布尔通常用相等
  } else if (isDateField(newField) || isNumericField(newField)) {
    filter.operator = 'eq' // 日期和数字通常用相等或大于小于
  } else {
    filter.operator = 'like' // 默认文本通常用包含
  }
}

const getOperators = (fieldValue: string) => {
  const ops = [
    { label: '等于', value: 'eq' },
    { label: '不等于', value: 'neq' }
  ]
  
  if (isBooleanField(fieldValue) || fieldValue === 'fStatus') {
    return ops
  }

  if (isDateField(fieldValue) || isNumericField(fieldValue)) {
    return [
      ...ops,
      { label: '大于', value: 'gt' },
      { label: '小于', value: 'lt' },
      { label: '大于等于', value: 'ge' },
      { label: '小于等于', value: 'le' }
    ]
  }

  return [
    ...ops,
    { label: '包含', value: 'like' },
    { label: '大于', value: 'gt' },
    { label: '小于', value: 'lt' },
    { label: '大于等于', value: 'ge' },
    { label: '小于等于', value: 'le' }
  ]
}
</script>

<style scoped>
.filter-badge {
  margin-left: 4px;
}

.dynamic-filter-container {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.filter-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  font-weight: bold;
  font-size: 14px;
  border-bottom: 1px solid var(--el-border-color-light);
  padding-bottom: 8px;
}

.filter-body {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.filter-row {
  display: flex;
  align-items: center;
  gap: 8px;
}

.filter-field {
  width: 140px;
  flex-shrink: 0;
}

.filter-operator {
  width: 110px;
  flex-shrink: 0;
}

.filter-value {
  flex: 1;
}

.remove-btn {
  margin-left: 4px;
}

.filter-footer {
  display: flex;
  justify-content: flex-end;
  border-top: 1px solid var(--el-border-color-light);
  padding-top: 12px;
  margin-top: 4px;
}
</style>
