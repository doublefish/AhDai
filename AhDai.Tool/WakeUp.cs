using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AhDai.Tool;

internal partial class WakeUp
{
	//通过正则表达式设定MAC地址筛选标准，关于正则表达式请自行百度
	const string macCheckRegexString = @"^([0-9a-fA-F]{2})(([/\s:-][0-9a-fA-F]{2}){5})$";
	[GeneratedRegex(macCheckRegexString)]
	private static partial Regex MyRegex();

	private static readonly Regex MacCheckRegex = MyRegex();

	/// <summary>
	/// WakeUpOnLan
	/// </summary>
	/// <param name="mac"></param>
	/// <returns></returns>
	public static bool WakeUpOnLan(string mac)
	{
		if (MacCheckRegex.IsMatch(mac, 0))
		{
			var macBytes = FormatMac(mac);
			WakeUpCore(macBytes);
			return true;
		}
		return false;
	}

	private static int WakeUpCore(byte[] mac)
	{
		var client = new UdpClient();
		client.Connect(System.Net.IPAddress.Broadcast, 40000);
		// 下方为发送内容的编制，6遍“FF”+17遍mac的byte类型字节。
		var packet = new byte[17 * 6];
		for (var i = 0; i < 6; i++)
			packet[i] = 0xFF;
		for (var i = 1; i <= 16; i++)
			for (int j = 0; j < 6; j++)
				packet[i * 6 + j] = mac[j];

		return client.Send(packet, packet.Length);
	}


	private static byte[] FormatMac(string mac)
	{
		var array = mac.Split('-');
		var bytes = new byte[6];
		for (var i = 0; i < 6; i++)
		{
			var byteValue = Convert.ToByte(array[i], 16);
			bytes[i] = byteValue;
		}
		return bytes;
	}

	
}
