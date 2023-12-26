using AhDai.Base.Extensions;
using AhDai.Db.Models;
using AhDai.Service.Models;
using System.Collections.Generic;

namespace AhDai.Service.Converters;

internal static class DictDatumConverter
{
	/// <summary>
	/// ToModel
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public static DictDatum ToModel(this DictDatumInput input)
	{
		var model = new DictDatum()
		{
			Id = input.Id,
			DictId = input.DictId,
			Code = input.Code,
			Name = input.Name,
			Value = input.Value,
			Remark = input.Remark,
			Sort = input.Sort,
			Status = input.Status
		};
		return model;
	}

	/// <summary>
	/// ToModels
	/// </summary>
	/// <param name="inputs"></param>
	/// <returns></returns>
	public static ICollection<DictDatum> ToModels(this ICollection<DictDatumInput> inputs)
	{
		var models = new List<DictDatum>();
		foreach (var input in inputs)
		{
			models.Add(input.ToModel());
		}
		return models.ToArray();
	}

	/// <summary>
	/// ToOutput
	/// </summary>
	/// <param name="model"></param>
	/// <returns></returns>
	public static DictDatumOutput ToOutput(this DictDatum model)
	{
		var output = new DictDatumOutput()
		{
			DictId = model.DictId,
			Code = model.Code,
			Name = model.Name,
			Value = model.Value,
			Remark = model.Remark,
			Sort = model.Sort,
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
	public static ICollection<DictDatumOutput> ToOutputs(this ICollection<DictDatum> models)
	{
		var outputs = new List<DictDatumOutput>();
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
	public static DictDatumInput ToInput(this DictDatumOutput output)
	{
		var input = new DictDatumInput()
		{
			Id = output.Id,
			DictId = output.DictId,
			Code = output.Code,
			Name = output.Name,
			Value = output.Value,
			Remark = output.Remark,
			Sort = output.Sort,
			Status = output.Status
		};
		return input;
	}

	/// <summary>
	/// ToInputs
	/// </summary>
	/// <param name="outputs"></param>
	/// <returns></returns>
	public static ICollection<DictDatumInput> ToInputs(this ICollection<DictDatumOutput> outputs)
	{
		var inputs = new List<DictDatumInput>();
		foreach (var output in outputs)
		{
			inputs.Add(output.ToInput());
		}
		return inputs.ToArray();
	}


	/// <summary>
	/// ToValueNameDictionary
	/// </summary>
	/// <param name="data"></param>
	/// <returns></returns>
	public static Dictionary<string, string> ToStringValueNameDictionary(this ICollection<DictDatumOutput> data)
	{
		var dict = new Dictionary<string, string>();
		foreach (var d in data)
		{
			dict.Add(d.Value, d.Name);
		}
		return dict;
	}

	/// <summary>
	/// ToValueNameDictionary
	/// </summary>
	/// <param name="data"></param>
	/// <returns></returns>
	public static Dictionary<int, string> ToIntValueNameDictionary(this ICollection<DictDatumOutput> data)
	{
		var dict = new Dictionary<int, string>();
		foreach (var d in data)
		{
			dict.Add(d.Value.ToInt32(), d.Name);
		}
		return dict;
	}
}
