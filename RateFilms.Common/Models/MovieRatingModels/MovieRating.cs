using Microsoft.ML.Data;

namespace RateFilms.Common.Models.MovieRatingModels
{
    public class MovieRating
    {
        [ColumnName("UserId")]
        public string UserId { get; set; }
        [ColumnName("MovieId")]
        public string MovieId { get; set; }
        [ColumnName("Genres")]
        public string[] Genres { get; set; }
        [ColumnName("Label")]
        public bool Label { get; set; }
    }
}
