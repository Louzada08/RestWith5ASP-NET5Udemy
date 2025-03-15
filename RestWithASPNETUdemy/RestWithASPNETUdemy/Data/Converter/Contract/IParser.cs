﻿namespace RestWithASPNET.Domain.Entities
{
    public interface IParser<O,D>
    {
        D Parse(O origin);
        List<D> Parse(List<O> origin);
    }
}
