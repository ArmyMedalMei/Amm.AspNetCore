#region 版权信息

//  ---------------------------------------------------------------------
// - 文件名: AutoMapperExtension.cs
// - 项目名: Amm.NetworkMark.Core
// - 作   者：梅军章
// - 创建时间：20181125
//  - © 2002-2030. All rights reserved.
// ---------------------------------------------------------------------

#endregion

#region 项目引用

using System.Linq;
using Amm.AspNetCore.TypeFinders;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Amm.AspNetCore.AutoMappers
{
    /// <summary>
    ///     automaper拓展类
    /// </summary>
    public static class AutoMapperExtension
    {
        /// <summary>
        ///     添加自动实体映射
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(new MapperConfigurationExpression());
            services.AddSingleton<ITypeFinder, AppDomainTypeFinder>();
            services.AddSingleton<IAutoMapAttributeFinder, AutoMapAttributeFinder>();
            services.AddSingleton<IMapTuple, MapTupleProfile>();

            return services;
        }

        /// <summary>
        ///     使用自动实体映射
        /// </summary>
        public static void UserAutoMapper(this IApplicationBuilder app)
        {
            var provider = app.ApplicationServices;

            var cfg = provider.GetService<MapperConfigurationExpression>() ?? new MapperConfigurationExpression();

            //各个模块DTO自定义的 IAutoMapperConfiguration 映射实现类
            var configs = provider.GetServices<IAutoMapperConfiguration>().ToArray();

            foreach (var config in configs) config.CreateMaps(cfg);

            //获取已注册到IoC的所有Profile
            var tuples = provider.GetServices<IMapTuple>().ToArray();
            foreach (var mapTuple in tuples)
            {
                mapTuple.CreateMap();
                cfg.AddProfile(mapTuple as Profile);
            }

            Mapper.Initialize(cfg);
        }
    }
}