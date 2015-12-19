using System;
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
    }
}
