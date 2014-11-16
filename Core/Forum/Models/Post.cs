using System;
using System.Collections.Generic;

namespace Core.Forum.Models
{
    public class Post
    {
        public Post()
        {
            this.ApprovalStorage = string.Empty;
        }

        public string AuthorName { get; set; }

        public DateTime Created { get; set; }

        public int Id { get; set; }

        public string Text { get; set; }

        public int TopicId { get; set; }

        public TopicDetails Topic { get; set; }

        public IEnumerable<string> Approvals
        {
            get
            {
                return this.ApprovalStorage.Split(',');
            }
        }

        // Todo make private/protected
        public string ApprovalStorage { get; set; }

        public void AddApproval(string username)
        {
            List<string> approvals = new List<string>(this.Approvals);
            approvals.Add(username);
            this.ApprovalStorage = string.Join("'", approvals);
        }
    }
}