using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System;

[assembly: ResourceLocation("Resource Folder Name")]
[assembly: RootNamespace("App Root Namespace")]

namespace Adai.Core
{
	/// <summary>
	/// VerifyCodeHelper
	/// </summary>
	public static class VerifyCodeHelper
	{
		/// <summary>
		/// 生成图片验证码
		/// </summary>
		/// <param name="httpResponse"></param>
		/// <param name="guid"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static void GenerateImageCode(this HttpResponse httpResponse, string guid, int length = 4)
		{
			if (httpResponse == null || guid == null)
			{
				throw new ArgumentNullException("参数不能为空。");
			}
			var bytes = Adai.Standard.VerifyCodeHelper.GenerateImageCode(guid, length);
			httpResponse.Headers.Add("Access-Control-Expose-Headers", "X-VGuid");
			httpResponse.Headers.Add("X-VGuid", guid);
			httpResponse.ContentType = "image/png";
			httpResponse.StatusCode = StatusCodes.Status200OK;
			httpResponse.Body.WriteAsync(bytes, 0, bytes.Length);
		}

		/// <summary>
		/// 验证图片验证码
		/// </summary>
		/// <param name="httpRequest"></param>
		public static void VerifyImageCode(this HttpRequest httpRequest)
		{
			if (httpRequest == null)
			{
				throw new ArgumentNullException("参数不能为空。");
			}
			httpRequest.Headers.TryGetValue("X-VGuid", out var guid);
			httpRequest.Headers.TryGetValue("X-VCode", out var code);

			try
			{
				Adai.Standard.VerifyCodeHelper.VerifyImageCode(guid, code);
			}
			catch (ArgumentException ex)
			{
				var error = ex.Message switch
				{
					"Unique code cannot be empty." => "唯一编码已存在。",
					"Verification code error." => "验证码错误。",
					"Verification code timeout." => "验证码超时。",
					_ => ex.Message,
				};
				throw new ArgumentException(error);
			}
		}
	}
}
