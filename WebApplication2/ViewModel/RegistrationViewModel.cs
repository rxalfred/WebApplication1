using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApplication2.ViewModel
{
    public class RegistrationViewModel
    {
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
