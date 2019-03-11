#region 版权信息

// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：Amm.NetworkMark.Core
// 文件名称：VerifyCodeHandler.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-25 10:55
// 更改时间：2018-12-25 10:56
// ------------------------------------------------------------------------------

#endregion

#region 项目引用

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Amm.AspNetCore.Datas;
using Amm.AspNetCore.Datas.Entity;
using Amm.AspNetCore.Dependency;
using Amm.NetworkMark.Core.Extentions;
using Microsoft.Extensions.Caching.Distributed;

#endregion

namespace Amm.AspNetCore.Mvc
{
    /// <summary>
    ///     验证码处理类
    /// </summary>
    public static class VerifyCodeHandler
    {
        private const string Separator = "#$#";

        /// <summary>
        ///     校验验证码有效性
        /// </summary>
        /// <param name="code">要校验的验证码</param>
        /// <param name="id">验证码编号</param>
        /// <param name="removeIfSuccess">验证成功时是否移除</param>
        /// <returns></returns>
        public static bool CheckCode(string code, string id, bool removeIfSuccess = true)
        {
            if (string.IsNullOrEmpty(code)) return false;
            var key = $"{AmmConstants.VerifyCodeKeyPrefix}_{id}";
            IDistributedCache cache = ServiceLocator.Instance.GetService<IDistributedCache>();
            var flag = code.Equals(cache.GetString(key), StringComparison.OrdinalIgnoreCase);
            if (removeIfSuccess && flag) cache.Remove(key);
            return flag;
        }

        /// <summary>
        ///     设置验证码到Session中
        /// </summary>
        public static void SetCode(string code, out string id)
        {
            id = Guid.NewGuid().ToString("N");
            var key = $"{AmmConstants.VerifyCodeKeyPrefix}_{id}";
            IDistributedCache cache = ServiceLocator.Instance.GetService<IDistributedCache>();
            const int seconds = 60 * 3;
            cache.SetString(key, code,
                new DistributedCacheEntryOptions {AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(seconds)});
        }

        /// <summary>
        ///     将图片序列化成字符串
        /// </summary>
        public static string GetImageString(Image image, string id)
        {
            Check.NotNull(image, nameof(image));
            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png);
                var bytes = ms.ToArray();
                var str = $"data:image/png;base64,{bytes.ToBase64String()}{Separator}{id}";
                return str.ToBase64String();
            }
        }
    }
}