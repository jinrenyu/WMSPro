<template>
  <el-select
    v-model="selectedValue"
    :placeholder="placeholder"
    :disabled="disabled"
    :clearable="clearable"
    filterable
    remote
    :remote-method="debouncedSearch"
    :loading="loading"
    style="width: 100%"
    @focus="handleFocus"
    @change="handleChange"
    @clear="handleClear"
  >
    <el-option
      v-for="item in displayOptions"
      :key="item.uid"
      :label="`${item.fNumber} - ${item.fName}`"
      :value="item.uid"
    />
    <template #empty>
      <p style="text-align: center; color: #999; padding: 10px 0; margin: 0;">
        {{ loading ? '加载中...' : emptyText }}
      </p>
    </template>
  </el-select>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import { getLookup, type LookupItem } from '../api/lookup'

const props = withDefaults(defineProps<{
  modelValue: string
  module: string
  parentId?: string
  placeholder?: string
  disabled?: boolean
  clearable?: boolean
  /** 是否在聚焦时预加载全部数据（适用于数据量小的基础资料，如单位、币别） */
  preload?: boolean
}>(), {
  placeholder: '请选择',
  disabled: false,
  clearable: true,
  preload: false
})

const emit = defineEmits<{
  'update:modelValue': [value: string]
  'change': [item: LookupItem | null]
}>()

const selectedValue = ref(props.modelValue)
const options = ref<LookupItem[]>([])
const selectedItem = ref<LookupItem | null>(null)
const loading = ref(false)
const hasSearched = ref(false)
let debounceTimer: ReturnType<typeof setTimeout> | null = null

// Merge selected item into options for display (ensures edit mode shows the current value)
const displayOptions = computed(() => {
  if (selectedItem.value && !options.value.find(o => o.uid === selectedItem.value!.uid)) {
    return [selectedItem.value, ...options.value]
  }
  return options.value
})

const emptyText = computed(() => {
  if (hasSearched.value) return '无匹配数据'
  return '请输入关键字搜索'
})

// Sync external value changes
watch(() => props.modelValue, (val) => {
  selectedValue.value = val
  if (val) {
    resolveInitialValue(val)
  } else {
    selectedItem.value = null
  }
})

// Reset when parentId changes
watch(() => props.parentId, () => {
  options.value = []
  hasSearched.value = false
  if (selectedValue.value) {
    selectedValue.value = ''
    selectedItem.value = null
    emit('update:modelValue', '')
    emit('change', null)
  }
})

// Resolve the display label for an existing ID (edit mode)
const resolveInitialValue = async (uid: string) => {
  if (!uid) return
  // Already in options or selectedItem
  if (selectedItem.value?.uid === uid) return
  if (options.value.find(o => o.uid === uid)) {
    selectedItem.value = options.value.find(o => o.uid === uid)!
    return
  }
  // Fetch from API to resolve the label
  try {
    const res: any = await getLookup(props.module, {
      keyword: undefined,
      parentId: props.parentId || undefined,
      maxCount: 200
    })
    const items: LookupItem[] = res.data || []
    const found = items.find(i => i.uid === uid)
    if (found) {
      selectedItem.value = found
      // Cache the results
      options.value = items
      hasSearched.value = true
    }
  } catch (e) {
    console.error(`Resolve ${props.module} lookup value failed:`, e)
  }
}

const fetchOptions = async (keyword?: string) => {
  loading.value = true
  try {
    const res: any = await getLookup(props.module, {
      keyword: keyword || undefined,
      parentId: props.parentId || undefined,
      maxCount: 50
    })
    options.value = res.data || []
    hasSearched.value = true
  } catch (e) {
    console.error(`Fetch ${props.module} lookup failed:`, e)
  } finally {
    loading.value = false
  }
}

const handleFocus = () => {
  // For small datasets, preload on focus
  if (props.preload && !hasSearched.value) {
    fetchOptions()
  }
}

const debouncedSearch = (keyword: string) => {
  if (debounceTimer) clearTimeout(debounceTimer)
  if (!keyword) {
    // When keyword is cleared, show preloaded data or reset
    if (props.preload) {
      fetchOptions()
    } else {
      options.value = selectedItem.value ? [selectedItem.value] : []
      hasSearched.value = false
    }
    return
  }
  debounceTimer = setTimeout(() => {
    fetchOptions(keyword)
  }, 300)
}

const handleChange = (val: string) => {
  emit('update:modelValue', val || '')
  const item = displayOptions.value.find(o => o.uid === val) || null
  selectedItem.value = item
  emit('change', item)
}

const handleClear = () => {
  selectedItem.value = null
  options.value = []
  hasSearched.value = false
  emit('update:modelValue', '')
  emit('change', null)
}

// Resolve initial value on mount
if (props.modelValue) {
  resolveInitialValue(props.modelValue)
}
</script>
