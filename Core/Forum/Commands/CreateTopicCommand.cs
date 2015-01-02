﻿using System;
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

        public IEnumerable<string> ExcludedUserNames { get; set; }

        public Topic ToTopic()
        {
            return new Topic
                       {
                           CreatedBy = this.AuthorName, 
                           ExcludedUserNames = this.ExcludedUserNames, 
                           LastPostAuthor = this.AuthorName, 
                           LastPostTime = this.TimeStamp, 
                           PostCount = 1, 
                           LastAccessTimes = new Dictionary<string, DateTime> { { this.AuthorName, this.TimeStamp.AddMilliseconds(1) } }, 
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