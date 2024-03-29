﻿using AhDai.Db.Models;
using AhDai.Service.Models;
using System.Collections.Generic;

namespace AhDai.Service.Converters;

internal static class InterfaceConverter
{
	/// <summary>
	/// ToModel
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public static Interface ToModel(this InterfaceInput input)
	{
		var model = new Interface()
		{
			Name = input.Name,
			Method = input.Method,
			Url = input.Url,
			Remark = input.Remark,
			Status = input.Status
		};
		return model;
	}

	/// <summary>
	/// ToOutput
	/// </summary>
	/// <param name="model"></param>
	/// <returns></returns>
	public static InterfaceOutput ToOutput(this Interface model)
	{
		var output = new InterfaceOutput()
		{
			Name = model.Name,
			Method = model.Method,
			Url = model.Url,
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
	public static ICollection<InterfaceOutput> ToOutputs(this ICollection<Interface> models)
	{
		var outputs = new List<InterfaceOutput>();
		foreach (var model in models)
		{
			outputs.Add(model.ToOutput());
		}
		return outputs;
	}
}
