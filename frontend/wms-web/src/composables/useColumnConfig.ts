import { ref, computed } from 'vue'

export interface ColumnDef {
  /** 列的唯一标识，通常是 prop */
  key: string
  /** 列标题 */
  label: string
  /** 绑定的数据字段 */
  prop?: string
  /** 固定宽度 */
  width?: number | string
  /** 最小宽度 */
  minWidth?: number | string
  /** 对齐方式 */
  align?: 'left' | 'center' | 'right'
  /** 是否固定列 */
  fixed?: string | boolean
  /** 是否默认显示 */
  defaultVisible?: boolean
  /** 是否允许隐藏（如操作列不允许隐藏） */
  hideable?: boolean
  /** 自定义渲染插槽名 */
  slotName?: string
}

const STORAGE_PREFIX = 'col_config_'

export function useColumnConfig(pageKey: string, allColumns: ColumnDef[]) {
  const storageKey = STORAGE_PREFIX + pageKey

  // 从 localStorage 读取用户配置
  const loadConfig = (): string[] | null => {
    try {
      const raw = localStorage.getItem(storageKey)
      if (raw) return JSON.parse(raw)
    } catch { /* ignore */ }
    return null
  }

  // 默认可见的列 key 列表
  const defaultVisibleKeys = allColumns
    .filter(c => c.defaultVisible !== false)
    .map(c => c.key)

  // 当前可见的列 key 列表
  const visibleKeys = ref<string[]>(loadConfig() ?? [...defaultVisibleKeys])

  // 持久化
  const saveConfig = () => {
    localStorage.setItem(storageKey, JSON.stringify(visibleKeys.value))
  }

  // 可配置的列（hideable !== false 的列）
  const configurableColumns = computed(() =>
    allColumns.filter(c => c.hideable !== false)
  )

  const isColumnVisible = (col: ColumnDef) => {
    if (col.hideable === false) return true
    return visibleKeys.value.includes(col.key)
  }

  const toggleColumn = (key: string, visible: boolean) => {
    if (visible) {
      if (!visibleKeys.value.includes(key)) {
        visibleKeys.value.push(key)
      }
    } else {
      visibleKeys.value = visibleKeys.value.filter(k => k !== key)
    }
    saveConfig()
  }

  const resetColumns = () => {
    visibleKeys.value = [...defaultVisibleKeys]
    saveConfig()
  }

  return {
    allColumns,
    visibleKeys,
    configurableColumns,
    toggleColumn,
    resetColumns,
    isColumnVisible
  }
}
