using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models.Pay;

/// <summary>
/// 商品
/// </summary>
public class GoodsDetailOutput
{
    /// <summary>
    /// 商品编码
    /// </summary>
    [JsonPropertyName("goods_id")]
    public string GoodsId { get; set; } = default!;
    /// <summary>
    /// 用户购买的数量
    /// </summary>
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
    /// <summary>
    /// 商品单价，单位为分
    /// </summary>
    [JsonPropertyName("unit_price")]
    public int UnitPrice { get; set; }
    /// <summary>
    /// 商品优惠金额
    /// </summary>
    [JsonPropertyName("discount_amount")]
    public int DiscountAmount { get; set; }
    /// <summary>
    /// 商品备注信息
    /// </summary>
    [JsonPropertyName("goods_remark")]
    public string? GoodsRemark { get; set; }
}
