using System.Data.Entity;

using Core.Forum.Models;

using MediaCommMvc.Web.ViewModels;

using Microsoft.AspNet.Identity.EntityFramework;

namespace MediaCommMvc.Web.Infrastructure.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<TopicDetails> TopicDetails { get; set; }

        public DbSet<TopicOverview> TopicOverviews { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}