using NodaTime;

namespace EdiPOC.Transport.Goods;

public record DangerousGoodSpecifications
{
    public string Class { get; private set; }
    public string Label { get; private set; }
    public string UnNumber { get; private set; }
    public bool IsLimitedQuantity { get; private set; }
    public bool IsImdg { get; private set; }
    public bool IsDirty { get; private set; }
    public string? FcNumber { get; private set; }
    public Instant? CryogenDate { get; private set; }
    public Instant? GasDate { get; private set; }
    public string? GasType { get; private set; }
    public int? ExplosiveGrossWeight { get; private set; }
    public bool IsEnvironmentHazard { get; private set; }
    public string? PackingGroup { get; private set; }
    public HashSet<LanguageMutation> Information { get; private set; }

    private DangerousGoodSpecifications() {} // Parameterless constructor for EF Core
    
    private DangerousGoodSpecifications(
        string @class,
        string label,
        string unNumber,
        bool isLimitedQuantity,
        bool isImdg,
        bool isDirty,
        string? fcNumber,
        Instant? cryogenDate,
        Instant? gasDate,
        string? gasType,
        int? explosiveGrossWeight,
        bool isEnvironmentHazard,
        string? packingGroup,
        HashSet<LanguageMutation> information)
    {
        Class = @class;
        Label = label;
        UnNumber = unNumber;
        IsLimitedQuantity = isLimitedQuantity;
        IsImdg = isImdg;
        IsDirty = isDirty;
        FcNumber = fcNumber;
        CryogenDate = cryogenDate;
        GasDate = gasDate;
        GasType = gasType;
        ExplosiveGrossWeight = explosiveGrossWeight;
        IsEnvironmentHazard = isEnvironmentHazard;
        PackingGroup = packingGroup;
        Information = information;
    }

    public static DangerousGoodSpecifications Create(
        string @class,
        string label,
        string unNumber,
        bool isLimitedQuantity,
        bool isImdg,
        bool isDirty,
        string? fcNumber,
        Instant? cryogenDate,
        Instant? gasDate,
        string? gasType,
        int? explosiveGrossWeight,
        bool isEnvironmentHazard,
        string? packingGroup,
        HashSet<LanguageMutation> information)
    {
        return new DangerousGoodSpecifications(@class, label, unNumber, isLimitedQuantity, isImdg, isDirty, fcNumber, cryogenDate, gasDate, gasType, explosiveGrossWeight,
            isEnvironmentHazard, packingGroup, information);
    }
}