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

		static readonly ILog logInfo = LogManager.GetLogger(Repository.Name, "LogInfo");
		static readonly ILog logError = LogManager.GetLogger(Repository.Name, "LogError");

		/// <summary>
		/// InfoFormat
		/// </summary>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public static void InfoFormat(string format, params object[] args)
		{
			logInfo.InfoFormat(format, args);
		}

		/// <summary>
		/// ErrorFormat
		/// </summary>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public static void ErrorFormat(string format, params object[] args)
		{
			logError.ErrorFormat(format, args);
		}

		/// <summary>
		/// InfoFormat
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Info(string message, Exception exception = null)
		{
			logInfo.Info(message, exception);
		}

		/// <summary>
		/// Error
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Error(string message, Exception exception = null)
		{
			logError.Error(message, exception);
		}
	}
}
