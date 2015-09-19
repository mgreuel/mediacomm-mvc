using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

using MediaCommMvc.Web.Forum.Models;

using Resources;

namespace MediaCommMvc.Web.Forum.ViewModels
{
    public class EditTopicViewModel
    {
        public EditTopicViewModel()
        {
            this.Poll = new EditPollViewModel();
        }

        public string Id { get; set; }

        public IEnumerable<SelectListItem> AllUserNames { get; set; }

        [Display(ResourceType = typeof(Forums), Name = "ExcludeUsers")]
        public IEnumerable<string> ExcludedUserNames { get; set; }

        [Required(ErrorMessageResourceType = typeof(Forums), ErrorMessageResourceName = "SubjectRequired")]
        [Display(ResourceType = typeof(Forums), Name = "Subject")]
        public string Title { get; set; }

        [Required(ErrorMessageResourceType = typeof(Forums), ErrorMessageResourceName = "TextRequired")]
        [Display(ResourceType = typeof(Forums), Name = "Message")]
        [AllowHtml]
        public string Text { get; set; }

        [Display(ResourceType = typeof(Forums), Name = "Poll")]
        public EditPollViewModel Poll { get; set; }

        [Display(ResourceType = typeof(Forums), Name = "MarkAsSticky")]
        public bool IsSticky { get; set; }

    }
}