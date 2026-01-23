using MarsCompetitionReqnroll.Net8Selenium.Drivers;
using MarsCompetitionReqnroll.Net8Selenium.Pages;
using MarsCompetitionReqnroll.Net8Selenium.Utilities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using System;

namespace MarsCompetitionReqnroll.Net8Selenium.StepDefinitions
{
    [Binding]
    public class SignInFeatureStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly LoginPage _loginPage;
        private readonly JObject _data;

        public SignInFeatureStepDefinitions()
        {
            _driver = Driver.GetDriver();
            _loginPage = new LoginPage(_driver);
            _data = JsonReader.GetData("TestData/SignInData.json");
        }

        [Given(@"I navigate to the Mars portal login page")]
        public void GivenINavigateToTheMarsPortalLoginPage()
        {
            _loginPage.GoToLoginPage();
        }

        [When(@"I enter valid username and password")]
        public void WhenIEnterValidUsernameAndPassword()
        {
            var user = _data["validUser"]
                       ?? throw new Exception("validUser not found in SignInData.json");

            _loginPage.EnterCredentials(
                user["Email"]!.ToString(),
                user["Password"]!.ToString()
            );
        }

        [When(@"I click on the login button")]
        public void WhenIClickOnTheLoginButton()
        {
            _loginPage.ClickLogin();
        }

        [Then(@"I should be redirected to the dashboard")]
        public void ThenIShouldBeRedirectedToTheDashboard()
        {
            Assert.That(_loginPage.IsDashboardVisible(), Is.True);
        }

        [Then(@"the user name should be displayed on the top right corner")]
        public void ThenTheUserNameShouldBeDisplayedOnTheTopRightCorner()
        {
            Assert.That(_loginPage.IsUserNameDisplayed(), Is.True);
        }
    }
}
