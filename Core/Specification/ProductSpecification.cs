using Core.Entities;
using Core.Specification;
using Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
	public ProductSpecification(ProductSpecParams specParams)
		: base(x =>
		(
			(string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search.ToLower())) &&
			(specParams.Brands.Count == 0 || specParams.Brands.Contains(x.Brand)) &&
			(specParams.Types.Count == 0 || specParams.Types.Contains(x.Type))
		))
	{
		ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

		switch (specParams.Sort)
		{
			case "priceAsc":
				AddOrderBy(p => p.Price);
				break;
			case "priceDesc":
				AddOrderByDescending(p => p.Price);
				break;
			default:
				AddOrderBy(p => p.Name);
				break;
		}
	}
}
