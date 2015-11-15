using System;
using System.Collections.Generic;

namespace MediaCommMvc.Web.Features.Forum.Models
{
    public class PollAnswer
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public IList<string> Usernames { get; set; }
    }
}