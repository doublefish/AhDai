using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace AhDai.Core.Utils;

/// <summary>
/// ComputerUtil
/// </summary>
public static class ComputerUtil
{
	/// <summary>
	/// 获取Mac地址
	/// </summary>
	/// <returns></returns>
	public static string GetMacAddress()
	{
		return GetMacAddresses().FirstOrDefault();
	}

	/// <summary>
	/// 获取Mac地址
	/// </summary>
	/// <returns></returns>
	public static ICollection<string> GetMacAddresses()
	{
		var addresses = new List<string>();
		var networks = NetworkInterface.GetAllNetworkInterfaces();
		foreach (var network in networks)
		{
			if (network.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
			{
				var physicalAddress = network.GetPhysicalAddress();
				var address = string.Join(":", physicalAddress.GetAddressBytes().Select(b => b.ToString("X2")));
				addresses.Add(address);
			}
		}
		return addresses;
	}
}
