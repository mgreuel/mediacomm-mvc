using System;

namespace Core.Forum.Commands
{
    public class AddReplyCommand
    {
        public int TopicId { get; set; }

        public string AuthorName { get; set; }

        public DateTime Created { get; set; }

        public string Text { get; set; }
    }
}