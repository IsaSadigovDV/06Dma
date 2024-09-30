using Api006.App.Context;
using Api006.App.Mappings;
using Api006.App.Repositories.Abstractions;
using Api006.App.Repositories.Concrets;
using Api006.App.Services.Abstractions;
using Api006.App.Services.Concrets;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

// task 1. category respository
// task 2. generic respository
// task 3. 


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(CategoryMap));
builder.Services.AddFluentValidation(opt => opt.RegisterValidatorsFromAssemblyContaining<CategoryMap>());

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

//repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

//services
builder.Services.AddScoped<ICategoryService, CategoryService>();

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
