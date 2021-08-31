using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Adai.Standard
{
	/// <summary>
	/// JsonHelper
	/// </summary>
	public static class JsonHelper
	{
		/// <summary>
		/// JsonSerializerOptions
		/// </summary>
		public static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions()
		{
			Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
			Converters = { new DatetimeJsonConverter(Const.DateTimeFormat) }
		};

		/// <summary>
		/// 序列化
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public static string SerializeObject<T>(T obj, JsonSerializerOptions options = null)
		{
			return JsonSerializer.Serialize(obj, options ?? JsonSerializerOptions);
		}

		/// <summary>
		/// 反序列化
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="json"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public static T DeserializeObject<T>(string json, JsonSerializerOptions options = null)
		{
			return JsonSerializer.Deserialize<T>(json, options ?? JsonSerializerOptions);
		}
	}
}
