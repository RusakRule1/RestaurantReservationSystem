using LaMaison.Core.Enums;
using LaMaison.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LaMaison.Web.Pages.Admin;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
        => _context = context;

    public IReadOnlyList<Core.Entities.Reservation> Reservations { get; set; } = [];
    public int TotalGuests { get; set; }
    public Dictionary<TimeOnly, int> SlotGuestCounts { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public DateOnly? FilterDate { get; set; }

    [BindProperty(SupportsGet = true)]
    public ReservationStatus? FilterStatus { get; set; }

    [BindProperty(SupportsGet = true)]
    public string SortOrder { get; set; } = "date_asc";

    public async Task OnGetAsync()
    {
        var query = _context.Reservations.AsQueryable();

        if (FilterDate.HasValue)
            query = query.Where(r => r.Date == FilterDate.Value);

        if (FilterStatus.HasValue)
            query = query.Where(r => r.Status == FilterStatus.Value);

        query = SortOrder == "date_desc"
            ? query.OrderByDescending(r => r.Date).ThenByDescending(r => r.TimeSlot)
            : query.OrderBy(r => r.Date).ThenBy(r => r.TimeSlot);

        Reservations = await query.ToListAsync();

        if (FilterDate.HasValue)
        {
            TotalGuests = Reservations
                .Where(r => r.Status != ReservationStatus.Cancelled)
                .Sum(r => r.PartySize);

            SlotGuestCounts = Reservations
                .Where(r => !r.IsPrivateDining && r.Status != ReservationStatus.Cancelled)
                .GroupBy(r => r.TimeSlot)
                .ToDictionary(g => g.Key, g => g.Sum(r => r.PartySize));
        }
    }
}
