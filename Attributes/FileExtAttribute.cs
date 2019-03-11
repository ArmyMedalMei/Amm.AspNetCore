#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：JwellFace
// 文件名称：FileExtAttribute.cs
// 版本号: V1.0.0.0
// 创建时间：2018-11-27 16:01
// 更改时间：2018-11-27 16:01
// ------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Amm.AspNetCore.Attributes
{
    /// <summary>
    ///  文件拓展名验证属性标签
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class FileExtAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        private List<string> AllowedExtensions { get; set; }

        /// <summary>
        ///     文件拓展名
        /// </summary>
        public string Extensions { get; set; }

        /// <summary>
        ///  重写验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            AllowedExtensions = Extensions.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (!(value is IFormFile file)) return true;
            var ext = Path.GetExtension(file.FileName);
            return AllowedExtensions.Any(y => string.Equals(ext, y, StringComparison.CurrentCultureIgnoreCase));

        }
    }
}