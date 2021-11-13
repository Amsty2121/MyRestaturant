using Application.Common.Interfaces;
using Common.Models.PagedRequest;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Persistence.Extensions;

namespace Persistence
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly AppDbContext _context;

		public GenericRepository(AppDbContext context)
		{
			_context = context;
		}

		public Task<T> GetById(int id)
		{
			return _context.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
		}

		public Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
		{
			return _context.Set<T>().FirstOrDefaultAsync(predicate);
		}

		public async Task Add(T entity)
		{
			entity.CreatedDateTime = DateTime.Now;
			entity.ModifiedDateTime = DateTime.Now;
			await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

		public Task Update(T entity)
		{
			// In case AsNoTracking is used
			entity.ModifiedDateTime = DateTime.Now;
			_context.Entry(entity).State = EntityState.Modified;
			return _context.SaveChangesAsync();
		}

		public Task Remove(T entity)
		{
			_context.Set<T>().Remove(entity);
			return _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<T>> GetAll()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
		{
			return await _context.Set<T>().Where(predicate).ToListAsync();
		}

		public Task<int> CountAll()
		{
			return _context.Set<T>().CountAsync();
		}

		public Task<int> CountWhere(Expression<Func<T, bool>> predicate)
		{
			return _context.Set<T>().CountAsync(predicate);
		}

		public async Task<T> GetByIdWithInclude(int id, params Expression<Func<T, object>>[] includeProperties)
		{
			var query = IncludeProperties(includeProperties);
			return await query.FirstOrDefaultAsync(entity => entity.Id == id);
		}

		public async Task<IEnumerable<T>> GetAllWithInclude(params Expression<Func<T, object>>[] includeProperties)
		{
			IQueryable<T> entities = IncludeProperties(includeProperties);

			return await entities.ToListAsync();
		}

		private IQueryable<T> IncludeProperties(params Expression<Func<T, object>>[] includeProperties)
		{
			IQueryable<T> entities = _context.Set<T>();
			foreach (var includeProperty in includeProperties)
			{
				entities = entities.Include(includeProperty);
			}
			return entities;
		}

		public async Task<PaginatedResult<T>> GetPagedData<T>(PagedRequest pagedRequest) where T : BaseEntity
		{
			return await _context.Set<T>().CreatePaginatedResultAsync<T>(pagedRequest);
		}
	}
}