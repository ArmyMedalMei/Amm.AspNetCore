#region 版权信息
//  ---------------------------------------------------------------------
// - 文件名: ExceptionHandlingMiddleware.cs
// - 项目名: Amm.NetworkMark.Web
// - 作   者：梅军章
// - 创建时间：20181118
//  - © 2002-2030. All rights reserved.
// ---------------------------------------------------------------------
#endregion

using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Amm.AspNetCore.Datas;
using Amm.AspNetCore.Datas.Entity;
using Amm.AspNetCore.Exceptions;
using Amm.AspNetCore.Mvc.Https;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Amm.AspNetCore.Mvc.Exceptions
{
    /// <summary>
    /// 自定义异常处理中间件
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly CustomExceptionHandlerOptions _options;
        private readonly JsonSerializerSettings _jsonSerializer;
        private readonly ILogger _logger;

        /// <summary>
        /// ExceptionHandlingMiddleware
        /// </summary>
        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory,
            IOptions<CustomExceptionHandlerOptions> options,
            IOptions<MvcJsonOptions> mvcJsonOptions)
        {
            _next = next;
            _jsonSerializer = mvcJsonOptions.Value.SerializerSettings;
            _options = options.Value;
            _logger = loggerFactory.CreateLogger<ExceptionHandlingMiddleware>();
            if (_options.ExceptionHandler != null)
                return;
            if (_options.ExceptionHandlingPath == null)
                throw new InvalidOperationException("请求的异常处理地址为空");
            _options.ExceptionHandler = _next;
        }

        /// <summary>
        /// 执行句柄
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            //判断http请求是否为api接口
            var originalPath = context.Request.Path;
            var isWebApi = new Regex(@"\/api\/", RegexOptions.IgnoreCase).IsMatch(originalPath);
            try
            {
                await _next(context);
            }
            catch (Exception ex1)
            {
                var errorMsg = "程序在运行期间发生了一个未知错误";
                if (ex1 is UserFriendlyException)
                    errorMsg = ex1.Message;

                _logger.LogError(ex1, errorMsg);

                if (context.Response.HasStarted)
                {
                    _logger.LogError("页面资源响应已经开始发送！", ex1);
                    throw;
                }
                //判断api和页面报错
                if (isWebApi)
                    await HandleExceptionAsync(context, errorMsg);
                else
                {
                    context.Response.StatusCode = 500;

                    var feature = new ExceptionHandlerFeature
                    {
                        Error = ex1,
                        Path = context.Request.Path,
                    };
                    context.Response.OnStarting(ClearCacheHeaders, context.Response);
                    context.Features.Set<IExceptionHandlerFeature>(feature);
                    context.Features.Set<IExceptionHandlerPathFeature>(feature);

                    if (_options.ExceptionHandlingPath.HasValue)
                    {
                        context.Request.Path = _options.ExceptionHandlingPath;
                    }
                    var handler = _options.ExceptionHandler ?? _next;
                    try
                    {
                        await handler(context);
                    }
                    finally
                    {
                        context.Request.Path = originalPath;
                    }
                }
            }
        }



        /// <summary>
        ///   清除缓存头部
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private Task ClearCacheHeaders(object state)
        {
            var httpResponse = (HttpResponse)state;
            httpResponse.Headers["Cache-Control"] = "no-cache";
            httpResponse.Headers["Pragma"] = "no-cache";
            httpResponse.Headers["Expires"] = "-1";
            httpResponse.Headers.Remove("ETag");
            return Task.CompletedTask;
        }

        /// <summary>
        ///   处理异常
        /// </summary>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context, string msg, bool isUserFriendlyException = true)
        {
            //清除响应体
            context.Response.Clear();
            var json = JsonConvert.SerializeObject(new HttpResponseWrapper<NullEntity>(default(NullEntity))
            {
                Message = msg,
                Success = false,
                Code = isUserFriendlyException ? HttpStatusCode.OK : HttpStatusCode.InternalServerError
            }, _jsonSerializer);

            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(json);
        }
    }
}