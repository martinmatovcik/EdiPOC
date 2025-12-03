using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.Note.ChassisType;

internal sealed class Note_AppendWhenChassisTypeIsSklapRule : EdiRule
{
    public override string Name => nameof(Note_AppendWhenChassisTypeIsSklapRule);
    public override int Priority => 10;

    private const string ChassisTypeSklapText = "Gestellung per Kippchassis.";


    protected override bool IsApplicable(Transport.Transport transport) => transport.Services.IsChassisTypeSklap();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.AppendNote(ChassisTypeSklapText);
        
        return edi;
    }
}