using System.ComponentModel.DataAnnotations;

namespace MediaCommMvc.Web.Features.Account.ViewModels
{
    public class EditUserProfileViewModel
    {
        [Display(Name = "UserName", ResourceType = typeof(Resources.Users))]
        public string UserName { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Resources.Users))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resources.Users))]
        public string LastName { get; set; }

        [Display(Name = "EMailAddress", ResourceType = typeof(Resources.Users))]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Login), ErrorMessageResourceName = "EmailAddressErrorMessage")]
        public string Email { get; set; }

        [Display(Name = "DateOfBirth", ResourceType = typeof(Resources.Users))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd")]
        public string DateOfBirth { get; set; }

        [Display(Name = "SkypeNick", ResourceType = typeof(Resources.Users))]
        public string SkypeNick { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Resources.Users))]
        public string PhoneNumber { get; set; }

        [Display(Name = "MobilePhoneNumber", ResourceType = typeof(Resources.Users))]
        public string MobilePhoneNumber { get; set; }

        [Display(Name = "Street", ResourceType = typeof(Resources.Users))]
        public string Street { get; set; }

        [Display(Name = "ZipCode", ResourceType = typeof(Resources.Users))]
        public string ZipCode { get; set; }

        [Display(Name = "City", ResourceType = typeof(Resources.Users))]
        public string City { get; set; }
    }
}