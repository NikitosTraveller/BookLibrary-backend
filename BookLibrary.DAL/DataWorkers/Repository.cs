using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.DAL.DataWorkers
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _entities;
        private readonly DbContext _dbContext;

        public Repository(DbContext context)
        {
            this._entities = (DbSet<T>)context.Set<T>();
            _dbContext = context;
        }

        public void Create(T entity)
        {
            _entities.Add(entity);
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
        }
    }
}
