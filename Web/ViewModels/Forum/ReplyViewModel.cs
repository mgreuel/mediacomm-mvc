using System.Web.Mvc;

namespace MediaCommMvc.Web.ViewModels.Forum
{
    public class ReplyViewModel
    {
        [AllowHtml]
        public string Text { get; set; }

        public int TopicId { get; set; }
    }
}