using MarsCompetitionReqnroll.Net8Selenium.Drivers;
using MarsCompetitionReqnroll.Net8Selenium.Models;
using MarsCompetitionReqnroll.Net8Selenium.Pages;
using MarsCompetitionReqnroll.Net8Selenium.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using System.Data;

namespace MarsCompetitionReqnroll.Net8Selenium.StepDefinitions
{
    [Binding]
    public class CertificationManagementStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly CertificationPage _certificationPage;
        private CertificationTestData _currentTestData;

        public CertificationManagementStepDefinitions()
        {
            _driver = Driver.GetDriver();
            _certificationPage = new CertificationPage(_driver);
        }

        [Then("the user navigates to the Profile page")]
        public void GivenTheUserNavigatesToTheProfilePage()
        {
            _certificationPage.NavigateToProfile();
        }

        [Then("the user opens the Certifications section")]
        public void GivenTheUserOpensTheCertificationsSection()
        {
            _certificationPage.OpenCertificationsTab();
        }

        [Given("the certification record exists using test data {string}")]
        public void GivenCertificationRecordExists(string key)
        {
            var data = CertificationJsonDataReader.GetCertificationData(key);

            if (!_certificationPage.IsCertificationPresent(data.Certificate))
            {
                _certificationPage.AddCertification(data.Certificate, data.From, data.Year);
            }
        }


        [When("the user creates a certification using test data {string}")]
        public void WhenTheUserCreatesACertificationUsingTestData(string testDataKey)
        {
            _currentTestData = CertificationJsonDataReader.GetCertificationData(testDataKey);

            _certificationPage.AddCertification(
                _currentTestData.Certificate,
                _currentTestData.From,
                _currentTestData.Year
            );
        }

        [When("the user updates the certification name using test data {string}")]
        public void WhenTheUserUpdatesTheCertificationNameUsingTestData(string testDataKey)
        {
            _currentTestData = CertificationJsonDataReader.GetCertificationData(testDataKey);

            // existing name comes from TC_01 data
            var existingData = CertificationJsonDataReader.GetCertificationData("TC_01");

            _certificationPage.UpdateCertification(existingData.Certificate, _currentTestData.Certificate);
        }


        [When("the user deletes the certification using test data {string}")]
        public void WhenTheUserDeletesTheCertificationUsingTestData(string testDataKey)
        {
            _currentTestData = CertificationJsonDataReader.GetCertificationData(testDataKey);
            _certificationPage.DeleteCertification(_currentTestData.Certificate);
        }

        [When("the user attempts to create a certification using test data {string}")]
        public void WhenTheUserAttemptsToCreateACertificationUsingTestData(string testDataKey)
        {
            _currentTestData = CertificationJsonDataReader.GetCertificationData(testDataKey);

            _certificationPage.AddCertification(
                _currentTestData.Certificate,
                _currentTestData.From,
                _currentTestData.Year
            );
        }

        [When("the user deletes all certifications created during this test run")]
        public void WhenTheUserDeletesAllCertificationsCreatedDuringThisTestRun()
        {
            _certificationPage.DeleteAllCertifications();
        }

        [Then("a success notification should be displayed for certification {string} with action {string}")]
        public void ThenASuccessNotificationShouldBeDisplayedForCertificationWithAction(string testDataKey, string action)
        {
            var data = CertificationJsonDataReader.GetCertificationData(testDataKey);

            string expected = action == "deleted"
                ? $"{data.Certificate} has been deleted from your certification"
                : $"{data.Certificate} has been {action} to your certification";

            string actual = ToastMessageReader.GetToastMessage(_driver);

            Assert.That(actual, Does.Contain(expected));
        }


        [Then("an error notification should be displayed with message key {string}")]
        public void ThenAnErrorNotificationShouldBeDisplayedWithMessageKey(string messageKey)
        {
            string expected = CertificationJsonDataReader.GetMessage(messageKey);
            string actual = ToastMessageReader.GetToastMessage(_driver);

            Assert.That(actual, Is.EqualTo(expected));

        }

        [Then("no test certification records should exist")]
        public void ThenNoTestCertificationRecordsShouldExist()
        {
            Assert.That(
                _certificationPage.AreAnyTestCertificationsPresent(),
                Is.False
            );
        }
    }
}
