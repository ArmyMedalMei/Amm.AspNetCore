#region 版权信息

// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：Amm.NetworkMark.Core
// 文件名称：EnumExtensions.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-25 11:17
// 更改时间：2018-12-25 11:18
// ------------------------------------------------------------------------------

#endregion

#region 项目引用

using System;
using System.ComponentModel;
using System.Linq;
using Amm.AspNetCore.TypeFinders;

#endregion

namespace Amm.AspNetCore.Extentions
{
    /// <summary>
    ///     枚举<see cref="Enum" />的扩展辅助操作方法
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        ///     获取枚举项上的<see cref="DescriptionAttribute" />特性的文字描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum value)
        {
            var type = value.GetType();
            var member = type.GetMember(value.ToString()).FirstOrDefault();
            return member != null ? member.GetDescription() : value.ToString();
        }
    }
}