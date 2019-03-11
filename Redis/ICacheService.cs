#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：JwellFace
// 文件名称：ICacheService.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-05 10:43
// 更改时间：2018-12-05 10:43
// ------------------------------------------------------------------------------
#endregion

using System.Threading.Tasks;

namespace Amm.AspNetCore.Redis
{
    /// <summary>
    /// ICacheService
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expirationTime">绝对过期时间(毫秒)</param>
        Task AddCacheAsync<T>(string key, T value, int? expirationTime = null);

        /// <summary>
        /// GetCacheWithTAsync
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetCacheWithTAsync<T>(string key);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <returns></returns>
        Task<string> GetCacheAsync(string key);

        /// <summary>
        ///   移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RemoveCacheAsync(string key);

        /// <summary>
        ///   发布消息
        /// </summary>
        /// <param name="channelName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<long> PubLishMessageAsync(string channelName, string message);
    }
}