using System;

namespace AhDai.Entity;

/// <summary>
/// IDateEntity
/// </summary>
public interface IDateEntity
{
    /// <summary>
    /// 金额
    /// </summary>
    public DateTime Date { get; set; }
}

/// <summary>
/// INullableDateEntity
/// </summary>
public interface INullableDateEntity
{
    /// <summary>
    /// 金额
    /// </summary>
    public DateTime? Date { get; set; }
}
