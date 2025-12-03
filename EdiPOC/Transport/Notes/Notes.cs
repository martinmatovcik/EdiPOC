namespace EdiPOC.Transport.Notes;

public sealed record Notes
{
    public string? Dispatcher { get; private set; }
    public string? Metrans { get; internal set; }
    public string? Customs { get; private set; }
    public string? Declaration { get; private set; }

    private Notes()
    {
    }

    private Notes(string? dispatcher, string? metrans, string? customs, string? declaration)
    {
        Dispatcher = dispatcher;
        Metrans = metrans;
        Customs = customs;
        Declaration = declaration;
    }
    
    public static Notes Create(string? dispatcher, string? metrans, string? customs, string? declaration) =>
        new(dispatcher, metrans, customs, declaration);

    public void SetDispatcherNote(string? newValue)
    {
        Dispatcher = newValue;
    }
}
