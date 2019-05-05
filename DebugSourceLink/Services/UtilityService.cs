using DebugSourceLink.Abstract;
using DebugSourceLink.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace DebugSourceLink.Services
{
    public class UtilityService
    {
        #region Methods.
        public static async Task CreateUnauthorizedResponse(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            httpContext.Response.ContentType = "application/json;charset=utf-8";

            ResponseResult responseResult = new ResponseResult
            {
                Status = false,
                ErrorMessage = "Please login first.",
                ResponseCode = ResponseCode.Unauthorized
            };
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(responseResult), AppHostDefaults.DefaultEncoding);
        }
        #endregion
    }
}
