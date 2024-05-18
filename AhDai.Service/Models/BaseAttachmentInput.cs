namespace AhDai.Service;

/// <summary>
/// BaseAttachmentInput
/// </summary>
public abstract class BaseAttachmentInput : BaseInput, IAttachmentInput
{
	/// <summary>
	/// 附件Id
	/// </summary>
	public long[]? AttachmentIds { get; set; }

}
