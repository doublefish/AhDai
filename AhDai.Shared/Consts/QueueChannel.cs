using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Shared.Consts;

/// <summary>
/// 队列频道
/// </summary>
public class QueueChannel
{
	/// <summary>
	/// 附件保存
	/// </summary>
	public const string BASE_ATTACHMENT_SAVE = "channel_base_attachment_save";
	/// <summary>
	/// 合同审批
	/// </summary>
	public const string CONT_CONTRACT_AUDITED = "channel_cont_contract_audited";
	/// <summary>
	/// 订单创建
	/// </summary>
	public const string ORD_ORDER_CREATE = "channel_ord_order_create";
}
