<template>
  <el-dialog
    v-model="visible"
    title="下推"
    width="420px"
    append-to-body
    class="pushdown-dialog"
  >
    <div class="pd-source">
      <span class="pd-label">源单：</span>
      <span class="pd-value">{{ sourceName }} {{ sourceBillNo }}</span>
    </div>
    <div class="pd-tip">请选择下推生成的目标单据：</div>
    <el-radio-group v-model="selected" class="pd-options">
      <el-radio v-for="t in targets" :key="t.value" :value="t.value" border class="pd-option">
        {{ t.label }}
      </el-radio>
    </el-radio-group>

    <template #footer>
      <el-button @click="visible = false">取消</el-button>
      <el-button type="primary" :disabled="!selected" @click="confirm">下推</el-button>
    </template>
  </el-dialog>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage } from 'element-plus'
import { getPushTargets, DOC_TARGET_ROUTE as TARGET_ROUTE, type PushDownTarget } from '../api/selBill'

// TARGET_ROUTE：目标单据模板编号 -> 目标维护页路由名（共享自 api/selBill.ts）。
// 这些目标页 onMounted 会识别下推 query（srcform/srcuid/srcbillno）自动带入源单明细。
const router = useRouter()

const visible = ref(false)
const targets = ref<PushDownTarget[]>([])
const selected = ref('')
const sourceType = ref('')
const sourceUid = ref('')
const sourceBillNo = ref('')
const sourceName = ref('')

/**
 * 打开下推选择框。
 * @param srcType 源单模板编号（如 PUR_PurchaseOrder）
 * @param srcUid  源单 Uid
 * @param billNo  源单编号（用于目标页回填源单号显示）
 * @param srcName 源单名称（仅用于弹窗展示，可选）
 */
async function open(srcType: string, srcUid: string, billNo: string, srcName = '') {
  if (!srcType || !srcUid) { ElMessage.warning('请先保存并审核单据后再下推'); return }
  try {
    const res: any = await getPushTargets(srcType)
    const list: PushDownTarget[] = (res.data || []).filter((t: PushDownTarget) => TARGET_ROUTE[t.value])
    if (!list.length) { ElMessage.warning('当前单据未配置可下推的目标单据（请在「出入库流程配置」中维护）'); return }
    targets.value = list
    selected.value = list[0].value
    sourceType.value = srcType
    sourceUid.value = srcUid
    sourceBillNo.value = billNo || ''
    sourceName.value = srcName || ''
    visible.value = true
  } catch (e) {
    console.error('获取下推目标失败:', e)
    ElMessage.error('获取下推目标失败')
  }
}

function confirm() {
  const routeName = TARGET_ROUTE[selected.value]
  if (!routeName) { ElMessage.error('暂不支持下推到该单据类型'); return }
  visible.value = false
  router.push({
    name: routeName,
    query: { srcform: sourceType.value, srcuid: sourceUid.value, srcbillno: sourceBillNo.value }
  })
}

defineExpose({ open })
</script>

<style scoped>
.pd-source { margin-bottom: 12px; font-size: 14px; }
.pd-label { color: var(--el-text-color-secondary); }
.pd-value { color: var(--el-text-color-primary); font-weight: 600; }
.pd-tip { margin-bottom: 10px; color: var(--el-text-color-regular); font-size: 13px; }
.pd-options { display: flex; flex-direction: column; gap: 10px; }
.pd-option { margin-right: 0; width: 100%; }
</style>
