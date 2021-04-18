using LivescoreAPI.Attributes;
using LivescoreAPI.Constants;
using LivescoreAPI.Exceptions;
using LivescoreAPI.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LivescoreAPI.Controllers
{
    [Validation]
    [ApiController]
    [Produces(ContentTypes.JSON)]
    public abstract class LivescoreController : ControllerBase
    {
        protected T Success<T>(T result) where T : ApiResponse
        {
            result.HttpStatusCode = StatusCodes.Status200OK;
            result.Message = Messages.Success;
            return result;
        }

        protected RequestException BadRequest(string message)
        {
            return this.Error(message, StatusCodes.Status400BadRequest);
        }

        protected RequestException NotFound(string message)
        {
            return this.Error(message, StatusCodes.Status404NotFound);
        }

        protected RequestException Error(string message = Errors.InternalError, int code = StatusCodes.Status500InternalServerError)
        {
            return new RequestException(message, code);
        }
    }
}