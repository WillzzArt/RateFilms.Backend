using System.ComponentModel.DataAnnotations;

namespace RateFilms.Domain.DTO.Authorization
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Укажите никнейм или электронный адрес")]
        public string UserLogin { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Укажите пароль")]
        public string Password { get; set; }
    }
}
