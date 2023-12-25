using RateFilms.Domain.Models.Authorization;

namespace RateFilms.Domain.Models.DomainModels
{
    public class Favorite
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public bool IsFavorite { get; set; } = false;
        public StatusMovie Status { get; set; } = StatusMovie.None;
        public int? Score { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

        /// <summary>
        /// Высчитывает средний рейтинг по списку избранных
        /// </summary>
        /// <param name="favorites">Список избранных фильма или серила</param>
        /// <returns>Средний рейтинг</returns>
        public static double? GetAvgRating(IEnumerable<Favorite>? favorites)
        {
            if (favorites != null)
            {
                var avgRating = 0.0;
                if (favorites.Any(fav => fav.Score != null))
                {
                    avgRating = favorites
                        .Where(fav => fav.Score != null)
                        .Select(fav => (int)fav.Score!)
                        .Average();
                }

                if (avgRating > 0)
                {
                    return Math.Round((double)avgRating, 2);
                }

            }
            return null;
        }

        public static int GetCountFavorite(IEnumerable<Favorite>? favorites)
        {
            if (favorites != null)
            {
                return favorites.Where(x => x.IsFavorite).Count();
            }
            return 0;
        }

        /// <summary>
        /// Считает оценки людей
        /// </summary>
        /// <param name="favorites">Список избранных фильма или серила</param>
        /// <returns>Словарь с ключем "оценка" и значением "количество людей"</returns>
        public static Dictionary<int, int> GetRatings(IEnumerable<Favorite>? favorites)
        {
            var ratings = new Dictionary<int, int>(4)
            {
                { 1, 0 },
                { 2, 0 },
                { 3, 0 },
                { 4, 0 },
                { 5, 0 }
            };

            if (favorites != null)
            {
                //var ratings = new Dictionary<int, int>(4);

                for (var i = 1; i <= 5; i++)
                {
                    ratings[i] = favorites.Where(x => x.Score != null && x.Score == i).Count();
                }
            }

            return ratings;
        }

        /// <summary>
        /// Считает количество людей в категориях
        /// </summary>
        /// <param name="favorites">Список избранных фильма или серила</param>
        /// <returns>Словарь с ключем "категория" и значением "количество людей"</returns>
        public static Dictionary<string, int> GetStatusOfPeople(IEnumerable<Favorite>? favorites)
        {
            var statuses = new Dictionary<string, int>(5)
            {
                { StatusMovie.Looking.ToString(), 0 },
                { StatusMovie.InThePlans.ToString(), 0 },
                { StatusMovie.Viewed.ToString(), 0 },
                { StatusMovie.Postponed.ToString(), 0 },
                { StatusMovie.Abandoned.ToString(), 0 }
            };

            if (favorites != null)
            {
                //var statuses = new Dictionary<string, int>(5);
                for (var i = 1; i <= 5; i++)
                {
                    statuses[((StatusMovie)i).ToString()] = favorites.Where(x => x.Status == (StatusMovie)i).Count();
                }
            }
            return statuses;
        }
    }
}
