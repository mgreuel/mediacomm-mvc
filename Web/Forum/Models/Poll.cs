using System.Collections.Generic;

namespace MediaCommMvc.Web.Forum.Models
{
    public class Poll
    {
        public string Question { get; set; }

        public List<PollAnswer> Answers { get; set; }
    }
}