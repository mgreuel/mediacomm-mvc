using System;

namespace MediaCommMvc.Web.Forum.ViewModels
{
    public class PollAnswerViewModel
    {
        public bool Checked { get; set; }

        public Guid AnswerId { get; set; }
    }
}