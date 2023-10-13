using Microsoft.EntityFrameworkCore;
using RateFilms.Domain.Repositories;
using RateFilms.Infrastructure.Data;
using RateFilms.Infrastructure.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IBaseRepository, BaseRepository>();

var connectionString = builder.Configuration.GetConnectionString("WebApiDatabase");

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseNpgsql(connectionString);
});



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
