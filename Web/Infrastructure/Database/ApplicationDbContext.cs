using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;

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
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<TopicDetails> TopicDetails { get; set; }

        public DbSet<TopicOverview> TopicOverviews { get; set; }

        public DbSet<Post> Posts { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TopicOverview>().HasKey(overview => overview.TopicId);
            modelBuilder.Entity<TopicOverview>()
                .Property(overview => overview.DisplayPriority)
                .HasColumnAnnotation("index", new IndexAnnotation(new IndexAttribute("IX_TopicOrder", 1)));
            modelBuilder.Entity<TopicOverview>()
                .Property(overview => overview.LastPostTime)
                .HasColumnAnnotation("index", new IndexAnnotation(new IndexAttribute("IX_TopicOrder", 2)));

            modelBuilder.Entity<TopicDetails>().HasKey(details => details.TopicId);
            modelBuilder.Entity<TopicDetails>().Property(details => details.TopicId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<TopicDetails>().HasMany(details => details.Posts);

            modelBuilder.Entity<Post>().HasKey(post => post.Id);
            modelBuilder.Entity<Post>().Property(post => post.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            /* some foreign keys are missing to intentially misssing navigation properties
             possible solution: http://blog.oneunicorn.com/2012/03/26/code-first-data-annotations-on-non-public-properties/ */

            modelBuilder.Conventions.Add(new DateTime2Convention()); 

            base.OnModelCreating(modelBuilder);
        }
    }
}