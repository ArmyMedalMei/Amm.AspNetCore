#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：JwellFace
// 文件名称：INoSqlRepository.cs
// 版本号: V1.0.0.0
// 创建时间：2018-11-29 11:24
// 更改时间：2018-11-29 11:24
// ------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Amm.AspNetCore.MongoDbs
{
    /// <summary>
    ///   非关系型数据库仓储接口
    /// </summary>
    public interface INoSqlRepository<TDocument>
    {
        Task InsertAsync(TDocument entity);
        Task InsertAsync(IEnumerable<TDocument> entities);
        Task<TDocument> GetAsync(object id);
        Task<TDocument> GetAsync(Expression<Func<TDocument, bool>> ipredicatesExpression);
        Task<bool> DeleteAsync(Expression<Func<TDocument, object>> field, object value);
        Task<bool> UpdateAsync(TDocument entity);
        Task<int> UpdateAsync(IEnumerable<TDocument> entities);
        Task<List<TDocument>> FindAllAsync();
        Task<List<TDocument>> FindFilterAsync(Expression<Func<TDocument, bool>> ipredicatesExpression);
    }
}