using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;

using Elmah;

using MediaCommMvc.Web.Features.Account;
using MediaCommMvc.Web.Infrastructure;

using Raven.Client;

using Resources;

namespace MediaCommMvc.Web.Features.Photos
{
    public class PhotoNotificationSender
    {
        private readonly ILogger logger;

        public PhotoNotificationSender(ILogger logger)
        {
            this.logger = logger;
        }

        public void SendNewPhotoAlbumNotifications(string albumTitle)
        {
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

                            IList<string> usersMailAddressesToNotify = userStorage.GetMailAddressesToNotifyAboutNewPhotoAlbum();

                            if (!usersMailAddressesToNotify.Any())
                            {
                                return;
                            }

                            string subject = Mail.NewPhotosTitle + config.Sitename;
                            string body = string.Format(Mail.NewPhotosBody, albumTitle) + "<br /><br />" +
                                          $"<a href='{config.BaseUrl}'>{config.BaseUrl}</a>";

                            mailSender.SendMail(subject, body, usersMailAddressesToNotify);
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.GetDefault(null).Log(new Error(ex));
                        this.logger.Error("SendNewPhotoAlbumNotifications failed", ex);
                    }
                });
        }
    }
}
