using AventStack.ExtentReports;
using FiservAssignment.Config;
using FiservAssignment.Page;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using TestFramework;

namespace FiservAssignment.Test
{
    [TestFixture]
    public class SearchResult
    {
        IWebDriver driver;
        [Test]
        public void TestSearch()
        {
             driver = DriverSetup.GetDriver();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\source\repos\FiservAssignment\FiservAssignment\" + "appconfig.json";
            // Deserialize appconfig.json to BrowserOptions object
            string JsonPath = File.ReadAllText(path);
            BrowserSearchInfo searchEngine = JsonConvert.DeserializeObject<BrowserSearchInfo>(JsonPath);
            driver.Navigate().GoToUrl(searchEngine.SearchEngine.Google.ToString());
            IWebElement searchBox = driver.FindElement(SearchResultPage.SearchBox);
            searchBox.SendKeys("Fiserv");
            searchBox.SendKeys(Keys.Return);
            IWebElement firstResult = driver.FindElement(SearchResultPage.FirstResult);
            string actualResult = firstResult.GetAttribute("href");
            string expectedResult = "https://www.fiserv.com/";
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}