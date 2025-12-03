namespace EdiPOC.Edi.Generator;

/// <summary>
/// Defines a generator for EDI documents based on transport data.
/// </summary>
public interface IEdiGenerator
{
    /// <summary>
    /// Generates a list of EDI documents from the provided transports.
    /// </summary>
    /// <param name="transports">A collection of transport entities to generate EDI documents for.</param>
    /// <returns>A list of generated EDI documents.</returns>
    List<Domain.Edi> Generate(IEnumerable<Transport.Transport> transports);
}