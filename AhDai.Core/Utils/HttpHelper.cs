using AhDai.Base.Extensions;
using AhDai.Core.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace AhDai.Core.Utils
{
	/// <summary>
	/// HttpHelper
	/// </summary>
	public static class HttpHelper
	{
		/// <summary>
		/// 发送内容类型为Url的Get请求
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static Models.HttpResponse Get(string url, IDictionary<string, object> parameters = null)
		{
			var task = GetAsync(url, parameters);
			task.Wait();
			return task.Result;
		}

		/// <summary>
		/// 发送内容类型为Url的Get请求
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static async Task<Models.HttpResponse> GetAsync(string url, IDictionary<string, object> parameters = null)
		{
			return await SendAsync(HttpMethod.Get, url, parameters, HttpContentType.Url);
		}

		/// <summary>
		/// 发送内容类型为Url的Post请求
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static Models.HttpResponse Post(string url, IDictionary<string, object> parameters = null)
		{
			var task = PostAsync(url, parameters);
			task.Wait();
			return task.Result;
		}

		/// <summary>
		/// 发送内容类型为Url的Post请求
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static async Task<Models.HttpResponse> PostAsync(string url, IDictionary<string, object> parameters = null)
		{
			return await SendAsync(HttpMethod.Post, url, parameters, HttpContentType.Url);
		}

		/// <summary>
		/// 发送内容类型为Json的Post请求
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static Models.HttpResponse PostJson(string url, IDictionary<string, object> parameters = null)
		{
			var task = PostJsonAsync(url, parameters);
			task.Wait();
			return task.Result;
		}

		/// <summary>
		/// 发送内容类型为Json的Post请求
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static async Task<Models.HttpResponse> PostJsonAsync(string url, IDictionary<string, object> parameters = null)
		{
			return await SendAsync(HttpMethod.Post, url, parameters, HttpContentType.Json);
		}

		/// <summary>
		/// 发送内容类型为Url的Put请求
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static Models.HttpResponse Put(string url, IDictionary<string, object> parameters = null)
		{
			var task = PutAsync(url, parameters);
			task.Wait();
			return task.Result;
		}

		/// <summary>
		/// 发送内容类型为Url的Put请求
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static async Task<Models.HttpResponse> PutAsync(string url, IDictionary<string, object> parameters = null)
		{
			return await SendAsync(HttpMethod.Put, url, parameters, HttpContentType.Url);
		}

		/// <summary>
		/// 发送内容类型为Json的Put请求
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static Models.HttpResponse PutJson(string url, IDictionary<string, object> parameters = null)
		{
			var task = PutJsonAsync(url, parameters);
			task.Wait();
			return task.Result;
		}

		/// <summary>
		/// 发送内容类型为Json的Put请求
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static async Task<Models.HttpResponse> PutJsonAsync(string url, IDictionary<string, object> parameters = null)
		{
			return await SendAsync(HttpMethod.Put, url, parameters, HttpContentType.Json);
		}

		/// <summary>
		/// 发送内容类型为Url的Delete请求
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static Models.HttpResponse Delete(string url, IDictionary<string, object> parameters = null)
		{
			var task = DeleteAsync(url, parameters);
			task.Wait();
			return task.Result;
		}

		/// <summary>
		/// 发送内容类型为Url的Delete请求
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static async Task<Models.HttpResponse> DeleteAsync(string url, IDictionary<string, object> parameters = null)
		{
			return await SendAsync(HttpMethod.Delete, url, parameters, HttpContentType.Url);
		}

		/// <summary>
		/// 发送请求
		/// </summary>
		/// <param name="method"></param>
		/// <param name="url"></param>
		/// <param name="parameters"></param>
		/// <param name="contentType"></param>
		/// <returns></returns>
		public static Models.HttpResponse Send(HttpMethod method, string url, IDictionary<string, object> parameters = null, string contentType = HttpContentType.Json)
		{
			var task = SendAsync(method, url, parameters, contentType);
			task.Wait();
			return task.Result;
		}

		/// <summary>
		/// 发送请求
		/// </summary>
		/// <param name="method"></param>
		/// <param name="url"></param>
		/// <param name="parameters"></param>
		/// <param name="contentType"></param>
		/// <returns></returns>
		public static async Task<Models.HttpResponse> SendAsync(HttpMethod method, string url, IDictionary<string, object> parameters = null, string contentType = HttpContentType.Json)
		{
			var data = new Models.HttpRequest(method, url, contentType)
			{
				Body = parameters
			};
			return await SendAsync(data);
		}

		/// <summary>
		/// 创建请求
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static HttpRequestMessage CreateRequest(Models.HttpRequest data)
		{
			if (data.ContentType == HttpContentType.Json)
			{
				if (data.Body != null)
				{
					data.Content = JsonHelper.SerializeObject(data.Body);
				}
			}
			else if (data.Body != null)
			{
				data.Content = data.Body.ToQueryString();
			}
			else
			{
			}

			if (data.Method == HttpMethod.Get)
			{
				if (!string.IsNullOrEmpty(data.Content))
				{
					data.Url += (data.Url.Contains('?') ? '&' : '?') + data.Content;
					data.Content = null;
				}
			}

			var requestMessage = new HttpRequestMessage(data.Method, data.Url);
			if (data.Headers != null)
			{
				foreach (var kv in data.Headers)
				{
					requestMessage.Headers.Add(kv.Key, kv.Value);
				}
			}
			if (!requestMessage.Headers.Contains("Accept"))
			{
				requestMessage.Headers.Add("Accept", "*/*");
			}
			if (!requestMessage.Headers.Contains("Accept-Language"))
			{
				requestMessage.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9,zh-TW;q=0.8,en-US;q=0.7,en;q=0.6");
			}
			if (!requestMessage.Headers.Contains("User-Agent"))
			{
				requestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36");
			}
			if (!requestMessage.Headers.Contains("Keep-Alive"))
			{
				requestMessage.Headers.Add("Keep-Alive", "true");
			}

			//async
			//写入请求参数
			if (!string.IsNullOrEmpty(data.Content))
			{
				var bytes = Encoding.UTF8.GetBytes(data.Content);
				requestMessage.Content = new ByteArrayContent(bytes);
				requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(data.ContentType) { CharSet = "utf-8" };
				//requestMessage.Content.Headers.ContentLength = bytes.Length;
				//requestMessage.Content.Headers.ContentType.CharSet = "utf-8";
			}

			return requestMessage;
		}

		/// <summary>
		/// 发送请求
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static async Task<Models.HttpResponse> SendAsync(Models.HttpRequest data)
		{
			// 创建请求
			var httpWebRequest = CreateRequest(data);

			try
			{
				using var client = new HttpClient();
				using var httpResponse = await client.SendAsync(httpWebRequest);
				var response = new Models.HttpResponse()
				{
					ResponseUri = null,
					StatusCode = httpResponse.StatusCode,
					ReasonPhrase = httpResponse.ReasonPhrase
				};
				if (httpResponse.Content.Headers.ContentType != null)
				{
					response.ContentType = httpResponse.Content.Headers.ContentType;
					response.ContentLength = httpResponse.Content.Headers.ContentLength ?? 0;
					response.ContentEncoding = httpResponse.Content.Headers.ContentEncoding;
					response.ContentLanguage = httpResponse.Content.Headers.ContentLanguage;
				}

				var charSet = httpResponse.Content.Headers.ContentType?.CharSet;
				var encoding = Base.Utils.TextHelper.GetEncoding(charSet);
				using var stream = await httpResponse.Content.ReadAsStreamAsync();
				if (charSet == "gzip")
				{
					using var gzStream = new GZipStream(stream, CompressionMode.Decompress);
					using var reader = new StreamReader(gzStream, encoding);
					response.Content = reader.ReadToEnd();
				}
				else
				{
					using var reader = new StreamReader(stream, encoding);
					response.Content = reader.ReadToEnd();
				}
				return response;
			}
			catch (WebException ex)
			{
				var error = ex.Message;
				if (ex.Response != null)
				{
					using (var reader = new StreamReader(ex.Response.GetResponseStream(), Encoding.UTF8))
					{
						error = reader.ReadToEnd();
					}
					if (string.IsNullOrEmpty(error))
					{
						error = ex.Message;
					}
				}
				throw new Exception(error);
			}
		}

		/// <summary>
		/// 类型转换
		/// </summary>
		/// <param name="method"></param>
		/// <returns></returns>
		public static HttpMethod ConvertHttpMethod(string method)
		{
			return method.ToUpper() switch
			{
				"GET" => HttpMethod.Get,
				"POST" => HttpMethod.Post,
				"PUT" => HttpMethod.Put,
				"DELETE" => HttpMethod.Delete,
				_ => throw new NotImplementedException(),
			};
		}

		/// <summary>
		/// ParseQueryString
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public static IDictionary<string, string> ParseQueryString(string query)
		{
			if (string.IsNullOrEmpty(query))
			{
				return null;
			}
			var array = query.Split('&');
			var dic = new Dictionary<string, string>();
			foreach (var kv in array)
			{
				var _array = kv.Split('=');
				if (_array.Length > 2)
				{
					_array = new string[] { _array[0], string.Join("=", _array, 1, _array.Length - 1) };
				}
				else if (_array.Length != 2)
				{
					throw new ArgumentException("Parameter format error.");
				}
				dic.Add(_array[0], _array[1]);
			}
			return dic;
		}

		/// <summary>
		/// UrlEncode
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static T UrlEncode<T>(T parameters) where T : IDictionary<string, string>
		{
			return UrlEncode(parameters, Encoding.UTF8);
		}

		/// <summary>
		/// UrlEncode
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="parameters"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public static T UrlEncode<T>(T parameters, Encoding encoding) where T : IDictionary<string, string>
		{
			for (var i = 0; i < parameters.Keys.Count; i++)
			{
				var kv = parameters.ElementAt(i);
				parameters[kv.Key] = HttpUtility.UrlEncode(kv.Value, encoding);
			}
			return parameters;
		}

		/// <summary>
		/// UrlEncode
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static T UrlEncodeUpper<T>(T parameters) where T : IDictionary<string, string>
		{
			return UrlEncodeUpper(parameters, Encoding.UTF8);
		}

		/// <summary>
		/// UrlEncode
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="parameters"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public static T UrlEncodeUpper<T>(T parameters, Encoding encoding) where T : IDictionary<string, string>
		{
			for (var i = 0; i < parameters.Keys.Count; i++)
			{
				var kv = parameters.ElementAt(i);
				parameters[kv.Key] = UrlEncodeUpper(kv.Value, encoding);
			}
			return parameters;
		}

		/// <summary>
		/// 转换Url编码为大写
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string UrlEncodeUpper(string str)
		{
			return UrlEncodeUpper(str, Encoding.UTF8);
		}

		/// <summary>
		/// 转换Url编码为大写
		/// </summary>
		/// <param name="str"></param>
		/// <param name="encoding">编码</param>
		/// <returns></returns>
		public static string UrlEncodeUpper(string str, Encoding encoding)
		{
			if (string.IsNullOrEmpty(str))
			{
				return str;
			}
			var builder = new StringBuilder();
			foreach (var c in str)
			{
				var code = HttpUtility.UrlEncode(c.ToString(), encoding);
				if (code.Length > 1)
				{
					builder.Append(code.ToUpper());
				}
				else
				{
					builder.Append(c);
				}
			}
			return builder.ToString();
		}

		/// <summary>
		/// UrlDecode
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static T UrlDecode<T>(T parameters) where T : IDictionary<string, string>
		{
			return UrlDecode(parameters, Encoding.UTF8);
		}

		/// <summary>
		/// UrlDecode
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="parameters"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public static T UrlDecode<T>(T parameters, Encoding encoding) where T : IDictionary<string, string>
		{
			for (var i = 0; i < parameters.Keys.Count; i++)
			{
				var kv = parameters.ElementAt(i);
				parameters[kv.Key] = HttpUtility.UrlDecode(kv.Value, encoding);
			}
			return parameters;
		}

		/// <summary>
		/// IsIp
		/// </summary>
		/// <param name="ip"></param>
		/// <returns></returns>
		public static bool IsIp(string ip)
		{
			if (string.IsNullOrWhiteSpace(ip) || ip.Length < 7 || ip.Length > 15)
			{
				return false;
			}
			return new Regex(@"^(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})", RegexOptions.IgnoreCase).IsMatch(ip);
		}
	}
}
