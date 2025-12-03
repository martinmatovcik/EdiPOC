using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.Note.ChassisType;

internal sealed class Note_AppendWhenChassisTypeIsKlausRule : EdiRule
{
    public override string Name => nameof(Note_AppendWhenChassisTypeIsKlausRule);
    public override int Priority => 10;
    private const string ChassisTypeKlausText = "Gestellung per Seitenlader.";

    protected override bool IsApplicable(Transport.Transport transport) => transport.Services.IsChassisTypeKlaus();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.AppendNote(ChassisTypeKlausText);
        
        return edi;
    }
}