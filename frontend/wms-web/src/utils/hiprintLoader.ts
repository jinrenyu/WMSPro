// vue-plugin-hiprint 懒加载器：首次调用时把 jQuery 挂到 window 并动态加载 hiprint，
// 之后复用缓存。供标签模板设计页与各打印页共用。
let modCache: any = null

export async function loadHiprint(): Promise<any> {
  if (modCache) return modCache
  const jq = (await import('jquery')).default
  ;(window as any).jQuery = (window as any).$ = jq
  const mod: any = await import('vue-plugin-hiprint')
  // 没装 electron-hiprint 客户端时关闭自动连，避免控制台反复报错
  try { mod.hiPrintPlugin?.disAutoConnect?.() } catch { /* ignore */ }
  modCache = mod
  return mod
}
