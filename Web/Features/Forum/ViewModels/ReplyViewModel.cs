using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using Resources;

namespace MediaCommMvc.Web.Features.Forum.ViewModels
{
    public class ReplyViewModel
    {
        [AllowHtml]
        [Required(ErrorMessageResourceType = typeof(Forums), ErrorMessageResourceName = "TextRequired")]
        public string Text { get; set; }

        public string TopicId { get; set; }
    }
}