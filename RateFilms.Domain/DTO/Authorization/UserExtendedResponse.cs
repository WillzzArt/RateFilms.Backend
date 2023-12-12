using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Authorization
{
    public class UserExtendedResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string UserName { get; set; }
        public Image? Image { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }
        public Dictionary<string, int> StatisticStatus { get; set; }
    }
}
