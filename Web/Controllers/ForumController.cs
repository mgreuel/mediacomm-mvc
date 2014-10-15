using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MediaCommMvc.Web.Models.Forum;
using MediaCommMvc.Web.ViewModels.Forum;

using PagedList;

namespace MediaCommMvc.Web.Controllers
{
    public partial class ForumController : Controller
    {
        private const int PostsPerPage = 25;

        private const int TopicsPerPage = 15;

        public virtual ActionResult Index(int page)
        {
            List<TopicOverviewViewModel> topics = new List<TopicOverviewViewModel>();

            for (int i = 0; i < 50; i++)
            {
                topics.Add(
                    new TopicOverviewViewModel
                        {
                            CreatedBy = "User " + i,
                            DisplayPriority = TopicDisplayPriority.None,
                            Id = i,
                            LastPostAuthor = "author " + i,
                            LastPostTime = string.Format("{0:g}", DateTime.UtcNow.AddDays(-1)),
                            PostCount = i.ToString(),
                            ReadByCurrentUser = i % 2 == 0,
                            Title = "Topic No " + i
                        });
            }

            ForumViewModel viewModel = new ForumViewModel { Topics = topics.ToPagedList(page, TopicsPerPage) };
            return this.View(viewModel);
        }

        public virtual ActionResult CreateTopic()
        {
            return this.View();
        }

        [HttpPost]
        public virtual ActionResult DeletePost(int id)
        {
            return new EmptyResult();
        }

        public virtual ActionResult EditPost(int id)
        {
            return new EmptyResult();
        }

        public virtual ActionResult Topic(int id, int page)
        {
            var viewModel = new TopicDetailsViewModel();

            viewModel.Title = id + " " + page;
            
            List<PostViewModel> posts = new List<PostViewModel>();

            for (int i = 0; i < 50; i++)
            {
                posts.Add(
                    new PostViewModel
                    {
                        AuthorName = "author " + i,
                        Created = string.Format("{0:g}", DateTime.UtcNow.AddHours(-1)),
                        IsEditable = true,
                        Text = "Post No " + i,
                        Id = i
                    });
            }

            viewModel.Posts = posts.ToPagedList(page, PostsPerPage);

            return this.View(viewModel);
        }
    }
}