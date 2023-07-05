﻿using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.FastCode.Common.Emum;
using Misa.FastCode.Common.Error;
using Misa.FastCode.Common.Exceptions;
using Misa.FastCode.Common.Resource;
using OfficeOpenXml.Style.XmlAccess;

namespace Misa.FastCode.Api.MiddleWares
{
    /// <summary>
    /// Middleware xử lý khi exception được bắn ra 
    /// created by: nqhuy(21/05/2023)
    /// </summary>
    public class ExceptionMiddleware
    {

        #region
        private readonly RequestDelegate _next;
        #endregion

        #region
        /// <summary>
        /// phương thức khởi tạo
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <param name="next"></param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion

        #region
        /// <summary>
        /// phương thức bắt exception và gọi hàm xử lý exception
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }


        /// <summary>
        /// phương thức xử lý execption
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // kiểm tra excception có phải do lập trình viên throw hay không
            if (exception is BaseException)
            {

                // tạo message và trả về kết quảư2
                var ex = (BaseException)exception;
                context.Response.StatusCode = (int)ex.HttpStatusCode;
                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(
                        new
                        {
                            statusCode = (int)ex.HttpStatusCode,
                            errorCode = (int)ex.ErrorCode,
                            message = ex.Message,
                            data = ex.Data,
                            userMessage = ex.UserMessage,
                        })
                );

            }
            // trường hợp lỗi do hệ thống throw
            else
            {
                // tạo message và trả về kết quả
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(
                 JsonSerializer.Serialize(
                        new
                        {
                            statusCode = (int)HttpStatusCode.InternalServerError,
                            errorCode = ErrorCode.Exception,
                            message = exception.Message,
                            userMessage = ErrorMessage.InternalError
                        })
                );
            }
        }
        #endregion
    }
}
