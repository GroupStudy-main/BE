using ServiceLayer.Interface;
using ShareResource.DTO;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace APIExtension.Validator
{
    public interface IAccountValidator
    {
        Task<ValidatorResult> ValidateParams(AccountUpdateDto dto);
    }
    public class AccountValidator : BaseValidator, IAccountValidator
    {
        private IServiceWrapper services;

        public AccountValidator(IServiceWrapper services)
        {
            this.services = services;
        }
        //^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$
        //^[0-9]{8,20}$
        Regex phoneRegex = new Regex(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$");


        public async Task<ValidatorResult> ValidateParams(AccountUpdateDto dto)
        {
            try
            {
                //Nếu null thì ko update
                if (dto.FullName != null && dto.FullName.Trim().Length == 0)
                {
                    validatorResult.Failures.Add("Thiếu họ tên");
                }
                if (dto.FullName != null && dto.FullName.Trim().Length > 50)
                {
                    validatorResult.Failures.Add("Tên meeting quá dài");
                }
                if (dto.Phone != null && dto.Phone.Trim().Length == 0)
                {
                    validatorResult.Failures.Add("Thiếu số điện thoại");
                }
                if (dto.Phone != null && !phoneRegex.IsMatch(dto.Phone))
                {
                    validatorResult.Failures.Add("Số điện thoại không đúng định dạng");
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