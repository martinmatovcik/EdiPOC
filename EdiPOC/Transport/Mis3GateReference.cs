namespace EdiPOC.Transport;

/// <summary>
/// Represents a gate reference used in MIS3 order messages.
/// </summary>
/// <param name="GateReference">The gate reference identifier.</param>
/// <param name="IsValid">Indicates whether the gate reference is valid.</param>
public sealed record Mis3GateReference(string GateReference, bool IsValid)
{
    /// <summary>The gate reference identifier.</summary>
    public string GateReference { get; init; } = GateReference;

    /// <summary>Indicates whether the gate reference is valid.</summary>
    public bool IsValid { get; init; } = IsValid;
}