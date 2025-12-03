namespace EdiPOC.Edi.Formatter;

public interface IEdiFormatter
{
    Domain.Edi Format(Transport.Transport transport);
}