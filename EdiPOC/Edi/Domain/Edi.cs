using MIS3.Trucks.Common.Extensions;

namespace EdiPOC.Edi.Domain;

/// <summary>
/// Represents an entity which holds all necessary attributes which will be used for generating EDI (XML/PDF) files.
/// Those files will be sent to carriers notfying them about new transports or changes to existing ones.
/// </summary>
public sealed class Edi
{

    #region Constructors

    public Edi(
        Guid transportId,
        EdiActionType actionType,
        string cmrNumber,
        string referenceNumber,
        string containerType,
        string containerNumber,
        string? releaseReference,
        string goodsDescription,
        int goodsWeight,
        string? seals,
        string deliveryDate,
        string deliveryTime,
        string? customsClearance,
        string? customsDocumentType,
        string note,
        string? isWeighingRequest,
        bool isImport,
        List<EdiLocation> locations,
        string? harbourCode,
        string? destinationHarbour,
        string? shipName,
        string shippingCompany,
        EdiContact metransContact,
        List<EdiDangerousGood>? dangerousGoods,
        string orderNumber,
        bool isWaste,
        string terminalReturnDate,
        string terminalReturnTime,
        string containerNotes,
        EdiCarrier carrier)
    {
        TransportId = transportId;
        ActionType = actionType;

        _cmrNumber = cmrNumber;
        _referenceNumber = referenceNumber;
        _containerType = containerType;
        _containerNumber = containerNumber;
        _releaseReference = releaseReference;
        _goodsDescription = goodsDescription;
        _seals = seals;
        _deliveryDate = deliveryDate;
        _deliveryTime = deliveryTime;
        _customsClearance = customsClearance;
        _customsDocumentType = customsDocumentType;
        _note = note;
        _isWeighingRequest = isWeighingRequest;
        Locations = locations;
        _harbourCode = harbourCode;
        _destinationHarbour = destinationHarbour;
        _shipName = shipName;
        _shippingCompany = shippingCompany;
        MetransContact = metransContact;

        if (dangerousGoods.IsNotNullOrEmpty())
        {
            _dangerousGoodsSpecification = null;
            DangerousGoods = dangerousGoods;
        }
        else
        {
            _dangerousGoodsSpecification = ".";
            DangerousGoods = null;
        }

        IsWaste = isWaste;
        _orderNumber = orderNumber;
        _terminalReturnDate = terminalReturnDate;
        _terminalReturnTime = terminalReturnTime;
        _containerNotes = containerNotes;
        GoodsWeight = goodsWeight;
        IsImport = isImport;
        ImportExportValue = isImport ? 1 : 0;
        Carrier = carrier;
    }

    #endregion

    #region Properties

    public Guid TransportId { get; private set; }
    public EdiActionType ActionType { get; private set; }

    private string _cmrNumber;
    private string _referenceNumber;
    private string _containerType;
    private string _containerNumber;
    private string? _releaseReference;
    private string _goodsDescription;
    private string? _seals;
    private string _deliveryDate;
    private string _deliveryTime;
    private string? _customsClearance;
    private string? _customsDocumentType;
    private string _note;
    private string? _isWeighingRequest;
    private string? _harbourCode;
    private string? _destinationHarbour;
    private string? _shipName;
    private string _shippingCompany;
    private string? _dangerousGoodsSpecification;
    private string _orderNumber;
    private string _terminalReturnDate;
    private string _terminalReturnTime;
    private string _containerNotes;

    public string CmrNumber
    {
        get => _cmrNumber;
        private set => _cmrNumber = value.RemoveControlCharacters() ?? string.Empty;
    }

    public const string VendorId = "CZPRHMETA";

    public string ReferenceNumber
    {
        get => _referenceNumber;
        private set => _referenceNumber = value.RemoveControlCharacters() ?? string.Empty;
    }

    public string ContainerType
    {
        get => _containerType;
        private set => _containerType = value.RemoveControlCharacters() ?? string.Empty;
    }

    public string ContainerNumber
    {
        get => _containerNumber;
        private set => _containerNumber = value.RemoveControlCharacters() ?? string.Empty;
    }

    public string? ReleaseReference
    {
        get => _releaseReference;
        private set => _releaseReference = value.RemoveControlCharacters();
    }

    public string GoodsDescription
    {
        get => _goodsDescription;
        private set => _goodsDescription = value.RemoveControlCharacters() ?? string.Empty;
    }

    public int GoodsWeight { get; private set; }

    public string? Seals
    {
        get => _seals;
        private set => _seals = value.RemoveControlCharacters();
    }

    public string DeliveryDate
    {
        get => _deliveryDate;
        private set => _deliveryDate = value.RemoveControlCharacters() ?? string.Empty;
    }

    public string DeliveryTime
    {
        get => _deliveryTime;
        private set => _deliveryTime = value.RemoveControlCharacters() ?? string.Empty;
    }

    public string? CustomsClearance
    {
        get => _customsClearance;
        private set => _customsClearance = value.RemoveControlCharacters();
    }

    public string? CustomsDocumentType
    {
        get => _customsDocumentType;
        private set => _customsDocumentType = value.RemoveControlCharacters();
    }

    public string Note
    {
        get => _note;
        private set => _note = value.RemoveControlCharacters() ?? string.Empty;
    }

    public string? IsWeighingRequest
    {
        get => _isWeighingRequest;
        private set => _isWeighingRequest = value.RemoveControlCharacters();
    }

    public bool IsImport { get; private set; }
    public int ImportExportValue { get; private set; }

    public List<EdiLocation> Locations { get; private set; }

    public string? HarbourCode
    {
        get => _harbourCode;
        private set => _harbourCode = value.RemoveControlCharacters();
    }

    public string? DestinationHarbour
    {
        get => _destinationHarbour;
        private set => _destinationHarbour = value.RemoveControlCharacters();
    }

    public string? ShipName
    {
        get => _shipName;
        private set => _shipName = value.RemoveControlCharacters();
    }

    public string ShippingCompany
    {
        get => _shippingCompany;
        private set => _shippingCompany = value.RemoveControlCharacters() ?? string.Empty;
    }

    public EdiContact MetransContact { get; private set; }

    public string? DangerousGoodsSpecification
    {
        get => _dangerousGoodsSpecification;
        private set => _dangerousGoodsSpecification = value.RemoveControlCharacters();
    }
    
    public List<EdiDangerousGood>? DangerousGoods { get; private set; }

    #endregion

    #region Pdf Specific properties
    
    public bool IsWaste { get; private set; }

    public string OrderNumber
    {
        get => _orderNumber;
        private set => _orderNumber = value.RemoveControlCharacters() ?? string.Empty;
    } //TODO: Atribut "BuchNr. Ref." #Gablik

    public string TerminalReturnDate
    {
        get => _terminalReturnDate;
        private set => _terminalReturnDate = value.RemoveControlCharacters() ?? string.Empty;
    } //TODO: Atribut "Terminal Ruckgabe" #Gablik - je tam nejaky vzorec na vypocet?

    public string TerminalReturnTime
    {
        get => _terminalReturnTime;
        private set => _terminalReturnTime = value.RemoveControlCharacters() ?? string.Empty;
    } //TODO: Atribut "Terminal Ruckgabe" #Gablik - je tam nejaky vzorec na vypocet?

    public string ContainerNotes
    {
        get => _containerNotes;
        private set => _containerNotes = value.RemoveControlCharacters() ?? string.Empty;
    } //TODO: Atribut "Bemerkungen" pri kontajnere #Gablik

    public EdiCarrier Carrier { get; private set; }
    
    #endregion

    #region Internal methods
    
    internal void SetImportExportValue(int value)
    {
        if (value is < 0 or > 2) throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and 2");

        ImportExportValue = value;
    }
    
    internal void RemoveContainerTypePrefix() => ContainerType = ContainerType.Replace("1/", string.Empty, StringComparison.OrdinalIgnoreCase);
    internal void SetContainerNumber(string value) => ContainerNumber = value;
    internal void SetReleaseReference(string? value) => ReleaseReference = value;
    internal void SetSeals(List<string> seals) => Seals = string.Join(';', seals);
    internal void AppendNote(string value) => Note = string.IsNullOrEmpty(Note) ? value : Note + " " + value;
    internal void SetShipName(string? value) => ShipName = value;
    internal void SetCustomsClearance(string? value) => CustomsClearance = value;
    internal void SetCustomsDocumentType(string? value) => CustomsDocumentType = value;
    internal void SetDestinationHarbour(string? destinationHarbourName, string? destinationHarbourCountryCode) => DestinationHarbour = $"{destinationHarbourCountryCode}-{destinationHarbourName}";
    internal void SetShippingCompany(string value) => ShippingCompany = value;
    internal void SetReferenceNumber(string value) => ReferenceNumber = value;
    internal void SetWeighingRequest(string? value) => IsWeighingRequest = value;
    internal void SetHarbourCode(string? value) => HarbourCode = value;

    internal void SetGateInReference(string value)
    {
        var location = GetDropoffLocation();
        if (location is null) return;

        location.GateInReference = value;
    }

    internal void SetGateOutReference(string value)
    {
        var location = GetPickupLocation();
        if (location is null) return;

        location.GateOutReference = value;
    }

    internal EdiLocation? GetPickupLocation() => Locations.SingleOrDefault(x => x.IsPickupLocation());
    internal EdiLocation? GetDropoffLocation() => Locations.SingleOrDefault(x => x.IsDropoffLocation());
    
    #endregion
    
}