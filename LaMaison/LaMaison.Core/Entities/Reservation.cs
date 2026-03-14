using LaMaison.Core.Enums;

namespace LaMaison.Core.Entities;

public class Reservation
{
    public int Id { get; set; }
    public string ReferenceCode { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public DateOnly Date { get; set; }
    public TimeOnly TimeSlot { get; set; }
    public int PartySize { get; set; }
    public string? SpecialRequests { get; set; }
    public bool IsPrivateDining { get; set; }
    public ReservationStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
