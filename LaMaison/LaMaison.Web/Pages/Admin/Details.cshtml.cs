using LaMaison.Core.Entities;
using LaMaison.Core.Enums;
using LaMaison.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LaMaison.Web.Pages.Admin;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
        => _context = context;

    public Reservation Reservation { get; set; } = null!;

    [BindProperty]
    public ReservationStatus NewStatus { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);

        if (reservation is null)
            return RedirectToPage("Index");

        Reservation = reservation;
        NewStatus = reservation.Status;
        return Page();
    }

    public async Task<IActionResult> OnPostUpdateStatusAsync(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);

        if (reservation is null)
            return RedirectToPage("Index");

        reservation.Status = NewStatus;
        reservation.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return RedirectToPage(new { id });
    }
}
