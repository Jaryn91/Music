using Microsoft.AspNetCore.Http.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace Musiction.API.Models.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class PptxExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return new ValidationResult("Ejjj! Załącz jakiś plik!");
            }
            var fileName = ((FormFile)value).FileName;
            if (fileName.ToString().ToLower().EndsWith(".pptx"))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Bo ja potrafię tylko obsługiwać pliki w formacie <b>pptx</b> :(.");
        }
    }
}