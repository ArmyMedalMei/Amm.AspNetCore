#region 版权信息

// ------------------------------------------------------------------------------
// 项目名：Amm.Web
// 文件名：HttpResponseHelper.cs
// 创建标识：梅军章 2018-09-04 11:41
// 修改标识：梅军章 2018-09-04 12:21
// ------------------------------------------------------------------------------

#endregion

#region 命名空间

using System.Net;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace Amm.AspNetCore.Mvc.Https
{
    /// <summary>
    ///     请求体包括器
    /// </summary>
    public static class HttpResponseHelper
    {

        /// <summary>
        ///   将响应体返回成JSON字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="message">操作消息</param>
        /// <returns></returns>
        public static JsonResult ResponseAsSuccessJson<T>(this T entity, string message = "操作成功")
        {
            var httpWarpper = new HttpResponseWrapper<T>(entity)
            {
                Message = message
            };

            return new JsonResult(httpWarpper);
        }

        /// <summary>
        ///   将响应体返回成JSON字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="message">操作消息</param>
        /// <returns></returns>
        public static JsonResult ResponseAsErrorJson<T>(this T entity, string message = "操作成功")
        {
            var httpWarpper = new HttpResponseWrapper<T>(entity)
            {
                Message = message,
                Success =  false
            };

            return new JsonResult(httpWarpper);
        }
    }

    /// <summary>
    ///     响应体包裹器
    /// </summary>
    public class HttpResponseWrapper
    {
        /// <summary>
        ///     代码
        /// </summary>
        public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;

        /// <summary>
        ///  异常信息
        /// </summary>
        public object Error { get; set; }

        /// <summary>
        ///  是否成功
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        ///     消息
        /// </summary>
        public string Message { get; set; }
    }

    /// <inheritdoc />
    /// <summary>
    ///     响应体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpResponseWrapper<T> : HttpResponseWrapper
    {
        public HttpResponseWrapper(T result)
        {
            Result = result;
        }

        /// <summary>
        ///     结果体
        /// </summary>
        public T Result { get; set; }
    }
}