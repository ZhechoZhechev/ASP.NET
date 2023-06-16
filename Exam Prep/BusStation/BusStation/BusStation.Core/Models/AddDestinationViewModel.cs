namespace BusStation.Core.Models;

using System.ComponentModel.DataAnnotations;

public class AddDestinationViewModel
{
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string DestinationName { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Origin { get; set; }

    [NotSmallerThanCurrentDate]
    public DateTime DateTime { get; set; }

    [Required]
    [StringLength(30)]
    [RegularExpression(@"^\d{1,2}/\d{1,2}/\d{4}$" , ErrorMessage = "Date have to be in format - MM/dd/yyyy")]
    public string Date { get; set; }

    [Required]
    [StringLength(30)]
    [RegularExpression(@"^([01]\d|2[0-3]):[0-5]\d$", ErrorMessage ="The time have to be in the 24 hours time format - HH:mm")]
    public string Time { get; set; }

    [Required]
    public string ImageUrl { get; set; }
}

public class NotSmallerThanCurrentDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime date = Convert.ToDateTime(value);


        if (date < DateTime.Now.Date)
        {
            return new ValidationResult("The date cannot be smaller than the current date.");
        }

        return ValidationResult.Success;
    }
}

