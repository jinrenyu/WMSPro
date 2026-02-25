import { ref, computed, type Ref, type ComputedRef } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'

interface HasUidAndStatus {
  uid?: string
  fStatus?: number
  fDeleted?: boolean
  fDisabled?: boolean
}

interface UseTableSelectionOptions<T extends HasUidAndStatus> {
  entityName: string
  approveFn: (uid: string) => Promise<any>
  unapproveFn: (uid: string) => Promise<any>
  deleteFn: (uid: string) => Promise<any>
  disableFn?: (uid: string) => Promise<any>
  enableFn?: (uid: string) => Promise<any>
  onSuccess: () => void
}

interface UseTableSelectionReturn<T extends HasUidAndStatus> {
  selectedRows: Ref<T[]>
  selectedCount: ComputedRef<number>
  canEdit: ComputedRef<boolean>
  canApprove: ComputedRef<boolean>
  canUnapprove: ComputedRef<boolean>
  canDelete: ComputedRef<boolean>
  canDisable: ComputedRef<boolean>
  canEnable: ComputedRef<boolean>
  batchLoading: Ref<boolean>
  handleSelectionChange: (rows: T[]) => void
  handleBatchApprove: () => void
  handleBatchUnapprove: () => void
  handleBatchDelete: () => void
  handleBatchDisable: () => void
  handleBatchEnable: () => void
  clearSelection: () => void
}

export function useTableSelection<T extends HasUidAndStatus>(
  options: UseTableSelectionOptions<T>
): UseTableSelectionReturn<T> {
  const { entityName, approveFn, unapproveFn, deleteFn, disableFn, enableFn, onSuccess } = options

  const selectedRows = ref<T[]>([]) as Ref<T[]>
  const batchLoading = ref(false)

  const selectedCount = computed(() => selectedRows.value.length)
  const canEdit = computed(() => selectedRows.value.length === 1)
  const canApprove = computed(() => selectedRows.value.length > 0 && selectedRows.value.every(r => r.fStatus !== 40))
  const canUnapprove = computed(() => selectedRows.value.length > 0 && selectedRows.value.every(r => r.fStatus === 40))
  const canDelete = computed(() => selectedRows.value.length > 0 && selectedRows.value.every(r => r.fStatus !== 40))
  const canDisable = computed(() => selectedRows.value.length > 0 && selectedRows.value.every(r => !r.fDisabled))
  const canEnable = computed(() => selectedRows.value.length > 0 && selectedRows.value.every(r => r.fDisabled))

  const handleSelectionChange = (rows: T[]) => {
    selectedRows.value = rows
  }

  const clearSelection = () => {
    selectedRows.value = []
  }

  const executeBatch = async (
    action: string,
    fn: (uid: string) => Promise<any>
  ) => {
    const count = selectedRows.value.length
    try {
      await ElMessageBox.confirm(
        `确定要${action}选中的 ${count} 条${entityName}吗？`,
        `批量${action}`,
        { confirmButtonText: '确定', cancelButtonText: '取消', type: 'warning' }
      )
    } catch {
      return // user cancelled
    }

    batchLoading.value = true
    try {
      const results = await Promise.allSettled(
        selectedRows.value.map(row => fn(row.uid!))
      )
      const succeeded = results.filter(r => r.status === 'fulfilled').length
      const failed = results.filter(r => r.status === 'rejected').length

      if (failed === 0) {
        ElMessage.success(`${action}成功，共 ${succeeded} 条`)
      } else if (succeeded === 0) {
        ElMessage.error(`${action}失败，共 ${failed} 条`)
      } else {
        ElMessage.warning(`${action}完成：成功 ${succeeded} 条，失败 ${failed} 条`)
      }
      onSuccess()
      clearSelection()
    } finally {
      batchLoading.value = false
    }
  }

  const handleBatchApprove = () => executeBatch('审核', approveFn)
  const handleBatchUnapprove = () => executeBatch('反审核', unapproveFn)
  const handleBatchDelete = () => executeBatch('删除', deleteFn)
  const handleBatchDisable = () => disableFn ? executeBatch('禁用', disableFn) : Promise.resolve()
  const handleBatchEnable = () => enableFn ? executeBatch('反禁用', enableFn) : Promise.resolve()

  return {
    selectedRows,
    selectedCount,
    canEdit,
    canApprove,
    canUnapprove,
    canDelete,
    canDisable,
    canEnable,
    batchLoading,
    handleSelectionChange,
    handleBatchApprove,
    handleBatchUnapprove,
    handleBatchDelete,
    handleBatchDisable,
    handleBatchEnable,
    clearSelection,
  }
}
