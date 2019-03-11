#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：JwellFace
// 文件名称：IRedisCacheContext.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-05 16:01
// 更改时间：2018-12-05 16:01
// ------------------------------------------------------------------------------
#endregion

using StackExchange.Redis;

namespace Amm.AspNetCore.Redis
{
    /// <summary>
    ///  IRedisCacheContext
    /// </summary>
    public interface IRedisCacheContext
    {
        /// <summary>
        /// RedisDatabase
        /// </summary>
        IDatabase RedisDatabase { get; set; }

        /// <summary>
        /// RedisMultiplexer
        /// </summary>
        IConnectionMultiplexer RedisMultiplexer { get; set; }
    }
}