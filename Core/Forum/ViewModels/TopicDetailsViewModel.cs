using System.Collections.Generic;
using System.Linq;

using Core.Forum.Models;

namespace Core.Forum.ViewModels
{
    public class TopicDetailsViewModel
    {

        public TopicDetailsViewModel(TopicDetails topicDetails)
        {
            this.Title = topicDetails.Title;
            this.Id = topicDetails.Id;
            // todo: permissions
            /*AuthorName.Equals(this.User.Identity.Name, StringComparison.OrdinalIgnoreCase) ||
                         HttpContext.Current.User.IsInRole("Administrators")*/
            this.Posts = topicDetails.Posts.Select(post => new PostViewModel {Approvals = post.Approvals, AuthorName = post.AuthorName, Created = post.Created, Id = post.Id, Text = post.Text}).ToList();
        }

        public string Title { get; set; }

        public int Id { get; set; }

        public List<PostViewModel> Posts { get; set; }
    }
}