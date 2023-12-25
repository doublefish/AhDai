using AhDai.Core.Services;
using AhDai.Core.Utils;
using AhDai.Db;
using AhDai.Db.Models;
using AhDai.Service.Converters;
using AhDai.Service.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Service.Impl;

/// <summary>
/// FileServiceImpl
/// </summary>
internal class FileServiceImpl : BaseServiceImpl<File, FileOutput, FileQueryInput>, IFileService
{
	readonly Core.Services.IFileService _fileService;

	public FileServiceImpl(Core.Services.IFileService fileService)
	{
		_fileService = fileService;
	}

	public async Task<ICollection<FileOutput>> UploadAsync(params IFormFile[] files)
	{
		var data = await _fileService.UploadAsync(MyApp.WebRootPath, files);

		var models = new List<File>();
		foreach (var d in data)
		{
			models.Add(new File()
			{
				Name = d.Name + d.Extension,
				Extension = d.Extension,
				Type = d.Type,
				Length = d.Length,
				Path = d.VirtualPath,
				Hash = d.Hash
			});
		}
		using var db = new DefaultDbContext();
		db.Files.AddRange(models);
		db.SaveChanges();

		return models.ToOutputs();
	}

	/// <summary>
	/// 查询
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public override async Task<FileOutput> GetByIdAsync(int id)
	{
		return await base.GetByIdAsync(id);
	}

	/// <summary>
	/// 查询
	/// </summary>
	/// <param name="ids"></param>
	/// <returns></returns>
	public override async Task<ICollection<FileOutput>> GetByIdsAsync(int[] ids)
	{
		return await base.GetByIdsAsync(ids);
	}

	protected override IQueryable<File> GenerateQuery(DefaultDbContext db, IQueryable<File> query, FileQueryInput input)
	{
		if (!string.IsNullOrEmpty(input.Name))
		{
			query = query.Where(o => o.Name.Contains(input.Name));
		}
		if (!string.IsNullOrEmpty(input.Extension))
		{
			query = query.Where(o => o.Extension == input.Extension);
		}
		if (!string.IsNullOrEmpty(input.Type))
		{
			query = query.Where(o => o.Type == input.Type);
		}
		return query;
	}

	protected override FileOutput ToOutput(File model)
	{
		return model.ToOutput();
	}
}
