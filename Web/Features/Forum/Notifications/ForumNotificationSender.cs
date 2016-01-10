using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;

using MediaCommMvc.Web.Features.Account;
using MediaCommMvc.Web.Features.Forum.Models;
using MediaCommMvc.Web.Infrastructure;

using Raven.Client;

using Resources;

namespace MediaCommMvc.Web.Features.Forum.Notifications
{
    public class ForumNotificationSender
    {
        private readonly Func<UserStorage> userStorageFactory;

        private readonly MailSender mailSender;

        private readonly Config config;

        public ForumNotificationSender(Func<UserStorage> userStorageFactory, MailSender mailSender, Config config)
        {
            this.userStorageFactory = userStorageFactory;
            this.mailSender = mailSender;
            this.config = config;
        }

        public void SendNewTopicNotifications(string topicId)
        {
            HostingEnvironment.QueueBackgroundWorkItem(
                _ =>
                {
                    try
                    {
                        IDocumentSession documentSession = DocumentStoreContainer.CreateNewSession;
                        UserStorage userStorage = this.userStorageFactory();

                        Topic topic = documentSession.Load<Topic>(topicId);

                        DateTime notificationTime = DateTime.UtcNow;
                        IList<string> excludedUserNames = topic.ExcludedUserNames;
                        excludedUserNames.Add(topic.CreatedBy);

                        IList<string> usersMailAddressesToNotify = userStorage.GetMailAddressesToNotifyAboutNewPost(excludedUserNames);

                        if (!usersMailAddressesToNotify.Any())
                        {
                            return;
                        }

                        string subject = Mail.NewTopicTitle + this.config.Sitename;
                        string body = string.Format(Mail.NewTopicBody, topic.CreatedBy, topic.Title, topic.CreatedAt) + "<br /><br />" +
                                     this.config.BaseUrl;

                        this.mailSender.SendMail(subject, body, usersMailAddressesToNotify);

                        userStorage.UpdateLastForumsNotification(usersMailAddressesToNotify, notificationTime);
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.Get(null).Raise(ex);
                    }
                });
        }

        public void SendNewReplyNotifications(string topicId)
        {
            throw new System.NotImplementedException();
        }


    }
}