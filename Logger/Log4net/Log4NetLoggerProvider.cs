#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：JwellFace
// 文件名称：Log4Net.cs
// 版本号: V1.0.0.0
// 创建时间：2018-11-27 10:08
// 更改时间：2018-11-27 10:08
// ------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using Microsoft.Extensions.Logging;

namespace Amm.AspNetCore.Logger.Log4net
{
    /// <summary>
    /// log4net 日志对象提供者
    /// </summary>
    public class Log4NetLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, Log4NetLogger> _loggers = new ConcurrentDictionary<string, Log4NetLogger>();
        private const string DefaultLog4NetFileName = "log4net.config";
        private readonly ILoggerRepository _loggerRepository;

        /// <summary>
        /// 初始化一个<see cref="Log4NetLoggerProvider"/>类型的新实例
        /// </summary>
        public Log4NetLoggerProvider() : this(DefaultLog4NetFileName)
        { }

        /// <summary>
        /// 初始化一个<see cref="Log4NetLoggerProvider"/>类型的新实例
        /// </summary>
        public Log4NetLoggerProvider(string log4NetConfigFile)
        {
            var file = log4NetConfigFile ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DefaultLog4NetFileName);
            var assembly = Assembly.GetEntryAssembly() ?? GetCallingAssemblyFromStartup();
            _loggerRepository = LogManager.CreateRepository(assembly, typeof(Hierarchy));

            if (File.Exists(file))
            {
                XmlConfigurator.ConfigureAndWatch(_loggerRepository, new FileInfo(file));
                return;
            }
            var appender = new RollingFileAppender
            {
                Name = "root",
                File = "log\\log_",
                AppendToFile = true,
                LockingModel = new FileAppender.MinimalLock(),
                RollingStyle = RollingFileAppender.RollingMode.Date,
                DatePattern = "yyyyMMdd-HH\".log\"",
                StaticLogFileName = false,
                MaxSizeRollBackups = 10,
                Layout = new PatternLayout("[%d{HH:mm:ss.fff}] %-5p %c T%t %n%m%n")
            };
            appender.ClearFilters();
            appender.AddFilter(new LevelMatchFilter { LevelToMatch = Level.Debug });
            BasicConfigurator.Configure(_loggerRepository, appender);
            appender.ActivateOptions();
        }

        /// <summary>
        /// 创建一个 <see cref="T:Microsoft.Extensions.Logging.ILogger" /> 的新实例
        /// </summary>
        /// <param name="categoryName">记录器生成的消息的类别名称。</param>
        /// <returns>日志实例</returns>
        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, key => new Log4NetLogger(_loggerRepository.Name, key));
        }

        /// <summary>
        ///   获取获程序集通过启动项
        /// </summary>
        /// <returns></returns>
        private static Assembly GetCallingAssemblyFromStartup()
        {
            var stackTrace = new System.Diagnostics.StackTrace(2);
            for (var i = 0; i < stackTrace.FrameCount; i++)
            {
                var frame = stackTrace.GetFrame(i);
                var type = frame.GetMethod()?.DeclaringType;

                if (string.Equals(type?.Name, "Startup", StringComparison.OrdinalIgnoreCase))
                {
                    return type?.Assembly;
                }
            }
            return null;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            _loggers.Clear();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}