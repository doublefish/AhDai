using AhDai.Service.Sys.Models;

namespace AhDai.Service;

/// <summary>
/// 带附件的出参
/// </summary>
public interface IAttachmentOutput
{
	/// <summary>
	/// 附件Id
	/// </summary>
	public long[] AttachmentIds { get; set; }
	/// <summary>
	/// 附件
	/// </summary>
	public FileOutput[]? Attachments { get; set; }
}
