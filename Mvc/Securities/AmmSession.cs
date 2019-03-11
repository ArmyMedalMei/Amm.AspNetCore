#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2019  梅军章
// 项目名称：Amm.AspNetCore
// 文件名称：AmmSession.cs
// 版本号: V1.0.0.0
// 创建时间：2019-01-17 13:46
// 更改时间：2019-01-17 13:46
// ------------------------------------------------------------------------------
#endregion


using System.Security.Claims;
using Amm.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace Amm.AspNetCore.Mvc.Securities
{
    /// <summary>
    ///  http会话集成器
    /// </summary>
    public class AmmSession : IAmmSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// AmmSession
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public AmmSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId => _httpContextAccessor.HttpContext.User.GetClaimValue(ClaimTypes.NameIdentifier);

        /// <summary>
        ///  头像
        /// </summary>
        public string HeadPortrait => _httpContextAccessor.HttpContext.User.GetClaimValue(ClaimTypeExtensions.HeadPortrait);

        /// <summary>
        ///  用户名
        /// </summary>
        public string UserName => _httpContextAccessor.HttpContext.User.GetClaimValue(ClaimTypeExtensions.UserName);
        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName => _httpContextAccessor.HttpContext.User.GetClaimValue(ClaimTypes.Name);
        /// <summary>
        ///  电话号码
        /// </summary>
        public string TelPhone => _httpContextAccessor.HttpContext.User.GetClaimValue(ClaimTypes.MobilePhone);

        /// <summary>
        ///  角色名字
        /// </summary>
        public string RoleName => _httpContextAccessor.HttpContext.User.GetClaimValue(ClaimTypes.Role);

        /// <summary>
        /// 公司名字
        /// </summary>
        public string CompanyName => _httpContextAccessor.HttpContext.User.GetClaimValue(ClaimTypeExtensions.CompanyName);
        /// <summary>
        ///  邮箱
        /// </summary>
        public string Email => _httpContextAccessor.HttpContext.User.GetClaimValue(ClaimTypes.Email);
    }
}