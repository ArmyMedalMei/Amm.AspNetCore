#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：Amm.AspNetCore
// 文件名称：AmmPermissionHandler.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-28 13:44
// 更改时间：2018-12-28 13:44
// ------------------------------------------------------------------------------
#endregion

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Amm.AspNetCore.Constants;
using Amm.AspNetCore.Identity;
using Amm.AspNetCore.Redis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Amm.AspNetCore.Mvc.Securities
{
    /// <summary>
    /// AmmPermissionHandler
    /// </summary>
    public class AmmAuthorizationHandler : IAuthorizationHandler
    {
        private readonly ILogger<AmmAuthorizationHandler> _logger;
        private readonly ICacheService _cacheService;
        private readonly IOptions<AmmAuthorizationOptions> _ammAuthorizationOptions;

        /// <summary>
        /// AmmAuthorizationHandlerHandler
        /// </summary>
        public AmmAuthorizationHandler(
            ILogger<AmmAuthorizationHandler> logger,
            IOptions<AmmAuthorizationOptions> ammAuthorizationOptions,
            ICacheService cacheService)
        {
            _logger = logger;
            _ammAuthorizationOptions = ammAuthorizationOptions;
            _cacheService = cacheService;
        }

        /// <summary>
        /// HandleAsync
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements.ToList();

            foreach (var authorizationRequirement in pendingRequirements)
            {
                if (authorizationRequirement is AmmAuthorizationRequirement)
                {
                    if (IsCanAccessWebApiByUserId(context.User, context.Resource))
                    {
                        context.Succeed(authorizationRequirement);
                    }
                }
            }

            return Task.CompletedTask;
        }

        /// <summary>
        ///  是否能访问webapi通过请求上下文中的用户ID
        /// </summary>
        /// <param name="user"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        private bool IsCanAccessWebApiByUserId(ClaimsPrincipal user, object resource)
        {
            //用户没有认证
            if (!user.Identity.IsAuthenticated)
                return false;

            //请求webapi接口处理
            if (!(resource is AuthorizationFilterContext context) || !_ammAuthorizationOptions.Value.IsEnableAuthrization) return true;

            var action = context.HttpContext.Request.Path;

            _logger.LogInformation($"开始检测权限接口地址：{action}");

            //获取缓存
            var userNameIdentifier = user.GetClaimValue(ClaimTypes.NameIdentifier);

            //使用同步方法
            var userPermissions = _cacheService.GetCacheWithTAsync<List<string>>($"{RedisCacheBase.SecurityUserPermission}{userNameIdentifier}").Result;

            _logger.LogInformation($"获取到用户ID：{userNameIdentifier}的权限值为：{string.Join(",", userPermissions)}");

            //用户的权限里面是否有相应的api的操作
            return userPermissions.Any() && userPermissions.Contains(action);

        }
    }
}