using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using MediaCommMvc.Web.Features.Account.ViewModels;

using Raven.Client;
using Raven.Client.Linq;

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
            // in memory, could also be done with an index in raven db, see http://stackoverflow.com/questions/34848950/multiple-nested-where-clauses-in-ravendb
            return this.ravenSession.Query<User>().ToList()
                .Where(u => 
                    u.NotifyOnNewForumPost 
                    && (u.LastForumsNotification < u.LastVisit || u.LastForumsNotification < DateTime.UtcNow.AddDays(-7)) 
                    && !excludedUserNames.Contains(u.UserName))
                .Select(u => u.Email)
                .ToList();
        }

        public void UpdateLastForumsNotification(IList<string> usersMailAddressesToNotify, DateTime notificationTime)
        {
            var users = this.ravenSession.Query<User>().Where(user=> user.Email.In(usersMailAddressesToNotify)).ToList();

            foreach (User userFromIndex in users)
            {
                var user = this.ravenSession.Load<User>(userFromIndex.Id);

                user.LastForumsNotification = notificationTime;
            }
        }

        public void UpdateUser(EditUserProfileViewModel input, string name)
        {
            User userFromIndex = this.GetUser(name);

            User user = this.ravenSession.Load<User>(userFromIndex.Id);

            Mapper.Map(input, user);
        }

        public IList<string> GetMailAddressesToNotifyAboutNewPhotoAlbum()
        {
            return this.ravenSession.Query<User>().Where(u => u.NotifyOnNewPhotoAlbum).Select(u => u.Email).ToList();
        }
    }
}
