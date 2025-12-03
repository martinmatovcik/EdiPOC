using EdiPOC.Edi.Domain;
using EdiPOC.Transport;
using EdiPOC.Transport.Goods;
using EdiPOC.Transport.LocationChain;
using MIS3.Trucks.Common.Helpers;
using Mis3.Trucks.Transport.De.Be.Api.Enum.Common;

namespace EdiPOC.Edi.Formatter;

public sealed class EdiFormatter : IEdiFormatter
{
    private string EdiAction { get; }

    public EdiFormatter() => EdiAction = Domain.EdiAction.New;
    private EdiFormatter(string ediAction) => EdiAction = ediAction;
    public static EdiFormatter CreateFormatterForNewEdi() => new(Domain.EdiAction.New);
    public static EdiFormatter CreateFormatterForChangeEdi() => new(Domain.EdiAction.Change);
    public static EdiFormatter CreateFormatterForCancelEdi() => new(Domain.EdiAction.Cancel);

    public Domain.Edi Format(Transport.Transport transport)
    {
        var cmr = transport.GetActiveCmr();
        if (cmr is null) 
            throw new InvalidOperationException("Cannot create new EDI without an active CMR.");
        
        return new Domain.Edi(
            EdiAction,
            cmr.CmrNumber,
            $"{transport.OrderDetails.OrderNumber}_{transport.OrderDetails.OrderItemSequenceNumber}",
            $"1/{transport.Container.ContainerType.Code}",
            GetContainerNumberForEdi(transport),
            null,
            transport.GoodsDetails.GetMergedDescription(),
            transport.GoodsDetails.GrossWeightTotal ?? 0,
            null,
            transport.DeliveryDate.ToFormattedString("dd.MM.yyyy")!,
            transport.DeliveryTime.ToFormattedString("HH:mm")!,
            transport.LocationChain.Customs?.BuildAddress(),
            null,
            transport.Notes.Metrans ?? string.Empty,
            null,
            transport.TransportType == TransportType.IMPORT,
            FormatLocations(transport.LocationChain),
            null,
            null,
            transport.ShipDetails.ShipName,
            transport.ShipDetails.ShippingCompany ?? string.Empty,
            FormatContact(transport.OrderDetails.MetransContact),
            FormatDangerousGoods(transport.GoodsDetails),
            transport.OrderDetails.OrderNumber,
            transport.Services.IsWaste,
            "terminalReturnDate - doplnit",      //TODO: PDF samostatny ticket --> https://metrans.atlassian.net/browse/TRUC-3998
            "terminalReturnTime - doplnit",      //TODO: PDF samostatny ticket --> https://metrans.atlassian.net/browse/TRUC-3998
            "containerNotes - doplnit",             //TODO: PDF samostatny ticket --> https://metrans.atlassian.net/browse/TRUC-3998
            new EdiCarrier(transport.Carrier.Name, transport.Carrier.Location.Street, transport.Carrier.Location.City, transport.Carrier.Location.PostalCode, transport.Carrier.Location.CountryIso));
    }

    private static string GetContainerNumberForEdi(Transport.Transport transport) => 
        transport.IsContainerNumberFictional() 
            ? transport.Container.EnvelopeNumber 
            : transport.Container.ContainerNumber;

    private static List<EdiLocation> FormatLocations(LocationChain locationChain)
    {
        List<EdiLocation?> ediLocations =
        [
            FormatLocation(locationChain.First, nameof(LocationTypeEnum.Pickup)), //pridat hardcoded PickUp - enum/static 
            FormatLocation(locationChain.Customs, nameof(LocationTypeEnum.Customs)),
            FormatLocation(locationChain.Declaration, nameof(LocationTypeEnum.Delivery)), // TODO declaration -> delivery?
            FormatLocation(locationChain.Consignee, nameof(LocationTypeEnum.Delivery)), //TODO: #Gablik - priadavame do XML aj adresu prijemce/odesilatele?
            FormatLocation(locationChain.Last, nameof(LocationTypeEnum.Dropoff)), 
        ];
        ediLocations.AddRange(locationChain.ImportExport
            .Select(item => FormatLocation(item, nameof(LocationTypeEnum.Delivery)))); // TODO workaround & why override previous ediLocations assignment?
        /*ediLocations.AddRange(locationChain.ImportExport
            .Select(item => FormatLocation(item, nameof(LocationTypeEnum.Delivery))));*/
        return ediLocations.Where(x => x != null).ToList()!;
    }
    
    private static EdiLocation? FormatLocation(LocationItem? locationItem, string locationType)
    {
        if (locationItem is null) return null;

        return new EdiLocation(
            locationType,
            locationItem.ChainSequence ?? -9999,
            locationItem.Name,
            locationItem.CountryIso,
            locationItem.PostalCode,
            locationItem.City,
            locationItem.Street,
            null,
            null,
            null);
    }
    
    private static EdiContact FormatContact(Contact contact) =>
        new(contact.Name ?? "MISSING METRANS CONTACT NAME",
            contact.PhoneNumber ?? "MISSING METRANS CONTACT PHONE-NUMBER",
            contact.Email?? "MISSING METRANS CONTACT EMAIL");
    
    private static List<EdiDangerousGood>? FormatDangerousGoods(GoodsDetails goodsDetails) =>
        !goodsDetails.HasDangerousGoods() ? null : goodsDetails.Goods.Select(FormatDangerousGood).ToList();

    private static EdiDangerousGood FormatDangerousGood(Good good)
    {
        var specifications = good.DangerousGoodSpecifications;

        if (specifications is null)
            throw new InvalidOperationException("Cannot format dangerous good without specifications.");
        
        return new EdiDangerousGood(
            specifications.UnNumber,
            specifications.Information.FirstOrDefault(x => x.IsGerman())?.Value,
            specifications.Class,
            specifications.Label,
            specifications.PackingGroup ?? "MISSING PACKING GROUP",
            good.GrossWeight ?? 0,
            specifications.ExplosiveGrossWeight,
            specifications.IsLimitedQuantity);
    }
}