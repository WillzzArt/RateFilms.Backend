using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ML;
using RateFilms.Domain.Repositories;
using RateFilms.Infrastructure.Data;
using RateFilms.Infrastructure.Data.Repository;
using RateFilms.MovieRecomendation.Model;

namespace RateFilms.MovieRecomendation
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(configuration);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Database")));


            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            
            ServiceProvider serviceProvider = services.BuildServiceProvider();

            var favoriteRepository = serviceProvider.GetRequiredService<IFavoriteRepository>();

            var trainModel = new TrainModel(favoriteRepository);

            MLContext mlContext = new MLContext();

            (IDataView trainingDataView, IDataView testDataView) = await trainModel.LoadData(mlContext);

            var model = trainModel.BuildAndTrain(mlContext, trainingDataView);
            trainModel.EvaluateModel(mlContext, testDataView, model);

            var predictionEngine = mlContext.Model.CreatePredictionEngine<MovieRating, MovieRatingPrediction>(model);

            MovieRating testData = new MovieRating() 
            {
                UserId = "768cd7e4-8c17-45f6-a635-d028066f070f",
                MovieId = "3c7b869a-70ed-442e-9f30-f5e0a5182cb1",
                Genres = new[] { "Action", "Fantasy" }
            };

            var movieRatingPrediction = predictionEngine.Predict(testData);
            Console.WriteLine($"UserId:{testData.UserId} with movieId: {testData.MovieId} Score:{(float)(100 / (1 + Math.Exp(-movieRatingPrediction.Score)))} and Label {movieRatingPrediction.PredictedLabel}");

        }
    }
}
