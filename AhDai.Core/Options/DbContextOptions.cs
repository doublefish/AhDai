using System.Collections.Generic;

namespace AhDai.Core.Options;

/// <summary>
/// DbContextOptions
/// </summary>
public class DbContextOptions
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public ICollection<DbContext.Models.DbConfig> Configs { get; set; } = default!;
}
