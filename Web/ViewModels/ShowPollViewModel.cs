using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

using MediaCommMvc.Web.Models.Forum.Models;

namespace MediaCommMvc.Web.ViewModels
{
    public class ShowPollViewModel
    {
        public ShowPollViewModel(Poll poll, string currentUserName)
        {
            this.Question = poll.Question;
            List<PollAnswer> orderedAnswers = poll.Answers.OrderBy(a => a.Text).ToList();
            this.AnswerTexts = orderedAnswers.Select(a => a.Text);

            // The results are group by answer option, we building the table we to group by user
            IEnumerable<string> allUsers = poll.Answers.SelectMany(a => a.Usernames).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(u => u).ToList();
            this.UserAnswers = allUsers.ToDictionary(
                u => u,
                userName =>
                orderedAnswers.Select(
                    answer => answer.Usernames.Any(answerUsername => answerUsername.Equals(userName, StringComparison.OrdinalIgnoreCase))));

            // The current user should always be part of the answers collection as it is also used for taking aprt in the poll
            if (!this.UserHasAnswered(currentUserName))
            {
                this.UserAnswers.Add(currentUserName, new bool[orderedAnswers.Count]);
            }
        }

        public string Question { get; private set; }

        public IEnumerable<string> AnswerTexts { get; set; }

        public IDictionary<string, IEnumerable<bool>> UserAnswers { get; set; }

        public bool UserHasAnswered(string username)
        {
            return this.UserAnswers.Any(ua => ua.Key.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}