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
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Accounts");

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
                            Email = "student8@gmail.com",
                            FullName = "Tran Van H",
                            Password = "123456789",
                            Phone = "0123456789",
                            RoleId = 2,
                            Username = "student8"
                        },
                        new
                        {
                            Id = 9,
                            Email = "student9@gmail.com",
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

            modelBuilder.Entity("DataLayer.DBObject.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Classes");

                    b.HasData(
                        new
                        {
                            Id = 6,
                            Name = 6
                        },
                        new
                        {
                            Id = 7,
                            Name = 7
                        },
                        new
                        {
                            Id = 8,
                            Name = 8
                        },
                        new
                        {
                            Id = 9,
                            Name = 9
                        },
                        new
                        {
                            Id = 10,
                            Name = 10
                        },
                        new
                        {
                            Id = 11,
                            Name = 11
                        },
                        new
                        {
                            Id = 12,
                            Name = 12
                        });
                });

            modelBuilder.Entity("DataLayer.DBObject.Connection", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("End")
                        .HasColumnType("datetime2");

                    b.Property<int>("MeetingId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("MeetingId");

                    b.ToTable("MeetingParticipations");
                });

            modelBuilder.Entity("DataLayer.DBObject.DocumentFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Approved")
                        .HasColumnType("bit");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("HttpLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("DocumentFiles");
                });

            modelBuilder.Entity("DataLayer.DBObject.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClassId = 7,
                            Name = "Nhóm 1 của học sinh 1"
                        },
                        new
                        {
                            Id = 2,
                            ClassId = 7,
                            Name = "Nhóm 2 của học sinh 1"
                        },
                        new
                        {
                            Id = 3,
                            ClassId = 8,
                            Name = "Nhóm 1 của học sinh 2"
                        });
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

                    b.Property<string>("InviteMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("AccountId", "GroupId")
                        .IsUnique();

                    b.ToTable("GroupMembers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountId = 1,
                            GroupId = 1,
                            State = 0
                        },
                        new
                        {
                            Id = 2,
                            AccountId = 2,
                            GroupId = 1,
                            InviteMessage = "Nhóm của mình rất hay. Bạn vô nha",
                            State = 1
                        },
                        new
                        {
                            Id = 3,
                            AccountId = 3,
                            GroupId = 1,
                            InviteMessage = "Nhóm của mình rất hay. Bạn vô nha",
                            State = 2
                        },
                        new
                        {
                            Id = 4,
                            AccountId = 4,
                            GroupId = 1,
                            RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha",
                            State = 3
                        },
                        new
                        {
                            Id = 5,
                            AccountId = 5,
                            GroupId = 1,
                            RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha",
                            State = 4
                        },
                        new
                        {
                            Id = 6,
                            AccountId = 1,
                            GroupId = 2,
                            State = 0
                        },
                        new
                        {
                            Id = 7,
                            AccountId = 2,
                            GroupId = 2,
                            InviteMessage = "Nhóm của mình rất hay. Bạn vô nha",
                            State = 1
                        },
                        new
                        {
                            Id = 8,
                            AccountId = 3,
                            GroupId = 2,
                            InviteMessage = "Nhóm của mình rất hay. Bạn vô nha",
                            State = 2
                        },
                        new
                        {
                            Id = 9,
                            AccountId = 4,
                            GroupId = 2,
                            RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha",
                            State = 3
                        },
                        new
                        {
                            Id = 10,
                            AccountId = 2,
                            GroupId = 3,
                            State = 0
                        },
                        new
                        {
                            Id = 11,
                            AccountId = 1,
                            GroupId = 3,
                            InviteMessage = "Nhóm của mình rất hay. Bạn vô nha",
                            State = 1
                        },
                        new
                        {
                            Id = 12,
                            AccountId = 3,
                            GroupId = 3,
                            InviteMessage = "Nhóm của mình rất hay. Bạn vô nha",
                            State = 2
                        },
                        new
                        {
                            Id = 13,
                            AccountId = 4,
                            GroupId = 3,
                            RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha",
                            State = 3
                        });
                });

            modelBuilder.Entity("DataLayer.DBObject.GroupSubject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("SubjectId");

                    b.ToTable("GroupSubjects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            GroupId = 1,
                            SubjectId = 1
                        },
                        new
                        {
                            Id = 2,
                            GroupId = 1,
                            SubjectId = 4
                        },
                        new
                        {
                            Id = 3,
                            GroupId = 1,
                            SubjectId = 8
                        },
                        new
                        {
                            Id = 4,
                            GroupId = 2,
                            SubjectId = 1
                        },
                        new
                        {
                            Id = 5,
                            GroupId = 2,
                            SubjectId = 2
                        },
                        new
                        {
                            Id = 6,
                            GroupId = 2,
                            SubjectId = 3
                        },
                        new
                        {
                            Id = 7,
                            GroupId = 3,
                            SubjectId = 5
                        },
                        new
                        {
                            Id = 8,
                            GroupId = 3,
                            SubjectId = 6
                        },
                        new
                        {
                            Id = 9,
                            GroupId = 3,
                            SubjectId = 9
                        });
                });

            modelBuilder.Entity("DataLayer.DBObject.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CountMember")
                        .HasColumnType("int");

                    b.Property<DateTime?>("End")
                        .HasColumnType("datetime2");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("ScheduleEnd")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ScheduleStart")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Start")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Meetings");
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

                    b.ToTable("Roles");

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

            modelBuilder.Entity("DataLayer.DBObject.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DaysOfWeek")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("DataLayer.DBObject.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Subjects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Toán"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Lí"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Hóa"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Văn"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Sử"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Địa"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Sinh"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Anh"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Giáo dục công dân"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Công nghệ"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Quốc phòng"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Thể dục"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Tin"
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

            modelBuilder.Entity("DataLayer.DBObject.DocumentFile", b =>
                {
                    b.HasOne("DataLayer.DBObject.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("DataLayer.DBObject.Group", b =>
                {
                    b.HasOne("DataLayer.DBObject.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("DataLayer.DBObject.GroupMember", b =>
                {
                    b.HasOne("DataLayer.DBObject.Account", "Account")
                        .WithMany("GroupMembers")
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

            modelBuilder.Entity("DataLayer.DBObject.GroupSubject", b =>
                {
                    b.HasOne("DataLayer.DBObject.Group", "Group")
                        .WithMany("GroupSubjects")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.DBObject.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("DataLayer.DBObject.Meeting", b =>
                {
                    b.HasOne("DataLayer.DBObject.Group", "Group")
                        .WithMany("Meetings")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.DBObject.Schedule", "Schedule")
                        .WithMany("Meetings")
                        .HasForeignKey("ScheduleId");

                    b.Navigation("Group");

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("DataLayer.DBObject.Schedule", b =>
                {
                    b.HasOne("DataLayer.DBObject.Group", "Group")
                        .WithMany("Schedules")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("DataLayer.DBObject.Account", b =>
                {
                    b.Navigation("GroupMembers");
                });

            modelBuilder.Entity("DataLayer.DBObject.Group", b =>
                {
                    b.Navigation("GroupMembers");

                    b.Navigation("GroupSubjects");

                    b.Navigation("Meetings");

                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("DataLayer.DBObject.Meeting", b =>
                {
                    b.Navigation("Connections");
                });

            modelBuilder.Entity("DataLayer.DBObject.Role", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("DataLayer.DBObject.Schedule", b =>
                {
                    b.Navigation("Meetings");
                });
#pragma warning restore 612, 618
        }
    }
}
