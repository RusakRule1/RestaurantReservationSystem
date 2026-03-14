namespace LaMaison.Core.Rules;

public static class PrivateDiningRules
{
    public const int MinPartySize = 6;
    public const int MaxPartySize = 12;

    public static readonly TimeOnly[] AvailableSlots =
    [
        new(18, 0), new(18, 30),
        new(19, 0), new(19, 30),
        new(20, 0), new(20, 30),
        new(21, 0)
    ];

    public static bool IsAvailableOnDate(DateOnly date)
        => date.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday;
}