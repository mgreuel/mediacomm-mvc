using System.Collections.Generic;
using System.Linq;

using MediaCommMvc.Web.Models.Forum.Models;

namespace MediaCommMvc.Web.Forum.ViewModels
{
    public class CreatePollViewModel
    {
        public CreatePollViewModel(Poll poll)
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
        }

        public CreatePollViewModel()
        {
            this.Answers = new List<string> { string.Empty, string.Empty, string.Empty };
        }

        public string Question { get; set; }

        public IList<string> Answers { get; set; }

        public Poll ToPoll()
        {
            return new Poll
                       {
                           Question = this.Question, 
                           Answers =
                               this.Answers.Where(a => !string.IsNullOrWhiteSpace(a))
                               .Select(a => new PollAnswer { Text = a, Usernames = new List<string>() })
                               .ToList()
                       };
        }
    }
}