﻿using System;
using System.Collections.Generic;
using System.Linq;

using MediaCommMvc.Web.Features.Account.ViewModels;

using Raven.Client;

namespace MediaCommMvc.Web.Features.Account
{
    public class UserStorage
    {
        private readonly IDocumentSession ravenSession;

        public UserStorage(IDocumentSession ravenSession)
        {
            this.ravenSession = ravenSession;
        }

        public User GetUser(string username)
        {
            return this.ravenSession.Query<User>().SingleOrDefault(u => u.UserName == username);
        }

        public void CreateUser(User user)
        {
            if (this.ravenSession.Query<User>().Any(u => u.UserName == user.UserName))
            {
                throw new Exception($"Cannot register user, username {user.UserName} already exists");
            }

            this.ravenSession.Store(user);
        }

        public IEnumerable<string> GetAllUsernames()
        {
            return this.ravenSession.Query<User>().Select(u => u.UserName).ToList();
        }

        public IList<UserOverviewItemViewModel> GetUserOverView()
        {
            return this.ravenSession.Query<User>().Select(u => new UserOverviewItemViewModel { Username = u.UserName, Firstname = u.FirstName, Lastname = u.LastName }).ToList();
        }

        public void SaveUser(User user)
        {
            this.ravenSession.Store(user);
        }

        public IList<string> GetMailAddressesToNotifyAboutNewPost(IList<string> excludedUserNames)
        {
    //        IEnumerable<string> mailAddresses =
    //this.Session.CreateSQLQuery(
    //    @"SELECT     EMailAddress
    //                FROM         MediaCommUsers
    //                WHERE     (ForumsNotificationInterval = 1) AND (LastForumsNotification IS NULL) OR
    //                  (ForumsNotificationInterval = 1) AND (LastVisit IS NULL) OR
    //                  (ForumsNotificationInterval = 1) AND (LastForumsNotification < LastVisit) OR
    //                  (ForumsNotificationInterval = 1) AND (LastForumsNotification < DATEADD(day, - 7, GETDATE()))")
    //    .List<string>();

           return this.ravenSession.Query<User>()
                .Where(u => 
                    u.NotifyOnNewForumPost 
                    && (u.LastForumsNotification < u.LastVisit || u.LastForumsNotification < DateTime.UtcNow.AddDays(-7)) 
                    && !excludedUserNames.Contains(u.UserName))
                .Select(u => u.Email)
                .ToList();
        }
    }
}
