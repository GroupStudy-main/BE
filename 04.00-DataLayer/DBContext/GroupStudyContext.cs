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
    public class GroupStudyContext  : DbContext
    {
        public GroupStudyContext(DbContextOptions<GroupStudyContext> options):base(options)
        {
            
        }
        public virtual DbSet<Account> Accounts { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<MeetingRoom> MeetingRooms { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Seed();
            void Seed() {
                modelBuilder.Entity<Role>().HasData(
                    new Role
                    {
                        Id = 1,
                        Name = "Parent"
                    },
                    new Role
                    {
                        Id=2 ,
                        Name="Student"
                    }
                    ) ;
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
                        FullName ="Dao Thi B",
                        Email = "student2@gmail.com",
                        Password = "123456789",
                        Phone = "0123456789",
                        RoleId = 2
                    },
                    new Account
                    {
                        Id = 3,
                        Username = "student3",
                        FullName ="Tran Van C",
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
                        FullName="Tran Khoi",
                        Email = "trankhaiminhkhoi@gmail.com",
                        Password = "123456789",
                        Phone = "0123456789",
                        RoleId = 1
                    }
                );
            }
        }
    }
}
