using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
	private readonly StoreContext _context;

	public GenericRepository(StoreContext context)
	{
		_context = context;
	}

	public void Add(T entity)
	{

		_context.Set<T>().Add(entity);
	}

	public void Remove(T entity)
	{
		_context.Set<T>().Remove(entity);
	}

	public bool Exists(int id)
	{
		return _context.Set<T>().Any(x => x.Id == id);
	}

	public async Task<T?> GetByIdAsync(int id)

	{
		return await _context.Set<T>().FindAsync(id);
	}

	public async Task<IReadOnlyList<T>> ListAllAsync()
	{
		return await _context.Set<T>().ToListAsync();
	}

	public async Task<bool> SaveAllAsync()
	{
		return await _context.SaveChangesAsync() > 0;
	}

	public void Update(T entity)
	{
		_context.Set<T>().Attach(entity);
		_context.Entry(entity).State = EntityState.Modified;
	}

	public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
	{
		return await ApplySpecification(spec).FirstOrDefaultAsync();

	}

	public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
	{
		return await ApplySpecification(spec).ToListAsync();
	}

	private IQueryable<T> ApplySpecification(ISpecification<T> spec)

	{

		return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);

	}
	private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> spec)

	{

		return SpecificationEvaluator<T>.GetQuery<T, TResult>(_context.Set<T>().AsQueryable(), spec);

	}
	public async Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec)
	{
		return await ApplySpecification(spec).FirstOrDefaultAsync();
	}

	public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec)
	{
		return await ApplySpecification(spec).ToListAsync();
	}

	public async Task<int> CountAsync(ISpecification<T> spec)
	{
		var query = _context.Set<T>().AsQueryable();
		query = spec.ApplyCriteria(query);
		return await query.CountAsync();
	}


}
