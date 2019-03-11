#region 版权信息
//  ---------------------------------------------------------------------
// - 文件名: PageInputDto.cs
// - 项目名: Amm.NetworkMark.Domain
// - 作   者：梅军章
// - 创建时间：20181121
//  - © 2002-2030. All rights reserved.
// ---------------------------------------------------------------------
#endregion

namespace Amm.AspNetCore.Datas.Entity
{
    /// <summary>
    ///   分页请求
    /// </summary>
    public abstract class PageInputDto
    {
        /// <summary>
        ///  当前页码，默认为第一页
        /// </summary>
        public virtual int PageIndex { get; set; } = 1;

        /// <summary>
        ///   分页条数，默认分页10条
        /// </summary>
        public virtual int PageSize { get; set; } = 10;

        /// <summary>
        ///   排序字符串，默认为Id Desc
        ///  <remarks>格式: Id Desc</remarks>
        /// </summary>
        public virtual string Sort { get; set; } = "Id Desc";
    }
}