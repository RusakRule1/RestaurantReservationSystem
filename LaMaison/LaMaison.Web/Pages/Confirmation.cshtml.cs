using LaMaison.Core.Entities;
using LaMaison.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LaMaison.Web.Pages;

public class ConfirmationModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public ConfirmationModel(ApplicationDbContext context)
        => _context = context;

    public Reservation Reservation { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            return RedirectToPage("Index");

        var reservation = await _context.Reservations
            .FirstOrDefaultAsync(r => r.ReferenceCode == code);

        if (reservation is null)
            return RedirectToPage("Index");

        Reservation = reservation;
        return Page();
    }
}
