using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Claims;

namespace WebApplication2.Providers
{
    public abstract class MyRazorView<TModel> : RazorPage<TModel>
    {
        protected AppUser CurrentUser
        {
            get
            {
                return new AppUser(User as ClaimsPrincipal);
            }
        }

        public bool IsAuthenticated()
        {
            return User.Identity.IsAuthenticated;
        }
    }

    public abstract class MyRazorView : MyRazorView<dynamic>
    {
    }
}