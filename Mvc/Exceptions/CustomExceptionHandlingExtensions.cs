#region 版权信息
//  ---------------------------------------------------------------------
// - 文件名: CustomExceptionHandlingExtensions.cs
// - 项目名: Amm.NetworkMark.Web
// - 作   者：梅军章
// - 创建时间：20181118
//  - © 2002-2030. All rights reserved.
// ---------------------------------------------------------------------
#endregion

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Amm.AspNetCore.Mvc.Exceptions
{
    /// <summary>
    ///  自定义异常处理类
    /// </summary>
    public static class CustomExceptionHandlingExtensions
    {
        /// <summary>
        /// 从管道捕获同步和异步实例参见 <see cref="T:System.Exception"/> 并生成HTML错误响应。
        /// </summary>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app,
            string errorHandlingPath, IHostingEnvironment env)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return app.UseCustomExceptionHandler(new CustomExceptionHandlerOptions
            {
                ExceptionHandlingPath = new PathString(errorHandlingPath),
                Environment = env
            });
        }

        /// <summary>
        /// 从管道捕获同步和异步实例参见 <see cref="T:System.Exception"/> 并生成HTML错误响应。
        /// </summary>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app,
            string errorHandlingPath)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return app.UseCustomExceptionHandler(new CustomExceptionHandlerOptions
            {
                ExceptionHandlingPath = new PathString(errorHandlingPath)
            });
        }

        /// <summary>
        /// 从管道捕获同步和异步实例参见 <see cref="T:System.Exception"/> 并生成HTML错误响应。
        /// </summary>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app, Action<IApplicationBuilder> configure)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (app == null)
                throw new ArgumentNullException(nameof(app));
            if (configure == null)
                throw new ArgumentNullException(nameof(configure));
            var applicationBuilder = app.New();
            configure(applicationBuilder);
            var requestDelegate = applicationBuilder.Build();
            return app.UseCustomExceptionHandler(new CustomExceptionHandlerOptions
            {
                ExceptionHandler = requestDelegate
            });
        }

        /// <summary>
        /// 从管道捕获同步和异步实例参见 <see cref="T:System.Exception"/> 并生成HTML错误响应。
        /// </summary>
        /// <returns>操作完成后app</returns>
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            return app.UseMiddleware<ExceptionHandlingMiddleware>(Array.Empty<object>());
        }

        /// <summary>
        /// 从管道捕获同步和异步实例参见 <see cref="T:System.Exception"/> 并生成HTML错误响应。
        /// </summary>
        /// <returns>操作完成后app</returns>
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app, CustomExceptionHandlerOptions options)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            return app.UseMiddleware<ExceptionHandlingMiddleware>((object)Options.Create(options));
        }
    }
}