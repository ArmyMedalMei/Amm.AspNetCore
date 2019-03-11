#region 版权信息
//  ---------------------------------------------------------------------
// - 文件名: IMapTuple.cs
// - 项目名: Amm.NetworkMark.Core
// - 作   者：梅军章
// - 创建时间：20181125
//  - © 2002-2030. All rights reserved.
// ---------------------------------------------------------------------
#endregion

namespace Amm.AspNetCore.AutoMappers
{
    /// <summary>
    /// 定义对象映射源与目标配对
    /// </summary>
    public interface IMapTuple
    {
        /// <summary>
        /// 执行对象映射构造
        /// </summary>
        void CreateMap();
    }
}