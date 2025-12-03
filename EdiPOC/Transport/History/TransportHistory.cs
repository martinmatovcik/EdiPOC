using EdiPOC.Carriers;
using MIS3.Trucks.Common.Domain.Entity;
using MIS3.Trucks.Common.Helpers;
using Mis3.Trucks.Transport.De.Be.Api.Enum.Carrier;
using Mis3.Trucks.Transport.De.Be.Api.Enum.Transport;
using Mis3.Trucks.Transport.De.Be.Api.Features.Transport.Dto;
using NodaTime;

namespace EdiPOC.Transport.History;

// TODO: Upravit vazby a duplicitne property (e.g. CarrierId, CarirerName, CarrierChangeReason, Carrier)
public class TransportHistory : Entity
{
    public required Guid TransportId { get; init; }
    public required Guid CarrierId { get; init; }
    public required Guid CmrId { get; init; }
    public required HistoryStatus Status { get; init; } = HistoryStatus.UNDEFINED;
    public required string CmrNumber { get; init; }
    public required string CarrierName { get; init; }
    public required string ChangedBy { get; init; }
    public required Instant ChangedAt { get; init; }
    public required UnassignReason UnassignReason { get; init; } = UnassignReason.UNDEFINED;
    public bool? HadExpenses { get; private init; }
    public string? UserId { get; private init; }
    
    public Transport Transport { get; init; }
    public Carrier Carrier { get; init; }
    public Cmr.Cmr Cmr { get; init; }

    private TransportHistory() { }

    public static TransportHistory CreateCmrCancelledWithExpenses(
        Guid transportId,
        Guid carrierId,
        Guid cmrId,
        string cmrNumber,
        string carrierName,
        string changedBy,
        string? userId,
        Instant? changedAt = null,
        Guid? id = null)
        => new TransportHistory
        {
            Status = HistoryStatus.CANCELLED,
            CmrNumber = cmrNumber,
            CarrierName = carrierName,
            ChangedBy = changedBy,
            ChangedAt = changedAt ?? NodaTimeHelpers.NowInstant(),
            UnassignReason = UnassignReason.NOT_APPLICABLE,
            HadExpenses = true,
            Id = id ?? Guid.NewGuid(),
            CarrierId = carrierId,
            TransportId = transportId,
            CmrId = cmrId,
            UserId = userId
        };

    public static TransportHistory CreateCmrCancelled(
        Guid transportId,
        Guid carrierId,
        Guid cmrId,
        string cmrNumber,
        string carrierName,
        string changedBy,
        UnassignReason unassignReason,
        string? userId,
        Instant? changedAt = null,
        Guid? id = null)
        => new TransportHistory
        {
            Status = HistoryStatus.CANCELLED,
            CmrNumber = cmrNumber,
            CarrierName = carrierName,
            ChangedBy = changedBy,
            ChangedAt = changedAt ?? NodaTimeHelpers.NowInstant(),
            UnassignReason = unassignReason,
            HadExpenses = false,
            Id = id ?? Guid.NewGuid(),
            CarrierId = carrierId,
            TransportId = transportId,
            CmrId = cmrId,
            UserId = userId
        };

    public static TransportHistory CreateAssigned(
        Guid transportId,
        Guid carrierId,
        Guid cmrId,
        string cmrNumber,
        string carrierName,
        string changedBy,
        string? userId,
        Instant? changedAt = null,
        Guid? id = null)
        => new TransportHistory
        {
            Status = HistoryStatus.ASSIGNED,
            CmrNumber = cmrNumber,
            CarrierName = carrierName,
            ChangedBy = changedBy,
            ChangedAt = changedAt ?? NodaTimeHelpers.NowInstant(),
            UnassignReason = UnassignReason.NOT_APPLICABLE,
            Id = id ?? Guid.NewGuid(),
            CarrierId = carrierId,
            TransportId = transportId,
            CmrId = cmrId,
            UserId = userId
        };

    public static TransportHistory CreateCmrUncancelled(
        Guid transportId,
        Guid carrierId,
        Guid cmrId,
        string cmrNumber,
        string carrierName,
        string changedBy,
        string? userId,
        Instant? changedAt = null,
        Guid? id = null)
        => new TransportHistory
        {
            Status = HistoryStatus.UNCANCELLED,
            CmrNumber = cmrNumber,
            CarrierName = carrierName,
            ChangedBy = changedBy,
            ChangedAt = changedAt ?? NodaTimeHelpers.NowInstant(),
            UnassignReason = UnassignReason.NOT_APPLICABLE,
            Id = id ?? Guid.NewGuid(),
            CarrierId = carrierId,
            TransportId = transportId,
            CmrId = cmrId,
            UserId = userId
        };
    
    public TransportHistoryDto ToHistoryDto()
        => new TransportHistoryDto
        {
            CarrierName = CarrierName,
            ChangedAt = ChangedAt.ToFormattedString(),
            HistoryStatus = Status,
            ChangedBy = ChangedBy,
            CmrNumber = CmrNumber,
            UnassignReason = UnassignReason,
            HadExpenses = HadExpenses
        };
}