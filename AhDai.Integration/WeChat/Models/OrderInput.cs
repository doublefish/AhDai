using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// 下单
/// </summary>
public class OrderInput
{
    /// <summary>
    /// 公众号ID
    /// </summary>
    [Required]
    [JsonPropertyName("appid")]
    public string AppId { get; set; } = default!;
    /// <summary>
    /// 直连商户号
    /// </summary>
    [Required]
    [JsonPropertyName("mchid")]
    public string MchId { get; set; } = default!;
    /// <summary>
    /// 商品描述
    /// </summary>
    [Required]
    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;
    /// <summary>
    /// 商户订单号
    /// </summary>
    [Required]
    [JsonPropertyName("out_trade_no")]
    public string OutTradeNo { get; set; } = default!;
    /// <summary>
    /// 交易结束时间：2018-06-08T10:34:56+08:00
    /// </summary>
    [JsonPropertyName("time_expire")]
    public DateTime? TimeExpire { get; set; }
    /// <summary>
    /// 附加数据
    /// </summary>
    [JsonPropertyName("attach")]
    public string? Attach { get; set; }
    /// <summary>
    /// 通知地址
    /// </summary>
    [Required]
    [JsonPropertyName("notify_url")]
    public string NotifyUrl { get; set; } = default!;
    /// <summary>
    /// 订单优惠标记
    /// </summary>
    [JsonPropertyName("goods_tag")]
    public string? GoodsTag { get; set; }
    /// <summary>
    /// 电子发票入口开放标识
    /// </summary>
    [JsonPropertyName("support_fapiao")]
    public bool? SupportFaPiao { get; set; }
    /// <summary>
    /// 金额
    /// </summary>
    [JsonIgnore]
    public decimal Amount { get; set; }
    /// <summary>
    /// 金额信息
    /// </summary>
    [Required]
    [JsonPropertyName("amount")]
    public AmountInput AmountInfo { get; set; } = default!;
    /// <summary>
    /// 优惠功能
    /// </summary>
    [JsonPropertyName("detail")]
    public OrderDetailInput? Detail { get; set; }
    /// <summary>
    /// 场景信息：H5支付必填
    /// </summary>
    [JsonPropertyName("scene_info")]
    public SceneInfoInput? SceneInfo { get; set; }
    /// <summary>
    /// 结算信息
    /// </summary>
    [JsonPropertyName("settle_info")]
    public SettleInfoInput? SettleInfo { get; set; }
}
