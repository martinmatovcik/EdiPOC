using EdiPOC.Codebook;
using Mis3.Trucks.Transport.De.Be.Api.Enum.Transport;

namespace EdiPOC.Transport.Services;

public sealed record Services
{
    public bool IsCompressor { get; private set; }
    public bool IsWaste { get; private set; }
    public ServiceCategory ServiceCategory { get; private set; } = ServiceCategory.UNDEFINED;
    public ServiceSubcategory ServiceSubcategory { get; private set; } = ServiceSubcategory.UNDEFINED;
    public List<string>? ServiceTradeCodes { get; private set; } = [];
    public string? ChassisType { get; private set; }
    public bool IsSailSeal { get; private set; }
    
    private const string CompressorT12 = "T012";
    private const string CompressorT13 = "T013";

    private Services()
    {
    }

    private Services(bool isCompressor, bool isWaste, ServiceCategory serviceCategory, ServiceSubcategory serviceSubcategory, List<string> serviceTradeCodes, string? chassisType, bool isSailSeal)
    {
        IsCompressor = isCompressor;
        IsWaste = isWaste;
        ServiceCategory = serviceCategory;
        ServiceSubcategory = serviceSubcategory;
        ServiceTradeCodes = serviceTradeCodes;
        ChassisType = chassisType;
        IsSailSeal = isSailSeal;
    }

    public static Services Create(bool isCompressor, bool isWaste, ServiceCategory serviceCategory, ServiceSubcategory serviceSubcategory, List<string> serviceTradeCodes, string? chassisType, bool isSailSeal) =>
        new(isCompressor, isWaste, serviceCategory, serviceSubcategory, serviceTradeCodes, chassisType, isSailSeal);

    public static Services Create(bool isWaste, List<string> serviceTradeCodes, ContainerType containerType, bool isAdr, string? chassisType, bool isSailSeal) =>
        new(IsCompressorByServiceCodes(serviceTradeCodes), isWaste, containerType.GetServiceCategory(isAdr), containerType.GetServiceSubcategory(), serviceTradeCodes, chassisType, isSailSeal);

    private static bool IsCompressorByServiceCodes(List<string> serviceTradeCodes) => serviceTradeCodes.Contains(CompressorT12) || serviceTradeCodes.Contains(CompressorT13);
    
    internal bool IsChassisTypeSklap() => ChassisType == "Ch.Sklap";
    internal bool IsChassisTypeKlaus() => ChassisType == "Ch.Klaus";
}