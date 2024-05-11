using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ML;
using RateFilms.Common.MovieRatingModels;
using RateFilms.Domain.Repositories;
using RateFilms.Infrastructure.Data;
using RateFilms.Infrastructure.Data.Repository;

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
                UserId = "d5e54094-e591-452f-9abd-71cf3a34b703",
                MovieId = "30ef5bbd-4a7f-408a-ad5b-d1d1eb43bf87",
                Genres = new[] { "Fantasy", "Animated", "Comedy" }
            };

            var movieRatingPrediction = predictionEngine.Predict(testData);
            Console.WriteLine($"UserId: {testData.UserId} with movieId: {testData.MovieId} Score:{(float)(100 / (1 + Math.Exp(-movieRatingPrediction.Score)))} and Label {movieRatingPrediction.PredictedLabel}");
            
            trainModel.SaveModel(mlContext, trainingDataView.Schema, model);
        }
    }
}
