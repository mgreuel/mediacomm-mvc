using System.Collections.Generic;

namespace Core
{
    public class TopicDetails
    {
        public List<PostViewModel> Posts { get; set; }

        public string Title { get; set; }

        public int Id { get; set; }
    }
}