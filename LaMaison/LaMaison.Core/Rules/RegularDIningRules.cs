namespace LaMaison.Core.Rules;

public static class RegularDiningRules
{
    public const int MinPartySize = 1;
    public const int MaxPartySize = 10;
    public const int MaxGuestsPerSlot = 20;

    public static readonly TimeOnly[] AvailableSlots =
    [
        new(12, 0), new(12, 30),
        new(13, 0), new(13, 30),
        new(14, 0), new(14, 30),
        new(15, 0), new(15, 30),
        new(16, 0), new(16, 30),
        new(17, 0), new(17, 30),
        new(18, 0), new(18, 30),
        new(19, 0), new(19, 30),
        new(20, 0), new(20, 30),
        new(21, 0)
    ];
}
