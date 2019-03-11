#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：Amm.NetworkMark.Core
// 文件名称：AspNetCoreExtension.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-25 11:09
// 更改时间：2018-12-25 11:09
// ------------------------------------------------------------------------------
#endregion

using Amm.AspNetCore.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace Amm.AspNetCore.Mvc.Https
{
    /// <summary>
    /// AspNetCore模块
    /// </summary>
    public static  class AspNetCoreModuleExtension
    {
        /// <summary>
        ///   将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAspNetCore(this IServiceCollection services)
        {
            services.AddSingleton<IScopedServiceResolver, RequestScopedServiceResolver>();

            return services;
        }
    }
}