namespace RestWithASPNET.FrameWrkDrivers.Gateways
{
    public interface IParser<O,D>
    {
        D Parse(O origin);
        List<D> Parse(List<O> origin);
    }
}
