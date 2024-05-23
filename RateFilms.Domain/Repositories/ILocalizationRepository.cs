using System.Globalization;

namespace RateFilms.Domain.Repositories
{
    public interface ILocalizationRepository
    {
        public Dictionary<string, string> GetResource(CultureInfo culture);
    }
}
