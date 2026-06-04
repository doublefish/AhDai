namespace AhDai.Integration.Tianyancha.Models;

/// <summary>
/// TianyanchaBaseInfoOutput
/// </summary>
public class BaseInfoOutput
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// 简称
    /// </summary>
    public string? Alias { get; set; }
    /// <summary>
    /// 历史名称
    /// </summary>
    public string[]? HistoryNameList { get; set; }
    /// <summary>
    /// 注册状态
    /// </summary>
    public string? RegStatus { get; set; }
    /// <summary>
    /// 注销日期
    /// </summary>
    public string? CancelDate { get; set; }
    /// <summary>
    /// 注销原因
    /// </summary>
    public string? CancelReason { get; set; }
    /// <summary>
    /// 社会信用代码
    /// </summary>
    public string? CreditCode { get; set; }
    /// <summary>
    /// 工商注册号
    /// </summary>
    public string? RegNumber { get; set; }
    /// <summary>
    /// 纳税人识别号
    /// </summary>
    public string? TaxNumber { get; set; }
    /// <summary>
    /// 组织机构代码
    /// </summary>
    public string? OrgNumber { get; set; }
    /// <summary>
    /// 法定代表人
    /// </summary>
    public string? LegalPersonName { get; set; }
    /// <summary>
    /// 成立日期
    /// </summary>
    public long? EstiblishTime { get; set; }
    /// <summary>
    /// 注册资本
    /// </summary>
    public string? RegCapital { get; set; }
    /// <summary>
    /// 注册资本货币类型
    /// </summary>
    public string? RegCapitalCurrency { get; set; }
    /// <summary>
    /// 实缴资本
    /// </summary>
    public string? ActualCapital { get; set; }
    /// <summary>
    /// 实缴资本货币类型
    /// </summary>
    public string? ActualCapitalCurrency { get; set; }
    /// <summary>
    /// 营业期限
    /// </summary>
    public long? FromTime { get; set; }
    /// <summary>
    /// 营业期限
    /// </summary>
    public long? ToTime { get; set; }
    /// <summary>
    /// 企业类型
    /// </summary>
    public string? CompanyOrgType { get; set; }
    /// <summary>
    /// 注册地址
    /// </summary>
    public string? RegLocation { get; set; }
    /// <summary>
    /// 注册地所属省份
    /// </summary>
    public string? Province { get; set; }
    /// <summary>
    /// 注册地所属城市
    /// </summary>
    public string? City { get; set; }
    /// <summary>
    /// 注册地所属区县
    /// </summary>
    public string? District { get; set; }
    /// <summary>
    /// 注册机关
    /// </summary>
    public string? RegInstitute { get; set; }
    /// <summary>
    /// 行业
    /// </summary>
    public string? Industry { get; set; }
    /// <summary>
    /// 经营范围
    /// </summary>
    public string? BusinessScope { get; set; }
    /// <summary>
    /// 上市代码
    /// </summary>
    public string? BondNum { get; set; }
    /// <summary>
    /// 上市名称
    /// </summary>
    public string? BondName { get; set; }
    /// <summary>
    /// 上市类型
    /// </summary>
    public string? BondType { get; set; }
    /// <summary>
    /// 是否小微企业
    /// </summary>
    public int IsMicroEnt { get; set; }
    /// <summary>
    /// 人员规模
    /// </summary>
    public string? StaffNumRange { get; set; }
    /// <summary>
    /// 参保人数
    /// </summary>
    public int? SocialStaffNum { get; set; }
    /// <summary>
    /// 天眼评分
    /// </summary>
    public int PercentileScore { get; set; }
}
