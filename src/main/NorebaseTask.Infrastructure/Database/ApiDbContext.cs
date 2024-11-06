using Microsoft.EntityFrameworkCore;
using NorebaseTask.Core;

namespace NorebaseTask.Infrastructure;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

    public DbSet<Article>? Articles { get; set; }
    public DbSet<ArticleLike>? ArticleLikes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // Configure Article to User (Many-to-One) relationship by foreign key
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasOne<User>()
                  .WithMany()
                  .HasForeignKey(a => a.UserId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Article_User");
        });

        // Configure User to ArticleLike (One-to-Many) relationship
        modelBuilder.Entity<ArticleLike>(entity =>
        {
            entity.HasOne<User>()
                  .WithMany()
                  .HasForeignKey(al => al.UserId)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("FK_ArticleLike_User");
        });

        // Configure Article to ArticleLike (One-to-Many) relationship
        modelBuilder.Entity<ArticleLike>(entity =>
        {
            entity.HasOne<Article>()
                  .WithMany()
                  .HasForeignKey(al => al.ArticleId)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("FK_ArticleLike_Article");

            // Enforce unique User-Article pair in ArticleLike table
            entity.HasIndex(al => new { al.UserId, al.ArticleId })
                  .IsUnique()
                  .HasDatabaseName("IX_ArticleLike_User_Article");
        });
    }
}