#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：JwellFace
// 文件名称：FileSizeAttribute.cs
// 版本号: V1.0.0.0
// 创建时间：2018-11-27 16:12
// 更改时间：2018-11-27 16:13
// ------------------------------------------------------------------------------
#endregion

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Amm.AspNetCore.Attributes
{
    /// <summary>
    ///   文件大小验证属性标签
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class FileSizeAttribute : ValidationAttribute
    {
        /// <summary>
        /// 文件大小
        /// </summary>
        public int FileSize { get; set; }

        /// <summary>
        ///  重写验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (!(value is IFormFile file)) return true;

            return Convert.ToDecimal(file.Length / 1024) <= FileSize;
        }
    }
}