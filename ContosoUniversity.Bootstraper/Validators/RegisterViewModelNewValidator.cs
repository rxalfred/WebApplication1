using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication2.ViewModel;

namespace ContosoUniversity.Bootstraper.Validators
{
    public class RegisterViewModelNewValidator : BaseValidator<RegistrationViewNewModel>
    {
        public RegisterViewModelNewValidator()
        {
            RuleFor(reg => reg.FirstName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(reg => reg.LastName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(reg => reg.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100)
                .Must(email => email.EndsWith("microsoft.com"));
        }
    }
}
