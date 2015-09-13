using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

using MediaCommMvc.Web.Models;
using MediaCommMvc.Web.Models.Forum.Models;

namespace MediaCommMvc.Web.Forum.ViewModels
{
    public class PostViewModel
    {
        public PostViewModel(Post post)
        {
            this.Approvals = post.Approvals ?? new List<string>();
            this.AuthorName = post.AuthorName;
            this.Created = post.Created;
            //this.Id = post.Id;
            this.Text = post.Text;
            this.IndexInTopic = post.IndexInTopic;
        }

        public string AuthorName { get; set; }

        public DateTime Created { get; set; }

        //public int Id { get; set; }

        public bool IsEditable(IPrincipal currentUser)
        {
            return currentUser.Identity.Name.Equals(this.AuthorName, StringComparison.OrdinalIgnoreCase) || currentUser.IsInRole(UserRoles.Administrator);
        }

        public string Text { get; set; }

        public bool ShowApprovalButton(IPrincipal currentUser)
        {
                return !currentUser.Identity.Name.Equals(this.AuthorName, StringComparison.OrdinalIgnoreCase)
                       && !this.Approvals.Any(s => s.Equals(currentUser.Identity.Name, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<string> Approvals { get; set; }

        public int IndexInTopic { get; set; }
    }
}