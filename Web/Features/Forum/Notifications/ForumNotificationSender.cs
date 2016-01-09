using System;
using System.Collections.Generic;
using System.Linq;

using FluentScheduler;

using MediaCommMvc.Web.Features.Account;
using MediaCommMvc.Web.Features.Forum.Models;
using MediaCommMvc.Web.Infrastructure;

using Raven.Client;

using Resources;

namespace MediaCommMvc.Web.Features.Forum.Notifications
{
    public class ForumNotificationSender : Registry
    {
        private readonly Func<UserStorage> userStorageFactory;

        private readonly MailSender mailSender;

        private readonly Config config;

        public ForumNotificationSender(Func<UserStorage> userStorageFactory, MailSender mailSender, Config config)
        {
            this.userStorageFactory = userStorageFactory;
            this.mailSender = mailSender;
            this.config = config;
            //// Schedule an ITask to run at an interval
            //Schedule<MyTask>().ToRunNow().AndEvery(2).Seconds();

            //// Schedule an ITask to run once, delayed by a specific time interval
            //Schedule<MyTask>().ToRunOnceIn(5).Seconds();

            //// Schedule a simple task to run at a specific time
            //Schedule(() => Console.WriteLine("Timed Task - Will run every day at 9:15pm: " + DateTime.Now))
            //    .ToRunEvery(1).Days().At(21, 15);

            //// Schedule a more complex action to run immediately and on an monthly interval
            //Schedule(() =>
            //{
            //    Console.WriteLine("Complex Action Task Starts: " + DateTime.Now);
            //    Thread.Sleep(1000);
            //    Console.WriteLine("Complex Action Task Ends: " + DateTime.Now);
            //}).ToRunNow().AndEvery(1).Months().OnTheFirst(DayOfWeek.Monday).At(3, 0);

            ////Schedule multiple tasks to be run in a single schedule
            //Schedule<MyTask>().AndThen<MyOtherTask>().ToRunNow().AndEvery(5).Minutes();
        }

        public void SendNewTopicNotifications(string topicId)
        {
            this.Schedule(
                () =>
                {
                    IDocumentSession documentSession = DocumentStoreContainer.CreateNewSession;

                    Topic topic = documentSession.Load<Topic>(topicId);

                    DateTime notificationTime = DateTime.UtcNow;
                    IList<string> excludedUserNames = topic.ExcludedUserNames;
                    excludedUserNames.Add(topic.CreatedBy);

                    IList<string> usersMailAddressesToNotify = this.userStorageFactory().GetMailAddressesToNotifyAboutNewPost(excludedUserNames);

                    if (!usersMailAddressesToNotify.Any())
                    {
                        return;
                    }

                    string subject = Mail.NewTopicTitle + this.config.Sitename;
                    string body = string.Format(Mail.NewTopicBody, topic.CreatedBy, topic.Title, topic.CreatedAt) + "<br /><br />" +
                                 this.config.BaseUrl;

                    this.mailSender.SendMail(subject, body, usersMailAddressesToNotify);

                    //this.userRepository.UpdateLastForumsNotification(usersMailAddressesToNotify, notificationTime);

                });
        }

        public void SendNewReplyNotifications(string topicId)
        {
            throw new System.NotImplementedException();
        }


    }
}