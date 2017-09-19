using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ContosoUniversity.BusinessLogic.Logic;
using ContosoUniversity.Model.BusinessObject;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace WebApplication2.Controllers
{
    //[Route("api/[controller]")]
    [Route("")]
    [ResponseCache(CacheProfileName = "Default")]
    public class HomeController : Controller
    {
        private readonly AppSettings _config;
        private readonly IStringLocalizer<HomeController> _stringLocalizer;
        private readonly IStringLocalizer<Welcome> _sharedLocalizer;
        private readonly ILogger<HomeController> _logger;
        private readonly IUserLogic _userLogic;

        public HomeController(ILogger<HomeController> logger, 
            IStringLocalizer<HomeController> stringLocalizer, 
            IStringLocalizer<Welcome> sharedLocalizer, 
            IOptions<AppSettings> config, IUserLogic userLogic)
        {
            _logger = logger;
            _config = config.Value;
            _userLogic = userLogic;
            _stringLocalizer = stringLocalizer;
            _sharedLocalizer = sharedLocalizer;
        }

        [HttpGet("")]
        [ResponseCache(CacheProfileName = "VaryByHeader")]
        public IActionResult Index()
        {
            var a1 = _stringLocalizer["Welcome"];
            var a2 = _stringLocalizer["Welcome1"];

            //var a3 = _sharedLocalizer["Welcome"];
            var a4 = _sharedLocalizer["Welcome1"];

            var c = CultureInfo.CurrentCulture;
            var uic = CultureInfo.CurrentUICulture;

            var a = _config.Title;
            var b = _userLogic.TestUser();
            //throw new InsufficientMemoryException();
            //_logger.LogInformation("Index page says hello");
            //LogUtil.Log("testing123213123213");
            //new TestLogic().TestLogging();
            //new TestLogic().TestLogging();
            return View();
        }

        [HttpGet("about")]
        //[HttpGet("about/{id}")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpGet("error")]
        [ResponseCache(CacheProfileName = "Never")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost("setlanguage")]
        [ResponseCache(CacheProfileName = "Never")]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
