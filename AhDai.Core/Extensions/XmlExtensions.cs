using System.Xml;

namespace AhDai.Core.Extensions;

/// <summary>
/// XmlExt
/// </summary>
public static class XmlExtensions
{
    /// <summary>
    /// GetAttribute
    /// </summary>
    /// <param name="node"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string GetAttribute(this XmlNode node, string name)
    {
        return node?.Attributes?[name]?.Value ?? string.Empty;
    }

    /// <summary>
    /// GetInnerText
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="xpath"></param>
    /// <returns></returns>
    public static string GetInnerText(this XmlDocument doc, string xpath)
    {
        return (doc as XmlNode).GetInnerText(xpath);
    }

    /// <summary>
    /// GetInnerText
    /// </summary>
    /// <param name="node"></param>
    /// <param name="xpath"></param>
    /// <returns></returns>
    public static string GetInnerText(this XmlNode node, string xpath)
    {
        if (node == null || string.IsNullOrEmpty(xpath)) return string.Empty;

        return node.SelectSingleNode(xpath)?.InnerText ?? string.Empty;
    }
}
