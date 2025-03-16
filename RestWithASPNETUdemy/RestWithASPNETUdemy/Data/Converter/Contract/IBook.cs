﻿namespace RestWithASPNET.Domain.Entities
{
    public interface IBook<O,D>
    {
        D Book(O origin);
        List<D> Book(List<O> origin);
    }
}
