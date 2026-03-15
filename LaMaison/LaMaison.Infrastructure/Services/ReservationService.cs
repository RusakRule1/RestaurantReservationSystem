using LaMaison.Core.Enums;
using LaMaison.Core.Rules;
using LaMaison.Core.Services;
using LaMaison.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LaMaison.Infrastructure.Services;

public class ReservationService : IReservationService
{
    private readonly ApplicationDbContext _context;
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
                         && r.Status != ReservationStatus.Cancelled)
                .Select(r => r.TimeSlot)
                .ToListAsync();

            return PrivateDiningRules.AvailableSlots
                .Where(s => !bookedPrivateSlots.Contains(s));
        }

        var slotGuestCounts = await _context.Reservations
            .Where(r => r.Date == date
                     && !r.IsPrivateDining
                     && r.Status != ReservationStatus.Cancelled)
            .GroupBy(r => r.TimeSlot)
            .Select(g => new { Slot = g.Key, Count = g.Sum(r => r.PartySize) })
            .ToListAsync();

        return RegularDiningRules.AvailableSlots.Where(slot =>
        {
            var booked = slotGuestCounts.FirstOrDefault(s => s.Slot == slot)?.Count ?? 0;
            return booked + partySize <= RegularDiningRules.MaxGuestsPerSlot;
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
                            && r.Status != ReservationStatus.Cancelled);
        }

        var bookedGuests = await _context.Reservations
            .Where(r => r.Date == date
                     && r.TimeSlot == timeSlot
                     && !r.IsPrivateDining
                     && r.Status != ReservationStatus.Cancelled)
            .SumAsync(r => (int?)r.PartySize) ?? 0;

        return bookedGuests + partySize <= RegularDiningRules.MaxGuestsPerSlot;
    }

    public string GenerateReferenceCode()
    {
        var random = Guid.NewGuid().ToString("N")[..5].ToUpper();
        return $"LM-{random}";
    }
}
