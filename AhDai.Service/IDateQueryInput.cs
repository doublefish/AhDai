using System;

namespace AhDai.Service;

/// <summary>
/// 日期查询入参
/// </summary>
public interface IDateQueryInput
{
    /// <summary>
    /// 开始时间
    ///</summary>
    public DateTime? StartDate { get; set; }
    /// <summary>
    /// 结束时间
    ///</summary>
    public DateTime? EndDate { get; set; }
}
