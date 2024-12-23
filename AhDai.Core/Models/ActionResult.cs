using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AhDai.Core.Models;

/// <summary>
/// ActionResult
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="actionId">动作Id</param>
/// <param name="code">结果代码</param>
/// <param name="message">消息</param>
/// <param name="content">内容</param>
/// <param name="contentType">内容类型</param>
public class ActionResult<T>(string actionId, int code, string message, T content, string contentType = HttpContentType.Json) : Services.IActionResult<T>
{
    /// <summary>
    /// 动作Id
    /// </summary>
    public string ActionId { get; set; } = actionId;
    /// <summary>
    /// 状态代码
    /// </summary>
    public int Code { get; set; } = code;
    /// <summary>
    /// 消息
    /// </summary>
    public string Message { get; set; } = message;
    /// <summary>
    /// 结果
    /// </summary>
    public T Content { get; set; } = content;
    /// <summary>
    /// 内容类型
    /// </summary>
    public string ContentType { get; set; } = contentType;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="actionId">动作Id</param>
    /// <param name="content">内容</param>
    /// <param name="contentType">内容类型</param>
    public ActionResult(string actionId, T content, string contentType = HttpContentType.Json) : this(actionId, 0, "success", content, contentType)
    {
    }

    /// <summary>
    /// ExecuteResultAsync
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task ExecuteResultAsync(ActionContext context)
    {
        var text = ContentType switch
        {
            HttpContentType.Xml => Base.Utils.XmlUtil.SerializeObject(this),
            HttpContentType.TextXml => Base.Utils.XmlUtil.SerializeObject(this),
            _ => Utils.JsonUtil.Serialize(this),
        };
        //var bytes = Encoding.Default.GetBytes(text);
        context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
        context.HttpContext.Response.ContentType = ContentType;
        //context.HttpContext.Response.ContentLength = bytes.Length;
        context.HttpContext.Response.WriteAsync(text);
        return Task.CompletedTask;
    }
}