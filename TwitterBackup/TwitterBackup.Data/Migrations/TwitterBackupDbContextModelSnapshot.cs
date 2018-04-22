﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TwitterBackup.Data;

namespace TwitterBackup.Data.Migrations
{
    [DbContext(typeof(TwitterBackupDbContext))]
    partial class TwitterBackupDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TwitterBackup.Data.Models.Identity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("TwitterBackup.Data.Models.Identity.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("TwitterBackup.Data.Models.Identity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TwitterBackup.Data.Models.Identity.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("TwitterBackup.Data.Models.Identity.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("TwitterBackup.Data.Models.Identity.UserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("TwitterBackup.Data.Models.Identity.UserToken", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("TwitterBackup.Data.Models.TwAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<DateTime?>("CreatedAtTwitter");

                    b.Property<string>("Description");

                    b.Property<int>("FollowersCount");

                    b.Property<int>("FriendsCount");

                    b.Property<string>("Name");

                    b.Property<string>("ProfileImageUrl");

                    b.Property<string>("ScreenName");

                    b.Property<string>("TwitterId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("TwitterAccounts");
                });

            modelBuilder.Entity("TwitterBackup.Data.Models.TwAccountImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ProfileImage");

                    b.Property<int>("TwAccountId");

                    b.HasKey("Id");

                    b.HasIndex("TwAccountId")
                        .IsUnique();

                    b.ToTable("TwitterAccountImages");
                });

            modelBuilder.Entity("TwitterBackup.Data.Models.UserTwAccount", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("TwAccountId");

                    b.HasKey("UserId", "TwAccountId");

                    b.HasIndex("TwAccountId");

                    b.ToTable("UserTwAccount");
                });

            modelBuilder.Entity("TwitterBackup.Data.Models.Identity.RoleClaim", b =>
                {
                    b.HasOne("TwitterBackup.Data.Models.Identity.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TwitterBackup.Data.Models.Identity.UserClaim", b =>
                {
                    b.HasOne("TwitterBackup.Data.Models.Identity.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TwitterBackup.Data.Models.Identity.UserLogin", b =>
                {
                    b.HasOne("TwitterBackup.Data.Models.Identity.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TwitterBackup.Data.Models.Identity.UserRole", b =>
                {
                    b.HasOne("TwitterBackup.Data.Models.Identity.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TwitterBackup.Data.Models.Identity.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TwitterBackup.Data.Models.Identity.UserToken", b =>
                {
                    b.HasOne("TwitterBackup.Data.Models.Identity.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TwitterBackup.Data.Models.TwAccountImage", b =>
                {
                    b.HasOne("TwitterBackup.Data.Models.TwAccount", "TwAccount")
                        .WithOne("TwAccountImage")
                        .HasForeignKey("TwitterBackup.Data.Models.TwAccountImage", "TwAccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TwitterBackup.Data.Models.UserTwAccount", b =>
                {
                    b.HasOne("TwitterBackup.Data.Models.TwAccount", "TwAccount")
                        .WithMany("Users")
                        .HasForeignKey("TwAccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TwitterBackup.Data.Models.Identity.User", "User")
                        .WithMany("FavouriteUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
