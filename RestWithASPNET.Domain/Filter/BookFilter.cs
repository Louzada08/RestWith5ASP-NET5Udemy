using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;

namespace RestWithASPNET.Domain.Filter;

public class BookFilter : ICustomQueryable
{
    [QueryOperator(Operator = WhereOperator.StartsWith, HasName = "Author", CaseSensitive = false)]
    public string? Name { get; set; }

    [QueryOperator(Operator = WhereOperator.StartsWith, HasName = "title", CaseSensitive = false)]
    public string? title { get; set; }

    [QueryOperator(Operator = WhereOperator.GreaterThanOrEqualWhenNullable, HasName = "Launch_Date")]
    public DateTime? Launch_Date { get; set; }

    public int Limit { get; set; } = 30;
    public int Page { get; set; } = 1;
    public int? Section { get; set; }
    public Guid? Id { get; set; }
}
