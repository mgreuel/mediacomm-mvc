using System;
using System.Collections.Generic;
using System.Linq;

using MediaCommMvc.Web.Forum.Models;

namespace MediaCommMvc.Web.Forum.ViewModels
{
    public class EditPollViewModel
    {
        public EditPollViewModel()
        {
            this.Answers = new List<EditPollAnswerViewModel> { new EditPollAnswerViewModel(), new EditPollAnswerViewModel(), new EditPollAnswerViewModel() };
        }

        public string Question { get; set; }

        public List<EditPollAnswerViewModel> Answers { get; set; }

        public Poll ToNewPoll()
        {
            return new Poll
                       {
                           Question = this.Question,
                           Answers =
                               this.Answers.Where(a => !string.IsNullOrWhiteSpace(a.Text))
                               .Select(a => new PollAnswer { Text = a.Text, Id = a.Id == Guid.Empty ? Guid.NewGuid() : a.Id, Usernames = new List<string>() })
                               .ToList()
                       };
        }

        public bool IsEmpty()
        {
            return this.Answers.All(a => !string.IsNullOrWhiteSpace(a.Text));
        }

        public void UpdatePoll(Poll poll)
        {
            Poll temp = poll;
            poll = this.ToNewPoll();

            // We want the new question, but if there were already answers for some question, they need to be copied to the new poll
            foreach (PollAnswer pollAnswer in poll.Answers)
            {
                pollAnswer.Usernames = temp.Answers.SingleOrDefault(a => a.Id == pollAnswer.Id)?.Usernames;
            }
        }
    }
}