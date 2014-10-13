using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MediaCommMvc.Web.Models.Forum;
using MediaCommMvc.Web.ViewModels.Forum;

namespace MediaCommMvc.Web.Controllers
{
    public partial class ForumController : Controller
    {
        // GET: Forums
        public virtual ActionResult Index()
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
                            Title = "Post No " + i
                        });
            }

            ForumViewModel viewModel = new ForumViewModel { Topics = topics };
            return this.View(viewModel);
        }

        public virtual ActionResult CreateTopic()
        {
            return this.View();
        }

        public virtual ActionResult Topic()
        {
            return this.View();
        }
    }
}