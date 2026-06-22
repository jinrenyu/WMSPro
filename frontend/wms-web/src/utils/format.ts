export const formatDate = (dateStr: string | Date | undefined | null): string => {
    if (!dateStr) return ''
    const date = new Date(dateStr)
    if (isNaN(date.getTime())) return ''

    const y = date.getFullYear()
    const m = String(date.getMonth() + 1).padStart(2, '0')
    const d = String(date.getDate()).padStart(2, '0')
    const h = String(date.getHours()).padStart(2, '0')
    const min = String(date.getMinutes()).padStart(2, '0')
    const s = String(date.getSeconds()).padStart(2, '0')

    return `${y}-${m}-${d} ${h}:${min}:${s}`
}

// 纯业务日期（如订单日期/入库日期/退料日期等，由 type=date 选择器录入、时间恒为 00:00:00）只显示到天
export const formatDateOnly = (dateStr: string | Date | undefined | null): string => {
    if (!dateStr) return ''
    const date = new Date(dateStr)
    if (isNaN(date.getTime())) return ''

    const y = date.getFullYear()
    const m = String(date.getMonth() + 1).padStart(2, '0')
    const d = String(date.getDate()).padStart(2, '0')

    return `${y}-${m}-${d}`
}
