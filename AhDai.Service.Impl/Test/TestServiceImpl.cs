using AhDai.Entity.Sys;
using AhDai.Service.Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Service.Impl.Test;

/// <summary>
/// TestServiceImpl
/// </summary>
[Attributes.Service(ServiceLifetime.Singleton)]
internal class TestServiceImpl(ILogger<TestServiceImpl> logger)
    : BaseServiceImpl(logger)
    , ITestService
{
    public async Task<object?> TestAsync()
    {
        try
        {
            await Task.Run(() =>
            {
                Console.WriteLine("TestAsync");
            });
            await WashDataAsync2();
            return Task.FromResult<object?>(null);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    protected static async Task WashDataAsync2()
    {
        using var db = await MyApp.GetDefaultDbAsync();
        var users = await db.Users.Where(x => x.IsDeleted == false).ToArrayAsync();
        var userOrgs = await db.UserOrgs.Where(x => x.IsDeleted == false && x.IsDefault == true).ToArrayAsync();

        foreach (var user in users)
        {
            var userOrg = userOrgs.Where(x => x.UserId == user.Id).FirstOrDefault();
            if (userOrg == null)
            {
                continue;
            }
            var employee = new Employee()
            {
                Number = "",
                Name = user.Name,
                Birthday = user.Birthday,
                Gender = user.Gender,
                Email = user.Email,
                MobilePhone = user.MobilePhone,
                Telephone = user.Telephone,
                OrgId = userOrg.OrgId,
                PostIds = "",
                UserId = user.Id,
                Status = Shared.Enums.GenericStatus.Enabled,
            };
            //await db.Employees.AddAsync(employee);
        }

        //await db.SaveChangesAsync();
    }

   


}
