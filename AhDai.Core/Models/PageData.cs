using System.Collections.Generic;

namespace AhDai.Core.Models;

/// <summary>
/// 分页数据
/// </summary>
/// <typeparam name="T"></typeparam>
public class PageData<T>
{
    /// <summary>
    /// 总数
    /// </summary>
    public int Count { get; set; }
    /// <summary>
    /// 数据
    /// </summary>
    public ICollection<T> List { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="count"></param>
    /// <param name="list"></param>
    public PageData(int count, ICollection<T> list)
    {
        Count = count;
        List = list;
    }
}
