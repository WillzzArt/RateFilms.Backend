using RateFilms.Domain.Repositories;
using System.Globalization;

namespace RateFilms.Application.Services.Localization
{
    public class LocalizationService
    {
        private readonly ILocalizationRepository _localizationRepository;
        private Dictionary<CultureInfo, Dictionary<string, string>> _encyclopedia = new();
        public CultureInfo CurrentCulture { get; private set; } = CultureInfo.CurrentCulture;
        public IReadOnlyList<CultureInfo> Languages { get; private set; }


        public LocalizationService(ILocalizationRepository localizationRepository)
        {
            _localizationRepository = localizationRepository;

            Languages = new List<CultureInfo>()
            {
                CultureInfo.GetCultureInfo("en-GB"),
                CultureInfo.GetCultureInfo("ru-RU")
            };
        }

        public void SetLanguage(CultureInfo culture)
        {
            if(!Languages.Contains(culture))
            {
                culture = CultureInfo.CurrentCulture;
            }

            CurrentCulture = culture;
        }

        public string this[string selector]
        {
            set { }
            get => Get(selector, CurrentCulture);
        }

        private string Get(string selector, CultureInfo culture)
        {
            string value = null;

            _encyclopedia.TryGetValue(culture, out var lanDictionary);

            lanDictionary?.TryGetValue(selector, out value);

            return value;
        }

        public void LoadTranslation()
        {
            foreach (var culture in Languages)
            {
                var resource = _localizationRepository.GetResource(culture);

                _encyclopedia[culture] = resource;
            }
        }


    }
}
