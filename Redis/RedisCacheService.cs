#region 版权信息

// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：JwellFace
// 文件名称：RedisCacheService.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-05 10:45
// 更改时间：2018-12-05 10:45
// ------------------------------------------------------------------------------

#endregion

using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Amm.AspNetCore.Redis
{
    /// <summary>
    ///     缓存服务
    /// </summary>
    public class RedisCacheService : ICacheService
    {
        private readonly IRedisCacheContext _redisCacheContext;

        /// <summary>
        /// RedisCacheService
        /// </summary>
        public RedisCacheService(IRedisCacheContext redisCacheContext)
        {
            _redisCacheContext = redisCacheContext;
        }

        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationTime">绝对过期时间(毫秒)</param>
        public async Task AddCacheAsync<T>(string key, T value, int? expirationTime = null)
        {
            var redisValue = JsonConvert.SerializeObject(value);
            if (expirationTime.HasValue)
            {
                //获取过期时间戳
                var expirationTimeSpan = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
                await _redisCacheContext.RedisDatabase.StringSetAsync(key, redisValue, expirationTimeSpan);
            }
            else
                await _redisCacheContext.RedisDatabase.StringSetAsync(key, redisValue);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> GetCacheWithTAsync<T>(string key)
        {
            try
            {
                var valueJson = await _redisCacheContext.RedisDatabase.StringGetAsync(key);

                return JsonConvert.DeserializeObject<T>(valueJson);
            }
            catch (Exception)
            {
                return await Task.FromResult(default(T));
            }
        }

        /// <summary>
        ///  获取缓存
        /// </summary>
        /// <param name="key">redis键值</param>
        /// <returns></returns>
        public async Task<string> GetCacheAsync(string key)
        {
            return await _redisCacheContext.RedisDatabase.StringGetAsync(key);
        }

        /// <summary>
        ///  移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task RemoveCacheAsync(string key)
        {
            await _redisCacheContext.RedisDatabase.KeyDeleteAsync(key);
        }

        /// <summary>
        ///   发布消息
        /// </summary>
        /// <param name="channelName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<long> PubLishMessageAsync(string channelName, string message)
        {
            var sub = _redisCacheContext.RedisMultiplexer.GetSubscriber();

            return await sub.PublishAsync(channelName, message);
        }
    }
}