﻿using DataLayer.DBObject;
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
                    new Account {
                        Id = 1,
                        Usename = "student1",
                        Email = "trankhaiminhkhoi10a3@gmail.com",
                        Password = "123456789"   ,
                        RoleId=2
                    },
                    new Account
                    {
                        Id = 2,
                        Usename = "student2",
                        Email = "@gmail.com",
                        Password = "123456789"  ,
                        RoleId=2
                    },
                    new Account
                    {
                        Id = 3,
                        Usename = "student4",
                        Email = "@gmail.com",
                        Password = "123456789",
                        RoleId=2
                    },
                    new Account
                    {
                        Id = 4,
                        Usename = "parent1",
                        Email = "trankhaiminhkhoi@gmail.com",
                        Password = "123456789" ,
                        RoleId=1
                    }
                );
            }
        }
    }
}
