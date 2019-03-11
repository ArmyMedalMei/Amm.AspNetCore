#region 版权信息
//  ---------------------------------------------------------------------
// - 文件名: PageResultWrapper.cs
// - 项目名: Amm.NetworkMark.Domain
// - 作   者：梅军章
// - 创建时间：20181121
//  - © 2002-2030. All rights reserved.
// ---------------------------------------------------------------------
#endregion

using System.Collections.Generic;

namespace Amm.AspNetCore.Datas.Entity
{
    /// <summary>
    ///   分页结果包裹器
    /// </summary>
    public class PageResultWrapper<T>
    {
        /// <summary>
        ///  当前页面
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        ///  分页总条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        ///   分页条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///   分页结果对象
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        ///  扩展字段
        /// </summary>
        public object Extend { get; set; }
    }
}