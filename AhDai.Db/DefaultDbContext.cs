using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AhDai.Db;

/// <summary>
/// DefaultDbContext
/// </summary>
public partial class DefaultDbContext : Microsoft.EntityFrameworkCore.DbContext
{
	public DefaultDbContext()
	{
	}

	public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
		: base(options)
	{
	}

	public virtual DbSet<Interface> Interfaces { get; set; }

	public virtual DbSet<Dict> Dicts { get; set; }

	public virtual DbSet<DictDatum> DictData { get; set; }

	public virtual DbSet<File> Files { get; set; }

	public virtual DbSet<Function> Functions { get; set; }

	public virtual DbSet<FunctionInterface> FunctionInterfaces { get; set; }

	public virtual DbSet<Menu> Menus { get; set; }

	public virtual DbSet<OperationLog> OperationLogs { get; set; }

	public virtual DbSet<Organization> Organizations { get; set; }

	public virtual DbSet<Role> Roles { get; set; }

	public virtual DbSet<RoleFunction> RoleFunctions { get; set; }

	public virtual DbSet<User> Users { get; set; }

	public virtual DbSet<UserFunction> UserFunctions { get; set; }

	public virtual DbSet<UserPassword> UserPasswords { get; set; }


	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var config = Core.Utils.DbContextUtil.GetConfig();
		optionsBuilder.UseSqlServer(config.ConnectionString, options =>
		{

		});
		optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder =>
		{
			builder.AddConsole();
			builder.AddDebug();
		}));
		optionsBuilder.AddInterceptors(new Interceptors.MySaveChangesInterceptor());
		base.OnConfiguring(optionsBuilder);

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		var schema = "dbo";
		modelBuilder.HasDefaultSchema(schema);

		modelBuilder.ApplyConfiguration(new Configuration.DictConfiguration());
		modelBuilder.ApplyConfiguration(new Configuration.DictDatumConfiguration());

		modelBuilder.ApplyConfiguration(new Configuration.OrganizationConfiguration());

		modelBuilder.ApplyConfiguration(new Configuration.FileConfiguration());
		modelBuilder.ApplyConfiguration(new Configuration.OperationLogConfiguration());

		modelBuilder.ApplyConfiguration(new Configuration.InterfaceConfiguration());
		modelBuilder.ApplyConfiguration(new Configuration.MenuConfiguration());
		modelBuilder.ApplyConfiguration(new Configuration.FunctionConfiguration());
		modelBuilder.ApplyConfiguration(new Configuration.FunctionInterfaceConfiguration());
		modelBuilder.ApplyConfiguration(new Configuration.RoleConfiguration());
		modelBuilder.ApplyConfiguration(new Configuration.RoleFunctionConfiguration());
		modelBuilder.ApplyConfiguration(new Configuration.UserConfiguration());
		modelBuilder.ApplyConfiguration(new Configuration.UserPasswordConfiguration());
		modelBuilder.ApplyConfiguration(new Configuration.UserFunctionConfiguration());

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
