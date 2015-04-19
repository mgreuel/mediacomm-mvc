using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;

using MediaCommMvc.Web.Models.Forum.Models;
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

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Post> Posts { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>().HasKey(overview => overview.TopicId);
            modelBuilder.Entity<Topic>().Property(details => details.TopicId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Topic>()
                .Property(overview => overview.DisplayPriority)
                .HasColumnAnnotation("index", new IndexAnnotation(new IndexAttribute("IX_TopicOrder", 1)));
            modelBuilder.Entity<Topic>()
                .Property(overview => overview.LastPostTime)
                .HasColumnAnnotation("index", new IndexAnnotation(new IndexAttribute("IX_TopicOrder", 2)));

            modelBuilder.Entity<Topic>().HasMany(details => details.Posts).WithRequired(post => post.Topic).HasForeignKey(post => post.TopicId);

            modelBuilder.Entity<Post>().HasKey(post => post.Id);
            modelBuilder.Entity<Post>().Property(post => post.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            
            modelBuilder.Conventions.Add(new DateTime2Convention()); 

            base.OnModelCreating(modelBuilder);
        }
    }
}