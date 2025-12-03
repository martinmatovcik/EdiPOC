namespace EdiPOC.Transport.Trains;

public sealed record Trains
{
    public Train? Train { get; internal set; }
    public Feeder? Feeder { get; internal set; }

    private Trains()
    {
    }

    private Trains(Train? train, Feeder? feeder)
    {
        Train = train;
        Feeder = feeder;
    }
    
    public static Trains Create(Train? train, Feeder? feeder) => new(train, feeder);
}