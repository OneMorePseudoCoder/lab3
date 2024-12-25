using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lab3.Models;

public partial class Lab3Context : DbContext
{
    public Lab3Context()
    {
    }

    public Lab3Context(DbContextOptions<Lab3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__courses__3213E83FB5E67C6A");

            entity.ToTable("course");

            entity.Property(e => e.CourseId)
                .ValueGeneratedNever()
                .HasColumnName("course_id");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Price)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasMany(d => d.Materials).WithMany(p => p.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "MaterialsForCourse",
                    r => r.HasOne<Material>().WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MaterialsForCourse_material"),
                    l => l.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MaterialsForCourse_course"),
                    j =>
                    {
                        j.HasKey("CourseId", "MaterialId");
                        j.ToTable("MaterialsForCourse");
                        j.IndexerProperty<int>("CourseId").HasColumnName("course_id");
                        j.IndexerProperty<int>("MaterialId").HasColumnName("material_id");
                    });
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.ToTable("material");

            entity.Property(e => e.MaterialId)
                .ValueGeneratedNever()
                .HasColumnName("material_id");
            entity.Property(e => e.Cost)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cost");
            entity.Property(e => e.MaterialName)
                .HasMaxLength(100)
                .HasColumnName("material_name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.RewiewId);

            entity.ToTable("review");

            entity.Property(e => e.RewiewId)
                .ValueGeneratedNever()
                .HasColumnName("rewiew_id");
            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Rating)
                .HasColumnType("decimal(2, 1)")
                .HasColumnName("rating");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__3213E83F9E16B265");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E6164237CBF96").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .HasColumnName("middle_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");

            entity.HasMany(d => d.Courses).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserCourse",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserCourse_course"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserCourse_user"),
                    j =>
                    {
                        j.HasKey("UserId", "CourseId");
                        j.ToTable("UserCourse");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("CourseId").HasColumnName("course_id");
                    });

            entity.HasMany(d => d.Reviews).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserReview",
                    r => r.HasOne<Review>().WithMany()
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserReview_review"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserReview_user"),
                    j =>
                    {
                        j.HasKey("UserId", "ReviewId");
                        j.ToTable("UserReview");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("ReviewId").HasColumnName("review_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
