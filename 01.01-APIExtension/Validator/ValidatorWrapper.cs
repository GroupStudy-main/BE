using DataLayer.DBObject;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIExtension.Validator
{
    public interface IValidatorWrapper
    {
        IMeetingValidator Meetings { get; }
        IGroupMemberValidator GroupMembers { get; }
    }
    public class ValidatorWrapper : IValidatorWrapper
    {
        private IServiceWrapper services;

        public ValidatorWrapper(IServiceWrapper services)
        {
            this.services = services;
            meetings = new MeetingValidator(services);
        }

        private IMeetingValidator meetings;

        public IMeetingValidator Meetings
        {
            get
            {
                if (meetings == null)
                {
                    meetings = new MeetingValidator(services);
                }
                return meetings;
            }
        }

        private IGroupMemberValidator groupMembers;
        public IGroupMemberValidator GroupMembers
        {
            get
            {
                if (groupMembers == null)
                {
                    groupMembers = new GroupMemberValidator(services);
                }
                return groupMembers;
            }
        }
    }
}
