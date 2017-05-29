﻿using EnvDTE;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.IO;

namespace CrmDeveloperExtensions.Core.Logging
{
    public class ExtensionLogger
    {
        public ExtensionLogger(DTE dte)
        {
            CreateConfig(dte);
        }

        private static void CreateConfig(DTE dte)
        {
            var config = new LoggingConfiguration();
            var fileTarget = new FileTarget
            {
                CreateDirs = true,
                FileName = GetLogFilePath(dte)
            };
            config.AddTarget("file", fileTarget);

            config.AddRule(LogLevel.Info, LogLevel.Fatal, fileTarget);

            LogManager.Configuration = config;
        }

        public static void LogToFile(DTE dte, Logger logger, string message, LogLevel logLevel)
        {
            if (UserOptionsGrid.GetLoggingOptionBoolean(dte, "ExtensionLoggingEnabled"))
                logger.Log(logLevel, message);
        }

        private static string GetLogFilePath(DTE dte)
        {
            string logFilePath = UserOptionsGrid.GetLoggingOptionString(dte, "ExtensionLogFilePath");

            return Path.Combine(logFilePath, "CrmDevExLog_" + DateTime.Now.ToString("MMddyyyy") + ".log");
        }
    }
}
