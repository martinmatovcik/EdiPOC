namespace EdiPOC.Transport.Goods;

public record Temperature
{
    public int? Minimal { get; init; }
    public int? Maximal { get; init; }

    private Temperature() {} // Parameterless constructor for EF Core
    
    private Temperature(int? minimal, int? maximal)
    {
        if (minimal.HasValue && maximal.HasValue && minimal.Value > maximal.Value) 
            throw new ArgumentException("Minimal temperature cannot be greater than maximal temperature.");
        
        Minimal = minimal;
        Maximal = maximal;
    }

    public static Temperature CreateMinimal(int minimal) => new(minimal, null);
    public static Temperature CreateMaximal(int maximal) => new(null, maximal);
    public static Temperature CreateRange(int? minimal, int? maximal) => new(minimal, maximal);
}