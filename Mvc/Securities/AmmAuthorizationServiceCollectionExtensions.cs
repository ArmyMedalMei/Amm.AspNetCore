#region 版权信息

// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：Amm.AspNetCore
// 文件名称：AmmAuthorizationServiceCollectionExtensions.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-28 13:46
// 更改时间：2018-12-28 16:11
// ------------------------------------------------------------------------------

#endregion

#region 项目引用

using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Amm.AspNetCore.Mvc.Securities
{
    /// <summary>
    ///     AmmAuthorizationExtensions
    /// </summary>
    public static class AmmAuthorizationServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds authorization services to the specified &lt;see
        ///     cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /&gt;.
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddAmmAuthorization(this IServiceCollection services)
        {
            return AddAmmAuthorization(services, op => { op.IsEnableAuthrization = true; });
        }

        /// <summary>
        ///     Adds authorization services to the specified &lt;see
        ///     cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /&gt;.
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddAmmAuthorization(this IServiceCollection services,
            Action<AmmAuthorizationOptions> action)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            //添加配置
            services.Configure(action);
            services.AddSingleton<IAuthorizationHandler, AmmAuthorizationHandler>();
            services.AddSingleton<IAmmSession, AmmSession>();
            services.AddTransient<IAuthorizationPolicyProvider, AmmAuthorizationPolicyProvider>();

            return services;
        }
    }
}