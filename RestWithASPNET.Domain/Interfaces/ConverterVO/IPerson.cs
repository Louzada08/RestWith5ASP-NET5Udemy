namespace RestWithASPNET.Domain.Interfaces.ConverterVO
{
    public interface IPerson<O,D>
    {
        D Person(O origin);
        List<D> Person(List<O> origin);
    }
}
