#region 版权信息

// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：Amm.NetworkMark.Core
// 文件名称：HttpRequestExtensions.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-25 10:55
// 更改时间：2018-12-25 10:56
// ------------------------------------------------------------------------------

#endregion

#region 项目引用

using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

#endregion

namespace Amm.AspNetCore.Mvc.Https
{
    /// <summary>
    ///     HttpContext扩展方法
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        ///     确定指定的 HTTP 请求是否为 AJAX 请求。
        /// </summary>
        /// <returns>
        ///     如果指定的 HTTP 请求是 AJAX 请求，则为 true；否则为 false。
        /// </returns>
        /// <param name="request">HTTP 请求。</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="request" /> 参数为 null（在 Visual Basic 中为 Nothing）。</exception>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var flag = request.Headers?["X-Requested-With"].ToString()
                ?.Equals("XMLHttpRequest", StringComparison.OrdinalIgnoreCase);
            return flag.HasValue && flag.Value;
        }

        /// <summary>
        ///     确定指定的 HTTP 请求的 ContextType 是否为 Json 方式
        /// </summary>
        public static bool IsJsonContextType(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            var flag = request.Headers?["Content-Type"].ToString()
                           .IndexOf("application/json", StringComparison.OrdinalIgnoreCase) > -1
                       || request.Headers?["Content-Type"].ToString()
                           .IndexOf("text/json", StringComparison.OrdinalIgnoreCase) > -1;
            if (flag) return true;
            flag = request.Headers?["Accept"].ToString()
                       .IndexOf("application/json", StringComparison.OrdinalIgnoreCase) > -1
                   || request.Headers?["Accept"].ToString().IndexOf("text/json", StringComparison.OrdinalIgnoreCase) >
                   -1;
            return flag;
        }

        /// <summary>
        ///     获取<see cref="HttpRequest" />的请求数据
        /// </summary>
        /// <param name="request">请求信息</param>
        /// <param name="key">要获取数据的键名</param>
        /// <returns></returns>
        public static string Params(this HttpRequest request, string key)
        {
            if (request.Query.ContainsKey(key)) return request.Query[key];
            if (request.HasFormContentType) return request.Form[key];
            return null;
        }

        /// <summary>
        ///     获取客户端IP地址
        /// </summary>
        public static string GetClientIp(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip)) ip = context.Connection.RemoteIpAddress.ToString();
            return ip;
        }
    }
}