using AhDai.Integration.Abstractions;
using AhDai.Integration.ESign.Configs;
using AhDai.Integration.ESign.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AhDai.Integration.ESign;

/// <summary>
/// IESignService
/// </summary>
public interface IESignService : IBaseService<ESignConfig>
{
    #region 基础
    /// <summary>
    /// 根据机构名称查询机构认证信息
    /// </summary>
    /// <param name="orgName"></param>
    /// <returns></returns>
    Task<OrgOutput> GetOrgByNameAsync(string orgName);

    /// <summary>
    /// 根据个人账号标识名称查询个人认证信息
    /// </summary>
    /// <param name="psnAccount">手机号或邮箱</param>
    /// <returns></returns>
    Task<PersonOutput> GetPersonByAccountAsync(string psnAccount);
    #endregion

    #region 基础
    /// <summary>
    /// 获取机构授权链接
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<OrgAuthUrlOutput> GetOrgAuthUrlAsync(object input);
    #endregion

    #region 文件
    /// <summary>
    /// 获取文件上传地址
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<FileUploadUrlOutput> GetFileUploadUrlAsync(FileUploadUrlInput input);

    /// <summary>
    /// 上传本地文件到指定地址
    /// </summary>
    /// <param name="url"></param>
    /// <param name="stream"></param>
    /// <param name="contentMD5"></param>
    /// <param name="contentType"></param>
    /// <returns></returns>
    Task UploadFileToUrlAsync(string url, Stream stream, byte[]? contentMD5 = null, string? contentType = null);

    /// <summary>
    /// 上传本地文件
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<string> UploadFileAsync(FileUploadInput input);

    /// <summary>
    /// 查询文件上传状态
    /// </summary>
    /// <param name="fileId"></param>
    /// <param name="pageSize">是否返回文件首页的长宽值，默认值 false</param>
    /// <returns></returns>
    Task<FileInfoOutput> GetFileAsync(string fileId, bool pageSize = false);

    /// <summary>
    /// 查询文件上传状态：等待转换换成
    /// </summary>
    /// <param name="fileId"></param>
    /// <param name="maxRetryCount"></param>
    /// <returns></returns>
    Task<FileInfoOutput> GetFileUntilReadyAsync(string fileId, int maxRetryCount = 6);

    /// <summary>
    /// 检索文件关键字坐标
    /// </summary>
    /// <param name="fileId"></param>
    /// <param name="keywords"></param>
    /// <returns></returns>
    Task<GetFileKeywordPositionOutput[]> GetFileKeywordPositionsAsync(string fileId, params string[] keywords);
    #endregion

    #region 模板
    /// <summary>
    /// 获取制作合同模板页面
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<DocTemplateCreateUrlOutput> GetDocTemplateCreateUrlAsync(DocTemplateCreateUrlInput input);

    /// <summary>
    /// 获取编辑合同模板页面
    /// </summary>
    /// <param name="docTemplateId"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<DocTemplateEditUrlOutput> GetDocTemplateEditUrlAsync(string docTemplateId, DocTemplateEditUrlInput input);

    /// <summary>
    /// 查询合同模板中控件详情
    /// </summary>
    /// <param name="docTemplateId"></param>
    /// <returns></returns>
    Task<IDictionary<string, object>> GetDocTemplateAsync(string docTemplateId);

    /// <summary>
    /// 填写模板生成文件
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<FileCreateByDocTemplateOutput> CreateFileByDocTemplateAsync(FileCreateByDocTemplateInput input);
    #endregion

    #region 流程
    /// <summary>
    /// 基于文件发起签署
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<string> CreateSignFlowByFileAsync(SignFlowCreateByFileInput input);

    /// <summary>
    /// 撤销签署流程
    /// </summary>
    /// <param name="signFlowId"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task RevokeSignFlowAsync(string signFlowId, SignFlowRevokeInput input);

    /// <summary>
    /// 催签流程中签署人
    /// </summary>
    /// <param name="signFlowId"></param>
    /// <returns></returns>
    Task UrgeSignFlowAsync(string signFlowId) => UrgeSignFlowAsync(signFlowId, new SignFlowUrgeInput() { NoticeTypes = "1,2" });

    /// <summary>
    /// 催签流程中签署人
    /// </summary>
    /// <param name="signFlowId"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UrgeSignFlowAsync(string signFlowId, SignFlowUrgeInput input);

    /// <summary>
    /// 发起合同解约
    /// </summary>
    /// <param name="signFlowId"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<SignFlowRescissionOutput> InitiateSignFlowRescissionAsync(string signFlowId, SignFlowRescissionInput input);

    /// <summary>
    /// 查询签署流程详情
    /// </summary>
    /// <param name="signFlowId"></param>
    /// <returns></returns>
    Task<SignFlowDetailOutput> GetSignFlowDetailAsync(string signFlowId);

    /// <summary>
    /// 获取签署页面链接
    /// </summary>
    /// <param name="signFlowId"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<SignFlowSignUrlOutput> GetSignFlowSignUrlAsync(string signFlowId, SignFlowSignUrlInput input);

    /// <summary>
    /// 获取已签署文件下载链接
    /// </summary>
    /// <param name="signFlowId"></param>
    /// <returns></returns>
    Task<SignFileDownloadOutput> GetSignFlowFileDownloadUrlAsync(string signFlowId);

    /// <summary>
    /// 获取通知结果
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    Task<SignFlowNotifyOutput> GetSignFlowNotifyResultAsync(HttpContext httpContext);
    #endregion
}
