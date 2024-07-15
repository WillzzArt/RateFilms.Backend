namespace RateFilms.Domain.Models.StorageModels
{
    public class FilmDbModel : MovieDbModel
    {
        public int Duration { get; set; }
        public IEnumerable<ImageDbModel> Images { get; set; }
    }
}
