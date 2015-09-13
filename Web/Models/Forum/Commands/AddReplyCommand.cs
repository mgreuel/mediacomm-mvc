using System;

namespace MediaCommMvc.Web.Models.Forum.Commands
{
    public class AddReplyCommand
    {
        public string TopicId { get; set; }

        public string AuthorName { get; set; }

        public DateTime Created { get; set; }

        public string Text { get; set; }
    }
}