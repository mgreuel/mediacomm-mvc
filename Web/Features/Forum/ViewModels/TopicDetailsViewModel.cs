using System;
using System.Collections.Generic;

using PagedList;

namespace MediaCommMvc.Web.Features.Forum.ViewModels
{
    public class TopicDetailsViewModel
    {
        public ShowPollViewModel Poll { get; set; }

        public int TotalNumberOfPosts { get; set; }

        public int PageNumber { get; set; }

        public bool IsFirstPage => this.PageNumber == 1;

        public bool IsLastPage => this.PageNumber >= this.PageCount;

        public string Title { get; set; }

        private int PageCount => this.TotalNumberOfPosts > 0
            ? (int)Math.Ceiling(this.TotalNumberOfPosts / (double)ForumOptions.PostsPerPage)
            : 0;

        public string Id { get; set; }

        public IEnumerable<PostViewModel> Posts { get; set; }

        public IPagedList<PostViewModel> PostsForCurrentpage
            => new StaticPagedList<PostViewModel>(this.Posts, this.PageNumber, ForumOptions.PostsPerPage, this.TotalNumberOfPosts);

        public IEnumerable<string> ExcludedUsernames { get; set; }
    }
}