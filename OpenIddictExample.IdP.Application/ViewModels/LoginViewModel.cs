using System.ComponentModel.DataAnnotations;

namespace OpenIddictExample.IdP.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string User { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
