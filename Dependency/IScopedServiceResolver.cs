#region 版权信息

// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：Amm.NetworkMark.Core
// 文件名称：IScopedServiceResolver.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-25 11:01
// 更改时间：2018-12-25 11:01
// ------------------------------------------------------------------------------

#endregion

#region 项目引用

using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Amm.AspNetCore.Dependency
{
    /// <summary>
    ///     <see cref="ServiceLifetime.Scoped" />服务解析器
    /// </summary>
    public interface IScopedServiceResolver
    {
        /// <summary>
        ///     获取 是否可解析
        /// </summary>
        bool ResolveEnabled { get; }

        /// <summary>
        ///     获取 <see cref="ServiceLifetime.Scoped" />生命周期的服务提供者
        /// </summary>
        IServiceProvider ScopedProvider { get; }

        /// <summary>
        ///     获取指定服务类型的实例
        /// </summary>
        /// <typeparam name="T">服务类型</typeparam>
        /// <returns></returns>
        T GetService<T>();

        /// <summary>
        ///     获取指定服务类型的实例
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        object GetService(Type serviceType);

        /// <summary>
        ///     获取指定服务类型的所有实例
        /// </summary>
        /// <typeparam name="T">服务类型</typeparam>
        /// <returns></returns>
        IEnumerable<T> GetServices<T>();

        /// <summary>
        ///     获取指定服务类型的所有实例
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        IEnumerable<object> GetServices(Type serviceType);
    }
}