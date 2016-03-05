// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace MediaCommMvc.Web.Controllers
{
    public partial class ForumController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ForumController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AddApproval()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddApproval);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AnswerPoll()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AnswerPoll);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Index()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult EditTopic()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditTopic);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult EditPost()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditPost);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult FirstNewPostInTopic()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.FirstNewPostInTopic);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Reply()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Reply);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Topic()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Topic);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult MarkTopicAsRead()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MarkTopicAsRead);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ForumController Actions { get { return MVC.Forum; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Forum";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Forum";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string AddApproval = "AddApproval";
            public readonly string AnswerPoll = "AnswerPoll";
            public readonly string Index = "Index";
            public readonly string CreateTopic = "CreateTopic";
            public readonly string EditTopic = "EditTopic";
            public readonly string EditPost = "EditPost";
            public readonly string FirstNewPostInTopic = "FirstNewPostInTopic";
            public readonly string Reply = "Reply";
            public readonly string Topic = "Topic";
            public readonly string MarkTopicAsRead = "MarkTopicAsRead";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string AddApproval = "AddApproval";
            public const string AnswerPoll = "AnswerPoll";
            public const string Index = "Index";
            public const string CreateTopic = "CreateTopic";
            public const string EditTopic = "EditTopic";
            public const string EditPost = "EditPost";
            public const string FirstNewPostInTopic = "FirstNewPostInTopic";
            public const string Reply = "Reply";
            public const string Topic = "Topic";
            public const string MarkTopicAsRead = "MarkTopicAsRead";
        }


        static readonly ActionParamsClass_AddApproval s_params_AddApproval = new ActionParamsClass_AddApproval();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AddApproval AddApprovalParams { get { return s_params_AddApproval; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AddApproval
        {
            public readonly string topicId = "topicId";
            public readonly string postIndex = "postIndex";
        }
        static readonly ActionParamsClass_AnswerPoll s_params_AnswerPoll = new ActionParamsClass_AnswerPoll();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AnswerPoll AnswerPollParams { get { return s_params_AnswerPoll; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AnswerPoll
        {
            public readonly string answer = "answer";
        }
        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_EditTopic s_params_EditTopic = new ActionParamsClass_EditTopic();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EditTopic EditTopicParams { get { return s_params_EditTopic; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EditTopic
        {
            public readonly string viewModel = "viewModel";
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_EditPost s_params_EditPost = new ActionParamsClass_EditPost();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EditPost EditPostParams { get { return s_params_EditPost; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EditPost
        {
            public readonly string topicId = "topicId";
            public readonly string postIndex = "postIndex";
            public readonly string viewModel = "viewModel";
        }
        static readonly ActionParamsClass_FirstNewPostInTopic s_params_FirstNewPostInTopic = new ActionParamsClass_FirstNewPostInTopic();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_FirstNewPostInTopic FirstNewPostInTopicParams { get { return s_params_FirstNewPostInTopic; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_FirstNewPostInTopic
        {
            public readonly string topicId = "topicId";
        }
        static readonly ActionParamsClass_Reply s_params_Reply = new ActionParamsClass_Reply();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Reply ReplyParams { get { return s_params_Reply; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Reply
        {
            public readonly string viewModel = "viewModel";
        }
        static readonly ActionParamsClass_Topic s_params_Topic = new ActionParamsClass_Topic();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Topic TopicParams { get { return s_params_Topic; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Topic
        {
            public readonly string id = "id";
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_MarkTopicAsRead s_params_MarkTopicAsRead = new ActionParamsClass_MarkTopicAsRead();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_MarkTopicAsRead MarkTopicAsReadParams { get { return s_params_MarkTopicAsRead; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_MarkTopicAsRead
        {
            public readonly string id = "id";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string EditPost = "EditPost";
                public readonly string EditTopic = "EditTopic";
                public readonly string Index = "Index";
                public readonly string Reply = "Reply";
                public readonly string Topic = "Topic";
            }
            public readonly string EditPost = "~/Views/Forum/EditPost.cshtml";
            public readonly string EditTopic = "~/Views/Forum/EditTopic.cshtml";
            public readonly string Index = "~/Views/Forum/Index.cshtml";
            public readonly string Reply = "~/Views/Forum/Reply.cshtml";
            public readonly string Topic = "~/Views/Forum/Topic.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ForumController : MediaCommMvc.Web.Controllers.ForumController
    {
        public T4MVC_ForumController() : base(Dummy.Instance) { }

        [NonAction]
        partial void AddApprovalOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string topicId, int postIndex);

        [NonAction]
        public override System.Web.Mvc.ActionResult AddApproval(string topicId, int postIndex)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddApproval);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "topicId", topicId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "postIndex", postIndex);
            AddApprovalOverride(callInfo, topicId, postIndex);
            return callInfo;
        }

        [NonAction]
        partial void AnswerPollOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, MediaCommMvc.Web.Features.Forum.ViewModels.PollUserAnswerInput answer);

        [NonAction]
        public override System.Web.Mvc.ActionResult AnswerPoll(MediaCommMvc.Web.Features.Forum.ViewModels.PollUserAnswerInput answer)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AnswerPoll);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "answer", answer);
            AnswerPollOverride(callInfo, answer);
            return callInfo;
        }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int page);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index(int forumPage)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", forumPage);
            IndexOverride(callInfo, forumPage);
            return callInfo;
        }

        [NonAction]
        partial void CreateTopicOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult CreateTopic()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CreateTopic);
            CreateTopicOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void EditTopicOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, MediaCommMvc.Web.Features.Forum.ViewModels.EditTopicViewModel viewModel);

        [NonAction]
        public override System.Web.Mvc.ActionResult EditTopic(MediaCommMvc.Web.Features.Forum.ViewModels.EditTopicViewModel viewModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditTopic);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "viewModel", viewModel);
            EditTopicOverride(callInfo, viewModel);
            return callInfo;
        }

        [NonAction]
        partial void EditPostOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string topicId, int postIndex);

        [NonAction]
        public override System.Web.Mvc.ActionResult EditPost(string topicId, int postIndex)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditPost);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "topicId", topicId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "postIndex", postIndex);
            EditPostOverride(callInfo, topicId, postIndex);
            return callInfo;
        }

        [NonAction]
        partial void EditTopicOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult EditTopic(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditTopic);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            EditTopicOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void EditPostOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, MediaCommMvc.Web.Features.Forum.ViewModels.EditPostViewModel viewModel);

        [NonAction]
        public override System.Web.Mvc.ActionResult EditPost(MediaCommMvc.Web.Features.Forum.ViewModels.EditPostViewModel viewModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditPost);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "viewModel", viewModel);
            EditPostOverride(callInfo, viewModel);
            return callInfo;
        }

        [NonAction]
        partial void FirstNewPostInTopicOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string topicId);

        [NonAction]
        public override System.Web.Mvc.ActionResult FirstNewPostInTopic(string topicId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.FirstNewPostInTopic);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "topicId", topicId);
            FirstNewPostInTopicOverride(callInfo, topicId);
            return callInfo;
        }

        [NonAction]
        partial void ReplyOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, MediaCommMvc.Web.Features.Forum.ViewModels.ReplyViewModel viewModel);

        [NonAction]
        public override System.Web.Mvc.ActionResult Reply(MediaCommMvc.Web.Features.Forum.ViewModels.ReplyViewModel viewModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Reply);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "viewModel", viewModel);
            ReplyOverride(callInfo, viewModel);
            return callInfo;
        }

        [NonAction]
        partial void TopicOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id, int page);

        [NonAction]
        public override System.Web.Mvc.ActionResult Topic(string id, int page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Topic);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            TopicOverride(callInfo, id, page);
            return callInfo;
        }

        [NonAction]
        partial void MarkTopicAsReadOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult MarkTopicAsRead(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MarkTopicAsRead);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            MarkTopicAsReadOverride(callInfo, id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
