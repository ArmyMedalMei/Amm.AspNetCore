#region 版权信息
//  ---------------------------------------------------------------------
// - 文件名: StringLenght.cs
// - 项目名: Amm.AspNetCore
// - 作   者：梅军章
// - 创建时间：20190117
//  - © 2002-2030. All rights reserved.
// ---------------------------------------------------------------------
#endregion

using System;
using System.ComponentModel.DataAnnotations;

namespace Amm.AspNetCore.Attributes
{
    /// <summary>
    ///  字符串长度属性标签
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class StringLenghtRangeAttribute : ValidationAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minNum"></param>
        /// <param name="maxNum"></param>
        public StringLenghtRangeAttribute(int minNum, int maxNum)
        {
            MinNum = minNum;
            MaxNum = maxNum;
        }

        /// <summary>
        ///  最小长度
        /// </summary>
        public int MinNum { get; set; }

        /// <summary>
        ///  最大长度
        /// </summary>
        public int MaxNum { get; set; }

        /// <summary>
        ///  重写验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            return value != null && value.ToString().Length >= MinNum && value.ToString().Length <= MaxNum;
        }
    }
}