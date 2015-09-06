using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

using MediaCommMvc.Web.Models;
using MediaCommMvc.Web.Models.Forum.Models;

namespace MediaCommMvc.Web.ViewModels.Forum
{
    public class PostViewModel
    {
        private readonly IPrincipal currentUser;

        public PostViewModel(Post post, IPrincipal currentUser)
        {
            this.currentUser = currentUser;
            this.Approvals = post.Approvals;
            this.AuthorName = post.AuthorName;
            this.Created = post.Created;
            this.Id = post.Id;
            this.Text = post.Text;
        }

        public string AuthorName { get; set; }

        public DateTime Created { get; set; }

        public int Id { get; set; }

        public bool IsEditable => this.currentUser.Identity.Name.Equals(this.AuthorName, StringComparison.OrdinalIgnoreCase) || this.currentUser.IsInRole(UserRoles.Administrator);

        public string Text { get; set; }

        public bool ShowApprovalButton
        {
            get
            {
                return !this.currentUser.Identity.Name.Equals(this.AuthorName, StringComparison.OrdinalIgnoreCase)
                       && !this.Approvals.Any(s => s.Equals(this.currentUser.Identity.Name, StringComparison.OrdinalIgnoreCase));
            }
        }

        public IEnumerable<string> Approvals { get; set; }
    }
}