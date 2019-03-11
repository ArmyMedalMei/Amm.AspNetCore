#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：JwellFace
// 文件名称：CustomExceptionHandlerOptions.cs
// 版本号: V1.0.0.0
// 创建时间：2018-11-27 9:38
// 更改时间：2018-11-27 9:38
// ------------------------------------------------------------------------------
#endregion

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Amm.AspNetCore.Mvc.Exceptions
{
    /// <summary>
    ///   自定义异常处理选项配置
    /// </summary>
    public class CustomExceptionHandlerOptions
    {
        /// <summary>
        ///  环境变量
        /// </summary>
        public IHostingEnvironment Environment { get; set; }

        /// <summary>
        ///  异常处理页面
        /// </summary>
        public PathString ExceptionHandlingPath { get; set; }

        /// <summary>
        ///  异常处理
        /// </summary>
        public RequestDelegate ExceptionHandler { get; set; }
    }
}