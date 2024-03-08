using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;

namespace AhDai.Tool;

internal class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine("Hello, World!");

		try
		{
			WakeUp.WakeUpOnLan("00-CF-E0-45-8B-F3");
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}

	}


}
