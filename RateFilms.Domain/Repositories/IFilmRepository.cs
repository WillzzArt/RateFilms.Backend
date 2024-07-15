using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.Repositories
{
    /// <summary>
    /// Представляет репозиторий для работы с фильмами
    /// </summary>
    public interface IFilmRepository
    {
        /// <summary>
        /// Ищет все фильмы в базе данных включая в модель таблицу Favorite
        /// </summary>
        /// <returns>Список фильмов</returns>
        Task<IEnumerable<Film>> GetAllFilmsWithFavorite();

        /// <summary>
        /// Находит фильм по его уникальному ключу включая таблицу Favorite
        /// </summary>
        /// <param name="filmId">Уникальный ключ фильма</param>
        /// <returns>Модель фильма</returns>
        Task<Film?> GetFilmWithFavoriteById(Guid filmId);

        /// <summary>
        /// Ищет фильмы с непроверенными рецензиями для администратора
        /// </summary>
        /// <returns>Список фильмов</returns>
        Task<IEnumerable<Film>> GetFilmsWithUncheckedReview();
    }
}
