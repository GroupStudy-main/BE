﻿// <auto-generated />
using System;
using DataLayer.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(GroupStudyContext))]
    partial class GroupStudyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataLayer.DBObject.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Accounts", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "trankhaiminhkhoi10a3@gmail.com",
                            FullName = "Nguyen Van A",
                            Password = "123456789",
                            Phone = "0123456789",
                            RoleId = 2,
                            Username = "student1"
                        },
                        new
                        {
                            Id = 2,
                            Email = "student2@gmail.com",
                            FullName = "Dao Thi B",
                            Password = "123456789",
                            Phone = "0123456789",
                            RoleId = 2,
                            Username = "student2"
                        },
                        new
                        {
                            Id = 3,
                            Email = "student3@gmail.com",
                            FullName = "Tran Van C",
                            Password = "123456789",
                            Phone = "0123456789",
                            RoleId = 2,
                            Username = "student3"
                        },
                        new
                        {
                            Id = 4,
                            Email = "student4@gmail.com",
                            FullName = "Li Thi D",
                            Password = "123456789",
                            Phone = "0123456789",
                            RoleId = 2,
                            Username = "student4"
                        },
                        new
                        {
                            Id = 5,
                            Email = "student5@gmail.com",
                            FullName = "Tran Van E",
                            Password = "123456789",
                            Phone = "0123456789",
                            RoleId = 2,
                            Username = "student5"
                        },
                        new
                        {
                            Id = 6,
                            Email = "student6@gmail.com",
                            FullName = "Li Chinh F",
                            Password = "123456789",
                            Phone = "0123456789",
                            RoleId = 2,
                            Username = "student6"
                        },
                        new
                        {
                            Id = 7,
                            Email = "student7@gmail.com",
                            FullName = "Ngo Van G",
                            Password = "123456789",
                            Phone = "0123456789",
                            RoleId = 2,
                            Username = "student7"
                        },
                        new
                        {
                            Id = 8,
                            Email = "student3@gmail.com",
                            FullName = "Tran Van H",
                            Password = "123456789",
                            Phone = "0123456789",
                            RoleId = 2,
                            Username = "student8"
                        },
                        new
                        {
                            Id = 9,
                            Email = "student10@gmail.com",
                            FullName = "Tran Van I",
                            Password = "123456789",
                            Phone = "0123456789",
                            RoleId = 2,
                            Username = "student9"
                        },
                        new
                        {
                            Id = 10,
                            Email = "student10@gmail.com",
                            FullName = "Tran Van J",
                            Password = "123456789",
                            Phone = "0123456789",
                            RoleId = 2,
                            Username = "student10"
                        },
                        new
                        {
                            Id = 11,
                            Email = "trankhaiminhkhoi@gmail.com",
                            FullName = "Tran Khoi",
                            Password = "123456789",
                            Phone = "0123456789",
                            RoleId = 1,
                            Username = "parent1"
                        });
                });

            modelBuilder.Entity("DataLayer.DBObject.Connection", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("MeetingId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("MeetingId");

                    b.ToTable("Connections", (string)null);
                });

            modelBuilder.Entity("DataLayer.DBObject.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Groups", (string)null);
                });

            modelBuilder.Entity("DataLayer.DBObject.GroupMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsLeader")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupMembers", (string)null);
                });

            modelBuilder.Entity("DataLayer.DBObject.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("End")
                        .HasColumnType("datetime2");

                    b.Property<int>("MeetingRoomId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ScheduleEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ScheduleStart")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MeetingRoomId");

                    b.ToTable("Meetings", (string)null);
                });

            modelBuilder.Entity("DataLayer.DBObject.MeetingRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CountMember")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("MeetingRooms", (string)null);
                });

            modelBuilder.Entity("DataLayer.DBObject.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Parent"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Student"
                        });
                });

            modelBuilder.Entity("DataLayer.DBObject.Account", b =>
                {
                    b.HasOne("DataLayer.DBObject.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DataLayer.DBObject.Connection", b =>
                {
                    b.HasOne("DataLayer.DBObject.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.DBObject.Meeting", "Meeting")
                        .WithMany("Connections")
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Meeting");
                });

            modelBuilder.Entity("DataLayer.DBObject.GroupMember", b =>
                {
                    b.HasOne("DataLayer.DBObject.Account", "Account")
                        .WithMany("GroupMember")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.DBObject.Group", "Group")
                        .WithMany("GroupMembers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("DataLayer.DBObject.Meeting", b =>
                {
                    b.HasOne("DataLayer.DBObject.MeetingRoom", "MeetingRoom")
                        .WithMany("Meetings")
                        .HasForeignKey("MeetingRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MeetingRoom");
                });

            modelBuilder.Entity("DataLayer.DBObject.MeetingRoom", b =>
                {
                    b.HasOne("DataLayer.DBObject.Group", "Group")
                        .WithMany("MeetingRooms")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("DataLayer.DBObject.Account", b =>
                {
                    b.Navigation("GroupMember");
                });

            modelBuilder.Entity("DataLayer.DBObject.Group", b =>
                {
                    b.Navigation("GroupMembers");

                    b.Navigation("MeetingRooms");
                });

            modelBuilder.Entity("DataLayer.DBObject.Meeting", b =>
                {
                    b.Navigation("Connections");
                });

            modelBuilder.Entity("DataLayer.DBObject.MeetingRoom", b =>
                {
                    b.Navigation("Meetings");
                });

            modelBuilder.Entity("DataLayer.DBObject.Role", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
