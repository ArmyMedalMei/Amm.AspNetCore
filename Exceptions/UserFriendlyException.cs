#region 版权信息

//  ---------------------------------------------------------------------
// - 文件名: UserFriendlyException.cs
// - 项目名: Amm.NetworkMark.Core
// - 作   者：梅军章
// - 创建时间：20181118
//  - © 2002-2030. All rights reserved.
// ---------------------------------------------------------------------

#endregion

#region 项目引用

using System;

#endregion

namespace Amm.AspNetCore.Exceptions
{
    /// <summary>
    ///     友好错误异常
    /// </summary>
    public class UserFriendlyException : Exception
    {
        /// <summary>
        /// UserFriendlyException
        /// </summary>
        /// <param name="message"></param>
        public UserFriendlyException(string message) : base(message)
        {
        }

        /// <summary>
        /// UserFriendlyException
        /// </summary>
        /// <param name="exception"></param>
        public UserFriendlyException(Exception exception) : base(exception.Message)
        {

        }
    }
}