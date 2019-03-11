#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：JwellFace
// 文件名称：FileNoAttribute.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-07 9:47
// 更改时间：2018-12-07 9:47
// ------------------------------------------------------------------------------
#endregion

using System;
using Microsoft.AspNetCore.Http;

namespace Amm.AspNetCore.Attributes
{
    /// <summary>
    ///   未上传文件验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class FileRequiredAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        /// <summary>
        ///  重写验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (!(value is IFormFile file)) return false;
            return file.Length > 0;
        }
    }
}