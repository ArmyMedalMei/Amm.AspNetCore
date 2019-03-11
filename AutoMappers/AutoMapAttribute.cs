#region 版权信息
//  ---------------------------------------------------------------------
// - 文件名: AutoMapAttribute.cs
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
    ///  自动映射标签属性
    /// </summary>
    public class AutoMapAttribute : Attribute
    {
        /// <summary>
        /// 初始化一个<see cref="AutoMapAttribute"/>类型的新实例
        /// </summary>
        public AutoMapAttribute(params Type[] sourceTypes)
        {
            SourceTypes = sourceTypes;
        }

        /// <summary>
        /// 源类型
        /// </summary>
        public Type[] SourceTypes { get; }
    }
}