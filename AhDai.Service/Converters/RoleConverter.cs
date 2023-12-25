using AhDai.Db.Models;
using AhDai.Service.Models;
using System.Collections.Generic;

namespace AhDai.Service.Converters;

internal static class RoleConverter
{
	/// <summary>
	/// ToOutput
	/// </summary>
	/// <param name="model"></param>
	/// <returns></returns>
	public static RoleOutput ToOutput(this Role model)
	{
		var output = new RoleOutput()
		{
			Code = model.Code,
			Name = model.Name,
			Remark = model.Remark,
			Status = model.Status
		};
		output.CopyBase(model);
		return output;
	}

	/// <summary>
	/// ToOutputs
	/// </summary>
	/// <param name="models"></param>
	/// <returns></returns>
	public static ICollection<RoleOutput> ToOutputs(this ICollection<Role> models)
	{
		var outputs = new List<RoleOutput>();
		foreach (var model in models)
		{
			outputs.Add(model.ToOutput());
		}
		return outputs;
	}

	/// <summary>
	/// ToModel
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public static Role ToModel(this RoleInput input)
	{
		var model = new Role()
		{
			Code = input.Code,
			Name = input.Name,
			Remark = input.Remark,
			Status = input.Status
		};
		return model;
	}
}
