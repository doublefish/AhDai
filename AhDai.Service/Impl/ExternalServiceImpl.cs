using AhDai.Core.Services;
using AhDai.Core.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Service.Impl;

internal class ExternalServiceImpl : IExternalService
{
	/// <summary>
	/// _Logger
	/// </summary>
	ILogger<ExternalServiceImpl> _logger;
	/// <summary>
	/// HttpClientFactory
	/// </summary>
	IHttpClientFactory _httpClientFactory;

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="httpClientFactory"></param>
	public ExternalServiceImpl(ILogger<ExternalServiceImpl> logger, IHttpClientFactory httpClientFactory)
	{
		_logger = logger;
		_httpClientFactory = httpClientFactory;
	}

	/// <summary>
	/// TestGetAsync
	/// </summary>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	public async Task<string> TestGetAsync()
	{
		var url = "https://www.baidu.com/";
		var client = _httpClientFactory.CreateClient();
		using var response = await client.GetAsync(url);
		var content = await response.Content.ReadAsStringAsync();
		if (response.StatusCode != HttpStatusCode.OK)
		{
			throw new Exception($"测试=>接口响应状态异常[{(int)response.StatusCode}]{content}");
		}
		return content;
	}
}
