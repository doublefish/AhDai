using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Adai.Standard.Helper
{
	/// <summary>
	/// JsonPropertyContractResolver
	/// </summary>
	public class JsonPropertyContractResolver : DefaultContractResolver
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="includeProperties"></param>
		/// <param name="excludeProperties"></param>
		public JsonPropertyContractResolver(IEnumerable<string> includeProperties, IEnumerable<string> excludeProperties = null)
		{
			_IncludeProperties = includeProperties;
			_ExcludeProperties = excludeProperties;
		}

		IEnumerable<string> _IncludeProperties;
		IEnumerable<string> _ExcludeProperties;

		/// <summary>
		/// CreateProperties
		/// </summary>
		/// <param name="type"></param>
		/// <param name="memberSerialization"></param>
		/// <returns></returns>
		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			// 需要输出的属性 
			var properties = base.CreateProperties(type, memberSerialization).ToList();
			if (_IncludeProperties != null)
			{
				properties = properties.FindAll(p => _IncludeProperties.Contains(p.PropertyName));
			}
			if (_ExcludeProperties != null)
			{
				properties = properties.FindAll(p => !_ExcludeProperties.Contains(p.PropertyName));
			}
			return properties;
		}
	}
}