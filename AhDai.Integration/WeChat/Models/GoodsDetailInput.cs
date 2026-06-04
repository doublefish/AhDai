using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// 商品
/// </summary>
public class GoodsDetailInput
{
    /// <summary>
    /// 商户侧商品编码
    /// </summary>
    [Required]
    [JsonPropertyName("merchant_goods_id")]
    public string MerchantGoodsId { get; set; } = default!;
    /// <summary>
    /// 微信支付商品编码
    /// </summary>
    [JsonPropertyName("wechatpay_goods_id")]
    public string? WeChatPayGoodsId { get; set; } = default!;
    /// <summary>
    /// 商品名称
    /// </summary>
    [JsonPropertyName("goods_name")]
    public string? GoodsName { get; set; } = default!;
    /// <summary>
    /// 商品数量
    /// </summary>
    [Required]
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
    /// <summary>
    /// 商品单价，单位为分
    /// </summary>
    [Required]
    [JsonPropertyName("unit_price")]
    public int UnitPrice { get; set; }
}
