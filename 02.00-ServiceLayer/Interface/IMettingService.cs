﻿using DataLayer.DBObject;
using ShareResource.DTO;

namespace ServiceLayer.Interface
{
    public interface IMettingService
    {
        public Task<bool> AnyAsync(int id);
        public IQueryable<PastMeetingGetDto> GetPastMeetingsForGroup(int groupId);
        public IQueryable<ScheduleMeetingGetDto> GetScheduleMeetingsForGroup(int groupId);
        public IQueryable<LiveMeetingGetDto> GetLiveMeetingsForGroup(int groupId);
        public Task<Meeting> GetByIdAsync(int id);
        public Task CreateScheduleMeetingAsync(ScheduleMeetingCreateDto dto);
        public Task CreateInstantMeetingAsync(InstantMeetingCreateDto dto);
        public Task UpdateScheduleMeetingAsync(ScheduleMeetingUpdateDto dto);
    }
}