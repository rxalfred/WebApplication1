﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ContosoUniversity.BusinessLogic.ApiLogic.BusinessObjects
{
    public class RegisterStudentRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
