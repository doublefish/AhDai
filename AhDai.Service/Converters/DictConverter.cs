using AhDai.Db.Models;
using AhDai.Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace AhDai.Service.Converters;

internal static class DictConverter
{
	/// <summary>
	/// ToOutput
	/// </summary>
	/// <param name="model"></param>
	/// <returns></returns>
	public static DictOutput ToOutput(this Dict model)
	{
		var output = new DictOutput()
		{
			Code = model.Code,
			Name = model.Name,
			Remark = model.Remark,
			Status = model.Status
		};
		output.CopyBase(model);
		if (model.Data != null)
		{
			output.Data = model.Data.ToOutputs().ToArray();
		}
		return output;
	}

	/// <summary>
	/// ToOutputs
	/// </summary>
	/// <param name="models"></param>
	/// <returns></returns>
	public static ICollection<DictOutput> ToOutputs(this ICollection<Dict> models)
	{
		var outputs = new List<DictOutput>();
		foreach (var model in models)
		{
			outputs.Add(model.ToOutput());
		}
		return outputs.ToArray();
	}

	/// <summary>
	/// ToInput
	/// </summary>
	/// <param name="output"></param>
	/// <returns></returns>
	public static DictInput ToInput(this DictOutput output)
	{
		var input = new DictInput()
		{
			Code = output.Code,
			Name = output.Name,
			Remark = output.Remark,
			Status = output.Status
		};
		if (output.Data != null)
		{
			input.Data = output.Data.ToInputs().ToArray();
		}
		return input;
	}

	/// <summary>
	/// ToInputs
	/// </summary>
	/// <param name="outputs"></param>
	/// <returns></returns>
	public static ICollection<DictInput> ToInputs(this ICollection<DictOutput> outputs)
	{
		var inputs = new List<DictInput>();
		foreach (var output in outputs)
		{
			inputs.Add(output.ToInput());
		}
		return inputs.ToArray();
	}

	/// <summary>
	/// ToModel
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public static Dict ToModel(this DictInput input)
	{
		var model = new Dict()
		{
			Code = input.Code,
			Name = input.Name,
			Remark = input.Remark,
			Status = input.Status,
			Data = input.Data.ToModels()
		};
		return model;
	}
}
