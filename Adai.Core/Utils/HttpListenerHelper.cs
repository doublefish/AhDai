using Adai.Base.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace Adai.Core.Utils
{
	/// <summary>
	/// HttpListenerHelper
	/// </summary>
	public static class HttpListenerHelper
	{
		/// <summary>
		/// 开启服务
		/// </summary>
		/// <param name="uriPrefix"></param>
		/// <param name="contentType"></param>
		/// <param name="callback"></param>
		public static void Start(string uriPrefix, Func<HttpListenerRequest, IDictionary<string, string>, string> callback, string contentType = HttpContentType.Text)
		{
			var listener = new HttpListener()
			{
				//指定身份验证为匿名访问
				AuthenticationSchemes = AuthenticationSchemes.Anonymous
			};
			listener.Prefixes.Add(uriPrefix);
			listener.Start();

			//线程池
			ThreadPool.GetMaxThreads(out var maxThreads, out var maxPortThreads);
			ThreadPool.GetMinThreads(out var minThreads, out var minPortThreads);

			var thread = new Thread(() =>
			{
				while (true)
				{
					//等待请求连接
					//没有请求则GetContext处于阻塞状态
					var httpContext = listener.GetContext();

					ThreadPool.QueueUserWorkItem(new WaitCallback((o) =>
					{
						var httpContext = (HttpListenerContext)o;

						//设置返回给客服端http状态代码
						httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
						httpContext.Response.ContentType = $"{contentType}; charset=utf-8";

						IDictionary<string, string> parameters;
						if (httpContext.Request.HttpMethod == HttpMethod.Get.Method)
						{
							parameters = httpContext.Request.QueryString.ToDictionary();
						}
						else
						{
							var body = "";
							using (var stream = httpContext.Request.InputStream)
							{
								using var reader = new StreamReader(stream, Encoding.UTF8);
								body = reader.ReadToEnd();
							}
							if (!string.IsNullOrEmpty(body))
							{
								var contentType = httpContext.Request.ContentType;
								if (!string.IsNullOrEmpty(contentType) && contentType.IndexOf("/json") != -1)
								{
									parameters = JsonHelper.DeserializeObject<IDictionary<string, string>>(body);
								}
								else
								{
									parameters = HttpHelper.ParseQueryString(body);
								}
							}
							else
							{
								parameters = new Dictionary<string, string>();
							}
						}

						var result = callback(httpContext.Request, parameters);

						//使用Writer输出http响应代码,UTF8格式
						using var writer = new StreamWriter(httpContext.Response.OutputStream, Encoding.UTF8);
						writer.Write(result);
						writer.Close();
						httpContext.Response.Close();
					}), httpContext);
				}
			});
			thread.Start();

			//return listener;
		}
	}
}
