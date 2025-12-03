using MIS3.Libraries.MessageBus.Data.ServiceTrade;
using MIS3.Trucks.Common.Codebooks.Abstraction;
using MIS3.Trucks.Common.Helpers;

namespace EdiPOC.Codebook;

public class ServiceTrade : CodebookEntity
{
    public string Code { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string DescriptionCs { get; private set; } = string.Empty;
    public string DescriptionDe { get; private set; } = string.Empty;
    public string DescriptionEn { get; private set; } = string.Empty;
    public string DescriptionHu { get; private set; } = string.Empty;
    public string DescriptionPl { get; private set; } = string.Empty;
    public string DescriptionRo { get; private set; } = string.Empty;
    public string DescriptionSk { get; private set; } = string.Empty;
    public string DescriptionSl { get; private set; } = string.Empty;
    
    public void UpdateWithDto(ServiceTradeDto serviceTradeDto)
    {
        CodebookId = serviceTradeDto.Id;
        LastUpdatedFromEvent = NodaTimeHelpers.NowInstant();

        Code = serviceTradeDto.Code;
        Description = serviceTradeDto.Description;
        DescriptionCs = serviceTradeDto.DescriptionCs;
        DescriptionDe = serviceTradeDto.DescriptionDe;
        DescriptionEn = serviceTradeDto.DescriptionEn;
        DescriptionHu = serviceTradeDto.DescriptionHu;
        DescriptionPl = serviceTradeDto.DescriptionPl;
        DescriptionRo = serviceTradeDto.DescriptionRo;
        DescriptionSk = serviceTradeDto.DescriptionSk;
        DescriptionSl = serviceTradeDto.DescriptionSl;
    }
}