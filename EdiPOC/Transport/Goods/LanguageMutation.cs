namespace EdiPOC.Transport.Goods;

public record LanguageMutation
{
    public string Language { get; private set; }
    public string Value { get; private set; }

    private LanguageMutation() {} // Parameterless constructor for EF Core
    
    private LanguageMutation(string language, string value)
    {
        Language = language;
        Value = value;
    }

    public static LanguageMutation Create(string language, string value) => new(language, value);
    
    internal bool IsGerman() => Language == "DE";
}