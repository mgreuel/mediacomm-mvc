using System.Data.Entity;
using System.Linq;

using Core;

using MediaCommMvc.Web.ViewModels;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MediaCommMvc.Web.Infrastructure.Database
{
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            SeedUsers(context);
        }

        private static void SeedUsers(ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == UserRoles.Administrator))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = UserRoles.Administrator };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "Admin"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "Admin", Email = "admin@local.loc" };

                manager.Create(user, "ChangeIt!");
                manager.AddToRole(user.Id, UserRoles.Administrator);
            }

            if (!context.Users.Any(u => u.UserName == "User"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "User", Email = "user@local.loc" };

                manager.Create(user, "ChangeIt!");
            }

            if (!context.Users.Any(u => u.UserName == "User1"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "User1", Email = "user1@local.loc" };

                manager.Create(user, "ChangeIt!");
            }

            if (!context.Users.Any(u => u.UserName == "User2"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "User2", Email = "user2@local.loc" };

                manager.Create(user, "ChangeIt!");
            }
        }
    }
}