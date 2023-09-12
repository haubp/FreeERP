using System;
using FreeERP.Model.Tickets;
using FreeERP.Utils;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
using FreeERP.CustomValidators;

namespace FreeERP.Model
{
    public class TestModel : IValidatableObject
    {
        [Required(ErrorMessage = "{0} is permanently")]
        [Display(Name = "Test Name")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} length is at least {2}, max {1}")]
        [DataType(DataType.Text)]
        public string? Name { get; set; }

        [MinimumYearValidator(1996, ErrorMessage = "Minimum is {0}")]
        public DateTime DateOfBirth { get; set; }

        public DateTime FromDate { get; set; }

        [DateRangeValidator("FromDate", ErrorMessage = "From Date should less than ToDate")]
        public DateTime ToDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return new ValidationResult("");
        }
    }

    public class ListModel {
        public string ListTitle { get; set; } = "";
        public List<string> ListItems { get; set; }
    }
}
