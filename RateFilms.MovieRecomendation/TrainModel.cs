using Microsoft.ML;
using Microsoft.ML.Trainers;
using RateFilms.Common.MovieRatingModels;
using RateFilms.Domain.Repositories;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RateFilms.MovieRecomendation
{
    internal class TrainModel
    {
        private readonly IFavoriteRepository _favoriteRepository;

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

            var dataSplit = mLContext.Data.TrainTestSplit(mLContext.Data.LoadFromEnumerable(movieRatings), 0.2);

            IDataView trainingDataView = dataSplit.TrainSet;
            IDataView testDataView = dataSplit.TestSet;

            return (trainingDataView, testDataView);
        }

        public ITransformer TrainAndSave(MLContext mlContext, IDataView trainingDataView)
        {
            /*var dataProcessPipeline = mlContext.Transforms.Text
                .FeaturizeText("userIdFeaturized", nameof(MovieRating.UserId))
                .Append(mlContext.Transforms.Text.FeaturizeText("movieIdFeaturized", nameof(MovieRating.MovieId))
                .Append(mlContext.Transforms.Text.FeaturizeText("genresFeaturized", nameof(MovieRating.Genres))
                .Append(mlContext.Transforms.Concatenate("Features", "userIdFeaturized", "movieIdFeaturized", "genresFeaturized"))));

            var trainingPipeLine = dataProcessPipeline
                .Append(mlContext.BinaryClassification.Trainers.FieldAwareFactorizationMachine(new string[] { "Features" }));

            Console.WriteLine("=============== Training the model ===============");
            var model = trainingPipeLine.Fit(trainingDataView);
            return model;*/

            var dataProcessPipeline = mlContext.Transforms.Text
                .FeaturizeText("userIdFeaturized", nameof(MovieRating.UserId))
                .Append(mlContext.Transforms.Text.FeaturizeText("movieIdFeaturized", nameof(MovieRating.MovieId)))
                .Append(mlContext.Transforms.Text.FeaturizeText("genresFeaturized", nameof(MovieRating.Genres)))
                .Append(mlContext.Transforms.Concatenate("Features", new string[] { "userIdFeaturized", "movieIdFeaturized", "genresFeaturized" }))
                .Append(mlContext.Transforms.NormalizeMinMax("Features"));

            ITransformer dataPrepTransformer = dataProcessPipeline.Fit(trainingDataView);
            IDataView transformedData = dataPrepTransformer.Transform(trainingDataView);

            var pipeline = dataProcessPipeline.Append(
                mlContext.BinaryClassification.Trainers.FieldAwareFactorizationMachine(new string[] { "Features" }));

            var dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "data_preparation_pipeline.zip");
            mlContext.Model.Save(dataPrepTransformer, transformedData.Schema, dataPath);

            var model = pipeline.Fit(trainingDataView);
            var modelDataView = model.Transform(trainingDataView);

            var modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "MovieRecommenderModel.zip");
            mlContext.Model.Save(model, modelDataView.Schema, modelPath);

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

        public void SaveModel(MLContext mlContext, DataViewSchema trainingDataViewSchema, ITransformer model)
        {
            var modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "MovieRecommenderModel.zip");

            Console.WriteLine("=============== Saving the model to a file ===============");
            mlContext.Model.Save(model, trainingDataViewSchema, modelPath);
        }
    }
}
