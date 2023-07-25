using Microsoft.Extensions.DependencyInjection;
using Quartz;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.ServiceExtensions.Scheduler.Lib;

namespace APIExtension.Scheduler.Jobs
{
    //0 0 ** Wed    
    public class WeeklyJob
    {
        public static readonly string schedule = CronScheduleExpression.Weekly;
        private readonly IAutoMailService mailService;


        public WeeklyJob(IServiceProvider service, IAutoMailService mailService)
        {
            var scope = service.CreateScope();
            //mailService = scope.ServiceProvider.GetRequiredService<IServiceWrapper>().Mails;
            this.mailService = scope.ServiceProvider.GetRequiredService<IServiceWrapper>().Mails;
            //this.mailService = mailService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await mailService.SendEmailWithDefaultTemplateAsync(
                new[] { "trankhaiminhkhoi@gmail.com", "trankhaiminhkhoi10a3@gmail.com" },
                "Test Weekly Job Scheduler",
                $"Email sent at {DateTime.Now.ToString("dd/MM/yy hh.mm.ss")}",
                null
            );
            await mailService.SendEmailWithDefaultTemplateAsync(new List<string>() { "trankhaiminhkhoi10a3@gmail.com" }, "Test scheduler", "GS scheduler is working", null);
            Console.WriteLine($"Daily Task completed at {DateTime.Now.ToString("dd/MM/yy hh.mm.ss")}");
            //return Task.CompletedTask;
        }
    }
}
