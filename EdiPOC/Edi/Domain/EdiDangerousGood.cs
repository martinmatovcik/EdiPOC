using MIS3.Trucks.Common.Extensions;

namespace EdiPOC.Edi.Domain;

public sealed record EdiDangerousGood(string UnNumber, string? Description, string Class, string Label, string PackingGroup, int Weight, int? ExplosiveWeight, bool IsLimitedQuantity)
{
    private string _unNumber = UnNumber;
    private string? _description = Description;
    private string _class = Class;
    private string _label = Label;
    private string _packingGroup = PackingGroup;

    public string UnNumber
    {
        get => _unNumber;
        private set => _unNumber = value.RemoveControlCharacters() ?? string.Empty;
    }

    public string? Description
    {
        get => _description;
        private set => _description = value.RemoveControlCharacters();
    }

    public string Class
    {
        get => _class;
        private set => _class = value.RemoveControlCharacters() ?? string.Empty;
    }

    public string Label
    {
        get => _label;
        private set => _label = value.RemoveControlCharacters() ?? string.Empty;
    }

    public string PackingGroup
    {
        get => _packingGroup;
        private set => _packingGroup = value.RemoveControlCharacters() ?? string.Empty;
    }

    public int Weight { get; private set; } = Weight;
    public int? ExplosiveWeight { get; private set; } = ExplosiveWeight;
    public bool IsLimitedQuantity { get; private set; } = IsLimitedQuantity;
}