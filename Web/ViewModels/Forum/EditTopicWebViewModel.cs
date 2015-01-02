﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using Core;
using Core.Forum.Commands;

using Resources;

namespace MediaCommMvc.Web.ViewModels.Forum
{
    public class EditTopicWebViewModel
    {
        public int Id { get; set; }

        public IEnumerable<string> AllUserNames { get; set; }

        [Display(ResourceType = typeof(Forums), Name = "Hide")]
        public IEnumerable<string> ExcludedUserNames { get; set; }

        [Required(ErrorMessageResourceType = typeof(Forums), ErrorMessageResourceName = "SubjectRequired")]
        [Display(ResourceType = typeof(Forums), Name = "Subject")]
        public string Subject { get; set; }

        [Required(ErrorMessageResourceType = typeof(Forums), ErrorMessageResourceName = "TextRequired")]
        [Display(ResourceType = typeof(Forums), Name = "Message")]
        [AllowHtml]
        public string Text { get; set; }

        public CreateTopicCommand ToCreateTopicCommand(string userName)
        {
            return new CreateTopicCommand { AuthorName = userName, ExcludedUserNames = this.ExcludedUserNames, Text = this.Text, TimeStamp = DateTime.UtcNow, Title = this.Subject };
        }

        public UpdateTopicCommand ToUpdateTopicCommand()
        {
            return new UpdateTopicCommand { Text = this.Text, ExcludedUserNames = this.ExcludedUserNames, Title = this.Subject, Id = this.Id };
        }
    }
}