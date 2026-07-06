<template>
  <div class="erp-edit-container">
    <!-- 顶部工具栏 -->
    <div class="edit-toolbar">
      <el-button :icon="CirclePlus" @click="handleNew">新增</el-button>
      <el-button type="primary" :icon="Check" :disabled="isReadonly" @click="handleSave"
                 v-permission="isEdit ? 'erpsync:edit' : 'erpsync:add'">保存</el-button>
      <div class="toolbar-divider" />
      <el-button v-if="isEdit && form.fStatus !== 40" type="success" :icon="CircleCheck" @click="handleApprove" v-permission="'erpsync:approve'">审核</el-button>
      <el-button v-if="isEdit && form.fStatus === 40" type="warning" :icon="RefreshLeft" @click="handleUnapprove" v-permission="'erpsync:approve'">反审核</el-button>
      <el-button v-if="isEdit && form.fStatus === 40" type="primary" plain :icon="Refresh" :loading="syncLoading" @click="handleSync" v-permission="'erpsync:sync'">立即同步</el-button>
      <div class="toolbar-spacer" />
      <el-tag v-if="isEdit" :type="form.fStatus === 40 ? 'success' : 'warning'" size="large">{{ statusText(form.fStatus) }}</el-tag>
      <el-button class="back-btn" :icon="Back" @click="handleBack">退出</el-button>
    </div>

    <el-form ref="formRef" :model="form" :rules="rules" label-width="92px"
             :disabled="isReadonly" class="edit-form" v-loading="loading">
      <!-- 单据头 -->
      <div class="form-header">
        <el-row :gutter="16">
          <el-col :span="8">
            <el-form-item label="功能代码" prop="fnumber">
              <el-input v-model="form.fnumber" placeholder="如 BD_MATERIAL" maxlength="50" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="功能名称" prop="fname">
              <el-input v-model="form.fname" placeholder="如 物料" maxlength="50" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="ERP表单ID" prop="ferpbillid">
              <el-input v-model="form.ferpbillid" placeholder="金蝶表单ID，如 BD_MATERIAL" maxlength="50" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="16">
          <el-col :span="6">
            <el-form-item label="开始时间点">
              <el-input-number v-model="form.fstart" :min="0" :value-on-clear="0" controls-position="right" style="width:100%" />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="同步频率">
              <el-input-number v-model="form.fsynrate" :min="0" :value-on-clear="0" controls-position="right" style="width:100%" />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="时间单位">
              <el-select v-model="form.ftimetype" style="width:100%">
                <el-option v-for="u in timeUnits" :key="u.value" :label="u.label" :value="u.value" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="启用/预设">
              <el-checkbox v-model="form.fisenable">启用同步</el-checkbox>
              <el-checkbox v-model="form.isdefault">系统预设</el-checkbox>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="16">
          <el-col :span="12">
            <el-form-item label="同步过滤规则">
              <el-input v-model="form.fruleid" placeholder="如 FDOCUMENTSTATUS='C'" maxlength="500" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="备注">
              <el-input v-model="form.fnote" placeholder="备注" maxlength="200" />
            </el-form-item>
          </el-col>
        </el-row>
      </div>

      <!-- 左右两栏：实体信息 / 字段映射 -->
      <el-row :gutter="12" class="grids">
        <!-- 实体信息 -->
        <el-col :span="11">
          <div class="grid-section">
            <div class="grid-toolbar">
              <span class="grid-title">实体信息</span>
              <el-button type="primary" size="small" :disabled="isReadonly" @click="addEntity"><el-icon><Plus /></el-icon> 行新增</el-button>
              <el-button size="small" type="danger" :disabled="isReadonly || selectedEntityIndex < 0" @click="deleteEntity">行删除</el-button>
            </div>
            <el-table :data="entities" border size="small" row-key="_key" highlight-current-row
                      :row-class-name="entityRowClass" @row-click="onEntityRowClick"
                      empty-text="暂无实体，点击“行新增”添加" max-height="360">
              <el-table-column type="index" label="#" width="42" align="center" />
              <el-table-column label="主表" width="52" align="center">
                <template #default="{ row }"><el-checkbox v-model="row.fismain" :disabled="isReadonly" /></template>
              </el-table-column>
              <el-table-column label="源数据类型" width="110">
                <template #default="{ row }">
                  <el-select v-model="row.fsrcdatatype" :disabled="isReadonly" size="small">
                    <el-option v-for="t in srcDataTypes" :key="t.value" :label="t.label" :value="t.value" />
                  </el-select>
                </template>
              </el-table-column>
              <el-table-column label="源系统实体" min-width="150">
                <template #default="{ row }"><el-input v-model="row.fsrcdataname" :disabled="isReadonly" size="small" placeholder="ERP源表" /></template>
              </el-table-column>
              <el-table-column label="O3目标实体" min-width="200">
                <template #default="{ row }">
                  <el-select :key="'t'+row._key" v-model="row.faimdataname" filterable :disabled="isReadonly" size="small"
                             placeholder="选择目标表" @change="(v: string) => onTargetTableChange(row, v)">
                    <el-option v-for="t in targetTables" :key="t.tableName" :label="`${t.tableName}${t.description ? ' / ' + t.description : ''}`" :value="t.tableName" />
                  </el-select>
                </template>
              </el-table-column>
              <el-table-column label="目标唯一标识" min-width="160">
                <template #default="{ row }"><el-input v-model="row.faimdatakey" :disabled="isReadonly" size="small" placeholder="如 FCOMPANYID,FNUMBER" /></template>
              </el-table-column>
              <el-table-column label="ERP表单代码" min-width="130">
                <template #default="{ row }"><el-input v-model="row.ferpbillid" :disabled="isReadonly" size="small" placeholder="留空取表头" /></template>
              </el-table-column>
              <el-table-column label="查询字段脚本" min-width="220">
                <template #default="{ row }"><el-input v-model="row.fieldkeys" :disabled="isReadonly" size="small" placeholder="FNUMBER,FNAME,..." /></template>
              </el-table-column>
              <el-table-column label="过滤条件" min-width="150">
                <template #default="{ row }"><el-input v-model="row.fruleid" :disabled="isReadonly" size="small" placeholder="留空取表头" /></template>
              </el-table-column>
              <el-table-column label="排序" min-width="120">
                <template #default="{ row }"><el-input v-model="row.forderstring" :disabled="isReadonly" size="small" /></template>
              </el-table-column>
              <el-table-column label="关联字段" min-width="130">
                <template #default="{ row }"><el-input v-model="row.frelationfield" :disabled="isReadonly" size="small" /></template>
              </el-table-column>
              <el-table-column label="覆盖" width="52" align="center">
                <template #default="{ row }"><el-checkbox v-model="row.fiscover" :disabled="isReadonly" /></template>
              </el-table-column>
            </el-table>
          </div>
        </el-col>

        <!-- 字段映射 -->
        <el-col :span="13">
          <div class="grid-section">
            <div class="grid-toolbar">
              <span class="grid-title">字段映射信息{{ currentEntity ? `（${currentEntity.faimdataname || '未选目标表'}）` : '' }}</span>
              <el-button type="primary" size="small" :disabled="isReadonly || !currentEntity" @click="addField"><el-icon><Plus /></el-icon> 行新增</el-button>
              <el-button size="small" type="danger" :disabled="isReadonly || selectedFieldIndex < 0" @click="deleteField">行删除</el-button>
            </div>
            <el-table :data="currentFieldMaps" border size="small" row-key="_key" highlight-current-row
                      :row-class-name="fieldRowClass" @row-click="onFieldRowClick"
                      empty-text="请选择左侧实体后添加字段映射" max-height="360">
              <el-table-column type="index" label="#" width="42" align="center" />
              <el-table-column label="源数据字段" min-width="150">
                <template #default="{ row }"><el-input v-model="row.fsrcfield" :disabled="isReadonly" size="small" placeholder="ERP字段" /></template>
              </el-table-column>
              <el-table-column label="目标字段" min-width="180">
                <template #default="{ row }">
                  <el-select :key="'f'+row._key" v-model="row.faimfield" filterable :disabled="isReadonly || !currentEntity?.faimdataname" size="small"
                             placeholder="选择目标字段" @change="(v: string) => onTargetFieldChange(row, v)">
                    <el-option v-for="f in currentEntityFields" :key="f.fieldName" :label="`${f.fieldName}`" :value="f.fieldName" />
                  </el-select>
                </template>
              </el-table-column>
              <el-table-column label="目标类型" width="110">
                <template #default="{ row }"><el-input v-model="row.faimfieldtype" disabled size="small" /></template>
              </el-table-column>
              <el-table-column label="字段类型" width="120">
                <template #default="{ row }">
                  <el-select v-model="row.ffieldtype" :disabled="isReadonly" size="small">
                    <el-option v-for="t in fieldTypes" :key="t.value" :label="t.label" :value="t.value" />
                  </el-select>
                </template>
              </el-table-column>
              <el-table-column label="默认值" min-width="110">
                <template #default="{ row }"><el-input v-model="row.fdefaultvalue" :disabled="isReadonly" size="small" /></template>
              </el-table-column>
              <el-table-column label="固定值" min-width="110">
                <template #default="{ row }">
                  <el-input v-model="row.ffixedvalue" :disabled="isReadonly" size="small" @input="() => { if (row.ffixedvalue) row.fissetfixed = true }" />
                </template>
              </el-table-column>
              <el-table-column label="源主键" width="60" align="center">
                <template #default="{ row }"><el-checkbox v-model="row.fissrcfieldkey" :disabled="isReadonly" /></template>
              </el-table-column>
              <el-table-column label="目标主键" width="68" align="center">
                <template #default="{ row }"><el-checkbox v-model="row.faimfieldkey" :disabled="isReadonly" /></template>
              </el-table-column>
              <el-table-column label="加日志" width="60" align="center">
                <template #default="{ row }"><el-checkbox v-model="row.fislogshow" :disabled="isReadonly" /></template>
              </el-table-column>
            </el-table>
          </div>
        </el-col>
      </el-row>
    </el-form>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import type { FormInstance, FormRules } from 'element-plus'
import { Check, Back, Plus, CircleCheck, RefreshLeft, CirclePlus, Refresh } from '@element-plus/icons-vue'
import {
  getErpSyncConfig, createErpSyncConfig, updateErpSyncConfig,
  approveErpSyncConfig, unapproveErpSyncConfig, syncErpSyncConfig,
  getErpTargetTables, getErpTargetFields,
  type ErpSyncEntity, type ErpSyncFieldMap
} from '../../api/erpSyncConfig'

const router = useRouter()
const route = useRoute()

const formRef = ref<FormInstance>()
const loading = ref(false)
const syncLoading = ref(false)

const uid = ref<string>((route.query.uid as string) || '')
const isEdit = computed(() => !!uid.value)

const timeUnits = [
  { value: '1', label: '秒' }, { value: '2', label: '分' }, { value: '3', label: '小时' }, { value: '4', label: '天' }
]
const srcDataTypes = [
  { value: 0, label: '自定义' }, { value: 1, label: '表' }, { value: 2, label: '视图' }
]
const fieldTypes = [
  { value: 0, label: '标准字段' }, { value: 1, label: '辅助属性' }, { value: 2, label: '仓位' }, { value: 4, label: '仓库' }, { value: 3, label: '自定义' }
]

const defaultForm = {
  uid: '', fStatus: 0,
  fnumber: '', fname: '', ferpbillid: '',
  fstart: 0, fsynrate: 1, ftimetype: '1',
  fruleid: '', fisenable: false, isdefault: false, fnote: ''
}
const form = reactive({ ...defaultForm })
const isReadonly = computed(() => isEdit.value && form.fStatus === 40)

const rules: FormRules = {
  fnumber: [{ required: true, message: '请输入功能代码', trigger: 'blur' }],
  fname: [{ required: true, message: '请输入功能名称', trigger: 'blur' }],
}

// 实体信息 / 字段映射
const entities = ref<any[]>([])
const selectedEntityIndex = ref(-1)
const selectedFieldIndex = ref(-1)
let rowKeySeq = 0
const nextKey = () => ++rowKeySeq

const currentEntity = computed<any>(() => (selectedEntityIndex.value >= 0 ? entities.value[selectedEntityIndex.value] : null))
const currentFieldMaps = computed<any[]>(() => currentEntity.value?.fieldMaps || [])

// 目标目录
const targetTables = ref<any[]>([])
const fieldsCache = reactive<Record<string, any[]>>({})
const currentEntityFields = computed<any[]>(() => {
  const t = currentEntity.value?.faimdataname
  return t ? (fieldsCache[t] || []) : []
})

const statusText = (s?: number) => (s === 40 ? '审核' : '暂存')

const entityRowClass = ({ rowIndex }: { rowIndex: number }) => (rowIndex === selectedEntityIndex.value ? 'cur-row' : '')
const fieldRowClass = ({ rowIndex }: { rowIndex: number }) => (rowIndex === selectedFieldIndex.value ? 'cur-row' : '')
const onEntityRowClick = (row: any) => {
  selectedEntityIndex.value = entities.value.indexOf(row)
  selectedFieldIndex.value = -1
  if (row.faimdataname) loadFieldsFor(row.faimdataname)
}
const onFieldRowClick = (row: any) => { selectedFieldIndex.value = currentFieldMaps.value.indexOf(row) }

const newEntity = (): any => ({
  fismain: entities.value.length === 0, fsrcdatatype: 1,
  fsrcdataname: '', faimdataname: '', faimdatatype: 1, faimdatakey: '',
  ferpbillid: '', fieldkeys: '', fruleid: '', forderstring: '', frelationfield: '',
  fiscover: true, fieldMaps: [], _key: nextKey()
})
const addEntity = () => { entities.value.push(newEntity()); selectedEntityIndex.value = entities.value.length - 1; selectedFieldIndex.value = -1 }
const deleteEntity = () => {
  if (selectedEntityIndex.value < 0) return
  entities.value.splice(selectedEntityIndex.value, 1)
  selectedEntityIndex.value = entities.value.length > 0 ? Math.min(selectedEntityIndex.value, entities.value.length - 1) : -1
  selectedFieldIndex.value = -1
}

const newField = (): any => ({
  fsrcfield: '', faimfield: '', faimfieldtype: '', faimfielddes: '', ffieldtype: 0,
  fdefaultvalue: '', fissetfixed: false, ffixedvalue: '',
  fissrcfieldkey: false, faimfieldkey: false, fislogshow: false, fissort: false, _key: nextKey()
})
const addField = () => {
  if (!currentEntity.value) return
  currentEntity.value.fieldMaps.push(newField())
  selectedFieldIndex.value = currentEntity.value.fieldMaps.length - 1
}
const deleteField = () => {
  if (!currentEntity.value || selectedFieldIndex.value < 0) return
  currentEntity.value.fieldMaps.splice(selectedFieldIndex.value, 1)
  selectedFieldIndex.value = currentFieldMaps.value.length > 0 ? Math.min(selectedFieldIndex.value, currentFieldMaps.value.length - 1) : -1
}

async function loadTargetTables() {
  try {
    const res: any = await getErpTargetTables()
    targetTables.value = res.data || []
  } catch (e) { console.error('加载目标实体失败', e) }
}
async function loadFieldsFor(table: string) {
  if (!table || fieldsCache[table]) return
  try {
    const res: any = await getErpTargetFields(table)
    fieldsCache[table] = res.data || []
  } catch (e) { console.error('加载目标字段失败', e) }
}
const onTargetTableChange = (row: any, table: string) => {
  if (table) loadFieldsFor(table)
  // 目标表变更后，该实体已映射行的目标字段属于旧表，必须清空目标侧（保留源字段），
  // 否则残留跨表列会在同步时写入新表不存在的列而整批失败。
  for (const f of (row.fieldMaps || [])) {
    f.faimfield = ''
    f.faimfieldtype = ''
    f.faimfielddes = ''
    f.faimfieldkey = false
  }
}
const onTargetFieldChange = (row: any, field: string) => {
  const f = (currentEntityFields.value || []).find(x => x.fieldName === field)
  row.faimfieldtype = f?.fieldType || ''
  row.faimfielddes = f?.description || ''
  if (f?.isPrimaryKey) row.faimfieldkey = true
}

async function loadDetail(id: string) {
  loading.value = true
  try {
    const res: any = await getErpSyncConfig(id)
    const d = res.data
    Object.keys(defaultForm).forEach(k => { if (d[k] !== undefined && d[k] !== null) (form as any)[k] = d[k] })
    form.uid = d.uid
    form.fStatus = d.fStatus
    entities.value = (d.entities || []).map((e: ErpSyncEntity) => ({
      ...e,
      fieldMaps: (e.fieldMaps || []).map((f: ErpSyncFieldMap) => ({ ...f, _key: nextKey() })),
      _key: nextKey()
    }))
    selectedEntityIndex.value = entities.value.length > 0 ? 0 : -1
    selectedFieldIndex.value = -1
    // 预载已用到的目标表字段
    for (const e of entities.value) if (e.faimdataname) await loadFieldsFor(e.faimdataname)
  } catch (e) {
    console.error('加载ERP集成配置失败:', e)
    ElMessage.error('加载ERP集成配置失败')
  } finally {
    loading.value = false
  }
}

function buildPayload() {
  return {
    fnumber: form.fnumber,
    fname: form.fname,
    ferpbillid: form.ferpbillid,
    fstart: form.fstart,
    fsynrate: form.fsynrate,
    ftimetype: form.ftimetype,
    fruleid: form.fruleid,
    fisenable: form.fisenable,
    isdefault: form.isdefault,
    fnote: form.fnote,
    entities: entities.value.map(e => ({
      fismain: !!e.fismain,
      fsrcdatatype: e.fsrcdatatype ?? 1,
      fsrcdataname: e.fsrcdataname || '',
      fsrcdatades: e.fsrcdatades || '',
      fsrcdatakey: e.fsrcdatakey || '',
      ftimestampname: e.ftimestampname || '',
      filter: e.filter || '',
      faimdatatype: e.faimdatatype ?? 1,
      faimdataname: e.faimdataname || '',
      faimdatades: e.faimdatades || '',
      faimdatakey: e.faimdatakey || '',
      frelationfield: e.frelationfield || '',
      fiscover: !!e.fiscover,
      fieldkeys: e.fieldkeys || '',
      ferpbillid: e.ferpbillid || '',
      forderstring: e.forderstring || '',
      fruleid: e.fruleid || '',
      fieldMaps: (e.fieldMaps || []).map((f: any) => ({
        fsrcfield: f.fsrcfield || '',
        fsecondsrcfield: f.fsecondsrcfield || '',
        fthirdsrcfield: f.fthirdsrcfield || '',
        fsrcfielddes: f.fsrcfielddes || '',
        fissort: !!f.fissort,
        fissrcfieldkey: !!f.fissrcfieldkey,
        faimfield: f.faimfield || '',
        faimfieldtype: f.faimfieldtype || '',
        faimfielddes: f.faimfielddes || '',
        faimfieldkey: !!f.faimfieldkey,
        fmustinput: !!f.fmustinput,
        fdefaultvalue: f.fdefaultvalue || '',
        fissetfixed: !!f.fissetfixed || !!f.ffixedvalue,
        ffixedvalue: f.ffixedvalue || '',
        flookupclassid: f.flookupclassid || '',
        flookupfield: f.flookupfield || '',
        flookupsavefield: f.flookupsavefield || '',
        fisinnerid: !!f.fisinnerid,
        fislogshow: !!f.fislogshow,
        ffieldtype: f.ffieldtype ?? 0,
        ferpseq: f.ferpseq || ''
      }))
    }))
  }
}

async function handleSave() {
  if (!formRef.value) return
  try { await formRef.value.validate() } catch { return }
  loading.value = true
  try {
    if (isEdit.value) {
      await updateErpSyncConfig(uid.value, buildPayload())
      ElMessage.success('保存成功')
      await loadDetail(uid.value)
    } else {
      const res: any = await createErpSyncConfig(buildPayload())
      ElMessage.success('创建成功')
      const newUid = res?.data?.uid
      if (newUid) {
        uid.value = newUid
        router.replace({ name: 'ErpSyncConfigEdit', query: { uid: newUid } })
        await loadDetail(newUid)
      } else { handleBack() }
    }
  } catch (error: any) {
    ElMessage.error(error?.response?.data?.message || '提交失败')
  } finally {
    loading.value = false
  }
}

function resetForm() {
  Object.assign(form, defaultForm)
  entities.value = []
  selectedEntityIndex.value = -1
  selectedFieldIndex.value = -1
  formRef.value?.clearValidate()
}
const handleNew = () => { uid.value = ''; resetForm(); if (route.query.uid) router.replace({ name: 'ErpSyncConfigEdit' }) }

async function runStatus(fn: (id: string) => Promise<any>, msg: string) {
  if (!uid.value) return
  try { await fn(uid.value); ElMessage.success(msg); await loadDetail(uid.value) }
  catch (error: any) { ElMessage.error(error?.response?.data?.message || '操作失败') }
}
const handleApprove = () => runStatus(approveErpSyncConfig, '审核成功')
const handleUnapprove = () => runStatus(unapproveErpSyncConfig, '反审核成功')

async function handleSync() {
  if (!uid.value) return
  const r = await ElMessageBox.confirm('确认立即执行一次同步？', '立即同步', { type: 'warning' }).catch(() => 'cancel')
  if (r === 'cancel') return
  syncLoading.value = true
  try {
    const res: any = await syncErpSyncConfig(uid.value)
    const data = res.data || {}
    if (data.success) ElMessage.success(data.message || `同步完成，成功 ${data.successCount} 条`)
    else ElMessage.warning(data.message || `同步完成：成功 ${data.successCount}，失败 ${data.failCount}`)
    await loadDetail(uid.value)
  } catch (e: any) {
    ElMessage.error(e?.response?.data?.message || e?.message || '同步失败')
  } finally {
    syncLoading.value = false
  }
}

const handleBack = () => router.push({ name: 'ErpSyncConfigList' })

onMounted(async () => {
  await loadTargetTables()
  if (isEdit.value) await loadDetail(uid.value)
})
</script>

<style scoped>
.erp-edit-container {
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
  border-bottom: 1px solid var(--border-color, #ebeef5);
}
.toolbar-divider { width: 1px; height: 22px; background: var(--border-color, #ebeef5); margin: 0 4px; }
.toolbar-spacer { flex: 1; }
.back-btn { margin-left: 8px; }
.edit-form { padding: 16px 20px 24px; overflow-y: auto; }
.form-header { padding-bottom: 4px; border-bottom: 1px dashed var(--border-color, #ebeef5); margin-bottom: 8px; }
.grids { margin-top: 8px; }
.grid-section { margin-top: 8px; }
.grid-toolbar { display: flex; align-items: center; gap: 8px; margin-bottom: 8px; }
.grid-title { font-weight: 600; margin-right: 8px; }
.edit-form :deep(.el-table__body tr.cur-row > td.el-table__cell) {
  background-color: var(--el-color-primary-light-9);
}
</style>
