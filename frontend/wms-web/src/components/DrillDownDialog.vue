<template>
  <el-dialog
    v-model="visible"
    title="下查"
    width="640px"
    append-to-body
    class="drilldown-dialog"
  >
    <div class="dd-source">
      <span class="dd-label">源单：</span>
      <span class="dd-value">{{ sourceName }} {{ sourceBillNo }}</span>
    </div>

    <div class="dd-tip">选择目标单据类型：</div>
    <el-radio-group v-model="selectedType" class="dd-types" @change="loadDocs">
      <el-radio v-for="t in targets" :key="t.value" :value="t.value" border class="dd-type">
        {{ t.label }}
      </el-radio>
    </el-radio-group>

    <div class="dd-tip dd-tip-2">下游单据（仅已审核、未禁用）：</div>
    <el-table
      :data="docs"
      v-loading="docsLoading"
      height="260"
      highlight-current-row
      size="small"
      empty-text="暂无下游单据"
      @current-change="onCurrentChange"
      @row-dblclick="(row:DownstreamDoc) => { selectedDoc = row; confirm() }"
    >
      <el-table-column label="" width="44" align="center">
        <template #default="{ row }">
          <el-radio :value="row.uid" v-model="selectedUid" @change="() => (selectedDoc = row)">
            <span></span>
          </el-radio>
        </template>
      </el-table-column>
      <el-table-column prop="fbillno" label="单据编号" min-width="180" />
      <el-table-column label="单据日期" width="130">
        <template #default="{ row }">{{ fmtDate(row.fdate) }}</template>
      </el-table-column>
      <el-table-column label="状态" width="100" align="center">
        <template #default="{ row }">
          <el-tag :type="row.fStatus === 40 ? 'success' : 'info'" size="small">
            {{ row.fStatus === 40 ? '已审核' : row.fStatus === 70 ? '已关闭' : '未审核' }}
          </el-tag>
        </template>
      </el-table-column>
    </el-table>

    <template #footer>
      <el-button @click="visible = false">取消</el-button>
      <el-button type="primary" :disabled="!selectedDoc" @click="confirm">查看</el-button>
    </template>
  </el-dialog>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage } from 'element-plus'
import { getTraceTargets, getDownstreamDocs, DOC_TARGET_ROUTE as TARGET_ROUTE, type PushDownTarget, type DownstreamDoc } from '../api/selBill'
import { formatDateOnly } from '../utils/format'

// TARGET_ROUTE（共享自 api/selBill.ts）：目标单据模板编号 -> 目标维护页路由名。
// 下查仅传 uid 进入目标页 → 目标页 onMounted 走「加载既有单据」分支，支持查看/修改/二次审核。

// 业务日期展示：过滤 K3 的 1900 哨兵（DateTime.MinValue 落库为 1900-01-01），与入库/退料单列表页 fmtDateOnly 口径一致
function fmtDate(d?: string | null) {
  if (!d) return ''
  const dt = new Date(d)
  if (isNaN(dt.getTime()) || dt.getFullYear() <= 1900) return ''
  return formatDateOnly(d)
}

const router = useRouter()

const visible = ref(false)
const targets = ref<PushDownTarget[]>([])
const selectedType = ref('')
const docs = ref<DownstreamDoc[]>([])
const docsLoading = ref(false)
const selectedUid = ref('')
const selectedDoc = ref<DownstreamDoc | null>(null)

const sourceType = ref('')
const sourceBillNo = ref('')
const sourceName = ref('')

/**
 * 打开下查选择框。
 * @param srcType 源单模板编号（如 PUR_PurchaseOrder）
 * @param billNo  源单编号（下查关联键，必填）
 * @param srcName 源单名称（仅用于弹窗展示，可选）
 */
async function open(srcType: string, billNo: string, srcName = '') {
  // 入口统一重置，任何早退/异常分支都不残留上一次的目标/单据/选中态
  targets.value = []
  docs.value = []
  selectedType.value = ''
  selectedUid.value = ''
  selectedDoc.value = null
  if (!srcType || !billNo) { ElMessage.warning('请先保存并审核单据后再下查'); return }
  try {
    // 下查取「可追溯」目标类型：不看流程启用/禁用，反映历史既成事实（流程事后被禁用也能查到已生成单据）
    const res: any = await getTraceTargets(srcType)
    const list: PushDownTarget[] = (res.data || []).filter((t: PushDownTarget) => TARGET_ROUTE[t.value])
    if (!list.length) { ElMessage.warning('当前单据未配置可下查的目标单据（请在「出入库流程配置」中维护）'); return }
    targets.value = list
    sourceType.value = srcType
    sourceBillNo.value = billNo
    sourceName.value = srcName || ''
    selectedType.value = list[0].value
    visible.value = true
    await loadDocs()
  } catch (e) {
    console.error('获取下查目标失败:', e)
    ElMessage.error('获取下查目标失败')
  }
}

async function loadDocs() {
  selectedUid.value = ''
  selectedDoc.value = null
  docs.value = []
  if (!selectedType.value) return
  docsLoading.value = true
  try {
    const res: any = await getDownstreamDocs(sourceType.value, sourceBillNo.value, selectedType.value)
    docs.value = res.data || []
  } catch (e) {
    console.error('获取下游单据失败:', e)
    ElMessage.error('获取下游单据失败')
  } finally {
    docsLoading.value = false
  }
}

function onCurrentChange(row: DownstreamDoc | null) {
  if (!row) return
  selectedDoc.value = row
  selectedUid.value = row.uid
}

function confirm() {
  const doc = selectedDoc.value
  if (!doc) { ElMessage.warning('请选择要查看的下游单据'); return }
  const routeName = TARGET_ROUTE[selectedType.value]
  if (!routeName) { ElMessage.error('暂不支持查看该单据类型'); return }
  visible.value = false
  router.push({ name: routeName, query: { uid: doc.uid } })
}

defineExpose({ open })
</script>

<style scoped>
.dd-source { margin-bottom: 12px; font-size: 14px; }
.dd-label { color: var(--el-text-color-secondary); }
.dd-value { color: var(--el-text-color-primary); font-weight: 600; }
.dd-tip { margin-bottom: 10px; color: var(--el-text-color-regular); font-size: 13px; }
.dd-tip-2 { margin-top: 16px; }
.dd-types { display: flex; flex-wrap: wrap; gap: 10px; }
.dd-type { margin-right: 0; }
</style>
