using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Linq;

namespace MarsCompetitionReqnroll.Net8Selenium.Pages
{
    public class CertificationPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public CertificationPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        private By profileTab =>
            By.XPath("//a[text()='Profile']");

        private By certificationsTab =>
            By.XPath("//a[text()='Certifications']");
        private By addNewButton =>
            By.XPath("//th[contains(text(),'Certificate')]/following-sibling::th//div");

        private By certificationNameInput =>
            By.Name("certificationName");

        private By certificationFromInput =>
            By.Name("certificationFrom");

        private By certificationYearDropdown =>
            By.Name("certificationYear");

        private By addButton =>
            By.XPath("//input[@value='Add']");

        private By updateButton =>
            By.XPath("//input[@value='Update']");

        private By certificationRows =>
            By.XPath("//tbody/tr");

        private By EditIconForCertificate(string certificateName) =>
            By.XPath($"//td[text()='{certificateName}']/following-sibling::td//i[contains(@class,'write')]");

        private By DeleteIconForCertificate(string certificateName) =>
            By.XPath($"//td[text()='{certificateName}']/following-sibling::td//i[contains(@class,'remove')]");

        public void NavigateToProfile()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(profileTab)).Click();
        }

        public void OpenCertificationsTab()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(certificationsTab)).Click();
        }

        private void ClickAddNew()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(addNewButton)).Click();
        }

        private void EnterCertificationName(string name)
        {
            var el = _wait.Until(ExpectedConditions.ElementIsVisible(certificationNameInput));
            el.Clear();
            el.SendKeys(name);
        }

        private void EnterCertificationFrom(string from)
        {
            var el = _wait.Until(ExpectedConditions.ElementIsVisible(certificationFromInput));
            el.Clear();
            el.SendKeys(from);
        }

        private void SelectCertificationYear(string year)
        {
            var dropdown = _wait.Until(ExpectedConditions.ElementIsVisible(certificationYearDropdown));

            if (!string.IsNullOrEmpty(year))
            {
                new SelectElement(dropdown).SelectByText(year);
            }
        }

        public void ClickAdd()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(addButton)).Click();
        }

        public void AddCertification(string name, string from, string year)
        {
            EnsureAddNewFormIsOpen();

            EnterCertificationName(name);
            EnterCertificationFrom(from);
            SelectCertificationYear(year);
            ClickAdd();
        }

        private void EnsureAddNewFormIsOpen()
        {
            try
            {
                if (_driver.FindElements(certificationNameInput).Count > 0)
                    return;
            }
            catch
            {
            }

            _wait.Until(ExpectedConditions.ElementToBeClickable(addNewButton)).Click();
            _wait.Until(ExpectedConditions.ElementIsVisible(certificationNameInput));
        }

        public void UpdateCertification(string existingName, string updatedName)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(EditIconForCertificate(existingName))).Click();

            var nameInput = _wait.Until(ExpectedConditions.ElementIsVisible(certificationNameInput));
            nameInput.Clear();
            nameInput.SendKeys(updatedName);

            _wait.Until(ExpectedConditions.ElementToBeClickable(updateButton)).Click();
        }

        public bool IsCertificationPresent(string certificateName)
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementExists(
                    By.XPath($"//td[text()='{certificateName}']")));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void DeleteCertification(string certificateName)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(
                DeleteIconForCertificate(certificateName)
            )).Click();
        }

        public void DeleteAllCertifications()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(certificationsTab)).Click();

            By certRows = By.XPath("//form//table/tbody/tr");
            By deleteIcon = By.XPath(".//td[last()]//span[2]/i");
            By modal = By.CssSelector(".ui.modal");

            while (true)
            {
                var rows = _driver.FindElements(certRows);

                if (rows.Count == 0)
                    break; 

                var firstRow = rows[0];
                var removeBtn = firstRow.FindElement(deleteIcon);

                ((IJavaScriptExecutor)_driver)
                    .ExecuteScript("arguments[0].click();", removeBtn);

                try
                {
                    _wait.Until(ExpectedConditions.ElementIsVisible(modal));

                    var confirmBtn = _driver.FindElement(
                        By.CssSelector(".ui.modal.visible.active .ui.green.button"));

                    ((IJavaScriptExecutor)_driver)
                        .ExecuteScript("arguments[0].click();", confirmBtn);
                }
                catch (WebDriverTimeoutException)
                {
                }

                _wait.Until(driver =>
                    driver.FindElements(certRows).Count < rows.Count
                );
            }
        }

        public bool AreAnyTestCertificationsPresent()
        {
            return _driver.FindElements(certificationRows).Any();
        }
    }
}
