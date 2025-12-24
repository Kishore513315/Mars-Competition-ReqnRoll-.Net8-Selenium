using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using MarsCompetitionReqnroll.Net8Selenium.Drivers;
using MarsCompetitionReqnroll.Net8Selenium.Utilities;
using Reqnroll;
using System;

namespace MarsCompetitionReqnroll.Net8Selenium.Support
{
    [Binding]
    public class Hooks
    {
        private static ExtentReports _extent = ExtentManager.GetExtent();
        private static ExtentTest? _feature;
        private static ExtentTest? _scenario;
        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _extent = ExtentManager.GetExtent();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext feature)
        {
            if (_extent != null)
            {
                _feature = _extent.CreateTest<Feature>(feature.FeatureInfo.Title);
            }
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            if (_feature == null)
            {
                _feature = _extent.CreateTest<AventStack.ExtentReports.Gherkin.Model.Feature>(
                    FeatureContext.Current.FeatureInfo.Title
                );
            }

            _scenario = _feature.CreateNode<AventStack.ExtentReports.Gherkin.Model.Scenario>(
                _scenarioContext.ScenarioInfo.Title
            );


            Driver.InitializeDriver();
        }

        [AfterStep]
        public void AfterStep()
        {
            var stepInfo = _scenarioContext.StepContext.StepInfo;

            if (_scenarioContext.TestError == null)
            {
                _scenario!.CreateNode(stepInfo.StepDefinitionType.ToString(), stepInfo.Text);
            }
            else
            {
                string screenshotPath = Driver.TakeScreenshot(_scenarioContext.ScenarioInfo.Title);
                _scenario!.CreateNode(stepInfo.StepDefinitionType.ToString(), stepInfo.Text)
                          .Fail(_scenarioContext.TestError.Message)
                          .AddScreenCaptureFromPath(screenshotPath);
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Driver.QuitDriver();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            try
            {
                _extent.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine("Extent flush failed: " + e);
            }
        }
    }
}





