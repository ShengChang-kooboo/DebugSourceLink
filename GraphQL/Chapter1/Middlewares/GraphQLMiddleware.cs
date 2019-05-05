using Chapter1.Models;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Chapter1.Middlewares
{
    public class GraphQLMiddleware
    {
        #region Members.
        private readonly RequestDelegate _nextDelegate;
        private readonly IDocumentExecuter _documentExecuter;
        private readonly IDocumentWriter _documentWriter;
        private readonly ISchema _helloGraphQLSchema;
        private readonly string _graphQLSegment;
        private readonly string _graphQLRequestMethod;
        private readonly Encoding _textEncoding;
        #endregion

        #region Constructors.
        public GraphQLMiddleware(RequestDelegate requestDelegate, IDocumentExecuter documentExecuter,
                            IDocumentWriter documentWriter, Encoding encoding)
        {
            _nextDelegate = requestDelegate;
            _documentExecuter = documentExecuter;
            _documentWriter = documentWriter;
            /*app.UseMiddleware会注入单例的中间件实例，如果在中间件对象的构造函数中用Scoped的依赖注入服务，会导致
             System.InvalidOperationException: Cannot resolve scoped service 'GraphQL.Types.ISchema' from root provider
             所以把Schema参数修改成从InvokeAsync入口注入
             Original Version: https://www.cnblogs.com/lwqlun/p/9937468.html*/
            //_helloGraphQLSchema = schema;
            _graphQLSegment = "/api/graphql";
            _graphQLRequestMethod = "POST";
            _textEncoding = encoding;
        }
        #endregion

        #region Methods.
        public async Task InvokeAsync(HttpContext httpContext, ISchema schema)
        {
            if (httpContext.Request.Path.StartsWithSegments(_graphQLSegment)
                && string.Equals(httpContext.Request.Method, _graphQLRequestMethod, StringComparison.OrdinalIgnoreCase))
            {
                string body = string.Empty;
                using (var streamReader = new StreamReader(httpContext.Request.Body))
                {
                    body = await streamReader.ReadToEndAsync();
                    var queryRequest = JsonConvert.DeserializeObject<GraphQLRequest>(body);

                    var queryResult = await _documentExecuter
                        .ExecuteAsync(executionOption =>
                        {
                            //executionOption.Schema = _helloGraphQLSchema;
                            executionOption.Schema = schema;

                            executionOption.Query = queryRequest.Query;
                            executionOption.Inputs = queryRequest.Variables.ToInputs();
                        })
                        .ConfigureAwait(false);
                    /*还需要把查询结果，转换成字符串*/
                    var resultJson = await _documentWriter.WriteToStringAsync(queryResult);
                    await httpContext.Response.WriteAsync(resultJson, _textEncoding);
                }
            }
            else
            {
                await _nextDelegate.Invoke(httpContext);
            }
        }
        #endregion
    }
}
