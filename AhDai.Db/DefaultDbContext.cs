using AhDai.Entity.Sys;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AhDai.Db;

/// <summary>
/// DefaultDbContext
/// </summary>
/// <param name="options"></param>
public partial class DefaultDbContext(DbContextOptions<DefaultDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    #region 系统


    public virtual DbSet<Dict> Dicts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<File> Files { get; set; }

    public virtual DbSet<Interface> Interfaces { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleMenu> RoleMenus { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    public virtual DbSet<TenantMenu> TenantMenus { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserExtension> UserExtensions { get; set; }

    public virtual DbSet<UserPassword> UserPasswords { get; set; }

    public virtual DbSet<UserFunction> UserFunctions { get; set; }

    public virtual DbSet<UserOrg> UserOrgs { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }


    public virtual DbSet<OperationLog> OperationLogs { get; set; }

    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Chinese_PRC_CI_AS");

        OnSysModelCreating(modelBuilder);
        OnModelCreatingPartial(modelBuilder);

        var allProperties = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).ToArray();

        // 设置全局精度和小数位数
        var decimalType = typeof(decimal);
        var decimalType2 = typeof(decimal?);
        foreach (var property in allProperties)
        {
            if (property.ClrType == decimalType || property.ClrType == decimalType2)
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }
        }
    }

    /// <summary>
    /// 系统
    /// </summary>
    /// <param name="modelBuilder"></param>
    static void OnSysModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Configuration.Sys.DictConfiguration());
        //modelBuilder.ApplyConfiguration(new Configuration.Sys.DictDatumConfiguration());
        modelBuilder.ApplyConfiguration(new Configuration.Sys.EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new Configuration.Sys.FileConfiguration());
        modelBuilder.ApplyConfiguration(new Configuration.Sys.InterfaceConfiguration());
        modelBuilder.ApplyConfiguration(new Configuration.Sys.MenuConfiguration());
        modelBuilder.ApplyConfiguration(new Configuration.Sys.OperationLogConfiguration());
        modelBuilder.ApplyConfiguration(new Configuration.Sys.OrganizationConfiguration());
        modelBuilder.ApplyConfiguration(new Configuration.Sys.PostConfiguration());
        modelBuilder.ApplyConfiguration(new Configuration.Sys.RoleConfiguration());
        modelBuilder.ApplyConfiguration(new Configuration.Sys.RoleMenuConfiguration());
        modelBuilder.ApplyConfiguration(new Configuration.Sys.TenantConfiguration());
        modelBuilder.ApplyConfiguration(new Configuration.Sys.TenantMenuConfiguration());
        modelBuilder.ApplyConfiguration(new Configuration.Sys.UserConfiguration());
        modelBuilder.ApplyConfiguration(new Configuration.Sys.UserExtensionConfiguration());
        modelBuilder.ApplyConfiguration(new Configuration.Sys.UserOrgConfiguration());
        modelBuilder.ApplyConfiguration(new Configuration.Sys.UserPasswordConfiguration());
        modelBuilder.ApplyConfiguration(new Configuration.Sys.UserRoleConfiguration());
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
