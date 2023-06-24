using DataLayer.DBContext;
using DataLayer.DBObject;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ShareResource.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIExtension.ImMemorySeeding
{
    public static class DbInitializerExtension
    {
        public static IApplicationBuilder SeedInMemoryDb(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<GroupStudyContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return app;
        }
        public class DbInitializer
        {
            internal static void Initialize(GroupStudyContext context)
            {
                ArgumentNullException.ThrowIfNull(context, nameof(context));
                context.Database.EnsureCreated();
                #region seed Role
                if (!context.Roles.Any())
                {
                    var roles = new Role[] {
                        new Role
                    {
                        Id = 1,
                        Name = "Parent"
                    },
                    new Role
                    {
                        Id = 2,
                        Name = "Student"
                    }};
                    context.Roles.AddRange(roles);
                }
                #endregion

                #region seed Account
                if (!context.Accounts.Any())
                {
                    var accounts = new Account[] {
                        new Account
                        {
                            Id = 1,
                            Username = "student1",
                            FullName = "Tran Khai Minh Khoi InMemory",
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
                            Email = "student8@gmail.com",
                            Password = "123456789",
                            Phone = "0123456789",
                            RoleId = 2
                        }
                        , new Account
                        {
                            Id = 9,
                            Username = "student9",
                            FullName = "Tran Van I",
                            Email = "student9@gmail.com",
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
                    };
                    context.Accounts.AddRange(accounts);

                }
                #endregion

                #region seed class
                if (!context.Classes.Any())
                {
                    var classes = new Class[]
                    {
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
                    };
                    context.Classes.AddRange(classes);
                }
                #endregion

                #region seed subject
                if (!context.Subjects.Any())
                {
                    var subjects = new Subject[]
                    {
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
                    };
                    context.Subjects.AddRange(subjects);
                }
                #endregion

                #region seed group
                if (!context.Groups.Any())
                {
                    var groups = new Group[]
                    {
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
                        } ,
                        new Group
                        {
                            Id = 4,
                            Name = "Nhóm 2 của học sinh 2",
                            ClassId = 8,
                        },
                        new Group
                        {
                            Id = 5,
                            Name = "Nhóm 1 của học sinh 3",
                            ClassId = 8,
                        },
                        new Group
                        {
                            Id = 6,
                            Name = "Nhóm 2 của học sinh 3",
                            ClassId = 8,
                        },
                    };
                    context.Groups.AddRange(groups);
                }
                #endregion

                #region seed group member
                if (!context.GroupMembers.Any())
                {
                    var groupMembers = new GroupMember[]
                    {
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
                            //InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                        },
                        //Fix later
                        //new GroupMember
                        //{
                        //    Id = 3,
                        //    GroupId = 1,
                        //    AccountId = 3,
                        //    State = GroupMemberState.Inviting,
                        //    //InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                        //},
                        //new GroupMember
                        //{
                        //    Id = 4,
                        //    GroupId = 1,
                        //    AccountId = 4,
                        //    State = GroupMemberState.Requesting,
                        //    //RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha"
                        //},
                        new GroupMember
                        {
                            //Id = 5,
                            Id = 3,
                            GroupId = 1,
                            AccountId = 5,
                            State = GroupMemberState.Banned,
                            //RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha"
                        },
                        #endregion
                        #region Member group 2
                        new GroupMember
                        {
                            //Id = 6,
                            Id = 4,
                            GroupId = 2,
                            AccountId = 1,
                            State = GroupMemberState.Leader
                        },
                        new GroupMember
                        {
                            //Id = 7,
                            Id = 5,
                            GroupId = 2,
                            AccountId = 2,
                            State = GroupMemberState.Member,
                            ////InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                        },
                        //new GroupMember
                        //{
                        //    Id = 8,
                        //    GroupId = 2,
                        //    AccountId = 3,
                        //    State = GroupMemberState.Requesting,
                        //    //InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                        //},
                        //new GroupMember
                        //{
                        //    Id = 9,
                        //    GroupId = 2,
                        //    AccountId = 4,
                        //    State = GroupMemberState.Inviting,
                        //    //RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha"
                        //},
                        #endregion   
                        #region Member group 3
                        new GroupMember
                        {
                            //Id = 10,
                            Id = 6,
                            GroupId = 3,
                            AccountId = 2,
                            State = GroupMemberState.Leader
                        },
                        new GroupMember
                        {
                            //Id = 11,
                            Id = 7,
                            GroupId = 3,
                            AccountId = 1,
                            State = GroupMemberState.Member,
                            //InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                        },
                        //new GroupMember
                        //{
                        //    Id = 12,
                        //    GroupId = 3,
                        //    AccountId = 3,
                        //    State = GroupMemberState.Inviting,
                        //    //InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                        //},
                        //new GroupMember
                        //{
                        //    Id = 13,
                        //    GroupId = 3,
                        //    AccountId = 4,
                        //    State = GroupMemberState.Requesting,
                        //    //RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha"
                        //},
                        #endregion
                        #region member group 4
                        new GroupMember
                        {
                            //Id = 14,
                            Id = 8,
                            GroupId = 4,
                            AccountId = 2,
                            State = GroupMemberState.Leader
                        },
                        // new GroupMember
                        //{
                        //    Id = 15,
                        //    GroupId = 4,
                        //    AccountId = 3,
                        //    State = GroupMemberState.Inviting,
                        //    //InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                        //},
                        //new GroupMember
                        //{
                        //    Id = 16,
                        //    GroupId = 4,
                        //    AccountId = 4,
                        //    State = GroupMemberState.Requesting,
                        //    //RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha"
                        //},
                        #endregion
                        #region member group 5
                        new GroupMember
                        {
                            //Id = 17,
                            Id = 9,
                            GroupId = 5,
                            AccountId = 3,
                            State = GroupMemberState.Leader
                        },
                        // new GroupMember
                        //{
                        //    Id = 18,
                        //    GroupId = 5,
                        //    AccountId = 3,
                        //    State = GroupMemberState.Inviting,
                        //    //InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                        //},
                        //new GroupMember
                        //{
                        //    Id = 19,
                        //    GroupId = 5,
                        //    AccountId = 3,
                        //    State = GroupMemberState.Requesting,
                        //    //RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha"
                        //},
                        #endregion
                        #region member group 6
                        new GroupMember
                        {
                            //Id = 20,
                            Id = 10,
                            GroupId = 6,
                            AccountId = 3,
                            State = GroupMemberState.Leader
                        }
                        // new GroupMember
                        //{
                        //    Id = 21,
                        //    GroupId = 6,
                        //    AccountId = 2,
                        //    State = GroupMemberState.Inviting,
                        //    //InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                        //},
                        //new GroupMember
                        //{
                        //    Id = 22,
                        //    GroupId = 6,
                        //    AccountId = 1,
                        //    State = GroupMemberState.Requesting,
                        //    //RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha"
                        //},
                        #endregion
                    };
                    context.GroupMembers.AddRange(groupMembers);
                }
                #endregion

                #region seed group subject
                if (!context.GroupSubjects.Any())
                {
                    var groupSubjects = new GroupSubject[]
                    {
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
                    };
                    context.GroupSubjects.AddRange(groupSubjects);
                }
                #endregion

                #region seed invite
                if (!context.Invites.Any())
                {
                    JoinInvite[] list =
                    {
                        #region Group 1
                        new JoinInvite {
                            Id = 1,
                            GroupId= 1,
                            AccountId=2,
                            State = InviteRequestStateEnum.Approved
                        },
                        new JoinInvite
                        {
                            Id = 2,
                            GroupId = 1,
                            AccountId = 3,
                            State = InviteRequestStateEnum.Waiting,
                        },
                        #endregion

                        #region Group 2
                         new JoinInvite
                        {
                            Id = 3,
                            GroupId = 2,
                            AccountId = 2,
                            State = InviteRequestStateEnum.Approved,
                        },
                        new JoinInvite
                        {
                            Id = 4,
                            GroupId = 2,
                            AccountId = 3,
                            State = InviteRequestStateEnum.Waiting,
                        },
                        #endregion 

                        #region Group 3
                          new JoinInvite
                        {
                            Id = 5,
                            GroupId = 3,
                            AccountId = 1,
                            State = InviteRequestStateEnum.Approved,
                        },
                        new JoinInvite
                        {
                            Id = 6,
                            GroupId = 3,
                            AccountId = 3,
                            State = InviteRequestStateEnum.Waiting,
                        },
                        #endregion
                        
                        #region Group 4
                        new JoinInvite
                        {
                            Id = 7,
                            GroupId = 4,
                            AccountId = 3,
                            State = InviteRequestStateEnum.Waiting,
                        },
                        #endregion
                        
                        #region Group 5
                        new JoinInvite
                        {
                            Id = 8,
                            GroupId = 5,
                            AccountId = 3,
                            State = InviteRequestStateEnum.Waiting,
                        },
                        #endregion
                        
                        #region Group 6
                        new JoinInvite
                        {
                            Id = 9,
                            GroupId = 6,
                            AccountId = 2,
                            State = InviteRequestStateEnum.Waiting,
                        },
                        #endregion

                    };
                    context.Invites.AddRange(list);
                }
                #endregion

                #region seed request
                if (!context.Requests.Any())
                {
                    JoinRequest[] list =
                    {
                        
                        #region Group 1
                        new JoinRequest
                        {
                            Id = 1,
                            GroupId = 1,
                            AccountId = 4,
                            State = InviteRequestStateEnum.Waiting,
                        },
                        #endregion
                        
                        #region Group 2
                        new JoinRequest
                        {
                            Id = 2,
                            GroupId = 2,
                            AccountId = 3,
                            State = InviteRequestStateEnum.Waiting,
                        },
                        #endregion
                        
                        #region Group 3
                         new JoinRequest
                        {
                            Id = 3,
                            GroupId = 3,
                            AccountId = 4,
                            State = InviteRequestStateEnum.Waiting,
                        },
                        #endregion
                        
                        #region Group 4
                        new JoinRequest
                        {
                            Id = 4,
                            GroupId = 4,
                            AccountId = 4,
                            State = InviteRequestStateEnum.Waiting,
                        },
                        #endregion
                        
                        #region Group 5
                        new JoinRequest
                        {
                            Id = 5,
                            GroupId = 5,
                            AccountId = 3,
                            State = InviteRequestStateEnum.Waiting,
                        },
                        #endregion
                        
                        #region Group 6
                        new JoinRequest
                        {
                            Id = 6,
                            GroupId = 6,
                            AccountId = 1,
                            State = InviteRequestStateEnum.Waiting,
                        },
                        #endregion
                    };
                    context.Requests.AddRange(list);
                }
                #endregion

                #region seed meeting
                if (!context.Meetings.Any())
                {
                    var meetings = new Meeting[]
                    {
                        #region meeting for group 1
                        //Forgoten meeting
                        new Meeting
                        {
                            Id = 1,
                            GroupId=1,
                            Name="Forgoten past history",
                            ScheduleStart = DateTime.Now.AddDays(-3),
                            ScheduleEnd = DateTime.Now.AddDays(-3).AddHours(1),
                        },
                        //Ended schedule meeting
                        new Meeting
                        {
                            Id = 2,
                            GroupId=1,
                            Name="Ended schedule past history",
                            ScheduleStart = DateTime.Now.AddDays(-2).AddMinutes(15),
                            ScheduleEnd = DateTime.Now.AddDays(-2).AddHours(1),
                            Start = DateTime.Now.AddDays(-2).AddMinutes(30),
                            End = DateTime.Now.AddDays(-2).AddHours(2),
                        },
                        //Ended instant meeting
                        new Meeting
                        {
                            Id = 3,
                            GroupId=1,
                            Name="Ended instant past history",
                            Start = DateTime.Now.AddDays(-2).AddMinutes(30),
                            End = DateTime.Now.AddDays(-2).AddHours(2),
                        },
                        //Live schedule meeting
                        new Meeting
                        {
                            Id = 4,
                            GroupId=1,
                            Name="Live schedule meeting",
                            ScheduleStart = DateTime.Now.AddMinutes(15),
                            ScheduleEnd = DateTime.Now.AddHours(1),
                            Start = DateTime.Now.AddMinutes(30),
                        },
                        //Live Instant meeting
                        new Meeting
                        {
                            Id = 5,
                            GroupId=1,
                            Name="Live Instant meeting",
                            Start = DateTime.Now.AddMinutes(-30),
                        },
                        //Future Schedule meeting
                        new Meeting
                        {
                            Id = 6,
                            GroupId=1,
                            Name="Today late schedule meeting",
                            ScheduleStart = DateTime.Now.AddMinutes(-15),
                            ScheduleEnd = DateTime.Now.AddHours(1),
                        },
                        new Meeting
                        {
                            Id = 7,
                            GroupId=1,
                            Name="Today early schedule meeting",
                            ScheduleStart = DateTime.Now.AddMinutes(15),
                            ScheduleEnd = DateTime.Now.AddHours(1),
                        },
                        new Meeting
                        {
                            Id = 8,
                            GroupId=1,
                            Name="Future schedule meeting",
                            ScheduleStart = DateTime.Now.AddDays(1).AddMinutes(15),
                            ScheduleEnd = DateTime.Now.AddDays(1).AddHours(1),
                        },
                        #endregion
                    };
                    context.Meetings.AddRange(meetings);
                }
                #endregion
                context.SaveChanges();
            }
        }
    }
}
