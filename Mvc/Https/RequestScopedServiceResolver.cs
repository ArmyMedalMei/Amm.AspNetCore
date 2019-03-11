#region 版权信息

// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：Amm.NetworkMark.Core
// 文件名称：RequestScopedServiceResolver.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-25 11:09
// 更改时间：2018-12-25 11:09
// ------------------------------------------------------------------------------

#endregion

#region 项目引用

using System;
using System.Collections.Generic;
using Amm.AspNetCore.Dependency;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Amm.AspNetCore.Mvc.Https
{
    /// <summary>
    ///     Request的<see cref="ServiceLifetime.Scoped" />服务解析器
    /// </summary>
    public class RequestScopedServiceResolver : IScopedServiceResolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        ///     初始化一个<see cref="RequestScopedServiceResolver" />类型的新实例
        /// </summary>
        public RequestScopedServiceResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        ///     获取 是否可解析
        /// </summary>
        public bool ResolveEnabled => _httpContextAccessor.HttpContext != null;

        /// <summary>
        ///     获取 <see cref="ServiceLifetime.Scoped" />生命周期的服务提供者
        /// </summary>
        public IServiceProvider ScopedProvider => _httpContextAccessor.HttpContext.RequestServices;

        /// <summary>
        ///     获取指定服务类型的实例
        /// </summary>
        /// <typeparam name="T">服务类型</typeparam>
        /// <returns></returns>
        public T GetService<T>()
        {
            return _httpContextAccessor.HttpContext.RequestServices.GetService<T>();
        }

        /// <summary>
        ///     获取指定服务类型的实例
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            return _httpContextAccessor.HttpContext.RequestServices.GetService(serviceType);
        }

        /// <summary>
        ///     获取指定服务类型的所有实例
        /// </summary>
        /// <typeparam name="T">服务类型</typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetServices<T>()
        {
            return _httpContextAccessor.HttpContext.RequestServices.GetServices<T>();
        }

        /// <summary>
        ///     获取指定服务类型的所有实例
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _httpContextAccessor.HttpContext.RequestServices.GetServices(serviceType);
        }
    }
}