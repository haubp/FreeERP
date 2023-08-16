using System;
using System.ComponentModel.DataAnnotations;

namespace FreeERP.CustomValidators
{
	public class MinimumYearValidatorAttribute : ValidationAttribute
	{
        public int MinimumYear { get; set; }
        public MinimumYearValidatorAttribute()
        {
            MinimumYear = 2000;
        }
        public MinimumYearValidatorAttribute(int minYear)
        {
            MinimumYear = minYear;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;
                if (date.Year >= MinimumYear)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? "Error: {0}", MinimumYear));
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            return null;
        }
    }
}

