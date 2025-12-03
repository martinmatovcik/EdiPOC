using System.Data;
using EdiPOC.Carriers;
using EdiPOC.Codebook;
using EdiPOC.Transport.Customs;
using EdiPOC.Transport.Goods;
using EdiPOC.Transport.History;
using EdiPOC.Transport.LocationChain;
using EdiPOC.Transport.Price;
using EdiPOC.Transport.Trains;
using MIS3.Trucks.Common.Domain.Entity;
using MIS3.Trucks.Common.Helpers;
using Mis3.Trucks.Transport.De.Be.Api.Enum.Carrier;
using Mis3.Trucks.Transport.De.Be.Api.Enum.Cmr;
using Mis3.Trucks.Transport.De.Be.Api.Enum.Common;
using Mis3.Trucks.Transport.De.Be.Api.Enum.Transport;
using Mis3.Trucks.Transport.De.Be.Api.Events.Private.Classic.Get.ForCapacities;
using NodaTime;

namespace EdiPOC.Transport;

public sealed class Transport : Entity, ILastUpdate
{
    public long IdMode { get; init; } = -1;

    public AssignedType AssignedType { get; private set; } = AssignedType.UNDEFINED;

    public LocalDate? DeliveryDate { get; private set; }
    
    public LocalTime? DeliveryTime { get; private set; }
    
    public TransportStatus Status { get; private set; } = TransportStatus.UNDEFINED;

    public List<TransportHistory> History { get; } = [];

    public TransportType TransportType { get; private set; } = TransportType.UNDEFINED;

    public string TerminalCode { get; private set; } = "UNDEFINED_TERMINAL_CODE";

    public LocationChain.LocationChain LocationChain { get; private set; }

    public Carrier? Carrier { get; private set; }

    public Guid? CarrierId { get; private set; }
    
    public AutomatonState? AutomatonState { get; private set; }
    
    public string? MrnNumber { get; private set; }
    
    public Container Container { get; private set; }

    public Services.Services Services { get; private set; }

    public List<ServiceTrade> ServiceTrades { get; private set; } = [];
    
    public Notes.Notes Notes { get; private set; }

    public OrderDetails OrderDetails { get; private set; }

    public Trains.Trains Trains { get; private set; }

    public GoodsDetails GoodsDetails { get; private set; }

    public List<Cmr.Cmr> Cmrs { get; } = [];

    public Instant LastUpdatedFromEvent { get; set; } = NodaTimeHelpers.NowInstant();
    
    public PriceDetails PriceDetails { get; private set; }
    
    public BusinessPartner? BusinessPartner { get; private set; }

    public CustomsDetails CustomsDetails { get; private set; }
    
    public HarbourDetails HarbourDetails { get; private set; }

    public ShipDetails ShipDetails { get; private set; }
    
    public bool IsDrayage { get; private set; }

    private Transport() // Parameterless constructor for EF Core
    {
    }

    private Transport(
        long idMode,
        AssignedType assignedType,
        LocalDate? deliveryDate,
        LocalTime? deliveryTime,
        TransportStatus status,
        TransportType transportType,
        string terminalCode,
        LocationChain.LocationChain locationChain,
        string? mrnNumber,
        Container container,
        Services.Services services,
        List<ServiceTrade> serviceTrades,
        Notes.Notes notes,
        OrderDetails orderDetails,
        Trains.Trains trains,
        GoodsDetails goodsDetails,
        PriceDetails priceDetails,
        BusinessPartner businessPartner,
        CustomsDetails customsDetails,
        HarbourDetails harbourDetails,
        ShipDetails shipDetails,
        bool isDrayage)
    {
        IdMode = idMode;
        AssignedType = assignedType;
        DeliveryDate = deliveryDate;
        DeliveryTime = deliveryTime;
        Status = status;
        TransportType = transportType;
        TerminalCode = terminalCode;
        LocationChain = locationChain;
        MrnNumber = mrnNumber;
        Container = container;
        Services = services;
        ServiceTrades = serviceTrades;
        Notes = notes;
        OrderDetails = orderDetails;
        Trains = trains;
        GoodsDetails = goodsDetails;
        PriceDetails = priceDetails;
        BusinessPartner = businessPartner;
        CustomsDetails = customsDetails;
        HarbourDetails = harbourDetails;
        ShipDetails = shipDetails;
        IsDrayage = isDrayage;
    }

    public static Transport Create(
        long idMode,
        LocalDateTime? deliveryDateTime,
        TransportType transportType,
        string terminalCode,
        LocationChain.LocationChain locationChain,
        string? mrnNumber,
        Container container,
        Services.Services services,
        List<ServiceTrade> serviceTrades,
        Notes.Notes notes,
        OrderDetails orderDetails,
        Trains.Trains trains,
        GoodsDetails goodsDetails,
        PriceDetails priceDetails,
        BusinessPartner businessPartner,
        CustomsDetails customsDetails,
        HarbourDetails harbourDetails,
        ShipDetails shipDetails,
        bool isDrayage)
    {
        var transportStatus = GetTransportStatus(deliveryDateTime);

        return new Transport(idMode, AssignedType.MANUAL, deliveryDateTime?.Date, deliveryDateTime?.TimeOfDay, transportStatus, transportType, terminalCode, locationChain, mrnNumber, container,
            services, serviceTrades, notes, orderDetails, trains, goodsDetails, priceDetails, businessPartner, customsDetails, harbourDetails, shipDetails, isDrayage);
    }

    private static TransportStatus GetTransportStatus(LocalDateTime? deliveryDateTime) =>
        deliveryDateTime.HasValue
            ? TransportStatus.UNASSIGNED
            : TransportStatus.PREPARING;

    public Cmr.Cmr? GetActiveCmr()
    {
        var activeCmrs = Cmrs.Where(x => x.Status == CmrStatus.ACTIVE).ToList();
        if (activeCmrs.Count > 1)
        {
            throw new DataException($"Transport with id {Id} has more than one active cmr.");
        }
        return activeCmrs.FirstOrDefault();
    }

    public bool IsUpdatePossible(Instant timestamp) => LastUpdatedFromEvent <= timestamp;

    public void SetLastUpdated(Instant lastUpdated) => LastUpdatedFromEvent = lastUpdated;

    public bool IsAssignedToCarrier() => IsStatus(TransportStatus.ASSIGNED) && Carrier is not null;
    private bool IsAssignedToCarrier(string carrierName) => IsAssignedToCarrier() && Carrier!.IsCarrier(carrierName); //namiesto carrier name radsej pouzit SapNumber
    
    internal bool IsCarrierGut() => IsAssignedToCarrier("GUT");
    internal bool IsCarrierEkb() => IsAssignedToCarrier("EKB");
    internal bool IsCarrierKloiber() => IsAssignedToCarrier("KLOIBER");
    internal bool IsImport() => TransportType == TransportType.IMPORT;
    internal bool IsExport() => TransportType == TransportType.EXPORT;

    private void UnassignCarrier()
    {
        if (Carrier is null)
        {
            throw new DataException($"Trying to unassign carrier from transport but it has no carrier assigned. Transport ID: {Id}");
        }
        Carrier = null;
        CarrierId = null;
    }

    public void UncancelCmr(Cmr.Cmr cmr, string username, string? userId)
    {
        AddHistory(TransportHistory.CreateCmrUncancelled(
            Id,
            cmr.Carrier.Id,
            cmr.Id,
            cmr.CmrNumber,
            cmr.Carrier.Name,
            username,
            userId));
        AssignCarrier(cmr.Carrier);
    }
    
    public void AssignCarrier(Carrier carrier)
    {
        if (Carrier is not null)
        {
            throw new InvalidOperationException($"Trying to assign carrier to a transport with id {Id} but it already has a carrier assigned.");
        }
        Status = TransportStatus.ASSIGNED;
        Carrier = carrier;
        CarrierId = carrier.Id;
        AutomatonState = Mis3.Trucks.Transport.De.Be.Api.Enum.Carrier.AutomatonState.ASSIGNED_SUCCESSFULLY;
    }

    public TransportForCapacityDto CreateTransportForCapacity()
    {
        if (DeliveryDate is null || DeliveryTime is null)
        {
            throw new ConstraintException("Trying to create transport for capacities module without delivery date or time - these values are required.");
        }
        
        return new TransportForCapacityDto
        {
            DeliveryDate = DeliveryDate.Value,
            DeliveryTime = DeliveryTime.Value,
            ServiceCategory = Services.ServiceCategory,
            ServiceSubcategory = Services.ServiceSubcategory,
            TerminalCode = TerminalCode,
            TransportId = Id,
            TransportStatus = Status,
            CarrierId = Carrier.IdInDriver,
            IdMode = IdMode
        };
    }
    
    private bool IsStatus(TransportStatus transportStatus) => Status == transportStatus;

    public bool HasActiveCmr()
    {
        var cmr = Cmrs.SingleOrDefault(x => x.Status == CmrStatus.ACTIVE);
        return cmr is not null;
    }

    public Transport Update(
        LocalDateTime? deliveryDateTime,
        TransportType transportType,
        string terminalCode,
        LocationChain.LocationChain locationChain,
        string mrnNumber,
        Container container,
        Services.Services services,
        List<ServiceTrade> serviceTrades,
        Notes.Notes notes,
        OrderDetails orderDetails,
        Trains.Trains trains,
        GoodsDetails goodsDetails,
        PriceDetails priceDetails,
        BusinessPartner businessPartner)
    {
        DeliveryDate = deliveryDateTime?.Date;
        DeliveryTime = deliveryDateTime?.TimeOfDay;
        TransportType = transportType;
        TerminalCode = terminalCode;
        LocationChain = locationChain;
        MrnNumber = mrnNumber;
        Container = container;
        Services = services;
        ServiceTrades = serviceTrades;
        Notes = notes;
        OrderDetails = orderDetails;
        Trains = trains;
        GoodsDetails = goodsDetails;
        PriceDetails = priceDetails; 
        BusinessPartner = businessPartner;
        
        return this;
    }

    public void SetTerminalCode(string terminalCode) => TerminalCode = terminalCode;

    public void SetDeliveryDateTime(LocalDateTime? deliveryDateTime)
    {
        DeliveryDate = deliveryDateTime?.Date;
        DeliveryTime = deliveryDateTime?.TimeOfDay;
        Status = GetTransportStatus(deliveryDateTime);
    }

    public void SetContainer(Container container) => Container = container;
    public void SetWeightDetails(WeightDetails weightDetails) => Container.WeightDetails = weightDetails;
    public void SetTerminalPosition(string? terminalPosition) => Container.TerminalPosition = terminalPosition;
    public void SetSeals(List<string> requestSeals) => Container.Seals = requestSeals;
    public void SetOwnerCode(string? ownerCode) => Container.OwnerCode = ownerCode ?? "N/A";
    public void SetHeldByDepotWorker(bool isHeldByDepotWorker) => Container.IsHeldByDepotWorker = isHeldByDepotWorker;
    public void SetTerminalArrival(LocalDateTime? terminalArrival) => Container.TerminalArrival = terminalArrival;
    public void SetContainerNumber(string containerNumber) => Container.ContainerNumber = containerNumber;
    
    internal bool IsContainerNumberFictional()
    {
        const int importFictionalContainerUnderscoreIndex = 9;

        switch (TransportType)
        {
            case TransportType.EXPORT when Container.ContainerNumber.StartsWith('_'):
            case TransportType.IMPORT when Container.ContainerNumber.Length > importFictionalContainerUnderscoreIndex &&
                                           Container.ContainerNumber[importFictionalContainerUnderscoreIndex] == '_':
                return true;
            case TransportType.UNDEFINED:
            default:
                return false;
        }

    }
    // TODO hardcoded terminalCodes - refactor into repository
    internal bool IsDussTerminal()
    {
        // "Munchen", "Kornwestheim", "Leipzig"
        // taken from https://codebooks.dmis.corp.metrans.cz/terminal;relativeTo=Route%28url:'',%20path:''%29/(left:enums-table//right:terminal-table)
        var dussTerminalCodes = new List<string> { "DUSS MUC", "DUSS KOR", "DUSS LEJ" };
        return dussTerminalCodes.Contains(TerminalCode);
    }
    
    public void SetContainerType(ContainerType containerType) => Container.ContainerType = containerType;

    public void SetServices(Services.Services services) => Services = services;

    public void SetLocationChain(LocationChain.LocationChain locationChain) => LocationChain = locationChain;
    public void SetConsigneeLocation(LocationItem? consigneeLocation) => LocationChain.Consignee = consigneeLocation;

    public void SetPriceDetails(PriceDetails priceDetails) => PriceDetails = priceDetails;
    public void SetZone(int zone) => PriceDetails.Zone = zone;
    
    public void SetOrderDetails(OrderDetails orderDetails) => OrderDetails = orderDetails;

    public void SetTemperature(Temperature? temperature) => GoodsDetails.Temperature = temperature;

    public void SetMetransNote(string? metransNote) => Notes.Metrans = metransNote;
    
    public void SetTrain(Train? train) => Trains.Train = train;
    public void SetFeeder(Feeder? feeder) => Trains.Feeder = feeder;
    
    public void SetGoods(GoodsDetails goods) => GoodsDetails = goods;
    public void SetAutomatonState(AutomatonState? automatonState) => AutomatonState = automatonState;

    internal void CancelWithExpenses(Guid canceledCmrId, string canceledCmrNumber, string username, string? userId)
    {
        if (GetActiveCmr() is not null) throw new InvalidOperationException("Unable to cancel transport with active CMR.");
     
        Status = TransportStatus.CANCELLED_WITH_EXPENSES;
        
        AddHistory(TransportHistory.CreateCmrCancelledWithExpenses(
            Id,
            Carrier.Id,
            canceledCmrId,
            canceledCmrNumber,
            Carrier.Name,
            username,
            userId));
    }

    public void CmrCancelled(string username, string? userId, UnassignReason unassignReason, Guid? cmrId = null)
    {
        var currentCmr = GetActiveCmr();
        if (cmrId is not null && cmrId != GetActiveCmr()?.Id)
        {
            return;
        }
        AddHistory(TransportHistory.CreateCmrCancelled(
            Id,
            Carrier.Id,
            currentCmr.Id,
            currentCmr.CmrNumber,
            Carrier.Name,
            username,
            unassignReason,
            userId,
            NodaTimeHelpers.NowInstant()
        ));
        UnassignCarrier();

        if (Status != TransportStatus.CANCELLED_TRANSPORT)
        {
            Status = TransportStatus.UNASSIGNED;
        }
    }

    public void AddHistory(TransportHistory history) => History.Add(history);

    public void SetBusinessPartner(BusinessPartner businessPartner) => BusinessPartner = businessPartner;

    public void TransportCancelled() => Status = TransportStatus.CANCELLED_TRANSPORT;
    
    public bool IsMis2Origin() => Container.EnvelopeNumber == "MIS-2";

    /// <summary>
    /// Determines whether the transport is associated with a shipping company order.
    /// Definition from J. Gablik https://metransas-my.sharepoint.com/:x:/g/personal/gablik_metransas_onmicrosoft_com/ES1KjDS1_4VDu5a8k0zbcdkBXInSP1asoBHBBQnpTpBa-g?rtime=vytEC-Dm3Ug tab 'Gate reference' cell: 'G3'
    /// </summary>
    /// <returns>
    /// True if the container's owner code is not null and consists solely of letters; otherwise, false.
    /// </returns>
    internal bool IsShippingCompanyOrder() => Container.OwnerCode is not null && Container.OwnerCode.All(char.IsLetter);
}