using DebugSourceLink.Abstract;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

namespace DebugSourceLink.Filters
{
    /// <summary>
    /// Original Version: https://www.cnblogs.com/runningsmallguo/p/10234307.html#commentform
    /// </summary>
    public class TestActionFilter:ActionFilterAttribute
    {
        #region Members.
        private readonly IAmateurPoetry _amateurPoetryFromConstructor;
        #endregion

        #region Constructors.
        /// <summary>
        /// InvalidOperationException: Cannot resolve scoped service 'DebugSourceLink.Abstract.IAmateurPoetry' from root provider.
        /// </summary>
        //public TestActionFilter(IAmateurPoetry amateurPoetry)
        //{
        //    _amateurPoetryFromConstructor = amateurPoetry;
        //}
        public TestActionFilter()
        {
        }
        #endregion

        #region Methods.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //var resultFromConstructor = _amateurPoetryFromConstructor.InterfaceMethodMustImpleInDerivedClass();

            StringValues strValues = new StringValues();
            if (!context.HttpContext.Request.Query.TryGetValue("PoetType", out strValues))
            {
                strValues = new StringValues("AMATEUR");
            };
            /*构造函数中的services是从root provider获取，此处的HttpContext.RequestServices是scoped provider，指向root provider*/
            using (var scopeServices = context.HttpContext.RequestServices.CreateScope())
            {
                var poetUniqueInfo = string.Empty;
                switch (strValues.ToString())
                {
                    case "AMATEUR":
                        poetUniqueInfo = scopeServices.ServiceProvider.GetService<IAmateurPoetry>()?.InterfaceMethodMustImpleInDerivedClass();
                        break;
                    case "PROFESSIONAL":
                    default:
                        poetUniqueInfo = scopeServices.ServiceProvider.GetService<IProfessionalPoetry>()?.InterfaceMethodMustImpleInDerivedClass();
                        break;
                }
            }

            base.OnActionExecuting(context);
        }

        /// <summary>
        /// 同一个request被处理的过程中，scoped类型的service都是同一个实例
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var amateurFromMethodArgument = context.HttpContext.RequestServices.GetRequiredService<IAmateurPoetry>();
            var resultFromMethodArgument = amateurFromMethodArgument.InterfaceMethodMustImpleInDerivedClass();
            base.OnActionExecuted(context);
        }
        #endregion
    }
}
