using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.Helpers
{
    public static class StringExtension
    {
        public static T ToEnum<T>(this string value, T defaultValue) where T : struct, Enum
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }

            T result;
            return Enum.TryParse(value, true, out result) ? result : defaultValue;
        }
    }
}
