using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AhDai.Service.Impl.Utils;

/// <summary>
/// ExcelUtil
/// </summary>
internal class ExcelUtil
{
	/// <summary>
	/// CreateWorkbook
	/// </summary>
	/// <param name="stream"></param>
	/// <param name="fileName"></param>
	/// <returns></returns>
	public static IWorkbook CreateWorkbook(Stream stream, string? fileName = null)
	{
		if (!string.IsNullOrEmpty(fileName))
		{
			var extension = Path.GetExtension(fileName);
			return string.Compare(extension, ".xlsx", StringComparison.OrdinalIgnoreCase) == 0 ? new XSSFWorkbook(stream) : new HSSFWorkbook(stream);
		}
		try
		{
			return new XSSFWorkbook(stream);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
			return new HSSFWorkbook(stream);
		}
	}


	private static IDictionary<int, PropertyInfo> GetPropertyInfoIndexer<T>(IDictionary<string, string> columns)
	{
		ArgumentNullException.ThrowIfNull(columns);
		var properties = typeof(T).GetProperties();
		var indexer = new Dictionary<int, PropertyInfo>();
		for (var i = 0; i < columns.Count; i++)
		{
			var c = columns.ElementAt(i);
			var pi = properties.Where(x => string.Compare(x.Name, c.Key, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
			if (pi != null)
			{
				indexer.Add(i, pi);
			}
		}
		return indexer;
	}

	/// <summary>
	/// ToList
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="workBook"></param>
	/// <param name="columns"></param>
	/// <param name="headerIndex"></param>
	/// <returns></returns>
	public static T[] ToArray<T>(IWorkbook workBook, IDictionary<string, string> columns, int headerIndex = 0)
		where T : new()
	{
		ArgumentNullException.ThrowIfNull(workBook);
		ArgumentNullException.ThrowIfNull(columns);

		var list = new List<T>();

		var sheet = workBook.GetSheetAt(0);
		var indexer = GetPropertyInfoIndexer<T>(columns);
		if (indexer.Count != columns.Count)
		{
			throw new Exception("文件列头与模板不一致");
		}
		var lastRowNum = sheet.LastRowNum;

		for (var i = headerIndex + 1; i <= lastRowNum; i++)
		{
			var row = sheet.GetRow(i);
			if (row == null) continue;

			var obj = new T();
			foreach (var kv in indexer)
			{
				var cell = row.GetCell(kv.Key);
				if (cell == null)
				{
					continue;
				}
				try
				{
					var cellValue = cell.ToString();
					if (kv.Value.PropertyType == typeof(string))
					{
						kv.Value.SetValue(obj, cellValue, null);
					}
					else
					{
						var value = Convert.ChangeType(cellValue, kv.Value.PropertyType);
						kv.Value.SetValue(obj, value, null);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					throw;
				}
			}
			list.Add(obj);
		}
		return [.. list];
	}

	/// <summary>
	/// ToList
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="fileName"></param>
	/// <param name="stream"></param>
	/// <param name="columns"></param>
	/// <param name="headerIndex"></param>
	/// <returns></returns>
	public static T[] ToArray<T>(Stream stream, string fileName, IDictionary<string, string> columns, int headerIndex = 0)
		where T : new()
	{
		ArgumentNullException.ThrowIfNull(fileName);
		ArgumentNullException.ThrowIfNull(stream);
		ArgumentNullException.ThrowIfNull(columns);

		var workBook = CreateWorkbook(stream, fileName);
		return ToArray<T>(workBook, columns, headerIndex);
	}

	/// <summary>
	/// ToList
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="stream"></param>
	/// <param name="columns"></param>
	/// <param name="headerIndex"></param>
	/// <returns></returns>
	public static T[] ToArray<T>(Stream stream, IDictionary<string, string> columns, int headerIndex = 0)
		where T : new()
	{
		ArgumentNullException.ThrowIfNull(stream);
		ArgumentNullException.ThrowIfNull(columns);

		var workBook = CreateWorkbook(stream);
		return ToArray<T>(workBook, columns, headerIndex);
	}

	/// <summary>
	/// ToList
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="file"></param>
	/// <param name="columns"></param>
	/// <param name="headerIndex"></param>
	/// <returns></returns>
	public static T[] ToArray<T>(IFormFile file, IDictionary<string, string> columns, int headerIndex = 0)
		where T : new()
	{
		ArgumentNullException.ThrowIfNull(file);
		ArgumentNullException.ThrowIfNull(columns);

		using var stream = file.OpenReadStream();
		return ToArray<T>(stream, file.FileName, columns, headerIndex);
	}
}
