using System.ComponentModel.DataAnnotations;

namespace MediaCommMvc.Web.Features.Account.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
