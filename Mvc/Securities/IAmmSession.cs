#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2019  梅军章
// 项目名称：Amm.AspNetCore
// 文件名称：IAmmSession.cs
// 版本号: V1.0.0.0
// 创建时间：2019-01-17 13:46
// 更改时间：2019-01-17 13:46
// ------------------------------------------------------------------------------
#endregion

namespace Amm.AspNetCore.Mvc.Securities
{
    /// <summary>
    /// IAmmSession
    /// </summary>
    public interface IAmmSession
    {
        /// <summary>
        ///  用户ID
        /// </summary>
        string UserId { get; }

        /// <summary>
        ///  头像
        /// </summary>
        string HeadPortrait { get; }

        /// <summary>
        ///   用户名
        /// </summary>
        string UserName { get; }

        /// <summary>
        ///  姓名
        /// </summary>
        string RealName { get; }

        /// <summary>
        ///  电话号码
        /// </summary>
        string TelPhone { get; }

        /// <summary>
        ///  角色
        /// </summary>
        string RoleName { get; }

        /// <summary>
        ///  公司名称
        /// </summary>
        string CompanyName { get; }

        /// <summary>
        ///  邮箱
        /// </summary>
        string Email { get; }
    }
}