using Microsoft.ML;
using Microsoft.ML.Trainers;
using RateFilms.Domain.Repositories;
using RateFilms.MovieRecomendation.Model;
using System.Drawing;

namespace RateFilms.MovieRecomendation
{
    internal class TrainModel
    {
        private readonly IFavoriteRepository _favoriteRepository;

        private List<MovieRating> testData = new List<MovieRating>() 
        {
            new MovieRating() 
            {
                UserId = "95830040-d2b1-4f9d-b12c-22714f86976f",
                MovieId = "d6f2db08-b3e1-4cd3-9def-2fa8027b560e",
                Genres = new[] { "Action", "Fantasy", "Horror" },
                Label = false
            },
            new MovieRating()
            {
                UserId = "bb2cf527-d32b-430b-b993-8f3af037603b",
                MovieId = "54814870-3963-4fc8-9847-6f667c4305c3",
                Genres = new[] { "Fantasy", "Animated", "Drama" },
                Label = true
            },
            new MovieRating()
            {
                UserId = "b4c09461-0ca8-4420-a175-bd5c4f16e44b",
                MovieId = "ec22be77-76d4-401e-a1b2-1e7a73b811ce",
                Genres = new[] { "Action" },
                Label = true
            },
            new MovieRating()
            {
                UserId = "df26ffa9-07d6-4edc-b593-b899062f256c",
                MovieId = "5b7096a9-c9e6-479a-afed-503eeae05040",
                Genres = new[] { "Fantasy" },
                Label = false
            }
        };

        public TrainModel(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public async Task<(IDataView training, IDataView test)> LoadData(MLContext mLContext)
        {
            var favInFilms = await _favoriteRepository.FindFavoriteInFilms();
            var favInSerials = await _favoriteRepository.FindFavoriteInSerials();
            var movieRatings = new List<MovieRating>();

            foreach (var fav in favInFilms)
            {
                var movieRating = new MovieRating
                {
                    UserId = fav.UserId.ToString(),
                    Genres = fav.Film!.Genre.Select(x => x.Genre).ToArray(),
                    MovieId = fav.FilmId.ToString(),
                    Label = fav.Score! > 3.5 ? true : false
                };

                movieRatings.Add(movieRating);
            }

            foreach (var fav in favInSerials)
            {
                var movieRating = new MovieRating
                {
                    UserId = fav.UserId.ToString(),
                    Genres = fav.Serial!.Genre.Select(x => x.Genre).ToArray(),
                    MovieId = fav.SerialId.ToString(),
                    Label = fav.Score! > 3.5 ? true : false
                };

                movieRatings.Add(movieRating);
            }

            IDataView trainingDataView = mLContext.Data.LoadFromEnumerable(movieRatings);
            IDataView testDataView = mLContext.Data.LoadFromEnumerable(testData);

            return (trainingDataView, testDataView);
        }

        public ITransformer BuildAndTrain(MLContext mlContext, IDataView trainingDataView)
        {
            var dataProcessPipeline = mlContext.Transforms.Text
                .FeaturizeText("userIdFeaturized", nameof(MovieRating.UserId))
                .Append(mlContext.Transforms.Text.FeaturizeText("movieIdFeaturized", nameof(MovieRating.MovieId))
                .Append(mlContext.Transforms.Text.FeaturizeText("genresFeaturized", nameof(MovieRating.Genres))
                .Append(mlContext.Transforms.Concatenate("Features", "userIdFeaturized", "movieIdFeaturized", "genresFeaturized"))));

            var trainingPipeLine = dataProcessPipeline
                .Append(mlContext.BinaryClassification.Trainers.FieldAwareFactorizationMachine(new string[] { "Features" }));

            Console.WriteLine("=============== Training the model ===============");
            var model = trainingPipeLine.Fit(trainingDataView);
            return model;
        }

        public void EvaluateModel(MLContext mlContext, IDataView testDataView, ITransformer model)
        {
            Console.WriteLine("=============== Evaluating the model ===============");
            var prediction = model.Transform(testDataView);

            var metrics = mlContext.BinaryClassification
                .Evaluate(
                    data: prediction,
                    labelColumnName: "Label", 
                    scoreColumnName: "Score", 
                    predictedLabelColumnName: "PredictedLabel");

            Console.WriteLine("Evaluation Metrics: acc:" + Math.Round(metrics.Accuracy, 4) + " AreaUnderRocCurve(AUC):" + Math.Round(metrics.AreaUnderRocCurve, 4));
        }
    }
}
