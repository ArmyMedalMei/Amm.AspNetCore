#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：JwellFace
// 文件名称：INoSqlDbContext.cs
// 版本号: V1.0.0.0
// 创建时间：2018-11-29 15:56
// 更改时间：2018-11-29 15:56
// ------------------------------------------------------------------------------
#endregion

using MongoDB.Driver;

namespace Amm.AspNetCore.MongoDbs
{
    /// <summary>
    ///   非关系性数据库数据库上下文接口
    /// </summary>
    public interface IMongoDbContext<TEntity>
    {
        /// <summary>
        /// 集合对象
        /// </summary>
        IMongoCollection<TEntity> MongoCollection { get; }
    }
}