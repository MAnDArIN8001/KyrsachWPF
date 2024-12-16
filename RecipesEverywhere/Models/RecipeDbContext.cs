using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RecepiesEverywhere.Models;

public partial class RecipeDbContext : DbContext
{
    public RecipeDbContext()
    {
    }

    public RecipeDbContext(DbContextOptions<RecipeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Favourite> Favourites { get; set; }

    public virtual DbSet<Mark> Marks { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=recipesDB;Username=recipesUser;Password=recipesPassword");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Favourite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("favourite_pk");

            entity.ToTable("Favourite");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Favourites)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("favourite_recipe_fk");

            entity.HasOne(d => d.User).WithMany(p => p.Favourites)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("favourite_user_fk");
        });

        modelBuilder.Entity<Mark>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("marks_pk");

            entity.Property(e => e.MarkValue).HasColumnName("Mark");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Marks)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("marks_recipe_fk");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recipes_pk");

            entity.ToTable("Recipe");

            entity.HasOne(d => d.Author).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("recipe_users_id_fk");

            entity.HasOne(d => d.Status).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_statuses_id_fk");

            entity.HasMany(d => d.Tags) // Добавлено для связи с Tag
                    .WithMany(p => p.Recipes)
                    .UsingEntity(j => j.ToTable("RecipeTags")); 
        });


        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tags_pk");

            entity.ToTable("Tag");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pk");

            entity.HasIndex(e => e.Name, "Roles_Name_key").IsUnique();
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("statuses_pk");

            entity.HasIndex(e => e.Name, "Statuses_Name_key").IsUnique();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pk");

            entity.HasIndex(e => e.Name, "Users_Name_key").IsUnique();

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_roles_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
