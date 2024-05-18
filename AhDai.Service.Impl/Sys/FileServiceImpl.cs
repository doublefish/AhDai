using AhDai.Core.Services;
using AhDai.Db;
using AhDai.Entity.Sys;
using AhDai.Service.Sys;
using AhDai.Service.Sys.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Service.Impl.Sys;

/// <summary>
/// FileServiceImpl
/// </summary>
[Attributes.Service(ServiceLifetime.Singleton)]
internal class FileServiceImpl(ILogger<FileServiceImpl> logger
	, IBaseFileService fileService
	, IUserService userService)
	: BaseServiceImpl<File, FileInput, FileOutput, FileQueryInput>(logger)
	, IFileService
{
	readonly IBaseFileService _fileService = fileService;
	readonly IUserService _userService = userService;

	public async Task<FileOutput> UploadAsync(IFormFile file)
	{
		var outputs = await UploadAsync([file]);
		return outputs.First();
	}

	public async Task<FileOutput[]> UploadAsync(IFormFile[] files)
	{
		var data = await _fileService.UploadAsync(MyApp.WebRootPath, files);

		var entities = new List<File>();
		foreach (var d in data)
		{
			entities.Add(new File()
			{
				Name = d.Name + d.Extension,
				Extension = d.Extension[1..],
				Type = d.Type,
				Length = d.Length,
				Path = d.VirtualPath,
				Hash = d.Hash ?? ""
			});
		}
		using var db = await MyApp.GetDefaultDbAsync();
		db.Files.AddRange(entities);
		db.SaveChanges();

		var outputs = Mapper.Map<FileOutput[]>(entities);
		await GetAssociatedDataAsync(db, outputs, 1);
		return outputs;
	}

	protected override Task<IQueryable<File>> GenerateQueryAsync(DefaultDbContext db, IQueryable<File> query, FileQueryInput input)
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
		if (input.Types != null && input.Types.Length > 0)
		{
			query = query.Where(x => input.Types.Contains(x.Type));
		}
		return base.GenerateQueryAsync(db, query, input);
	}


	protected override async Task GetAssociatedDataAsync(DefaultDbContext db, FileOutput[] outputs, int dataDepth)
	{
		if (outputs == null || outputs.Length == 0 || dataDepth < 1) return;

		var ids = new HashSet<long>();
		var userIds = new HashSet<long>();
		foreach (var output in outputs)
		{
			ids.Add(output.Id);
			userIds.Add(output.CreatorId);
			if (output.ModifierId.HasValue) userIds.Add(output.ModifierId.Value);
		}

		var getUserTask = _userService.GetByIdsAsync([.. userIds], dataDepth - 1, true);
		await getUserTask;

		var users = getUserTask.Result;

		foreach (var output in outputs)
		{
			output.SetBaseInfo(users);
		}
		await base.GetAssociatedDataAsync(db, outputs, dataDepth);
	}


}
