using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using LivescoreAPI.Response;
using LivescoreAPI.Constants;
using Newtonsoft.Json;

namespace LivescoreAPI.Attributes
{
    public class ValidationAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var modelState = context.ModelState;
            if (modelState.IsValid)
            {
                await base.OnActionExecutionAsync(context, next);
                return;
            }

            var response = new ApiResponse();
            response.HttpStatusCode = StatusCodes.Status422UnprocessableEntity;
            response.Message = Errors.InvalidModel;
            response.Errors = modelState.ToDictionary(
                kv => kv.Key,
                kv => kv.Value
                        .Errors
                        .Select(e => e.ErrorMessage).ToList());

            context.HttpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            context.HttpContext.Response.ContentType = ContentTypes.JSON;

            await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}