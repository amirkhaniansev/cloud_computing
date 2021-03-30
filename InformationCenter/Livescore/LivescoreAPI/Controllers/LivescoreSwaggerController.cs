using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LivescoreAPI.Attributes;
using LivescoreAPI.Constants;
using LivescoreAPI.Response;
using LivescoreDAL.Factories;

namespace LivescoreAPI.Controllers
{
    [Validation]
    [ApiController]
    [Produces(ContentTypes.JSON)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public abstract class LivescoreSwaggerController : LivescoreController
    {
        protected readonly DalFactory factory;

        public LivescoreSwaggerController(DalFactory factory)
        {
            this.factory = factory;
        }
    }
}