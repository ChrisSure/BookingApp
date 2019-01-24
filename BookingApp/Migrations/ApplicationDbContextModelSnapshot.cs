﻿// <auto-generated />
using System;
using BookingApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookingApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BookingApp.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool?>("IsApproved")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

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

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("BookingApp.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("CreatedUserId")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndTime");

                    b.Property<bool>("IsCancelled");

                    b.Property<int>("ResourceId");

                    b.Property<DateTime>("StartTime");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("UpdatedUserId")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.HasKey("BookingId");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("ResourceId");

                    b.HasIndex("UpdatedUserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("BookingApp.Models.Resource", b =>
                {
                    b.Property<int>("ResourceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("CreatedUserId")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.Property<string>("Description");

                    b.Property<int>("RuleId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int?>("TreeGroupId");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("UpdatedUserId")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.HasKey("ResourceId");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("RuleId");

                    b.HasIndex("TreeGroupId");

                    b.HasIndex("UpdatedUserId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("BookingApp.Models.Rule", b =>
                {
                    b.Property<int>("RuleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("CreatedUserId")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.Property<int?>("MaxTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1440);

                    b.Property<int?>("MinTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.Property<int?>("PreOrderTimeLimit")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1440);

                    b.Property<int?>("ReuseTimeout")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int?>("ServiceTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int?>("StepTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("UpdatedUserId")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.HasKey("RuleId");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("UpdatedUserId");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("BookingApp.Models.TreeGroup", b =>
                {
                    b.Property<int>("TreeGroupId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("CreatedUserId")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.Property<int?>("DefaultRuleId");

                    b.Property<int?>("ParentTreeGroupId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("UpdatedUserId")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.HasKey("TreeGroupId");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("DefaultRuleId");

                    b.HasIndex("ParentTreeGroupId");

                    b.HasIndex("UpdatedUserId");

                    b.ToTable("TreeGroups");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
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

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BookingApp.Models.Booking", b =>
                {
                    b.HasOne("BookingApp.Models.ApplicationUser", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BookingApp.Models.Resource", "Resource")
                        .WithMany("Bookings")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BookingApp.Models.ApplicationUser", "Updater")
                        .WithMany()
                        .HasForeignKey("UpdatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BookingApp.Models.Resource", b =>
                {
                    b.HasOne("BookingApp.Models.ApplicationUser", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BookingApp.Models.Rule", "Rule")
                        .WithMany()
                        .HasForeignKey("RuleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BookingApp.Models.TreeGroup", "TreeGroup")
                        .WithMany("Resources")
                        .HasForeignKey("TreeGroupId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BookingApp.Models.ApplicationUser", "Updater")
                        .WithMany()
                        .HasForeignKey("UpdatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BookingApp.Models.Rule", b =>
                {
                    b.HasOne("BookingApp.Models.ApplicationUser", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BookingApp.Models.ApplicationUser", "Updater")
                        .WithMany()
                        .HasForeignKey("UpdatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BookingApp.Models.TreeGroup", b =>
                {
                    b.HasOne("BookingApp.Models.ApplicationUser", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BookingApp.Models.Rule", "DefaultRule")
                        .WithMany()
                        .HasForeignKey("DefaultRuleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BookingApp.Models.TreeGroup", "ParentTreeGroup")
                        .WithMany("ChildGroups")
                        .HasForeignKey("ParentTreeGroupId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BookingApp.Models.ApplicationUser", "Updater")
                        .WithMany()
                        .HasForeignKey("UpdatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BookingApp.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BookingApp.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BookingApp.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BookingApp.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
