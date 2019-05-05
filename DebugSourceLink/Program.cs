using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DebugSourceLink.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DebugSourceLink
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

            ///*路由模板中不能随便加空格！*/
            ////string routeTemplate = "weather/{city=0512}/{days=20}";
            //string validationTemplate = @"/weather/{city:regex(^0\d{{2,3}}$)}/{days:int:range(1,20)}";
            ////string validationTemplateOne = @"/weather/{city:regex(^0\d{{2,3}}$)}/{*date}";
            //new WebHostBuilder()
            //    .UseKestrel()
            //    .ConfigureServices(svcs => { svcs.AddRouting(); })
            //    //.Configure(appBuilder => appBuilder.UseRouter(routeBuilder =>
            //    //                            routeBuilder
            //    //                                .MapGet(validationTemplate, WeatherForecast)))
            //    .Configure(appBuilder => appBuilder.UseRouter(routeBuilder =>
            //                                routeBuilder
            //                                    //.MapRoute(validationTemplate, WeatherForecast)
            //                                    .MapMiddlewareGet(validationTemplate, appBuilderOne => { appBuilderOne.Run(WeatherForecast); })
            //                                    )
            //              )
            //    .Build()
            //    .Run();

            ///*Original Version: http://www.cnblogs.com/artech/p/asp-net-core-routing-02.html*/
            ////RouteData routeData = new RouteData();
            ////RouteData.RouteDataSnapshot snapshot = routeData.PushState(null, null, null);
            ////routeData.Values.Add("foo", 1);
            ////routeData.DataTokens.Add("bar", 2);
            ////routeData.Routers.Add(new RouteHandler(null));
            ////snapshot.Restore();
            ////Debug.Assert(!routeData.Values.Any());
            ////Debug.Assert(!routeData.DataTokens.Any());
            ////Debug.Assert(!routeData.Routers.Any());


            //PopulateGenericErrorMiddleware();
            //ShowCompilationException();
            //AddCustomMiddleware();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        #region TryNetCoreRoute
        private static Dictionary<string, string> _cities = new Dictionary<string, string>
        {
            ["010"] = "北京",
            ["028"] = "成都",
            ["0512"] = "苏州"
        };

        /// <summary>
        /// Original Version: https://www.cnblogs.com/artech/p/asp-net-core-routing-01.html
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        internal static async Task WeatherForecast(HttpContext httpContext)
        {
            var routeData = httpContext.GetRouteData();
            if (routeData == null)
            {
                return;
            }

            Encoding messageEncoding = Encoding.UTF8;
            string city;
            int days;
            if (routeData.Values.ContainsKey("city") && routeData.Values.ContainsKey("days"))
            {
                city = _cities[routeData.Values["city"].ToString()];
                days = int.Parse(routeData.Values["days"].ToString());
            }
            else
            {
                city = "苏州";
                days = 17;
            }
            WeatherReport weatherReport = new WeatherReport(city, days);

            httpContext.Response.ContentType = "text/html; charset=utf-8";
            await httpContext.Response.WriteAsync("<html><head><title>Weather</title></head><body>");
            await httpContext.Response.WriteAsync($"<h3>{city}</h3>");
            foreach (var item in weatherReport.WeatherInfos)
            {
                await httpContext.Response.WriteAsync($"{item.Key.ToString("yyyy-MM-dd")}:");
                await httpContext.Response.WriteAsync($"{item.Value.Condition}({item.Value.MinimumTemperature}℃ ~ {item.Value.MaximumTemperature}℃)<br/><br/>");
            }
            await httpContext.Response.WriteAsync("</body></html>");
        }
        #endregion

        #region Error Handling
        /// <summary>
        /// Original Version: https://www.cnblogs.com/artech/p/error-handling-in-asp-net-core-1.html
        /// </summary>
        internal static void PopulateGenericErrorMiddleware()
        {
            Encoding encoding = Encoding.UTF8;
            string customExceptionMessage = "[CustomMessage01] Unhandled exception occured!";
            RequestDelegate customExceptionHandler = async httpContext => await httpContext.Response.WriteAsync(customExceptionMessage, encoding);
            Random random = new Random();

            //1、Adds a StatusCodePages middleware with the specified handler that checks for responses with status codes between 400 and 599 that do not have a body.
            Func<StatusCodeContext, Task> customStatusHandler01 = async statusCodeContext =>
            {
                var response = statusCodeContext.HttpContext.Response;
                if (response.StatusCode > 499)
                {
                    await response.WriteAsync($"({response.StatusCode}) server handling request error");
                }
                else if (response.StatusCode > 399)
                {
                    await response.WriteAsync($"({response.StatusCode}) client request arguments error");
                }
            };
            //2、Adds a StatusCodePages middleware to the pipeline with the specified alternate middleware pipeline to execute to generate the response body.
            RequestDelegate customStatusHandler02 = async httpContext =>
            {
                var response = httpContext.Response;
                if (response.StatusCode > 499)
                {
                    await response.WriteAsync($"({response.StatusCode}) server handling request error");
                }
                else if (response.StatusCode > 399)
                {
                    await response.WriteAsync($"({response.StatusCode}) client request arguments error");
                }
            };
            //3、Adds a middleware delegate to the application's request pipeline.
            Func<RequestDelegate, RequestDelegate> customStatusHandler03 = taskDelegate =>
            {
                return (httpContext) =>
                {
                    var response = httpContext.Response;
                    response.WriteAsync($"<strong>response.StatusCode: {response.StatusCode}</strong><hr/> ");
                    return taskDelegate(httpContext);
                };
            };

            new WebHostBuilder()
                .UseKestrel()
                .ConfigureServices(svcs => svcs.AddRouting())
                .Configure(appBuilder => appBuilder
                                            /*1-显示开发者异常页面*/
                                            //.UseDeveloperExceptionPage()
                                            /*2-显示定制异常页面*/
                                            //.UseExceptionHandler(new ExceptionHandlerOptions { ExceptionHandler = customExceptionHandler })
                                            /*3-用UseExceptionHandler来自定义错误页面路径的方式，不需要单独提供RequestDelegate，会覆盖上面两种异常处理方式*/
                                            //.UseExceptionHandler("/custom_error_page")
                                            //.UseRouter(routeBuilder => routeBuilder.MapRoute("custom_error_page"
                                            //                                            , async httpContext => await httpContext.Response.WriteAsync("[CustomMessage02] custom_error_page")))
                                            //.UseExceptionHandler("/custom_error_page")
                                            //.UseRouter(routeBuilder => routeBuilder.MapRoute("custom_error_page"
                                            //                                            , HandleCustomError))
                                            //.Run(httpContext => 
                                            //        Task.FromException(new InvalidOperationException("A generic error occured.")))
                                            /*4-针对响应状态码定制错误页面*/
                                            //.UseStatusCodePages("text/javascript", "Error occured ({0})")
                                            //.UseStatusCodePages(customStatusHandler01)
                                            /*终于知道如何通过RequestDelegate委托类型，任意添加中间件。
                                              多谢 https://www.cnblogs.com/wyt007/p/8099884.html */
                                            //.UseStatusCodePages(appBuilderOne => {appBuilderOne.Use(customStatusHandler03).Run(customStatusHandler02); })
                                            //.Run(httpContext => { httpContext.Response.StatusCode = 501; return Task.CompletedTask; })
                                            ////.Run(httpContext => Task.Run(() => httpContext.Response.StatusCode = 501))
                                            /*5-用UseStatusCodePagesWithReExecute来自定义错误页面路径
                                              为什么UseStatusCodePagesWithRedirects会改变URL，而UseStatusCodePagesWithReExecute不改变URL?*/
                                            .UseStatusCodePagesWithReExecute("/handle_error_route_and_redirect/{0}")
                                            //.UseStatusCodePagesWithRedirects("/handle_error_route_and_redirect/{0}")
                                            .UseRouter(routeBuilder => routeBuilder.MapRoute("/handle_error_route_and_redirect/{400to599StatusCode}"
                                                                                            , HandleErrorAndServerRedirect))
                                            .Run(httpContext => Task.Run(() => httpContext.Response.StatusCode = random.Next(100, 599)))
                                            
                           )
                .Build()
                .Run();

        }

        /// <summary>
        /// Original Version: https://www.cnblogs.com/artech/p/error-handling-in-asp-net-core-2.html
        /// </summary>
        internal static void ShowCompilationException()
        {
            new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureServices(svcs => svcs.AddMvc())
                .Configure(appBuilder =>
                {
                    appBuilder.UseDeveloperExceptionPage(new DeveloperExceptionPageOptions { SourceCodeLineCount = 3});
                    appBuilder.UseMvc();
                })
                .Build()
                .Run();
        }

        /// <summary>
        /// Original Version: https://www.cnblogs.com/kenwoo/p/9404671.html
        /// </summary>
        internal static void AddCustomMiddleware()
        {
            new WebHostBuilder()
                .UseKestrel()
                //.ConfigureServices(svcs => svcs.AddMvc())
                .Configure(appBuilder =>
                {
                    //appBuilder.UseMvc();
                    appBuilder.Use(async (httpContext, next) =>
                    {
                        await httpContext.Response.WriteAsync($"[Start custom http request middleware02]...{Environment.NewLine}");
                        await next.Invoke();
                        await httpContext.Response.WriteAsync($"[End custom http request middleware02]...{Environment.NewLine}");
                    }).Run(async (httpContext) =>
                    {
                        await httpContext.Response.WriteAsync($"[Start middleware03]    Environment.OSVersion: {Environment.OSVersion}    ");
                        await httpContext.Response.WriteAsync($"[End middleware03]...{Environment.NewLine}");
                    });
                })
                .Build()
                .Run();
        }

        /// <summary>
        /// Original Version: https://www.cnblogs.com/artech/p/error-handling-in-asp-net-core-3.html
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private async static Task HandleCustomError(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "text/html";
            Exception ex = httpContext.Features.Get<IExceptionHandlerPathFeature>()?.Error;
            await httpContext.Response.WriteAsync("<html><head><title>Error</title></head><body>");
            if (ex != null)
            {
                await httpContext.Response.WriteAsync($"<h3>{ex.Message}</h3>");
                await httpContext.Response.WriteAsync($"<p>Type: {ex.GetType().FullName}");
                await httpContext.Response.WriteAsync($"<p>StackTrace: {ex.StackTrace}");
            }
            else
            {
                await httpContext.Response.WriteAsync($"<h3>There is no IExceptionHandlerPathFeature instance in httpContext.Features collection.</h3>");
            }
            await httpContext.Response.WriteAsync("</body></html>");

        }

        /// <summary>
        /// Original Version: https://www.cnblogs.com/artech/p/error-handling-in-asp-net-core-4.html
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private async static Task HandleErrorAndServerRedirect(HttpContext httpContext)
        {
            var from400to599StatusCode = httpContext.GetRouteData().Values["400to599StatusCode"];
            await httpContext.Response.WriteAsync($"Error occured ({from400to599StatusCode}) and has been redirected to '/handle_error_route_and_redirect' route" +
                $"{Environment.NewLine} Why current url doesn't change to /handle_error_route_and_redirect? Because this is a server redirect,");
        }
        #endregion
    }


    //public class CustomCookController : Controller
    //{
    //    #region Methods.
    //    [HttpGet("/")]
    //    public IActionResult Index()
    //    {
    //        return View();
    //        if (typeof(IMiddleware).GetTypeInfo().IsAssignableFrom(typeof(IMiddleware).GetTypeInfo()))
    //        {

    //        }
    //    }
    //    #endregion
    //}
}
