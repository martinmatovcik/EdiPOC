using EdiPOC.Carriers;
using MIS3.Trucks.Common.Domain.Entity;
using MIS3.Trucks.Common.Helpers;
using Mis3.Trucks.Transport.De.Be.Api.Enum.Carrier;
using Mis3.Trucks.Transport.De.Be.Api.Enum.Cmr;
using NodaTime;

namespace EdiPOC.Cmr;

public class Cmr : Entity
{
    public CompletionStatus CompletionStatus { get; init; } = CompletionStatus.UNDEFINED;
    public CmrStatus Status { get; set; } = CmrStatus.UNDEFINED;
    public string CmrNumber { get; init; } = "UNDEFINED_CMR_NUMBER";
    public int SequenceNumber { get; init; } = -1;
    public Transport.Transport Transport { get; init; }
    public Instant ChangedAt { get; private set; }
    public string ChangedBy { get; private set; }
    public Carrier? Carrier { get; init; }

    private Cmr() { }

    public static Cmr CreateActive(string cmrPrefix, int sequenceNumber, Transport.Transport transport, string username, Carrier carrier) =>
        new()
        {
            CompletionStatus = CompletionStatus.NOT_COMPLETED,
            Status = CmrStatus.ACTIVE,
            CmrNumber = CreateCmrNumber(cmrPrefix, sequenceNumber),
            SequenceNumber = sequenceNumber,
            Transport = transport,
            ChangedAt = NodaTimeHelpers.NowInstant(),
            ChangedBy = username,
            Carrier = carrier
        };

    private static string CreateCmrNumber(string prefix, int number) => prefix + DateTime.Now.Year + number.ToString().PadLeft(6, '0');

    public void CancelWithExpenses(string username, string? userId)
    {
        Cancel(username, userId, UnassignReason.NOT_APPLICABLE);
        Transport.CancelWithExpenses(Id, CmrNumber, username, userId);
    }

    public void Cancel(string username, string? userId, UnassignReason unassignReason)
    {
        Transport.CmrCancelled(username, userId, unassignReason, Id);
        Status = CmrStatus.INACTIVE_CANCELLED;
        ChangedAt = NodaTimeHelpers.NowInstant();
        ChangedBy = username;
    }
    
    public void Uncancel(string username, string? userId)
    {
        Transport.UncancelCmr(this, username, userId);
        Status = CmrStatus.ACTIVE;
        ChangedAt = NodaTimeHelpers.NowInstant();
        ChangedBy = username;
    }
}