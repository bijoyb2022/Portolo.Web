using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Portolo.Framework.Utils;

namespace Portolo.Framework.Web
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PasswordAdvisor.IsPasswordPolicyValidate(value.ToString());
            return ValidationResult.Success;
        }
    }
}