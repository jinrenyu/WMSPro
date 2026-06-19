namespace OPSOFT.O3.WebAPI.Application.DTOs;

/// <summary>
/// 采购标签打印 - 采购订单列表行（按采购订单明细行展开，仅已审核且非禁用）
/// </summary>
public class PurchaseOrderLabelListDto
{
    /// <summary>采购订单主表 Uid</summary>
    public string Uid { get; set; } = string.Empty;
    /// <summary>采购订单明细 Uid（条码生成页据此关联）</summary>
    public string EntryUid { get; set; } = string.Empty;
    /// <summary>行号</summary>
    public int? Fentryid { get; set; }
    /// <summary>订单日期</summary>
    public DateTime? Fdate { get; set; }
    /// <summary>单据编号</summary>
    public string Fbillno { get; set; } = string.Empty;
    /// <summary>采购组织名称</summary>
    public string FcompanyName { get; set; } = string.Empty;

    /// <summary>物料 Uid</summary>
    public string Fmaterialid { get; set; } = string.Empty;
    public string FmaterialNumber { get; set; } = string.Empty;
    public string FmaterialName { get; set; } = string.Empty;
    public string FSpecification { get; set; } = string.Empty;
    /// <summary>辅助属性名称</summary>
    public string FauxpropName { get; set; } = string.Empty;
    /// <summary>单位名称</summary>
    public string FunitName { get; set; } = string.Empty;

    /// <summary>供应商代码</summary>
    public string FsupplyNumber { get; set; } = string.Empty;
    /// <summary>供应商名称</summary>
    public string FsupplyName { get; set; } = string.Empty;

    /// <summary>采购数量</summary>
    public decimal Fqty { get; set; }
    /// <summary>累计入库数量</summary>
    public decimal Finstockqty { get; set; }
    /// <summary>最小包装量（取自物料）</summary>
    public decimal Fincreaseqty { get; set; }
    /// <summary>保质期限（取自物料）</summary>
    public int Fkfperiod { get; set; }

    /// <summary>数据状态（采购订单审核状态名）</summary>
    public int FStatus { get; set; }
    public string FstatusName { get; set; } = string.Empty;
    /// <summary>禁用</summary>
    public bool FDisabled { get; set; }

    /// <summary>制单人 / 制单日期</summary>
    public string CuserName { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
    /// <summary>审核人 / 审核日期</summary>
    public string FcheckerName { get; set; } = string.Empty;
    public DateTime? Fcheckdate { get; set; }
}

/// <summary>
/// 条码生成页 - 单据头（来源采购订单 + 选中明细行，只读展示 + 生成默认值）
/// </summary>
public class PurchaseOrderLabelHeadDto
{
    /// <summary>采购订单主表 Uid</summary>
    public string Uid { get; set; } = string.Empty;
    /// <summary>采购订单明细 Uid（生成条码的关联键 = FPODETAILID）</summary>
    public string EntryUid { get; set; } = string.Empty;
    public int? Fentryid { get; set; }

    public string Fbillno { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    public string FcompanyName { get; set; } = string.Empty;
    public string Fbusinesstype { get; set; } = string.Empty;

    public string Fsupplyid { get; set; } = string.Empty;
    public string FsupplyNumber { get; set; } = string.Empty;
    public string FsupplyName { get; set; } = string.Empty;

    public string Fmaterialid { get; set; } = string.Empty;
    public string FmaterialNumber { get; set; } = string.Empty;
    public string FmaterialName { get; set; } = string.Empty;
    public string FSpecification { get; set; } = string.Empty;

    /// <summary>辅助属性</summary>
    public string Fauxpropid { get; set; } = string.Empty;
    public string FauxpropName { get; set; } = string.Empty;

    /// <summary>采购数量</summary>
    public decimal Fqty { get; set; }
    /// <summary>采购单位</summary>
    public string Funitid { get; set; } = string.Empty;
    public string FunitName { get; set; } = string.Empty;
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>启用批次 / 订单批次</summary>
    public bool FisBatchManage { get; set; }
    public string Flot { get; set; } = string.Empty;

    /// <summary>启用保质期 / 保质期限 / 保质期单位</summary>
    public bool FisKfPeriod { get; set; }
    public int Fkfperiod { get; set; }
    public int Fkfunit { get; set; }

    /// <summary>物料默认条码类型（1=单品 2=最小包装量 3=批次），无则回落 1</summary>
    public int Fbartype { get; set; }

    /// <summary>最小包装量（生成时"包装数量"默认值）</summary>
    public decimal Fincreaseqty { get; set; }

    /// <summary>已生成条码数（含历史）</summary>
    public int GeneratedCount { get; set; }
}

/// <summary>
/// 条码明细行（T_BD_BARCODERS1 关联 T_BD_BARCODERS）
/// </summary>
public class BarcodeLineDto
{
    public string Uid { get; set; } = string.Empty;
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>条码状态（0=初始 1=已入库 9=作废）</summary>
    public int Fbarcodestatus { get; set; }
    public string FbarcodestatusName { get; set; } = string.Empty;
    /// <summary>库存状态（0=未入库 1=已入库）</summary>
    public int Fstockstatus { get; set; }
    public string FstockstatusName { get; set; } = string.Empty;

    public string FcompanyName { get; set; } = string.Empty;
    /// <summary>打印日期</summary>
    public DateTime? Fdate { get; set; }

    public string FmaterialNumber { get; set; } = string.Empty;
    public string FmaterialName { get; set; } = string.Empty;
    public string FSpecification { get; set; } = string.Empty;
    public string Flot { get; set; } = string.Empty;
    public string FauxpropName { get; set; } = string.Empty;

    public decimal Fqty { get; set; }
    public string FunitName { get; set; } = string.Empty;
    public decimal Fbaseqty { get; set; }
    public string FbaseunitName { get; set; } = string.Empty;

    /// <summary>采购日期/生产日期</summary>
    public DateTime? Fkfdate { get; set; }
    public int Fkfperiod { get; set; }
    /// <summary>有效期至</summary>
    public DateTime? Fusefuldate { get; set; }

    public decimal Fsecunitqty { get; set; }

    /// <summary>采购订单编号 / 收料单编号</summary>
    public string Fpobillno { get; set; } = string.Empty;
    public string Fpurbillno { get; set; } = string.Empty;
    public int Fpoentreyid { get; set; }
}

/// <summary>
/// 条码生成请求
/// </summary>
public class GenerateBarcodeRequest
{
    /// <summary>采购订单明细 Uid</summary>
    public string PoEntryUid { get; set; } = string.Empty;
    /// <summary>条码类型 1/2/3</summary>
    public int Fbartype { get; set; } = 1;
    /// <summary>本次打印数量</summary>
    public decimal PrintQty { get; set; }
    /// <summary>包装数量（每个条码的数量）</summary>
    public decimal PackageQty { get; set; }
    /// <summary>批次</summary>
    public string Flot { get; set; } = string.Empty;
    /// <summary>采购日期/生产日期</summary>
    public DateTime? Fkfdate { get; set; }
    /// <summary>有效期至</summary>
    public DateTime? Fusefuldate { get; set; }
    /// <summary>保质期限</summary>
    public int Fkfperiod { get; set; }
    /// <summary>保质期单位</summary>
    public int Fkfunit { get; set; }
    /// <summary>启用辅助数量</summary>
    public bool EnableAuxQty { get; set; }
    /// <summary>辅助单位</summary>
    public string Fsecunitid { get; set; } = string.Empty;
    /// <summary>辅助单位数量</summary>
    public decimal Fsecqty { get; set; }
    public string Fnote { get; set; } = string.Empty;
}

/// <summary>
/// 条码作废/反作废/打印 请求（按条码字符串）
/// </summary>
public class BarcodeActionRequest
{
    public List<string> Barcodes { get; set; } = new();
}
