using AhDai.Service.Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
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
        var filePath = @"C:\Users\shuang\Desktop\俞双\haopin.sql";
        var sqls1 = new List<string>();
        var sqls2 = new List<string>();
        using var reader = new StreamReader(filePath);
        while (true)
        {
            var line = reader.ReadLine();
            if (line == null) break;
            if (line.StartsWith("INSERT INTO `ask_info`"))
            {
                var temps = line.Split("),");
                sqls1.Add(temps[0].Replace("`ask_info`", "[askinfo]") + ");");
                for (var i = 1; i < temps.Length - 1; i++)
                {
                    sqls1.Add("INSERT INTO [askinfo] VALUES " + temps[i] + ");");
                }
                sqls1.Add("INSERT INTO [askinfo] VALUES " + temps[^1]);
            }
            else if (line.StartsWith("INSERT INTO `good`"))
            {
                var temps = line.Split("),");
                sqls2.Add(temps[0].Replace("`good`", "[good]") + ");");
                for (var i = 1; i < temps.Length - 1; i++)
                {
                    sqls2.Add("INSERT INTO [good] VALUES " + temps[i] + ");");
                }
                sqls1.Add("INSERT INTO [good] VALUES " + temps[^1]);
            }
        }
        using var db = await MyApp.GetDefaultDbAsync();
        for (var i = 0; i < sqls1.Count; i++)
        {
            //Console.WriteLine(sqls1[i]);
            await db.Database.ExecuteSqlRawAsync(sqls1[0]);
        }
        for (var i = 0; i < sqls2.Count; i++)
        {
            //Console.WriteLine(sqls1[i]);
            await db.Database.ExecuteSqlRawAsync(sqls2[0]);
        }

        //await db.SaveChangesAsync();
    }




}
