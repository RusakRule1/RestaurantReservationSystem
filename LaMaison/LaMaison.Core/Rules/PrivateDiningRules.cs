namespace LaMaison.Core.Rules;

public static class PrivateDiningRules
{
    public const int MinPartySize = 6;
    public const int MaxPartySize = 12;

    public static bool IsAvailableOnDate(DateOnly date)
        => date.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday;
}
