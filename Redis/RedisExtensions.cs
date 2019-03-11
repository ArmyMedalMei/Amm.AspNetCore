#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：JwellFace
// 文件名称：RedisExtensions.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-05 10:35
// 更改时间：2018-12-05 10:35
// ------------------------------------------------------------------------------
#endregion

using System;
using Microsoft.Extensions.DependencyInjection;

namespace Amm.AspNetCore.Redis
{
    /// <summary>
    ///   redis缓存模块
    /// </summary>
    public static class RedisExtensions
    {
        /// <summary>
        ///  使用redis缓存
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddRedis(this IServiceCollection services, Action<RedisOptions> options)
        {
            //注入redis服务
            services.Configure(options);
            services.AddSingleton<IRedisCacheContext, RedisCacheContext>();
            services.AddSingleton(typeof(ICacheService), typeof(RedisCacheService));

            return services;
        }
    }
}