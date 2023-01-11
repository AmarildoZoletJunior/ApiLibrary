using AutoMapper;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.DTO.AutorMapper;
using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.Repository;
using Biblioteca.Infra.Data.Context;
using Biblioteca.Infra.Data.Repository;
using Biblioteca.Services.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region FluentInjection
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddTransient<IValidator<ClientRequest>, ClientValidation>();
builder.Services.AddTransient<IValidator<AuthorRequest>, AuthorValidation>();
builder.Services.AddTransient<IValidator<CategoryRequest>, CategoryValidation>();


#endregion

#region IMapper
var configuration = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
IMapper mapper = configuration.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

#region InjectionRepository
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
#endregion

builder.Services.AddDbContext<ClassContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
