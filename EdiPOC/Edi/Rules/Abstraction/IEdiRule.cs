namespace EdiPOC.Edi.Rules.Abstraction;

/// <summary>
/// Represents an EDI rule that can be applied to an <see cref="Domain.Edi"/> instance
/// in the context of a specific <see cref="Transport.Transport"/>.
/// </summary>
internal interface IEdiRule
{
    /// <summary>
    /// Gets the name of the EDI rule.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Gets the priority of the rule. Lower values indicate lower priority.
    /// The more specific the rule, the higher the priority.
    /// All rules should be applied in the ascending order of their priority.
    /// Values should be like this: 10, 20, 30, ...
    /// Example #1: a rule that applies only to exports for a specific carrier should have priority 20.
    /// Example #2: a rule that applies to all exports should have priority 10.
    /// </summary>
    int Priority { get; }

    /// <summary>
    /// Applies the rule to the specified <paramref name="edi"/> and <paramref name="transport"/>.
    /// </summary>
    /// <param name="edi">The EDI object to apply the rule to.</param>
    /// <param name="transport">The transport context for the rule.</param>
    /// <returns>The modified EDI object.</returns>
    Domain.Edi Apply(Domain.Edi edi, Transport.Transport transport);
}