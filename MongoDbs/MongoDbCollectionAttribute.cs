#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：JwellFace
// 文件名称：MongoDbCollectionAttribute.cs
// 版本号: V1.0.0.0
// 创建时间：2018-11-29 17:15
// 更改时间：2018-11-29 17:15
// ------------------------------------------------------------------------------
#endregion

using System;

namespace Amm.AspNetCore.MongoDbs
{
    /// <summary>
    ///   芒果数据库表属性标签
    /// </summary>
    public class MongoDbCollectionAttribute : Attribute
    {
        /// <summary>
        /// CollectionName
        /// </summary>
        public string CollectionName { get; set; }

        /// <summary>
        ///MongoDbCollectionAttribute
        /// </summary>
        /// <param name="collectionName"></param>
        public MongoDbCollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}