#region 版权信息

// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：Amm.AspNetCore
// 文件名称：AmmMudleAttribute.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-28 11:28
// 更改时间：2018-12-28 11:49
// ------------------------------------------------------------------------------

#endregion

#region 项目引用

using System;
using Microsoft.AspNetCore.Authorization;

#endregion

namespace Amm.AspNetCore.Mvc.Securities
{
    /// <summary>
    ///     模块授权属性标签
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AmmModuleAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        ///     AmmMudleAttribute
        /// </summary>
        /// <param name="moduleName">模块名称，控制器名称</param>
        public AmmModuleAuthorizeAttribute(string moduleName)
        {
            ModuleName = moduleName;
            Policy = $"AmmPermissionPolicy - {moduleName}";
        }

        /// <summary>
        ///  AmmMudleAttribute
        /// </summary>
        public AmmModuleAuthorizeAttribute(bool loginOnly = false)
        {
            Policy = "AmmPermissionPolicy";
            LoginOnly = loginOnly;
        }

        /// <summary>
        ///     名字
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        ///  仅仅需要登录权限即可
        /// </summary>
        public bool LoginOnly { get; set; }
    }
}