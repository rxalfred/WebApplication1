using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Middleware.Culture
{
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestCultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var cultureQuery = context.Request.Cookies[".AspNetCore.Culture"];
            if (!string.IsNullOrWhiteSpace(cultureQuery))
            {
                //var result = new CookieRequestCultureProvider().DetermineProviderCultureResult(context).Result;
                //result.Cultures
                //var culture = new CultureInfo(result.Cultures);
                
                //CultureInfo.CurrentCulture = ;
                //CultureInfo.CurrentUICulture = result.UICultures.First();

                //var culture = new CultureInfo(cultureQuery);

                //CultureInfo.CurrentCulture = culture;
                //CultureInfo.CurrentUICulture = culture;

            //
            }
            else
            {
                var culture = new CultureInfo("zh");
                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = CultureInfo.CreateSpecificCulture(culture.Name);

                context.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            }

            // Call the next delegate/middleware in the pipeline
            return this._next(context);
        }
    }
}
