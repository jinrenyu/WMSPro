<template>
  <div class="mbt-edit-container">
    <!-- 顶部工具栏 -->
    <div class="edit-toolbar">
      <el-button type="primary" :disabled="isReadonly" @click="handleSave" v-permission="isEdit ? 'materialbartype:edit' : 'materialbartype:add'">
        <el-icon><Check /></el-icon> 保存
      </el-button>
      <el-button v-if="showApprove" type="success" @click="handleApprove" v-permission="'materialbartype:approve'">审核</el-button>
      <el-button v-if="showUnapprove" type="warning" @click="handleUnapprove" v-permission="'materialbartype:approve'">反审核</el-button>
      <el-button v-if="showDisable" type="info" @click="handleDisable" v-permission="'materialbartype:disable'">禁用</el-button>
      <el-button v-if="showEnable" @click="handleEnable" v-permission="'materialbartype:disable'">反禁用</el-button>
      <div class="toolbar-spacer" />
      <el-tag v-if="isEdit" :type="form.fStatus === 40 ? 'success' : 'warning'" size="large">
        {{ form.fStatus === 40 ? '已审核' : '未审核' }}
      </el-tag>
      <el-tag v-if="form.fDisabled" type="danger" size="large" style="margin-left: 8px;">已禁用</el-tag>
      <el-button class="back-btn" @click="handleBack">
        <el-icon><Back /></el-icon> 退出
      </el-button>
    </div>

    <el-form
      ref="formRef"
      :model="form"
      :rules="rules"
      label-width="90px"
      :disabled="isReadonly"
      class="edit-form"
      v-loading="loading"
    >
      <!-- 公共头部 -->
      <div class="form-header">
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="物料编码" prop="fmaterialid">
              <LookupSelect
                v-if="!isEdit"
                v-model="form.fmaterialid"
                module="material"
                placeholder="请选择物料"
                preload
                @change="handleMaterialChange"
              />
              <el-input v-else :model-value="form.fmaterialnumber" disabled />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="物料名称">
              <el-input :model-value="form.fmaterialname" disabled placeholder="选择物料后带出" />
            </el-form-item>
          </el-col>
        </el-row>
      </div>

      <!-- Tab 区 -->
      <el-tabs v-model="activeTab" class="edit-tabs">
        <!-- ============ 基本 ============ -->
        <el-tab-pane label="基本" name="basic">
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="规格型号">
                <el-input :model-value="form.fSpecification" disabled placeholder="选择物料后带出" />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="启用序列号">
                <el-checkbox :model-value="form.fFeedSn" disabled />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="组织">
                <el-input :model-value="companyDisplayName" disabled placeholder="当前组织" />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="条码类型" prop="fbartype">
                <el-select v-model="form.fbartype" placeholder="请选择条码类型" style="width: 100%">
                  <el-option v-for="o in barTypeOptions" :key="o.value" :label="o.label" :value="o.value" />
                </el-select>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="分组">
                <el-tree-select
                  v-model="form.fGroupId"
                  :data="groupTree"
                  :props="{ label: 'fName', children: 'children', value: 'uid' }"
                  placeholder="请选择分组"
                  clearable
                  check-strictly
                  style="width: 100%"
                />
              </el-form-item>
            </el-col>
          </el-row>
        </el-tab-pane>

        <!-- ============ 其他（只读系统信息） ============ -->
        <el-tab-pane label="其他" name="other">
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="制单人">
                <el-input :model-value="form.cUserName || form.cUser" disabled />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="制单日期">
                <el-input :model-value="fmtAuditDate(form.cYmd)" disabled />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="修改人">
                <el-input :model-value="form.mUserName || form.mUser" disabled />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="修改日期">
                <el-input :model-value="fmtAuditDate(form.mYmd)" disabled />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="审核人">
                <el-input :model-value="form.fCheckerName || form.fcheckerid" disabled />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="审核日期">
                <el-input :model-value="fmtAuditDate(form.fcheckdate)" disabled />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="禁用人">
                <el-input :model-value="form.fDisableName || form.fdisableid" disabled />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="禁用日期">
                <el-input :model-value="fmtAuditDate(form.fdisabledate)" disabled />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="禁用">
                <el-checkbox :model-value="form.fDisabled" disabled />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="单据状态">
                <el-input :model-value="form.fStatusName || (form.fStatus === 40 ? '已审核' : '未审核')" disabled />
              </el-form-item>
            </el-col>
          </el-row>
        </el-tab-pane>
      </el-tabs>
    </el-form>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { ElMessage } from 'element-plus'
import type { FormInstance, FormRules } from 'element-plus'
import { Check, Back } from '@element-plus/icons-vue'
import {
  getMaterialBarType, createMaterialBarType, updateMaterialBarType,
  approveMaterialBarType, unapproveMaterialBarType, disableMaterialBarType, enableMaterialBarType,
} from '../../api/materialBarType'
import { getMaterial } from '../../api/material'
import { getGroupTree, type BaseDataGroup } from '../../api/baseDataGroup'
import { formatDate } from '../../utils/format'
import { useOrgStore } from '../../stores/org'
import LookupSelect from '../../components/LookupSelect.vue'
import type { LookupItem } from '../../api/lookup'

const router = useRouter()
const route = useRoute()
const orgStore = useOrgStore()

const formRef = ref<FormInstance>()
const loading = ref(false)
const activeTab = ref('basic')
const groupTree = ref<BaseDataGroup[]>([])

const uid = ref<string>((route.query.uid as string) || '')
const isEdit = computed(() => !!uid.value)
const isReadonly = computed(() => isEdit.value && (form.fStatus === 40 || form.fDisabled))

// 条码类型：1=单品条码;2=最小包装量条码;3=批次条码
const barTypeOptions = [
  { label: '单品条码', value: 1 },
  { label: '最小包装量条码', value: 2 },
  { label: '批次条码', value: 3 },
]

// 组织显示名：FCompanyId 记录所选组织的上级(公司)，故显示上级组织名。
const companyDisplayName = computed(() => {
  if (isEdit.value && form.fCompanyName) return form.fCompanyName
  if (!isEdit.value) {
    const cur = orgStore.currentOrg
    if (cur) return cur.parentOrgName || cur.orgName
  }
  const org = orgStore.orgs.find(o => o.orgId === form.fCompanyId)
  if (org) return org.parentOrgName || org.orgName
  return form.fCompanyName || form.fCompanyId
})

const rules: FormRules = {
  fmaterialid: [{ required: true, message: '请选择物料', trigger: 'change' }],
  fbartype: [{ required: true, message: '请选择条码类型', trigger: 'change' }],
}

const defaultForm = {
  uid: '' as string,
  fStatus: 0,
  fDisabled: false,
  // 头部 / 物料
  fmaterialid: '',
  fmaterialnumber: '',
  fmaterialname: '',
  fSpecification: '',   // 规格型号（物料带出，只读）
  fFeedSn: false,       // 启用序列号（物料带出，只读）
  // 基本
  fbartype: undefined as number | undefined,
  fCompanyId: '',
  fCompanyName: '',
  fGroupId: '',
  // 只读系统信息
  cUser: '',
  cYmd: '',
  mUser: '',
  mYmd: '',
  fcheckerid: '',
  fcheckdate: '',
  fdisableid: '',
  fdisabledate: '',
  // 关联解析名（后端 JOIN 带出）
  cUserName: '',
  mUserName: '',
  fCheckerName: '',
  fDisableName: '',
  fStatusName: '',
}

const form = reactive({ ...defaultForm })

const showApprove = computed(() => isEdit.value && form.fStatus !== 40 && !form.fDisabled)
const showUnapprove = computed(() => isEdit.value && form.fStatus === 40)
const showDisable = computed(() => isEdit.value && !form.fDisabled)
const showEnable = computed(() => isEdit.value && form.fDisabled)

// 审核/禁用日期：未审核/未禁用记录为哨兵值（SqlSugar 把 DateTime.MinValue 落库为 1900-01-01），过滤为空
const fmtAuditDate = (d?: string) => {
  if (!d) return ''
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() <= 1900) return ''
  return formatDate(d)
}

// 选择物料 → 带出名称、规格型号、启用序列号
const handleMaterialChange = (item: LookupItem | null) => {
  if (item) {
    form.fmaterialnumber = item.fNumber
    form.fmaterialname = item.fName
    loadMaterialExtra(item.uid)
  } else {
    form.fmaterialnumber = ''
    form.fmaterialname = ''
    form.fSpecification = ''
    form.fFeedSn = false
  }
}

async function loadMaterialExtra(materialUid?: string) {
  if (!materialUid) {
    form.fSpecification = ''
    form.fFeedSn = false
    return
  }
  try {
    const res: any = await getMaterial(materialUid)
    const m = res.data
    form.fSpecification = m.fSpecification || ''
    form.fFeedSn = !!m.fFeedSn
  } catch (e) {
    console.error('加载物料规格/序列号失败:', e)
  }
}

async function loadGroupTree() {
  try {
    const res: any = await getGroupTree('MaterialBarType')
    groupTree.value = res.data || []
  } catch (e) {
    console.error('加载条码类型分组失败:', e)
  }
}

async function loadDetail(id: string) {
  loading.value = true
  try {
    const res: any = await getMaterialBarType(id)
    const d = res.data
    Object.keys(defaultForm).forEach((k) => {
      if (d[k] !== undefined && d[k] !== null) {
        ;(form as any)[k] = d[k]
      }
    })
    form.uid = d.uid
    // 历史/异常数据 fbartype 不在 1/2/3 内时置空，避免下拉空白且 required 误判通过
    if (form.fbartype !== 1 && form.fbartype !== 2 && form.fbartype !== 3) form.fbartype = undefined
    // 规格型号 / 启用序列号由后端详情(JOIN 物料 FInterId)带出，无需前端再查物料
  } catch (e) {
    console.error('加载物料条码类型详情失败:', e)
    ElMessage.error('加载物料条码类型详情失败')
  } finally {
    loading.value = false
  }
}

async function handleSave() {
  if (!formRef.value) return
  try {
    await formRef.value.validate()
  } catch {
    activeTab.value = 'basic'
    return
  }
  loading.value = true
  try {
    if (isEdit.value) {
      await updateMaterialBarType(uid.value, {
        fbartype: form.fbartype,
        fGroupId: form.fGroupId,
      })
      ElMessage.success('保存成功')
      await loadDetail(uid.value)
    } else {
      const res: any = await createMaterialBarType({
        fmaterialid: form.fmaterialid,
        fmaterialnumber: form.fmaterialnumber,
        fmaterialname: form.fmaterialname,
        fbartype: form.fbartype,
        fCompanyId: form.fCompanyId,
        fGroupId: form.fGroupId,
      })
      ElMessage.success('创建成功')
      const newUid = res?.data?.uid
      if (newUid) {
        uid.value = newUid
        router.replace({ name: 'MaterialBarTypeEdit', query: { uid: newUid } })
        await loadDetail(newUid)
      } else {
        handleBack()
      }
    }
  } catch (error: any) {
    const msg = error?.response?.data?.message || '提交失败'
    ElMessage.error(msg)
  } finally {
    loading.value = false
  }
}

async function runStatusAction(action: (id: string) => Promise<any>, successMsg: string) {
  if (!uid.value) return
  try {
    await action(uid.value)
    ElMessage.success(successMsg)
    await loadDetail(uid.value)
  } catch (error: any) {
    const msg = error?.response?.data?.message || '操作失败'
    ElMessage.error(msg)
  }
}

const handleApprove = () => runStatusAction(approveMaterialBarType, '审核成功')
const handleUnapprove = () => runStatusAction(unapproveMaterialBarType, '反审核成功')
const handleDisable = () => runStatusAction(disableMaterialBarType, '禁用成功')
const handleEnable = () => runStatusAction(enableMaterialBarType, '反禁用成功')

const handleBack = () => {
  router.push({ name: 'MaterialBarTypeList' })
}

onMounted(async () => {
  if (!orgStore.loaded) orgStore.loadOrgs()
  await loadGroupTree()
  if (isEdit.value) {
    await loadDetail(uid.value)
  } else {
    // 新增：默认使用当前切换的组织
    form.fCompanyId = orgStore.currentOrgId
    if (route.query.groupId) form.fGroupId = route.query.groupId as string
  }
})
</script>

<style scoped>
.mbt-edit-container {
  background-color: var(--bg-card);
  border-radius: 8px;
  box-shadow: var(--shadow-card);
  display: flex;
  flex-direction: column;
  height: 100%;
}

.edit-toolbar {
  display: flex;
  align-items: center;
  gap: 8px;
  padding: 12px 16px;
  border-bottom: 1px solid var(--border-color);
}

.toolbar-spacer {
  flex: 1;
}

.back-btn {
  margin-left: 8px;
}

.edit-form {
  padding: 16px 24px 24px;
  overflow-y: auto;
}

.form-header {
  padding-bottom: 8px;
  border-bottom: 1px dashed var(--border-color);
  margin-bottom: 8px;
}

.edit-tabs {
  margin-top: 8px;
}

/* 标签保持单行 */
.edit-form :deep(.el-form-item__label) {
  white-space: nowrap;
}
</style>
