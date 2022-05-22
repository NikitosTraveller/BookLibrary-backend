using Autofac;
using BookLibrary.DAL;
using BookLibrary.DAL.DataWorkers;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.BL
{
    public class RegisterModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationContext>().AsSelf().As<DbContext>().InstancePerLifetimeScope();

            builder.RegisterType<Finder<Book>>()
                .As<IFinder<Book>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Finder<Comment>>()
                .As<IFinder<Comment>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Finder<User>>()
                .As<IFinder<User>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Repository<Book>>()
                .As<IRepository<Book>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Repository<Comment>>()
                .As<IRepository<Comment>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Repository<User>>()
                .As<IRepository<User>>()
                .InstancePerLifetimeScope();

        }
    }
}
