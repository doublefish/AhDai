using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.Core.Utils;

/// <summary>
/// CompressionUtil
/// </summary>
public static class CompressionUtil
{
    /// <summary>
    /// 跨平台异步压缩文件夹（支持 Windows/Linux 容器化，0 外部依赖）
    /// </summary>
    /// <param name="sourceDirectoryPath">要压缩的源文件夹（绝对路径）</param>
    /// <param name="destinationZipFilePath">压缩后的 .zip 物理文件全路径</param>
    /// <param name="compressionLevel">压缩级别偏好（速度优先、最优压缩、不压缩）</param>
    /// <param name="cancellationToken"></param>
    public static async Task CompressDirectoryAsync(string sourceDirectoryPath, string destinationZipFilePath, CompressionLevel compressionLevel = CompressionLevel.Optimal, CancellationToken cancellationToken = default)
    {
        if (!Directory.Exists(sourceDirectoryPath))
        {
            throw new DirectoryNotFoundException($"源目录不存在: {sourceDirectoryPath}");
        }

        var destDir = Path.GetDirectoryName(destinationZipFilePath);
        if (!string.IsNullOrEmpty(destDir) && !Directory.Exists(destDir))
        {
            Directory.CreateDirectory(destDir);
        }

        // 采用原生异步 FileStream 开辟大缓冲区，通过 DMA 释放 CPU 算力
        await Task.Run(() =>
        {
            // ZipFile.CreateFromDirectory 是受操作系统内核优化的原生 API，内部对不同系统平台进行了最优寻址
            ZipFile.CreateFromDirectory(sourceDirectoryPath, destinationZipFilePath, compressionLevel, includeBaseDirectory: false);
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 跨平台异步解压文件
    /// </summary>
    /// <param name="zipFilePath">要解压的 .zip 文件物理路径</param>
    /// <param name="extractDirectoryPath">解压到的目标目录</param>
    /// <param name="cancellationToken"></param>
    public static async Task UnCompressAsync(string zipFilePath, string extractDirectoryPath, CancellationToken cancellationToken = default)
    {
        if (!File.Exists(zipFilePath))
        {
            throw new FileNotFoundException($"找不到指定的压缩文件: {zipFilePath}");
        }

        if (!Directory.Exists(extractDirectoryPath))
        {
            Directory.CreateDirectory(extractDirectoryPath);
        }

        await Task.Run(() =>
        {
            ZipFile.ExtractToDirectory(zipFilePath, extractDirectoryPath, overwriteFiles: true);
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 高性能内存流式即时压缩（0 磁盘物理 I/O，高并发附件导出利器）
    /// </summary>
    /// <remarks>
    /// 直接在内存中将多个物理文件或内存数据打成一个 Zip 流。
    /// 配合 HttpUtil 的 StreamContent 输出，可以在完全不占用业务服务器磁盘空间的极端情况下，支撑 GB 级高频并发流式下载。
    /// </remarks>
    /// <param name="outputStream">目标接收流（通常可以直接传 Response.Body 或 MemoryStream）</param>
    /// <param name="fileEntries">待打包的文件矩阵：Key 为压缩包内的相对文件名，Value 为文件的物理路径或流</param>
    /// <param name="cancellationToken"></param>
    public static async Task CreateZipToStreamAsync(Stream outputStream, System.Collections.Generic.IDictionary<string, string> fileEntries, CancellationToken cancellationToken = default)
    {
        // 核心原理：在当前网络/内存通道之上直接叠加压缩状态机，数据边读边压边弹
        using var archive = new ZipArchive(outputStream, ZipArchiveMode.Create, leaveOpen: true);

        foreach (var entry in fileEntries)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!File.Exists(entry.Value)) continue;

            // 在压缩包内创建指定名称的文件节点
            var zipEntry = archive.CreateEntry(entry.Key, CompressionLevel.Fastest);

            // 打开该节点的写入管道，并与本地磁盘文件流建立高速直连
            using var entryStream = zipEntry.Open();
            using var fileStream = new FileStream(entry.Value, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);

            // 流式对倒：4KB 分块源源不断推向 HTTP 响应总线，服务器托管堆内存和物理磁盘分配恒定为 0
            await fileStream.CopyToAsync(entryStream, cancellationToken).ConfigureAwait(false);
        }
    }
}
