using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Quartz;

namespace DriveSpaceSlackBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var slackNotificationService = new SlackNotificationService();

            HostFactory.Run(c =>
            {
                c.ScheduleQuartzJobAsService(q =>
                        q.WithJob(() =>
                            JobBuilder.Create<DriveSpaceMonitoringService>().Build())
                        .AddTrigger(() =>
                            TriggerBuilder.Create()
                                .WithSimpleSchedule(builder => builder
                                    .WithIntervalInSeconds(10)
                                    .RepeatForever())
                                .Build())
                );
            });

            //HostFactory.Run(x =>
            //{
            //    x.Service<MyService>(s =>
            //    {
            //        s.WhenStarted(service => service.OnStart());
            //        s.WhenStopped(service => service.OnStop());
            //        s.ConstructUsing(() => new MyService());

            //        s.ScheduleQuartzJob(q =>
            //            q.WithJob(() =>
            //                JobBuilder.Create<DriveSpaceMonitoringService>().Build())
            //                .AddTrigger(() => TriggerBuilder.Create()
            //                    .WithSimpleSchedule(b => b
            //                        .WithIntervalInSeconds(10)
            //                        .RepeatForever())
            //                    .Build()));
            //    });

            //    x.RunAsLocalSystem()
            //        .DependsOnEventLog()
            //        .StartAutomatically()
            //        .EnableServiceRecovery(rc => rc.RestartService(1));

            //    x.SetServiceName("My Topshelf Service");
            //    x.SetDisplayName("My Topshelf Service");
            //    x.SetDescription("My Topshelf Service's description");
            //});
        }
    }

    //static void Main(string[] args)
    //{
    //    var slackNotificationService = new SlackNotificationService();

    //    HostFactory.Run(
    //    configuration =>
    //    {
    //        configuration.Service<DriveSpaceMonitoringService>(
    //            service =>
    //            {
    //                service.WhenStarted(s => service.OnStart());
    //                service.WhenStopped(s => service.OnStop());
    //                service.ConstructUsing(() => new DriveSpaceMonitoringService(slackNotificationService));

    //                service.ScheduleQuartzJob(q =>
    //                    q.WithJob(() =>
    //                        JobBuilder.Create<DriveSpaceMonitoringService>().Build())
    //                        .AddTrigger(() => TriggerBuilder.Create()
    //                            .WithSimpleSchedule(b => b
    //                                .WithIntervalInSeconds(10)
    //                                .RepeatForever())
    //                            .Build()));
    //                                    });

    //        configuration.RunAsLocalSystem();

    //        configuration.SetServiceName("ASimpleService");
    //        configuration.SetDisplayName("A Simple Service");
    //        configuration.SetDescription("Don't Code Tired Demo");
    //    });
    //}

    public class MyService
    {
        public void OnStart()
        {
        }

        public void OnStop()
        {
        }
    }

    public class MyJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"[{DateTime.UtcNow}] Welcome from MyJob!");
        }
    }
}
