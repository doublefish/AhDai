using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.IO;

namespace Adai.Standard
{
	/// <summary>
	/// Log4netHelper
	/// </summary>
	public static class Log4netHelper
	{
		static ILoggerRepository repository;

		/// <summary>
		/// Repository
		/// </summary>
		public static ILoggerRepository Repository
		{
			get
			{
				if (repository == null)
				{
					repository = LogManager.CreateRepository("Log4net");
					XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
				}
				return repository;
			}
		}

		static readonly ILog LogInfo = LogManager.GetLogger(Repository.Name, "LogInfo");
		static readonly ILog LogError = LogManager.GetLogger(Repository.Name, "LogError");
		static readonly ILog LogDebug = LogManager.GetLogger(Repository.Name, "LogDebug");

		/// <summary>
		/// InfoFormat
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Info(string message, Exception exception = null)
		{
			LogInfo.Info(message, exception);
		}

		/// <summary>
		/// InfoFormat
		/// </summary>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public static void InfoFormat(string format, params object[] args)
		{
			LogInfo.InfoFormat(format, args);
		}

		/// <summary>
		/// Error
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Error(string message, Exception exception = null)
		{
			LogError.Error(message, exception);
		}

		/// <summary>
		/// ErrorFormat
		/// </summary>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public static void ErrorFormat(string format, params object[] args)
		{
			LogError.ErrorFormat(format, args);
		}

		/// <summary>
		/// Error
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Debug(string message, Exception exception = null)
		{
			LogError.Debug(message, exception);
		}

		/// <summary>
		/// ErrorFormat
		/// </summary>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public static void DebugFormat(string format, params object[] args)
		{
			LogError.DebugFormat(format, args);
		}
	}
}
