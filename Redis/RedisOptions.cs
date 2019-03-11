#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：JwellFace
// 文件名称：RedisOptions.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-05 15:58
// 更改时间：2018-12-05 15:58
// ------------------------------------------------------------------------------
#endregion

namespace Amm.AspNetCore.Redis
{
    /// <summary>
    /// RedisOptions
    /// </summary>
    public class RedisOptions
    {
        /// <summary>
        ///   数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        ///   数据库索引
        /// </summary>
        public int DataBaseIndex { get; set; }
    }
}