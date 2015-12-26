using System.ComponentModel.DataAnnotations;

namespace MediaCommMvc.Web.Features.Account.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resources.General))]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Login), ErrorMessageResourceName = "EmailAddressErrorMessage")]
        [Display(Name = "Email", ResourceType = typeof(Resources.Login))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resources.General))]
        [StringLength(100, ErrorMessageResourceName = "MinimumLength", ErrorMessageResourceType = typeof(Resources.General), MinimumLength = 3)]
        [Display(Name = "Username", ResourceType = typeof(Resources.Login))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resources.General))]
        [StringLength(100, ErrorMessageResourceName = "MinimumLength", ErrorMessageResourceType = typeof(Resources.General), MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Login))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Login))]
        [Compare("Password", ErrorMessageResourceName = "PasswordsDoNotMatch", ErrorMessageResourceType = typeof(Resources.Login))]
        public string ConfirmPassword { get; set; }

        [Display(Name = "RegistrationKey", ResourceType = typeof(Resources.Login))]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resources.General))]
        public string RegistrationCode { get; set; }
    }
}