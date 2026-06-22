namespace OPSOFT.O3.WebAPI.Application.DTOs;

// ===== 出入库流程配置（真实表 T_BOS_SELBILL / T_BOS_SELBILLENTRY，一主一从）=====
// 配置后续单据（采购入库单、采购退料单等）的「源单类型」可取值，以及单据可下推为哪些目标单据。
// 表头无 FBILLNO：单据编号 = FNUMBER（系统生成，唯一）。
// 源单类型 FSOURCETRANTYPE / 目标单据类型 FDESTTRANTYPE 存 SYS_BILLTEMPLATE.FNUMBER，按 FNUMBER 解析名称。
// 明细 FSOURCEID / FDESTID 存 T_BAS_BILLTYPE.Uid，按 Uid 解析编号(FNUMBER)/名称(FNAME)。
// 状态 FSTATUS：10=草稿(未审)、40=审核(已审)、70=关闭。

/// <summary>
/// 出入库流程配置列表项（按主表一行，带解析后的名称列）
/// </summary>
public class SelBillListDto
{
    public string Uid { get; set; } = string.Empty;
    /// <summary>单据编号（= FNUMBER）</summary>
    public string Fnumber { get; set; } = string.Empty;
    /// <summary>单据名称</summary>
    public string Fname { get; set; } = string.Empty;
    /// <summary>源单类型编码（SYS_BILLTEMPLATE.FNUMBER）</summary>
    public string Fsourcetrantype { get; set; } = string.Empty;
    public string FsourcetypeName { get; set; } = string.Empty;
    /// <summary>目标单据类型编码（SYS_BILLTEMPLATE.FNUMBER）</summary>
    public string Fdesttrantype { get; set; } = string.Empty;
    public string FdesttranName { get; set; } = string.Empty;
    // 开关
    public bool Fiscontrolqty { get; set; }
    public bool Fisopensource { get; set; }
    public bool Fdefault { get; set; }
    public bool Fisuse { get; set; }
    public bool Fcheck { get; set; }
    public bool Fisdefaultstock { get; set; }
    // 状态 / 禁用
    public int FStatus { get; set; }
    public string FstatusName { get; set; } = string.Empty;
    public bool FDisabled { get; set; }
    // 制单 / 审核 / 禁用
    public string CuserName { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
    public string FcheckerName { get; set; } = string.Empty;
    public DateTime? Fcheckdate { get; set; }
    public string FdisableName { get; set; } = string.Empty;
    public DateTime? Fdisabledate { get; set; }
}

/// <summary>
/// 出入库流程配置-单据类型映射明细行
/// </summary>
public class SelBillEntryDto
{
    public string Uid { get; set; } = string.Empty;
    public int Fentryid { get; set; }
    /// <summary>源单类型（T_BAS_BILLTYPE.Uid）</summary>
    public string Fsourceid { get; set; } = string.Empty;
    /// <summary>源单编号（T_BAS_BILLTYPE.FNUMBER）</summary>
    public string FsourceNumber { get; set; } = string.Empty;
    /// <summary>源单名称（T_BAS_BILLTYPE.FNAME）</summary>
    public string FsourceName { get; set; } = string.Empty;
    /// <summary>目标单据类型（T_BAS_BILLTYPE.Uid）</summary>
    public string Fdestid { get; set; } = string.Empty;
    /// <summary>目标编号（T_BAS_BILLTYPE.FNUMBER）</summary>
    public string FdestNumber { get; set; } = string.Empty;
    /// <summary>目标名称（T_BAS_BILLTYPE.FNAME）</summary>
    public string FdestName { get; set; } = string.Empty;
    /// <summary>是否默认值</summary>
    public bool Fdefault { get; set; }
}

/// <summary>
/// 出入库流程配置详情（主表 + 名称解析 + 明细）
/// </summary>
public class SelBillDetailDto
{
    public string Uid { get; set; } = string.Empty;
    public string Fnumber { get; set; } = string.Empty;
    public string Fname { get; set; } = string.Empty;
    public string Fsourcetrantype { get; set; } = string.Empty;
    public string FsourcetypeName { get; set; } = string.Empty;
    public string Fdesttrantype { get; set; } = string.Empty;
    public string FdesttranName { get; set; } = string.Empty;
    // ---- 基本页签：开关 ----
    /// <summary>启用流程</summary>
    public bool Fisuse { get; set; }
    /// <summary>默认源单</summary>
    public bool Fdefault { get; set; }
    /// <summary>开放源单</summary>
    public bool Fisopensource { get; set; }
    /// <summary>提交审核</summary>
    public bool Fcheck { get; set; }
    /// <summary>控制数量</summary>
    public bool Fiscontrolqty { get; set; }
    /// <summary>带出仓库仓位</summary>
    public bool Fisdefaultstock { get; set; }
    /// <summary>同步到ERP</summary>
    public bool Fcansynerp { get; set; }
    /// <summary>实时同步到ERP（检验ERP单据）</summary>
    public bool Fcheckerpstock { get; set; }
    /// <summary>检查批次</summary>
    public bool Fischecklot { get; set; }
    /// <summary>检查辅助属性</summary>
    public bool Fischeckaux { get; set; }
    /// <summary>检查保质期</summary>
    public bool Fischeckkfdate { get; set; }
    /// <summary>启用条码解析（启用标签规则）</summary>
    public bool Fisusecoderule { get; set; }
    /// <summary>是否可以下推</summary>
    public bool Fcanpushdown { get; set; }
    /// <summary>是否整单录入</summary>
    public bool Fiswholeout { get; set; }
    /// <summary>条码类型</summary>
    public string Fkind { get; set; } = string.Empty;
    // ---- Wise配置页签 ----
    /// <summary>ERP存储过程</summary>
    public string Fproname { get; set; } = string.Empty;
    // ---- 状态 ----
    public int FStatus { get; set; }
    public string FstatusName { get; set; } = string.Empty;
    public bool FDisabled { get; set; }
    // ---- 其他页签：制单/审核/修改/禁用 ----
    public string CUser { get; set; } = string.Empty;
    public string CuserName { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
    public string MUser { get; set; } = string.Empty;
    public string MuserName { get; set; } = string.Empty;
    public DateTime? MYmd { get; set; }
    public string Fcheckerid { get; set; } = string.Empty;
    public string FcheckerName { get; set; } = string.Empty;
    public DateTime? Fcheckdate { get; set; }
    public string Fdisableid { get; set; } = string.Empty;
    public string FdisableName { get; set; } = string.Empty;
    public DateTime? Fdisabledate { get; set; }
    public List<SelBillEntryDto> Entries { get; set; } = new();
}

public class CreateSelBillEntryRequest
{
    /// <summary>源单类型（T_BAS_BILLTYPE.Uid）</summary>
    public string Fsourceid { get; set; } = string.Empty;
    /// <summary>目标单据类型（T_BAS_BILLTYPE.Uid）</summary>
    public string Fdestid { get; set; } = string.Empty;
    public bool Fdefault { get; set; }
}

public class CreateSelBillRequest
{
    /// <summary>单据编号（= FNUMBER）；为空时由服务端生成</summary>
    public string Fnumber { get; set; } = string.Empty;
    public string Fname { get; set; } = string.Empty;
    public string Fsourcetrantype { get; set; } = string.Empty;
    public string Fdesttrantype { get; set; } = string.Empty;
    public bool Fisuse { get; set; }
    public bool Fdefault { get; set; }
    public bool Fisopensource { get; set; }
    public bool Fcheck { get; set; }
    public bool Fiscontrolqty { get; set; }
    public bool Fisdefaultstock { get; set; }
    public bool Fcansynerp { get; set; }
    public bool Fcheckerpstock { get; set; }
    public bool Fischecklot { get; set; }
    public bool Fischeckaux { get; set; }
    public bool Fischeckkfdate { get; set; }
    public bool Fisusecoderule { get; set; }
    public bool Fcanpushdown { get; set; }
    public bool Fiswholeout { get; set; }
    public string Fkind { get; set; } = string.Empty;
    public string Fproname { get; set; } = string.Empty;
    public List<CreateSelBillEntryRequest> Entries { get; set; } = new();
}

public class UpdateSelBillRequest
{
    public string Fname { get; set; } = string.Empty;
    public string Fsourcetrantype { get; set; } = string.Empty;
    public string Fdesttrantype { get; set; } = string.Empty;
    public bool Fisuse { get; set; }
    public bool Fdefault { get; set; }
    public bool Fisopensource { get; set; }
    public bool Fcheck { get; set; }
    public bool Fiscontrolqty { get; set; }
    public bool Fisdefaultstock { get; set; }
    public bool Fcansynerp { get; set; }
    public bool Fcheckerpstock { get; set; }
    public bool Fischecklot { get; set; }
    public bool Fischeckaux { get; set; }
    public bool Fischeckkfdate { get; set; }
    public bool Fisusecoderule { get; set; }
    public bool Fcanpushdown { get; set; }
    public bool Fiswholeout { get; set; }
    public string Fkind { get; set; } = string.Empty;
    public string Fproname { get; set; } = string.Empty;
    public List<CreateSelBillEntryRequest> Entries { get; set; } = new();
}
