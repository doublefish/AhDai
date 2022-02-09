using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Versioning;

namespace Adai.Core.Utils
{
	/// <summary>
	/// RARHelper
	/// </summary>
	[SupportedOSPlatform("windows")]
	public static class RARHelper
	{
		/// <summary>
		/// WinRAR.exe路径
		/// </summary>
		public static string ApplicationPath { get; private set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		static RARHelper()
		{
			try
			{
				using var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
				// 判断是否安装了WinRAR.exe
				if (key != null)
				{
					// 获取WinRAR.exe路径
					ApplicationPath = key.GetValue(string.Empty).ToString();
				}
			}
			catch
			{
				throw new Exception("WinRAR is not installed, please do this after confirming that WinRAR is installed.");
			}
		}

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="applicationPath"></param>
		public static void SetApplicationPath(string applicationPath)
		{
			ApplicationPath = applicationPath;
		}

		/// <summary>
		/// 利用WinRAR进行压缩
		/// </summary>
		/// <param name="sourcePath">要压缩的文件夹（绝对路径）</param>
		/// <param name="rarPath">压缩后的.rar文件的存放目录（绝对路径）</param>
		/// <param name="rarName">压缩文件的名称（包括后缀）</param>
		/// <param name="level">压缩级别（0-5）</param>
		public static void Compress(string sourcePath, string rarPath, string rarName, int level = 0)
		{
			if (!Directory.Exists(sourcePath))//判断输入目录是否存在
			{
				throw new ArgumentException("The source file directory does not exist.");
			}
			//cmd = " a -m0 " + rarName + " " + sourcePath + " *.* -r";
			var cmd = $"a -m{level} -ep1 \"{rarName}\" \"{sourcePath}\" -r";//执行rar的命令参数

			var processStartInfo = new ProcessStartInfo
			{
				FileName = ApplicationPath,//指定启动文件名
				Arguments = cmd,//指定启动该文件时的命令、参数
				WindowStyle = ProcessWindowStyle.Hidden,//指定启动窗口模式：隐藏
				WorkingDirectory = rarPath//指定压缩后到达路径
			};//创建启动进程的参数

			using var process = new Process
			{
				StartInfo = processStartInfo//指定进程对象启动信息对象
			};//创建进程对象
			process.Start();//启动进程
			process.WaitForExit();//指定进程自行退行为止
		}

		/// <summary>
		/// 利用WinRAR进行解压缩
		/// </summary>
		/// <param name="path">文件解压路径（绝对）</param>
		/// <param name="rarPath">要解压缩的.rar文件的存放目录（绝对路径）</param>
		/// <param name="rarName">要解压缩的.rar文件名（包括后缀）</param>
		public static void UnCompress(string path, string rarPath, string rarName)
		{
			if (!Directory.Exists(path))//如果压缩到目标路径不存在
			{
				Directory.CreateDirectory(path);//创建压缩到目标路径
			}
			//cmd = "x " + rarName + " " + path + " -y";
			var cmd = $"x \"{rarName}\" \"{path}\" -y";//执行rar的命令参数

			var processStartInfo = new ProcessStartInfo
			{
				FileName = ApplicationPath,//指定启动文件名
				Arguments = cmd,//指定启动该文件时的命令、参数
				WindowStyle = ProcessWindowStyle.Hidden,//指定启动窗口模式：隐藏
				WorkingDirectory = rarPath//指定压缩后到达路径
			};//启动进程的参数

			using var process = new Process
			{
				StartInfo = processStartInfo//指定进程对象启动信息对象
			};//进程对象
			process.Start();//启动进程
			process.WaitForExit();//指定进程自行退行为止
		}
	}
}
