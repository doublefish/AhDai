using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Adai.Standard.Extensions
{
	/// <summary>
	/// HttpResponseExtensions
	/// </summary>
	public static class HttpResponseExtensions
	{
		/// <summary>
		/// 写返回值
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="response"></param>
		/// <param name="data"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public static Task WriteObjectAsync<T>(this HttpResponse response, T data, CancellationToken cancellationToken = default) where T : class
		{
			var contentType = response.ContentType?.ToLower();
			response.StatusCode = StatusCodes.Status200OK;
			if (!string.IsNullOrEmpty(contentType) && (contentType == HttpContentType.Xml || contentType == HttpContentType.TextHtml))
			{
				return response.WriteAsync(Base.Utils.XmlHelper.SerializeObject(data), cancellationToken);
			}
			else
			{
				response.ContentType = HttpContentType.Json;
				return response.WriteAsync(Utils.JsonHelper.Serialize(data), cancellationToken);
			}
		}
	}
}
