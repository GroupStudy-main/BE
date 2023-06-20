﻿using AutoMapper;
using DataLayer.DBContext;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.ClassImplement
{
    public class RepoWrapper : IRepoWrapper
    {
        private readonly IMapper mapper;
        private readonly GroupStudyContext dbContext;

        public RepoWrapper(GroupStudyContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            users = new AccountRepo(dbContext, mapper);
            meeting = new MeetingRepository(dbContext, mapper);
            groupMembers = new GroupMemberReposity(dbContext);
            classes = new ClassRepository(dbContext);
            subjects = new SubjectRepository(dbContext);
            connections = new ConnectionRepository(dbContext);
        }

        private IAccountRepo users;
        public IAccountRepo Accounts { 
            get 
            { 
                if(users is null)
                {
                    users = new AccountRepo(dbContext, mapper);
                }
                return users; 
            } 
        }

        private IMeetingRoomRepository meetingRooms;
        public IMeetingRoomRepository Rooms
        {
            get
            {
                if (meetingRooms is null)
                {
                    meetingRooms = new MeetingRoomRepository(dbContext, mapper);
                }
                return meetingRooms;
            }
        }

        private IMeetingRepository meeting;

        public IMeetingRepository Meetings
        {
            get
            {
                if (meeting is null)
                {
                    meeting = new MeetingRepository(dbContext);
                }
                return meeting;
            }
        }

        private IGroupRepository groups;
        public IGroupRepository Groups
        {
            get
            {
                if (groups is null)
                {
                    groups = new GroupRepository(dbContext);
                }
                return groups;
            }
        }

        private IMeetingRepository meetings;
        public IMeetingRepository Meeting
        {
            get
            {
                if (meetings is null)
                {
                    meetings = new MeetingRepository(dbContext);
                }
                return meetings;
            }
        }
        
        private IDocumentFileRepository documents;

        public IDocumentFileRepository DocumentFiles
        {
            get
            {
                if (documents is null)
                {
                    documents = new DocumentFileRepository(dbContext);
                }
                return documents;
            }
            
        }
    }
}
