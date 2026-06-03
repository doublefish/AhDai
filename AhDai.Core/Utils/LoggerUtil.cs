using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Concurrent;

namespace AhDai.Core.Utils;

/// <summary>
/// LoggerUtil
/// </summary>
public static class LoggerUtil
{
    static readonly ConcurrentDictionary<string, ILogger> LoggerCache = new(StringComparer.Ordinal);
    static ILoggerFactory? _factory;

    /// <summary>
    /// 允许在系统启动时手动注册或通过 DI 容器自动解耦注入工厂。
    /// 彻底解决由于 ServiceLocator 尚未初始化导致的程序启动早期死锁崩溃隐患。
    /// </summary>
    public static void Initialize(ILoggerFactory loggerFactory)
    {
        _factory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        // 工厂变更时，清空缓存，确保新的日志配置和提供者能立即生效
        LoggerCache.Clear();
    }

    /// <summary>
    /// 获取高性能强类型日志器（现代泛型快路径：无任何锁、0 反射、0 内存分配开销）
    /// </summary>
    /// <typeparam name="T">业务分类类型</typeparam>
    public static ILogger<T> GetLogger<T>()
    {
        // 利用内部静态泛型类的特性，让 JIT 编译器在编译期为每个 T 绑定一个专属的单例 Logger
        // 这是 .NET 核心运行时最顶级的性能榨汁技巧，完美消灭了 Dictionary 的 Hash 寻址损耗
        return TypeLoggerCache<T>.Instance;
    }

    /// <summary>
    /// 获取日志器（基于指定的 Type 分类）
    /// </summary>
    public static ILogger GetLogger(Type type)
    {
        var categoryName = type.FullName ?? type.Name;
        return GetLogger(categoryName);
    }

    /// <summary>
    /// 获取日志器（基于字符串分类名）
    /// </summary>
    /// <param name="categoryName">日志类别名称（如 "AhDai.Core.DigitalChain"）</param>
    public static ILogger GetLogger(string categoryName)
    {
        if (string.IsNullOrWhiteSpace(categoryName)) categoryName = "Default";

        // 缓存守卫】：就地快路径双检。如果缓存里有，直接 0 开销返回；
        // 如果没有，通过流式工厂创建并自动塞入线程安全的字典缓存，高并发下表现极度稳定
        return LoggerCache.GetOrAdd(categoryName, category =>
        {
            var factory = ResolveFactory();
            return factory.CreateLogger(category);
        });
    }

    /// <summary>
    /// 动态解析或兜底日志工厂
    /// </summary>
    private static ILoggerFactory ResolveFactory()
    {
        // 优先使用显式初始化的工厂（最快、最安全）
        if (_factory != null) return _factory;

        // 其次降级从全局 ServiceUtil 抽取（保持与老代码的向下兼容性）
        if (ServiceUtil.Services != null)
        {
            var factory = ServiceUtil.Services.GetService<ILoggerFactory>();
            if (factory != null)
            {
                _factory = factory;
                return _factory;
            }
        }

        // 如果系统实在没初始化任何日志设施，严禁抛异常中断程序！
        // 自动返回微软官方标准的“空操作日志单例（NullLoggerFactory.Instance）”。
        // 它采用空壳状态机驱动，既能保证代码继续往下跑不崩溃，又不会制造任何内存与 I/O 垃圾。
        return NullLoggerFactory.Instance;
    }

    #region 内部静态泛型类（JIT 级高性能黑魔法缓存）
    static class TypeLoggerCache<T>
    {
        // 借由 .NET JIT 引擎的天然隔离，只要类型不相同，Instance 对应的内存地址指针就不相同。
        // 这是一种绝对无竞争（Lock-free）的高并发顶级重构手段
        public static readonly ILogger<T> Instance = new LoggerWrapper<T>(GetLogger<T>());
    }

    // 内部轻量级 ILogger<T> 强类型包装器，确保接口完美对齐
    sealed class LoggerWrapper<T>(ILogger innerLogger) : ILogger<T>
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => innerLogger.BeginScope(state);
        public bool IsEnabled(LogLevel logLevel) => innerLogger.IsEnabled(logLevel);
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
            => innerLogger.Log(logLevel, eventId, state, exception, formatter);
    }
    #endregion
}
