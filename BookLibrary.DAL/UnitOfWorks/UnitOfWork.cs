using BookLibrary.DAL.Contracts;
using BookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext context;

        public UnitOfWork(ApplicationContext context)
        {
            this.context = context;
        }

        public Task<int> Commit()
        {
            return context.SaveChangesAsync();
        }
    }
}
