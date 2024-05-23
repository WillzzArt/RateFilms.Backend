namespace RateFilms.Domain.Models.DomainModels
{
    public class Image
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public bool isPreview { get; set; }
        public string? Name { get; set; }
        public byte[]? Img { get; set; }
    }
}
