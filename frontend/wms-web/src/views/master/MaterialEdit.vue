<template>
  <div class="material-edit-container">
    <!-- 顶部工具栏 -->
    <div class="edit-toolbar">
      <el-button type="primary" :disabled="isReadonly" @click="handleSave" v-permission="isEdit ? 'material:edit' : 'material:add'">
        <el-icon><Check /></el-icon> 保存
      </el-button>
      <el-button v-if="showApprove" type="success" @click="handleApprove" v-permission="'material:approve'">审核</el-button>
      <el-button v-if="showUnapprove" type="warning" @click="handleUnapprove" v-permission="'material:approve'">反审核</el-button>
      <el-button v-if="showDisable" type="info" @click="handleDisable" v-permission="'material:disable'">禁用</el-button>
      <el-button v-if="showEnable" @click="handleEnable" v-permission="'material:disable'">反禁用</el-button>
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
          <el-col :span="8">
            <el-form-item label="物料代码" prop="fNumber">
              <el-input v-model="form.fNumber" :disabled="isEdit" placeholder="编辑时不可修改" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="物料名称" prop="fName">
              <el-input v-model="form.fName" placeholder="必填" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="规格型号">
              <el-input v-model="form.fSpecification" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="8">
            <el-form-item label="使用组织">
              <el-input :model-value="companyDisplayName" disabled placeholder="当前组织" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="物料分组">
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
          <el-col :span="8">
            <el-form-item label="物料类别">
              <LookupSelect v-model="form.fTypeId" module="material/material-types" placeholder="选择物料类别" preload />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="24">
            <el-form-item label="描述">
              <el-input v-model="form.fDescription" />
            </el-form-item>
          </el-col>
        </el-row>
      </div>

      <!-- Tab 区 -->
      <el-tabs v-model="activeTab" class="edit-tabs">
        <!-- ============ 物料信息 ============ -->
        <el-tab-pane label="物料信息" name="info">
          <!-- 基本信息 -->
          <div class="section-title">基本信息</div>
          <el-row :gutter="20">
            <el-col :span="8">
              <el-form-item label="物料属性">
                <el-select v-model="form.fErpClsId" style="width: 100%" clearable>
                  <el-option v-for="o in erpClsOptions" :key="o" :label="o" :value="o" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="默认仓库">
                <LookupSelect v-model="form.fDeStockId" module="warehouse" placeholder="请选择默认仓库" preload @change="handleWarehouseChange" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="默认仓位">
                <LookupSelect v-model="form.fDeSpId" module="stockplace" :parent-id="form.fDeStockId" placeholder="请先选择仓库" :disabled="!form.fDeStockId" preload />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="8">
              <el-form-item label="图号">
                <el-input v-model="form.fChartNumber" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="税率(%)">
                <el-input-number v-model="form.fTaxRate" :min="0" :max="100" :precision="2" :controls="false" style="width: 100%" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="ABC分类">
                <el-select v-model="form.fAbc" style="width: 100%" clearable>
                  <el-option v-for="o in abcOptions" :key="o" :label="o" :value="o" />
                </el-select>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="8">
              <el-form-item label="最低采购量">
                <el-input-number v-model="form.fMinPoQty" :min="0" :precision="4" :controls="false" style="width: 100%" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="条码">
                <el-input v-model="form.fBarcode" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="旧料号">
                <el-input v-model="form.fOldNumber" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="24">
              <el-form-item label="物料图片">
                <div class="picture-uploader">
                  <el-upload
                    :auto-upload="false"
                    :show-file-list="false"
                    accept="image/*"
                    :disabled="isReadonly"
                    @change="handlePictureChange"
                  >
                    <el-button :disabled="isReadonly">
                      <el-icon><Picture /></el-icon> 选择图片
                    </el-button>
                  </el-upload>
                  <el-button v-if="form.fPictureBase64" link type="danger" :disabled="isReadonly" @click="form.fPictureBase64 = null">移除</el-button>
                  <div v-if="pictureSrc" class="picture-preview">
                    <img :src="pictureSrc" alt="物料图片" />
                  </div>
                </div>
              </el-form-item>
            </el-col>
          </el-row>

          <!-- 包装与单位信息 -->
          <div class="section-title">包装与单位信息</div>
          <el-row :gutter="20">
            <el-col :span="8">
              <el-form-item label="条码类型">
                <el-select v-model="form.fBarType" style="width: 100%" clearable>
                  <el-option v-for="o in barTypeOptions" :key="o.value" :label="o.label" :value="o.value" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="最小包装量">
                <el-input-number v-model="form.fIncreaseQty" :min="0" :precision="4" :controls="false" style="width: 100%" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="基本单位">
                <LookupSelect v-model="form.fBaseUnitId" module="unit" placeholder="请选择基本单位" preload />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="8">
              <el-form-item label="采购单位">
                <LookupSelect v-model="form.fPurchaseUnitId" module="unit" placeholder="请选择采购单位" preload />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="销售单位">
                <LookupSelect v-model="form.fSaleUnitId" module="unit" placeholder="请选择销售单位" preload />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="库存单位">
                <LookupSelect v-model="form.fStoreUnitId" module="unit" placeholder="请选择库存单位" preload />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="8">
              <el-form-item label="委外单位">
                <LookupSelect v-model="form.fSubConUnitId" module="unit" placeholder="请选择委外单位" preload />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="生产单位">
                <LookupSelect v-model="form.fProduceUnitId" module="unit" placeholder="请选择生产单位" preload />
              </el-form-item>
            </el-col>
          </el-row>

          <!-- 尺寸信息 -->
          <div class="section-title">尺寸信息</div>
          <el-row :gutter="20">
            <el-col :span="8">
              <el-form-item label="长">
                <el-input-number v-model="form.fLength" :min="0" :precision="3" :controls="false" style="width: 100%" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="宽">
                <el-input-number v-model="form.fWidth" :min="0" :precision="3" :controls="false" style="width: 100%" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="高">
                <el-input-number v-model="form.fHeight" :min="0" :precision="3" :controls="false" style="width: 100%" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="8">
              <el-form-item label="体积">
                <el-input-number v-model="form.fVolume" :min="0" :precision="4" :controls="false" style="width: 100%" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="毛重">
                <el-input-number v-model="form.fGrossWeight" :min="0" :precision="4" :controls="false" style="width: 100%" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="净重">
                <el-input-number v-model="form.fNetWeight" :min="0" :precision="4" :controls="false" style="width: 100%" />
              </el-form-item>
            </el-col>
          </el-row>

          <!-- 库存控制信息 -->
          <div class="section-title">库存控制信息</div>
          <el-row :gutter="20">
            <el-col :span="8">
              <el-form-item label="最大库存量">
                <el-input-number v-model="form.fMaxQty" :min="0" :precision="4" :controls="false" style="width: 100%" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="安全库存量">
                <el-input-number v-model="form.fSafeQty" :min="0" :precision="4" :controls="false" style="width: 100%" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="最低库存量">
                <el-input-number v-model="form.fLowLimit" :min="0" :precision="4" :controls="false" style="width: 100%" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="8">
              <el-form-item label="启用保质期">
                <el-checkbox v-model="form.fIsKfPeriod" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="保质期单位">
                <el-select v-model="form.fKfUnit" :disabled="!form.fIsKfPeriod" style="width: 100%">
                  <el-option :value="0" label="天" />
                  <el-option :value="1" label="月" />
                  <el-option :value="2" label="年" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="保质期限">
                <el-input-number v-model="form.fKfPeriod" :min="0" :disabled="!form.fIsKfPeriod" :controls="false" style="width: 100%" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="8">
              <el-form-item label="启用序列号">
                <el-checkbox v-model="form.fFeedSn" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="启用批号管理">
                <el-checkbox v-model="form.fIsBatchManage" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="第三方序列号">
                <el-checkbox v-model="form.fOtherSn" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="8">
              <el-form-item label="启用辅助单位">
                <el-checkbox v-model="form.fIsSecUnit" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="不投料">
                <el-checkbox v-model="form.fIsFeed" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="来料检验">
                <el-checkbox v-model="form.fCheckIncoming" />
              </el-form-item>
            </el-col>
          </el-row>

          <!-- 其他信息 -->
          <div class="section-title">其他信息</div>
          <el-row :gutter="20">
            <el-col :span="8">
              <el-form-item label="终检">
                <el-checkbox v-model="form.fIsPinal" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="套件">
                <el-checkbox v-model="form.fSuite" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="虚拟件">
                <el-checkbox v-model="form.fVPart" />
              </el-form-item>
            </el-col>
          </el-row>
        </el-tab-pane>

        <!-- ============ 物料维度（辅助属性配置） ============ -->
        <el-tab-pane label="物料维度" name="aux">
          <el-empty v-if="!isEdit" description="保存物料后可维护物料维度" />
          <template v-else>
            <div class="grid-toolbar">
              <el-button type="primary" size="small" :disabled="isReadonly" @click="addDimension">
                <el-icon><Plus /></el-icon> 新增
              </el-button>
            </div>
            <el-table :data="dimensions" border size="small" empty-text="暂无辅助属性配置">
              <el-table-column type="index" label="#" width="45" align="center" />
              <el-table-column label="辅助属性编码 / 名称" min-width="240">
                <template #default="{ row }">
                  <LookupSelect
                    v-model="row.fAuxPropertyId"
                    module="material/aux-properties"
                    placeholder="选择辅助属性"
                    preload
                    :disabled="isReadonly"
                    @change="(item: any) => onAuxPropChange(row, item)"
                  />
                </template>
              </el-table-column>
              <el-table-column label="启用" width="64" align="center">
                <template #default="{ row }"><el-checkbox v-model="row.fIsEnable" :disabled="isReadonly" /></template>
              </el-table-column>
              <el-table-column label="组织控制" width="80" align="center">
                <template #default="{ row }"><el-checkbox v-model="row.fIsComControl" :disabled="isReadonly" /></template>
              </el-table-column>
              <el-table-column label="影响价格" width="80" align="center">
                <template #default="{ row }"><el-checkbox v-model="row.fIsAffectPrice" :disabled="isReadonly" /></template>
              </el-table-column>
              <el-table-column label="影响计划" width="80" align="center">
                <template #default="{ row }"><el-checkbox v-model="row.fIsAffectPlan" :disabled="isReadonly" /></template>
              </el-table-column>
              <el-table-column label="影响出库成本" width="100" align="center">
                <template #default="{ row }"><el-checkbox v-model="row.fIsAffectCost" :disabled="isReadonly" /></template>
              </el-table-column>
              <el-table-column label="值设置状态" width="90" align="center">
                <template #default="{ row }"><el-checkbox v-model="row.fValueSet" :disabled="isReadonly" /></template>
              </el-table-column>
              <el-table-column label="操作" width="64" align="center" fixed="right">
                <template #default="{ $index }">
                  <el-button link type="danger" :disabled="isReadonly" @click="dimensions.splice($index, 1)">删除</el-button>
                </template>
              </el-table-column>
            </el-table>
          </template>
        </el-tab-pane>

        <!-- ============ 辅助单位换算 ============ -->
        <el-tab-pane label="辅助单位" name="secunit">
          <el-empty v-if="!isEdit" description="保存物料后可维护辅助单位换算" />
          <template v-else>
            <div class="grid-toolbar">
              <el-button type="primary" size="small" :disabled="isReadonly" @click="addSecUnit">
                <el-icon><Plus /></el-icon> 新增
              </el-button>
            </div>
            <el-table :data="secUnits" border size="small" empty-text="暂无辅助单位换算">
              <el-table-column type="index" label="#" width="45" align="center" />
              <el-table-column label="换算类型" width="110">
                <template #default="{ row }">
                  <el-select v-model="row.fConvertType" size="small" :disabled="isReadonly" style="width: 100%">
                    <el-option label="固定" value="固定" />
                    <el-option label="浮动" value="浮动" />
                  </el-select>
                </template>
              </el-table-column>
              <el-table-column label="辅助单位" min-width="180">
                <template #default="{ row }">
                  <LookupSelect v-model="row.fSecUnitId" module="unit" placeholder="辅助单位" preload :disabled="isReadonly" />
                </template>
              </el-table-column>
              <el-table-column label="辅助单位数量" width="140">
                <template #default="{ row }">
                  <el-input-number v-model="row.fConvertNumerator" :min="0" :precision="6" :controls="false" size="small" :disabled="isReadonly" style="width: 100%" />
                </template>
              </el-table-column>
              <el-table-column label="=" width="36" align="center"><template #default>=</template></el-table-column>
              <el-table-column label="基本单位" min-width="180">
                <template #default="{ row }">
                  <LookupSelect v-model="row.fBaseUnitId" module="unit" placeholder="基本单位" preload :disabled="isReadonly" />
                </template>
              </el-table-column>
              <el-table-column label="基本单位数量" width="140">
                <template #default="{ row }">
                  <el-input-number v-model="row.fConvertDenominator" :min="0" :precision="6" :controls="false" size="small" :disabled="isReadonly" style="width: 100%" />
                </template>
              </el-table-column>
              <el-table-column label="操作" width="64" align="center" fixed="right">
                <template #default="{ $index }">
                  <el-button link type="danger" :disabled="isReadonly" @click="secUnits.splice($index, 1)">删除</el-button>
                </template>
              </el-table-column>
            </el-table>
          </template>
        </el-tab-pane>

        <!-- ============ 其他（只读系统信息） ============ -->
        <el-tab-pane label="其他" name="other">
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="制单人">
                <el-input :model-value="form.cUser" disabled />
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
                <el-input :model-value="form.mUser" disabled />
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
                <el-input :model-value="form.fCheckerId" disabled />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="审核日期">
                <el-input :model-value="fmtAuditDate(form.fCheckDate)" disabled />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="禁用人">
                <el-input :model-value="form.fdisableid" disabled />
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
              <el-form-item label="数据状态">
                <el-input :model-value="form.fStatus === 40 ? '已审核' : '未审核'" disabled />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="禁用">
                <el-checkbox :model-value="form.fDisabled" disabled />
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
import { Check, Back, Plus, Picture } from '@element-plus/icons-vue'
import {
  getMaterial, createMaterial, updateMaterial,
  approveMaterial, unapproveMaterial, disableMaterial, enableMaterial,
  getMaterialDimensions, saveMaterialDimensions, getMaterialSecUnits, saveMaterialSecUnits,
  type MaterialDimension, type MaterialSecUnit
} from '../../api/material'
import type { LookupItem } from '../../api/lookup'
import { getGroupTree, type BaseDataGroup } from '../../api/baseDataGroup'
import LookupSelect from '../../components/LookupSelect.vue'
import { formatDate } from '../../utils/format'
import { useOrgStore } from '../../stores/org'

const router = useRouter()
const route = useRoute()
const orgStore = useOrgStore()

const formRef = ref<FormInstance>()
const loading = ref(false)
const activeTab = ref('info')
const groupTree = ref<BaseDataGroup[]>([])

const uid = ref<string>((route.query.uid as string) || '')
const isEdit = computed(() => !!uid.value)
const isReadonly = computed(() => isEdit.value && (form.fStatus === 40 || form.fDisabled))

// 使用组织显示名：物料 FCompanyId 记录的是所选组织的上级(公司)，故显示上级组织名。
// 编辑态用后端解析的 FCompanyName；新增态用当前所选组织的上级名。
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

const erpClsOptions = ['外购', '自制', '委外', '虚拟件', '资产', '费用']
const abcOptions = ['A', 'B', 'C']
const barTypeOptions = [
  { label: '不启用', value: 0 },
  { label: '单品条码', value: 1 },
  { label: '批次条码', value: 2 },
]

const rules: FormRules = {
  fNumber: [{ required: true, message: '请输入物料代码', trigger: 'blur' }],
  fName: [{ required: true, message: '请输入物料名称', trigger: 'blur' }],
}

const defaultForm = {
  uid: '' as string,
  fStatus: 0,
  fDisabled: false,
  // 头部
  fNumber: '',
  fName: '',
  fSpecification: '',
  fCompanyId: '',
  fCompanyName: '',
  fGroupId: '',
  fTypeId: '',
  fDescription: '',
  // 基本信息
  fErpClsId: '',
  fDeStockId: '',
  fDeSpId: '',
  fChartNumber: '',
  fTaxRate: 0,
  fAbc: '',
  fMinPoQty: 0,
  fBarcode: '',
  fOldNumber: '',
  // 包装与单位
  fBarType: 0,
  fIncreaseQty: 0,
  fBaseUnitId: '',
  fPurchaseUnitId: '',
  fSaleUnitId: '',
  fStoreUnitId: '',
  fSubConUnitId: '',
  fProduceUnitId: '',
  // 尺寸
  fLength: 0,
  fWidth: 0,
  fHeight: 0,
  fVolume: 0,
  fGrossWeight: 0,
  fNetWeight: 0,
  // 库存控制
  fMaxQty: 0,
  fSafeQty: 0,
  fLowLimit: 0,
  fIsKfPeriod: false,
  fKfUnit: 0,
  fKfPeriod: 0,
  fFeedSn: false,
  fIsBatchManage: false,
  fOtherSn: false,
  fIsSecUnit: false,
  fIsFeed: false,
  fCheckIncoming: false,
  // 其他信息
  fIsPinal: false,
  fSuite: false,
  fVPart: false,
  // 只读系统信息
  cUser: '',
  cYmd: '',
  mUser: '',
  mYmd: '',
  fCheckerId: '',
  fCheckDate: '',
  fdisableid: '',
  fdisabledate: '',
  // 图片
  fPictureBase64: null as string | null,
}

const form = reactive({ ...defaultForm })

// 物料维度（辅助属性配置）与辅助单位换算明细
const dimensions = ref<MaterialDimension[]>([])
const secUnits = ref<MaterialSecUnit[]>([])

const showApprove = computed(() => isEdit.value && form.fStatus !== 40 && !form.fDisabled)
const showUnapprove = computed(() => isEdit.value && form.fStatus === 40)
const showDisable = computed(() => isEdit.value && !form.fDisabled)
const showEnable = computed(() => isEdit.value && form.fDisabled)

const fmtAuditDate = (d?: string) => {
  if (!d) return ''
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() < 1900) return ''
  return formatDate(d)
}

const handleWarehouseChange = () => {
  form.fDeSpId = ''
}

// ---- 图片 ----
const pictureSrc = computed(() => {
  const b64 = form.fPictureBase64
  if (!b64) return ''
  let mime = 'image/png'
  if (b64.startsWith('/9j/')) mime = 'image/jpeg'
  else if (b64.startsWith('iVBOR')) mime = 'image/png'
  else if (b64.startsWith('R0lGOD')) mime = 'image/gif'
  else if (b64.startsWith('UklGR')) mime = 'image/webp'
  return `data:${mime};base64,${b64}`
})

const handlePictureChange = (uploadFile: any) => {
  const raw = uploadFile?.raw
  if (!raw) return
  if (raw.size > 5 * 1024 * 1024) {
    ElMessage.warning('图片大小不能超过 5MB')
    return
  }
  const reader = new FileReader()
  reader.onload = (e) => {
    const dataUrl = (e.target?.result as string) || ''
    const comma = dataUrl.indexOf(',')
    form.fPictureBase64 = comma >= 0 ? dataUrl.slice(comma + 1) : dataUrl
  }
  reader.readAsDataURL(raw)
}

// ---- 物料维度 ----
const addDimension = () => {
  dimensions.value.push({
    fAuxPropertyId: '', fAuxPropertyNumber: '', fAuxPropertyName: '',
    fIsEnable: true, fIsComControl: false, fIsMustInput: false,
    fIsAffectPrice: false, fIsAffectPlan: false, fIsAffectCost: false, fValueSet: false,
  })
}
const onAuxPropChange = (row: MaterialDimension, item: LookupItem | null) => {
  row.fAuxPropertyNumber = item?.fNumber || ''
  row.fAuxPropertyName = item?.fName || ''
}

// ---- 辅助单位换算 ----
const addSecUnit = () => {
  secUnits.value.push({
    fConvertType: '固定', fSecUnitId: '',
    fBaseUnitId: form.fBaseUnitId || '',
    fConvertNumerator: 1, fConvertDenominator: 1,
  })
}

async function loadDimensions(id: string) {
  try {
    const res: any = await getMaterialDimensions(id)
    dimensions.value = res.data || []
  } catch (e) {
    console.error('加载物料维度失败:', e)
  }
}

async function loadSecUnits(id: string) {
  try {
    const res: any = await getMaterialSecUnits(id)
    secUnits.value = res.data || []
  } catch (e) {
    console.error('加载辅助单位失败:', e)
  }
}

async function loadGroupTree() {
  try {
    const res: any = await getGroupTree('Material')
    groupTree.value = res.data || []
  } catch (e) {
    console.error('加载物料分组失败:', e)
  }
}

async function loadDetail(id: string) {
  loading.value = true
  try {
    const res: any = await getMaterial(id)
    const d = res.data
    // 将后端字段合并到表单（缺失项保持默认）
    Object.keys(defaultForm).forEach((k) => {
      if (d[k] !== undefined && d[k] !== null) {
        ;(form as any)[k] = d[k]
      }
    })
    form.uid = d.uid
  } catch (e) {
    console.error('加载物料详情失败:', e)
    ElMessage.error('加载物料详情失败')
  } finally {
    loading.value = false
  }
}

function buildPayload() {
  return {
    fNumber: form.fNumber,
    fName: form.fName,
    fSpecification: form.fSpecification,
    fDescription: form.fDescription,
    fBarcode: form.fBarcode,
    fErpClsId: form.fErpClsId,
    fAbc: form.fAbc,
    fMaxQty: form.fMaxQty,
    fSafeQty: form.fSafeQty,
    fNetWeight: form.fNetWeight,
    fGrossWeight: form.fGrossWeight,
    fBaseUnitId: form.fBaseUnitId,
    fStoreUnitId: form.fStoreUnitId,
    fSaleUnitId: form.fSaleUnitId,
    fPurchaseUnitId: form.fPurchaseUnitId,
    fIsKfPeriod: form.fIsKfPeriod,
    fKfUnit: form.fKfUnit,
    fKfPeriod: form.fKfPeriod,
    fIsBatchManage: form.fIsBatchManage,
    fMinPoQty: form.fMinPoQty,
    fIncreaseQty: form.fIncreaseQty,
    fCheckIncoming: form.fCheckIncoming,
    fOldNumber: form.fOldNumber,
    fDeStockId: form.fDeStockId,
    fDeSpId: form.fDeSpId,
    fVPart: form.fVPart,
    fTypeId: form.fTypeId,
    fGroupId: form.fGroupId,
    fCompanyId: form.fCompanyId,
    // 扩展字段
    fChartNumber: form.fChartNumber,
    fTaxRate: form.fTaxRate,
    fBarType: form.fBarType,
    fSubConUnitId: form.fSubConUnitId,
    fProduceUnitId: form.fProduceUnitId,
    fAuxUnitId: '',
    fLength: form.fLength,
    fWidth: form.fWidth,
    fHeight: form.fHeight,
    fVolume: form.fVolume,
    fLowLimit: form.fLowLimit,
    fFeedSn: form.fFeedSn,
    fOtherSn: form.fOtherSn,
    fIsSecUnit: form.fIsSecUnit,
    fIsFeed: form.fIsFeed,
    fIsPinal: form.fIsPinal,
    fSuite: form.fSuite,
    fPrice: 0,
    fPictureBase64: form.fPictureBase64,
  }
}

async function handleSave() {
  if (!formRef.value) return
  try {
    await formRef.value.validate()
  } catch {
    activeTab.value = 'info'
    return
  }
  const payload = buildPayload()
  loading.value = true
  try {
    if (isEdit.value) {
      const { fNumber, ...updateData } = payload
      await updateMaterial(uid.value, updateData)
      await saveMaterialDimensions(uid.value, dimensions.value)
      await saveMaterialSecUnits(uid.value, secUnits.value)
      ElMessage.success('保存成功')
      await loadDetail(uid.value)
      await loadDimensions(uid.value)
      await loadSecUnits(uid.value)
    } else {
      const res: any = await createMaterial(payload)
      ElMessage.success('创建成功')
      const newUid = res?.data?.uid
      if (newUid) {
        uid.value = newUid
        router.replace({ name: 'MaterialEdit', query: { uid: newUid } })
        await loadDetail(newUid)
        await loadDimensions(newUid)
        await loadSecUnits(newUid)
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

const handleApprove = () => runStatusAction(approveMaterial, '审核成功')
const handleUnapprove = () => runStatusAction(unapproveMaterial, '反审核成功')
const handleDisable = () => runStatusAction(disableMaterial, '禁用成功')
const handleEnable = () => runStatusAction(enableMaterial, '反禁用成功')

const handleBack = () => {
  router.push({ name: 'MaterialList' })
}

onMounted(async () => {
  if (!orgStore.loaded) orgStore.loadOrgs()
  await loadGroupTree()
  if (isEdit.value) {
    await loadDetail(uid.value)
    await loadDimensions(uid.value)
    await loadSecUnits(uid.value)
  } else {
    // 新增：默认使用当前切换的组织
    form.fCompanyId = orgStore.currentOrgId
    if (route.query.groupId) form.fGroupId = route.query.groupId as string
  }
})
</script>

<style scoped>
.material-edit-container {
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

.section-title {
  font-size: 14px;
  font-weight: 600;
  color: var(--text-primary);
  margin: 16px 0 12px;
  padding-left: 8px;
  border-left: 3px solid var(--primary-color);
}

.edit-tabs {
  margin-top: 8px;
}

.grid-toolbar {
  margin-bottom: 8px;
}

.picture-uploader {
  display: flex;
  align-items: flex-start;
  gap: 8px;
}

.picture-preview {
  margin-left: 12px;
}

.picture-preview img {
  max-width: 180px;
  max-height: 180px;
  border: 1px solid var(--border-color);
  border-radius: 4px;
  object-fit: contain;
}
</style>
