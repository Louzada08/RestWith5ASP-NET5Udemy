namespace RestWithASPNET.Domain.Interfaces.ConverterVO
{
    public interface IBook<O,D>
    {
        D Book(O origin);
        List<D> Book(List<O> origin);
    }
}
