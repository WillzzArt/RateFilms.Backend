using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;

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
        /// Сохраняет модель фильма в базу данных
        /// </summary>
        /// <param name="film">Сторожевая модель фильма</param>
        /// <returns></returns>
        Task CreateAsync(FilmDbModel film);

        /// <summary>
        /// Добавляет фильм в список избранного для авторизованного пользователя
        /// </summary>
        /// <param name="favoriteFilm">Модель таблицы Favorite</param>
        /// <param name="user">Модель авторизованного пользователя</param>
        /// <returns></returns>
        Task SetFavoriteFilm(FavoriteMovie favoriteFilm, User user);

        /// <summary>
        /// Находит фильм по его уникальному ключу
        /// </summary>
        /// <param name="filmId">Уникальный ключ фильма</param>
        /// <returns>Модель фильма</returns>
        //Task<Film?> GetFilmById(Guid filmId);

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
