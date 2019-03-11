#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：JwellFace
// 文件名称：MongoDbOptions.cs
// 版本号: V1.0.0.0
// 创建时间：2018-11-29 11:16
// 更改时间：2018-11-29 11:16
// ------------------------------------------------------------------------------
#endregion

namespace Amm.AspNetCore.MongoDbs
{
    /// <summary>
    ///  芒果数据库配置项
    /// </summary>
    public class MongoDbOptions
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///   数据库连接
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        ///   是否开启验证
        /// </summary>
        public bool IsEnabledAuthorization { get; set; }

        /// <summary>
        ///   数据库
        /// </summary>
        public string DataBase { get; set; }
    }
}