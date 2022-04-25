using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.DAL.DataWorkers
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
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            var query = BuildQuery(filter, includes);
            return await query.ToListAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            var query = BuildQuery(filter);
            return await query.FirstOrDefaultAsync();
        }



        private IQueryable<T> BuildQuery(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
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

            return query;
        }
    }
}
