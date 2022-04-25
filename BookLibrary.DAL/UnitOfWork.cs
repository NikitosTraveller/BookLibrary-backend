using BookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.DAL
{
    public interface IUnitOfWork
    {
        Task<int> Commit();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext context;

        public UnitOfWork(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<int> Commit()
        {
            return await context.SaveChangesAsync();
        }
    }
}
