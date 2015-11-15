using System;

namespace MediaCommMvc.Web.Features.Forum.ViewModels
{
    public class PollAnswerViewModel
    {
        public bool Checked { get; set; }

        public Guid AnswerId { get; set; }
    }
}