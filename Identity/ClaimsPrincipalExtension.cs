#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：Amm.NetworkMark.Core
// 文件名称：ClaimsPrincipalExtension.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-25 10:16
// 更改时间：2018-12-25 10:17
// ------------------------------------------------------------------------------
#endregion

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Amm.AspNetCore.Mvc;
using Amm.AspNetCore.Mvc.Https;
using Microsoft.AspNetCore.Http;

namespace Amm.AspNetCore.Identity
{
    /// <summary>
    /// ClaimsPrincipal拓展类 
    /// </summary>
    public static class ClaimsPrincipalExtension
    {
        /// <summary>
        /// 获取申明值
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public static string GetClaimValue(this IEnumerable<Claim> claims, string claimType)
        {
            var claim = claims.FirstOrDefault(c => c.Type == claimType);

            return string.IsNullOrEmpty(claim?.Value) ? "" : claim?.Value;
        }

        /// <summary>
        ///  获取申明值
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public static string GetClaimValue(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            return GetClaimValue(claimsPrincipal.Claims, claimType);
        }

        /// <summary>
        ///  获取请求通用值
        /// </summary>
        /// <returns></returns>
        public static string GetCommonClaimValue(this ClaimsPrincipal claimsPrincipal, HttpContext context)
        {
            return
                $"用户[ID：{claimsPrincipal.GetClaimValue(ClaimTypes.NameIdentifier)}，姓名：{claimsPrincipal.Identity.Name}，IP：{context.GetClientIp()}]";
        }
    }
}