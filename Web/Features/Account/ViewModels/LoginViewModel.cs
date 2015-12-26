using System.ComponentModel.DataAnnotations;

namespace MediaCommMvc.Web.Features.Account.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username", ResourceType = typeof(Resources.Login))]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Login))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Resources.Login))]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}