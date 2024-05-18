namespace AhDai.Service;

/// <summary>
/// 流程数据入参
/// </summary>
public interface IProcessInput : IBaseInput
{
    /// <summary>
    /// 是否提交
    /// </summary>
    public bool Submit { get; set; }
}
