using System.Collections.Immutable;
using System.Text;
using EdiPOC.Edi.Domain;

namespace EdiPOC;

internal static class PdfEdiElements
{
    internal record Data(bool IsHighlighted, string? Value);

    internal record StopData(
        bool IsHighlighted,
        string AddressCountry,
        string AddressName,
        string AddressStreet,
        string Description,
        string Number,
        string StopName)
    {
        internal static StopData Create(EdiLocation ediLocation)
        {
            return new StopData(
                false,
                $"{ediLocation.PostalCode}",
                ediLocation.Company,
                ediLocation.Street,
                "idk-description",
                ediLocation.SequenceNumber.ToString(),
                ediLocation.Type
            );
        }
    }

    internal record GoodsData(Data GoodsDescription, List<DangerousGoodData> DangerousGoods);

    internal record DangerousGoodData(Data Class, Data Description, Data PackingGroup, Data UnNumber);

    internal static IImmutableDictionary<string, Data> GetBodyData(Edi.Domain.Edi edi)
    {
        var dictionary = new Dictionary<string, Data>
        {
            // --- Additional Info ---
            // TODO: Nechat len podpis
            {
                NoteOutsideBorder, new Data(
                    false,
                    $"""
                     Der Fahrer muss sich mit PSA ausrüsten und alle für das Betreten der Be- und Entladezone
                     erforderlichen Regeln einhalten.
                     Bei Problemen/Rueckfragen oder Verzoegerungen bitte um umgehende Info zwecks Weiter
                     leitung an unseren Kunden. Sonst die Extrakosten koennen wir leider nicht akzeptieren.
                     Unregelmässigkeiten sind unbedingt vor Verlassen des Terminals an den Customer Service und 
                     das Terminal zu melden.

                     Vielen Dank,
                     {edi.MetransContact.Name}
                     Tel.: {edi.MetransContact.PhoneNumber}
                     E-mail: {edi.MetransContact.EMail}
                     """)
            },

            // --- Address To ---
            { CarrierName, new Data(false, edi.Carrier.Name) },
            { CarrierCountry, new Data(false, $"{edi.Carrier.Country} {edi.Carrier.PostalCode} {edi.Carrier.City}") },
            { CarrierStreet, new Data(false, $"{edi.Carrier.Street}") },

            // --- Notes ---
            { Notes, new Data(false, $"{edi.Note};") },

            // --- Reservation ---
            { ContainerNumber, new Data(false, edi.ContainerNumber) },
            { CustomsDocumentType, new Data(false, edi.CustomsDocumentType) },

            // --- Reservation Details ---
            { DeliveryDateTime, new Data(false, $"{edi.DeliveryDate} - {edi.DeliveryTime}") },
            { Harbour, new Data(false, edi.HarbourCode) },
            { HarbourOfLoading, new Data(false, edi.DestinationHarbour) },
            { ShippingCompany, new Data(false, edi.ShippingCompany) },
            { ContainerNotes, new Data(false, edi.ContainerNotes) },
            { ReferenceNumber, new Data(false, edi.ReferenceNumber) },
            { Seal, new Data(false, edi.Seals) },
            { ShipName, new Data(false, edi.ShipName) },

            //TODO: TerminalReturnDateTime
            { TerminalReturnDateTime, new Data(false, "29.05.24 bis 20:00") },

            { ContainerType, new Data(false, edi.ContainerType) },
            { IsWeighingRequest, new Data(false, edi.IsWeighingRequest) },
            { Weight, new Data(false, edi.GoodsWeight.ToString()) },
            { CmrNumber, new Data(false, edi.CmrNumber) },

            // --- Transport & Warning Text ---
            { OrderHeader, new Data(false, $"TRANSPORTAUFTRAG - {FormatImportExport(edi.IsImport)} Nr.: {edi.OrderNumber}") },
            { WarningText, new Data(false, "Aenderung") }
        };

        return dictionary.ToImmutableDictionary();
    }

    internal static IImmutableQueue<StopData> GetStopsData(Edi.Domain.Edi edi)
    {
        var stops = FormatLocations(edi.Locations.OrderBy(x => x.SequenceNumber));
        return ImmutableQueue.CreateRange(stops);
    }

    private static Queue<StopData> FormatLocations(IOrderedEnumerable<EdiLocation> ediLocations)
    {
        var result = new Queue<StopData>();
        foreach (var ediLocation in ediLocations)
            result.Enqueue(StopData.Create(ediLocation));

        return result;
    }

    private static string FormatImportExport(bool isImport) => isImport ? "IMPORT" : "EXPORT";

    internal static GoodsData GetGoodsData(Edi.Domain.Edi edi)
    {
        if (edi.DangerousGoods is not { Count: > 0 } dangerousGoods)
        {
            return new GoodsData(new Data(false, edi.GoodsDescription), []);
        }

        const int maxDangerousGoodsInGefahrgutSection = 4;
        if (dangerousGoods.Count > maxDangerousGoodsInGefahrgutSection)
        {
            var formattedDescription = FormatDangerousGoodsForGoodDescriptionSection(dangerousGoods);
            return new GoodsData(new Data(false, formattedDescription), []);
        }

        var detailedItems = dangerousGoods
            .Select(dg => new DangerousGoodData(
                new Data(false, dg.Class),
                new Data(false, dg.Description),
                new Data(false, dg.PackingGroup),
                new Data(false, dg.UnNumber)))
            .ToList();

        return new GoodsData(new Data(false, edi.GoodsDescription), detailedItems);
    }

    private static string FormatDangerousGoodsForGoodDescriptionSection(IEnumerable<EdiDangerousGood> dangerousGoods)
    {
        var sb = new StringBuilder();

        var isFirst = true;
        foreach (var dangerousGood in dangerousGoods)
        {
            var value =
                $"UN:{dangerousGood.UnNumber} {dangerousGood.Class} {dangerousGood.PackingGroup} {dangerousGood.Description}";

            if (isFirst)
            {
                sb.Append(value);
                isFirst = false;
            }
            else
            {
                sb.AppendLine(value);
            }
        }

        return sb.ToString();
    }

    #region Dictionary keys

    // --- Dictionary keys ----
    internal const string NoteOutsideBorder = "noteOutsideBorder";

    // --- Carrier ---
    internal const string CarrierName = "carrierName";
    internal const string CarrierCountry = "carrierCountry";
    internal const string CarrierStreet = "carrierStreet";

    // --- Notes ---
    internal const string Notes = "notes";

    // --- Reservation ---
    internal const string ContainerNumber = "containerNumber";
    internal const string CustomsDocumentType = "customsDocumentType";

    // --- Reservation Details ---
    internal const string DeliveryDateTime = "deliveryDateTime";
    internal const string Harbour = "harbour";
    internal const string HarbourOfLoading = "harbourOfLoading";
    internal const string ShippingCompany = "shippingCompany";
    internal const string ContainerNotes = "containerNotes";
    internal const string ReferenceNumber = "referenceNumber";
    internal const string Seal = "seal";
    internal const string ShipName = "shipName";
    internal const string TerminalReturnDateTime = "terminalReturnDateTime";
    internal const string ContainerType = "type";
    internal const string IsWeighingRequest = "isWeighingRequest";
    internal const string Weight = "weight";
    internal const string CmrNumber = "cmrNumber";

    // --- Transport & Warning ---
    internal const string OrderHeader = "orderHeaderText";
    internal const string WarningText = "warningText";

    #endregion
}