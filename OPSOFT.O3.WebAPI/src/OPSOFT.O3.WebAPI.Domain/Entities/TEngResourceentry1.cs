using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 资源表体-配置
/// </summary>
[SugarTable("T_ENG_RESOURCEENTRY1")]
public class TEngResourceentry1 : BaseEntity
{
    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 启用OPC连接
    /// </summary>
    [SugarColumn(ColumnName = "FUSEOPC")]
    public bool Fuseopc { get; set; }

    /// <summary>
    /// OPC IP地址
    /// </summary>
    [SugarColumn(ColumnName = "FOPCIP")]
    public string Fopcip { get; set; } = string.Empty;

    /// <summary>
    /// OPC协议类型
    /// </summary>
    [SugarColumn(ColumnName = "FOPCPROTOCOL")]
    public int Fopcprotocol { get; set; }

    /// <summary>
    /// 开工条码地址位
    /// </summary>
    [SugarColumn(ColumnName = "FPWSBARCODE")]
    public string Fpwsbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 汇报条码地址位
    /// </summary>
    [SugarColumn(ColumnName = "FPRPTBARCODE")]
    public string Fprptbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 汇报数据地址位
    /// </summary>
    [SugarColumn(ColumnName = "FPRPTDATA")]
    public string Fprptdata { get; set; } = string.Empty;

    /// <summary>
    /// 汇报数据解析脚本
    /// </summary>
    [SugarColumn(ColumnName = "FPYSHELL")]
    public string Fpyshell { get; set; } = string.Empty;

    /// <summary>
    /// 启用人脸识别
    /// </summary>
    [SugarColumn(ColumnName = "FUSEFACEREC")]
    public bool Fusefacerec { get; set; }

    /// <summary>
    /// 人脸识别类型
    /// </summary>
    [SugarColumn(ColumnName = "FFACEAPITYPE")]
    public int Ffaceapitype { get; set; }

    /// <summary>
    /// 人脸识别服务IP
    /// </summary>
    [SugarColumn(ColumnName = "FFACEAPIIP")]
    public string Ffaceapiip { get; set; } = string.Empty;

    /// <summary>
    /// 启用配方管理
    /// </summary>
    [SugarColumn(ColumnName = "FUSERECIPE")]
    public bool Fuserecipe { get; set; }

    /// <summary>
    /// 配方内码
    /// </summary>
    [SugarColumn(ColumnName = "FRECIPEID")]
    public string Frecipeid { get; set; } = string.Empty;

    /// <summary>
    /// 启用接口
    /// </summary>
    [SugarColumn(ColumnName = "FISAPI")]
    public bool Fisapi { get; set; }

    /// <summary>
    /// 开工接口地址
    /// </summary>
    [SugarColumn(ColumnName = "FAPIWORKSTART")]
    public string Fapiworkstart { get; set; } = string.Empty;

    /// <summary>
    /// 汇报接口
    /// </summary>
    [SugarColumn(ColumnName = "FAPIRPT")]
    public string Fapirpt { get; set; } = string.Empty;

    /// <summary>
    /// 开工信号地址位
    /// </summary>
    [SugarColumn(ColumnName = "FPWORKSTART")]
    public string Fpworkstart { get; set; } = string.Empty;

    /// <summary>
    /// 汇报信号地址位
    /// </summary>
    [SugarColumn(ColumnName = "FPREPORT")]
    public string Fpreport { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
