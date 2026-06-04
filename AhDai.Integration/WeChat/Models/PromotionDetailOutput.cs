using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// 优惠功能，享受优惠时返回该字段
/// </summary>
public class PromotionDetailOutput
{
    /// <summary>
    /// 券ID
    /// </summary>
    [JsonPropertyName("coupon_id")]
    public string CouponId { get; set; } = default!;
    /// <summary>
    /// 优惠名称
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    /// <summary>
    /// 优惠范围，枚举值：
    /// GLOBAL：全场代金券
    /// SINGLE：单品优惠
    /// </summary>
    [JsonPropertyName("scope")]
    public string? Scope { get; set; }
    /// <summary>
    /// 优惠类型，枚举值：
    /// CASH：充值型代金券
    /// NOCASH：免充值型代金券
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    /// <summary>
    /// 优惠券面额
    /// </summary>
    [JsonPropertyName("amount")]
    public int Amount { get; set; }
    /// <summary>
    /// 活动ID
    /// </summary>
    [JsonPropertyName("stock_id")]
    public string? StockId { get; set; }
    /// <summary>
    /// 微信出资，单位为分
    /// </summary>
    [JsonPropertyName("wechatpay_contribute")]
    public int WeChatPayContribute { get; set; }
    /// <summary>
    /// 商户出资，单位为分
    /// </summary>
    [JsonPropertyName("merchant_contribute")]
    public int MerchantContribute { get; set; }
    /// <summary>
    /// 其他出资，单位为分
    /// </summary>
    [JsonPropertyName("other_contribute")]
    public int OtherContribute { get; set; }
    /// <summary>
    /// 币种
    /// </summary>
    [JsonPropertyName("currency")]
    public string Currency { get; set; } = default!;
    /// <summary>
    /// 单品列表信息
    /// </summary>
    [JsonPropertyName("goods_detail")]
    public GoodsDetailOutput[]? GoodsDetail { get; set; }
}
