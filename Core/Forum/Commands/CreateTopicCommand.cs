using System;
using System.Collections.Generic;

using Core.Forum.Models;

namespace Core.Forum.Commands
{
    public class CreateTopicCommand
    {
        public string AuthorName { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Text { get; set; }

        public string Title { get; set; }

        public TopicOverview ToTopicOverview(int id)
        {
            return new TopicOverview
                {
                    CreatedBy = this.AuthorName,
                    TopicId = id,
                    LastPostAuthor = this.AuthorName,
                    LastPostTime = this.TimeStamp,
                    PostCount = 1,
                    Title = this.Title
                };
        }

        public TopicDetails ToTopicDetails()
        {
            return new TopicDetails
                       {
                           Title = this.Title,
                           Posts =
                               new List<Post>
                                   {
                                       new Post
                                           {
                                               AuthorName = this.AuthorName,
                                               Created = this.TimeStamp,
                                               Id = 1,
                                               Text = this.Text
                                           }
                                   }
                       };
        }
    }
}