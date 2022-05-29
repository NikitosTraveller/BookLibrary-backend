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
    public interface IFinder<T> where T : class
    {
        public T? GetById(int id);

        public DbSet<T> Entities { get; }

        public Task<IEnumerable<T>> GetListAsync();
    }
}
