using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIExtension.Validator
{
    public class BaseValidator
    {
        public BaseValidator()
        {
            validatorResult = new ValidatorResult();
        }

        protected ValidatorResult validatorResult { get; set; }
    }
}
