#region 版权信息
//  ---------------------------------------------------------------------
// - 文件名: IAutoMapAttributeFinder.cs
// - 项目名: Amm.NetworkMark.Core
// - 作   者：梅军章
// - 创建时间：20181125
//  - © 2002-2030. All rights reserved.
// ---------------------------------------------------------------------
#endregion

using Amm.AspNetCore.TypeFinders;

namespace Amm.AspNetCore.AutoMappers
{
    /// <summary>
    ///   属性注入
    /// </summary>
    public class AutoMapAttributeFinder : AttributeTypeFinderBase<AutoMapAttribute>, IAutoMapAttributeFinder
    {
        /// <summary>
        /// 属性注入
        /// </summary>
        /// <param name="finder"></param>
        public AutoMapAttributeFinder(ITypeFinder finder) : base(finder)
        {
        }

    }
}