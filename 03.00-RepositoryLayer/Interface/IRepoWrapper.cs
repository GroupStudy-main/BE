﻿using DataLayer.DBObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RepositoryLayer.Interface
{
    public interface IRepoWrapper
    {
        public IAccountRepo Accounts { get; }
        public IMeetingRepository Meetings { get; }
        public IGroupRepository Groups { get; }
        public IGroupMemberReposity GroupMembers { get; }
        public IClassReposity Classes { get; }
        public ISubjectRepository Subjects { get; }
        public IConnectionRepository Connections { get; }
    }
}
