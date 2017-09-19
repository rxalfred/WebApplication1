using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.ViewModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApplication2.Models.Interface;
using System.IO;

namespace WebApplication2.Controllers
{
    public class MemberController : BaseController
    {
        public IActionResult Register()
        {
            var isAuth = User.Identity.IsAuthenticated;
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegistrationViewNewModel model)
        {
            if (this.ModelState.IsValid)
            {
                ViewBag.SuccessMessage = "Great!";
            }
            return View();
        }

        [Authorize(Policy = "IsAdmin")]
        public IActionResult TestAdmin()
        {
            try
            {
                var a = CurrentUser.Id;
            }
            catch (Exception ex) { }

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(RegistrationViewModel model)
        {
            try
            {
                int id = 1;
                string username = "alfred";
                string name = "alfred";
                int isAdmin = 1;

                const string Issuer = "http://contoso.com";
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString(), ClaimValueTypes.String, Issuer),
                    new Claim(ClaimTypes.Name, username, ClaimValueTypes.String, Issuer),
                    new Claim(ClaimTypes.GivenName, name, ClaimValueTypes.String, Issuer),
                    new Claim(ClaimTypes.Role, isAdmin.ToString(), ClaimValueTypes.String, Issuer)
                };


                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaims(claims);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                });
            }
            catch(Exception ex)
            {

            }
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index", "home");
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size, filePath });
        }
    }
}