namespace RateFilms.Domain.Models.StorageModels
{
    public class SerialDbModel : MovieDbModel
    {
        public Guid? PreviewImageId { get; set; }
        public ImageDbModel? PreviewImage { get; set; }
        public IEnumerable<SeasonDbModel> Seasons { get; set; }
    }
}
