using FluentAssertions;
using LaMaison.Core.Entities;
using LaMaison.Core.Enums;
using LaMaison.Infrastructure.Data;
using LaMaison.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace LaMaison.Tests;

public class ReservationServiceTests
{
    private ApplicationDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    // Test 1: Regular slot should be unavailable when 20 guests already booked
    [Fact]
    public async Task GetAvailableSlots_ShouldExcludeFullSlots_WhenCapacityReached()
    {
        using var context = CreateInMemoryContext();
        var service = new ReservationService(context);
        var date = new DateOnly(2026, 3, 23);
        var slot = new TimeOnly(19, 0);

        context.Reservations.Add(new Reservation
        {
            ReferenceCode = "LM-T0001",
            FullName = "Test User",
            Email = "test@test.com",
            PhoneNumber = "+385911234567",
            Date = date,
            TimeSlot = slot,
            PartySize = 20,
            IsPrivateDining = false,
            Status = ReservationStatus.Confirmed,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });
        await context.SaveChangesAsync();

        var slots = await service.GetAvailableSlotsAsync(date, 1, false);

        slots.Should().NotContain(slot);
    }

    // Test 2: Regular slot should still be available when capacity not reached
    [Fact]
    public async Task GetAvailableSlots_ShouldIncludeSlot_WhenCapacityNotReached()
    {
        using var context = CreateInMemoryContext();
        var service = new ReservationService(context);
        var date = new DateOnly(2026, 3, 23);
        var slot = new TimeOnly(19, 0);

        context.Reservations.Add(new Reservation
        {
            ReferenceCode = "LM-T0002",
            FullName = "Test User",
            Email = "test@test.com",
            PhoneNumber = "+385911234567",
            Date = date,
            TimeSlot = slot,
            PartySize = 10,
            IsPrivateDining = false,
            Status = ReservationStatus.Confirmed,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });
        await context.SaveChangesAsync();

        var slots = await service.GetAvailableSlotsAsync(date, 5, false);

        slots.Should().Contain(slot);
    }

    // Test 3: Private dining should not be available on weekdays
    [Fact]
    public async Task GetAvailableSlots_ShouldReturnEmpty_WhenPrivateDiningOnWeekday()
    {
        using var context = CreateInMemoryContext();
        var service = new ReservationService(context);
        var date = new DateOnly(2026, 3, 23);

        var slots = await service.GetAvailableSlotsAsync(date, 8, true);

        slots.Should().BeEmpty();
    }

    // Test 4: Private dining slot should be unavailable after one booking
    [Fact]
    public async Task GetAvailableSlots_ShouldExcludePrivateSlot_WhenAlreadyBooked()
    {
        using var context = CreateInMemoryContext();
        var service = new ReservationService(context);
        var date = new DateOnly(2026, 3, 28);
        var slot = new TimeOnly(18, 0);

        context.Reservations.Add(new Reservation
        {
            ReferenceCode = "LM-T0003",
            FullName = "Test User",
            Email = "test@test.com",
            PhoneNumber = "+385911234567",
            Date = date,
            TimeSlot = slot,
            PartySize = 8,
            IsPrivateDining = true,
            Status = ReservationStatus.Confirmed,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });
        await context.SaveChangesAsync();

        var slots = await service.GetAvailableSlotsAsync(date, 6, true);

        slots.Should().NotContain(slot);
    }

    // Test 5: Cancelled reservations should not count towards capacity
    [Fact]
    public async Task GetAvailableSlots_ShouldIncludeSlot_WhenExistingReservationIsCancelled()
    {
        using var context = CreateInMemoryContext();
        var service = new ReservationService(context);
        var date = new DateOnly(2026, 3, 23);
        var slot = new TimeOnly(19, 0);

        context.Reservations.Add(new Reservation
        {
            ReferenceCode = "LM-T0004",
            FullName = "Test User",
            Email = "test@test.com",
            PhoneNumber = "+385911234567",
            Date = date,
            TimeSlot = slot,
            PartySize = 20,
            IsPrivateDining = false,
            Status = ReservationStatus.Cancelled,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });
        await context.SaveChangesAsync();

        var slots = await service.GetAvailableSlotsAsync(date, 1, false);

        slots.Should().Contain(slot);
    }

    // Test 6: Private dining should only return slots between 18:00 and 22:00
    [Fact]
    public async Task GetAvailableSlots_ShouldOnlyReturnEveningSlots_WhenPrivateDining()
    {
        using var context = CreateInMemoryContext();
        var service = new ReservationService(context);
        var date = new DateOnly(2026, 3, 28);

        var slots = await service.GetAvailableSlotsAsync(date, 8, true);

        slots.Should().NotContain(s => s.Hour < 18);
        slots.Should().OnlyContain(s => s.Hour >= 18 && s.Hour < 22);
    }
}
