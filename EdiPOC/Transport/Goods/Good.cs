namespace EdiPOC.Transport.Goods;

public record Good
{
    public string? PackagingCode { get; private set; }
    public string? CombinedNomenclatureCode { get; private set; }
    public string? Description { get; private set; }
    public int? Quantity { get; private set; }
    public int? GrossWeight { get; private set; }
    public DangerousGoodSpecifications? DangerousGoodSpecifications { get; private set; }
    public HashSet<LanguageMutation> TechnicalDescriptions { get; private set; }
    public string? AtaCode { get; private set; }

    private Good() {} // Parameterless constructor for EF Core
    
    private Good(string? packagingCode,
        string? combinedNomenclatureCode,
        string? description,
        int? quantity,
        int? grossWeight,
        DangerousGoodSpecifications? dangerousGoodSpecifications,
        HashSet<LanguageMutation> technicalDescriptions,
        string? ataCode)
    {
        PackagingCode = packagingCode;
        CombinedNomenclatureCode = combinedNomenclatureCode;
        Description = description;
        Quantity = quantity;
        GrossWeight = grossWeight;
        DangerousGoodSpecifications = dangerousGoodSpecifications;
        TechnicalDescriptions = technicalDescriptions;
        AtaCode  = ataCode;
    }

    public static Good Create(string? packagingCode, string? combinedNomenclatureCode, string? description, int? quantity, int? grossWeight, DangerousGoodSpecifications? dangerousGoodSpecifications, HashSet<LanguageMutation> technicalDescriptions,
        string? ataCode)
    {
        return new Good(packagingCode, combinedNomenclatureCode, description, quantity, grossWeight, dangerousGoodSpecifications, technicalDescriptions, ataCode);
    }
}