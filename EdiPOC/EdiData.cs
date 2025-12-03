using EdiPOC.Edi.Domain;
using EdiPOC.Edi.Formatter;

namespace EdiPOC;

internal static class EdiData
{
    internal static Edi.Domain.Edi Create(string cmrNumber = "cmr-number", string shippingCompany = "", bool addLocations = true, bool addDangerousGoods = true)
    {
        List<EdiLocation> locations =
        [
            new(
                nameof(LocationTypeEnum.Pickup),
                1,
                "locationCompany",
                "locationCountryIso",
                "locationPostalCode",
                "locationCity",
                "locationStreet",
                null,
                null,
                null),
            new(
                nameof(LocationTypeEnum.Dropoff),
                2,
                "locationCompany2",
                "locationCountryIso2",
                "locationPostalCode2",
                "locationCity2",
                "locationStreet2",
                null,
                null,
                null)
        ];

        List<EdiDangerousGood> dangerousGoods =
        [
            new(
                "dangerousGoodUnNumber",
                "dangerousGoodDescription",
                "dangerousGoodClass",
                "dangerousGoodLabel",
                "dangerousGoodPackingGroup",
                1,
                1,
                true),
            new(
                "dangerousGoodUnNumber2",
                "dangerousGoodDescription2",
                "dangerousGoodClass2",
                "dangerousGoodLabel2",
                "dangerousGoodPackingGroup2",
                2,
                2,
                false)
        ];

        return new Edi.Domain.Edi(
            EdiAction.New,
            cmrNumber,
            "reference-number",
            "container-type",
            "container-number",
            "release-reference",
            "goods-description",
            12345,
            "seals",
            "2025-09-26",
            "13:00",
            "customs-clearance",
            "customs-document-type",
            "note",
            "JA",
            false,
            addLocations ? locations : [],
            string.Empty,
            "destination-harbour",
            "ship-name",
            shippingCompany,
            new EdiContact("metransContact-name","metransContact-phoneNumber", "metransContact-email"),
            addDangerousGoods ? dangerousGoods : [],
            "order-number",
            false,
            "2025-09-26",
            "18:00",
            "containerNotes",
            new EdiCarrier("carrier-name", "carrier-street", "carrier-city", "carrier-postalCode", "carrier-country"));
    }

    internal static Edi.Domain.Edi CreateWithEmptyCollections(string cmrNumber = "cmr-number")
    {
        return new Edi.Domain.Edi(
            EdiAction.New,
            cmrNumber,
            "reference-number",
            "container-type",
            "container-number",
            "release-reference",
            "goods-description",
            12345,
            "seals",
            "2025-09-26",
            "13:00",
            "customs-clearance",
            "customs-document-type",
            "note",
            "NEIN",
            false,
            [],
            string.Empty,
            "destination-harbour",
            "ship-name",
            "owner-code",
            new EdiContact("metransContact-name", "metransContact-phoneNumber", "metransContact-email"),
            [],
            "order-number",
            false,
            "2025-09-26",
            "18:00",
            "containerNotes",
            new EdiCarrier("carrier-name", "carrier-street", "carrier-city", "carrier-postalCode", "carrier-country"));
    }

    internal static Edi.Domain.Edi CreateFormatted(Transport.Transport transport)
    {
        var formatter = EdiFormatter.CreateFormatterForNewEdi();
        return formatter.Format(transport);
    }

    internal static Edi.Domain.Edi CreateEdiWithRealData()
    {
        const string containerNumber = "MRKU 761461-6";
        const string note = $"{containerNumber}";

        return new Edi.Domain.Edi(
            EdiAction.New,
            "LEJ2024683158",
            "RAUI08957001",
            "40hc",
            containerNumber,
            "unimportant-release-reference",
            "Agricultural Machines",
            12765,
            "MLKR0449053",
            "29.05.2024",
            "07:30",
            "unimportant-customs-clearance",
            "T1",
            note,
            "NEIN",
            true,
            [
                //Abnahmeterminal
                new EdiLocation(
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
                new EdiLocation(
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
                new EdiLocation(
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
            ],
            "JP-TOKYO",
            "HMBG",
            "MADISON MAERSK",
            "MSC/459IHA1124865",
            new EdiContact("J. Gablik", "+420 123 456 789", "mail@mail.cz"),
            [
                new EdiDangerousGood(
                    "3077",
                    "UMWELTGEFÄHRDENDER STOFF, FEST, N.A.G.",
                    "9", "uninmportant-label",
                    "III",
                    -1,
                    -1,
                    true),
                new EdiDangerousGood(
                    "3076",
                    "UMWELTGEFÄHRDENDER STOFF, FEST, N.A.G.",
                    "9", "uninmportant-label",
                    "III",
                    -1,
                    -1,
                    true),
                new EdiDangerousGood(
                    "3075",
                    "UMWELTGEFÄHRDENDER STOFF, FEST, N.A.G.",
                    "9", "uninmportant-label",
                    "III",
                    -1,
                    -1,
                    true),
                new EdiDangerousGood(
                    "3074",
                    "UMWELTGEFÄHRDENDER STOFF, FEST, N.A.G.",
                    "9", "uninmportant-label",
                    "III",
                    -1,
                    -1,
                    true),
                new EdiDangerousGood(
                    "3074",
                    "UMWELTGEFÄHRDENDER STOFF, FEST, N.A.G.",
                    "9", "uninmportant-label",
                    "III",
                    -1,
                    -1,
                    true),
                new EdiDangerousGood(
                    "3074",
                    "UMWELTGEFÄHRDENDER STOFF, FEST, N.A.G.",
                    "9", "uninmportant-label",
                    "III",
                    -1,
                    -1,
                    true),
                
            ],
            "FOUI07888",
            false,
            "29.05.24",
            "20:00",
            "DIRECT ZUM EMPF.",
            new EdiCarrier("MHT", "INDUSTRIESTRASSE 4-6", "WURZEN", "04808", "DE")
        );
    }
}