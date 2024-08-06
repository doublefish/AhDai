using System.Xml;

namespace AhDai.Base.Extensions
{
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
            foreach (XmlAttribute attribute in node.Attributes)
            {
                if (attribute.Name != name)
                {
                    continue;
                }
                return attribute.Value;
            }
            return string.Empty;
        }

        /// <summary>
        /// GetInnerText
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static string GetInnerText(this XmlDocument doc, string xpath)
        {
            var child = doc.SelectSingleNode(xpath);
            if (child == null)
            {
                return string.Empty;
            }
            return child.InnerText;
        }

        /// <summary>
        /// GetInnerText
        /// </summary>
        /// <param name="node"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static string GetInnerText(this XmlNode node, string xpath)
        {
            var child = node.SelectSingleNode(xpath);
            if (child == null)
            {
                return string.Empty;
            }
            return child.InnerText;
        }
    }
}
