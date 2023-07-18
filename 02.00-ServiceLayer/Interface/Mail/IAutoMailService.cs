using Microsoft.AspNetCore.Http;
using ServiceLayer.ClassImplement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IAutoMailService
    {
        public Task<bool> SendEmailWithDefaultTemplateAsync(IEnumerable<string> receivers, string subject, string content,
       IFormFileCollection attachments);

        //public Task<bool> SendPaymentReminderAsync();

        #region unsued code

        public Task<bool> SendEmailWithDefaultTemplateAsync(MailMessageEntity mail);
        bool SendSimpleMail(MailMessageEntity message);
        bool SendSimpleMail(IEnumerable<string> receivers, string subject, string content, IFormFileCollection attachments);
        Task<bool> SendSimpleEmailAsync(MailMessageEntity message);
        Task<bool> SendSimpleMailAsync(IEnumerable<string> receivers, string subject, string content, IFormFileCollection attachments);

        #endregion
    }
}
