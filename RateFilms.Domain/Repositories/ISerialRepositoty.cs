using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Repositories
{
    /// <summary>
    /// Представляет репозиторий для работы с сериалами
    /// </summary>
    public interface ISerialRepositoty
    {
        /// <summary>
        /// Ищет все сериалы в базе данных включая в модель таблицу Favorite
        /// </summary>
        /// <returns>Список сериалов</returns>
        Task<IEnumerable<Serial>> GetAllSerialsWithFavorite();

        /// <summary>
        /// Находит сериал по его уникальному ключу включая таблицу Favorite
        /// </summary>
        /// <param name="serialId">Уникальный ключ сериала</param>
        /// <returns>Модель сериала</returns>
        Task<Serial?> GetSerialWithFavoriteById(Guid serialId);

        /// <summary>
        /// Сохраняет модель сериала в базу данных
        /// </summary>
        /// <param name="serial">Сторожевая модель сериала</param>
        /// <returns></returns>
        Task CreateAsync(SerialDbModel serial);

        /// <summary>
        /// Добавляет фильм в список избранного для авторизованного пользователя
        /// </summary>
        /// <param name="favoriteSerial">Модель таблицы Favorite</param>
        /// <param name="userName">Модель авторизованного пользователя</param>
        /// <returns></returns>
        Task SetFavoriteSerial(FavoriteMovie favoriteSerial, User user);
    }
}
