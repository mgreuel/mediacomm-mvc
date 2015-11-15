using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MediaCommMvc.Web.Features.Forum.ViewModels
{
    public class EditPostViewModel
    {
        [Required]
        [AllowHtml]
        public string Text { get; set; }

        public string TopicId { get; set; }

        public int PostIndex { get; set; }
    }
}