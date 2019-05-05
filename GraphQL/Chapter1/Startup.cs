using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chapter1.Abstract;
using Chapter1.CustomSchema;
using Chapter1.DAL;
using Chapter1.Middlewares;
using Chapter1.Models;
using Chapter1.Models.GraphqlType;
using Chapter1.Query;
using Chapter1.Query.Mutation;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Chapter1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<ApplicationDbContext>(dbOptionsBuilder =>
            {
                dbOptionsBuilder.UseSqlServer(Configuration.GetSection("DbConnectionStrings")["LocalMSSQL"]);
            });

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            /* Original Version: https://www.cnblogs.com/lwqlun/p/9937468.html
             * !!!!将注册服务的生命周期从单例(Singleton)改为作用域(Scoped), 因为当注入服务的生命周期为单例时，需要处理多线程问题和潜在的内存泄漏问题*/
            services.AddScoped<IMockDataSource, MockDataSource>();
            //services.AddScoped<HelloGraphQL>();
            //services.AddScoped<ISchema, HelloGraphQLSchema>();
            services.AddSingleton<Encoding>(Encoding.UTF8);

            /*GraphQL的优点：1、联合查询多个对象属性比较方便。
              GraphQL的缺点：1、每个对象属性都需要单独写处理方法，若属性较多，需要写很多冗余处理方法。
                            2、借助Mutation修改对象时，还需额外写Mutation片段。*/
            services.AddScoped<InventoryQuery>();
            services.AddScoped<ISchema, InventorySchema>();
            services.AddScoped<SellableItemInputType>();
            services.AddScoped<InventoryMutation>();

            /*!!!!不清楚问题原因，也不清楚解决办法为何起效 Original Version: https://www.cnblogs.com/lwqlun/p/9949559.html*/
            services.AddScoped<SellableItemType>();
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<CustomCustomerType>();
            services.AddScoped<CustomCustomerInputType>();
            services.AddScoped<CustomOrderType>();
            services.AddScoped<CustomOrderInputType>();

            /*【熊林丽推荐】深入了解GraphQL的优缺点 https://ponyfoo.com/articles/graphql-in-depth-what-why-and-how*/
            services.AddScoped<CustomOrderSellableItemRelationType>();
            services.AddScoped<CustomOrderSellableItemRelationInputType>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            //app.UseMvc();

            /*Original Version: https://www.cnblogs.com/lwqlun/p/9925542.html*/
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMiddleware<GraphQLMiddleware>();


            //var schema = new Schema
            //{
            //    Query = new HelloGraphQL()
            //};
            //app.Run(async (context) =>
            //{
            //    if (context.Request.Path.StartsWithSegments("/api/graphql")
            //        && string.Equals(context.Request.Method, "POST", StringComparison.OrdinalIgnoreCase))
            //    {
            //        string body = string.Empty;
            //        using (var streamReader = new StreamReader(context.Request.Body))
            //        {
            //            body = await streamReader.ReadToEndAsync();
            //            var queryRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<GraphQLRequest>(body);
            //            var queryResult = await new DocumentExecuter()
            //                        .ExecuteAsync(executionOption =>
            //                        {
            //                            executionOption.Schema = schema;
            //                            executionOption.Query = queryRequest.Query;
            //                        })
            //                        .ConfigureAwait(false);
            //            var resultJson = new DocumentWriter(indent: true)
            //                                .Write(queryResult);
            //            var resultBytes = Encoding.UTF8.GetBytes(resultJson);
            //            await context.Response.Body.WriteAsync(resultBytes, 0, resultBytes.Length);
            //        }
            //    }
            //});
        }
    }
}
