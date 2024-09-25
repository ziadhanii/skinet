using System;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{

public static IQueryable<T> GetQuery (IQueryable<T> query ,ISpecification<T> spec )
{
	if(spec.Criteria != null)
	{

			query = query.Where(spec.Criteria); // x=>x.Brand == brand
	}
		if(spec.OrderBy != null)
	{

			query = query.OrderBy(spec.OrderBy); // x=>x.Brand == brand
	}
	
			if(spec.OrderByDescending != null)
	{

			query = query.OrderByDescending(spec.OrderByDescending); // x=>x.Brand == brand
	}
	
	
		return query; 
	
}


}
