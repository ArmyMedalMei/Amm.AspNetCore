#region 版权信息

//  ---------------------------------------------------------------------
// - 文件名: MapperExtensions.cs
// - 项目名: Amm.NetworkMark.Core
// - 作   者：梅军章
// - 创建时间：20181125
//  - © 2002-2030. All rights reserved.
// ---------------------------------------------------------------------

#endregion

#region 项目引用

using AutoMapper;

#endregion

namespace Amm.AspNetCore.AutoMappers
{
    /// <summary>
    ///     对象映射扩展操作
    /// </summary>
    public static class MapperExtensions
    {
        /// <summary>
        ///     将对象映射为指定类型
        /// </summary>
        /// <typeparam name="TTarget">要映射的目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns>目标类型的对象</returns>
        public static TTarget MapTo<TTarget>(this object source)
        {
            return Mapper.Map<TTarget>(source);
        }

        /// <summary>
        ///     使用源类型的对象更新目标类型的对象
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="target">待更新的目标对象</param>
        /// <returns>更新后的目标类型对象</returns>
        public static TTarget MapTo<TSource, TTarget>(this TSource source, TTarget target)
        {
            return Mapper.Map(source, target);
        }

    }
}