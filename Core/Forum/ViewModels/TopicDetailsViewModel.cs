using System;
using System.Collections.Generic;
using System.Linq;

using Core.Forum.Models;

namespace Core.Forum.ViewModels
{
    public class TopicDetailsViewModel
    {
        public TopicDetailsViewModel(TopicDetails topicDetails, int pageNumber, int postsPerPage, string currentUser)
        {
            this.Title = topicDetails.Title;
            this.Id = topicDetails.TopicId;
            // todo: permissions
            /*AuthorName.Equals(this.User.Identity.Name, StringComparison.OrdinalIgnoreCase) ||
                         HttpContext.Current.User.IsInRole("Administrators")*/

            this.PostsPerPage = postsPerPage;
            this.PageNumber = pageNumber;
            this.TotalNumberOfPosts = topicDetails.Posts.Count;
            int pageCount = this.TotalNumberOfPosts > 0
                            ? (int)Math.Ceiling(this.TotalNumberOfPosts / (double)postsPerPage)
                            : 0;

            this.IsLastPage = pageNumber >= pageCount;
            
            this.PostsForCurrentPage =
                topicDetails.Posts
                    .OrderBy(post => post.Created)
                    .Skip((pageNumber - 1) * postsPerPage)
                    .Take(postsPerPage)
                    .Select(post => new PostViewModel(post))
                    .ToList();
        }

        public int TotalNumberOfPosts { get; private set; }

        public int PostsPerPage { get; private set; }

        public int PageNumber { get; private set; }

        public bool IsLastPage { get; private set; }

        public string Title { get; private set; }

        public int Id { get; private set; }

        public IEnumerable<PostViewModel> PostsForCurrentPage { get; private set; }
    }
}