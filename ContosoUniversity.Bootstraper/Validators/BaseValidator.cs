using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContosoUniversity.Bootstraper.Validators
{
    /// <summary>
    /// Base validator for common validation methods.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <seealso cref="FluentValidation.AbstractValidator{TRequest}" />
    public abstract class BaseValidator<TRequest> : AbstractValidator<TRequest>, IBaseValidator
    {
        protected static bool DateLaterThanToday(DateTime currentDate)
        {
            return currentDate > DateTime.Now;
        }

        protected static bool DateLaterThanDefinedDate(DateTime currentDate, DateTime definedDate)
        {
            return currentDate > definedDate;
        }
    }
}
