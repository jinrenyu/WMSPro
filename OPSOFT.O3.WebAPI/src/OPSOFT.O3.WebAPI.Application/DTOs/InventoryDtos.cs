namespace OPSOFT.O3.WebAPI.Application.DTOs;

/// <summary>
/// 即时库存列表行（真实表 T_STK_INVENTORY，按物料/仓库/仓位/批次等维度一行一条；解析关联名称）
/// </summary>
public class InventoryListDto
{
    /// <summary>库存行主键</summary>
    public string Uid { get; set; } = string.Empty;

    // ── 物料 ──
    /// <summary>物料代码</summary>
    public string FmaterialNumber { get; set; } = string.Empty;
    /// <summary>物料名称</summary>
    public string FmaterialName { get; set; } = string.Empty;
    /// <summary>规格型号</summary>
    public string FSpecification { get; set; } = string.Empty;

    /// <summary>批次</summary>
    public string Flot { get; set; } = string.Empty;
    /// <summary>辅助属性</summary>
    public string FauxpropName { get; set; } = string.Empty;

    // ── 数量 / 单位 ──
    /// <summary>数量（库存主单位数量）</summary>
    public decimal Fqty { get; set; }
    /// <summary>单位（库存主单位）名称</summary>
    public string FstockunitName { get; set; } = string.Empty;
    /// <summary>基本单位数量</summary>
    public decimal Fbaseunitqty { get; set; }
    /// <summary>基本单位名称</summary>
    public string FbaseunitName { get; set; } = string.Empty;
    /// <summary>辅助单位数量</summary>
    public decimal Fsecunitqty { get; set; }
    /// <summary>辅助单位名称</summary>
    public string FsecunitName { get; set; } = string.Empty;

    // ── 仓库 / 仓位 ──
    /// <summary>仓库代码</summary>
    public string FstockNumber { get; set; } = string.Empty;
    /// <summary>仓库名称</summary>
    public string FstockName { get; set; } = string.Empty;
    /// <summary>仓位代码</summary>
    public string FstocklocNumber { get; set; } = string.Empty;
    /// <summary>仓位名称</summary>
    public string FstocklocName { get; set; } = string.Empty;

    // ── 保质期 ──
    /// <summary>是否启用保质期（取自物料）</summary>
    public bool FisKfPeriod { get; set; }
    /// <summary>生产日期/采购日期</summary>
    public DateTime? Fkfdate { get; set; }
    /// <summary>保质期限（取自物料）</summary>
    public int FKfPeriod { get; set; }
    /// <summary>保质期单位（取自物料：0=日 1=月 2=年）</summary>
    public int FKfUnit { get; set; }
    /// <summary>有效期至</summary>
    public DateTime? Fusefuldate { get; set; }

    // ── 状态 / 组织 ──
    /// <summary>库存状态名称</summary>
    public string FstockstatusName { get; set; } = string.Empty;
    /// <summary>库存组织名称</summary>
    public string FstockorgName { get; set; } = string.Empty;
    /// <summary>组织名称</summary>
    public string FcompanyName { get; set; } = string.Empty;
}
