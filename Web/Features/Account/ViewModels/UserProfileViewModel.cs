namespace MediaCommMvc.Web.Features.Account.ViewModels
{
    public class UserProfileViewModel
    {
        public UserProfileViewModel(User user)
        {
            this.Username = user.UserName;
            this.Firstname = user.FirstName;
            this.Lastname = user.LastName;
            this.City = user.City;
            this.DateOfBirth = user.DateOfBirth?.ToString("d");
            this.Email = user.Email;
            this.LastVisit = $"{user.LastVisit?.ToLocalTime():g}";
            this.MobilePhoneNumber = user.MobilePhoneNumber;
            this.PhoneNumber = user.PhoneNumber;
            this.Street = user.Street;
            this.SkypeNick = user.SkypeNick;
            this.ZipCode = user.ZipCode;
        }

        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string DateOfBirth { get; set; }

        public string SkypeNick { get; set; }

        public string PhoneNumber { get; set; }

        public string MobilePhoneNumber { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string LastVisit { get; set; }
    }
}
