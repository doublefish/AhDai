using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Ocr;

/// <summary>
/// VatInvoiceWordsResult
/// </summary>
public class VatInvoiceWordsResult
{
    /// <summary>
    /// 发票消费类型。**不同消费类型输出：**餐饮、电器设备、通讯、服务、日用品食品、医疗、交通、其他
    /// </summary>
    [JsonPropertyName("ServiceType")]
    public string ServiceType { get; set; } = default!;
    /// <summary>
    /// 发票种类。不同类型发票输出：普通发票、专用发票、电子普通发票、电子专用发票、通行费电子普票、区块链发票、通用机打电子发票、电子发票(专用发票)、电子发票(普通发票)
    /// </summary>
    [JsonPropertyName("InvoiceType")]
    public string InvoiceType { get; set; } = default!;
    /// <summary>
    /// 发票名称
    /// </summary>
    [JsonPropertyName("InvoiceTypeOrg")]
    public string InvoiceTypeOrg { get; set; } = default!;
    /// <summary>
    /// 发票代码
    /// </summary>
    [JsonPropertyName("InvoiceCode")]
    public string InvoiceCode { get; set; } = default!;
    /// <summary>
    /// 发票号码
    /// </summary>
    [JsonPropertyName("InvoiceNum")]
    public string InvoiceNum { get; set; } = default!;
    /// <summary>
    /// 发票代码的辅助校验码，一般业务情景可忽略
    /// </summary>
    [JsonPropertyName("InvoiceCodeConfirm")]
    public string InvoiceCodeConfirm { get; set; } = default!;
    /// <summary>
    /// 发票号码的辅助校验码，一般业务情景可忽略
    /// </summary>
    [JsonPropertyName("InvoiceNumConfirm")]
    public string InvoiceNumConfirm { get; set; } = default!;
    /// <summary>
    /// 数电票号，仅针对纸质的全电发票，在密码区有数电票号码的字段输出
    /// </summary>
    [JsonPropertyName("InvoiceNumDigit")]
    public string InvoiceNumDigit { get; set; } = default!;
    /// <summary>
    /// 增值税发票左上角标志。 包含：通行费、销项负数、代开、收购、成品油、其他
    /// </summary>
    [JsonPropertyName("InvoiceTag")]
    public string InvoiceTag { get; set; } = default!;
    /// <summary>
    /// 机打号码。仅增值税卷票含有此参数
    /// </summary>
    [JsonPropertyName("MachineNum")]
    public string MachineNum { get; set; } = default!;
    /// <summary>
    /// 机器编号。仅增值税卷票含有此参数
    /// </summary>
    [JsonPropertyName("MachineCode")]
    public string MachineCode { get; set; } = default!;
    /// <summary>
    /// 校验码
    /// </summary>
    [JsonPropertyName("CheckCode")]
    public string CheckCode { get; set; } = default!;
    /// <summary>
    /// 开票日期
    /// </summary>
    [JsonPropertyName("InvoiceDate")]
    public string InvoiceDate { get; set; } = default!;
    /// <summary>
    /// 购方名称
    /// </summary>
    [JsonPropertyName("PurchaserName")]
    public string PurchaserName { get; set; } = default!;
    /// <summary>
    /// 购方纳税人识别号
    /// </summary>

    [JsonPropertyName("PurchaserRegisterNum")]
    public string PurchaserRegisterNum { get; set; } = default!;
    /// <summary>
    /// 购方地址及电话
    /// </summary>
    [JsonPropertyName("PurchaserAddress")]
    public string PurchaserAddress { get; set; } = default!;
    /// <summary>
    /// 购方开户行及账号
    /// </summary>
    [JsonPropertyName("PurchaserBank")]
    public string PurchaserBank { get; set; } = default!;
    /// <summary>
    /// 密码区
    /// </summary>
    [JsonPropertyName("Password")]
    public string Password { get; set; } = default!;
    /// <summary>
    /// 省
    /// </summary>
    [JsonPropertyName("Province")]
    public string Province { get; set; } = default!;
    /// <summary>
    /// 市
    /// </summary>
    [JsonPropertyName("City")]
    public string City { get; set; } = default!;
    /// <summary>
    /// 联次信息。专票第一联到第三联分别输出：第一联：记账联、第二联：抵扣联、第三联：发票联；普通发票第一联到第二联分别输出：第一联：记账联、第二联：发票联
    /// </summary>
    [JsonPropertyName("SheetNum")]
    public string SheetNum { get; set; } = default!;
    /// <summary>
    /// 是否代开
    /// </summary>
    [JsonPropertyName("Agent")]
    public string Agent { get; set; } = default!;
    /// <summary>
    /// 货物名称
    /// </summary>
    [JsonPropertyName("CommodityName")]
    public WordItemOutput[] CommodityName { get; set; } = default!;
    /// <summary>
    /// 规格型号
    /// </summary>

    [JsonPropertyName("CommodityType")]
    public WordItemOutput[] CommodityType { get; set; } = default!;
    /// <summary>
    /// 单位
    /// </summary>
    [JsonPropertyName("CommodityUnit")]
    public WordItemOutput[] CommodityUnit { get; set; } = default!;
    /// <summary>
    /// 数量
    /// </summary>
    [JsonPropertyName("CommodityNum")]
    public WordItemOutput[] CommodityNum { get; set; } = default!;
    /// <summary>
    /// 单价
    /// </summary>
    [JsonPropertyName("CommodityPrice")]
    public WordItemOutput[] CommodityPrice { get; set; } = default!;
    /// <summary>
    /// 金额
    /// </summary>
    [JsonPropertyName("CommodityAmount")]
    public WordItemOutput[] CommodityAmount { get; set; } = default!;
    /// <summary>
    /// 税率
    /// </summary>
    [JsonPropertyName("CommodityTaxRate")]
    public WordItemOutput[] CommodityTaxRate { get; set; } = default!;
    /// <summary>
    /// 税额
    /// </summary>
    [JsonPropertyName("CommodityTax")]
    public WordItemOutput[] CommodityTax { get; set; } = default!;
    /// <summary>
    /// 车牌号。仅通行费增值税电子普通发票含有此参数
    /// </summary>
    [JsonPropertyName("CommodityPlateNum")]
    public string[] CommodityPlateNum { get; set; } = default!;
    /// <summary>
    /// 类型。仅通行费增值税电子普通发票含有此参数
    /// </summary>
    [JsonPropertyName("CommodityVehicleType")]
    public string[] CommodityVehicleType { get; set; } = default!;
    /// <summary>
    /// 通行日期起。仅通行费增值税电子普通发票含有此参数
    /// </summary>
    [JsonPropertyName("CommodityStartDate")]
    public string[] CommodityStartDate { get; set; } = default!;
    /// <summary>
    /// 通行日期止。仅通行费增值税电子普通发票含有此参数
    /// </summary>
    [JsonPropertyName("CommodityEndDate")]
    public string[] CommodityEndDate { get; set; } = default!;
    /// <summary>
    /// 电子支付标识。仅区块链发票含有此参数
    /// </summary>
    [JsonPropertyName("OnlinePay")]
    public string OnlinePay { get; set; } = default!;
    /// <summary>
    /// 销售方名称
    /// </summary>
    [JsonPropertyName("SellerName")]
    public string SellerName { get; set; } = default!;
    /// <summary>
    /// 销售方纳税人识别号
    /// </summary>
    [JsonPropertyName("SellerRegisterNum")]
    public string SellerRegisterNum { get; set; } = default!;
    /// <summary>
    /// 销售方地址及电话
    /// </summary>
    [JsonPropertyName("SellerAddress")]
    public string SellerAddress { get; set; } = default!;
    /// <summary>
    /// 销售方开户行及账号
    /// </summary>
    [JsonPropertyName("SellerBank")]
    public string SellerBank { get; set; } = default!;
    /// <summary>
    /// 合计金额
    /// </summary>
    [JsonPropertyName("TotalAmount")]
    public decimal TotalAmount { get; set; }
    /// <summary>
    /// 合计税额
    /// </summary>
    [JsonPropertyName("TotalTax")]
    public decimal TotalTax { get; set; }
    /// <summary>
    /// 价税合计（大写）
    /// </summary>
    [JsonPropertyName("AmountInWords")]
    public string AmountInWords { get; set; } = default!;
    /// <summary>
    /// 价税合计（小写）
    /// </summary>
    [JsonPropertyName("AmountInFiguers")]
    public decimal AmountInFiguers { get; set; }
    /// <summary>
    /// 收款人
    /// </summary>
    [JsonPropertyName("Payee")]
    public string Payee { get; set; } = default!;
    /// <summary>
    /// 复核
    /// </summary>
    [JsonPropertyName("Checker")]
    public string Checker { get; set; } = default!;
    /// <summary>
    /// 开票人
    /// </summary>
    [JsonPropertyName("NoteDrawer")]
    public string NoteDrawer { get; set; } = default!;
    /// <summary>
    /// 备注
    /// </summary>
    [JsonPropertyName("Remarks")]
    public string Remarks { get; set; } = default!;
    /// <summary>
    /// 判断是否存在印章。返回“0或1”，1代表存在印章，0代表不存在印章，当 seal_tag=true 时返回该字段
    /// </summary>
    [JsonPropertyName("company_seal")]
    public string? CompanySeal { get; set; }
    /// <summary>
    /// 印章识别结果内容。当 seal_tag=true 时返回该字段
    /// </summary>
    [JsonPropertyName("seal_info")]
    public string? SealInfo { get; set; }
}
