using AhDai.Integration.Aliyun.Configs;
using AhDai.Integration.Aliyun.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AhDai.Integration.Aliyun;

/// <summary>
/// IAliyunOssService
/// </summary>
public interface IAliyunOssService : IBaseService<AliyunOssConfig>
{
    /// <summary>
    /// 生成PolicyToken
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<OssPolicyTokenOutput> GeneratePolicyTokenAsync(OssPolicyTokenInput input);

    /// <summary>
    /// 上传回调
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    Task<OssUploadCallbackOutput> UploadCallbackAsync(HttpContext httpContext);

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="region"></param>
    /// <param name="bucket"></param>
    /// <param name="dir"></param>
    /// <param name="name"></param>
    /// <param name="filePath"></param>
    /// <param name="enableMD5"></param>
    /// <returns></returns>
    Task PutObjectAsync(string region, string bucket, string dir, string name, string filePath, bool enableMD5 = false);

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="region"></param>
    /// <param name="bucket"></param>
    /// <param name="dir"></param>
    /// <param name="name"></param>
    /// <param name="stream"></param>
    /// <param name="enableMD5"></param>
    /// <returns></returns>
    Task PutObjectAsync(string region, string bucket, string dir, string name, Stream stream, bool enableMD5 = false);

    /// <summary>
    /// 获取文件链接
    /// </summary>
    /// <param name="region"></param>
    /// <param name="bucket"></param>
    /// <param name="dir"></param>
    /// <param name="name"></param>
    /// <param name="expiration"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task<string> GetObjectUrlAsync(string region, string bucket, string dir, string name, long expiration, IDictionary<string, string?>? parameters = null);

    /// <summary>
    /// 修改文件的访问权限
    /// </summary>
    /// <param name="region"></param>
    /// <param name="bucket"></param>
    /// <param name="dir"></param>
    /// <param name="name"></param>
    /// <param name="acl"></param>
    /// <returns></returns>
    Task PutObjectAclAsync(string region, string bucket, string dir, string name, string acl);
}
