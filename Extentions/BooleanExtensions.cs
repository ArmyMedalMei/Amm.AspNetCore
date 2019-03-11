#region 版权信息

// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：Amm.NetworkMark.Core
// 文件名称：BooleanExtensions.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-25 11:17
// 更改时间：2018-12-25 11:17
// ------------------------------------------------------------------------------

#endregion

namespace Amm.AspNetCore.Extentions
{
    /// <summary>
    ///     布尔值<see cref="bool" />类型的扩展辅助操作类
    /// </summary>
    public static class BooleanExtensions
    {
        /// <summary>
        ///     把布尔值转换为小写字符串
        /// </summary>
        public static string ToLower(this bool value)
        {
            return value.ToString().ToLower();
        }
    }
}