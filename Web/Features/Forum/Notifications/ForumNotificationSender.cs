using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;

using Elmah;

using MediaCommMvc.Web.Features.Account;
using MediaCommMvc.Web.Features.Forum.Models;
using MediaCommMvc.Web.Infrastructure;

using Raven.Client;

using Resources;

namespace MediaCommMvc.Web.Features.Forum.Notifications
{
    public class ForumNotificationSender
    {
        public void SendNewTopicNotifications(string topicId)
        {
           
            // without saving now, the loading of the topic in the background thread migth be faster than ne save at the end of the http request
            DocumentStoreContainer.CurrentRequestSession.SaveChanges();

            HostingEnvironment.QueueBackgroundWorkItem(
                _ =>
                {
                    try
                    {
                        IDocumentSession documentSession = DocumentStoreContainer.NewSession;
                        Config config = documentSession.Load<Config>(Config.ConfigId);
                        MailSender mailSender = new MailSender(documentSession.Load<MailConfig>(MailConfig.MailConfigId));
                        UserStorage userStorage = new UserStorage(documentSession);

                        Topic topic = documentSession.Load<Topic>(topicId);

                        DateTime notificationTime = DateTime.UtcNow;
                        IList<string> excludedUserNames = topic.ExcludedUserNames;
                        excludedUserNames.Add(topic.CreatedBy);

                        IList<string> usersMailAddressesToNotify = userStorage.GetMailAddressesToNotifyAboutNewPost(excludedUserNames);

                        if (!usersMailAddressesToNotify.Any())
                        {
                            return;
                        }

                        string subject = Mail.NewTopicTitle + config.Sitename;
                        string body = string.Format(Mail.NewTopicBody, topic.CreatedBy, topic.Title, topic.CreatedAt) + "<br /><br />" +
                                      config.BaseUrl;

                        mailSender.SendMail(subject, body, usersMailAddressesToNotify);

                        userStorage.UpdateLastForumsNotification(usersMailAddressesToNotify, notificationTime);
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.GetDefault(null).Log(new Error(ex));
                    }
                });
        }

        public void SendNewReplyNotifications(string topicId)
        {
            throw new System.NotImplementedException();
        }


    }
}