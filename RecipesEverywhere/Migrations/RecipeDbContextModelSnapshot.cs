﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RecepiesEverywhere.Models;

#nullable disable

namespace RecepiesEverywhere.Migrations
{
    [DbContext(typeof(RecipeDbContext))]
    partial class RecipeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RecepiesEverywhere.Models.Favourite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("RecipeId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("favourite_pk");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserId");

                    b.ToTable("Favourite", (string)null);
                });

            modelBuilder.Entity("RecepiesEverywhere.Models.Mark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("MarkValue")
                        .HasColumnType("integer")
                        .HasColumnName("Mark");

                    b.Property<int>("RecipeId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("marks_pk");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserId");

                    b.ToTable("Marks");
                });

            modelBuilder.Entity("RecepiesEverywhere.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<string>("Picture")
                        .HasColumnType("text");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("recipes_pk");

                    b.HasIndex("AuthorId");

                    b.HasIndex("StatusId");

                    b.ToTable("Recipe", (string)null);
                });

            modelBuilder.Entity("RecepiesEverywhere.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("roles_pk");

                    b.HasIndex(new[] { "Name" }, "Roles_Name_key")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("RecepiesEverywhere.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("statuses_pk");

                    b.HasIndex(new[] { "Name" }, "Statuses_Name_key")
                        .IsUnique();

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("RecepiesEverywhere.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("users_pk");

                    b.HasIndex("RoleId");

                    b.HasIndex(new[] { "Name" }, "Users_Name_key")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RecepiesEverywhere.Models.Favourite", b =>
                {
                    b.HasOne("RecepiesEverywhere.Models.Recipe", "Recipe")
                        .WithMany("Favourites")
                        .HasForeignKey("RecipeId")
                        .IsRequired()
                        .HasConstraintName("favourite_recipe_fk");

                    b.HasOne("RecepiesEverywhere.Models.User", "User")
                        .WithMany("Favourites")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("favourite_user_fk");

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecepiesEverywhere.Models.Mark", b =>
                {
                    b.HasOne("RecepiesEverywhere.Models.Recipe", "Recipe")
                        .WithMany("Marks")
                        .HasForeignKey("RecipeId")
                        .IsRequired()
                        .HasConstraintName("marks_recipe_fk");

                    b.HasOne("RecepiesEverywhere.Models.User", "Users")
                        .WithMany("Marks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("RecepiesEverywhere.Models.Recipe", b =>
                {
                    b.HasOne("RecepiesEverywhere.Models.User", "Author")
                        .WithMany("Recipes")
                        .HasForeignKey("AuthorId")
                        .HasConstraintName("recipe_users_id_fk");

                    b.HasOne("RecepiesEverywhere.Models.Status", "Status")
                        .WithMany("Recipes")
                        .HasForeignKey("StatusId")
                        .IsRequired()
                        .HasConstraintName("recipe_statuses_id_fk");

                    b.Navigation("Author");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("RecepiesEverywhere.Models.User", b =>
                {
                    b.HasOne("RecepiesEverywhere.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("users_roles_fk");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("RecepiesEverywhere.Models.Recipe", b =>
                {
                    b.Navigation("Favourites");

                    b.Navigation("Marks");
                });

            modelBuilder.Entity("RecepiesEverywhere.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("RecepiesEverywhere.Models.Status", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("RecepiesEverywhere.Models.User", b =>
                {
                    b.Navigation("Favourites");

                    b.Navigation("Marks");

                    b.Navigation("Recipes");
                });
#pragma warning restore 612, 618
        }
    }
}
