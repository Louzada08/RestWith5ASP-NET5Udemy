using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;

namespace RestWithASPNET.Domain.Filter;

public class PersonFilter : ICustomQueryable
{
    [QueryOperator(Operator = WhereOperator.StartsWith, HasName = "Primeiro Nome", CaseSensitive = false)]
    public string firstName { get; set; } = string.Empty;

    [QueryOperator(Operator = WhereOperator.Contains, HasName = "Segundo Nome", CaseSensitive = false)]
    public string? lastName { get; set; } = string.Empty;
}
