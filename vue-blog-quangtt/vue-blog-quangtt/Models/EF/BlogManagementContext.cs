using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace vue_blog_quangtt.Models.EF;

public partial class BlogManagementContext : DbContext
{
    public BlogManagementContext()
    {
    }

    public BlogManagementContext(DbContextOptions<BlogManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<BlogLocation> BlogLocations { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-CPTPR8L;Initial Catalog=BlogManagement;Persist Security Info=True;User ID=sa;Password=1; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.ToTable("Blog");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Detail).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Note).HasMaxLength(50);

            entity.HasOne(d => d.IdTypeNavigation).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.IdType)
                .HasConstraintName("FK_Blog_Type");
        });

        modelBuilder.Entity<BlogLocation>(entity =>
        {
            entity.ToTable("BlogLocation");

            entity.HasOne(d => d.IdBlogNavigation).WithMany(p => p.BlogLocations)
                .HasForeignKey(d => d.IdBlog)
                .HasConstraintName("FK_BlogLocation_Blog");

            entity.HasOne(d => d.IdLocationNavigation).WithMany(p => p.BlogLocations)
                .HasForeignKey(d => d.IdLocation)
                .HasConstraintName("FK_BlogLocation_Location");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Location");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.ToTable("Type");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
