using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using System.Threading.Tasks;
using System;

namespace AhDai.WebApi.Configs;

/// <summary>
/// CommaSeparatedArrayModelBinder
/// </summary>
public class CommaSeparatedArrayModelBinder : IModelBinder
{
	/// <summary>
	/// BindModelAsync
	/// </summary>
	/// <param name="bindingContext"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException"></exception>
	public Task BindModelAsync(ModelBindingContext bindingContext)
	{
		ArgumentNullException.ThrowIfNull(bindingContext);

		var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
		if (valueProviderResult == ValueProviderResult.None) return Task.CompletedTask;

		bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

		var value = valueProviderResult.FirstValue;
		if (string.IsNullOrEmpty(value)) return Task.CompletedTask;

		var elementType = bindingContext.ModelType.GetElementType() ?? bindingContext.ModelType.GetGenericArguments()[0];
		var array = value.Split(',', StringSplitOptions.RemoveEmptyEntries);

		var convertedArray = Array.CreateInstance(elementType, array.Length);
		for (var i = 0; i < array.Length; i++)
		{
			convertedArray.SetValue(Convert.ChangeType(array[i], elementType, CultureInfo.InvariantCulture), i);
		}

		bindingContext.Result = ModelBindingResult.Success(convertedArray);
		return Task.CompletedTask;
	}
}
