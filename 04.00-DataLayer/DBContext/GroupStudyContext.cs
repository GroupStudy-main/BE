using DataLayer.DBObject;
using Microsoft.EntityFrameworkCore;
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
        public virtual DbSet<Connection> Connections { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupMember> GroupMembers { get; set; }
        public virtual DbSet<MeetingRoom> MeetingRooms { get; set; }
        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<GroupSubject> GroupSubjects { get; set; }
        public virtual DbSet<DocumentFile> DocumentFiles { get; set; }

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
                        Name =7
                    },
                    new Class
                    {
                        Id = 8,
                        Name =8
                    },
                    new Class
                    {
                        Id = 9,
                        Name =9
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
            }
        }
    }
}
