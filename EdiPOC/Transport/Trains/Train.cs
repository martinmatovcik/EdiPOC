using NodaTime;

namespace EdiPOC.Transport.Trains;

public record Train
{
    public string TrainNumber { get; private set; }
    public LocalDateTime ScheduledDateTime { get; private set; }
    public LocalDateTime? ActualDateTime { get; private set; }

    protected Train(string trainNumber, LocalDateTime scheduledDateTime, LocalDateTime? actualDateTime = null)
    {
        TrainNumber = trainNumber;
        ScheduledDateTime = scheduledDateTime;
        ActualDateTime = actualDateTime;
    }
    
    public static Train Create(string trainNumber, LocalDateTime scheduledDateTime, LocalDateTime? actualDateTime = null) => new(trainNumber, scheduledDateTime, actualDateTime);
}