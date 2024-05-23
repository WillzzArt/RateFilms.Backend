using System.Globalization;

namespace RateFilms.WebAPI.Helpers
{
    public static class CultureHelper
    {
        public static CultureInfo GetCurrentCulture(HttpRequest request)
        {
            var language = request.Headers["Accept-Language"].ToString().Split(",").First();
            return new CultureInfo(language);
        }
    }
}
