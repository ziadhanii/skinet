using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specification;

public class BaseSpecification<T> : ISpecification<T>
{
    private readonly Expression<Func<T, bool>>? _criteria;
    public BaseSpecification(Expression<Func<T, bool>>? criteria)
    {
        _criteria = criteria;
    }
    protected BaseSpecification() : this(null) { }
    public Expression<Func<T, bool>>? Criteria => _criteria;
}
