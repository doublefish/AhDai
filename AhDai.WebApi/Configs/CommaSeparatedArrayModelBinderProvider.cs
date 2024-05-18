using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace AhDai.WebApi.Configs
{
	/// <summary>
	/// CommaSeparatedArrayModelBinderProvider
	/// </summary>
	public class CommaSeparatedArrayModelBinderProvider : IModelBinderProvider
	{
		/// <summary>
		/// GetBinder
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public IModelBinder? GetBinder(ModelBinderProviderContext context)
		{
			ArgumentNullException.ThrowIfNull(context);
			if (context.Metadata.ModelType.IsArray)
			{
				var elementType = context.Metadata.ModelType.GetElementType();
				if (elementType == typeof(string) || elementType == typeof(long) || elementType == typeof(int) || elementType == typeof(decimal))
				{
					return new CommaSeparatedArrayModelBinder();
				}
			}
			return null;
		}
	}
}
