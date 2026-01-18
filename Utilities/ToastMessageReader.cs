using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MarsCompetitionReqnroll.Net8Selenium.Utilities
{
    public static class ToastMessageReader
    {
        public static string GetToastMessage(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            return wait.Until(d =>
            {
                var elements = d.FindElements(By.XPath("//*[contains(@class,'ns-box')]"));

                foreach (var el in elements)
                {
                    if (el.Displayed && !string.IsNullOrWhiteSpace(el.Text))
                        return el.Text.Trim();
                }

                return null;
            });
        }
    }
}

