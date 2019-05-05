using DebugSourceLink.Abstract;
using DebugSourceLink.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DebugSourceLink.Middlewares
{
    /// <summary>
    /// Extension method used to add the middleware to the HTTP request pipeline.
    /// </summary>
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomLoggingMiddleware>();
        }

        public static IApplicationBuilder UseCustomAuthorizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAuthorizationMiddleware>();
        }
    }

    /// <summary>
    /// Original Version: https://www.cnblogs.com/runningsmallguo/p/10234307.html#commentform
    /// </summary>
    public class CustomLoggingMiddleware
    {
        #region Members.
        private readonly RequestDelegate _nextDelegate;
        private readonly IAmateurPoetry _amateurPoetry;
        #endregion

        #region Constructors.
        public CustomLoggingMiddleware(RequestDelegate requestDelegate, IAmateurPoetry amateurPoetry01)
        {
            _nextDelegate = requestDelegate;
            _amateurPoetry = amateurPoetry01;
        }
        #endregion

        #region Methods.
        public async Task Invoke(HttpContext httpContext, IAmateurPoetry amateurPoetry02)
        {
            IAmateurPoetry amateurPoetry03 = httpContext.RequestServices.GetRequiredService<IAmateurPoetry>();

            Debug.WriteLine("=====================Request starting=====================");
            await _nextDelegate.Invoke(httpContext);
            Debug.WriteLine($"[_amateurPoetry]{_amateurPoetry.InterfaceMethodMustImpleInDerivedClass()}");
            Debug.WriteLine($"[amateurPoetry02]{amateurPoetry02.InterfaceMethodMustImpleInDerivedClass()}");
            Debug.WriteLine($"[amateurPoetry03]{amateurPoetry03.InterfaceMethodMustImpleInDerivedClass()}");
            Debug.WriteLine("=====================Request complete=====================");
        }
        #endregion
    }

    /// <summary>
    /// Original Version: https://www.bug2048.com/netcore20180327/
    /// </summary>
    public class CustomAuthorizationMiddleware
    {
        #region Members.
        private readonly RequestDelegate _nextDelegate;
        #endregion

        #region Constructors.
        public CustomAuthorizationMiddleware(RequestDelegate requestDelegate)
        {
            _nextDelegate = requestDelegate;
        }
        #endregion

        #region Methods.
        public async Task Invoke(HttpContext httpContext)
        {
            string requestPath = httpContext.Request.Path.ToString().ToLower();
            //判断请求路径是否是不需要排除权限限制的
            if (AppHostDefaults.PublicUris.Contains(requestPath))
            {
                await _nextDelegate(httpContext);
                return;
            }
            //从request header中寻找authorization token
            string userToken = string.Empty;
            bool hasValue = httpContext.Request.Headers.TryGetValue(AppHostDefaults.INVOKER_TOKEN_HEADER, out StringValues token);
            if (!hasValue || token.Count == 0)
            {
                //从request cookie中找token
                userToken = httpContext.Request.Cookies[AppHostDefaults.INVOKER_TOKEN_HEADER];
                if (string.IsNullOrWhiteSpace(userToken))
                {
                    //未授权，跳转到授权入口
                    await UtilityService.CreateUnauthorizedResponse(httpContext);
                    return;
                }
            }
            else
            {
                userToken = token[0];
            }

            //TO DO: check that whether the token is valid

            await _nextDelegate(httpContext);
        }
        #endregion
    }
}
