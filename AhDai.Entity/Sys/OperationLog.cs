namespace AhDai.Entity.Sys;

/// <summary>
/// 操作日志
/// </summary>
public class OperationLog : BaseEntity
{
    public string MenuName { get; set; } = "";

    public string FunctionName { get; set; } = "";

    public string ApiUrl { get; set; } = "";

    public string Type { get; set; } = "";

    public string Content { get; set; } = "";
}
