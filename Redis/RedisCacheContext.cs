#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：JwellFace
// 文件名称：RedisCache.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-05 15:36
// 更改时间：2018-12-05 15:36
// ------------------------------------------------------------------------------
#endregion

using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Amm.AspNetCore.Redis
{
    /// <summary>
    ///   redis数据库上下文
    /// </summary>
    public class RedisCacheContext: IRedisCacheContext
    {
        /// <summary>
        /// RedisCache
        /// </summary>
        public RedisCacheContext(IOptions<RedisOptions> options)
        {
            RedisMultiplexer = ConnectionMultiplexer.Connect(options.Value.ConnectionString);
            RedisDatabase = RedisMultiplexer.GetDatabase(options.Value.DataBaseIndex);
        }

        /// <summary>
        /// RedisDatabase
        /// </summary>
        public IDatabase RedisDatabase { get; set; }

        /// <summary>
        ///  RedisMultiplexer
        /// </summary>
        public IConnectionMultiplexer RedisMultiplexer { get; set; }
    }
}