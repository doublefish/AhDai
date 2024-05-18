namespace AhDai.Service;

/// <summary>
/// 带附件的入参
/// </summary>
public interface IAttachmentInput
{
	/// <summary>
	/// 附件Id
	/// </summary>
	public long[]? AttachmentIds { get; set; }
}
