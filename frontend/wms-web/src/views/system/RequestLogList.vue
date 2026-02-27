<template>
  <div class="request-log-container">
    <!-- 统计卡片 -->
    <div class="stat-cards" v-if="statistics">
      <div class="stat-card">
        <div class="stat-value">{{ statistics.totalCount }}</div>
        <div class="stat-label">总请求数</div>
      </div>
      <div class="stat-card">
        <div class="stat-value">{{ statistics.todayCount }}</div>
        <div class="stat-label">今日请求</div>
      </div>
      <div class="stat-card">
        <div class="stat-value">{{ statistics.avgResponseMs }} ms</div>
        <div class="stat-label">平均响应时间</div>
      </div>
      <div class="stat-card">
        <div class="stat-value error-text">{{ statistics.errorCount }}</div>
        <div class="stat-label">错误请求</div>
      </div>
    </div>

    <!-- 搜索区域 -->
    <div class="filter-bar">
      <el-input
        v-model="queryParams.keyword"
        placeholder="搜索路径/用户/IP"
        class="filter-item"
        clearable
        @clear="fetchData"
        @keyup.enter="fetchData"
      >
        <template #append>
          <el-button @click="fetchData"><el-icon><Search /></el-icon></el-button>
        </template>
      </el-input>
      <el-select v-model="queryParams.method" placeholder="HTTP方法" clearable class="filter-item filter-select" @change="fetchData">
        <el-option label="GET" value="GET" />
        <el-option label="POST" value="POST" />
        <el-option label="PUT" value="PUT" />
        <el-option label="DELETE" value="DELETE" />
        <el-option label="PATCH" value="PATCH" />
      </el-select>
      <el-input-number
        v-model="queryParams.statusCode"
        placeholder="状态码"
        :min="100"
        :max="599"
        :controls="false"
        clearable
        class="filter-item filter-select"
        @change="fetchData"
      />
      <el-date-picker
        v-model="dateRange"
        type="daterange"
        range-separator="至"
        start-placeholder="开始日期"
        end-placeholder="结束日期"
        value-format="YYYY-MM-DD"
        class="filter-item"
        @change="onDateChange"
      />
      <el-button type="primary" plain @click="handleExport" v-permission="'requestlog:export'">
        <el-icon><Download /></el-icon> 导出
      </el-button>
    </div>

    <!-- 数据表格 -->
    <el-table
      v-loading="loading"
      :data="logList"
      style="width: 100%"
      border
      class="log-table"
      @row-click="handleRowClick"
    >
      <el-table-column label="请求时间" width="170">
        <template #default="{ row }">
          {{ formatDate(row.frequesttime) }}
        </template>
      </el-table-column>
      <el-table-column label="方法" width="90">
        <template #default="{ row }">
          <el-tag :type="methodTagType(row.fmethod)" size="small" disable-transitions>
            {{ row.fmethod }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="fpath" label="路径" min-width="250" show-overflow-tooltip />
      <el-table-column label="状态码" width="90" align="center">
        <template #default="{ row }">
          <el-tag :type="statusTagType(row.fstatuscode)" size="small" disable-transitions>
            {{ row.fstatuscode }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column label="耗时" width="100" align="right">
        <template #default="{ row }">
          <span :class="{ 'slow-text': row.felapsedms > 500, 'critical-text': row.felapsedms > 2000 }">
            {{ row.felapsedms }} ms
          </span>
        </template>
      </el-table-column>
      <el-table-column prop="fip" label="IP" width="140" show-overflow-tooltip />
      <el-table-column prop="fuserid" label="用户" width="120" show-overflow-tooltip />
    </el-table>

    <!-- 分页 -->
    <div class="pagination-container">
      <el-pagination
        v-model:current-page="queryParams.pageIndex"
        v-model:page-size="queryParams.pageSize"
        :total="total"
        :page-sizes="[20, 50, 100]"
        layout="total, sizes, prev, pager, next"
        @current-change="fetchData"
        @size-change="fetchData"
      />
    </div>

    <!-- 详情抽屉 -->
    <el-drawer v-model="drawerVisible" title="请求详情" size="500px">
      <template v-if="currentRow">
        <el-descriptions :column="1" border>
          <el-descriptions-item label="请求时间">{{ formatDate(currentRow.frequesttime) }}</el-descriptions-item>
          <el-descriptions-item label="HTTP方法">
            <el-tag :type="methodTagType(currentRow.fmethod)" size="small">{{ currentRow.fmethod }}</el-tag>
          </el-descriptions-item>
          <el-descriptions-item label="路径">{{ currentRow.fpath }}</el-descriptions-item>
          <el-descriptions-item label="查询字符串">{{ currentRow.fquerystring || '-' }}</el-descriptions-item>
          <el-descriptions-item label="状态码">
            <el-tag :type="statusTagType(currentRow.fstatuscode)" size="small">{{ currentRow.fstatuscode }}</el-tag>
          </el-descriptions-item>
          <el-descriptions-item label="响应耗时">{{ currentRow.felapsedms }} ms</el-descriptions-item>
          <el-descriptions-item label="客户端IP">{{ currentRow.fip }}</el-descriptions-item>
          <el-descriptions-item label="用户ID">{{ currentRow.fuserid || '-' }}</el-descriptions-item>
          <el-descriptions-item label="User-Agent">{{ currentRow.fuseragent || '-' }}</el-descriptions-item>
          <el-descriptions-item label="关联ID">{{ currentRow.fcorrelationid || '-' }}</el-descriptions-item>
        </el-descriptions>
        <div class="body-section" v-if="currentRow.frequestbody">
          <h4>请求体</h4>
          <el-input
            type="textarea"
            :model-value="formatBody(currentRow.frequestbody)"
            readonly
            :autosize="{ minRows: 4, maxRows: 16 }"
          />
        </div>
        <div class="body-section" v-if="currentRow.fresponsebody">
          <h4>响应体</h4>
          <el-input
            type="textarea"
            :model-value="formatBody(currentRow.fresponsebody)"
            readonly
            :autosize="{ minRows: 4, maxRows: 16 }"
          />
        </div>
      </template>
    </el-drawer>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { getRequestLogs, getRequestLogStatistics, exportRequestLogs, type RequestLog, type RequestLogSummary } from '../../api/requestLog'
import { formatDate } from '../../utils/format'
import { ElMessage } from 'element-plus'
import { Search, Download } from '@element-plus/icons-vue'

const loading = ref(false)
const logList = ref<RequestLog[]>([])
const total = ref(0)
const statistics = ref<RequestLogSummary | null>(null)
const drawerVisible = ref(false)
const currentRow = ref<RequestLog | null>(null)
const dateRange = ref<string[] | null>(null)

const queryParams = reactive({
  pageIndex: 1,
  pageSize: 20,
  keyword: '',
  method: '',
  statusCode: undefined as number | undefined,
  startDate: '',
  endDate: '',
  minElapsedMs: undefined as number | undefined
})

const fetchData = async () => {
  loading.value = true
  try {
    const res: any = await getRequestLogs(queryParams)
    logList.value = res.data.items
    total.value = res.data.totalCount
  } catch (error) {
    console.error('Fetch request logs failed:', error)
  } finally {
    loading.value = false
  }
}

const fetchStatistics = async () => {
  try {
    const res: any = await getRequestLogStatistics({ trendDays: 7 })
    statistics.value = res.data
  } catch (error) {
    console.error('Fetch statistics failed:', error)
  }
}

const onDateChange = () => {
  if (dateRange.value && dateRange.value.length === 2) {
    queryParams.startDate = dateRange.value[0]
    queryParams.endDate = dateRange.value[1]
  } else {
    queryParams.startDate = ''
    queryParams.endDate = ''
  }
  queryParams.pageIndex = 1
  fetchData()
}

const handleExport = async () => {
  try {
    const res: any = await exportRequestLogs(queryParams)
    const blob = new Blob([res], { type: 'text/csv' })
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = `request-logs-${new Date().toISOString().slice(0, 10)}.csv`
    link.click()
    window.URL.revokeObjectURL(url)
    ElMessage.success('导出成功')
  } catch (error) {
    console.error('Export failed:', error)
    ElMessage.error('导出失败')
  }
}

const handleRowClick = (row: RequestLog) => {
  currentRow.value = row
  drawerVisible.value = true
}

const methodTagType = (method: string): string => {
  const map: Record<string, string> = {
    GET: 'success',
    POST: 'primary',
    PUT: 'warning',
    DELETE: 'danger',
    PATCH: 'info'
  }
  return map[method] || 'info'
}

const statusTagType = (code: number): string => {
  if (code >= 200 && code < 300) return 'success'
  if (code >= 300 && code < 400) return 'info'
  if (code >= 400 && code < 500) return 'warning'
  if (code >= 500) return 'danger'
  return 'info'
}

const formatBody = (body: string): string => {
  try {
    return JSON.stringify(JSON.parse(body), null, 2)
  } catch {
    return body
  }
}

onMounted(() => {
  fetchData()
  fetchStatistics()
})
</script>

<style scoped>
.request-log-container {
  padding: 20px;
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
}

.stat-cards {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 16px;
  margin-bottom: 20px;
}

.stat-card {
  background-color: var(--bg-body);
  border: 1px solid var(--border-color);
  border-radius: 8px;
  padding: 16px 20px;
  text-align: center;
}

.stat-value {
  font-size: 24px;
  font-weight: 600;
  color: var(--text-primary);
}

.stat-label {
  font-size: 13px;
  color: var(--text-secondary);
  margin-top: 4px;
}

.error-text {
  color: var(--el-color-danger);
}

.filter-bar {
  display: flex;
  gap: 12px;
  margin-bottom: 20px;
  flex-wrap: wrap;
  align-items: center;
}

.filter-item {
  width: 200px;
}

.filter-select {
  width: 160px;
}

.log-table {
  cursor: pointer;
}

.slow-text {
  color: var(--el-color-warning);
  font-weight: 500;
}

.critical-text {
  color: var(--el-color-danger);
  font-weight: 600;
}

.pagination-container {
  margin-top: 20px;
  display: flex;
  justify-content: flex-end;
}

.body-section {
  margin-top: 20px;
}

.body-section h4 {
  margin-bottom: 8px;
  color: var(--text-primary);
}
</style>
