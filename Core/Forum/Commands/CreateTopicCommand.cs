using System;

namespace Core.Forum.Commands
{
    public class CreateTopicCommand
    {
        public string AuthorName { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Text { get; set; }

        public string Title { get; set; }
    }
}