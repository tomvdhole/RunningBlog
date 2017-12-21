using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RunningBlog.Models;

namespace RunningBlog.Data
{
    public class RunningBlogDbContext : IdentityDbContext<ApplicationUser>
    {
        public RunningBlogDbContext(DbContextOptions<RunningBlogDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<PostCategory>()
                .HasKey(pc => new { pc.PostId, pc.CategoryId });

            builder.Entity<PostCategory>()
                .HasOne(pc => pc.Post)
                .WithMany(p => p.PostCategories)
                .HasForeignKey(pc => pc.PostId);

            builder.Entity<PostCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(p => p.PostCategories)
                .HasForeignKey(pc => pc.CategoryId);
        }

        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
