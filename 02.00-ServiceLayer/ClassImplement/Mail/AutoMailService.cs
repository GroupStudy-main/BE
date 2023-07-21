using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Interface;
using ServiceLayer.Interface;
using MimeKit;
using ContentType = MimeKit.ContentType;
using MimeKit.Utils;
using Microsoft.Extensions.Configuration;

namespace ServiceLayer.ClassImplement
{
    public class AutoMailService : IAutoMailService
    {
        /// <summary>
        ///     The default Email Template. {0}: email content
        /// </summary>
        private const string DefaultTemplate = "<h2 style='color:red;'>{0}</h2>";

        private readonly MailConfiguration _emailConfig;

        private readonly IRepoWrapper repositories;

        //private readonly IWebHostEnvironment env;
        private readonly string rootPath;

        private readonly string logoPath;

        public AutoMailService(IWebHostEnvironment env,  IRepoWrapper repositories, IConfiguration configuration)
        {
            _emailConfig = configuration
                .GetSection("EmailConfiguration")
                .Get<MailConfiguration>(); 
            //this.env = env;
            rootPath = env.WebRootPath;
            this.repositories = repositories;
            logoPath = rootPath + Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar +
                           //"logowhite.png";
                           "Logo.png";
        }

        public async Task<bool> SendEmailWithDefaultTemplateAsync(IEnumerable<string> receivers, string subject,
            string content, IFormFileCollection attachments)
        {
            var message = new MailMessageEntity(receivers, subject, content, attachments);
            var mimeMessages = CreateMimeMessageWithSimpleTemplateList(message /*, rootPath*/);

            foreach (var mimeMessage in mimeMessages) await SendAsync(mimeMessage);
            return true;
        }


        public async Task<bool> SendEmailWithDefaultTemplateAsync(MailMessageEntity message)
        {
            var mimeMessages = CreateMimeMessageWithSimpleTemplateList(message/*, rootPath*/);

            foreach (var mimeMessage in mimeMessages) await SendAsync(mimeMessage);
            return true;
        }


        public async Task<bool> SendSimpleEmailAsync(MailMessageEntity message)
        {
            MimeMessage mailMessage = CreateSimpleEmailMessage(message);

            await SendAsync(mailMessage);
            return true;
        }

        public async Task<bool> SendSimpleMailAsync(IEnumerable<string> receivers, string subject, string content, IFormFileCollection attachments)
        {
            MailMessageEntity message = new MailMessageEntity(receivers, subject, content, attachments);
            MimeMessage mailMessage = CreateSimpleEmailMessage(message);

            await SendAsync(mailMessage);
            return true;
        }

        private List<MimeMessage> CreateMimeMessageWithSimpleTemplateList(MailMessageEntity message)
        {
            var list = new List<MimeMessage>();
            //var templatePath = rootPath + Path.DirectorySeparatorChar + MailTemplateHelper.FOLDER +
            //                   Path.DirectorySeparatorChar + MailTemplateHelper.DEFAULT_TEMPLATE_FILE;
            var template = MailTemplateHelper.DEFAULT_TEMPLATE(rootPath);
            
            foreach (var receiver in message.Receivers)
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(_emailConfig.From));
                mimeMessage.To.Add(receiver);
                mimeMessage.Subject = message.Subject;


                var bodyBuilder = new BodyBuilder();
                try                                    
                {
                    var email = receiver.Address;
                    //<!--{0} is logo-->
                    //<!--{1} is username-->
                    //<!--{2} is content-->
                    //bodyBuilder = new BodyBuilder { HtmlBody = string.Format(template, logoPath, email, message.Content) };
                    var logo = bodyBuilder.LinkedResources.Add(logoPath);
                    logo.ContentId = MimeUtils.GenerateMessageId();
                    bodyBuilder.HtmlBody = FormatTemplate(template, logo.ContentId, email, message.Content);
                }
                catch
                {
                    //bodyBuilder = new BodyBuilder { HtmlBody = string.Format(DefaultTemplate, message.Content) };
                    bodyBuilder = new BodyBuilder { HtmlBody = FormatTemplate(DefaultTemplate, message.Content) };
                }

                if (message.Attachments != null && message.Attachments.Any())
                {
                    byte[] fileBytes;
                    foreach (var attachment in message.Attachments)
                    {
                        using (var ms = new MemoryStream())
                        {
                            attachment.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }

                        bodyBuilder.Attachments.Add(attachment.FileName, fileBytes,
                            ContentType.Parse(attachment.ContentType));
                    }
                }

                mimeMessage.Body = bodyBuilder.ToMessageBody();
                list.Add(mimeMessage);
            }

            return list;
        }

        /// <summary>
        ///     Replacing {number} marker in the email template with string values
        /// </summary>
        /// <param name="template">Email template</param>
        /// <param name="values">String values</param>
        /// <returns></returns>
        private string FormatTemplate(string template, string logoContentId, params string[] values)
        {
            template = template.Replace("{logo}", logoContentId);
            for (var i = 0; i < values.Length; i++)
                // {{{i}}}: {0}, {1} in string
                template = template.Replace($"{{{i}}}", values[i]);
            return template;
        }


        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.AppPassword);

                    await client.SendAsync(mailMessage);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }

        #region Unused Code


        private MimeMessage CreateSimpleEmailMessage(MailMessageEntity message)
        {
            MimeMessage emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.Receivers);
            emailMessage.Subject = message.Subject;

            //var bodyBuilder = new BodyBuilder { HtmlBody = string.Format(DefaultTemplate, message.Content) };
            var bodyBuilder = new BodyBuilder { HtmlBody = FormatTemplate(DefaultTemplate, message.Content) };

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }
        #endregion
    }
}
