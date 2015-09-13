using System.Collections.Generic;

namespace MediaCommMvc.Web.Forum.ViewModels
{
    public class PollUserAnswerInput
    {
        public string TopicId { get; set; }

        public string Username { get; set; }

        public IList<int> CheckedAnswers { get; set; }
    }
}