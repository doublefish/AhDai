// See https://aka.ms/new-console-template for more information


using Adai.DbContext;
using Adai.DbContext.MySql;
using Adai.DbContext.Test;

Console.WriteLine("Hello, World!");

var type = typeof(Program);
Console.WriteLine(type.FullName);

Class1.InitDbConfig();

var eventId = Guid.NewGuid().ToString();
var dbContext = new MySqlDbContext(eventId);

var sql = $"select * from User";
var list = dbContext.GetList<Adai.DbContext.Test.Models.User>("db", sql);

Console.WriteLine(list.Count);

