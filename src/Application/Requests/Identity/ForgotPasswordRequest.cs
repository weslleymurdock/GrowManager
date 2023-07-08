using System.ComponentModel.DataAnnotations;

namespace GrowManager.Application.Requests.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}