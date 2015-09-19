using System.Collections.Generic;

namespace MediaCommMvc.Web.Forum.ViewModels
{
    public class PollUserAnswerViewModel
    {
        public string Username { get; set; }

        public IList<PollAnswerViewModel> Answers { get; set; }
    }
}