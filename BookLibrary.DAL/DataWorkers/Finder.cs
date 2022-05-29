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
            this._entities = context.Set<T>();
            _dbContext = context;
        }

        public async Task<IEnumerable<T>> GetListAsync()
        {
            return await _entities.ToListAsync();
        }

        public DbSet<T> Entities
        {
            get { return _entities; }
        }

        public T? GetById(int id)
        {
            return _entities.Find(id);
        }
    }
}
