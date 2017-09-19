
using ContosoUniversity.Model.BusinessObject;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.BusinessLogic.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly AppSettings _config;

        public UserLogic(IOptions<AppSettings> config)
        {
            _config = config.Value;
        }

        public string TestUser()
        {
            return _config.Title;
        }
    }
}
