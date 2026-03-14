using LaMaison.Core.Entities;
using LaMaison.Core.Enums;

namespace LaMaison.Infrastructure.Data;

public static class DatabaseSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        if (context.Reservations.Any())
            return;

        var reservations = new List<Reservation>
        {
            new()
            {
                ReferenceCode = "LM-AA111",
                FullName = "Ivan Horvat",
                Email = "ivan.horvat@email.com",
                PhoneNumber = "+385911234567",
                Date = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                TimeSlot = new TimeOnly(19, 0),
                PartySize = 4,
                SpecialRequests = "Stolić uz prozor",
                IsPrivateDining = false,
                Status = ReservationStatus.Confirmed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-BB222",
                FullName = "Marija Kovač",
                Email = "marija.kovac@email.com",
                PhoneNumber = "+385921234567",
                Date = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                TimeSlot = new TimeOnly(20, 0),
                PartySize = 2,
                SpecialRequests = null,
                IsPrivateDining = false,
                Status = ReservationStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-CC3333",
                FullName = "Tomislav Babić",
                Email = "tomislav.babic@email.com",
                PhoneNumber = "+385951234567",
                Date = DateOnly.FromDateTime(DateTime.Today.AddDays(2)),
                TimeSlot = new TimeOnly(18, 0),
                PartySize = 8,
                SpecialRequests = "Proslava rođendana",
                IsPrivateDining = true,
                Status = ReservationStatus.Confirmed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-DD4444",
                FullName = "Ana Novak",
                Email = "ana.novak@email.com",
                PhoneNumber = "+385981234567",
                Date = DateOnly.FromDateTime(DateTime.Today.AddDays(3)),
                TimeSlot = new TimeOnly(12, 0),
                PartySize = 3,
                SpecialRequests = null,
                IsPrivateDining = false,
                Status = ReservationStatus.Cancelled,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-EE5555",
                FullName = "Petra Šimić",
                Email = "petra.simic@email.com",
                PhoneNumber = "+385911111111",
                Date = DateOnly.FromDateTime(DateTime.Today.AddDays(4)),
                TimeSlot = new TimeOnly(19, 30),
                PartySize = 6,
                SpecialRequests = "Veganski meni",
                IsPrivateDining = false,
                Status = ReservationStatus.Confirmed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        context.Reservations.AddRange(reservations);
        context.SaveChanges();
    }
}
