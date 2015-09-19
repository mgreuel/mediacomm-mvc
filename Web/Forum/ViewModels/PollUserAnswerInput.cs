using System;
using System.Collections.Generic;

namespace MediaCommMvc.Web.Forum.ViewModels
{
    public class PollUserAnswerInput
    {
        public string TopicId { get; set; }

        public IList<Guid> CheckedAnswers { get; set; }
    }
}