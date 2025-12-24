using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;

namespace MarsCompetitionReqnroll.Net8Selenium.Utilities
{
    public static class ExtentManager
    {
        private static ExtentReports? _extent;

        public static ExtentReports GetExtent()
        {
            if (_extent == null)
            {
                string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
                Directory.CreateDirectory(folder);

                string reportPath = Path.Combine(folder, "TestReport.html");

                var spark = new ExtentSparkReporter(reportPath);
                spark.Config.DocumentTitle = "Automation Test Report";
                spark.Config.ReportName = "Mars Competition Tests";

                _extent = new ExtentReports();
                _extent.AttachReporter(spark);
            }

            return _extent;
        }
    }
}








