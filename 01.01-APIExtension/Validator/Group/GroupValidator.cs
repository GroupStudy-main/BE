using ServiceLayer.Interface;

namespace APIExtension.Validator
{
    public interface IGroupValidator
    {

    }
    public class GroupValidator : BaseValidator, IGroupValidator
    {
        private IServiceWrapper services;

        public GroupValidator(IServiceWrapper services)
        {
            this.services = services;
        }
    }
}