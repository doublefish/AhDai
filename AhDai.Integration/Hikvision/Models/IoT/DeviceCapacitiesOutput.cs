using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 设备/通道能力集
/// </summary>
public class DeviceCapacitiesOutput
{
    /// <summary>
    /// 是否支持人脸，1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportFDLib")]
    public int? SupportFDLib { get; set; }
    /// <summary>
    /// 是否支持蓝牙，1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportBluetooth")]
    public int? SupportBluetooth { get; set; }
    /// <summary>
    /// 是否支持保存人脸图片，1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportSaveFacePic")]
    public int? SupportSaveFacePic { get; set; }
    /// <summary>
    /// 是否支持人员时间计划，1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportUserRightPlanTemplate")]
    public int? SupportUserRightPlanTemplate { get; set; }
    /// <summary>
    /// 是否支持终端显示配置，1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportIdentityTerminal")]
    public int? SupportIdentityTerminal { get; set; }
    /// <summary>
    /// 是否支持采集指纹，1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportCaptureFingerPrint")]
    public int? SupportCaptureFingerPrint { get; set; }
    /// <summary>
    /// 是否支持门时间计划，1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportDoorStatusPlanTemplate")]
    public int? SupportDoorStatusPlanTemplate { get; set; }
    /// <summary>
    /// 是否支持删除指纹，1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportFingerPrintDelete")]
    public int? SupportFingerPrintDelete { get; set; }
    /// <summary>
    /// 是否支持指纹，1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportFingerPrintCfg")]
    public int? SupportFingerPrintCfg { get; set; }
    /// <summary>
    /// 是否支持人员，1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportUserInfo")]
    public int? SupportUserInfo { get; set; }
    /// <summary>
    /// 是否支持对讲设置，1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportDeviceId")]
    public int? SupportDeviceId { get; set; }
    /// <summary>
    /// 是否支持门禁配置，1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportAcsCfg")]
    public int? SupportAcsCfg { get; set; }
    /// <summary>
    /// 是否支持采集卡片，1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportCaptureCardInfo")]
    public int? SupportCaptureCardInfo { get; set; }
    /// <summary>
    /// 是否支持远程控门，1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportRemoteControlDoor")]
    public int? SupportRemoteControlDoor { get; set; }
    /// <summary>
    /// 是否支持查询门状态，1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportAcsWorkStatus")]
    public int? SupportAcsWorkStatus { get; set; }
    /// <summary>
    /// 是否支持卡片，1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportCardInfo")]
    public int? SupportCardInfo { get; set; }
    /// <summary>
    /// 是否支持认证语音播报配置，1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportTTSText")]
    public int? SupportTTSText { get; set; }
    /// <summary>
    /// 是否支持认证文本配置，1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportCustomPrompt")]
    public int? SupportCustomPrompt { get; set; }
    /// <summary>
    /// 是否支持人员密码开门（组合方式中包含密码），1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportUserPassword")]
    public int? SupportUserPassword { get; set; }
    /// <summary>
    /// 是否支持人员纯密码开门，1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportPurePwdVerify")]
    public int? SupportPurePwdVerify { get; set; }
    /// <summary>
    /// 人员编号最大长度
    /// </summary>
    [JsonPropertyName("maxEmployeeNoLength")]
    public int? MaxEmployeeNoLength { get; set; }
    /// <summary>
    /// 支持最多门数量
    /// </summary>
    [JsonPropertyName("maxDoorNo")]
    public int? MaxDoorNo { get; set; }
    /// <summary>
    /// 人员最多关联的计划模板数
    /// </summary>
    [JsonPropertyName("maxUserPlanTemplate")]
    public int? MaxUserPlanTemplate { get; set; }
    /// <summary>
    /// 支持的最多读卡器数量
    /// </summary>
    [JsonPropertyName("maxCardReaderNo")]
    public int? MaxCardReaderNo { get; set; }
    /// <summary>
    /// 支持的读卡器编号枚举
    /// </summary>
    [JsonPropertyName("cardReaderNoOpt")]
    public int[]? CardReaderNoOpt { get; set; }
    /// <summary>
    /// 远程控门支持的枚举：open、close、alwaysOpen、alwaysClose
    /// </summary>
    [JsonPropertyName("doorCmdOpt")]
    public string[]? DoorCmdOpt { get; set; }
    /// <summary>
    /// 是否支持读卡器认证，1支持、0不支持
    /// </summary>
    [JsonPropertyName("supportVerifyWeekPlanCfg")]
    public int? SupportVerifyWeekPlanCfg { get; set; }
    /// <summary>
    /// 读卡器认证方式枚举
    /// </summary>
    [JsonPropertyName("verifyModeOpt")]
    public string[]? VerifyModeOpt { get; set; }
    /// <summary>
    /// 人员密码最大长度
    /// </summary>
    [JsonPropertyName("maxUserPasswordLength")]
    public int? MaxUserPasswordLength { get; set; }
    /// <summary>
    /// 人员密码最小长度
    /// </summary>
    [JsonPropertyName("minUserPasswordLength")]
    public int? MinUserPasswordLength { get; set; }


    #region 文档未列出的
    /// <summary>
    /// 是否支持设备版本
    /// </summary>
    [JsonPropertyName("supportDeviceVersion")]
    public int? SupportDeviceVersion { get; set; }
    /// <summary>
    /// 是否支持NVR时间叠加
    /// </summary>
    [JsonPropertyName("supportNvrOverlayDT")]
    public int? SupportNvrOverlayDT { get; set; }
    /// <summary>
    /// 快放最大倍速
    /// </summary>
    [JsonPropertyName("supportQuickplayMaxtime")]
    public int? SupportQuickplayMaxtime { get; set; }
    /// <summary>
    /// 是否支持自定义音频URL
    /// </summary>
    [JsonPropertyName("supportNvrCustomAudioURL")]
    public int? SupportNvrCustomAudioURL { get; set; }
    /// <summary>
    /// 是否支持录像查询
    /// </summary>
    [JsonPropertyName("supportRecordPage")]
    public int? SupportRecordPage { get; set; }
    /// <summary>
    /// 是否支持视频清晰度
    /// </summary>
    [JsonPropertyName("supportVideoQuality")]
    public int? SupportVideoQuality { get; set; }
    /// <summary>
    /// 来源
    /// </summary>
    [JsonPropertyName("source")]
    public string? Source { get; set; }
    /// <summary>
    /// 是否支持通道对讲
    /// </summary>
    [JsonPropertyName("supportChannelTalkback")]
    public int? SupportChannelTalkback { get; set; }
    /// <summary>
    /// 是否支持内网访问
    /// </summary>
    [JsonPropertyName("supportIntranet")]
    public int? SupportIntranet { get; set; }
    /// <summary>
    /// 是否支持预置点
    /// </summary>
    [JsonPropertyName("supportPtzPreset")]
    public int? SupportPtzPreset { get; set; }
    /// <summary>
    /// 型号系列ID
    /// </summary>
    [JsonPropertyName("matchSeriesId")]
    public int? MatchSeriesId { get; set; }
    /// <summary>
    /// 通道快捷入口
    /// </summary>
    [JsonPropertyName("channelShortcut")]
    public string? ChannelShortcut { get; set; }
    /// <summary>
    /// 是否支持加密
    /// </summary>
    [JsonPropertyName("supportEncrypt")]
    public int? SupportEncrypt { get; set; }
    /// <summary>
    /// 是否支持设备视频封面
    /// </summary>
    [JsonPropertyName("supportDeviceVideoCover")]
    public int? SupportDeviceVideoCover { get; set; }
    /// <summary>
    /// 是否支持ISAPI
    /// </summary>
    [JsonPropertyName("supportIsapi")]
    public int? SupportIsapi { get; set; }
    /// <summary>
    /// 云台能力元数据
    /// </summary>
    [JsonPropertyName("ptzMeta")]
    public string? PtzMeta { get; set; }
    /// <summary>
    /// 是否支持升级任务
    /// </summary>
    [JsonPropertyName("supportUpgradePackageTask")]
    public int? SupportUpgradePackageTask { get; set; }
    /// <summary>
    /// 是否支持云台左右控制
    /// </summary>
    [JsonPropertyName("supportPtzLeftRight")]
    public int? SupportPtzLeftRight { get; set; }
    /// <summary>
    /// 是否支持通道代理
    /// </summary>
    [JsonPropertyName("supportChannelProxy")]
    public int? SupportChannelProxy { get; set; }
    /// <summary>
    /// 系列名称
    /// </summary>
    [JsonPropertyName("seriesName")]
    public string? SeriesName { get; set; }
    /// <summary>
    /// 临时能力A
    /// </summary>
    [JsonPropertyName("temp59A")]
    public int? Temp59A { get; set; }
    /// <summary>
    /// 是否完成
    /// </summary>
    [JsonPropertyName("completed")]
    public bool? Completed { get; set; }
    /// <summary>
    /// 是否支持录像
    /// </summary>
    [JsonPropertyName("supportRecord")]
    public int? SupportRecord { get; set; }
    /// <summary>
    /// 是否支持报警消息检测
    /// </summary>
    [JsonPropertyName("supportAlarmMessageDetection")]
    public int? SupportAlarmMessageDetection { get; set; }
    /// <summary>
    /// 是否支持低功耗
    /// </summary>
    [JsonPropertyName("supportLowPower")]
    public int? SupportLowPower { get; set; }
    /// <summary>
    /// 是否支持人体检测
    /// </summary>
    [JsonPropertyName("supportHuman")]
    public int? SupportHuman { get; set; }
    /// <summary>
    /// Link Pro物理版本
    /// </summary>
    [JsonPropertyName("linkProPhysicalVersion")]
    public string? LinkProPhysicalVersion { get; set; }
    /// <summary>
    /// 是否支持下载
    /// </summary>
    [JsonPropertyName("supportDownload")]
    public int? SupportDownload { get; set; }
    /// <summary>
    /// 是否支持DSTP
    /// </summary>
    [JsonPropertyName("supportDSTP")]
    public int? SupportDSTP { get; set; }
    /// <summary>
    /// 是否支持抓图
    /// </summary>
    [JsonPropertyName("supportCapture")]
    public int? SupportCapture { get; set; }
    /// <summary>
    /// 是否支持对讲
    /// </summary>
    [JsonPropertyName("supportTalk")]
    public int? SupportTalk { get; set; }
    /// <summary>
    /// 是否支持本地存储
    /// </summary>
    [JsonPropertyName("supportLocalStorage")]
    public int? SupportLocalStorage { get; set; }
    /// <summary>
    /// 云备份能力
    /// </summary>
    [JsonPropertyName("cloudBackup")]
    public DeviceCapabilityStateOutput? CloudBackup { get; set; }
    /// <summary>
    /// 匹配型号
    /// </summary>
    [JsonPropertyName("matchModel")]
    public string? MatchModel { get; set; }
    /// <summary>
    /// 型号ID
    /// </summary>
    [JsonPropertyName("matchModelId")]
    public int? MatchModelId { get; set; }
    /// <summary>
    /// 是否支持阴影能力
    /// </summary>
    [JsonPropertyName("supportShadow")]
    public int? SupportShadow { get; set; }
    /// <summary>
    /// Link物理版本
    /// </summary>
    [JsonPropertyName("linkPhysicalVersion")]
    public string? LinkPhysicalVersion { get; set; }
    /// <summary>
    /// 默认视频清晰度
    /// </summary>
    [JsonPropertyName("defaultVideoLevel")]
    public int? DefaultVideoLevel { get; set; }
    /// <summary>
    /// 通道设置面板
    /// </summary>
    [JsonPropertyName("channelSettingPanel")]
    public string? ChannelSettingPanel { get; set; }
    /// <summary>
    /// 是否支持录像分类查询
    /// </summary>
    [JsonPropertyName("supportVideosClassifiedQuery")]
    public int? SupportVideosClassifiedQuery { get; set; }
    /// <summary>
    /// 是否支持方向控制
    /// </summary>
    [JsonPropertyName("supportPtzDirection")]
    public int? SupportPtzDirection { get; set; }
    /// <summary>
    /// 是否支持报警图片
    /// </summary>
    [JsonPropertyName("supportAlarmPic")]
    public int? SupportAlarmPic { get; set; }
    /// <summary>
    /// 是否支持Link
    /// </summary>
    [JsonPropertyName("supportLink")]
    public int? SupportLink { get; set; }
    /// <summary>
    /// 子错误信息
    /// </summary>
    [JsonPropertyName("subErrors")]
    public string[]? SubErrors { get; set; }
    /// <summary>
    /// 是否支持云台
    /// </summary>
    [JsonPropertyName("supportPtz")]
    public int? SupportPtz { get; set; }
    /// <summary>
    /// 过期时间（秒）
    /// </summary>
    [JsonPropertyName("expired")]
    public int? Expired { get; set; }
    /// <summary>
    /// 动态属性服务
    /// </summary>
    [JsonPropertyName("dynamicAttrService")]
    public string? DynamicAttrService { get; set; }
    /// <summary>
    /// 是否支持车辆检测
    /// </summary>
    [JsonPropertyName("supportCarDetect")]
    public int? SupportCarDetect { get; set; }
    /// <summary>
    /// 是否支持录像回放
    /// </summary>
    [JsonPropertyName("supportVideoBack")]
    public int? SupportVideoBack { get; set; }
    /// <summary>
    /// 是否支持升级
    /// </summary>
    [JsonPropertyName("supportUpgrade")]
    public int? SupportUpgrade { get; set; }
    /// <summary>
    /// 智能提醒能力
    /// </summary>
    [JsonPropertyName("smartMessage")]
    public DeviceCapabilityStateOutput? SmartMessage { get; set; }
    /// <summary>
    /// 快放倍速选项
    /// </summary>
    [JsonPropertyName("supportPlayQuickOpt")]
    public string? SupportPlayQuickOpt { get; set; }
    /// <summary>
    /// 功能模型ID列表
    /// </summary>
    [JsonPropertyName("featureModelIds")]
    public int[]? FeatureModelIds { get; set; }
    /// <summary>
    /// 是否支持缓冲播放
    /// </summary>
    [JsonPropertyName("supportVideoBuffer")]
    public int? SupportVideoBuffer { get; set; }
    /// <summary>
    /// 是否支持布防计划
    /// </summary>
    [JsonPropertyName("supportDefenceplan")]
    public int? SupportDefenceplan { get; set; }
    /// <summary>
    /// 是否支持码流V2
    /// </summary>
    [JsonPropertyName("supportStreamCodeV2")]
    public int? SupportStreamCodeV2 { get; set; }
    /// <summary>
    /// 是否支持画面比例
    /// </summary>
    [JsonPropertyName("supportVideoPicRatio")]
    public int? SupportVideoPicRatio { get; set; }
    /// <summary>
    /// 慢放倍速选项
    /// </summary>
    [JsonPropertyName("supportPlaySlowOpt")]
    public string? SupportPlaySlowOpt { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("a")]
    public string? A { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("b")]
    public string? B { get; set; }
    /// <summary>
    /// 是否支持录像计划
    /// </summary>
    [JsonPropertyName("supportRecordPlan")]
    public int? SupportRecordPlan { get; set; }
    /// <summary>
    /// 视频清晰度选项
    /// </summary>
    [JsonPropertyName("supportVideoQualityOpt")]
    public DeviceVideoQualityOptionOutput[]? SupportVideoQualityOpt { get; set; }
    /// <summary>
    /// 是否支持循环录像
    /// </summary>
    [JsonPropertyName("supportRecordLoop")]
    public int? SupportRecordLoop { get; set; }
    /// <summary>
    /// 是否支持车辆识别
    /// </summary>
    [JsonPropertyName("supportCar")]
    public int? SupportCar { get; set; }
    /// <summary>
    /// 是否支持名称叠加
    /// </summary>
    [JsonPropertyName("supportNvrOverlayName")]
    public int? SupportNvrOverlayName { get; set; }
    /// <summary>
    /// 是否支持人体识别
    /// </summary>
    [JsonPropertyName("supportHumanDetect")]
    public int? SupportHumanDetect { get; set; }
    /// <summary>
    /// 是否支持缩放
    /// </summary>
    [JsonPropertyName("supportPtzZoom")]
    public int? SupportPtzZoom { get; set; }
    /// <summary>
    /// 是否支持硬盘管理
    /// </summary>
    [JsonPropertyName("supportHdd")]
    public int? SupportHdd { get; set; }
    /// <summary>
    /// 是否支持日志上传
    /// </summary>
    [JsonPropertyName("supportRemoteLogUpload")]
    public int? SupportRemoteLogUpload { get; set; }
    /// <summary>
    /// 是否支持布防
    /// </summary>
    [JsonPropertyName("supportDefence")]
    public int? SupportDefence { get; set; }
    /// <summary>
    /// 是否支持回放倍速
    /// </summary>
    [JsonPropertyName("supportReplaySpeed")]
    public int? SupportReplaySpeed { get; set; }
    /// <summary>
    /// 是否支持云备份
    /// </summary>
    [JsonPropertyName("supportCloudBackup")]
    public int? SupportCloudBackup { get; set; }
    /// <summary>
    /// 设备设置面板
    /// </summary>
    [JsonPropertyName("deviceSettingPanel")]
    public string? DeviceSettingPanel { get; set; }
    /// <summary>
    /// 是否支持视频回放检测
    /// </summary>
    [JsonPropertyName("supportVideoBackDetection")]
    public int? SupportVideoBackDetection { get; set; }
    /// <summary>
    /// 是否支持NVR物联
    /// </summary>
    [JsonPropertyName("supportNvrIot")]
    public int? SupportNvrIot { get; set; }
    /// <summary>
    /// 是否支持智能提醒
    /// </summary>
    [JsonPropertyName("supportSmartMessage")]
    public int? SupportSmartMessage { get; set; }
    /// <summary>
    /// 是否支持更多物联能力
    /// </summary>
    [JsonPropertyName("supportNvrIotMore")]
    public int? SupportNvrIotMore { get; set; }
    /// <summary>
    /// 是否支持上下控制
    /// </summary>
    [JsonPropertyName("supportPtzTopBottom")]
    public int? SupportPtzTopBottom { get; set; }
    /// <summary>
    /// 是否支持物理型号
    /// </summary>
    [JsonPropertyName("supportPhysicalModel")]
    public int? SupportPhysicalModel { get; set; }
    /// <summary>
    /// 是否支持人车检测
    /// </summary>
    [JsonPropertyName("supportSmartHumanCar")]
    public int? SupportSmartHumanCar { get; set; }
    #endregion

}
