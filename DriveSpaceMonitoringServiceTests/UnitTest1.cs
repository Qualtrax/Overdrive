using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DriveSpaceSlackBot;

namespace DriveSpaceMonitoringServiceTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var service = new DriveSpaceMonitoringService(new SlackNotificationService());

            service.Start();
        }
    }
}
