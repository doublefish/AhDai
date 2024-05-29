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
            if (!context.Metadata.ModelType.IsArray) return null;
            var elementType = context.Metadata.ModelType.GetElementType();
            if (elementType == null) return null;
            if (elementType.IsEnum || elementType == typeof(string)
                || elementType == typeof(long) || elementType == typeof(int)
                || elementType == typeof(decimal) || elementType == typeof(double))
            {
                return new CommaSeparatedArrayModelBinder();
            }
            return null;
        }
	}
}
