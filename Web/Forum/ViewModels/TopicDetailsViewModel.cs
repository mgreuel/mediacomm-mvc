using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

using MediaCommMvc.Web.Infrastructure;
using MediaCommMvc.Web.Models.Forum.Models;

using PagedList;

namespace MediaCommMvc.Web.Forum.ViewModels
{
    public class TopicDetailsViewModel
    {
        public ShowPollViewModel Poll { get; set; }

        public int TotalNumberOfPosts { get; set; }

        public int PageNumber { get; set; }

        public bool IsLastPage => this.PageNumber >= this.PageCount;

        public string Title { get; set; }

        private int PageCount => this.TotalNumberOfPosts > 0
            ? (int)Math.Ceiling(this.TotalNumberOfPosts / (double)ForumOptions.PostsPerPage)
            : 0;

        public int Id { get; private set; }

        public IEnumerable<PostViewModel> Posts { get; set; }

        public IPagedList<PostViewModel> PostsForCurrentpage
            => new StaticPagedList<PostViewModel>(this.Posts, this.PageNumber, ForumOptions.PostsPerPage, this.TotalNumberOfPosts);
    }
}