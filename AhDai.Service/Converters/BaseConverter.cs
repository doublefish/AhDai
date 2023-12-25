using AhDai.Db.Models;
using AhDai.Service.Models;

namespace AhDai.Service.Converters;

internal static class BaseConverter
{
	/// <summary>
	/// CopyBase
	/// </summary>
	/// <param name="v"></param>
	/// <param name="m"></param>
	/// <returns></returns>
	public static void CopyBase<V, M>(this V v, M m)
		where V : BaseOutput
		where M : BaseModel
	{
		v.Id = m.Id;
		v.RowVersion = m.RowVersion;
		v.RowCreateUser = m.RowCreateUser;
		v.RowCreateUsername = m.RowCreateUsername;
		v.RowCreateTime = m.RowCreateTime;
		v.RowUpdateUser = m.RowUpdateUser;
		v.RowUpdateUsername = m.RowUpdateUsername;
		v.RowUpdateTime = m.RowUpdateTime;
		v.RowDeleted = m.RowDeleted;
	}
}
