using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.Repositories
{
    public interface IFilmRepository
    {
        IEnumerable<Film?> GetAllFilms();
    }
}
