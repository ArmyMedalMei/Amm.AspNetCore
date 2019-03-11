#region 版权信息

// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：JwellFace
// 文件名称：MongoRepository.cs
// 版本号: V1.0.0.0
// 创建时间：2018-11-29 11:25
// 更改时间：2018-11-29 16:08
// ------------------------------------------------------------------------------

#endregion

#region 项目引用

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

#endregion

namespace Amm.AspNetCore.MongoDbs
{
    /// <summary>
    ///     芒果数据库仓储
    /// </summary>
    public class MongoRepository<T> : INoSqlRepository<T> where T : IEntity
    {
        private readonly IMongoDbContext<T> _mongoDbContext;

        public MongoRepository(IMongoDbContext<T> mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        /// <summary>
        ///  InsertAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(T entity)
        {
            await _mongoDbContext.MongoCollection.InsertOneAsync(entity);
        }

        /// <summary>
        /// InsertAsync
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task InsertAsync(IEnumerable<T> entities)
        {
            await _mongoDbContext.MongoCollection.InsertManyAsync(entities);
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(object id)
        {
            var filter = Builders<T>.Filter.Eq(m => m.Id, id);

            return await _mongoDbContext
                .MongoCollection
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="ipredicatesExpression"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(Expression<Func<T, bool>> ipredicatesExpression)
        {
            var filter = Builders<T>.Filter.Where(ipredicatesExpression);

            return await _mongoDbContext
                .MongoCollection
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        ///  DeleteAsync
        /// </summary>
        /// <param name="ipredicatesExpression">表达式</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Expression<Func<T, object>> ipredicatesExpression, object value)
        {
            var filter = Builders<T>.Filter.Eq(ipredicatesExpression, value);

            var deleteResult = await _mongoDbContext
                .MongoCollection
                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                   && deleteResult.DeletedCount > 0;
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T entity)
        {
            var updateResult =
                await _mongoDbContext
                    .MongoCollection
                    .ReplaceOneAsync(
                        g => g.Id == entity.Id,
                        entity);

            return updateResult.IsAcknowledged
                   && updateResult.ModifiedCount > 0;
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(IEnumerable<T> entities)
        {
            var sucessCount = 0;
            foreach (var entity in entities)
            {
                var updateResult =
                    await _mongoDbContext
                        .MongoCollection
                        .ReplaceOneAsync(
                            g => g.Id == entity.Id,
                            entity);

                sucessCount += updateResult.IsAcknowledged
                                && updateResult.ModifiedCount > 0 ? 1 : 0;
            }

            return sucessCount;
        }

        /// <summary>
        /// FindAllAsync
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> FindAllAsync()
        {
            return await _mongoDbContext
                .MongoCollection
                .Find(_ => true)
                .ToListAsync();
        }

        /// <summary>
        ///  FindFilterAsync
        /// </summary>
        /// <param name="predicateExpression"></param>
        /// <returns></returns>
        public async Task<List<T>> FindFilterAsync(Expression<Func<T, bool>> predicateExpression)
        {
            var filter = Builders<T>.Filter.Where(predicateExpression);

            var entities = await _mongoDbContext
                .MongoCollection
                .Find(filter)
                .ToListAsync();

            return entities;
        }
    }
}