using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace LivescoreAPI.Response
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            this.HttpStatusCode = StatusCodes.Status200OK;
        }

        public int HttpStatusCode { get; set; }

        public string Message { get; set; }

        public Dictionary<string, List<string>> Errors { get; set; }
    }
}
