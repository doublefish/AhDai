using Adai.Standard.Models;
using Adai.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Adai.Standard.Utilities
{
	/// <summary>
	/// HttpHelper
	/// </summary>
	public static class HttpHelper
	{
		/// <summary>
		/// 发送参数类型为Url的GET请求
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static HttpResponse SendGet(string url, IDictionary<string, string> parameters = null)
		{
			var data = new HttpRequest(url, HttpMethod.Get, HttpContentType.Url)
			{
				Body = parameters
			};
			return SendRequest(data);
		}

		/// <summary>
		/// 发送参数类型为Url的POST请求
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static HttpResponse SendPost(string url, IDictionary<string, string> parameters = null)
		{
			var data = new HttpRequest(url, HttpMethod.Post, HttpContentType.Url)
			{
				Body = parameters
			};
			return SendRequest(data);
		}

		/// <summary>
		/// 发送参数类型为Json的POST请求
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static HttpResponse SendPostByJson<TValue>(string url, IDictionary<string, TValue> parameters)
		{
			var data = new HttpRequest(url, HttpMethod.Post, HttpContentType.Json)
			{
				Content = JsonHelper.SerializeObject(parameters)
			};
			return SendRequest(data);
		}

		/// <summary>
		/// 创建请求
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static HttpWebRequest CreateRequest(HttpRequest data)
		{
			if (data.ContentType == HttpContentType.Json)
			{
				data.ContentType += ";charset=utf-8";
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
					data.Url += (data.Url.IndexOf('?') == -1 ? '?' : '&') + data.Content;
					data.Content = null;
				}
			}

			var request = (HttpWebRequest)WebRequest.Create(data.Url);
			//request.Proxy = null;
			//request.Referer = "";
			request.Accept = "application/json,text/javascript,*/*;q=0.01";
			//request.Headers["Accept-Encoding"] = "gzip, deflate";
			request.Headers["Accept-Language"] = "zh-Hans-CN,zh-Hans;q=0.8,zh-Hant-TW;q=0.7,zh-Hant;q=0.5,en-GB;q=0.3,en;q=0.2";
			if (data.Headers != null)
			{
				foreach (var kv in data.Headers)
				{
					request.Headers[kv.Key] = kv.Value;
				}
			}
			//Edge
			request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36 Edge/17.17134";
			request.KeepAlive = true;
			request.ContentType = data.ContentType;
			request.Method = data.Method;
			request.ReadWriteTimeout = 3000;

			//写入请求参数
			if (!string.IsNullOrEmpty(data.Content))
			{
				var bytes = Encoding.UTF8.GetBytes(data.Content);
				request.ContentLength = bytes.Length;
				using var stream = request.GetRequestStream();
				stream.Write(bytes, 0, bytes.Length);
			}

			return request;
		}

		/// <summary>
		/// 发送POST请求
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static HttpResponse SendRequest(HttpRequest data)
		{
			//创建请求
			var httpWebRequest = CreateRequest(data);

			//获取请求结果
			WebResponse webResponse;
			try
			{
				webResponse = httpWebRequest.GetResponse();
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

			//读取响应内容
			HttpResponse response;
			using (var httpWebResponse = (HttpWebResponse)webResponse)
			{
				var content = string.Empty;
				if (httpWebResponse.StatusCode != HttpStatusCode.OK)
				{
					content = httpWebResponse.StatusDescription;
				}
				else
				{
					var encoding = TextHelper.GetEncoding(httpWebResponse.CharacterSet);
					using var stream = httpWebResponse.GetResponseStream();
					var content_encoding = httpWebResponse.Headers.Get("Content-Encoding");
					if (content_encoding == "gzip")
					{
						using var zipStream = new GZipStream(stream, CompressionMode.Decompress);
						using var reader = new StreamReader(zipStream, encoding);
						content = reader.ReadToEnd();
					}
					else
					{
						using var reader = new StreamReader(stream, encoding);
						content = reader.ReadToEnd();
					}
				}
				response = new HttpResponse(httpWebResponse, content);
			}
			return response;
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
