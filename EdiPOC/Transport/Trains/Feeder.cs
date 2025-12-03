using NodaTime;

namespace EdiPOC.Transport.Trains;

public record Feeder : Train
{
    private Feeder(string trainNumber, LocalDateTime scheduledDateTime) : base(trainNumber, scheduledDateTime)
    {
    }
    
    public static Feeder Create(string trainNumber, LocalDateTime scheduledDateTime) => new(trainNumber, scheduledDateTime);
} 