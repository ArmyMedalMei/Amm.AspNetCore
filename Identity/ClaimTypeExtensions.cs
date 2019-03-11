#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：Amm.NetworkMark.Core
// 文件名称：ClaimTypeExtensions.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-25 10:49
// 更改时间：2018-12-25 10:49
// ------------------------------------------------------------------------------
#endregion

namespace Amm.AspNetCore.Identity
{
    /// <summary>
    ///   用户申明拓展类
    /// </summary>
    public class ClaimTypeExtensions
    {
        /// <summary>
        ///  公司名字
        /// </summary>
        public const string CompanyName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/companyName";

        /// <summary>
        ///  用户名
        /// </summary>
        public const string UserName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/userName";

        /// <summary>
        ///  头像
        /// </summary>
        public const string HeadPortrait = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/headPortrait";
    }
}