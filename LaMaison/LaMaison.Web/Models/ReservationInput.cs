using LaMaison.Core.Rules;
using System.ComponentModel.DataAnnotations;

namespace LaMaison.Web.Models;

public class ReservationInput : IValidatableObject
{
    [Required(ErrorMessage = "Full name is required.")]
    [MaxLength(200)]
    [RegularExpression(@"^[\p{L}\s\-']+$",
        ErrorMessage = "Name can only contain letters, spaces, and hyphens.")]
    public string FullName { get; set; } = "";

    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [MaxLength(254)]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Phone number is required.")]
    [MaxLength(20)]
    [RegularExpression(@"^\+[1-9]\d{6,14}$",
        ErrorMessage = "Phone number must be in international format, e.g. +385911234567")]
    public string PhoneNumber { get; set; } = "";

    [Required(ErrorMessage = "Please select a date.")]
    public DateOnly? Date { get; set; }

    [Required(ErrorMessage = "Please select a time slot.")]
    public TimeOnly? TimeSlot { get; set; }

    public int PartySize { get; set; } = 2;

    [MaxLength(500, ErrorMessage = "Special requests cannot exceed 500 characters.")]
    public string? SpecialRequests { get; set; }

    public bool IsPrivateDining { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (IsPrivateDining)
        {
            if (PartySize < PrivateDiningRules.MinPartySize || PartySize > PrivateDiningRules.MaxPartySize)
                yield return new ValidationResult($"Private dining requires between {PrivateDiningRules.MinPartySize} and {PrivateDiningRules.MaxPartySize} guests.",
                    [nameof(PartySize)]);
        }
        else
        {
            if (PartySize < RegularDiningRules.MinPartySize || PartySize > RegularDiningRules.MaxPartySize)
                yield return new ValidationResult($"Party size must be between {RegularDiningRules.MinPartySize} and {RegularDiningRules.MaxPartySize}.",
                    [nameof(PartySize)]);
        }
    }
}
