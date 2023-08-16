using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FreeERP.CustomValidators
{
	public class DateRangeValidator : ValidationAttribute
    {
        public string OtherPropertyName { get; set; }
        public DateRangeValidator(string othePropertyName)
        {
            OtherPropertyName = othePropertyName;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime to_date = Convert.ToDateTime(value);
                PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);
                if (otherProperty != null)
                {
                    DateTime from_date = Convert.ToDateTime(otherProperty.GetValue(validationContext.ObjectInstance));
                    if (from_date > to_date)
                    {
                        return new ValidationResult(ErrorMessage, new string[] { OtherPropertyName, validationContext.MemberName });
                    }
                }
                
                return ValidationResult.Success;
            }
            return null;
        }
    }
}

