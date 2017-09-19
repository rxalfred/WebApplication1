using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Providers
{
    public class AdminRequirement : IAuthorizationRequirement
    {
        public int IsAdmin { get; private set; }

        public AdminRequirement(int isAdmin)
        {
            IsAdmin = isAdmin;
        }
    }
}
