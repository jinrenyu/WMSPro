import request from '../utils/request'

export interface RequestLog {
    uid: string
    fmethod: string
    fpath: string
    fquerystring: string
    fstatuscode: number
    felapsedms: number
    fip: string
    fuserid: string
    fuseragent: string
    fcorrelationid: string
    frequesttime: string
    fresponsebody: string
}

export interface RequestLogQuery {
    pageIndex: number
    pageSize: number
    method?: string
    path?: string
    statusCode?: number
    userId?: string
    startDate?: string
    endDate?: string
    minElapsedMs?: number
    keyword?: string
}

export interface RequestLogSummary {
    totalCount: number
    todayCount: number
    avgResponseMs: number
    errorCount: number
    byMethod: { method: string; count: number }[]
    byStatusCode: { statusCode: number; count: number }[]
    topPaths: { path: string; count: number }[]
    dailyTrend: { date: string; count: number }[]
}

export const getRequestLogs = (params: RequestLogQuery) => {
    return request({
        url: '/requestlog',
        method: 'get',
        params
    })
}

export const getRequestLogStatistics = (params?: { startDate?: string; endDate?: string; trendDays?: number }) => {
    return request({
        url: '/requestlog/statistics',
        method: 'get',
        params
    })
}

export const exportRequestLogs = (params: RequestLogQuery) => {
    return request({
        url: '/requestlog/export',
        method: 'get',
        params,
        responseType: 'blob'
    })
}
