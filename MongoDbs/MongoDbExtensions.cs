#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：JwellFace
// 文件名称：MongoDbExtensions.cs
// 版本号: V1.0.0.0
// 创建时间：2018-11-29 11:16
// 更改时间：2018-11-29 11:16
// ------------------------------------------------------------------------------
#endregion

using System;
using Microsoft.Extensions.DependencyInjection;

namespace Amm.AspNetCore.MongoDbs
{
    /// <summary>
    ///   芒果数据库模块
    /// </summary>
    public static class MongoDbExtensions
    {
        /// <summary>
        ///   使用芒果数据库
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddMongoDb(this IServiceCollection services, Action<MongoDbOptions> options)
        {
            //注入配置文件
            services.Configure(options);
            services.AddSingleton(typeof(IMongoDbContext<>), typeof(MongoDbContext<>));
            services.AddSingleton(typeof(INoSqlRepository<>), typeof(MongoRepository<>));

            return services;
        }
    }
}