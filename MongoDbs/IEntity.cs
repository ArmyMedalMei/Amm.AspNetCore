#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：JwellFace
// 文件名称：IEntity.cs
// 版本号: V1.0.0.0
// 创建时间：2018-11-29 16:28
// 更改时间：2018-11-29 16:28
// ------------------------------------------------------------------------------
#endregion

namespace Amm.AspNetCore.MongoDbs
{
    /// <summary>
    /// IEntity  
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        ///  主键Id
        /// </summary>
        object Id { get; set; }
    }
}