using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

using MediaCommMvc.Web.Models.Forum.Commands;

using Resources;

namespace MediaCommMvc.Web.Forum.ViewModels
{
    public class EditTopicViewModel
    {
        public EditTopicViewModel()
        {
            this.Poll = new CreatePollViewModel();
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
        public CreatePollViewModel Poll { get; set; }

        public bool HasPoll
        {
            get
            {
                return this.Poll.Answers.Any(a => !string.IsNullOrWhiteSpace(a));
            }
        }

        //public CreateTopicCommand ToCreateTopicCommand(string userName)
        //{
        //    return new CreateTopicCommand { AuthorName = userName, ExcludedUserNames = this.ExcludedUserNames, Text = this.Text, TimeStamp = DateTime.UtcNow, Title = this.Title, Poll = this.Poll.ToPoll() };
        //}
    }
}