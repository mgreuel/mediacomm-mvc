using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using MediaCommMvc.Web.Models.Forum.Models;

using Resources;

namespace MediaCommMvc.Web.ViewModels.Forum
{
    public class PollViewModel
    {
        public PollViewModel(Poll poll) 
        {
            if (poll == null || !poll.Answers.Any())
            {
                this.Answers = new List<string> { string.Empty, string.Empty, string.Empty };
            }
            else
            {
                this.Answers = poll.Answers.Select(a => a.Text).ToList();
            }

            if (poll == null)
            {
                return;
            }

            this.Question = poll.Question;
            this.PollType = poll.PollType;
        }

        public PollViewModel()
        {
            this.Answers = new List<string> { string.Empty, string.Empty, string.Empty };
        }

        public string Question { get; set; }

        public IList<string> Answers { get; set; }

        [Display(ResourceType = typeof(Forums), Name = "PollType")]
        public PollType PollType { get; set; }

        public Poll CreatePoll()
        {
            return new Poll { Question = this.Question, Answers = this.Answers.Select(a => new PollAnswer { Text = a, Usernames = new List<string>() }).ToList(), PollType = this.PollType};
        }
    }
}