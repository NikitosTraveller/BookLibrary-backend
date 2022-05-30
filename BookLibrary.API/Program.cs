using BookLibrary.BL.Contracts;
using BookLibrary.Models;
using BookLibrary.Services;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BookLibrary.API.Mapping;
using FluentValidation.AspNetCore;
using BookLibrary.DAL;
using BookLibrary.DAL.DataWorkers;
using Autofac.Extensions.DependencyInjection;
using BookLibrary.BL;
using Autofac;
using BookLibrary.DAL.Contracts;
using BookLibrary.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();

builder.Services.AddAutofac();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidation();

builder.Services.AddScoped(typeof(IFinder<>), typeof(Finder<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new RegisterModules());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
