using DataLayer.DBObject;
using Microsoft.EntityFrameworkCore;
using ShareResource.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DBContext
{
    public class GroupStudyContext : DbContext
    {
        public GroupStudyContext(DbContextOptions<GroupStudyContext> options) : base(options)
        {

        }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Connection> Connections { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupMember> GroupMembers { get; set; }
        public virtual DbSet<GroupSubject> GroupSubjects { get; set; }
        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<MeetingRoom> MeetingRooms { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Username).IsUnique();
            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Email).IsUnique();

            Seed();
            void Seed()
            {
                #region seed Role
                modelBuilder.Entity<Role>().HasData(
                    new Role
                    {
                        Id = 1,
                        Name = "Parent"
                    },
                    new Role
                    {
                        Id = 2,
                        Name = "Student"
                    }
                );
                #endregion

                #region seed Account
                modelBuilder.Entity<Account>().HasData(
                    new Account
                    {
                        Id = 1,
                        Username = "student1",
                        FullName = "Nguyen Van A",
                        Email = "trankhaiminhkhoi10a3@gmail.com",
                        Password = "123456789",
                        Phone = "0123456789",
                        RoleId = 2
                    },
                    new Account
                    {
                        Id = 2,
                        Username = "student2",
                        FullName = "Dao Thi B",
                        Email = "student2@gmail.com",
                        Password = "123456789",
                        Phone = "0123456789",
                        RoleId = 2
                    },
                    new Account
                    {
                        Id = 3,
                        Username = "student3",
                        FullName = "Tran Van C",
                        Email = "student3@gmail.com",
                        Password = "123456789",
                        Phone = "0123456789",
                        RoleId = 2
                    },
                    new Account
                    {
                        Id = 4,
                        Username = "student4",
                        FullName = "Li Thi D",
                        Email = "student4@gmail.com",
                        Password = "123456789",
                        Phone = "0123456789",
                        RoleId = 2
                    },
                    new Account
                    {
                        Id = 5,
                        Username = "student5",
                        FullName = "Tran Van E",
                        Email = "student5@gmail.com",
                        Password = "123456789",
                        Phone = "0123456789",
                        RoleId = 2
                    },
                    new Account
                    {
                        Id = 6,
                        Username = "student6",
                        FullName = "Li Chinh F",
                        Email = "student6@gmail.com",
                        Password = "123456789",
                        Phone = "0123456789",
                        RoleId = 2
                    },
                    new Account
                    {
                        Id = 7,
                        Username = "student7",
                        FullName = "Ngo Van G",
                        Email = "student7@gmail.com",
                        Password = "123456789",
                        Phone = "0123456789",
                        RoleId = 2
                    },
                    new Account
                    {
                        Id = 8,
                        Username = "student8",
                        FullName = "Tran Van H",
                        Email = "student3@gmail.com",
                        Password = "123456789",
                        Phone = "0123456789",
                        RoleId = 2
                    }
                    , new Account
                    {
                        Id = 9,
                        Username = "student9",
                        FullName = "Tran Van I",
                        Email = "student10@gmail.com",
                        Password = "123456789",
                        Phone = "0123456789",
                        RoleId = 2
                    }, new Account
                    {
                        Id = 10,
                        Username = "student10",
                        FullName = "Tran Van J",
                        Email = "student10@gmail.com",
                        Password = "123456789",
                        Phone = "0123456789",
                        RoleId = 2
                    },
                    new Account
                    {
                        Id = 11,
                        Username = "parent1",
                        FullName = "Tran Khoi",
                        Email = "trankhaiminhkhoi@gmail.com",
                        Password = "123456789",
                        Phone = "0123456789",
                        RoleId = 1
                    }
                );
                #endregion

                #region seed class
                modelBuilder.Entity<Class>().HasData(
                    new Class
                    {
                        Id = 6,
                        Name = 6
                    },
                    new Class
                    {
                        Id = 7,
                        Name = 7
                    },
                    new Class
                    {
                        Id = 8,
                        Name = 8
                    },
                    new Class
                    {
                        Id = 9,
                        Name = 9
                    },
                    new Class
                    {
                        Id = 10,
                        Name = 10
                    },
                    new Class
                    {
                        Id = 11,
                        Name = 11
                    },
                    new Class
                    {
                        Id = 12,
                        Name = 12
                    }
                );
                #endregion

                #region seed Subject
                modelBuilder.Entity<Subject>().HasData(
                    new Subject
                    {
                        Id = 1,
                        Name = "Toán"
                    },
                    new Subject
                    {
                        Id = 2,
                        Name = "Lí"
                    },
                    new Subject
                    {
                        Id = 3,
                        Name = "Hóa"
                    },
                    new Subject
                    {
                        Id = 4,
                        Name = "Văn"
                    },
                    new Subject
                    {
                        Id = 5,
                        Name = "Sử"
                    },
                    new Subject
                    {
                        Id = 6,
                        Name = "Địa"
                    },
                    new Subject
                    {
                        Id = 7,
                        Name = "Sinh"
                    },
                    new Subject
                    {
                        Id = 8,
                        Name = "Anh"
                    },
                    new Subject
                    {
                        Id = 9,
                        Name = "Giáo dục công dân"
                    },
                    new Subject
                    {
                        Id = 10,
                        Name = "Công nghệ"
                    },
                    new Subject
                    {
                        Id = 11,
                        Name = "Quốc phòng"
                    },
                    new Subject
                    {
                        Id = 12,
                        Name = "Thể dục"
                    },
                    new Subject
                    {
                        Id = 13,
                        Name = "Tin"
                    }
                );
                #endregion

                #region seed Group, GroupMember, Group Subject
                #region seed Group
                modelBuilder.Entity<Group>().HasData(
                        //Group 1
                        new Group
                        {
                            Id = 1,
                            Name = "Nhóm 1 của học sinh 1",
                            ClassId = 7,
                        },
                        new Group
                        {
                            Id = 2,
                            Name = "Nhóm 2 của học sinh 1",
                            ClassId = 7,
                        } ,
                        new Group
                        {
                            Id = 3,
                            Name = "Nhóm 1 của học sinh 2",
                            ClassId = 8,
                        }
                    );

                #region seed later
                //e.HasData(
                
                //new Group
                //{
                //    Id = 3,
                //    Name = "Nhóm 1 của học sinh 2",
                //    ClassId = 8,
                //    GroupSubjects = new GroupSubject[]
                //  {
                //      
                //  },
                //    GroupMembers = new GroupMember[]
                //  {
                //      
                //  },

                //}
                //);
                #endregion

                #endregion
                #region seed GroupSubjects
                modelBuilder.Entity<GroupSubject>().HasData(
                #region Subject Group 1
                    new GroupSubject
                    {
                        Id = 1,
                        GroupId = 1,
                        SubjectId = (int)SubjectEnum.Toan
                    },
                    new GroupSubject
                    {
                        Id = 2,
                        GroupId = 1,
                        SubjectId = (int)SubjectEnum.Van
                    },
                    new GroupSubject
                    {
                        Id = 3,
                        GroupId = 1,
                        SubjectId = (int)SubjectEnum.Anh
                    },
                #endregion
                #region Subject group 2
                    new GroupSubject
                    {
                        Id = 4,
                        GroupId = 2,
                        SubjectId = (int)SubjectEnum.Toan
                    },
                    new GroupSubject
                    {
                        Id = 5,
                        GroupId = 2,
                        SubjectId = (int)SubjectEnum.Li
                    },
                    new GroupSubject
                    {
                        Id = 6,
                        GroupId = 2,
                        SubjectId = (int)SubjectEnum.Hoa
                    } ,
                #endregion
                #region Subject group 3
                    new GroupSubject
                    {
                        Id = 7,
                        GroupId = 3,
                        SubjectId = (int)SubjectEnum.Su
                    },
                    new GroupSubject
                    {
                        Id = 8,
                        GroupId = 3,
                        SubjectId = (int)SubjectEnum.Dia
                    },
                    new GroupSubject
                    {
                        Id = 9,
                        GroupId = 3,
                        SubjectId = (int)SubjectEnum.Gdcd
                    }
                #endregion
                );
                #endregion
                modelBuilder.Entity<GroupMember>().HasData(
                    #region Member Group 1
                    new GroupMember
                    {
                       Id = 1,
                       GroupId = 1,
                       AccountId = 1,
                       State = GroupMemberState.Leader
                    },
                    new GroupMember
                    {
                        Id = 2,
                        GroupId = 1,
                        AccountId = 2,
                        State = GroupMemberState.Member,
                        InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                    },
                    new GroupMember
                    {
                        Id = 3,
                        GroupId = 1,
                        AccountId = 3,
                        State = GroupMemberState.Inviting,
                        InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                    },
                    new GroupMember
                    {
                        Id = 4,
                        GroupId = 1,
                        AccountId = 4,
                        State = GroupMemberState.Requesting,
                        RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha"
                    },
                    new GroupMember
                    {
                        Id = 5,
                        GroupId = 1,
                        AccountId = 5,
                        State = GroupMemberState.Banned,
                        RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha"
                    },
                    #endregion
                    #region Member group 2
                    new GroupMember
                    {
                        Id = 6,
                        GroupId = 2,
                        AccountId = 1,
                        State = GroupMemberState.Leader
                    },
                    new GroupMember
                    {
                        Id = 7,
                        GroupId = 2,
                        AccountId = 2,
                        State = GroupMemberState.Member,
                        InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                    },
                    new GroupMember
                    {
                        Id = 8,
                        GroupId = 2,
                        AccountId = 3,
                        State = GroupMemberState.Inviting,
                        InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                    },
                    new GroupMember
                    {
                        Id = 9,
                        GroupId = 2,
                        AccountId = 4,
                        State = GroupMemberState.Requesting,
                        RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha"
                    },
                    #endregion
                    #region Member group 3
                    new GroupMember
                    {
                        Id = 10,
                        GroupId = 3,
                        AccountId = 2,
                        State = GroupMemberState.Leader
                    },
                    new GroupMember
                    {
                        Id = 11,
                        GroupId = 3,
                        AccountId = 1,
                        State = GroupMemberState.Member,
                        InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                    },
                    new GroupMember
                    {
                        Id = 12,
                        GroupId = 3,
                        AccountId = 3,
                        State = GroupMemberState.Inviting,
                        InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                    },
                    new GroupMember
                    {
                        Id = 13,
                        GroupId = 3,
                        AccountId = 4,
                        State = GroupMemberState.Requesting,
                        RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha"
                    }
                    #endregion
               );
                #endregion




            }
        }
    }
}
