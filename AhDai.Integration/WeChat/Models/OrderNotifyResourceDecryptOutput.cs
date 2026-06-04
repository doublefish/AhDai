using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// 通知资源数据
/// </summary>
public class OrderNotifyResourceDecryptOutput
{
    /// <summary>
    /// 直连商户申请的公众号或移动应用AppID
    /// </summary>
    [JsonPropertyName("appid")]
    public string AppId { get; set; } = default!;
    /// <summary>
    /// 商户的商户号
    /// </summary>
    [JsonPropertyName("mchid")]
    public string MchId { get; set; } = default!;
    /// <summary>
    /// 商户系统内部订单号
    /// </summary>
    [JsonPropertyName("out_trade_no")]
    public string OutTradeNo { get; set; } = default!;
    /// <summary>
    /// 微信支付系统生成的订单号
    /// </summary>
    [JsonPropertyName("transaction_id")]
    public string TransactionId { get; set; } = default!;
    /// <summary>
    /// 交易类型，枚举值：
    /// JSAPI：公众号支付
    /// NATIVE：扫码支付
    /// App：App支付
    /// MICROPAY：付款码支付
    /// MWEB：H5支付
    /// FACEPAY：刷脸支付
    /// </summary>
    [JsonPropertyName("trade_type")]
    public string TradeType { get; set; } = default!;
    /// <summary>
    /// 交易状态，枚举值：
    /// SUCCESS：支付成功
    /// REFUND：转入退款
    /// NOTPAY：未支付
    /// CLOSED：已关闭
    /// REVOKED：已撤销（付款码支付）
    /// USERPAYING：用户支付中（付款码支付）
    /// PAYERROR：支付失败(其他原因，如银行返回失败)
    /// </summary>
    [JsonPropertyName("trade_state")]
    public string TradeState { get; set; } = default!;
    /// <summary>
    /// 交易状态描述
    /// </summary>
    [JsonPropertyName("trade_state_desc")]
    public string TradeStateDesc { get; set; } = default!;
    /// <summary>
    /// 银行类型，采用字符串类型的银行标识
    /// </summary>
    [JsonPropertyName("bank_type")]
    public string BankType { get; set; } = default!;
    /// <summary>
    /// 附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用，实际情况下只有支付完成状态才会返回该字段
    /// </summary>
    [JsonPropertyName("attach")]
    public string? Attach { get; set; }
    /// <summary>
    /// 支付完成时间
    /// </summary>
    [JsonPropertyName("success_time")]
    public DateTime SuccessTime { get; set; }
    /// <summary>
    /// 支付者信息
    /// </summary>
    [JsonPropertyName("payer")]
    public Payer Payer { get; set; } = default!;
    /// <summary>
    /// 订单金额信息
    /// </summary>
    [JsonPropertyName("amount")]
    public AmountOutput Amount { get; set; } = default!;
    /// <summary>
    /// 支付场景信息描述
    /// </summary>
    [JsonPropertyName("scene_info")]
    public SceneInfoOutput? SceneInfo { get; set; }
    /// <summary>
    /// 优惠功能，享受优惠时返回该字段
    /// </summary>
    [JsonPropertyName("promotion_detail")]
    public PromotionDetailOutput[]? PromotionDetail { get; set; }

    /// <summary>
    /// 是否交易成功
    /// </summary>
    [JsonIgnore]
    public bool IsSuccessful => TradeState == "SUCCESS";
}

