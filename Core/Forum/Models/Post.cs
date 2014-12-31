using System;
using System.Collections.Generic;
using System.Linq;

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

        public Topic Topic { get; set; }

        public IEnumerable<string> Approvals
        {
            get
            {
                return this.ApprovalStorage.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        // Todo make private/protected // http://blog.oneunicorn.com/2012/03/26/code-first-data-annotations-on-non-public-properties/ */
        public string ApprovalStorage { get; set; }

        public int IndexInTopic { get; set; }

        public void AddApproval(string username)
        {
            List<string> approvals = new List<string>(this.Approvals);
            approvals.Add(username);
            this.ApprovalStorage = string.Join(",", approvals.Distinct());
        }
    }
}