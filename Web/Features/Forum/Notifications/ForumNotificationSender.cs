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
        private readonly ILogger logger;

        public ForumNotificationSender(ILogger logger)
        {
            this.logger = logger;
        }

        public void SendNewTopicNotifications(string topicId)
        {
            // without saving now, the loading of the topic in the background thread might be faster than the save at the end of the http request
            DocumentStoreContainer.CurrentRequestSession.SaveChanges();

            HostingEnvironment.QueueBackgroundWorkItem(
                _ =>
                {
                    try
                    {
                        using (IDocumentSession documentSession = DocumentStoreContainer.NewSession)
                        {
                            Config config = documentSession.Load<Config>(Config.ConfigId);
                            MailSender mailSender = new MailSender(documentSession.Load<MailConfig>(MailConfig.MailConfigId), this.logger);
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
                            string body = string.Format(Mail.NewTopicBody, topic.CreatedBy, topic.Title, topic.CreatedAt.ToLocalTime()) 
                                          + "<br /><br />" 
                                          + $"<small>{topic.Posts.First().Text}</small>"
                                          + "<br /><br />"
                                          + $"<a href='{config.BaseUrl}'>{config.BaseUrl}</a>"
                                          + "<br /><br />"
                                          + Mail.NoNewNotification;


                            mailSender.SendMail(subject, body, usersMailAddressesToNotify);

                            userStorage.UpdateLastForumsNotification(usersMailAddressesToNotify, notificationTime);

                            documentSession.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.GetDefault(null).Log(new Error(ex));
                        this.logger.Error("SendNewTopicNotifications failed", ex);
                    }
                });
        }

        public void SendNewReplyNotifications(string topicId, int postIndex)
        {
            // without saving now, the loading of the topic in the background thread might be faster than the save at the end of the http request
            DocumentStoreContainer.CurrentRequestSession.SaveChanges();

            HostingEnvironment.QueueBackgroundWorkItem(
                _ =>
                {
                    try
                    {
                        using (IDocumentSession documentSession = DocumentStoreContainer.NewSession)
                        {
                            Config config = documentSession.Load<Config>(Config.ConfigId);
                            MailSender mailSender = new MailSender(documentSession.Load<MailConfig>(MailConfig.MailConfigId), this.logger);
                            UserStorage userStorage = new UserStorage(documentSession);

                            Topic topic = documentSession.Load<Topic>(topicId);
                            Post newPost = topic.Posts.Single(p => p.IndexInTopic == postIndex);

                            DateTime notificationTime = DateTime.UtcNow;
                            IList<string> excludedUserNames = new List<string>(topic.ExcludedUserNames);
                            excludedUserNames.Add(newPost.AuthorName);

                            IList<string> usersMailAddressesToNotify = userStorage.GetMailAddressesToNotifyAboutNewPost(excludedUserNames);

                            if (!usersMailAddressesToNotify.Any())
                            {
                                return;
                            }

                            string subject = Mail.NewPostTitle + config.Sitename;
                            string body = string.Format(Mail.NewPostBody, newPost.AuthorName, topic.Title, newPost.CreatedAt) 
                                            + "<br /><br />"
                                            + $"<small>{newPost.Text}</small>"
                                            + "<br /><br />"
                                            + $"<a href='{config.BaseUrl}'>{config.BaseUrl}</a>"
                                            + "<br /><br />"
                                            + Mail.NoNewNotification;

                            mailSender.SendMail(subject, body, usersMailAddressesToNotify);

                            userStorage.UpdateLastForumsNotification(usersMailAddressesToNotify, notificationTime);

                            documentSession.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.GetDefault(null).Log(new Error(ex));
                        this.logger.Error("SendNewReplyNotifications failed", ex);
                    }
                });
        }
    }
}