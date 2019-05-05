using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using WebAPIOne.Abstract;
using WebAPIOne.Services;

namespace WebAPIOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        #region Members.
        /// <summary>
        /// 依赖注入的Service类型可以是抽象类、基类、接口
        /// Original Version: https://www.cnblogs.com/tcjiaan/p/8732848.html
        /// </summary>
        private readonly AbstractValuesService _valuesService;
        #endregion

        #region Constructors.
        public ValuesController(AbstractValuesService valuesService)
        {
            _valuesService = valuesService;
        }
        #endregion

        // GET api/values/defaultget
        /// <summary>
        /// Action级的路由模板，以/开始，则表示从请求Url根路径开始匹配，会使对应Controller级的路由模板失效。
        /// 定义路由时，谨慎以/符号开头
        /// </summary>
        /// <returns></returns>
        [HttpGet("defaultget")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // GET api/values
        [HttpGet("qrcode/getoneimage")]
        public ActionResult<IEnumerable<string>> GetOneImage()
        {
            QRCodeGenerator.ECCLevel eCCLevel = QRCodeGenerator.ECCLevel.L;
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrData = qrGenerator.CreateQrCode("Hello QRCode", eCCLevel))
                {
                    using (QRCode qRCode = new QRCode(qrData))
                    {
                        Bitmap bitmap = qRCode.GetGraphic(20, Color.AliceBlue, Color.Wheat, true);
                        return File(_valuesService.Bitmap2Byte(bitmap, System.Drawing.Imaging.ImageFormat.Png), "image/png",
                            "helloQRCoderLibrary.png");
                    }
                }
            }
            //return new string[] { "QRCodeValue1", "QRCodeValue2" };
        }
    }
}
