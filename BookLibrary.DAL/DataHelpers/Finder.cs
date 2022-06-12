using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.DAL.DataHelpers
{
    public class Finder<T> : IFinder<T> where T : class
    {
        private readonly DbSet<T> _entities;

        public Finder(DbContext context)
        {
            this._entities = context.Set<T>();
        }

        protected DbSet<T> Entities
        {
            get { return _entities; }
        }

        public T? GetById(int id)
        {
            return _entities.Find(id);
        }
    }
}
