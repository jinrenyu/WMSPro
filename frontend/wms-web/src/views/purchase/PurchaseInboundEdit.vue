<template>
  <div class="in-edit-container">
    <!-- 顶部工具栏 -->
    <div class="edit-toolbar">
      <el-button type="primary" :disabled="isReadonly" @click="handleSave"
                 v-permission="isEdit ? 'purchasein:edit' : 'purchasein:add'">
        <el-icon><Check /></el-icon> 保存
      </el-button>
      <el-button v-if="isEdit && form.fStatus === 10" type="success" @click="handleApprove" v-permission="'purchasein:approve'">
        <el-icon><CircleCheck /></el-icon> 审核
      </el-button>
      <el-button v-if="isEdit && form.fStatus === 40" type="warning" @click="handleUnapprove" v-permission="'purchasein:approve'">
        <el-icon><RefreshLeft /></el-icon> 反审核
      </el-button>
      <div class="toolbar-spacer" />
      <el-tag v-if="isEdit" :type="form.fStatus === 40 ? 'success' : form.fStatus === 70 ? 'info' : 'warning'" size="large">
        {{ form.fStatus === 40 ? '已审核' : form.fStatus === 70 ? '已关闭' : '未审核' }}
      </el-tag>
      <el-button class="back-btn" @click="handleBack"><el-icon><Back /></el-icon> 退出</el-button>
    </div>

    <el-form ref="formRef" :model="form" :rules="rules" label-width="84px"
             :disabled="isReadonly" class="edit-form" v-loading="loading">
      <!-- 单据头 -->
      <div class="form-header">
        <el-row :gutter="16">
          <el-col :span="8">
            <el-form-item label="单据类型" prop="fbilltypeid">
              <LookupSelect v-model="form.fbilltypeid" module="billtype" parent-id="STK_InStock" placeholder="请选择单据类型" preload />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="单据编号">
              <el-input v-model="form.fbillno" :disabled="isEdit" placeholder="保存后自动生成" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="入库日期" prop="fdate">
              <el-date-picker v-model="form.fdate" type="date" value-format="YYYY-MM-DD" placeholder="选择日期" style="width:100%" />
            </el-form-item>
          </el-col>
        </el-row>
      </div>

      <el-tabs v-model="activeTab" class="edit-tabs">
        <!-- 基本 -->
        <el-tab-pane label="基本" name="basic">
          <el-row :gutter="16">
            <el-col :span="6">
              <el-form-item label="录入类型">
                <el-select v-model="form.ftypeid" placeholder="录入类型" style="width:100%" @change="onEntryTypeChange">
                  <el-option label="物料" :value="1" />
                  <el-option label="条码" :value="2" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="源单类型">
                <el-select v-model="form.fsrcformid" placeholder="源单类型" style="width:100%" @change="onSrcFormChange">
                  <el-option v-for="t in sourceTypes" :key="t.value" :label="t.label" :value="t.value" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="源单编号">
                <PurchaseOrderLookup v-if="form.fsrcformid === 'PUR_PurchaseOrder'"
                                     v-model="form.fsrcbillno" :display-text="form.fsrcbillno"
                                     :disabled="isReadonly" placeholder="选择采购订单" @change="onSrcOrderChange" />
                <ReceiveNoticeLookup v-else-if="form.fsrcformid === 'PUR_ReceiveBill'"
                                     v-model="form.fsrcbillno" :display-text="form.fsrcbillno"
                                     :disabled="isReadonly" placeholder="选择收料通知单" @change="onSrcOrderChange" />
                <el-input v-else v-model="form.fsrcbillno" :disabled="!form.fsrcformid" placeholder="无源单" />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="需求组织">
                <el-select v-model="form.fdemandorgid" placeholder="需求组织" style="width:100%">
                  <el-option v-for="o in demandOrgOptions" :key="o.orgId" :label="o.orgName" :value="o.orgId" />
                </el-select>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="6">
              <el-form-item label="采购组织">
                <el-select v-model="form.fpurchaseorgid" placeholder="采购组织" style="width:100%">
                  <el-option v-for="o in purOrgOptions" :key="o.orgId" :label="o.orgName" :value="o.orgId" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="采购部门">
                <LookupSelect v-model="form.fpurchasedeptid" module="department" placeholder="请选择采购部门" preload />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="采购员">
                <LookupSelect v-model="form.fpurchaserid" module="employee" placeholder="请选择采购员" preload />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="币别">
                <LookupSelect v-model="form.fcurrencyid" module="currency" placeholder="请选择币别" preload />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="6">
              <el-form-item label="收料组织">
                <el-select v-model="form.fcompanyid" placeholder="收料组织" style="width:100%">
                  <el-option v-for="o in companyOrgOptions" :key="o.orgId" :label="o.orgName" :value="o.orgId" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="收料部门">
                <LookupSelect v-model="form.fmrdeptid" module="department" placeholder="请选择收料部门" preload />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="仓管员">
                <LookupSelect v-model="form.fstockerid" module="employee" placeholder="请选择仓管员" preload />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="汇率">
                <el-input v-model="form.fexchangerate" placeholder="汇率" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="6">
              <el-form-item label="仓库">
                <LookupSelect v-model="form.fstockid" module="warehouse" placeholder="表头默认仓库" preload @change="onHeaderStockChange" />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="仓位">
                <LookupSelect v-model="form.fstocklocid" module="stockplace" :parent-id="form.fstockid"
                              :placeholder="form.fisOpenLocation ? '请选择仓位' : '未启用仓位管理'"
                              :disabled="!form.fstockid || !form.fisOpenLocation" preload />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="库存状态">
                <LookupSelect v-model="form.fstockstatusid" module="stockstatus" placeholder="库存状态" preload />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="供应商" prop="fsupplyid">
                <LookupSelect v-model="form.fsupplyid" module="supplier" placeholder="请选择供应商" preload />
              </el-form-item>
            </el-col>
          </el-row>
        </el-tab-pane>

        <!-- 其他 -->
        <el-tab-pane label="其他" name="other">
          <el-row :gutter="16">
            <el-col :span="8"><el-form-item label="制单人"><el-input :model-value="form.cuserName" disabled /></el-form-item></el-col>
            <el-col :span="8"><el-form-item label="制单日期"><el-input :model-value="fmtAuditDate(form.cYmd)" disabled /></el-form-item></el-col>
            <el-col :span="8"><el-form-item label="数据状态"><el-input :model-value="form.fstatusName" disabled /></el-form-item></el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="8"><el-form-item label="审核人"><el-input :model-value="form.fcheckerName" disabled /></el-form-item></el-col>
            <el-col :span="8"><el-form-item label="审核日期"><el-input :model-value="fmtAuditDate(form.fcheckdate)" disabled /></el-form-item></el-col>
            <el-col :span="8"></el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="8"><el-form-item label="修改人"><el-input :model-value="form.muserName" disabled /></el-form-item></el-col>
            <el-col :span="8"><el-form-item label="修改日期"><el-input :model-value="fmtAuditDate(form.mYmd)" disabled /></el-form-item></el-col>
            <el-col :span="8"></el-col>
          </el-row>
          <el-row :gutter="16">
            <el-col :span="8"><el-form-item label="禁用人"><el-input :model-value="form.fdisableName" disabled /></el-form-item></el-col>
            <el-col :span="8"><el-form-item label="禁用日期"><el-input :model-value="fmtAuditDate(form.fdisabledate)" disabled /></el-form-item></el-col>
            <el-col :span="8"><el-form-item label="禁用"><el-checkbox :model-value="!!form.fDisabled" disabled /></el-form-item></el-col>
          </el-row>
        </el-tab-pane>
      </el-tabs>

      <!-- 扫描条码 -->
      <div class="scan-section">
        <span class="scan-label">扫描条码</span>
        <el-input v-model="scanCode" class="scan-input" :disabled="form.ftypeid !== 2 || isReadonly"
                  :placeholder="form.ftypeid === 2 ? '扫描或输入条码后回车' : '录入类型为「条码」时可用'"
                  @keyup.enter="onScan">
          <template #append>
            <el-button :disabled="form.ftypeid !== 2 || isReadonly" :loading="scanning" @click="onScan">扫码</el-button>
          </template>
        </el-input>
      </div>

      <!-- 明细：录入明细(条码) / 物料汇总明细 -->
      <div class="grid-section">
        <el-tabs v-model="detailTab" class="detail-tabs">
          <!-- 录入明细（ENTRY1 条码级） -->
          <el-tab-pane label="录入明细" name="barcode">
            <div class="grid-toolbar">
              <span class="grid-tip">扫码自动生成；箱码保存时按装箱清单展开底阶条码</span>
              <div class="toolbar-spacer" />
              <el-button size="small" type="danger" :disabled="isReadonly || selectedBarcodeIndex < 0" @click="deleteBarcodeLine">行删除</el-button>
            </div>
            <el-table :data="barcodeLines" border size="small" row-key="_key" highlight-current-row
                      :row-class-name="(s:any) => barcodeRowClass(s)" @row-click="onBarcodeRowClick" empty-text="暂无条码，请在上方扫描">
              <el-table-column type="index" label="行号" width="55" align="center" fixed="left" />
              <el-table-column label="录入类型" width="80" align="center">
                <template #default="{ row }">{{ row.ftypeid === 2 ? '条码' : '物料' }}</template>
              </el-table-column>
              <el-table-column label="是否箱码" width="80" align="center">
                <template #default="{ row }"><el-checkbox :model-value="!!row.fisbox" disabled /></template>
              </el-table-column>
              <el-table-column prop="fboxbarcode" label="箱码" width="160" />
              <el-table-column prop="fbarcode" label="条码" width="160" fixed="left" />
              <el-table-column label="条码类型" width="90" align="center">
                <template #default="{ row }">{{ barTypeLabel(row.fbartype) }}</template>
              </el-table-column>
              <el-table-column prop="fmaterialNumber" label="物料代码" width="140" />
              <el-table-column prop="fmaterialName" label="物料名称" min-width="150" />
              <el-table-column prop="fSpecification" label="规格型号" min-width="120" />
              <el-table-column prop="fauxpropName" label="辅助属性" width="110" />
              <el-table-column label="启用批号管理" width="100" align="center">
                <template #default="{ row }"><el-checkbox :model-value="!!row.fisBatchManage" disabled /></template>
              </el-table-column>
              <el-table-column prop="flot" label="批次" width="120" />
              <el-table-column label="仓库" width="160">
                <template #default="{ row }">
                  <LookupSelect :key="'bs'+row._key" v-model="row.fstockid" module="warehouse" placeholder="仓库" :disabled="isReadonly" preload
                                @change="(it: any) => onBarcodeStockChange(row, it)" />
                </template>
              </el-table-column>
              <el-table-column prop="fstockNumber" label="仓库代码" width="110" />
              <el-table-column prop="fstockName" label="仓库名称" width="120" />
              <el-table-column label="启用仓位管理" width="100" align="center">
                <template #default="{ row }"><el-checkbox :model-value="!!row.fisOpenLocation" disabled /></template>
              </el-table-column>
              <el-table-column label="仓位" width="160">
                <template #default="{ row }">
                  <LookupSelect :key="'bl'+row._key" v-model="row.fstocklocid" module="stockplace" :parent-id="row.fstockid"
                                :placeholder="row.fisOpenLocation ? '请选择仓位' : '未启用仓位管理'"
                                :disabled="isReadonly || !row.fisOpenLocation" preload
                                @change="(it: any) => onBarcodeStockLocChange(row, it)" />
                </template>
              </el-table-column>
              <el-table-column prop="fqty" label="数量" width="100" align="right" />
              <el-table-column prop="funitName" label="单位名称" width="90" />
              <el-table-column label="启用辅助单位" width="100" align="center">
                <template #default="{ row }"><el-checkbox :model-value="!!row.fisSecUnit" disabled /></template>
              </el-table-column>
              <el-table-column prop="fsecunitNumber" label="辅助单位代码" width="110" />
              <el-table-column prop="fsecunitName" label="辅助单位名称" width="110" />
              <el-table-column prop="fsecunitqty" label="辅助单位数量" width="110" align="right" />
              <el-table-column label="入库类型" width="130">
                <template #default="{ row }">
                  <el-select v-model="row.fwwintype" :disabled="isReadonly" size="small" clearable placeholder="入库类型" style="width:100%">
                    <el-option v-for="o in wwInTypeOptions" :key="o.value" :label="o.label" :value="o.value" />
                  </el-select>
                </template>
              </el-table-column>
              <el-table-column label="源单类型" width="120">
                <template #default="{ row }">{{ srcFormLabel(row.fsrcformid) }}</template>
              </el-table-column>
              <el-table-column prop="fsrcbillno" label="源单编号" width="160" />
              <el-table-column prop="fsrcentryid" label="源单行号" width="90" align="center" />
              <el-table-column prop="forderbillno" label="订单编号" width="160" />
              <el-table-column prop="forderentryid" label="订单行号" width="90" align="center" />
              <el-table-column label="启用保质期" width="90" align="center">
                <template #default="{ row }"><el-checkbox :model-value="!!row.fisKfPeriod" disabled /></template>
              </el-table-column>
              <el-table-column label="保质日期" width="120">
                <template #default="{ row }">{{ fmtDate(row.fkfdate) }}</template>
              </el-table-column>
              <el-table-column prop="fKfPeriod" label="保质期限" width="90" align="right" />
              <el-table-column label="保质期单位" width="90" align="center">
                <template #default="{ row }">{{ kfUnitLabel(row.fKfUnit) }}</template>
              </el-table-column>
              <el-table-column label="有效期至" width="120">
                <template #default="{ row }">{{ fmtDate(row.fusefuldate) }}</template>
              </el-table-column>
              <el-table-column label="库存状态" width="150">
                <template #default="{ row }">
                  <LookupSelect :key="'bss'+row._key" v-model="row.fstockstatusid" module="stockstatus" placeholder="库存状态" :disabled="isReadonly" preload
                                @change="(it: any) => onBarcodeStockStatusChange(row, it)" />
                </template>
              </el-table-column>
              <el-table-column label="含税单价" width="110" align="right">
                <template #default="{ row }">{{ fmtNum(row.ftaxprice) }}</template>
              </el-table-column>
              <el-table-column prop="ftaxrate" label="税率%" width="80" align="right" />
              <el-table-column prop="fdiscountrate" label="折扣率%" width="90" align="right" />
            </el-table>
          </el-tab-pane>

          <!-- 物料汇总明细（ENTRY） -->
          <el-tab-pane label="物料汇总明细" name="material">
            <div class="grid-toolbar">
              <el-button type="primary" size="small" :disabled="isReadonly || form.ftypeid === 2" @click="addMaterialLine"><el-icon><Plus /></el-icon> 行新增</el-button>
              <el-button size="small" :disabled="isReadonly || form.ftypeid === 2" @click="insertMaterialLine">行插入</el-button>
              <el-button size="small" type="danger" :disabled="isReadonly || form.ftypeid === 2 || selectedMaterialIndex < 0" @click="deleteMaterialLine">行删除</el-button>
              <span v-if="form.ftypeid === 2" class="grid-tip">条码录入：本表由扫码按物料自动汇总（只读）</span>
            </div>
            <el-table :data="materialLines" border size="small" row-key="_key" highlight-current-row
                      :row-class-name="(s:any) => materialRowClass(s)" @row-click="onMaterialRowClick" empty-text="暂无明细">
              <el-table-column type="index" label="行号" width="55" align="center" fixed="left" />
              <el-table-column label="物料代码" min-width="200" fixed="left">
                <template #default="{ row }">
                  <MaterialLookup :key="row._key" v-model="row.fmaterialid" :display-text="row.fmaterialNumber"
                                  placeholder="选择物料" :disabled="isReadonly || form.ftypeid === 2"
                                  @change="(m: any) => onMaterialChange(row, m)" />
                </template>
              </el-table-column>
              <el-table-column prop="fmaterialName" label="物料名称" min-width="150" />
              <el-table-column prop="fSpecification" label="规格型号" min-width="120" />
              <el-table-column label="批次" width="120">
                <template #default="{ row }"><el-input v-model="row.flot" :disabled="isReadonly || form.ftypeid === 2" /></template>
              </el-table-column>
              <el-table-column label="启用辅助属性" width="100" align="center">
                <template #default="{ row }"><el-checkbox :model-value="!!row.fisAuxEnabled" disabled /></template>
              </el-table-column>
              <el-table-column label="辅助属性" width="170">
                <template #default="{ row }">
                  <LookupSelect v-if="row.fisAuxEnabled" :key="'a'+row._key" v-model="row.fauxpropid"
                                module="material/aux-properties" placeholder="选择辅助属性" :disabled="isReadonly || form.ftypeid === 2" preload
                                @change="(it: any) => onAuxChange(row, it)" />
                  <el-input v-else model-value="" disabled placeholder="未启用" />
                </template>
              </el-table-column>
              <el-table-column label="仓库" width="150">
                <template #default="{ row }">
                  <LookupSelect :key="'s'+row._key" v-model="row.fstockid" module="warehouse" placeholder="仓库" :disabled="isReadonly || form.ftypeid === 2" preload
                                @change="(it: any) => onStockChange(row, it)" />
                </template>
              </el-table-column>
              <el-table-column prop="fstockNumber" label="仓库代码" width="110" />
              <el-table-column prop="fstockName" label="仓库名称" width="110" />
              <el-table-column label="仓位" width="150">
                <template #default="{ row }">
                  <LookupSelect :key="'l'+row._key" v-model="row.fstocklocid" module="stockplace" :parent-id="row.fstockid"
                                :placeholder="row.fisOpenLocation ? '请选择仓位' : '未启用仓位管理'"
                                :disabled="isReadonly || form.ftypeid === 2 || !row.fisOpenLocation" preload
                                @change="(it: any) => onStockLocChange(row, it)" />
                </template>
              </el-table-column>
              <el-table-column label="库存状态" width="150">
                <template #default="{ row }">
                  <LookupSelect :key="'ss'+row._key" v-model="row.fstockstatusid" module="stockstatus" placeholder="库存状态" :disabled="isReadonly || form.ftypeid === 2" preload
                                @change="(it: any) => onStockStatusChange(row, it)" />
                </template>
              </el-table-column>
              <el-table-column label="实收数量" width="110">
                <template #default="{ row }">
                  <el-input-number v-model="row.frealqty" :min="0" :precision="4" :controls="false" :disabled="isReadonly || form.ftypeid === 2" style="width:100%" @change="() => recalcMaterial(row)" />
                </template>
              </el-table-column>
              <el-table-column label="应收数量" width="110">
                <template #default="{ row }">
                  <el-input-number v-model="row.fmustqty" :min="0" :precision="4" :controls="false" :disabled="isReadonly || form.ftypeid === 2" style="width:100%" />
                </template>
              </el-table-column>
              <el-table-column label="单位" width="120">
                <template #default="{ row }">
                  <LookupSelect :key="'u'+row._key" v-model="row.funitid" module="unit" placeholder="单位" :disabled="isReadonly || form.ftypeid === 2" preload
                                @change="(it: any) => onUnitChange(row, it)" />
                </template>
              </el-table-column>
              <el-table-column prop="funitNumber" label="单位代码" width="100" />
              <el-table-column prop="funitName" label="单位名称" width="100" />
              <el-table-column label="单价" width="110">
                <template #default="{ row }">
                  <el-input-number v-model="row.fprice" :min="0" :precision="6" :controls="false" :disabled="isReadonly || form.ftypeid === 2" style="width:100%" @change="() => recalcMaterial(row)" />
                </template>
              </el-table-column>
              <el-table-column label="含税单价" width="110" align="right">
                <template #default="{ row }">{{ fmtNum(row.ftaxprice) }}</template>
              </el-table-column>
              <el-table-column label="税率%" width="90">
                <template #default="{ row }">
                  <el-input-number v-model="row.ftaxrate" :min="0" :max="100" :precision="2" :controls="false" :disabled="isReadonly || form.ftypeid === 2" style="width:100%" @change="() => recalcMaterial(row)" />
                </template>
              </el-table-column>
              <el-table-column label="折扣率%" width="90">
                <template #default="{ row }">
                  <el-input-number v-model="row.fdiscountrate" :min="0" :max="100" :precision="2" :controls="false" :disabled="isReadonly || form.ftypeid === 2" style="width:100%" @change="() => recalcMaterial(row)" />
                </template>
              </el-table-column>
              <el-table-column label="折扣额" width="110" align="right">
                <template #default="{ row }">{{ fmtNum(row.fdiscount) }}</template>
              </el-table-column>
              <el-table-column label="税额" width="110" align="right">
                <template #default="{ row }">{{ fmtNum(row.ftaxamount) }}</template>
              </el-table-column>
              <el-table-column label="金额" width="110" align="right">
                <template #default="{ row }">{{ fmtNum(row.famount) }}</template>
              </el-table-column>
              <el-table-column label="价税合计" width="120" align="right">
                <template #default="{ row }">{{ fmtNum(row.fallamount) }}</template>
              </el-table-column>
              <el-table-column label="是否启用保质期" width="110" align="center">
                <template #default="{ row }"><el-checkbox :model-value="!!row.fisKfPeriod" disabled /></template>
              </el-table-column>
              <el-table-column label="保质日期" width="140">
                <template #default="{ row }">
                  <el-date-picker v-model="row.fkfdate" type="date" value-format="YYYY-MM-DD" placeholder="日期" :disabled="isReadonly || form.ftypeid === 2" style="width:100%" @change="() => recalcUseful(row)" />
                </template>
              </el-table-column>
              <el-table-column prop="fKfPeriod" label="保质期限" width="90" align="right" />
              <el-table-column label="保质期单位" width="90" align="center">
                <template #default="{ row }">{{ kfUnitLabel(row.fKfUnit) }}</template>
              </el-table-column>
              <el-table-column label="有效期至" width="140">
                <template #default="{ row }">
                  <el-date-picker v-model="row.fusefuldate" type="date" value-format="YYYY-MM-DD" placeholder="日期" :disabled="isReadonly || form.ftypeid === 2" style="width:100%" />
                </template>
              </el-table-column>
              <el-table-column label="入库类型" width="130">
                <template #default="{ row }">
                  <el-select v-model="row.fwwintype" :disabled="isReadonly || form.ftypeid === 2" size="small" clearable placeholder="入库类型" style="width:100%">
                    <el-option v-for="o in wwInTypeOptions" :key="o.value" :label="o.label" :value="o.value" />
                  </el-select>
                </template>
              </el-table-column>
              <el-table-column label="源单类型" width="120">
                <template #default="{ row }">{{ srcFormLabel(row.fsrcformid) }}</template>
              </el-table-column>
              <el-table-column prop="fsrcbillno" label="源单单号" width="160" />
              <el-table-column prop="fsrcentryid" label="源单行号" width="90" align="center" />
              <el-table-column prop="forderbillno" label="订单编号" width="150" />
              <el-table-column prop="forderentryid" label="订单行号" width="90" align="center" />
            </el-table>
          </el-tab-pane>
        </el-tabs>
      </div>
    </el-form>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import type { FormInstance, FormRules } from 'element-plus'
import { Check, Back, Plus, CircleCheck, RefreshLeft } from '@element-plus/icons-vue'
import {
  getInStock, createInStock, updateInStock,
  approveInStock, unapproveInStock, scanBarcode, getSourceBillTypes,
  getMaterialAuxEnabled, type SourceBillType
} from '../../api/purchaseInbound'
import { getPurchaseOrder } from '../../api/purchaseOrder'
import { getReceiveNotice } from '../../api/receiveNotice'
import { formatDate } from '../../utils/format'
import { useOrgStore } from '../../stores/org'
import LookupSelect from '../../components/LookupSelect.vue'
import MaterialLookup from '../../components/MaterialLookup.vue'
import PurchaseOrderLookup from '../../components/PurchaseOrderLookup.vue'
import ReceiveNoticeLookup from '../../components/ReceiveNoticeLookup.vue'

const router = useRouter()
const route = useRoute()
const orgStore = useOrgStore()

const formRef = ref<FormInstance>()
const loading = ref(false)
const scanning = ref(false)
const activeTab = ref('basic')
const detailTab = ref('material')
const scanCode = ref('')

// 入库类型下拉选项（存码值，显示中文）
const wwInTypeOptions = [
  { value: 'QLI', label: '合格入库' },
  { value: 'PSI', label: '工费入库' },
  { value: 'MSI', label: '料废入库' },
  { value: 'CRI', label: '让步接收入库' },
  { value: 'RFI', label: '不合格入库' },
]
const materialLines = ref<any[]>([])
const barcodeLines = ref<any[]>([])
const selectedMaterialIndex = ref(-1)
const selectedBarcodeIndex = ref(-1)
const sourceTypes = ref<SourceBillType[]>([])

const uid = ref<string>((route.query.uid as string) || '')
const isEdit = computed(() => !!uid.value)

const defaultForm = {
  uid: '', fStatus: 0,
  fbillno: '', fbilltypeid: '',
  ftypeid: 1,
  fdate: formatDate(new Date().toISOString()).slice(0, 10),
  fbusinesstype: '标准采购',
  fsrcformid: '', fsrcbillno: '',
  fdemandorgid: '', fdemandorgName: '',
  fpurchaseorgid: '', fpurchaseorgName: '',
  fcompanyid: '', fcompanyName: '',
  fpurchasedeptid: '', fmrdeptid: '',
  fpurchaserid: '', fstockerid: '', fempid: '',
  fsupplyid: '', fcurrencyid: '',
  fexchangetypeid: '固定汇率', fexchangerate: '1',
  fstockid: '', fstocklocid: '', fstockstatusid: '', fisOpenLocation: false,
  // 只读
  fstatusName: '',
  cuserName: '', cYmd: '',
  fcheckerName: '', fcheckdate: '',
  muserName: '', mYmd: '',
  fdisableName: '', fdisabledate: '', fDisabled: false
}

const form = reactive({ ...defaultForm })
// 仅草稿(10)可编辑；已审核(40)/已关闭(70)整单只读
const isReadonly = computed(() => isEdit.value && form.fStatus !== 10)

const rules: FormRules = {
  fbilltypeid: [{ required: true, message: '请选择单据类型', trigger: 'change' }],
  fdate: [{ required: true, message: '请选择入库日期', trigger: 'change' }],
  fsupplyid: [{ required: true, message: '请选择供应商', trigger: 'change' }]
}

let rowKeySeq = 0
const nextKey = () => ++rowKeySeq

const fmtAuditDate = (d?: string) => {
  if (!d) return ''
  const date = new Date(d)
  if (isNaN(date.getTime()) || date.getFullYear() <= 1900) return ''
  return formatDate(d)
}
const fmtDate = (d?: string) => fmtAuditDate(d)
const fmtNum = (n?: number) => (n == null ? '0' : Number(n).toFixed(2))
const barTypeLabel = (v?: number) => (v === 1 ? '单品' : v === 2 ? '最小包装' : v === 3 ? '批次' : '')
const kfUnitLabel = (v?: number) => (v === 0 ? '日' : v === 1 ? '月' : v === 2 ? '年' : '')
// 源单类型代码 -> 名称（取自数据驱动的源单类型下拉；空=无源单时显示空白）
const srcFormLabel = (v?: string) => {
  if (!v) return ''
  const t = sourceTypes.value.find(s => s.value === v)
  return t ? t.label : v
}
const toDateInput = (v?: string | null) => (fmtAuditDate(v || '') ? String(v).slice(0, 10) : null)

// 组织下拉：编辑态若单据组织不在当前用户可选组织里，补一项避免显示空白
const buildOrgOptions = (id: string, name: string) => {
  const list: any[] = [...orgStore.orgs]
  if (id && !list.some(o => o.orgId === id)) list.unshift({ orgId: id, orgName: name || id })
  return list
}
const demandOrgOptions = computed(() => buildOrgOptions(form.fdemandorgid, form.fdemandorgName))
const purOrgOptions = computed(() => buildOrgOptions(form.fpurchaseorgid, form.fpurchaseorgName))
const companyOrgOptions = computed(() => buildOrgOptions(form.fcompanyid, form.fcompanyName))

const materialRowClass = ({ rowIndex }: { rowIndex: number }) => (rowIndex === selectedMaterialIndex.value ? 'cur-row' : '')
const onMaterialRowClick = (row: any) => { selectedMaterialIndex.value = materialLines.value.indexOf(row) }
const barcodeRowClass = ({ rowIndex }: { rowIndex: number }) => (rowIndex === selectedBarcodeIndex.value ? 'cur-row' : '')
const onBarcodeRowClick = (row: any) => { selectedBarcodeIndex.value = barcodeLines.value.indexOf(row) }

// ===== 物料汇总明细（物料录入模式可编辑） =====
const newMaterialLine = () => ({
  fmaterialid: '', fmaterialNumber: '', fmaterialName: '', fSpecification: '',
  fisBatchManage: false, fisKfPeriod: false, fKfPeriod: 0, fKfUnit: 0,
  fauxpropid: '', fauxpropName: '', fisAuxEnabled: false,
  flot: '', fstockid: '', fstockNumber: '', fstockName: '', fisOpenLocation: false,
  fstocklocid: '', fstocklocName: '', fstockstatusid: '', fstockstatusName: '',
  frealqty: 0, fmustqty: 0,
  funitid: '', funitNumber: '', funitName: '', fbaseunitid: '', fbaseunitqty: 0,
  fsecunitid: '', fsecunitqty: 0, fwwintype: '',
  fkfdate: null as any, fusefuldate: null as any,
  fprice: 0, ftaxprice: 0, ftaxrate: 0, fdiscountrate: 0, fdiscount: 0, ftaxamount: 0, famount: 0, fallamount: 0,
  fsrcformid: '', fsrcbillno: '', fsrcentryid: 0,
  forderbillno: '', forderentryid: 0, forderinterid: '', forderdetailid: '',
  _key: nextKey()
})

const addMaterialLine = () => { materialLines.value.push(newMaterialLine()); selectedMaterialIndex.value = materialLines.value.length - 1 }
const insertMaterialLine = () => {
  const at = selectedMaterialIndex.value >= 0 ? selectedMaterialIndex.value : materialLines.value.length
  materialLines.value.splice(at, 0, newMaterialLine()); selectedMaterialIndex.value = at
}
const deleteMaterialLine = () => {
  if (selectedMaterialIndex.value < 0) return
  materialLines.value.splice(selectedMaterialIndex.value, 1)
  selectedMaterialIndex.value = materialLines.value.length > 0 ? Math.min(selectedMaterialIndex.value, materialLines.value.length - 1) : -1
}

const recalcMaterial = (row: any) => {
  const qty = Number(row.frealqty) || 0
  let price = Number(row.fprice) || 0
  const rate = Number(row.ftaxrate) || 0
  const disc = Number(row.fdiscountrate) || 0
  // 条码汇总行无未税单价时，从含税单价反算
  if (!price && row.ftaxprice) price = rate ? +(Number(row.ftaxprice) / (1 + rate / 100)).toFixed(6) : Number(row.ftaxprice)
  row.fprice = price
  row.fdiscount = +(qty * price * disc / 100).toFixed(2)        // 折扣额 = 数量×单价×折扣率%
  row.famount = +(qty * price * (1 - disc / 100)).toFixed(2)   // 金额 = 数量×单价×(1-折扣率%)
  row.ftaxamount = +(row.famount * rate / 100).toFixed(2)
  row.fallamount = +(row.famount + row.ftaxamount).toFixed(2)
  row.ftaxprice = +(price * (1 + rate / 100)).toFixed(6)        // 含税单价 = 单价×(1+税率%)
}

// 保质期至联动：保质日期 + 保质期限(按单位天/月/年) -> 有效期至。fKfUnit: 0=天,1=月,2=年
const recalcUseful = (row: any) => {
  if (!row.fisKfPeriod || !row.fkfdate || !row.fKfPeriod) return
  const d = new Date(row.fkfdate)
  if (isNaN(d.getTime())) return
  if (row.fKfUnit === 2) d.setFullYear(d.getFullYear() + Number(row.fKfPeriod))
  else if (row.fKfUnit === 1) d.setMonth(d.getMonth() + Number(row.fKfPeriod))
  else d.setDate(d.getDate() + Number(row.fKfPeriod))
  row.fusefuldate = `${d.getFullYear()}-${String(d.getMonth() + 1).padStart(2, '0')}-${String(d.getDate()).padStart(2, '0')}`
}

const onMaterialChange = async (row: any, item: any) => {
  row.fmaterialName = item?.fName || ''
  row.fmaterialNumber = item?.fNumber || ''
  row.fSpecification = item?.fSpecification || ''
  row.fisBatchManage = !!item?.fIsBatchManage
  row.fisKfPeriod = !!item?.fIsKfPeriod
  row.fKfPeriod = item?.fKfPeriod || 0
  row.fKfUnit = item?.fKfUnit || 0
  // 带出单位：业务单位(单据"单位"列)取采购单位；基本单位取基本单位。均用 if 守卫，避免覆盖已选/清空
  if (item?.fPurchaseUnitId) {
    row.funitid = item.fPurchaseUnitId
    row.funitName = item.fPurchaseUnitName || ''
    row.funitNumber = item.fPurchaseUnitNumber || ''
  }
  if (item?.fBaseUnitId) row.fbaseunitid = item.fBaseUnitId
  row.fauxpropid = ''
  row.fauxpropName = ''
  row.fisAuxEnabled = false
  // 物料带出保质期限/单位后，若该行已有保质日期则联动算出有效期至
  recalcUseful(row)
  if (item?.uid) {
    try {
      const res: any = await getMaterialAuxEnabled(item.uid)
      row.fisAuxEnabled = !!res.data
    } catch { row.fisAuxEnabled = false }
  }
}
const onAuxChange = (row: any, item: any) => { row.fauxpropName = item?.fName || '' }
const onUnitChange = (row: any, item: any) => { row.funitName = item?.fName || ''; row.funitNumber = item?.fNumber || '' }
const onStockChange = (row: any, item: any) => {
  row.fstockName = item?.fName || ''
  row.fstockNumber = item?.fNumber || ''
  row.fisOpenLocation = !!item?.fIsOpenLocation   // 带出"启用仓位管理"，决定仓位是否可编辑
  row.fstocklocid = ''                            // 换仓库后清空仓位（仓位按仓库级联）
  row.fstocklocName = ''
}
const onStockLocChange = (row: any, item: any) => { row.fstocklocName = item?.fName || '' }
const onStockStatusChange = (row: any, item: any) => { row.fstockstatusName = item?.fName || '' }
const onHeaderStockChange = (item: any) => {
  form.fisOpenLocation = !!item?.fIsOpenLocation   // 表头仓库的"启用仓位管理"，供表头仓位框及源单下推回落表头的行判定
  form.fstocklocid = ''                            // 换表头仓库后清空表头仓位
}

// 录入明细（条码行）手动编辑仓库/仓位：带出名称与"启用仓位管理"，并重算物料汇总（汇总维度含仓库/仓位）
const onBarcodeStockChange = (row: any, item: any) => {
  row.fstockName = item?.fName || ''
  row.fstockNumber = item?.fNumber || ''
  row.fisOpenLocation = !!item?.fIsOpenLocation
  row.fstocklocid = ''
  row.fstocklocName = ''
  recomputeAggregate()
}
const onBarcodeStockLocChange = (row: any, item: any) => {
  row.fstocklocName = item?.fName || ''
  recomputeAggregate()
}
const onBarcodeStockStatusChange = (row: any, item: any) => {
  row.fstockstatusName = item?.fName || ''
  recomputeAggregate()
}

// ===== 录入明细（条码扫描） =====
const onScan = async () => {
  const code = scanCode.value.trim()
  if (!code || form.ftypeid !== 2 || isReadonly.value) return
  scanning.value = true
  try {
    const res: any = await scanBarcode({
      fbarcode: code, fstockid: form.fstockid, fstocklocid: form.fstocklocid, fstockstatusid: form.fstockstatusid,
      fsrcformid: form.fsrcformid, fsrcbillno: form.fsrcbillno
    })
    const d = res.data
    if (!d?.found) { ElMessage.warning(d?.message || '条码不存在'); scanCode.value = ''; return }
    const e1 = d.entry1
    // 单内防重复扫描：同一条码/箱码不可重复录入（与后端去重一致，避免库存翻倍）
    const dupKey = (e1.fisbox && e1.fboxbarcode) ? e1.fboxbarcode : e1.fbarcode
    if (dupKey && barcodeLines.value.some(it => ((it.fisbox && it.fboxbarcode) ? it.fboxbarcode : it.fbarcode) === dupKey)) {
      ElMessage.warning(`条码 ${dupKey} 已扫描，请勿重复`); scanCode.value = ''; return
    }
    // 表头源单为空时，首次扫码自动按条码带出源单（之后再扫将以此源单校验归属）
    if (!form.fsrcbillno && e1.fsrcbillno) {
      if (e1.fsrcformid) form.fsrcformid = e1.fsrcformid
      form.fsrcbillno = e1.fsrcbillno
      ElMessage.info(`已按条码带出源单：${srcFormLabel(e1.fsrcformid)} ${e1.fsrcbillno}`)
    }
    barcodeLines.value.push({
      ...e1,
      fkfdate: toDateInput(e1.fkfdate),
      fusefuldate: toDateInput(e1.fusefuldate),
      _key: nextKey()
    })
    if (d.message) ElMessage.info(d.message)
    recomputeAggregate()
    scanCode.value = ''
    detailTab.value = 'barcode'
  } catch (e) {
    console.error('扫码失败:', e)
    ElMessage.error('扫码失败')
  } finally {
    scanning.value = false
  }
}

const deleteBarcodeLine = () => {
  if (selectedBarcodeIndex.value < 0) return
  barcodeLines.value.splice(selectedBarcodeIndex.value, 1)
  selectedBarcodeIndex.value = barcodeLines.value.length > 0 ? Math.min(selectedBarcodeIndex.value, barcodeLines.value.length - 1) : -1
  recomputeAggregate()
}

// 扫码后按物料维度汇总到物料明细（前端预览；后端保存时以服务端汇总为准）
const aggKey = (r: any) => [r.fmaterialid, r.flot, r.fstockid, r.fstocklocid, r.fstockstatusid, r.fauxpropid, r.funitid].join('|')
const recomputeAggregate = () => {
  const map = new Map<string, any>()
  for (const b of barcodeLines.value) {
    const k = aggKey(b)
    let g = map.get(k)
    if (!g) {
      g = {
        ...newMaterialLine(),
        fmaterialid: b.fmaterialid || '', fmaterialNumber: b.fmaterialNumber || '', fmaterialName: b.fmaterialName || '', fSpecification: b.fSpecification || '',
        fisBatchManage: !!b.fisBatchManage, flot: b.flot || '',
        fauxpropid: b.fauxpropid || '', fauxpropName: b.fauxpropName || '',
        fstockid: b.fstockid || '', fstockNumber: b.fstockNumber || '', fstockName: b.fstockName || '', fisOpenLocation: !!b.fisOpenLocation,
        fstocklocid: b.fstocklocid || '', fstocklocName: b.fstocklocName || '',
        fstockstatusid: b.fstockstatusid || '', fstockstatusName: b.fstockstatusName || '',
        funitid: b.funitid || '', funitNumber: b.funitNumber || '', funitName: b.funitName || '', fbaseunitid: b.fbaseunitid || '',
        fsecunitid: b.fsecunitid || '', fwwintype: b.fwwintype || '', fkfdate: b.fkfdate || null, fusefuldate: b.fusefuldate || null,
        ftaxprice: b.ftaxprice || 0, ftaxrate: b.ftaxrate || 0, fdiscountrate: b.fdiscountrate || 0,
        frealqty: 0, fmustqty: 0, fsecunitqty: 0
      }
      map.set(k, g)
    }
    g.frealqty += Number(b.fqty) || 0
    g.fsecunitqty += Number(b.fsecunitqty) || 0
  }
  const lines = Array.from(map.values())
  lines.forEach(l => { l.fmustqty = l.frealqty; recalcMaterial(l) })
  materialLines.value = lines
}

// ===== 录入类型切换 =====
const onEntryTypeChange = async (val: number) => {
  const hasData = barcodeLines.value.length > 0 || materialLines.value.some(it => it.fmaterialid)
  if (hasData) {
    const ok = await ElMessageBox.confirm('切换录入类型将清空已录入的明细，是否继续？', '切换录入类型', { type: 'warning' })
      .then(() => true).catch(() => false)
    if (!ok) { form.ftypeid = val === 2 ? 1 : 2; return }
  }
  barcodeLines.value = []
  materialLines.value = []
  selectedBarcodeIndex.value = -1
  selectedMaterialIndex.value = -1
  detailTab.value = val === 2 ? 'barcode' : 'material'
}

// ===== 源单下推 =====
const onSrcFormChange = () => { form.fsrcbillno = '' }

const onSrcOrderChange = async (order: any) => {
  if (!order?.uid) return
  if (materialLines.value.some(it => it.fmaterialid) || barcodeLines.value.length) {
    const ok = await ElMessageBox.confirm('选择源单将用其明细替换当前录入明细，是否继续？', '下推源单', { type: 'warning' })
      .then(() => true).catch(() => false)
    if (!ok) return
  }
  loading.value = true
  try {
    const isPO = form.fsrcformid === 'PUR_PurchaseOrder'
    const res: any = isPO ? await getPurchaseOrder(order.uid) : await getReceiveNotice(order.uid)
    const src = res.data
    if (!src) return
    // 下推按物料录入
    form.ftypeid = 1
    barcodeLines.value = []
    detailTab.value = 'material'
    // 表头仅当为空时补入
    if (!form.fsupplyid && src.fsupplyid) form.fsupplyid = src.fsupplyid
    if (!form.fcurrencyid && (src.fsettlecurrid || src.fcurrencyid)) form.fcurrencyid = src.fsettlecurrid || src.fcurrencyid
    if (!form.fpurchaserid && src.fpurchaserid) form.fpurchaserid = src.fpurchaserid
    if (!form.fpurchasedeptid && src.fpurchasedeptid) form.fpurchasedeptid = src.fpurchasedeptid
    if (src.fbusinesstype) form.fbusinesstype = src.fbusinesstype

    const srcEntries = src.entries || []
    materialLines.value = srcEntries.map((e: any) => {
      const qty = e.fqty != null ? e.fqty : (e.factreceiveqty || 0)
      const line: any = {
        ...newMaterialLine(),
        fmaterialid: e.fmaterialid || '', fmaterialNumber: e.fmaterialNumber || '', fmaterialName: e.fmaterialName || '', fSpecification: e.fSpecification || '',
        fisBatchManage: !!e.fisBatchManage, fisKfPeriod: !!e.fisKfPeriod, fKfPeriod: e.fKfPeriod || 0, fKfUnit: e.fKfUnit || 0,
        fauxpropid: e.fauxpropid || '', fauxpropName: e.fauxpropName || '', fisAuxEnabled: !!e.fisAuxEnabled,
        flot: e.flot || '',
        // 仓库/仓位：收料通知单行自带则用其值；采购订单行无该字段，回落表头默认仓库/仓位
        fstockid: e.fstockid || form.fstockid || '',
        fstockNumber: e.fstockNumber || '', fstockName: e.fstockName || '',
        // 行自带仓库用其"启用仓位管理"；回落表头仓库的行(如采购订单下推)取表头仓库的值，保存前即可正确启用/禁用仓位
        fisOpenLocation: e.fstockid ? !!e.fisOpenLocation : !!form.fisOpenLocation,
        fstocklocid: e.fstockid ? (e.fstocklocid || '') : (form.fstocklocid || ''),
        fstocklocName: e.fstocklocName || '',
        fstockstatusid: e.fstockstatusid || form.fstockstatusid || '',
        funitid: e.funitid || '', funitNumber: e.funitNumber || '', funitName: e.funitName || '',
        frealqty: qty, fmustqty: qty,
        fprice: e.fprice || 0, ftaxrate: e.ftaxrate || 0, fdiscountrate: e.fdiscountrate || 0,
        // 源单追溯
        fsrcformid: form.fsrcformid, fsrcbillno: src.fbillno || '', fsrcentryid: e.fentryid || 0,
        forderbillno: e.forderbillno || src.fbillno || '', forderentryid: e.fentryid || 0, forderinterid: src.uid || '', forderdetailid: e.uid || ''
      }
      recalcMaterial(line)
      return line
    })
    selectedMaterialIndex.value = materialLines.value.length > 0 ? 0 : -1
    if (materialLines.value.length) ElMessage.success(`已带入 ${materialLines.value.length} 条源单明细`)
    else ElMessage.warning('该源单无可下推的明细')
  } catch (e) {
    console.error('下推源单失败:', e)
    ElMessage.error('带入源单明细失败')
  } finally {
    loading.value = false
  }
}

// ===== 加载 / 保存 =====
async function loadDetail(id: string) {
  loading.value = true
  try {
    const res: any = await getInStock(id)
    const d = res.data
    Object.keys(defaultForm).forEach(k => { if (d[k] !== undefined && d[k] !== null) (form as any)[k] = d[k] })
    form.uid = d.uid
    form.fStatus = d.fStatus
    form.ftypeid = d.ftypeid || 1
    form.fdate = toDateInput(d.fdate) || ''
    form.fexchangerate = d.fexchangerate || '1'
    materialLines.value = (d.entries || []).map((e: any) => ({
      ...e, fkfdate: toDateInput(e.fkfdate), fusefuldate: toDateInput(e.fusefuldate), _key: nextKey()
    }))
    barcodeLines.value = (d.barcodeEntries || []).map((e: any) => ({
      ...e, fkfdate: toDateInput(e.fkfdate), fusefuldate: toDateInput(e.fusefuldate), _key: nextKey()
    }))
    detailTab.value = form.ftypeid === 2 ? 'barcode' : 'material'
    selectedMaterialIndex.value = materialLines.value.length > 0 ? 0 : -1
  } catch (e) {
    console.error('加载采购入库单失败:', e)
    ElMessage.error('加载采购入库单失败')
  } finally {
    loading.value = false
  }
}

function buildPayload() {
  const base: any = {
    fbillno: form.fbillno,
    fbilltypeid: form.fbilltypeid,
    ftypeid: form.ftypeid,
    fdate: form.fdate,
    fbusinesstype: form.fbusinesstype,
    fsrcformid: form.fsrcformid,
    fsrcbillno: form.fsrcbillno,
    fdemandorgid: form.fdemandorgid,
    fpurchaseorgid: form.fpurchaseorgid,
    fcompanyid: form.fcompanyid,
    fpurchasedeptid: form.fpurchasedeptid,
    fmrdeptid: form.fmrdeptid,
    fpurchaserid: form.fpurchaserid,
    fstockerid: form.fstockerid,
    fempid: form.fempid,
    fsupplyid: form.fsupplyid,
    fcurrencyid: form.fcurrencyid,
    fexchangetypeid: form.fexchangetypeid,
    fexchangerate: String(form.fexchangerate || '1'),
    fstockid: form.fstockid,
    fstocklocid: form.fstocklocid,
    fstockstatusid: form.fstockstatusid,
    entries: [] as any[],
    barcodeEntries: [] as any[]
  }
  if (form.ftypeid === 2) {
    base.barcodeEntries = barcodeLines.value.filter(it => it.fbarcode || it.fboxbarcode).map(it => ({
      fisbox: !!it.fisbox, fboxbarcode: it.fboxbarcode || '', fbarcode: it.fbarcode || '', fbartype: it.fbartype || 0,
      fmaterialid: it.fmaterialid || '', fauxpropid: it.fauxpropid || '', flot: it.flot || '',
      fstockid: it.fstockid || '', fstocklocid: it.fstocklocid || '', fstockstatusid: it.fstockstatusid || '',
      fqty: it.fqty || 0, funitid: it.funitid || '', fbaseunitid: it.fbaseunitid || '', fbaseunitqty: it.fbaseunitqty || 0,
      fsecunitid: it.fsecunitid || '', fsecunitqty: it.fsecunitqty || 0, fwwintype: it.fwwintype || '', fsupplyid: it.fsupplyid || '',
      fkfdate: it.fkfdate || null, fusefuldate: it.fusefuldate || null,
      ftaxprice: it.ftaxprice || 0, ftaxrate: it.ftaxrate || 0, fdiscountrate: it.fdiscountrate || 0,
      fsrcformid: it.fsrcformid || '', fsrcbillno: it.fsrcbillno || '', fsrcentryid: it.fsrcentryid || 0, fsrcdetailid: it.fsrcdetailid || '',
      fordertypeid: it.fordertypeid || '', forderinterid: it.forderinterid || '', forderbillno: it.forderbillno || '', forderdetailid: it.forderdetailid || '', forderentryid: it.forderentryid || 0
    }))
  } else {
    base.entries = materialLines.value.filter(it => it.fmaterialid).map(it => ({
      fmaterialid: it.fmaterialid, fauxpropid: it.fauxpropid || '', flot: it.flot || '',
      fstockid: it.fstockid || '', fstocklocid: it.fstocklocid || '', fstockstatusid: it.fstockstatusid || '',
      frealqty: it.frealqty || 0, fmustqty: it.fmustqty || 0,
      funitid: it.funitid || '', fbaseunitid: it.fbaseunitid || '', fbaseunitqty: it.fbaseunitqty || 0,
      fsecunitid: it.fsecunitid || '', fsecunitqty: it.fsecunitqty || 0, fwwintype: it.fwwintype || '',
      fkfdate: it.fkfdate || null, fusefuldate: it.fusefuldate || null,
      fprice: it.fprice || 0, ftaxrate: it.ftaxrate || 0, fdiscountrate: it.fdiscountrate || 0,
      fsrcformid: it.fsrcformid || '', fsrcbillno: it.fsrcbillno || '', fsrcentryid: it.fsrcentryid || 0,
      forderbillno: it.forderbillno || '', forderentryid: it.forderentryid || 0, forderinterid: it.forderinterid || '', forderdetailid: it.forderdetailid || ''
    }))
  }
  return base
}

// 明细行必填校验：数量(>0)、仓库、仓位(仓库启用仓位管理时)、库存状态。返回首个不合格行用于提示并定位
function validateDetailLines(): { ok: boolean; message?: string; tab?: string; index?: number } {
  if (form.ftypeid === 2) {
    for (let i = 0; i < barcodeLines.value.length; i++) {
      const r = barcodeLines.value[i]
      const tag = r.fbarcode || r.fboxbarcode || `第${i + 1}行`
      if (!(Number(r.fqty) > 0)) return { ok: false, message: `录入明细「${tag}」数量必须大于 0`, tab: 'barcode', index: i }
      if (!r.fstockid) return { ok: false, message: `录入明细「${tag}」请选择仓库`, tab: 'barcode', index: i }
      if (r.fisOpenLocation && !r.fstocklocid) return { ok: false, message: `录入明细「${tag}」仓库已启用仓位管理，请选择仓位`, tab: 'barcode', index: i }
      if (!r.fstockstatusid) return { ok: false, message: `录入明细「${tag}」请选择库存状态`, tab: 'barcode', index: i }
    }
  } else {
    for (let i = 0; i < materialLines.value.length; i++) {
      const r = materialLines.value[i]
      if (!r.fmaterialid) continue
      const tag = r.fmaterialName || r.fmaterialNumber || `第${i + 1}行`
      if (r.fisBatchManage && !String(r.flot || '').trim()) return { ok: false, message: `物料「${tag}」启用了批号管理，批次必填`, tab: 'material', index: i }
      if (!(Number(r.frealqty) > 0)) return { ok: false, message: `物料明细「${tag}」实收数量必须大于 0`, tab: 'material', index: i }
      if (!r.fstockid) return { ok: false, message: `物料明细「${tag}」请选择仓库`, tab: 'material', index: i }
      if (r.fisOpenLocation && !r.fstocklocid) return { ok: false, message: `物料明细「${tag}」仓库已启用仓位管理，请选择仓位`, tab: 'material', index: i }
      if (!r.fstockstatusid) return { ok: false, message: `物料明细「${tag}」请选择库存状态`, tab: 'material', index: i }
    }
  }
  return { ok: true }
}

async function handleSave() {
  if (!formRef.value) return
  try { await formRef.value.validate() } catch { activeTab.value = 'basic'; return }
  if (form.ftypeid === 2) {
    if (barcodeLines.value.length === 0) { ElMessage.warning('请先扫描条码'); detailTab.value = 'barcode'; return }
  } else {
    materialLines.value.forEach(it => recalcMaterial(it))
    if (buildPayload().entries.length === 0) { ElMessage.warning('请至少添加一条物料明细'); detailTab.value = 'material'; return }
  }
  // 明细行必填校验（数量/仓库/仓位/库存状态）
  const lineCheck = validateDetailLines()
  if (!lineCheck.ok) {
    ElMessage.warning(lineCheck.message!)
    if (lineCheck.tab) detailTab.value = lineCheck.tab
    if (lineCheck.index != null) {
      if (lineCheck.tab === 'barcode') selectedBarcodeIndex.value = lineCheck.index
      else selectedMaterialIndex.value = lineCheck.index
    }
    return
  }
  loading.value = true
  try {
    if (isEdit.value) {
      await updateInStock(uid.value, buildPayload())
      ElMessage.success('保存成功')
      await loadDetail(uid.value)
    } else {
      const res: any = await createInStock(buildPayload())
      ElMessage.success('创建成功')
      const newUid = res?.data?.uid
      if (newUid) {
        uid.value = newUid
        router.replace({ name: 'PurchaseInboundEdit', query: { uid: newUid } })
        await loadDetail(newUid)
      } else { handleBack() }
    }
  } catch (error: any) {
    ElMessage.error(error?.response?.data?.message || '提交失败')
  } finally {
    loading.value = false
  }
}

async function runStatus(fn: (id: string) => Promise<any>, msg: string) {
  if (!uid.value) return
  try { await fn(uid.value); ElMessage.success(msg); await loadDetail(uid.value) }
  catch (error: any) { ElMessage.error(error?.response?.data?.message || '操作失败') }
}
const handleApprove = () => runStatus(approveInStock, '审核成功')
const handleUnapprove = () => runStatus(unapproveInStock, '反审核成功')
const handleBack = () => router.push({ name: 'PurchaseInboundList' })

onMounted(async () => {
  if (!orgStore.loaded) await orgStore.loadOrgs()
  try {
    const res: any = await getSourceBillTypes()
    sourceTypes.value = res.data || []
  } catch { sourceTypes.value = [{ value: '', label: '无源单' }] }
  if (isEdit.value) await loadDetail(uid.value)
  else {
    if (!form.fdemandorgid) form.fdemandorgid = orgStore.currentOrgId
    if (!form.fpurchaseorgid) form.fpurchaseorgid = orgStore.currentOrgId
    if (!form.fcompanyid) form.fcompanyid = orgStore.currentOrgId
    // 下推进入：源单页带 query（srcform=源单模板, srcuid=源单Uid, srcbillno=源单号），复用引入逻辑自动带入明细
    const pushSrcForm = route.query.srcform as string
    const pushSrcUid = route.query.srcuid as string
    if (pushSrcForm && pushSrcUid) {
      form.fsrcformid = pushSrcForm
      form.fsrcbillno = (route.query.srcbillno as string) || ''
      await onSrcOrderChange({ uid: pushSrcUid })
    } else if (!form.fsrcformid && sourceTypes.value.length) {
      form.fsrcformid = sourceTypes.value[0].value
    }
  }
})
</script>

<style scoped>
.in-edit-container {
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
.toolbar-spacer { flex: 1; }
.back-btn { margin-left: 8px; }
.edit-form { padding: 16px 20px 24px; overflow-y: auto; }
.form-header { padding-bottom: 4px; border-bottom: 1px dashed var(--border-color, #ebeef5); margin-bottom: 4px; }
.edit-tabs { margin-top: 4px; }
.scan-section {
  display: flex;
  align-items: center;
  gap: 12px;
  margin-top: 12px;
  padding: 12px 16px;
  background-color: var(--el-fill-color-light);
  border-radius: 6px;
}
.scan-label { font-size: 18px; font-weight: 600; color: var(--el-text-color-primary); white-space: nowrap; }
.scan-input { flex: 1; max-width: 720px; }
.grid-section { margin-top: 12px; }
.grid-toolbar { display: flex; align-items: center; gap: 8px; margin-bottom: 8px; }
.grid-tip { color: var(--el-text-color-secondary); font-size: 12px; margin-left: 8px; }
.edit-form :deep(.el-table__body tr.cur-row > td.el-table__cell) {
  background-color: var(--el-color-primary-light-9);
}
</style>
