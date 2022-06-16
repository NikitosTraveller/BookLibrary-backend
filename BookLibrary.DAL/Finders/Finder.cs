using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.DAL.Finders
{
    public class Finder<T> where T : class
    {
        private readonly DbSet<T> _entities;

        public Finder(DbContext context)
        {
            _entities = context.Set<T>();
        }

        protected DbSet<T> Entities
        {
            get { return _entities; }
        }
    }
}
