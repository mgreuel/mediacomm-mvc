using System.Linq;
using System.Web.Mvc;

using Core.Forum;
using Core.Forum.Models;
using Core.Forum.ViewModels;

using MediaCommMvc.Web.Infrastructure;
using MediaCommMvc.Web.Infrastructure.Database;
using MediaCommMvc.Web.ViewModels;
using MediaCommMvc.Web.ViewModels.Forum;

using Microsoft.AspNet.Identity;

using PagedList;

namespace MediaCommMvc.Web.Controllers
{
    [Authorize]
    public partial class ForumController : Controller
    {
        private const int PostsPerPage = 15;

        private const int TopicsPerPage = 15;

        private readonly EfForumStorageService efForumStorageService;

        public ForumController(EfForumStorageService efForumStorageService)
        {
            this.efForumStorageService = efForumStorageService;
        }

        public virtual ActionResult Index(int page)
        {
            ForumOverview forumOverview = this.efForumStorageService.GetForumOverview(page, TopicsPerPage, this.User.Identity.GetUserName());

            StaticPagedList<TopicOverviewViewModel> topics = new StaticPagedList<TopicOverviewViewModel>(
                forumOverview.TopicsForCurrentPage, 
                page, 
                TopicsPerPage, 
                forumOverview.TotalNumberOfTopics);

            return this.View(new ForumViewModel { Topics = topics });
        }

        public virtual ActionResult CreateTopic()
        {
            return this.View(new CreateTopicViewModel());
        }

        [HttpPost]
        public virtual ActionResult CreateTopic(CreateTopicViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            int addTopic = this.efForumStorageService.AddTopic(viewModel.ToCommand(this.User.Identity.GetUserName()));

            return this.RedirectToAction(MVC.Forum.Index());
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
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            this.efForumStorageService.AddReply(viewModel.ToAddReplyCommand(this.User.Identity.GetUserName()));

            // Post post = new Post();
            // post.Text = HtmlSanitizer.Sanitize(viewModel.Text);
            // post.Topic = this.forumRepository.GetTopicById(viewModel.TopicId);
            // post.Author = this.userRepository.GetUserByName(this.User.Identity.Name);
            // post.Created = DateTime.UtcNow;

            // this.forumRepository.AddPost(post);

            // this.notificationSender.SendForumsNotification(post);

            // this.GetPostUrl(viewModel.TopicId, post)

            // todo go to last page
            return this.RedirectToAction(MVC.Forum.Topic().AddRouteValue("id", viewModel.TopicId));
        }

        public virtual ActionResult Topic(int id, int page)
        {
            // viewModel.Title = " Das ist nur ein Dummy titel " + id + " " + page;
            // viewModel.Id = id;

            // List<PostViewModel> posts = new List<PostViewModel>();

            // for (int i = 0; i < 500; i++)
            // {
            // if (i % 5 == 0)
            // {
            // posts.Add(
            // new PostViewModel
            // {
            // AuthorName = "author " + i, 
            // Created = DateTime.UtcNow.AddHours(-1), 
            // IsEditable = true, 
            // Text =
            // @"Bei Bedarf würden wir einen Transport (Bus o.ä.) von der Camera Obscura zum Unperfekthaus organisieren. Mit Bus & Bahn zur Camera Obscura zu fahren bzw. das Auto dort stehen zu lassen, hätte den Vorteil, dass man sich die Parkgebühren in Essen spart. Wir würden also gerne wissen, wer zur Camera Obscura kommt und eine Mitfahrgelegenheit bräuchte. Es wäre natürlich auch gut zu wissen, wer selber fährt und noch Plätze frei hat. Bitte gebt uns bis zum 17.6. Bescheid.", 
            // Id = i, 
            // ShowApprovalButton = true, 
            // Approvals = new List<string> { "abc says: Absolut!", "justANotherName says: Absolut!", "a really long username says: Absolut!" }
            // });
            // }

            // posts.Add(
            // new PostViewModel
            // {
            // AuthorName = "author " + i, 
            // Created = DateTime.UtcNow.AddHours(-1), 
            // IsEditable = true, 
            // Text = "Post No " + i, 
            // Id = i, 
            // ShowApprovalButton = true, 
            // Approvals = new List<string> { "abc says: Absolut!", "justANotherName says: Absolut!", "a really long username says: Absolut!" }
            // });
            // }

            // viewModel.Posts = posts.ToPagedList(page, PostsPerPage);
            TopicDetailsViewModel topicDetails = this.efForumStorageService.GetTopicDetailsViewModel(id, page, PostsPerPage, this.User.Identity.GetUserName());
            var viewModel = new PagedTopicDetailsViewModel(topicDetails);

            return this.View(viewModel);
        }
    }
}