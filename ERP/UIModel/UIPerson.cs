using System;
namespace FreeERP.Models
{
    public class UIPerson
    {
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender PersonGender { get; set; }

    }
    public enum Gender
    {
        Male, Female, Other
    }
}

