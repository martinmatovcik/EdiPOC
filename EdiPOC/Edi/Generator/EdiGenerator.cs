using EdiPOC.Edi.Formatter;
using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Generator;

internal sealed class EdiGenerator : IEdiGenerator
{
    private readonly IEdiFormatter _formatter;
    private readonly IOrderedEnumerable<IEdiRule> _rulesOrderedByPriority;

    public EdiGenerator(IEdiFormatter formatter, IEnumerable<IEdiRule> rules)
    { 
        _formatter = formatter; //TODO: Tu potrebujem zvolit ci sa jedna o NEW, CHANGE, CANCEL. Napriklad to nemusi ist vobec cez DI.
        _rulesOrderedByPriority = rules.OrderBy(r => r.Priority);
    }
    
    public List<Domain.Edi> Generate(IEnumerable<Transport.Transport> transports)
        => transports.Select(Generate).ToList();

    private Domain.Edi Generate(Transport.Transport transport)
    {
        var edi = _formatter.Format(transport);
        foreach (var rule in _rulesOrderedByPriority)
            edi = rule.Apply(edi, transport);

        return edi;
    }
}