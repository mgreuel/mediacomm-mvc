using System;
using System.Collections.Generic;

using Core.Forum.Models;

namespace Core.Forum.ViewModels
{
    public class PostViewModel
    {
        public PostViewModel(Post post)
        {
            this.Approvals = post.Approvals;
            this.AuthorName = post.AuthorName;
            this.Created = post.Created;
            this.Id = post.Id;
            this.Text = post.Text;
        }

        public string AuthorName { get; set; }

        public DateTime Created { get; set; }

        public int Id { get; set; }

        public bool IsEditable { get; set; }

        public string Text { get; set; }

        public bool ShowApprovalButton { get; set; }

        public IEnumerable<string> Approvals { get; set; }

        /*AuthorName.Equals(this.User.Identity.Name, StringComparison.OrdinalIgnoreCase) ||
                                 HttpContext.Current.User.IsInRole("Administrators")*/
    }
}