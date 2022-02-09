using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Runtime.Versioning;

namespace Adai.Core.Utils
{
	/// <summary>
	/// ComputerHelper
	/// </summary>
	public static class ComputerHelper
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

		/// <summary>
		/// 获取Mac地址
		/// </summary>
		/// <returns></returns>
		[SupportedOSPlatform("windows")]
		public static ICollection<string> GetMacAddressesByWin32()
		{
			var addresses = new List<string>();
			using var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
			var moc = mc.GetInstances();
			foreach (var mo in moc)
			{
				if ((bool)mo["IPEnabled"])
				{
					var address = mo["MacAddress"].ToString();
					addresses.Add(address);
				}
				mo.Dispose();
			}
			return addresses;
		}
	}
}
