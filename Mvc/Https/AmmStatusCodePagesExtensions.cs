#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：Amm.NetworkMark.Web
// 文件名称：AmmStatusCodePagesExtensions.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-26 11:30
// 更改时间：2018-12-26 11:30
// ------------------------------------------------------------------------------
#endregion

using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Amm.AspNetCore.Datas.Entity;
using Amm.AspNetCore.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amm.AspNetCore.Mvc.Https
{
    /// <summary>
    /// AmmStatusCodePagesExtensions
    /// </summary>
    public static class AmmStatusCodePagesExtensions
    {

        /// <summary>
        /// 添加具有指定处理程序的StatusCodePages中间件，该处理程序检查具有状态代码的响应
        /// 400至599之间，没有响应体
        /// </summary>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomStatusCodePages(this IApplicationBuilder app, string locationFormat)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return app.UseStatusCodePages(new StatusCodePagesOptions
            {
                HandleAsync = context =>
                {
                    var originalPath = context.HttpContext.Request.Path;

                    var isWebApi = new Regex(@"\/api\/", RegexOptions.IgnoreCase).IsMatch(originalPath);

                    //webapi接口页面跳转
                    if (isWebApi)
                    {
                        var statusCode = context.HttpContext.Response.StatusCode;

                        context.HttpContext.Response.ContentType = "application/json";

                        var mvcJsonOptions = (IOptions<MvcJsonOptions>)app.ApplicationServices.GetService(typeof(IOptions<MvcJsonOptions>));

                        //请求的webapi接口JSON对象
                        var actionInfoObect = new JObject();
                        var statusCodeMessage = ((HttpStatusCode)statusCode).ToString();
                        if (statusCode == (int)HttpStatusCode.Forbidden)
                        {
                            var swaggerUiOptions = (IOptions<SwaggerUiOptions>)app.ApplicationServices.GetService(typeof(IOptions<SwaggerUiOptions>));

                            if (swaggerUiOptions == null)
                            {
                                statusCodeMessage = "无当前访问接口权限";
                                actionInfoObect["summary"] = $" - {originalPath.Value}";
                            }
                            else
                            {
                                try
                                {
                                    //建立请求webapi接口相应体
                                    var httpClient = new HttpClient();
                                    var swaggerUiResponseString = httpClient.GetStringAsync($"http://{context.HttpContext.Request.Host.Value}{swaggerUiOptions.Value.SwaggerEndpointUrl}").Result;

                                    //获取swaggerui的相应的JSON对象
                                    var swaggerUiResponseJson = JsonConvert.DeserializeObject<JObject>(swaggerUiResponseString);


                                    //请求webapi接口信息
                                    actionInfoObect = swaggerUiResponseJson["paths"]
                                        .Value<JObject>($@"{originalPath}")
                                        .Value<JObject>(context.HttpContext.Request.Method.ToLower());

                                    statusCodeMessage = $"- {actionInfoObect["summary"]}";
                                }
                                catch (Exception)
                                {
                                    throw new UserFriendlyException("[自定义状态码中间件异常]解析接口权限SwaggeruiJson文件失败");
                                }
                            }
                        }

                        //序列化返回JSON
                        var json = JsonConvert.SerializeObject(new HttpResponseWrapper<NullEntity>(default(NullEntity))
                        {
                            Message = statusCodeMessage,
                            Error = new { ApiUrl = originalPath.Value, ActionDesc = actionInfoObect["summary"] },
                            Success = false,
                            Code = (HttpStatusCode)statusCode
                        }, mvcJsonOptions.Value.SerializerSettings);

                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;

                        return context.HttpContext.Response.WriteAsync(json);
                    }

                    //如果访问地址是js.css则不进行相应的页面跳转
                    if (new Regex(@".*.css|.*.js", RegexOptions.IgnoreCase).IsMatch(originalPath)) return Task.CompletedTask;

                    //异常处理特征
                    var feature = new ExceptionHandlerFeature
                    {
                        Error = new Exception($"{(HttpStatusCode)context.HttpContext.Response.StatusCode}"),
                        Path = context.HttpContext.Request.Path
                    };
                    context.HttpContext.Features.Set<IExceptionHandlerFeature>(feature);
                    context.HttpContext.Features.Set<IExceptionHandlerPathFeature>(feature);

                    //MVC页面状态报错页面跳转
                    if (!locationFormat.StartsWith("~"))
                    {
                        var location = string.Format(CultureInfo.InvariantCulture, locationFormat, context.HttpContext.Response.StatusCode);

                        context.HttpContext.Response.Redirect(location);
                        return Task.CompletedTask;
                    }

                    locationFormat = locationFormat.Substring(1);
                    var str = string.Format(CultureInfo.InvariantCulture, locationFormat, context.HttpContext.Response.StatusCode);
                    context.HttpContext.Response.Redirect(context.HttpContext.Request.PathBase + str);
                    return Task.CompletedTask;
                }
            });
        }
    }
}