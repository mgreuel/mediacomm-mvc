// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
#pragma warning disable 1591, 3008, 3009, 0108
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
            public readonly string Index = "Index";
            public readonly string CreateTopic = "CreateTopic";
            public readonly string EditTopic = "EditTopic";
            public readonly string EditPost = "EditPost";
            public readonly string Reply = "Reply";
            public readonly string Topic = "Topic";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string AddApproval = "AddApproval";
            public const string Index = "Index";
            public const string CreateTopic = "CreateTopic";
            public const string EditTopic = "EditTopic";
            public const string EditPost = "EditPost";
            public const string Reply = "Reply";
            public const string Topic = "Topic";
        }


        static readonly ActionParamsClass_AddApproval s_params_AddApproval = new ActionParamsClass_AddApproval();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AddApproval AddApprovalParams { get { return s_params_AddApproval; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AddApproval
        {
            public readonly string postId = "postId";
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
            public readonly string id = "id";
            public readonly string viewModel = "viewModel";
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
                public readonly string _Reply = "_Reply";
                public readonly string EditPost = "EditPost";
                public readonly string EditTopic = "EditTopic";
                public readonly string Index = "Index";
                public readonly string Topic = "Topic";
            }
            public readonly string _Reply = "~/Views/Forum/_Reply.cshtml";
            public readonly string EditPost = "~/Views/Forum/EditPost.cshtml";
            public readonly string EditTopic = "~/Views/Forum/EditTopic.cshtml";
            public readonly string Index = "~/Views/Forum/Index.cshtml";
            public readonly string Topic = "~/Views/Forum/Topic.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ForumController : MediaCommMvc.Web.Controllers.ForumController
    {
        public T4MVC_ForumController() : base(Dummy.Instance) { }

        [NonAction]
        partial void AddApprovalOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int postId);

        [NonAction]
        public override System.Web.Mvc.ActionResult AddApproval(int postId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddApproval);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "postId", postId);
            AddApprovalOverride(callInfo, postId);
            return callInfo;
        }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int page);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index(int page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            IndexOverride(callInfo, page);
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
        partial void EditTopicOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, MediaCommMvc.Web.ViewModels.Forum.EditTopicWebViewModel viewModel);

        [NonAction]
        public override System.Web.Mvc.ActionResult EditTopic(MediaCommMvc.Web.ViewModels.Forum.EditTopicWebViewModel viewModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditTopic);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "viewModel", viewModel);
            EditTopicOverride(callInfo, viewModel);
            return callInfo;
        }

        [NonAction]
        partial void EditPostOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult EditPost(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditPost);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            EditPostOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void EditTopicOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult EditTopic(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditTopic);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            EditTopicOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void EditPostOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, MediaCommMvc.Web.ViewModels.Forum.EditPostViewModel viewModel);

        [NonAction]
        public override System.Web.Mvc.ActionResult EditPost(MediaCommMvc.Web.ViewModels.Forum.EditPostViewModel viewModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditPost);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "viewModel", viewModel);
            EditPostOverride(callInfo, viewModel);
            return callInfo;
        }

        [NonAction]
        partial void ReplyOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, MediaCommMvc.Web.ViewModels.Forum.ReplyViewModel viewModel);

        [NonAction]
        public override System.Web.Mvc.ActionResult Reply(MediaCommMvc.Web.ViewModels.Forum.ReplyViewModel viewModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Reply);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "viewModel", viewModel);
            ReplyOverride(callInfo, viewModel);
            return callInfo;
        }

        [NonAction]
        partial void TopicOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id, int page);

        [NonAction]
        public override System.Web.Mvc.ActionResult Topic(int id, int page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Topic);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            TopicOverride(callInfo, id, page);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108
