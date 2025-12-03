namespace EdiPOC.Transport.Customs;

public record CustomsDetails
{
    public bool IsCustomsCleared { get; init; }

    private CustomsDetails(bool isCustomsCleared)
    {
        IsCustomsCleared = isCustomsCleared;
    }

    /// <summary>
    /// Creates a new instance with custom details.
    /// </summary>
    /// <param name="isCustomsCleared"></param>
    /// <returns>A new <see cref="CustomsDetails"/> instance with all details set.</returns>
    public static CustomsDetails Create(bool isCustomsCleared) => new(isCustomsCleared);
}