namespace EdiPOC.Edi.Rules.Abstraction;

/// <summary>
/// Abstract base class for EDI rules, implementing <see cref="IEdiRule"/>.
/// Provides a template for applying EDI transformation rules based on transport context.
/// </summary>
internal abstract class EdiRule : IEdiRule
{
    /// <summary>
    /// Gets the name of the EDI rule.
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// Gets the priority of the rule. Lower values indicate lower priority.
    /// </summary>
    public abstract int Priority { get; }

    /// <summary>
    /// Applies the rule to the given <paramref name="edi"/> object if the rule is applicable
    /// for the specified <paramref name="transport"/>.
    /// </summary>
    /// <param name="edi">The EDI object to apply the rule to.</param>
    /// <param name="transport">The transport context for rule applicability.</param>
    /// <returns>The potentially modified EDI object.</returns>
    public Domain.Edi Apply(Domain.Edi edi, Transport.Transport transport)
    {
        if (IsApplicable(transport)) edi = ApplyRuleFor(edi, transport);

        return edi;
    }

    /// <summary>
    /// Determines whether the rule is applicable for the specified <paramref name="transport"/>.
    /// </summary>
    /// <param name="transport">The transport context to check.</param>
    /// <returns><c>true</c> if the rule is applicable; otherwise, <c>false</c>.</returns>
    protected abstract bool IsApplicable(Transport.Transport transport);

    /// <summary>
    /// Applies the rule-specific transformation to the given <paramref name="edi"/> object.
    /// </summary>
    /// <param name="edi">The EDI object to transform.</param>
    /// <param name="transport">The transport which is used to set values in the EDI object.</param>
    /// <returns>The transformed EDI object.</returns>
    protected abstract Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport);
}