﻿using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.IO;

namespace Adai.Standard
{
	/// <summary>
	/// LogHelper
	/// </summary>
	public static class LogHelper
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
		/// <param name="eventId">事件Id</param>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Info(string eventId, string message, Exception exception = null)
		{
			LogInfo.Info($"[{eventId}]{message}", exception);
		}

		/// <summary>
		/// Error
		/// </summary>
		/// <param name="eventId">事件Id</param>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Error(string eventId, string message, Exception exception = null)
		{
			LogError.Error($"[{eventId}]{message}", exception);
		}

		/// <summary>
		/// Error
		/// </summary>
		/// <param name="eventId">事件Id</param>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Debug(string eventId, string message, Exception exception = null)
		{
			LogDebug.Debug($"[{eventId}]{message}", exception);
		}
	}
}
