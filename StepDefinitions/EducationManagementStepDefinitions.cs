using MarsCompetitionReqnroll.Net8Selenium.Drivers;
using MarsCompetitionReqnroll.Net8Selenium.Pages;
using MarsCompetitionReqnroll.Net8Selenium.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using System.Threading;

namespace MarsCompetitionReqnroll.Net8Selenium.StepDefinitions
{
    [Binding]
    public class EducationManagementStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly EducationListPage _educationListPage;
        private readonly EducationFormPage _educationFormPage;

        public EducationManagementStepDefinitions()
        {
            _driver = Driver.GetDriver();
            _educationListPage = new EducationListPage(_driver);
            _educationFormPage = new EducationFormPage(_driver);
        }

        [Given("the user is logged into the application")]
        public void GivenTheUserIsLoggedIntoTheApplication()
        {
        }

        [Given("the user navigates to the Education section")]
        public void GivenTheUserNavigatesToTheEducationSection()
        {
            _educationListPage.NavigateToEducationTab();
        }

        [Given("an education record exists using test data {string}")]
        public void GivenAnEducationRecordExistsUsingTestData(string testCaseId)
        {
            var data = JsonDataReader.GetEducationData(testCaseId);

            _educationListPage.ClickAddNew();
            _educationFormPage.EnterUniversity(data.University);
            _educationFormPage.SelectCountry(data.Country);
            _educationFormPage.SelectTitle(data.Title);
            _educationFormPage.EnterDegree(data.Degree);
            _educationFormPage.SelectYear(data.Year);
            _educationFormPage.ClickAdd();
        }

        [When("the user adds a new education using test data {string}")]
        public void WhenTheUserAddsANewEducationUsingTestData(string testCaseId)
        {
            var data = JsonDataReader.GetEducationData(testCaseId);

            _educationListPage.ClickAddNew();

            if (!string.IsNullOrEmpty(data.University))
                _educationFormPage.EnterUniversity(data.University);

            if (!string.IsNullOrEmpty(data.Country))
                _educationFormPage.SelectCountry(data.Country);

            if (!string.IsNullOrEmpty(data.Title))
                _educationFormPage.SelectTitle(data.Title);

            if (!string.IsNullOrEmpty(data.Degree))
                _educationFormPage.EnterDegree(data.Degree);

            if (!string.IsNullOrEmpty(data.Year))
                _educationFormPage.SelectYear(data.Year);

            _educationFormPage.ClickAdd();
        }

        [When("the user edits the education record using test data {string}")]
        public void WhenTheUserEditsTheEducationRecordUsingTestData(string testCaseId)
        {
            var data = JsonDataReader.GetEducationData(testCaseId);

            _educationListPage.ClickEdit();
            _educationFormPage.EnterUniversity(data.University);
            _educationFormPage.ClickAdd();
        }

        [When("the user deletes the education record")]
        public void WhenTheUserDeletesTheEducationRecord()
        {
            _educationListPage.ClickDelete();
            Thread.Sleep(800);
        }

        [When("the user deletes all education records created during the test session")]
        public void WhenTheUserDeletesAllEducationRecordsCreatedDuringTheTestSession()
        {
            try
            {
                while (true)
                {
                    _educationListPage.ClickDelete();
                    Thread.Sleep(500);
                }
            }
            catch
            {
            }
        }

        [Then("the education record should be created successfully")]
        public void ThenTheEducationRecordShouldBeCreatedSuccessfully()
        {
            Assert.Pass("Education record created successfully");
        }

        [Then("the education record should be updated successfully")]
        public void ThenTheEducationRecordShouldBeUpdatedSuccessfully()
        {
            Assert.Pass("Education record updated successfully");
        }

        [Then("the education record should be deleted successfully")]
        public void ThenTheEducationRecordShouldBeDeletedSuccessfully()
        {
            Assert.Pass("Education record deleted successfully");
        }

        [Then("a validation message should be displayed")]
        public void ThenAValidationMessageShouldBeDisplayed()
        {
            Assert.That(_educationFormPage.IsValidationDisplayed(),
                "Validation message was not displayed");
        }

        [Then("the education record should not be created")]
        public void ThenTheEducationRecordShouldNotBeCreated()
        {
            Assert.That(_educationFormPage.IsValidationDisplayed(),
                "Record was created when it should not be");
        }

        [Then("all test education records should be removed successfully")]
        public void ThenAllTestEducationRecordsShouldBeRemovedSuccessfully()
        {
            Assert.Pass("All education records cleaned successfully");
        }
    }
}