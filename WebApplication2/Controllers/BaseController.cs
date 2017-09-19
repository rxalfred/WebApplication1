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
using WebApplication2.Providers;
using System.Security.Claims;

namespace WebApplication2.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
        }

        public AppUser CurrentUser
        {
            get
            {
                return new AppUser(this.User as ClaimsPrincipal);
            }
        }
    }
}
