namespace AhDai.Entity.Sys;

/// <summary>
/// 功能
/// </summary>
public class Function : BaseEntity
{
    public int MenuId { get; set; }

    public string Name { get; set; } = "";

    public string? Remark { get; set; }

    public int Status { get; set; }
}
