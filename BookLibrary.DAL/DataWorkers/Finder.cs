using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.DAL
{
    public class Finder<T> : IFinder<T> where T : class
    {
        private readonly DbSet<T> _entities;
        private readonly DbContext _dbContext;

        public Finder(DbContext context)
        {
            this._entities = (DbSet<T>)context.Set<T>();
            _dbContext = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetListAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
            bool disableTracking = false)
        {
            var query = BuildQuery(filter, includes, null, disableTracking);
            return await query.ToListAsync();
        }

        private IQueryable<T> BuildQuery(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
            IBaseSearchOptions baseSearchOptions = null,
            bool disableTracking = false)
        {
            IQueryable<T> query = this._entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (baseSearchOptions != null)
            {
                query = SetOrderBy(query, baseSearchOptions);
                query = SetPaging(query, baseSearchOptions);
            }

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            return query;
        }
    }
}
