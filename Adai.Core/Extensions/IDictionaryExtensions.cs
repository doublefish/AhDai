using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adai.Core.Extensions
{
	/// <summary>
	/// IDictionaryExtensions
	/// </summary>
	public static class IDictionaryExtensions
	{
		/// <summary>
		/// ToQueryString
		/// </summary>
		/// <param name="pairs"></param>
		/// <param name="ignores"></param>
		/// <returns></returns>
		public static string ToQueryString(this IEnumerable<KeyValuePair<string, StringValues>> pairs, params string[] ignores)
		{
			if (pairs == null || !pairs.Any())
			{
				return "";
			}
			var builder = new StringBuilder();
			foreach (var kv in pairs)
			{
				if (kv.Value.Count == 0 || (ignores != null && ignores.Contains(kv.Key)))
				{
					continue;
				}
				builder.Append($"&{kv.Key}={kv.Value}");
			}
			return builder.Remove(0, 1).ToString();
		}
	}
}
