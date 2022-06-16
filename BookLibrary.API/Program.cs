using BookLibrary.BL.Contracts;
using BookLibrary.Models;
using BookLibrary.Services;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BookLibrary.API.Mapping;
using FluentValidation.AspNetCore;
using BookLibrary.DAL;
using BookLibrary.DAL.Contracts;
using BookLibrary.API;
using BookLibrary.DAL.Finders;
using BookLibrary.DAL.Repositories;
using BookLibrary.DAL.UnitOfWorks;

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidation();

builder.Services.AddScoped<IBookFinder, BookFinder>();
builder.Services.AddScoped<IUserFinder, UserFinder>();
builder.Services.AddScoped<ICommentFinder, CommentFinder>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IUserService, UserService>();

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
