using System.Collections.Generic;

namespace MediaCommMvc.Web.Models.Forum.Models
{
    public class Poll
    {
        public string Question { get; set; }

        public PollType PollType { get; set; }

        public IEnumerable<PollAnswer> Answers { get; set; }
    }
}