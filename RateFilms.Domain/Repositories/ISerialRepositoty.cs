using RateFilms.Domain.Models.DomainModels;

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
        /// Ищет сериалы с непроверенными рецензиями для администратора
        /// </summary>
        /// <returns>Список фильмов</returns>
        Task<IEnumerable<Serial>> GetSerialsWithUncheckedReview();
    }
}
