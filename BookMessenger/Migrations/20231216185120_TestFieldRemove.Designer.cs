﻿// <auto-generated />
using System;
using BookMessenger.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookMessenger.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20231216185120_TestFieldRemove")]
    partial class TestFieldRemove
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("BookGenre", b =>
                {
                    b.Property<int>("BooksId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GenresId")
                        .HasColumnType("INTEGER");

                    b.HasKey("BooksId", "GenresId");

                    b.HasIndex("GenresId");

                    b.ToTable("BookGenre");
                });

            modelBuilder.Entity("BookMessenger.Models.AdminActionLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("NameUser")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleCurrent")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RolePrevious")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("AdminActions");
                });

            modelBuilder.Entity("BookMessenger.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("BookMessenger.Models.AuthorBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfAuthor")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BookId");

                    b.ToTable("AuthorBooks");
                });

            modelBuilder.Entity("BookMessenger.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookMessenger.Models.Discuss", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Discusses");
                });

            modelBuilder.Entity("BookMessenger.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("BookMessenger.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("DiscussId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserProfileId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DiscussId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("BookMessenger.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BookMessenger.Models.UserBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("HasInLibrary")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MarkValue")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserProfileId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Marks");
                });

            modelBuilder.Entity("BookMessenger.Models.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("BookGenre", b =>
                {
                    b.HasOne("BookMessenger.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookMessenger.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookMessenger.Models.AuthorBook", b =>
                {
                    b.HasOne("BookMessenger.Models.Author", "Author")
                        .WithMany("AuthorBooks")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookMessenger.Models.Book", "Book")
                        .WithMany("AuthorBooks")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("BookMessenger.Models.Message", b =>
                {
                    b.HasOne("BookMessenger.Models.Discuss", "Discuss")
                        .WithMany("Messages")
                        .HasForeignKey("DiscussId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookMessenger.Models.UserProfile", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discuss");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookMessenger.Models.UserBook", b =>
                {
                    b.HasOne("BookMessenger.Models.Book", "Book")
                        .WithMany("UserBooks")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookMessenger.Models.UserProfile", "User")
                        .WithMany("UserBooks")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookMessenger.Models.UserProfile", b =>
                {
                    b.HasOne("BookMessenger.Models.User", "User")
                        .WithOne("UserProfile")
                        .HasForeignKey("BookMessenger.Models.UserProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookMessenger.Models.Author", b =>
                {
                    b.Navigation("AuthorBooks");
                });

            modelBuilder.Entity("BookMessenger.Models.Book", b =>
                {
                    b.Navigation("AuthorBooks");

                    b.Navigation("UserBooks");
                });

            modelBuilder.Entity("BookMessenger.Models.Discuss", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("BookMessenger.Models.User", b =>
                {
                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("BookMessenger.Models.UserProfile", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("UserBooks");
                });
#pragma warning restore 612, 618
        }
    }
}
