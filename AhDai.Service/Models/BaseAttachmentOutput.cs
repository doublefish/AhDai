using AhDai.Service.Sys.Models;

namespace AhDai.Service;

/// <summary>
/// BaseAttachmentOutput
/// </summary>
public abstract class BaseAttachmentOutput : BaseOutput, IAttachmentOutput
{
	/// <summary>
	/// 附件Id
	/// </summary>
	public long[] AttachmentIds { get; set; } = default!;
    /// <summary>
    /// 附件
    /// </summary>
    public FileOutput[]? Attachments { get; set; }

}
