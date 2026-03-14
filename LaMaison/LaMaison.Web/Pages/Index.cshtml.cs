using LaMaison.Core.Entities;
using LaMaison.Core.Enums;
using LaMaison.Core.Rules;
using LaMaison.Core.Services;
using LaMaison.Infrastructure.Data;
using LaMaison.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LaMaison.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IReservationService _reservationService;
    private readonly ApplicationDbContext _context;

    public IndexModel(IReservationService reservationService, ApplicationDbContext context)
    {
        _reservationService = reservationService;
        _context = context;
    }

    [BindProperty]
    public ReservationInput Input { get; set; } = new();

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var today = DateOnly.FromDateTime(DateTime.Today);

        if (Input.Date <= today)
        {
            ModelState.AddModelError("Input.Date", "Please select a future date.");
            return Page();
        }

        if (Input.Date > today.AddDays(30))
        {
            ModelState.AddModelError("Input.Date", "Reservations can only be made up to 30 days in advance.");
            return Page();
        }

        if (Input.IsPrivateDining && !PrivateDiningRules.IsAvailableOnDate(Input.Date.Value))
        {
            ModelState.AddModelError("Input.IsPrivateDining",
                "Private dining is only available on Fridays and Saturdays.");
            return Page();
        }

        bool hasCapacity = await _reservationService.HasCapacityAsync(
            Input.Date.Value, Input.TimeSlot!.Value, Input.PartySize, Input.IsPrivateDining);

        if (!hasCapacity)
        {
            ModelState.AddModelError("Input.TimeSlot",
                "This time slot is no longer available. Please select another.");
            return Page();
        }

        var reservation = new Reservation
        {
            ReferenceCode = _reservationService.GenerateReferenceCode(),
            FullName = ToTitleCase(Input.FullName.Trim()),
            Email = Input.Email.Trim().ToLowerInvariant(),
            PhoneNumber = Input.PhoneNumber.Trim(),
            Date = Input.Date.Value,
            TimeSlot = Input.TimeSlot.Value,
            PartySize = Input.PartySize,
            SpecialRequests = Input.SpecialRequests?.Trim(),
            IsPrivateDining = Input.IsPrivateDining,
            Status = ReservationStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _context.Reservations.AddAsync(reservation);
        await _context.SaveChangesAsync();

        return RedirectToPage("Confirmation", new { code = reservation.ReferenceCode });
    }

    public async Task<IActionResult> OnGetAvailableSlotsAsync(
        DateOnly date, int partySize, bool isPrivateDining)
    {
        var slots = await _reservationService.GetAvailableSlotsAsync(date, partySize, isPrivateDining);
        return new JsonResult(slots.Select(s => s.ToString("HH:mm")));
    }

    private static string ToTitleCase(string value)
        => System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
}
