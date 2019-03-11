#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：Amm.AspNetCore
// 文件名称：IAuthorizationPolicyProvider.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-28 13:35
// 更改时间：2018-12-28 13:35
// ------------------------------------------------------------------------------
#endregion

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Amm.AspNetCore.Mvc.Securities
{
    /// <summary>
    /// AmmAuthorizationPolicyProvider
    /// </summary>
    public class AmmAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        /// <summary>
        /// FallbackPolicyProvider
        /// </summary>
        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

        /// <summary>
        /// AmmAuthorizationPolicyProvider
        /// </summary>
        /// <param name="options"></param>
        public AmmAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        /// <summary>
        ///  GetPolicyAsync
        /// </summary>
        /// <param name="policyName"></param>
        /// <returns></returns>
        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            var policy = new AuthorizationPolicyBuilder();

            policy.AddRequirements(new AmmAuthorizationRequirement());

            return Task.FromResult(policy.Build());
        }

        /// <summary>
        /// GetDefaultPolicyAsync
        /// </summary>
        /// <returns></returns>
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return FallbackPolicyProvider.GetDefaultPolicyAsync();
        }
    }
}