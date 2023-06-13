﻿using DataLayer.DBObject;
using ServiceLayer.Interface;
using ShareResource.DTO;

namespace APIExtension.Validator
{
    public interface IMeetingValidator
    {
        public Task<ValidatorResult> ValidateParams(ScheduleMeetingCreateDto? dto, int studentId);
        public Task<ValidatorResult> ValidateParams(InstantMeetingCreateDto? dto, int studentId);
        public Task<ValidatorResult> ValidateParams(ScheduleMeetingUpdateDto? dto, int studentId);
    }
    public class MeetingValidator : BaseValidator, IMeetingValidator
    {
        private IServiceWrapper services;

        public MeetingValidator(IServiceWrapper services)
        {
            this.services = services;
        }

        public async Task<ValidatorResult> ValidateParams(InstantMeetingCreateDto? dto, int studentId)
        {
            try
            {
                if (!await services.Groups.IsStudentLeadingGroupAsync(studentId, dto.GroupId))
                {
                    validatorResult.Failures.Add("Bạn không phải nhóm trưởng của nhóm này");
                }
                if (dto.Name.Trim().Length == 0)
                {
                    validatorResult.Failures.Add("Thiếu tên meeting");
                }
                if (dto.Name.Trim().Length > 50)
                {
                    validatorResult.Failures.Add("Tên meeting quá dài");
                }

            }
            catch (Exception ex)
            {
                validatorResult.Failures.Add(ex.Message);
            }
            return validatorResult;
        }

        public async Task<ValidatorResult> ValidateParams(ScheduleMeetingCreateDto? dto, int studentId)
        {
            try
            {
                if(!await services.Groups.IsStudentLeadingGroupAsync(studentId, dto.GroupId))
                {
                    validatorResult.Failures.Add("Bạn không phải nhóm trưởng của nhóm này");
                }
                if (dto.Name.Trim().Length == 0)
                {
                    validatorResult.Failures.Add("Thiếu tên meeting");
                }
                if (dto.Name.Trim().Length > 50)
                {
                    validatorResult.Failures.Add("Tên meeting quá dài");
                }
                if (dto.ScheduleStart < DateTime.Now)
                {
                    validatorResult.Failures.Add("Thời gian bắt đầu meeting không hợp lí");
                }
                if (dto.ScheduleEnd < dto.ScheduleStart)
                {
                    validatorResult.Failures.Add("Thời gian kết thúc meeting không hợp lí");
                }
                else if(dto.ScheduleStart.Date!=dto.ScheduleEnd.Date)
                {
                    validatorResult.Failures.Add("Cuộc họp phải diễn ra và kết thúc trong 1 ngày");
                }

            }
            catch(Exception ex)
            {
                validatorResult.Failures.Add(ex.Message);
            }
            return validatorResult; 
        }

        public async Task<ValidatorResult> ValidateParams(ScheduleMeetingUpdateDto? dto, int studentId)
        {
            try
            {
                Meeting meeting = await services.Meetings.GetByIdAsync(dto.Id);
                if (meeting==null)
                {
                    validatorResult.Failures.Add("Meeting không tồn tại");
                }
                else if (!await services.Groups.IsStudentLeadingGroupAsync(studentId, meeting.GroupId))
                {
                    validatorResult.Failures.Add("Bạn không phải nhóm trưởng của nhóm này");
                }
                if(dto.Name.Trim().Length == 0)
                {
                    validatorResult.Failures.Add("Thiếu tên meeting");
                }
                if(dto.Name.Trim().Length > 50) 
                { 
                    validatorResult.Failures.Add("Tên meeting quá dài");
                }
                if (dto.ScheduleStart < DateTime.Now)
                {
                    validatorResult.Failures.Add("Thời gian bắt đầu meeting không hợp lí");
                }
                if (dto.ScheduleEnd < dto.ScheduleStart)
                {
                    validatorResult.Failures.Add("Thời gian kết thúc meeting không hợp lí");
                }
                else
                {
                    validatorResult.Failures.Add("Thời gian kết thúc meeting không hợp lí");
                }
            }
            catch (Exception ex)
            {
                validatorResult.Failures.Add(ex.Message);
            }
            return validatorResult;
        }
    }
}