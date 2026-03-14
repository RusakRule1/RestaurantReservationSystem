using LaMaison.Core.Rules;
using LaMaison.Core.Services;
using LaMaison.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LaMaison.Infrastructure.Services;

public class ReservationService : IReservationService
{
    private readonly ApplicationDbContext _context;

    private static readonly TimeOnly[] RegularSlots =
    [
        new(12, 0), new(12, 30), new(13, 0), new(13, 30),
        new(18, 0), new(18, 30), new(19, 0), new(19, 30), new(20, 0)
    ];

    private static readonly TimeOnly[] PrivateSlots =
    [
        new(18, 0), new(20, 0)
    ];

    private const int MaxGuestsPerSlot = 20;

    public ReservationService(ApplicationDbContext context)
        => _context = context;

    public async Task<IEnumerable<TimeOnly>> GetAvailableSlotsAsync(
        DateOnly date, int partySize, bool isPrivateDining)
    {
        if (isPrivateDining)
        {
            if (!PrivateDiningRules.IsAvailableOnDate(date))
                return [];

            var bookedPrivateSlots = await _context.Reservations
                .Where(r => r.Date == date
                         && r.IsPrivateDining
                         && r.Status != Core.Enums.ReservationStatus.Cancelled)
                .Select(r => r.TimeSlot)
                .ToListAsync();

            return PrivateSlots.Where(s => !bookedPrivateSlots.Contains(s));
        }

        var slotGuestCounts = await _context.Reservations
            .Where(r => r.Date == date
                     && !r.IsPrivateDining
                     && r.Status != Core.Enums.ReservationStatus.Cancelled)
            .GroupBy(r => r.TimeSlot)
            .Select(g => new { Slot = g.Key, Count = g.Sum(r => r.PartySize) })
            .ToListAsync();

        return RegularSlots.Where(slot =>
        {
            var booked = slotGuestCounts.FirstOrDefault(s => s.Slot == slot)?.Count ?? 0;
            return booked + partySize <= MaxGuestsPerSlot;
        });
    }

    public async Task<bool> HasCapacityAsync(
        DateOnly date, TimeOnly timeSlot, int partySize, bool isPrivateDining)
    {
        if (isPrivateDining)
        {
            return !await _context.Reservations
                .AnyAsync(r => r.Date == date
                            && r.TimeSlot == timeSlot
                            && r.IsPrivateDining
                            && r.Status != Core.Enums.ReservationStatus.Cancelled);
        }

        var bookedGuests = await _context.Reservations
            .Where(r => r.Date == date
                     && r.TimeSlot == timeSlot
                     && !r.IsPrivateDining
                     && r.Status != Core.Enums.ReservationStatus.Cancelled)
            .SumAsync(r => (int?)r.PartySize) ?? 0;

        return bookedGuests + partySize <= MaxGuestsPerSlot;
    }

    public string GenerateReferenceCode()
    {
        var timestamp = DateTime.UtcNow.ToString("yyMMdd");
        var random = Guid.NewGuid().ToString("N")[..6].ToUpper();
        return $"LM-{timestamp}-{random}";
    }
}
