using BookLibrary.BL.Contracts;
using BookLibrary.Models;
using BookLibrary.Services;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BookLibrary.API.Mapping;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidation();

builder.Services.AddScoped(typeof(IBookService), typeof(BookService));
builder.Services.AddScoped(typeof(ICommentService), typeof(CommentService));
builder.Services.AddScoped(typeof(IUserService), typeof(UserService));

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
