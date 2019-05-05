using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DebugSourceLink.Models;
using System.Net.Http;
using UtilityLibrary.Helper;
using DebugSourceLink.Abstract;
using DebugSourceLink.Filters;

namespace DebugSourceLink.Controllers
{
    public class HomeController : Controller
    {
        #region Members.
        public readonly IAmateurPoetry _amateurPoetry;
        public readonly IProfessionalPoetry _professionalPoetry;
        #endregion

        #region Constructors.
        public HomeController(IAmateurPoetry amateurPoetry, IProfessionalPoetry professionalPoetry)
        {
            _amateurPoetry = amateurPoetry;
            _professionalPoetry = professionalPoetry;
        }
        #endregion

        #region Methods.

        //[TypeFilter(typeof(TestActionFilter))]
        [ServiceFilter(typeof(TestActionFilter))]
        public IActionResult Index()
        {
            /*发送大量请求时，建议用HttpClientFactory实现HttpHandler的复用*/
            //HttpClient httpClient = new HttpClient();
            //var result = WebHelper.GetWebPageSourceCode(new Uri("https://www.baidu.com")).Result;

            string poemTitle = "LiXuePian";
            ViewData["amateurPoetryUniqueInfo"] = _amateurPoetry.InterfaceMethodMustImpleInDerivedClass();
            ViewData["amateurPoetry"] = _amateurPoetry.AmateurPoetryStyle(poemTitle);
            ViewData["professionalPoetryUniqueInfo"] = _professionalPoetry.InterfaceMethodMustImpleInDerivedClass();
            ViewData["professionalPoetry"] = _professionalPoetry.ProfessionalPoetryStyle(poemTitle);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
