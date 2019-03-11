#region 版权信息
//  ---------------------------------------------------------------------
// - 文件名: IAutoMapAttributeFinder.cs
// - 项目名: Amm.NetworkMark.Core
// - 作   者：梅军章
// - 创建时间：20181125
//  - © 2002-2030. All rights reserved.
// ---------------------------------------------------------------------
#endregion

using System;

namespace Amm.AspNetCore.AutoMappers
{
    /// <summary>
    ///   自动注入属性查找器接口
    /// </summary>
    public interface IAutoMapAttributeFinder
    {
        /// <summary>
        /// FindAllItems
        /// </summary>
        /// <returns></returns>
        Type[] FindAttributeClassItems();
    }
}