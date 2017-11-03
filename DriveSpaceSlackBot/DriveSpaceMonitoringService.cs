using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveSpaceSlackBot
{
    public class DriveSpaceMonitoringService : IJob
    {
        private const Int64 BytesInGigabyte = 1073741824;

        private SlackNotificationService slackNotificationService;

        public DriveSpaceMonitoringService()
        {
            Console.WriteLine(String.Format("[{0}] Drive Monitoring Job started", DateTime.Now));
            slackNotificationService = new SlackNotificationService();
        }

        public DriveSpaceMonitoringService(SlackNotificationService slackNotificationService)
        {
            this.slackNotificationService = slackNotificationService;
        }

        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine(String.Format("[{0}] Drive Monitoring Job started", DateTime.Now));
            Start();
        }

        public void Start()
        {
            var drives = System.IO.DriveInfo.GetDrives();
            var spaceThreshold = 5368709120;

            foreach (var drive in drives)
                //if (drive.AvailableFreeSpace < spaceThreshold)
                    SendNotification(drive);
        }

        private void SendNotification(DriveInfo drive)
        {
            var message = String.Format("<!channel> Drive {0} on server {1} only has {2} GB of space remaining.",
                drive.Name, Environment.MachineName, Convert.ToDouble(drive.AvailableFreeSpace / BytesInGigabyte));

            slackNotificationService.Send(message);
        }

        public void Stop()
        {
        }
    }
}
