using System;
using System.Collections.Generic;

namespace MediaCommMvc.Web.Forum.Models
{
    public class Post
    {
        public Post()
        {
            this.Approvals = new List<string>();
        }

        public string AuthorName { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Text { get; set; }

        public List<string> Approvals { get; }

        public int IndexInTopic { get; set; }
    }
}