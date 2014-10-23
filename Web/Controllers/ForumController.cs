using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Antlr.Runtime;

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

            for (int i = 0; i < 500; i++)
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
        
        
        [HttpPost]
        public virtual ActionResult Reply(ReplyViewModel viewModel)
        {
            //Post post = new Post();
            //post.Text = HtmlSanitizer.Sanitize(viewModel.Text);
            //post.Topic = this.forumRepository.GetTopicById(viewModel.TopicId);
            //post.Author = this.userRepository.GetUserByName(this.User.Identity.Name);
            //post.Created = DateTime.UtcNow;

            //this.forumRepository.AddPost(post);

            //this.notificationSender.SendForumsNotification(post);

            //this.GetPostUrl(viewModel.TopicId, post)

            return new EmptyResult();
        }


        public virtual ActionResult Topic(int id, int page)
        {
            var viewModel = new TopicDetailsViewModel();

            viewModel.Title = " Das ist nur ein Dummy titel " + id + " " + page;
            viewModel.Id = id;

            List<PostViewModel> posts = new List<PostViewModel>();

            for (int i = 0; i < 500; i++)
            {
                if (i % 5 == 0)
                {
                    posts.Add(
    new PostViewModel
    {
        AuthorName = "author " + i,
        Created = string.Format("{0:g}", DateTime.UtcNow.AddHours(-1)),
        IsEditable = true,
        Text = @"Bei Bedarf würden wir einen Transport (Bus o.ä.) von der Camera Obscura zum Unperfekthaus organisieren. Mit Bus & Bahn zur Camera Obscura zu fahren bzw. das Auto dort stehen zu lassen, hätte den Vorteil, dass man sich die Parkgebühren in Essen spart. Wir würden also gerne wissen, wer zur Camera Obscura kommt und eine Mitfahrgelegenheit bräuchte. Es wäre natürlich auch gut zu wissen, wer selber fährt und noch Plätze frei hat. Bitte gebt uns bis zum 17.6. Bescheid.",
        Id = i,
        ShowApprovalButton = true,
        Approvals = new List<string> { "abc says: Absolut!", "justANotherName says: Absolut!", "a really long username says: Absolut!" }
    });
                }

                posts.Add(
                    new PostViewModel
                    {
                        AuthorName = "author " + i,
                        Created = string.Format("{0:g}", DateTime.UtcNow.AddHours(-1)),
                        IsEditable = true,
                        Text = "Post No " + i,
                        Id = i,
                        ShowApprovalButton = true,
                        Approvals = new List<string> { "abc says: Absolut!", "justANotherName says: Absolut!", "a really long username says: Absolut!" }
                    });
            }

            viewModel.Posts = posts.ToPagedList(page, PostsPerPage);

            return this.View(viewModel);
        }
    }
}