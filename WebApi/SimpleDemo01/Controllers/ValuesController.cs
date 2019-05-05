using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SimpleDemo01.Controllers
{
    /// <summary>
    /// Values Controller111
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ValuesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// get multiple texts
        /// </summary>
        /// <returns>text array</returns>
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var environmentVariableOne = Environment.GetEnvironmentVariable("Test_Env_Variable_One");
            var environmentType = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var appSettingOne = _configuration["Logging:LogLevel:Default"];
            var appSettingTwo = _configuration["Logging:LogLevel:System"];
            return new string[] { $"appSettingOne: {appSettingOne}", $"appSettingTwo: {appSettingTwo}", $"environmentVariableOne: {environmentVariableOne}", $"environmentType: {environmentType}" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// modify one text resource
        /// </summary>
        /// <param name="value"></param>
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// replace one text resource entirely
        /// </summary>
        /// <param name="id">text id</param>
        /// <param name="value">text value</param>
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// delete one text
        /// </summary>
        /// <param name="id">text resource id</param>
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
