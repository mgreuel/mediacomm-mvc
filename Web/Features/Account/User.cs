using System;
using System.Security.Cryptography;
using System.Text;

namespace MediaCommMvc.Web.Features.Account
{
    public class User
    {
        public string City { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string Id { get; set; }

        public bool IsAdmin { get; set; }

        public string LastName { get; set; }

        public DateTime? LastVisit { get; set; }

        public string MobilePhoneNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string SkypeNick { get; set; }

        public string Street { get; set; }

        public string UserName { get; set; }

        public string ZipCode { get; set; }

        public bool NotifyOnNewForumPost { get; set; }

        public bool NotifyOnNewPhotos { get; set; }

        public bool NotifyOnNewVideos { get; set; }

        public DateTime? LastForumsNotification { get; set; }

        public DateTime? LastPhotosNotification { get; set; }

        public DateTime? LastVideosNotification { get; set; }

        const string ConstantSalt = "ijdsUIZ67?&";

        protected string HashedPassword { get; private set; }

        private string passwordSalt;
        private string PasswordSalt
        {
            get
            {
                return this.passwordSalt ?? (this.passwordSalt = Guid.NewGuid().ToString("N"));
            }
            set
            {
                this.passwordSalt = value;
            }
        }

        public User SetPassword(string pwd)
        {
            this.HashedPassword = this.GetHashedPassword(pwd);
            return this;
        }

        private string GetHashedPassword(string pwd)
        {
            using (var sha = SHA256.Create())
            {
                byte[] computedHash = sha.ComputeHash(Encoding.Unicode.GetBytes(this.PasswordSalt + pwd + ConstantSalt));
                return Convert.ToBase64String(computedHash);
            }
        }

        public bool ValidatePassword(string maybePwd)
        {
            if (this.HashedPassword == null)
            {
                return true;
            }

            return this.HashedPassword == this.GetHashedPassword(maybePwd);
        }
    }
}
