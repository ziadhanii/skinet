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

	public Expression<Func<T, object>>? OrderBy { get; private set; }

	public Expression<Func<T, object>>? OrderByDescending { get; private set; }


	protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
	{
		OrderBy = orderByExpression;
	}

	protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
	{
		OrderByDescending = orderByDescExpression;
	}


}