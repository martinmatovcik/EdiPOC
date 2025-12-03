using MIS3.Libraries.MessageBus.Data.BusinessPartner;
using MIS3.Trucks.Common.Codebooks.Abstraction;
using MIS3.Trucks.Common.Helpers;

namespace EdiPOC.Codebook;

public class BusinessPartner : CodebookEntity
{
    public string? Name { get; private set; }
    public string SapNr { get; private set; }

    private BusinessPartner() { }

    public static BusinessPartner CreateWithDto(BusinessPartnerDto businessPartnerDto)
        => new BusinessPartner
        {
            CodebookId = businessPartnerDto.Id,
            LastUpdatedFromEvent = NodaTimeHelpers.NowInstant(),
            Name = businessPartnerDto.Name,
            SapNr = businessPartnerDto.SapNr,
        };

    public void UpdateWithDto(BusinessPartnerDto businessPartnerDto)
    {
        CodebookId = businessPartnerDto.Id;
        LastUpdatedFromEvent = NodaTimeHelpers.NowInstant();
        Name = businessPartnerDto.Name;
        SapNr = businessPartnerDto.SapNr;
    }
}