using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RateFilms.Application.Services;
using RateFilms.Application.Services.Films;
using RateFilms.Application.Services.Serials;
using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Repositories;
using RateFilms.Infrastructure.Data;
using RateFilms.Infrastructure.Data.Repository;
using RateFilms.WebAPI.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
                                (Encoding.UTF8.GetBytes(config[key: "JwtSettings:Secret"]))

    };
});

builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("admin", build =>
    {
        build.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, Role.Admin.ToString()));
    });

    options.AddPolicy("user", build =>
    {
        build.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, Role.Admin.ToString())
                                    || x.User.HasClaim(ClaimTypes.Role, Role.User.ToString()));
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddScoped<IBaseRepository, BaseRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<IFilmRepository, FilmRepository>();

builder.Services.AddScoped<ISerialService, SerialService>();
builder.Services.AddScoped<ISerialRepositoty, SerialRepository>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
