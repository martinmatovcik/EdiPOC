using MIS3.Libraries.MessageBus.Data.ContType;
using MIS3.Trucks.Common.Codebooks.Abstraction;
using MIS3.Trucks.Common.Helpers;
using Mis3.Trucks.Transport.De.Be.Api.Enum.Transport;

namespace EdiPOC.Codebook;

public sealed class ContainerType : CodebookEntity
{
    public string Code { get; private set; } = string.Empty;
    public bool IsTankCont { get; private set; }
    public int? LengthFt { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public string DescriptionCs { get; private set; } = string.Empty;
    public string DescriptionDe { get; private set; } = string.Empty;
    public string DescriptionEn { get; private set; } = string.Empty;
    public string DescriptionHu { get; private set; } = string.Empty;
    public string DescriptionPl { get; private set; } = string.Empty;
    public string DescriptionRo { get; private set; } = string.Empty;
    public string DescriptionSk { get; private set; } = string.Empty;
    public string DescriptionSl { get; private set; } = string.Empty;
    
    public void UpdateWithDto(ContTypeDto containerTypeDto)
    {
        CodebookId = containerTypeDto.Id;
        LastUpdatedFromEvent = NodaTimeHelpers.NowInstant();

        Code = containerTypeDto.ContainerType;
        IsTankCont = containerTypeDto.IsTankCont;
        LengthFt = (int?)containerTypeDto.LengthFt;
        Description = containerTypeDto.Description;
        DescriptionCs = containerTypeDto.DescriptionCs;
        DescriptionDe = containerTypeDto.DescriptionDe;
        DescriptionEn = containerTypeDto.DescriptionEn;
        DescriptionHu = containerTypeDto.DescriptionHu;
        DescriptionPl = containerTypeDto.DescriptionPl;
        DescriptionRo = containerTypeDto.DescriptionRo;
        DescriptionSk = containerTypeDto.DescriptionSk;
        DescriptionSl = containerTypeDto.DescriptionSl;
    }

    private bool IsLength40Ft() => LengthFt is 40;

    private bool IsLength20Ft() => LengthFt is 20;

    internal ServiceCategory GetServiceCategory(bool isAdr)
    {
        return isAdr switch
        {
            true when IsTankCont => ServiceCategory.ADR_TK,
            true => ServiceCategory.ADR_STD,
            _ => ServiceCategory.STD
        };

    }

    internal ServiceSubcategory GetServiceSubcategory()
    {
        if (IsLength40Ft()) return ServiceSubcategory.FT40;
        
        if (IsLength20Ft()) return ServiceSubcategory.FT20;
        
        return ServiceSubcategory.NONE;
    }
}