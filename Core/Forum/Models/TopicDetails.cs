using System.Collections.Generic;

namespace Core.Forum.Models
{
    public class TopicDetails
    {
        public List<Post> Posts { get; set; }

        public string Title { get; set; }

        public int TopicId { get; set; }
    }
}