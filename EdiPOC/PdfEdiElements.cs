using System.Collections.Immutable;
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
        string ReceivingType);

    internal static IImmutableDictionary<string, Data> GetBodyData(EdiPOC.Edi.Domain.Edi edi)
    {
        var dictionary = new Dictionary<string, Data>
        {
            // --- Additional Info ---
            {
                "additionalInfo", new Data(
                    false,
                    "Der Fahrer muss sich mit PSA ausrüsten und alle für das Betreten der Be- und Entladezone   \n" +
                    "erforderlichen Regeln einhalten.  \n" +
                    "Bei Problemen/Rueckfragen oder Verzoegerungen bitte um umgehende Info zwecks Weiter\u0002\n" +
                    "leitung an unseren Kunden. Sonst die Extrakosten koennen wir leider nicht akzeptieren.  \n" +
                    "Unregelmässigkeiten sind unbedingt vor Verlassen des Terminals an den Customer Service und   \n" +
                    "das Terminal zu melden.  \n" +
                    "\n" +
                    "Vielen Dank,  \n" +
                    "J. Gablik  \n" +
                    "Tel.:  \n" +
                    "E-mail:mail@mmail.cz")
            },

            // --- Address To ---
            { "addressToCompany", new Data(false, "MHT") },
            { "addressToCountry", new Data(false, "DE 04808 WURZEN") },
            { "addressToStreet", new Data(false, "INDUSTRIESTRASSE 4-6") },

            // --- Notes ---
            {
                "notes", new Data(
                    false,
                    "MRKU 761461-6; SUDU 130506-6; MRKU 708775-2; TLLU 358577-0; TCLU 240554-1; MRKU 756575-9; MSKU 526132-1; MSKU 795322-6; ")
            },

            // --- Reservation ---
            { "reservationContainer", new Data(false, "MRKU 761461-6") },
            { "reservationContents", new Data(false, "Agricultural Machines") },
            { "reservationCustoms", new Data(false, "T1") },

            // --- Reservation: Dangerous Goods ---
            { "reservationDangerousGoodsClass", new Data(false, "9") },
            { "reservationDangerousGoodsDesc", new Data(false, "UMWELTGEFÄHRDENDER STOFF, FEST, N.A.G.") },
            { "reservationDangerousGoodsGroup", new Data(false, "III") },
            { "reservationDangerousGoodsNumber", new Data(false, "3077") },

            // --- Reservation Details ---
            { "reservationDeliveryDate", new Data(false, "29.05.24 - 07:30") },
            { "reservationPort", new Data(false, "JP-TOKYO") },
            { "reservationPortOfLoading", new Data(false, "HMBG") },
            { "reservationReeder", new Data(false, "MSC/459IHA1124865") },
            { "reservationRemarks", new Data(false, "DIRECT ZUM EMPF.") },
            { "reservationRefNumber", new Data(false, "RAUI08957001") },
            { "reservationSeal", new Data(false, "MLKR0449053") },
            { "reservationShip", new Data(false, "MADISON MAERSK") },
            { "reservationTerminalReturnDate", new Data(false, "29.05.24 bis 20:00") },
            { "reservationType", new Data(false, "40hc") },
            { "reservationWaste", new Data(false, "NEIN") },
            { "reservationWaybill", new Data(false, "LEJ2024683158") },
            { "reservationWeight", new Data(false, "12765") },

            // --- Transport & Warning Text ---
            { "transportText", new Data(false, "TRANSPORTAUFTRAG - IMPORT Nr.: FOUI07888") },
            { "warningText", new Data(false, "Aenderung") }
        };

        return dictionary.ToImmutableDictionary();
    }

    internal static IImmutableQueue<StopData> GetStopsData(EdiPOC.Edi.Domain.Edi edi)
    {
        var stops = FormatLocations(GetOrderedEdiLocations(edi));
        return ImmutableQueue.CreateRange(stops);
    }

    private static IOrderedEnumerable<EdiLocation> GetOrderedEdiLocations(EdiPOC.Edi.Domain.Edi edi)
    {
        List<EdiLocation> ediLocations =
        [
            //Abnahmeterminal
            new(
                nameof(LocationTypeEnum.Pickup),
                1,
                "Deutsche Umschlaggesellschaft Schiene–Straße (DUSS) mbH",
                "DE",
                "DE-04158",
                "Liepzig",
                "Hans-Grade-Str. 2",
                "Gate-in",
                "Delivery-text",
                "Gate-out"),

            //Empfänger
            new(
                nameof(LocationTypeEnum.Delivery),
                2,
                "EUNA HARZE GMBH",
                "DE",
                "DE 06237",
                "LEUNA",
                "AM HAUPTTOR -BAU 6619",
                "Gate-in",
                "Delivery-text",
                "Gate-out"),

            //Rücklieferung
            new(
                nameof(LocationTypeEnum.Dropoff),
                3,
                "DB Intermodel Services GmbH",
                "DE",
                "DE-04158",
                "Liepzig",
                "Am Exer 10",
                "Gate-in",
                "Delivery-text",
                "Gate-out")
        ];

        return ediLocations.OrderBy(x => x.SequenceNumber);
    }

    private static Queue<StopData> FormatLocations(IOrderedEnumerable<EdiLocation> ediLocations)
    {
        var result = new Queue<StopData>();
        foreach (var ediLocation in ediLocations)
            result.Enqueue(CreateStop(ediLocation));

        return result;
    }

    private static StopData CreateStop(EdiLocation ediLocation)
    {
        return new StopData(
            false,
            $"{ediLocation.PostalCode}",
            ediLocation.Company,
            ediLocation.Street,
            "idk-description",
            ediLocation.SequenceNumber.ToString(),
            "idk-recieving-type"
        );
    }
}