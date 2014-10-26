using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using Core;
using Core.Forum.Commands;

using Resources;

namespace MediaCommMvc.Web.ViewModels.Forum
{
    public class ReplyViewModel
    {
        [AllowHtml]
        [Required(ErrorMessageResourceType = typeof(Forums), ErrorMessageResourceName = "TextRequired")]
        public string Text { get; set; }

        public int TopicId { get; set; }

        public AddReplyCommand ToAddReplyCommand(string userName)
        {
            return new AddReplyCommand { AuthorName = userName, Created = DateTime.Now, Text = this.Text, TopicId = this.TopicId };
        }
    }
}