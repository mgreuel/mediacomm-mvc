using System.Collections.Generic;

namespace MediaCommMvc.Web.Forum.Models
{
    public class Poll
    {
        public string Question { get; set; }

        public IEnumerable<PollAnswer> Answers { get; set; }
    }
}