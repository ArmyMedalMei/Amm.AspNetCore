#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：JwellFace
// 文件名称：MongoDbClient.cs
// 版本号: V1.0.0.0
// 创建时间：2018-11-29 13:45
// 更改时间：2018-11-29 13:45
// ------------------------------------------------------------------------------
#endregion

using System.Reflection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Amm.AspNetCore.MongoDbs
{
    /// <summary>
    ///  芒果数据库对象上下文
    /// </summary>
    public class MongoDbContext<T> : IMongoDbContext<T>
    {
        private readonly IMongoDatabase _db;

        /// <summary>
        /// MongoDbContext
        /// </summary>
        /// <param name="option"></param>
        public MongoDbContext(IOptions<MongoDbOptions> option)
        {
            //芒果数据库客户端配置
            var settings = new MongoClientSettings
            {
                Server = MongoServerAddress.Parse(option.Value.ConnectionString)
            };
            //开启授权操作
            if (option.Value.IsEnabledAuthorization)
            {
                settings.Credential =
                    MongoCredential.CreateCredential(option.Value.DataBase, option.Value.UserName, option.Value.Password);
            }
            var client = new MongoClient(settings);
            _db = client.GetDatabase(option.Value.DataBase);
        }

        /// <summary>
        ///  集合名字
        /// </summary>
        private string _collectionName
        {
            get
            {
                var attribute = typeof(T).GetCustomAttribute<MongoDbCollectionAttribute>()?.CollectionName;
                return !string.IsNullOrWhiteSpace(attribute) ? attribute : typeof(T).Name;
            }
        }

        /// <summary>
        ///   获取集合对象
        /// </summary>
        public IMongoCollection<T> MongoCollection => _db.GetCollection<T>(_collectionName);
    }
}