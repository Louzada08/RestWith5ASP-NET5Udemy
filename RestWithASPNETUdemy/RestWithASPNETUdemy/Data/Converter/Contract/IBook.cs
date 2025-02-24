namespace RestWithASPNETUdemy.Data.Converter.Contract
{
    public interface IBook<O,D>
    {
        D Book(O origin);
        List<D> Book(List<O> origin);
    }
}
