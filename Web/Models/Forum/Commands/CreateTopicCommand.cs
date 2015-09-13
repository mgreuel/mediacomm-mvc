//using System;
//using System.Collections.Generic;

//using MediaCommMvc.Web.Models.Forum.Models;

//namespace MediaCommMvc.Web.Models.Forum.Commands
//{
//    public class CreateTopicCommand
//    {
//        public CreateTopicCommand()
//        {
//            this.ExcludedUserNames = new List<string>();
//        }

//        public string AuthorName { get; set; }

//        public DateTime TimeStamp { get; set; }

//        public string Text { get; set; }

//        public string Title { get; set; }

//        public IEnumerable<string> ExcludedUserNames { get; set; }

//        public Poll Poll { get; set; }

//        public Topic ToTopic()
//        {
//            var topic = new Topic
//                            {
//                                CreatedBy = this.AuthorName,
//                                ExcludedUserNames = this.ExcludedUserNames,
//                                LastPostAuthor = this.AuthorName,
//                                LastPostTime = this.TimeStamp,
//                                PostCount = 1,
//                                Title = this.Title,
//                                Poll = this.Poll,
//                                Posts =
//                                    new List<Post>
//                                        {
//                                            new Post
//                                                {
//                                                    AuthorName = this.AuthorName,
//                                                    Created = this.TimeStamp,
//                                                    IndexInTopic = 1,
//                                                    Text = this.Text
//                                                }
//                                        }
//                            };

//            topic.MarkTopicAsRead(this.AuthorName);

//            return topic;
//        }
//    }
//}