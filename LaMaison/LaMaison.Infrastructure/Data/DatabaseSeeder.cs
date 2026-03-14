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
                ReferenceCode = "LM-A1B2C",
                FullName = "Ivan Horvat",
                Email = "ivan.horvat@email.com",
                PhoneNumber = "+385911234567",
                Date = new DateOnly(2026, 3, 14),
                TimeSlot = new TimeOnly(12, 0),
                PartySize = 4,
                SpecialRequests = "Stolić uz prozor",
                IsPrivateDining = false,
                Status = ReservationStatus.Confirmed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-D3E4F",
                FullName = "Marija Kovač",
                Email = "marija.kovac@email.com",
                PhoneNumber = "+385921234567",
                Date = new DateOnly(2026, 3, 14),
                TimeSlot = new TimeOnly(12, 0),
                PartySize = 3,
                SpecialRequests = null,
                IsPrivateDining = false,
                Status = ReservationStatus.Confirmed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-G5H6I",
                FullName = "Tomislav Babić",
                Email = "tomislav.babic@email.com",
                PhoneNumber = "+385951234567",
                Date = new DateOnly(2026, 3, 14),
                TimeSlot = new TimeOnly(18, 0),
                PartySize = 8,
                SpecialRequests = "Proslava rođendana, torta na stolu",
                IsPrivateDining = true,
                Status = ReservationStatus.Confirmed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-J7K8L",
                FullName = "Ana Novak",
                Email = "ana.novak@email.com",
                PhoneNumber = "+385981234567",
                Date = new DateOnly(2026, 3, 14),
                TimeSlot = new TimeOnly(19, 0),
                PartySize = 5,
                SpecialRequests = null,
                IsPrivateDining = false,
                Status = ReservationStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-M9N0O",
                FullName = "Petra Šimić",
                Email = "petra.simic@email.com",
                PhoneNumber = "+385911111111",
                Date = new DateOnly(2026, 3, 14),
                TimeSlot = new TimeOnly(20, 0),
                PartySize = 2,
                SpecialRequests = "Veganski meni",
                IsPrivateDining = false,
                Status = ReservationStatus.Confirmed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-P1Q2R",
                FullName = "Luka Perić",
                Email = "luka.peric@email.com",
                PhoneNumber = "+385922222222",
                Date = new DateOnly(2026, 3, 17),
                TimeSlot = new TimeOnly(13, 0),
                PartySize = 6,
                SpecialRequests = "Poslovni ručak",
                IsPrivateDining = false,
                Status = ReservationStatus.Confirmed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-S3T4U",
                FullName = "Maja Blažević",
                Email = "maja.blazevic@email.com",
                PhoneNumber = "+385933333333",
                Date = new DateOnly(2026, 3, 17),
                TimeSlot = new TimeOnly(19, 30),
                PartySize = 3,
                SpecialRequests = null,
                IsPrivateDining = false,
                Status = ReservationStatus.Cancelled,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-V5W6X",
                FullName = "Nikola Jurić",
                Email = "nikola.juric@email.com",
                PhoneNumber = "+385944444444",
                Date = new DateOnly(2026, 3, 20),
                TimeSlot = new TimeOnly(18, 30),
                PartySize = 10,
                SpecialRequests = "Godišnjica braka",
                IsPrivateDining = false,
                Status = ReservationStatus.Confirmed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-Y7Z8A",
                FullName = "Sara Matić",
                Email = "sara.matic@email.com",
                PhoneNumber = "+385955555555",
                Date = new DateOnly(2026, 3, 20),
                TimeSlot = new TimeOnly(20, 0),
                PartySize = 10,
                SpecialRequests = null,
                IsPrivateDining = false,
                Status = ReservationStatus.Confirmed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-B9C0D",
                FullName = "Filip Knežević",
                Email = "filip.knezevic@email.com",
                PhoneNumber = "+385966666666",
                Date = new DateOnly(2026, 3, 20),
                TimeSlot = new TimeOnly(19, 0),
                PartySize = 12,
                SpecialRequests = "Korporativna večera",
                IsPrivateDining = true,
                Status = ReservationStatus.Confirmed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-E1F2G",
                FullName = "Kristina Pavlović",
                Email = "kristina.pavlovic@email.com",
                PhoneNumber = "+385977777777",
                Date = new DateOnly(2026, 3, 21),
                TimeSlot = new TimeOnly(12, 30),
                PartySize = 4,
                SpecialRequests = null,
                IsPrivateDining = false,
                Status = ReservationStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-H3I4J",
                FullName = "Dario Tomić",
                Email = "dario.tomic@email.com",
                PhoneNumber = "+385988888888",
                Date = new DateOnly(2026, 3, 21),
                TimeSlot = new TimeOnly(20, 30),
                PartySize = 7,
                SpecialRequests = "Bez glutena",
                IsPrivateDining = true,
                Status = ReservationStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-K5L6M",
                FullName = "Ivana Marković",
                Email = "ivana.markovic@email.com",
                PhoneNumber = "+385999999999",
                Date = new DateOnly(2026, 3, 21),
                TimeSlot = new TimeOnly(15, 0),
                PartySize = 2,
                SpecialRequests = "Svjećana večera",
                IsPrivateDining = false,
                Status = ReservationStatus.Confirmed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-N7O8P",
                FullName = "Bruno Čović",
                Email = "bruno.covic@email.com",
                PhoneNumber = "+385910000001",
                Date = new DateOnly(2026, 3, 25),
                TimeSlot = new TimeOnly(14, 0),
                PartySize = 5,
                SpecialRequests = null,
                IsPrivateDining = false,
                Status = ReservationStatus.Confirmed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-Q9R0S",
                FullName = "Tina Galić",
                Email = "tina.galic@email.com",
                PhoneNumber = "+385910000002",
                Date = new DateOnly(2026, 3, 25),
                TimeSlot = new TimeOnly(21, 0),
                PartySize = 3,
                SpecialRequests = "Alergija na orašaste plodove",
                IsPrivateDining = false,
                Status = ReservationStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-T1U2V",
                FullName = "Josip Herceg",
                Email = "josip.herceg@email.com",
                PhoneNumber = "+385910000003",
                Date = new DateOnly(2026, 3, 27),
                TimeSlot = new TimeOnly(19, 0),
                PartySize = 8,
                SpecialRequests = null,
                IsPrivateDining = false,
                Status = ReservationStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-W3X4Y",
                FullName = "Renata Božić",
                Email = "renata.bozic@email.com",
                PhoneNumber = "+385910000004",
                Date = new DateOnly(2026, 3, 27),
                TimeSlot = new TimeOnly(20, 30),
                PartySize = 6,
                SpecialRequests = "Proslava promocije",
                IsPrivateDining = true,
                Status = ReservationStatus.Confirmed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-Z5A6B",
                FullName = "Marin Vukić",
                Email = "marin.vukic@email.com",
                PhoneNumber = "+385910000005",
                Date = new DateOnly(2026, 3, 28),
                TimeSlot = new TimeOnly(13, 30),
                PartySize = 4,
                SpecialRequests = null,
                IsPrivateDining = false,
                Status = ReservationStatus.Confirmed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-C7D8E",
                FullName = "Lucija Šaban",
                Email = "lucija.saban@email.com",
                PhoneNumber = "+385910000006",
                Date = new DateOnly(2026, 3, 28),
                TimeSlot = new TimeOnly(18, 0),
                PartySize = 9,
                SpecialRequests = "Vjenčana godišnjica",
                IsPrivateDining = true,
                Status = ReservationStatus.Confirmed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                ReferenceCode = "LM-F9G0H",
                FullName = "Ante Stipić",
                Email = "ante.stipic@email.com",
                PhoneNumber = "+385910000007",
                Date = new DateOnly(2026, 3, 28),
                TimeSlot = new TimeOnly(20, 0),
                PartySize = 6,
                SpecialRequests = null,
                IsPrivateDining = false,
                Status = ReservationStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        context.Reservations.AddRange(reservations);
        context.SaveChanges();
    }
}
