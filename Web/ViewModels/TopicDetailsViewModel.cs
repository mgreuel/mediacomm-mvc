using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

using MediaCommMvc.Web.Models.Forum.Models;

namespace MediaCommMvc.Web.ViewModels
{
    public class TopicDetailsViewModel
    {
        public TopicDetailsViewModel(Topic topic, int pageNumber, int postsPerPage, IPrincipal currentUser)
        {
            this.Title = topic.Title;
            this.Id = topic.TopicId;
            this.PostsPerPage = postsPerPage;
            this.PageNumber = pageNumber;
            this.TotalNumberOfPosts = topic.Posts.Count;
            int pageCount = this.TotalNumberOfPosts > 0
                            ? (int)Math.Ceiling(this.TotalNumberOfPosts / (double)postsPerPage)
                            : 0;

            this.IsLastPage = pageNumber >= pageCount;
            
            this.PostsForCurrentPage =
                topic.Posts
                    .OrderBy(post => post.Created)
                    .Skip((pageNumber - 1) * postsPerPage)
                    .Take(postsPerPage)
                    .Select(post => new PostViewModel(post, currentUser))
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