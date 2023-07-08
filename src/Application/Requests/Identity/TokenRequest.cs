using System.ComponentModel.DataAnnotations;

namespace GrowManager.Application.Requests.Identity
{
    public class TokenRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}