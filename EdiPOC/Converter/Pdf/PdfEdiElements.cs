using System.Collections.Immutable;
using System.Text;
using EdiPOC.Edi.Domain;

namespace EdiPOC.Converter.Pdf;

internal class PdfEdiElements
{
    public readonly IImmutableDictionary<string, Data> BodyData;
    public readonly IImmutableQueue<StopData> StopsData;
    public readonly GoodsDatas GoodsData;
    
    private readonly Edi.Domain.Edi _current;
    private readonly Edi.Domain.Edi? _previous;

    public PdfEdiElements(Edi.Domain.Edi? previous, Edi.Domain.Edi current)
    {
        _current = current;
        _previous = previous;

        BodyData = GetBodyData();
        StopsData = GetStopsData();
        GoodsData = GetGoodsData();
    }

    internal record GoodsDatas(Data GoodsDescription, List<DangerousGoodData> DangerousGoods);

    internal record DangerousGoodData(Data Class, Data Description, Data PackingGroup, Data UnNumber);


    private ImmutableDictionary<string, Data> GetBodyData()
    {
        var dictionary = new Dictionary<string, Data>
        {
            // --- Additional Info ---
            {
                NoteOutsideBorder, Highlight(x => $"""
                                                   Der Fahrer muss sich mit PSA ausrüsten und alle für das Betreten der Be- und Entladezone
                                                   erforderlichen Regeln einhalten.
                                                   Bei Problemen/Rueckfragen oder Verzoegerungen bitte um umgehende Info zwecks Weiter
                                                   leitung an unseren Kunden. Sonst die Extrakosten koennen wir leider nicht akzeptieren.
                                                   Unregelmässigkeiten sind unbedingt vor Verlassen des Terminals an den Customer Service und 
                                                   das Terminal zu melden.

                                                   Vielen Dank,
                                                   {x.MetransContact.Name}
                                                   Tel.: {x.MetransContact.PhoneNumber}
                                                   E-mail: {x.MetransContact.EMail}
                                                   """)
            },

            // --- Address To ---
            { CarrierName, Highlight(x => x.Carrier.Name) },
            { CarrierCountry, Highlight(x => $"{x.Carrier.Country} {x.Carrier.PostalCode} {x.Carrier.City}") },
            { CarrierStreet, Highlight(x => $"{x.Carrier.Street}") },

            // --- Notes ---
            { Notes, Highlight(x => $"{x.Note};") },

            // --- Reservation ---
            { ContainerNumber, Highlight(x => x.ContainerNumber) },
            { CustomsDocumentType, Highlight(x => x.CustomsDocumentType) },

            // --- Reservation Details ---
            { DeliveryDateTime, Highlight(x => $"{x.DeliveryDate} - {x.DeliveryTime}") },
            { Harbour, Highlight(x => x.HarbourCode) },
            { HarbourOfLoading, Highlight(x => x.DestinationHarbour) },
            { ShippingCompany, Highlight(x => x.ShippingCompany) },
            { ContainerNotes, Highlight(x => x.ContainerNotes) },
            { ReferenceNumber, Highlight(x => x.ReferenceNumber) },
            { Seal, Highlight(x => x.Seals) },
            { ShipName, Highlight(x => x.ShipName) },

            // TODO: TerminalReturnDateTime (Hardcoded, no diff needed)
            { TerminalReturnDateTime, new Data(false, "29.05.24 bis 20:00") },

            { ContainerType, Highlight(x => x.ContainerType) },
            { IsWeighingRequest, Highlight(x => x.IsWeighingRequest) },
            { Weight, Highlight(x => x.GoodsWeight.ToString()) },
            { CmrNumber, Highlight(x => x.CmrNumber) },

            // --- Transport & Warning Text ---
            {
                OrderHeader, Highlight(x => $"TRANSPORTAUFTRAG - {FormatImportExport(x.IsImport)} Nr.: {x.OrderNumber}")
            },
            { WarningTextKey, HighlightWarningText() }
        };

        return dictionary.ToImmutableDictionary();
    }

    private Data Highlight(Func<Edi.Domain.Edi, string?> selector)
    {
        var currentValue = selector(_current);
        var previousValue = _previous != null ? selector(_previous) : null;
        return CreateDiffData(currentValue, previousValue);
    }

    private Data CreateDiffData(string? currentValue, string? previousValue)
    {
        var isChanged = _previous != null && !string.Equals(currentValue, previousValue, StringComparison.Ordinal);
        return new Data(isChanged, currentValue);
    }
    
    private Data HighlightWarningText()
    {
        //TODO: Doriesit EdiActionType.CANCEL
        var isChange = _current.ActionType == EdiActionType.CHANGE;
        var value = isChange ? WarningTextValue : null;
        return new Data(isChange, value);
    }

    internal ImmutableQueue<StopData> GetStopsData()
    {
        var result = new Queue<StopData>();

        var currentLocations = _current.Locations.OrderBy(x => x.SequenceNumber).ToList();
        var previousLocations = _previous?.Locations;

        foreach (var currentLocation in currentLocations)
        {
            var isHighlighted = false;
            if (previousLocations is null)
            {
                result.Enqueue(StopData.Create(isHighlighted, currentLocation));
                continue;
            }

            var previousLocation =
                previousLocations.FirstOrDefault(x => x.SequenceNumber == currentLocation.SequenceNumber);

            var stopExistedBefore = previousLocation != null;
            if (stopExistedBefore)
            {
                var currentStopData = StopData.Create(false, currentLocation);
                var prevStopData = StopData.Create(false, previousLocation!);

                var shouldHighlight = currentStopData != prevStopData;
                if (shouldHighlight)
                    isHighlighted = true;
            }
            else
            {
                isHighlighted = true;
            }

            result.Enqueue(StopData.Create(isHighlighted, currentLocation));
        }

        return ImmutableQueue.CreateRange(result);
    }

    internal GoodsDatas GetGoodsData()
    {
        var currentRaw = GetRawGoodsData(_current);
        var previousRaw = _previous != null ? GetRawGoodsData(_previous) : null;

        var descData = CreateDiffData(currentRaw.Description, previousRaw?.Description);

        var dangerousGoodsDataList = new List<DangerousGoodData>();
        for (var i = 0; i < currentRaw.Items.Count; i++)
        {
            var currentRawItem = currentRaw.Items[i];
            var previousRawItem = previousRaw != null && i < previousRaw.Items.Count ? previousRaw.Items[i] : null;

            dangerousGoodsDataList.Add(new DangerousGoodData(
                CreateDiffData(currentRawItem.Class, previousRawItem?.Class),
                CreateDiffData(currentRawItem.Description, previousRawItem?.Description),
                CreateDiffData(currentRawItem.PackingGroup, previousRawItem?.PackingGroup),
                CreateDiffData(currentRawItem.UnNumber, previousRawItem?.UnNumber)
            ));
        }

        return new GoodsDatas(descData, dangerousGoodsDataList);
    }

    private static RawGoodsResult GetRawGoodsData(Edi.Domain.Edi edi)
    {
        if (edi.DangerousGoods is not { Count: > 0 } dangerousGoods)
        {
            return new RawGoodsResult(edi.GoodsDescription, []);
        }

        const int maxDangerousGoodsInGefahrgutSection = 4;
        if (dangerousGoods.Count > maxDangerousGoodsInGefahrgutSection)
        {
            var formattedDescription = FormatDangerousGoodsForGoodDescriptionSection(dangerousGoods);
            return new RawGoodsResult(formattedDescription, []);
        }

        var items = dangerousGoods.Select(dg => new RawDangerousGood(
            dg.Class,
            dg.Description,
            dg.PackingGroup,
            dg.UnNumber
        )).ToList();

        return new RawGoodsResult(edi.GoodsDescription, items);
    }

    private static string FormatDangerousGoodsForGoodDescriptionSection(IEnumerable<EdiDangerousGood> dangerousGoods)
    {
        var sb = new StringBuilder();
        foreach (var dangerousGood in dangerousGoods)
        {
            sb.AppendLine(
                $"UN:{dangerousGood.UnNumber} {dangerousGood.Class} {dangerousGood.PackingGroup} {dangerousGood.Description}");
        }

        return sb.ToString();
    }

    internal const string WarningTextValue = "Aenderung";
    
    // --- Dictionary keys ---
    internal const string NoteOutsideBorder = "noteOutsideBorder";
    internal const string CarrierName = "carrierName";
    internal const string CarrierCountry = "carrierCountry";
    internal const string CarrierStreet = "carrierStreet";
    internal const string Notes = "notes";
    internal const string ContainerNumber = "containerNumber";
    internal const string CustomsDocumentType = "customsDocumentType";
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
    internal const string OrderHeader = "orderHeaderText";
    internal const string WarningTextKey = "warningText";

    // --- Private Implementation Details ---

    private static string FormatImportExport(bool isImport) => isImport ? "IMPORT" : "EXPORT";

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
        internal static StopData Create(bool isHighlighted, EdiLocation ediLocation)
        {
            return new StopData(
                isHighlighted,
                $"{ediLocation.PostalCode}",
                ediLocation.Company,
                ediLocation.Street,
                "TODO: gate-reference",
                ediLocation.SequenceNumber.ToString(),
                CreateStopName(ediLocation.Type)
            );
        }

        private static string CreateStopName(EdiLocationType locationType) => locationType switch
        {
            EdiLocationType.Pickup => "Abnahmeterminal",
            EdiLocationType.Customs => "Verzollung",
            EdiLocationType.Declaration => "Deklaration",
            EdiLocationType.Delivery => "Absender",
            EdiLocationType.Dropoff => "Rücklieferung",
            _ => throw new ArgumentOutOfRangeException(nameof(locationType), locationType, null)
        };
    }

    private record RawDangerousGood(string Class, string Description, string PackingGroup, string UnNumber);

    private record RawGoodsResult(string Description, List<RawDangerousGood> Items);
}