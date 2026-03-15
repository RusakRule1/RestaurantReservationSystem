namespace LaMaison.Core.Services;

public interface IReservationService
{
    Task<IEnumerable<TimeOnly>> GetAvailableSlotsAsync(
        DateOnly date, int partySize, bool isPrivateDining);

    Task<bool> HasCapacityAsync(
        DateOnly date, TimeOnly timeSlot, int partySize, bool isPrivateDining);

    string GenerateReferenceCode();
}
