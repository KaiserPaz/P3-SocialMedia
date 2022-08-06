﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SocialMediaWebAPI.Models.EF
{
    public partial class SocialDBContext : DbContext
    {
        public SocialDBContext()
        {
        }

        public SocialDBContext(DbContextOptions<SocialDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<ParentChildComment> ParentChildComments { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserFriend> UserFriends { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:nikhils-p2.database.windows.net,1433;Initial Catalog=SocialDB;Persist Security Info=False;User ID=trainer;Password=Password@1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CommentId).HasColumnName("commentId");

                entity.Property(e => e.Comment1)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("comment");

                entity.Property(e => e.CommentImage)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("comment_Image");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedAt");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("fk_postId_comment");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_userId_comment");
            });

            modelBuilder.Entity<ParentChildComment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("parent_child_Comment");

                entity.Property(e => e.ChildCommentId).HasColumnName("child_CommentId");

                entity.Property(e => e.ParentCommentId).HasColumnName("parent_CommentId");

                entity.HasOne(d => d.ChildComment)
                    .WithMany()
                    .HasForeignKey(d => d.ChildCommentId)
                    .HasConstraintName("fk_child_CommentId");

                entity.HasOne(d => d.ParentComment)
                    .WithMany()
                    .HasForeignKey(d => d.ParentCommentId)
                    .HasConstraintName("fk_parent_CommentId");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Image)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("message");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedAt");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_userId_post");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "uk_email")
                    .IsUnique();

                entity.HasIndex(e => e.MobilePhone, "uk_mobilePhone")
                    .IsUnique();

                entity.HasIndex(e => e.UserName, "uk_userName")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.Intro)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("intro");

                entity.Property(e => e.LastLogin)
                    .HasColumnType("datetime")
                    .HasColumnName("lastLogin");

                entity.Property(e => e.LastName)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("lastName");

                entity.Property(e => e.MobilePhone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("mobilePhone");

                entity.Property(e => e.Password)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Profile)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("profile");

                entity.Property(e => e.ProfilePicture)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("profilePicture");

                entity.Property(e => e.RegisteredAt)
                    .HasColumnType("datetime")
                    .HasColumnName("registeredAt");

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("userName");
            });

            modelBuilder.Entity<UserFriend>(entity =>
            {
                entity.ToTable("user_Friend");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.FriendId).HasColumnName("friendId");

                entity.Property(e => e.FriendStatus)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("friendStatus");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedAt");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Friend)
                    .WithMany(p => p.UserFriendFriends)
                    .HasForeignKey(d => d.FriendId)
                    .HasConstraintName("fk_friendId_friend");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserFriendUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_userId_friend");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
