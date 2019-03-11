#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：JwellFace
// 文件名称：Log4NetExtensions.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-07 11:06
// 更改时间：2018-12-07 11:06
// ------------------------------------------------------------------------------
#endregion

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Amm.AspNetCore.Logger.Log4net
{
    /// <summary>
    /// Log4Net模块
    /// </summary>
    public static class Log4NetExtensions
    {
        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddLog4Net(this IServiceCollection services)
        {
            services.AddSingleton<ILogger, Log4NetLogger>();
            services.AddSingleton<ILoggerProvider, Log4NetLoggerProvider>();

            return services;
        }
    }
}