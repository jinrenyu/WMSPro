namespace OPSOFT.O3.WebAPI.Application.DTOs;

/// <summary>编码规则可配置的业务表单</summary>
public class BillCodeFormDto
{
    public string FormKey { get; set; } = string.Empty;
    public string FormName { get; set; } = string.Empty;
    /// <summary>单据表头实体类名（基类反射取号匹配用；留空=由代码显式声明 formKey）</summary>
    public string EntityName { get; set; } = string.Empty;
}

/// <summary>登记/更新一个可配置业务表单（数据驱动目录）</summary>
public class BillCodeFormRegisterRequest
{
    public string FormKey { get; set; } = string.Empty;
    public string FormName { get; set; } = string.Empty;
    /// <summary>单据表头实体类名（选填，填了基类即可按实体反射自动取号）</summary>
    public string EntityName { get; set; } = string.Empty;
}

/// <summary>编码规则"登记新单据"可选单据（反射系统已注册单据 + 登记状态）：供下拉选择、自动带出技术标识</summary>
public class BillCodeDocumentDto
{
    /// <summary>编码规则表单标识（已登记取登记值，否则取建议值）</summary>
    public string FormKey { get; set; } = string.Empty;
    /// <summary>中文友好名（界面显示，可改）</summary>
    public string FormName { get; set; } = string.Empty;
    /// <summary>表头实体类名（技术标识，供基类反射取号匹配）</summary>
    public string EntityName { get; set; } = string.Empty;
    /// <summary>真实表名（仅展示）</summary>
    public string TableName { get; set; } = string.Empty;
    /// <summary>是否已在目录登记（已登记的下拉里置灰，避免重复登记）</summary>
    public bool Registered { get; set; }
}

/// <summary>编码规则可用动态字段（白名单项）</summary>
public class BillCodeFieldDto
{
    public string FieldId { get; set; } = string.Empty;
    public string FieldName { get; set; } = string.Empty;
    /// <summary>字段种类：text=文本（段类型2）、date=日期（段类型3）</summary>
    public string Kind { get; set; } = "text";
    /// <summary>示例值（前端预览与服务端长度校验用）</summary>
    public string Sample { get; set; } = string.Empty;
}

/// <summary>编码规则列表行（按业务表单一行，概览单据/条码两套规则）</summary>
public class BillCodeRuleListDto
{
    public string FormKey { get; set; } = string.Empty;
    public string FormName { get; set; } = string.Empty;
    public bool BillConfigured { get; set; }
    public string BillRuleName { get; set; } = string.Empty;
    public string BillSummary { get; set; } = string.Empty;
    public bool BarcodeConfigured { get; set; }
    public string BarcodeRuleName { get; set; } = string.Empty;
    public string BarcodeSummary { get; set; } = string.Empty;
    /// <summary>单据编号规则是否已禁用</summary>
    public bool BillDisabled { get; set; }
    /// <summary>条码编号规则是否已禁用</summary>
    public bool BarcodeDisabled { get; set; }
    public DateTime? MYmd { get; set; }
}

/// <summary>编码规则分段（SYS_LISTCODEENTRY / SYS_BARCODEENTRY 同构）</summary>
public class BillCodeSegmentDto
{
    /// <summary>段类型：1=常量 2=文本字段 3=日期字段 4=流水号（旧"5=数值字段"已并入文本字段）</summary>
    public string Ftype { get; set; } = "1";
    /// <summary>元素来源字段（段类型 2/3，取 BillCodeFields 白名单键）</summary>
    public string Fieldid { get; set; } = string.Empty;
    public string FieldName { get; set; } = string.Empty;
    /// <summary>设置值（常量段的固定文本）</summary>
    public string Fvalue { get; set; } = string.Empty;
    /// <summary>长度：流水段=位数补零；字段段=截取/补位长度（0=不限）</summary>
    public int Flenght { get; set; }
    /// <summary>起始流水号（流水段，默认 1）</summary>
    public int Fmin { get; set; }
    /// <summary>最大流水号（流水段，0=不限制）</summary>
    public int Fmax { get; set; }
    /// <summary>流水号步长（流水段，默认 1）</summary>
    public int Fstep { get; set; }
    /// <summary>格式：字段段 U=大写 L=小写；日期段为日期格式（如 yyyyMMdd）</summary>
    public string FormatString { get; set; } = string.Empty;
    /// <summary>补位符（单字符，字段段长度不足时使用）</summary>
    public string Fchar { get; set; } = string.Empty;
    /// <summary>补位方向：L=左补 R=右补</summary>
    public string FcharAlignment { get; set; } = string.Empty;
    /// <summary>编码依据：勾选的段值拼成流水键（日期段勾选=按日重置；字段段勾选=按字段值分组取号）</summary>
    public bool IsSerial { get; set; }
    /// <summary>编码元素：是否参与编号拼接（不勾选时仅可作依据）</summary>
    public bool IsMember { get; set; } = true;
    public string Fnote { get; set; } = string.Empty;
}

/// <summary>一套规则（规则头 + 分段）：对应 SYS_LISTCODE 或 SYS_BARCODE 一行</summary>
public class BillCodeRuleSectionDto
{
    public string Fname { get; set; } = string.Empty;
    /// <summary>是否允许手工指定编号（仅单据编号规则生效）</summary>
    public bool Ismodify { get; set; } = true;
    /// <summary>是否禁用该规则（禁用后取号被拦截，手工/自动一致）</summary>
    public bool Disabled { get; set; }
    public string Fnote { get; set; } = string.Empty;
    public List<BillCodeSegmentDto> Segments { get; set; } = new();
}

/// <summary>编码规则详情（单据编号 + 条码编号 + 各自可用字段白名单）</summary>
public class BillCodeRuleDetailDto
{
    public string FormKey { get; set; } = string.Empty;
    public string FormName { get; set; } = string.Empty;
    public BillCodeRuleSectionDto Bill { get; set; } = new();
    public BillCodeRuleSectionDto Barcode { get; set; } = new();
    public List<BillCodeFieldDto> BillFields { get; set; } = new();
    public List<BillCodeFieldDto> BarcodeFields { get; set; } = new();
}

/// <summary>保存编码规则请求（分段整删整插；Segments 为空 = 清空该规则）</summary>
public class SaveBillCodeRuleRequest
{
    public BillCodeRuleSectionDto? Bill { get; set; }
    public BillCodeRuleSectionDto? Barcode { get; set; }
}

/// <summary>取号预览（配置页"试取号"：按已保存规则读当前流水算下一个号，不占号）</summary>
public class BillCodePreviewDto
{
    /// <summary>是否已配置（有规则头且有分段）</summary>
    public bool Configured { get; set; }
    /// <summary>规则是否已禁用</summary>
    public bool Disabled { get; set; }
    /// <summary>流水键（编码依据拼成，空串=全局单一流水）</summary>
    public string BasisKey { get; set; } = string.Empty;
    /// <summary>该流水键当前已占用到的流水值（0=尚未取过号）</summary>
    public int CurrentSeq { get; set; }
    /// <summary>下一个将生成的编号（用字段白名单样例值 + 当前日期估算）</summary>
    public string NextNo { get; set; } = string.Empty;
    /// <summary>下一个编号的字节长度（生产 VARCHAR(50) 按字节计）</summary>
    public int ByteLength { get; set; }
    /// <summary>无法预览时的说明（未配置/字段缺值/格式错误等）</summary>
    public string Message { get; set; } = string.Empty;
}

/// <summary>一张表单的单据编号 + 条码编号两套规则的取号预览</summary>
public class BillCodeRulePreviewDto
{
    public BillCodePreviewDto Bill { get; set; } = new();
    public BillCodePreviewDto Barcode { get; set; } = new();
}
