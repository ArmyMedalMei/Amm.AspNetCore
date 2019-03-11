namespace Amm.NetworkMark.Domain.Common
{
    /// <summary>
    ///  软删除
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}