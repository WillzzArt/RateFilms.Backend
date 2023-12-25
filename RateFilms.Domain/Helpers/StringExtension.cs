namespace RateFilms.Domain.Helpers
{
    public static class StringExtension
    {
        public static T ToEnum<T>(this string? value, T defaultValue) where T : struct, Enum
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }

            T result;
            return Enum.TryParse(value.ToLower(), true, out result) ? result : defaultValue;
        }
    }
}
