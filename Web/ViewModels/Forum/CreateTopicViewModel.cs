using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using Core;

using Resources;

namespace MediaCommMvc.Web.ViewModels.Forum
{
    public class CreateTopicViewModel
    {
        public IEnumerable<string> AllUserNames { get; set; }

        [Display(ResourceType = typeof(Forums), Name = "Hide")]
        public string ExcludedUsers { get; set; }

        [Required(ErrorMessageResourceType = typeof(Forums), ErrorMessageResourceName = "SubjectRequired")]
        [Display(ResourceType = typeof(Forums), Name = "Subject")]
        public string Subject { get; set; }

        [Required(ErrorMessageResourceType = typeof(Forums), ErrorMessageResourceName = "TextRequired")]
        [Display(ResourceType = typeof(Forums), Name = "Message")]
        [AllowHtml]
        public string Text { get; set; }

        public CreateTopicCommand ToCommand(string userName)
        {
            return new CreateTopicCommand { AuthorName = userName, Text = this.Text, TimeStamp = DateTime.Now, Title = this.Subject };
        }
    }
}