using AhDai.Db;
using AhDai.Entity.Sys;
using AhDai.Service.Models;
using AhDai.Service.Sys;
using AhDai.Service.Sys.Models;
using AhDai.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Service.Impl.Sys;

/// <summary>
/// MenuServiceImpl
/// </summary>
[Attributes.Service(ServiceLifetime.Singleton)]
internal class MenuServiceImpl(ILogger<MenuServiceImpl> logger
    , IUserService userService)
    : BaseServiceImpl<Menu, MenuInput, MenuOutput, MenuQueryInput>(logger, false, true)
    , IMenuService
{
    readonly IUserService _userService = userService;

    public Task<MenuConfigOutput> GetConfigAsync(bool includeDeleted = false)
    {
        var config = new MenuConfigOutput()
        {
            Types = ValueNameDataExtensions.FromEnum<MenuType>(),
        };
        return Task.FromResult(config);
    }

    public async Task EnableAsync(long id)
    {
        await UpdateStatusAsync(id, GenericStatus.Enabled);
    }

    public async Task DisableAsync(long id)
    {
        await UpdateStatusAsync(id, GenericStatus.Disabled);
    }

    static async Task UpdateStatusAsync(long id, GenericStatus status)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(status);
        using var db = await MyApp.GetDefaultDbAsync();
        await db.Menus.Where(x => x.Id == id && x.IsDeleted == false).ExecuteUpdateAsync(setter => setter.SetProperty(x => x.Status, status));
    }

    public async Task<bool> ExistAsync(long id, CodeExistInput input)
    {
        using var db = await MyApp.GetDefaultDbAsync();
        var uniqueHelper = new Helpers.UniqueHelper<Menu>(db);
        return await uniqueHelper.ExistAsync(x => x.Code, input.Code, id, "编码已存在");
    }

    public async Task<MenuOutput?> GetByCodeAsync(string code, int dataDepth = 1, bool includeDeleted = false)
    {
        ArgumentNullException.ThrowIfNull(code);
        return await GetOneAsync(x => x.Code == code, dataDepth, includeDeleted);
    }

    public async Task<MenuOutput[]> GetByCodesAsync(string[] codes, int dataDepth = 1, bool includeDeleted = false)
    {
        ArgumentNullException.ThrowIfNull(codes);
        return await GetAsync(x => codes.Contains(x.Code), dataDepth, includeDeleted);
    }

    public async Task<MenuOutput[]> GetAllAsync(int dataDepth = 0, bool includeDeleted = false)
    {
        var list = await GetAsync(x => true, dataDepth, includeDeleted);
        return list.ToTreeArray();
    }


    protected override async Task<long> SaveAsync(DefaultDbContext db, long id, MenuInput input, bool isUpdate)
    {
        var uniqueHelper = new Helpers.UniqueHelper<Menu>(db);
        return await uniqueHelper.ExistAsync(async () =>
        {
            if (isUpdate && input.ParentId != 0)
            {
                var parent = await db.Menus.Where(x => x.Id == input.ParentId && x.IsDeleted == false).AnyAsync();
                if (!parent) throw new ArgumentException("无效的父级Id");
            }
            return await base.SaveAsync(db, id, input, isUpdate);
        }, x => x.Code, input.Code, id, "编码已存在");
    }

    protected override Task BeforeSaveAsync(DefaultDbContext db, Menu entity, MenuInput input, bool isUpdate)
    {
        if (isUpdate)
        {
            entity.Code = input.Code;
            entity.Name = input.Name;
            //entity.ParentId = input.ParentId;
            entity.Type = input.Type;
            entity.Icon = input.Icon;
            entity.Router = input.Router;
            entity.Redirect = input.Redirect;
            entity.Component = input.Component;
            entity.Permission = input.Permission;
            entity.Application = input.Application;
            entity.FormId = input.FormId;
            entity.Hidden = input.Hidden;
            entity.Remark = input.Remark;
            entity.Sort = input.Sort;
        }
        else
        {
            entity.Status = GenericStatus.Enabled;
        }
        return Task.CompletedTask;
    }


    protected override async Task DeleteByIdsAsync(DefaultDbContext db, long[] ids)
    {
        var hasChild = await db.Menus.Where(x => ids.Contains(x.ParentId) && x.IsDeleted == false).AnyAsync();
        if (hasChild) throw new Exception("不允许删除有子级的数据");
        await base.DeleteByIdsAsync(db, ids);
    }

    protected override IOrderedQueryable<Menu> GenerateQueryOrder(IQueryable<Menu> query)
    {
        return query.OrderBy(x => x.ParentId).ThenBy(x => x.Sort).ThenBy(x => x.CreationTime);
    }

    protected override Task<IQueryable<Menu>> GenerateQueryAsync(DefaultDbContext db, IQueryable<Menu> query, MenuQueryInput input)
    {
        if (!string.IsNullOrEmpty(input.Keyword))
        {
            query = query.Where(x => x.Code.Contains(input.Keyword) || x.Name.Contains(input.Keyword));
        }
        if (!string.IsNullOrEmpty(input.Code))
        {
            query = query.Where(x => x.Code == input.Code);
        }
        if (input.Codes != null && input.Codes.Length > 0)
        {
            query = query.Where(x => input.Codes.Contains(x.Code));
        }
        if (!string.IsNullOrEmpty(input.Name))
        {
            query = query.Where(x => x.Name.Contains(input.Name));
        }
        if (input.ParentId.HasValue)
        {
            query = query.Where(x => x.ParentId == input.ParentId.Value);
        }
        if (input.Type.HasValue)
        {
            query = query.Where(x => x.Type == input.Type.Value);
        }
        if (input.Types != null && input.Types.Length > 0)
        {
            query = query.Where(x => input.Types.Contains(x.Type));
        }
        if (input.Status.HasValue)
        {
            query = query.Where(x => x.Status == input.Status.Value);
        }
        return base.GenerateQueryAsync(db, query, input);
    }

    protected override async Task GetAssociatedDataAsync(DefaultDbContext db, MenuOutput[] outputs, int dataDepth)
    {
        if (outputs == null || outputs.Length == 0 || dataDepth < 1) return;

        var ids = new HashSet<long>();
        var formIds = new HashSet<long>();
        var userIds = new HashSet<long>();
        foreach (var output in outputs)
        {
            ids.Add(output.Id);
            if (output.FormId.HasValue) formIds.Add(output.FormId.Value);
            userIds.Add(output.CreatorId);
            if (output.ModifierId.HasValue) userIds.Add(output.ModifierId.Value);
        }

        //var getFormTask = _formService.GetByIdsAsync([.. userIds], dataDepth - 1, true);
        var getUserTask = _userService.GetByIdsAsync([.. userIds], dataDepth - 1, true);
        //await Task.WhenAll(getFormTask, getUserTask);

        //var forms = getFormTask.Result;
        var users = getUserTask.Result;

        foreach (var output in outputs)
        {
            //if (output.FormId.HasValue) output.FormName = forms.Where(x => x.Id == output.FormId.Value).FirstOrDefault()?.Name;
            output.SetBaseInfo(users);
        }
        await base.GetAssociatedDataAsync(db, outputs, dataDepth);
    }

}
