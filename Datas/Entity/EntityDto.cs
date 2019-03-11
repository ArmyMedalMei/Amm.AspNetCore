#region 版权信息
//  ---------------------------------------------------------------------
// - 文件名: EntityDto.cs
// - 项目名: Amm.NetworkMark.Core
// - 作   者：梅军章
// - 创建时间：20181213
//  - © 2002-2030. All rights reserved.
// ---------------------------------------------------------------------
#endregion

namespace Amm.AspNetCore.Datas.Entity
{
    /// <summary>
    /// EntityDto
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityDto<T>
    {
        /// <summary>
        ///  主键ID
        /// </summary>
        public virtual T Id { get; set; }
    }
}