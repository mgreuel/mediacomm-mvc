using System.Collections.Generic;

namespace MediaCommMvc.Web.Forum.Models
{
    public class PollAnswer
    {
        public string Text { get; set; }

        public IList<string> Usernames { get; set; }
    }
}