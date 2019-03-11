#region 版权信息

// ------------------------------------------------------------------------------
// 项目名：Amm.Web
// 文件名：IEntity.cs
// 创建标识：梅军章 2018-09-05 14:01
// 修改标识：梅军章 2018-09-05 14:02
// ------------------------------------------------------------------------------

#endregion

namespace Amm.NetworkMark.Domain.Common
{
    /// <summary>
    ///     IEntity
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IEntity<TPrimaryKey> 
    {
        /// <summary>
        ///     Id
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}