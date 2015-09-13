using System;
using System.Collections.Generic;
using System.Linq;

using MediaCommMvc.Web.Forum.Models;

namespace MediaCommMvc.Web.Forum.ViewModels
{
    public class ShowPollViewModel
    {
        public ShowPollViewModel(Poll poll, string currentUserName)
        {
            this.Question = poll.Question;

            // if the ordering is changed, make sure to also change it in the save method!
            List<PollAnswer> orderedAnswers = poll.Answers.OrderBy(a => a.Text).ToList();
            this.AnswerTexts = orderedAnswers.Select(a => a.Text);

            // The results are group by answer option, we building the table we to group by user
            IEnumerable<string> allUsers = poll.Answers.SelectMany(a => a.Usernames).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(u => u).ToList();
            this.UserAnswers = allUsers.Select(
                
                userName => new PollUserAnswerViewModel { Username = userName, Answers = 
                orderedAnswers.Select(
                    answer => answer.Usernames.Any(answerUsername => answerUsername.Equals(userName, StringComparison.OrdinalIgnoreCase))).ToList()
                }).ToList();

            // The current user should always be part of the answers collection as it is also used for taking aprt in the poll
            if (!this.UserHasAnswered(currentUserName))
            {
                this.UserAnswers.Add(new PollUserAnswerViewModel { Username = currentUserName, Answers = new bool[orderedAnswers.Count]});
            }
        }

        public string Question { get; private set; }

        public IEnumerable<string> AnswerTexts { get; set; }

        public IList<PollUserAnswerViewModel> UserAnswers { get; set; }

        public bool UserHasAnswered(string username)
        {
            return this.UserAnswers.Any(ua => ua.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}