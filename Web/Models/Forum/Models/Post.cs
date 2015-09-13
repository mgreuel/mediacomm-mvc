using System;
using System.Collections.Generic;

namespace MediaCommMvc.Web.Models.Forum.Models
{
    public class Post
    {
        public string AuthorName { get; set; }

        public DateTime Created { get; set; }

        public string Text { get; set; }

        public List<string> Approvals { get; set; }

        public int IndexInTopic { get; set; }
    }
}