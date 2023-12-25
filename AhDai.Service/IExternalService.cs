using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Service;

public interface IExternalService
{
	/// <summary>
	/// 测试
	/// </summary>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	Task<string> TestGetAsync();
}
