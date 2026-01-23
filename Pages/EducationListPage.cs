using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace MarsCompetitionReqnroll.Net8Selenium.Pages
{
    public class EducationListPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public EducationListPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private By EducationTab =>
            By.XPath("//a[text()='Education']");

        private By AddNewButton =>
            By.XPath("/html/body/div[1]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/thead/tr/th[6]/div");

        private By EditButton =>
            By.XPath("/html/body/div[1]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody/tr/td[6]/span[1]/i");

        private By DeleteButton =>
            By.XPath("/html/body/div[1]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody/tr/td[6]/span[2]/i");

        private By ValidationMessage =>
            By.XPath("//div[contains(@class,'ui basic red pointing prompt label')]");

        public void NavigateToEducationTab()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(EducationTab)).Click();
        }

        public void ClickAddNew()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(AddNewButton)).Click();
        }

        public void ClickEdit()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(EditButton)).Click();
        }

        public void ClickDelete()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(DeleteButton)).Click();
        }

        public bool IsValidationMessageDisplayed()
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(ValidationMessage)).Displayed;
        }
    }
}

