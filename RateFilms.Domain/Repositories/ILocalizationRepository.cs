using RateFilms.Common.Models.Localization;
using System.Globalization;

namespace RateFilms.Domain.Repositories
{
    public interface ILocalizationRepository
    {
        Dictionary<string, string> GetResource(CultureInfo culture);
        Task CreateResource(Resource resource, string culture);
    }
}
