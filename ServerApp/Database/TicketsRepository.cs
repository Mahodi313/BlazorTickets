using Microsoft.EntityFrameworkCore;

namespace ServerApp.Database;

public class TicketsRepository<T> where T : class
{
	private readonly TicketsDbContext _context;
	private readonly DbSet<T> _dbSet;
	public TicketsRepository(TicketsDbContext context)
	{
		_context = context;
		_dbSet = context.Set<T>();
	}

	public async Task<List<T>> GetAll()
	{
		return await _dbSet.ToListAsync();
	}

	/// <summary>
	/// Send argument as a string for example "Table1.Table2" to include Main content Table, Table1 and Table2 in it.
	/// If you want only one include send only one string of the table name.
	/// </summary>
	/// <param name="navigationProperties"></param>
	/// <returns>List of all <typeparamref name="T"/> in a table with all other specified tables included</returns>
	public async Task<List<T>?> GetAllInclude(params string[] navigationProperties)
	{

		IQueryable<T> query = _dbSet;   //Set to Queryable to build onto the LINQ method so it will be include().include().... until all sent in are included
										//Almost as a string builder

		foreach (var navigationProperty in navigationProperties)
		{
			query = query.Include(navigationProperty); // Build the query
		}

		return await query.ToListAsync(); //Execute and make it to a list
	}

	public async Task<T?> Get(int id)
	{
		return await _dbSet.FindAsync(id);
	}
	public async Task Add(T entity)
	{
		await _dbSet.AddAsync(entity);
	}

	public void Update(T entity)
	{
		_context.Attach(entity); //Attach to the context to make the program look at the object for changes and update if there are any
		_dbSet.Update(entity);
	}


	public async Task<T?> Delete(int id)
	{
		var entityToDelete = await _dbSet.FindAsync(id);
		if (entityToDelete != null)
		{
			_dbSet.Remove(entityToDelete);
			await Complete(); // Save changes 

			return entityToDelete;
		}
		return null; // Return null if the entity was not found
	}
	public async Task Complete()
	{
		await _context.SaveChangesAsync();
	}
}
