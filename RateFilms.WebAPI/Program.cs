using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ML;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RateFilms.Application.Option;
using RateFilms.Application.Services;
using RateFilms.Application.Services.Films;
using RateFilms.Application.Services.Localization;
using RateFilms.Application.Services.Movies;
using RateFilms.Application.Services.Serials;
using RateFilms.Common.Models.MovieRatingModels;
using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Repositories;
using RateFilms.Infrastructure.Data;
using RateFilms.Infrastructure.Data.Repository;
using RateFilms.WebAPI.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Globalization;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

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

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("ru-RU")
    };

    options.DefaultRequestCulture = new RequestCulture("ru-RU");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddControllers();
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

builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

builder.Services.AddScoped<ILocalizationRepository, LocalizationRepository>();
builder.Services.AddScoped<LocalizationService>();

builder.Services.AddPredictionEnginePool<MovieRating, MovieRatingPrediction>()
    .FromFile(modelName: "MovieRecommenderModel", filePath: "Data/MovieRecommenderModel.zip", watchForChanges: true);

builder.Services.AddPredictionEnginePool<MovieRating, MovieRatingPrediction>()
    .FromFile(modelName: "data_preparation_pipeline", filePath: "Data/data_preparation_pipeline.zip", watchForChanges: true);

builder.Services.Configure<TokenOptions>(config.GetSection("JwtSettings"));

var connectionString = builder.Configuration.GetConnectionString("WebApiDatabase");

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseNpgsql(connectionString);
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

app.MapControllers();

app.Run();
