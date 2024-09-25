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
}
