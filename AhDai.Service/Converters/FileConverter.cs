using AhDai.Core.Utils;
using AhDai.Db.Models;
using AhDai.Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace AhDai.Service.Converters;

internal static class FileConverter
{
	/// <summary>
	/// ToOutput
	/// </summary>
	/// <param name="model"></param>
	/// <returns></returns>
	public static FileOutput ToOutput(this File model)
	{
		var httpContext = ServiceUtil.HttpContextAccessor.HttpContext;
		var root = $"{httpContext.Request.Scheme}://{httpContext.Request.Host.Value}";
		var output = new FileOutput()
		{
			Name = model.Name,
			Extension = model.Extension,
			Type = model.Type,
			Length = model.Length,
			RelativePath = model.Path,
			Path = root + model.Path,
			Hash = model.Hash
		};
		output.CopyBase(model);
		return output;
	}

	/// <summary>
	/// ToOutputs
	/// </summary>
	/// <param name="models"></param>
	/// <returns></returns>
	public static ICollection<FileOutput> ToOutputs(this ICollection<File> models)
	{
		var outputs = new List<FileOutput>();
		foreach (var model in models)
		{
			outputs.Add(model.ToOutput());
		}
		return outputs;
	}

	/// <summary>
	/// ToModel
	/// </summary>
	/// <param name="model"></param>
	/// <returns></returns>
	public static File ToModel(this FileInput model)
	{
		var output = new File()
		{
			Name = model.Name,
		};
		return output;
	}
}
