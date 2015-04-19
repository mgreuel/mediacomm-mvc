using System.Collections.Generic;

namespace MediaCommMvc.Web.Models.Forum.Models
{
    public class PollAnswer
    {
        public string Text { get; set; }

        public IEnumerable<string> Usernames { get; set; }
    }
}