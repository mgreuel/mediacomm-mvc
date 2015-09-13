using System.Collections.Generic;
using System.Linq;

using MediaCommMvc.Web.Forum.Models;

namespace MediaCommMvc.Web.Forum.ViewModels
{
    public class CreatePollViewModel
    {


        public CreatePollViewModel()
        {
            this.Answers = new List<string> { string.Empty, string.Empty, string.Empty };
        }

        public string Question { get; set; }

        public IList<string> Answers { get; set; }

        public Poll ToPoll()
        {
            if (this.Answers.All(string.IsNullOrWhiteSpace))
            {
                return null;
            }

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