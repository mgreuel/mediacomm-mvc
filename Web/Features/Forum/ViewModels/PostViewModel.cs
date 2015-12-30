using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

using MediaCommMvc.Web.Features.Account;
using MediaCommMvc.Web.Features.Forum.Models;

namespace MediaCommMvc.Web.Features.Forum.ViewModels
{
    public class PostViewModel
    {
        public PostViewModel(Post post, bool topicIsWiki)
        {
            this.Approvals = post.Approvals ?? new List<string>();
            this.AuthorName = post.AuthorName;
            this.CreatedAt = $"{post.CreatedAt.ToLocalTime():g}"; 
            this.Text = post.Text;
            this.IndexInTopic = post.IndexInTopic;
            this.IsWiki = topicIsWiki && post.IndexInTopic == 0;
        }

        public string AuthorName { get; }

        public string CreatedAt { get;  }

        public bool IsEditable(IPrincipal currentUser)
        {
            return currentUser.Identity.Name.Equals(this.AuthorName, StringComparison.OrdinalIgnoreCase) 
                || currentUser.IsInRole(UserRoles.Administrator) 
                || this.IsWiki;
        }

        public bool IsWiki { get; set; }

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