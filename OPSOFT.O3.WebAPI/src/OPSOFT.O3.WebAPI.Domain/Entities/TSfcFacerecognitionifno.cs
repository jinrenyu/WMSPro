using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 人脸库
/// </summary>
[SugarTable("T_SFC_FACERECOGNITIONIFNO")]
public class TSfcFacerecognitionifno : BaseEntity
{
    /// <summary>
    /// 业务员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 工号
    /// </summary>
    [SugarColumn(ColumnName = "FEMPNO")]
    public string Fempno { get; set; } = string.Empty;

    /// <summary>
    /// 人脸图片路径
    /// </summary>
    [SugarColumn(ColumnName = "FFILEPATH")]
    public string Ffilepath { get; set; } = string.Empty;

    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCEID")]
    public string Fresourceid { get; set; } = string.Empty;

    /// <summary>
    /// 人脸服务设备IP
    /// </summary>
    [SugarColumn(ColumnName = "FDEVIP")]
    public string Fdevip { get; set; } = string.Empty;
}
