using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace MarsCompetitionReqnroll.Net8Selenium.Drivers
{
    public static class Driver
    {
        private static IWebDriver? _driver;

        public static IWebDriver GetDriver()
        {
            if (_driver == null)
                throw new Exception("WebDriver not initialized! Did BeforeScenario run?");

            return _driver;
        }

        public static void InitializeDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");

            _driver = new ChromeDriver(options);
            _driver.Manage().Window.Maximize();
        }

        public static string TakeScreenshot(string scenarioName)
        {
            // ensure folder exists
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
            Directory.CreateDirectory(folderPath);

            string fileName = $"{SanitizeFileName(scenarioName)}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            string filePath = Path.Combine(folderPath, fileName);

            Screenshot screenshot = ((ITakesScreenshot)_driver!).GetScreenshot();
            screenshot.SaveAsFile(filePath);

            return filePath;
        }

        public static void QuitDriver()
        {
            try
            {
                _driver?.Quit();
            }
            catch
            {
            }
            finally
            {
                _driver = null;
            }
        }

        private static string SanitizeFileName(string name)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            return name;
        }
    }
}

