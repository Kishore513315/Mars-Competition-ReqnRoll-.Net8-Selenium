using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace MarsCompetitionReqnroll.Net8Selenium.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        private readonly By signInLink = By.XPath("//a[text()='Sign In']");
        private readonly By usernameField = By.Name("email");
        private readonly By passwordField = By.Name("password");
        private readonly By loginButton = By.XPath("//button[text()='Login']");
        private readonly By dashboardUserLocator = By.XPath("//span[contains(text(),'Hi')]");

        public void GoToLoginPage()
        {
            _driver.Navigate().GoToUrl("http://localhost:5003/");
            _wait.Until(ExpectedConditions.ElementToBeClickable(signInLink)).Click();
        }

        public void EnterCredentials(string email, string password)
        {
            var emailField = _wait.Until(ExpectedConditions.ElementIsVisible(usernameField));
            emailField.Clear();
            emailField.SendKeys(email);

            var passwordFieldEl = _wait.Until(ExpectedConditions.ElementIsVisible(passwordField));
            passwordFieldEl.Clear();
            passwordFieldEl.SendKeys(password);
        }

        public void ClickLogin()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(loginButton)).Click();
        }

        public bool IsDashboardVisible()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(dashboardUserLocator));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsUserNameDisplayed()
        {
            try
            {
                var userName = _wait.Until(ExpectedConditions.ElementIsVisible(dashboardUserLocator));
                return userName.Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}

