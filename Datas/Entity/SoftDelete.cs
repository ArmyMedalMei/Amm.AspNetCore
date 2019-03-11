using Amm.NetworkMark.Domain.Common;

namespace Amm.AspNetCore.Datas.Entity
{
    /// <summary>
    ///   软删除基类
    /// </summary>
    public abstract class SoftDelete : ISoftDelete
    {
        /// <summary>
        ///  是否删除
        /// </summary>
        public virtual bool IsDeleted { get; set; }
    }
}