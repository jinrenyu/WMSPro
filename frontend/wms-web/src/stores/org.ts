import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { getMyOrgs, type MyOrg } from '../api/org'

const CURRENT_ORG_KEY = 'current_org_id'

export const useOrgStore = defineStore('org', () => {
  const orgs = ref<MyOrg[]>([])
  const currentOrgId = ref<string>(localStorage.getItem(CURRENT_ORG_KEY) || '')
  const loaded = ref(false)

  const currentOrg = computed(() => orgs.value.find(o => o.orgId === currentOrgId.value) || null)
  const currentOrgName = computed(() => currentOrg.value?.orgName || '')

  function persist() {
    if (currentOrgId.value) localStorage.setItem(CURRENT_ORG_KEY, currentOrgId.value)
    else localStorage.removeItem(CURRENT_ORG_KEY)
  }

  async function loadOrgs() {
    try {
      const res: any = await getMyOrgs()
      orgs.value = res.data || []
      loaded.value = true
      // 当前组织：保留仍有效的已选项；否则取默认组织；再否则取第一个
      const stillValid = currentOrgId.value && orgs.value.some(o => o.orgId === currentOrgId.value)
      if (!stillValid) {
        const def = orgs.value.find(o => o.isDefault) || orgs.value[0]
        currentOrgId.value = def ? def.orgId : ''
        persist()
      }
    } catch (e) {
      console.error('加载组织失败:', e)
    }
  }

  function setCurrentOrg(orgId: string) {
    currentOrgId.value = orgId
    persist()
  }

  function reset() {
    orgs.value = []
    currentOrgId.value = ''
    loaded.value = false
    localStorage.removeItem(CURRENT_ORG_KEY)
  }

  return { orgs, currentOrgId, loaded, currentOrg, currentOrgName, loadOrgs, setCurrentOrg, reset }
})
