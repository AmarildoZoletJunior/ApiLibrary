using AutoMapper;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.DTO.AutorMapper;
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
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddTransient<IValidator<AuthorDTO>, AuthorValidation>();
var configuration = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
IMapper mapper = configuration.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
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
