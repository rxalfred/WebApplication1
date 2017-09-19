using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace WebApplication2.Providers
{
    public class AppUser : ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal)
           : base(principal)
        {
            //Role = principal.GetClaimValue("secretkey");
        }

        public int Id
        {
            get
            {
                return Convert.ToInt16(this.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
        }

        public string UserName
        {
            get
            {
                return this.FindFirst(ClaimTypes.Name).Value;
            }
        }

        public string GivenName
        {
            get
            {
                return this.FindFirst(ClaimTypes.GivenName).Value;
            }
        }

        public string Email
        {
            get
            {
                return this.FindFirst(ClaimTypes.Email).Value;
            }
        }

        public string Role
        {
            get
            {
                return this.FindFirst(ClaimTypes.Role).Value;
            }
        }

        //public string Role { set; get; }
        
    }
}