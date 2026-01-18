using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using MarsCompetitionReqnroll.Net8Selenium.Drivers;
using MarsCompetitionReqnroll.Net8Selenium.Pages;
using MarsCompetitionReqnroll.Net8Selenium.Utilities;
using Newtonsoft.Json.Linq;
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
        private readonly FeatureContext _featureContext;

        public Hooks(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _extent = ExtentManager.GetExtent();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext feature)
        {
            _feature = _extent.CreateTest<Feature>(feature.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _scenario = _feature!.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);

            Driver.InitializeDriver();

            var scenarioTags = _scenarioContext.ScenarioInfo.Tags;
            var featureTags = _featureContext.FeatureInfo.Tags;

            bool hasLoginTag =
                scenarioTags.Contains("login") ||
                featureTags.Contains("login");

            if (hasLoginTag)
            {
                PerformLogin();
            }
        }

        private void PerformLogin()
        {
            var driver = Driver.GetDriver();
            var loginPage = new LoginPage(driver);

            JObject data = JsonReader.GetData("TestData/SignInData.json");
            var user = data["validUser"]
                       ?? throw new Exception("validUser not found in SignInData.json");

            loginPage.GoToLoginPage();
            loginPage.EnterCredentials(
                user["Email"]!.ToString(),
                user["Password"]!.ToString()
            );
            loginPage.ClickLogin();

            if (!loginPage.IsDashboardVisible())
                throw new Exception("Login failed — dashboard not visible.");
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
            _extent.Flush();
        }
    }
}