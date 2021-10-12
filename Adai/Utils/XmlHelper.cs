using System.IO;
using System.Xml.Serialization;

namespace Adai.Utils
{
	/// <summary>
	/// XmlHelper
	/// </summary>
	public static class XmlHelper
	{
		/// <summary>
		/// 序列化
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string SerializeObject<T>(T obj)
		{
			var stringWriter = new StringWriter();
			try
			{
				new XmlSerializer(obj.GetType()).Serialize(stringWriter, obj);
			}
			finally
			{
				stringWriter.Dispose();
			}
			return stringWriter.ToString();
		}
	}
}
