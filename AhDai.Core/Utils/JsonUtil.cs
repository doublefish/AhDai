using Newtonsoft.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace AhDai.Core.Utils;

/// <summary>
/// JsonUtil
/// </summary>
public static class JsonUtil
{
	/// <summary>
	/// Options
	/// </summary>
	public static JsonSerializerOptions Options { get; private set; }

	/// <summary>
	/// 构造函数
	/// </summary>
	static JsonUtil()
	{
		Options = new JsonSerializerOptions()
		{
			PropertyNameCaseInsensitive = true,
			Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
			Converters = { new DatetimeJsonConverter(Const.DateTimeFormat) }
		};

		Settings = new JsonSerializerSettings()
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore
		};
	}

	/// <summary>
	/// 序列化
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="obj"></param>
	/// <param name="options"></param>
	/// <returns></returns>
	public static string Serialize<T>(T obj, JsonSerializerOptions options = null)
	{
		return System.Text.Json.JsonSerializer.Serialize(obj, options ?? Options);
	}

	/// <summary>
	/// 反序列化
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="json"></param>
	/// <param name="options"></param>
	/// <returns></returns>
	public static T Deserialize<T>(string json, JsonSerializerOptions options = null)
	{
		return System.Text.Json.JsonSerializer.Deserialize<T>(json, options ?? Options);
	}

	#region Newtonsoft.Json
	/// <summary>
	/// Settings
	/// </summary>
	public static JsonSerializerSettings Settings { get; private set; }

	/// <summary>
	/// 序列化
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="obj"></param>
	/// <param name="settings"></param>
	/// <returns></returns>
	public static string SerializeObject<T>(T obj, JsonSerializerSettings settings = null)
	{
		return JsonConvert.SerializeObject(obj, settings ?? Settings);
	}

	/// <summary>
	/// SerializeObject
	/// </summary>
	/// <param name="value"></param>
	/// <param name="includeProperties"></param>
	/// <param name="excludeProperties"></param>
	/// <returns></returns>
	public static string SerializeObject(this object value, string[] includeProperties, string[] excludeProperties)
	{
		var settings = new JsonSerializerSettings()
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
			ContractResolver = new Models.JsonPropertyContractResolver(includeProperties, excludeProperties)
		};
		return JsonConvert.SerializeObject(value, Formatting.None, settings);
	}

	/// <summary>
	/// 反序列化
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="json"></param>
	/// <param name="settings"></param>
	/// <returns></returns>
	public static T DeserializeObject<T>(string json, JsonSerializerSettings settings = null)
	{
		return JsonConvert.DeserializeObject<T>(json, settings ?? Settings);
	}
	#endregion
}
