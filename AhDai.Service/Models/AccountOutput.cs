using AhDai.Service.Sys.Models;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace AhDai.Service.Models;

/// <summary>
/// 账号
/// </summary>
public class AccountOutput : UserOutput
{
	/// <summary>
	/// 构造函数
	/// </summary>
	public AccountOutput() { }

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="user"></param>
	public AccountOutput(UserOutput user)
	{
		Id = user.Id;
		Username = user.Username;
		AvatarUrl = user.AvatarUrl;
		Nickname = user.Nickname;
		Name = user.Name;
		Birthday = user.Birthday;
		Gender = user.Gender;
		Email = user.Email;
		MobilePhone = user.MobilePhone;
		Telephone = user.Telephone;
		Status = user.Status;
		Orgs = user.Orgs;
		RoleIds = user.RoleIds;
		Roles = user.Roles;
		WeChatOpenId = user.WeChatOpenId;
		ErpUserId = user.ErpUserId;
		LzezUserId = user.LzezUserId;

		Version = user.Version;
		CreationTime = user.CreationTime;
		CreatorId = user.CreatorId;
		CreatorUsername = user.CreatorUsername;
		CreatorName = user.CreatorName;
		ModificationTime = user.ModificationTime;
		ModifierId = user.ModifierId;
		ModifierUsername = user.ModifierUsername;
		ModifierName = user.ModifierName;
		IsDeleted = user.IsDeleted;
		DeletionTime = user.DeletionTime;
		DeleterId = user.DeleterId;
		DeleterUsername = user.DeleterUsername;
		TenantId = user.TenantId;
		TenantName = user.TenantName;
		TenantType = user.TenantType;
	}

}
