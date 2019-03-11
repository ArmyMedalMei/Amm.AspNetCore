#region 版权信息

//  ---------------------------------------------------------------------
// - 文件名: AmmActionFilter.cs
// - 项目名: Amm.NetworkMark.Web
// - 作   者：梅军章
// - 创建时间：20181123
//  - © 2002-2030. All rights reserved.
// ---------------------------------------------------------------------

#endregion

#region 项目引用

using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Amm.AspNetCore.Identity;
using Amm.AspNetCore.Mvc.Https;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

#endregion

namespace Amm.AspNetCore.Mvc.Filters
{
    /// <summary>
    ///     自定义过滤器
    /// </summary>
    public class AmmActionFilter : IActionFilter
    {
        private readonly ILogger _logger;
        private readonly JsonSerializerSettings _jsonSerializer;

        /// <summary>
        /// AmmActionFilter
        /// </summary>
        public AmmActionFilter(ILoggerFactory loggerFactory,
            IOptions<MvcJsonOptions> mvcJsonOptions)
        {
            _logger = loggerFactory.CreateLogger<AmmActionFilter>();
            _jsonSerializer = mvcJsonOptions.Value.SerializerSettings;
        }

        /// <summary>
        ///     执行方法之前
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var claimsPrincipal = context.HttpContext.User;

            //判断http请求是否为api接口
            var requestUrl = context.HttpContext.Request.Path;

            if (context.HttpContext.Request.Method.ToLower().Equals("post"))
            {
                var requestBody = string.Empty;

                if (context.HttpContext.Request.Body.CanSeek)
                {
                    using (var requestReader = new StreamReader(context.HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                    {
                        context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
                        requestBody = requestReader.ReadToEnd();
                    }
                }
                _logger.LogInformation($"{claimsPrincipal.GetCommonClaimValue(context.HttpContext)}，请求接口地址:{requestUrl}，POST请求正文为[application/json]：{requestBody}");
            }
            else if (context.HttpContext.Request.Method.ToLower().Equals("get"))
            {
                //get请求参数
                var parms = context.HttpContext.Request.Query.Keys.Aggregate(string.Empty, (current, queryKey) => current + $"{queryKey}:{context.HttpContext.Request.Query[queryKey]}");
                _logger.LogInformation(!string.IsNullOrWhiteSpace(parms)
                    ? $"{claimsPrincipal.GetCommonClaimValue(context.HttpContext)}，请求接口地址:{requestUrl}，GET请求参数为：{parms}"
                    : $"{claimsPrincipal.GetCommonClaimValue(context.HttpContext)}，请求接口地址:{requestUrl}，GET无请求参数");
            }

            var isWebApi = new Regex(@"\/api\/", RegexOptions.IgnoreCase).IsMatch(requestUrl);
            if (!isWebApi)
            {
                if (!context.ModelState.IsValid)
                    context.Result = new BadRequestResult();
            }
            else
            {
                if (!context.ModelState.IsValid)
                {
                    var result = new HttpResponseWrapper
                    {
                        Code = HttpStatusCode.BadRequest,
                        Success = false,
                        Message = string.Join(",", context.ModelState.Values
                            .SelectMany(x => x.Errors)
                            .Select(s => $" - {s.ErrorMessage}")),
                        //具体异常信息
                        Error = context.ModelState.Where(s => s.Value.Errors != null && s.Value.Errors.Any())
                            .Select(x => new
                            {
                                PropertyName = x.Key,
                                ErrorMembers = x.Value.Errors.Select(s => s.ErrorMessage)
                            })
                    };

                    _logger.LogWarning($"{claimsPrincipal.GetCommonClaimValue(context.HttpContext)}，请求接口地址：{requestUrl},试图验证未通过:{JsonConvert.SerializeObject(result, _jsonSerializer)}");

                    context.Result = new JsonResult(result);
                }
            }
        }

        /// <summary>
        ///     执行action之后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}