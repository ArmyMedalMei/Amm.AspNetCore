#region 版权信息
//  ---------------------------------------------------------------------
// - 文件名: IAutoMapperConfiguration.cs
// - 项目名: Amm.NetworkMark.Core
// - 作   者：梅军章
// - 创建时间：20181125
//  - © 2002-2030. All rights reserved.
// ---------------------------------------------------------------------
#endregion

using AutoMapper;

namespace Amm.AspNetCore.AutoMappers
{
    /// <summary>
    /// IAutoMapperConfiguration
    /// </summary>
    public interface IAutoMapperConfiguration
    {
        /// <summary>
        /// 创建对象映射
        /// </summary>
        /// <param name="mapper">映射配置表达</param>
        void CreateMaps(IMapperConfigurationExpression mapper);
    }
}