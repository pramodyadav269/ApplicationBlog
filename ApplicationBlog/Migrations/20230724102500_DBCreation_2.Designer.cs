﻿// <auto-generated />
using System;
using ApplicationBlog.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApplicationBlog.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    [Migration("20230724102500_DBCreation_2")]
    partial class DBCreation_2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ApplicationBlog.Model.ErrorLog", b =>
                {
                    b.Property<long>("ErrorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ErrorId"), 1L, 1);

                    b.Property<string>("ActionName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ClientIP")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ControllerName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ErrorStackTrace")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<string>("JSONRequest")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("RequestTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("ErrorId");

                    b.ToTable("tblErrorLog");
                });

            modelBuilder.Entity("ApplicationBlog.Model.UserPost", b =>
                {
                    b.Property<long>("UserPostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserPostId"), 1L, 1);

                    b.Property<long?>("CommentCount")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<long?>("LikeCount")
                        .HasColumnType("bigint");

                    b.Property<string>("PostMediaPath")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("PostText")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<string>("PostType")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("PostedOn")
                        .HasColumnType("datetime2");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("UserPostId");

                    b.ToTable("tblUserPost");
                });

            modelBuilder.Entity("ApplicationBlog.Model.UserPostComment", b =>
                {
                    b.Property<long>("UserPostCommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserPostCommentId"), 1L, 1);

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("CommentedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserPostId")
                        .HasColumnType("bigint");

                    b.HasKey("UserPostCommentId");

                    b.ToTable("tblUserPostComment");
                });

            modelBuilder.Entity("ApplicationBlog.Model.UserPostLike", b =>
                {
                    b.Property<long>("UserPostLikeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserPostLikeId"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LikedOn")
                        .HasColumnType("datetime2");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserPostId")
                        .HasColumnType("bigint");

                    b.HasKey("UserPostLikeId");

                    b.ToTable("tblUserPostLike");
                });

            modelBuilder.Entity("ApplicationBlog.Model.UserProfilePic", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ProfilePic")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("RegisteredOn")
                        .HasColumnType("datetime2");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("tblUserProfilePic");
                });

            modelBuilder.Entity("ApplicationBlog.Model.Users", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserId"), 1L, 1);

                    b.Property<DateTime?>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("char(1)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime?>("RegisteredOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("UserId");

                    b.ToTable("tblUsersMaster");
                });
#pragma warning restore 612, 618
        }
    }
}
