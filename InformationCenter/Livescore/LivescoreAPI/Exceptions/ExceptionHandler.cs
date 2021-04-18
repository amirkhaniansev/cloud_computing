using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using LivescoreAPI.Constants;
using LivescoreAPI.Response;
using Newtonsoft.Json;

namespace LivescoreAPI.Exceptions
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate next;

        public ExceptionHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var rEx = ex as RequestException;
                var statusCode = rEx != null ? rEx.HttpStatusCode : StatusCodes.Status500InternalServerError;
                var message = rEx != null ? rEx.Message : Errors.InternalError;

                var respone = new ApiResponse();
                respone.HttpStatusCode = statusCode;
                respone.Message = message;

                var json = JsonConvert.SerializeObject(respone);

                context.Response.ContentType = ContentTypes.JSON;
                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsync(json);
            }
        }
    }
}
