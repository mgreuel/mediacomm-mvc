using System;
using System.Collections.Generic;

namespace Core.Forum.Models
{
    public class Post
    {
        public string AuthorName { get; set; }

        public DateTime Created { get; set; }

        public int Id { get; set; }

        public string Text { get; set; }

        public IEnumerable<string> Approvals { get; set; }
    }
}