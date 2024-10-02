using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;


public class SpecificationEvaluator<T> where T : BaseEntity
{
	public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
	{
		if (spec.Criteria != null)
		{
			query = query.Where(spec.Criteria);
		}

		if (spec.OrderBy != null)
		{
			query = query.OrderBy(spec.OrderBy);
		}

		if (spec.OrderByDescending != null)
		{
			query = query.OrderByDescending(spec.OrderByDescending);
		}

		if (spec.IsDistinct)
		{
			query = query.Distinct();
		}

		// Optionally handle pagination if you add properties for it in ISpecification
		// if (spec.Take.HasValue)
		// {
		//     query = query.Take(spec.Take.Value);
		// }
		//
		// if (spec.Skip.HasValue)
		// {
		//     query = query.Skip(spec.Skip.Value);
		// }

		return query;
	}

	public static IQueryable<TResult> GetQuery<TSpec, TResult>(
		IQueryable<T> query,
		ISpecification<T, TResult> spec)
	{
		if (spec.Criteria != null)
		{
			query = query.Where(spec.Criteria);
		}

		if (spec.OrderBy != null)
		{
			query = query.OrderBy(spec.OrderBy);
		}

		if (spec.OrderByDescending != null)
		{
			query = query.OrderByDescending(spec.OrderByDescending);
		}

		   var selectQuery = query as IQueryable<TResult>;

        if (spec.Select != null)
        {
            selectQuery = query.Select(spec.Select);
        }

        if (spec.IsDistinct)
        {
            selectQuery = selectQuery?.Distinct();
        }
        return selectQuery ?? query.Cast<TResult>();
    }
}