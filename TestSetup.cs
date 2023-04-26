using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using FiservAssignment.Config;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using RestSharp;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace FiservAssignment
{
    [SetUpFixture]
    public class TestSetup
    {
        private ExtentReports extent;
        public ExtentTest testlog;

        [OneTimeSetUp]

        public void Setup()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\source\repos\FiservAssignment\FiservAssignment\" + "appconfig.json";
            // Deserialize appconfig.json to BrowserOptions object
            string JsonPath = File.ReadAllText(path);
            BrowserSearchInfo browserOptions = JsonConvert.DeserializeObject<BrowserSearchInfo>(JsonPath);
            TestFramework.DriverSetup.InitDriver(browserOptions.BrowserType.ChromeBrowser.ToString());
            var Extentpath = Assembly.GetCallingAssembly().CodeBase;
            var actualPath = Extentpath.Substring(0, Extentpath.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(projectPath.ToString() + "Reports");
            var reportPath = projectPath + "Reports\\ExtentReport.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            testlog = extent.CreateTest(TestContext.CurrentContext.Test.Name);

        }
        [OneTimeTearDown]
        public void GenerateTestReport()
        {
            IWebDriver driver = TestFramework.DriverSetup.GetDriver();
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                // Take a screenshot and attach it to the extent report

                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                testlog.Fail(TestContext.CurrentContext.Result.Message, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build());
            }
            else
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                testlog.Pass(TestContext.CurrentContext.Result.Message, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build());
            }
            extent.Flush();
            TestFramework.DriverSetup.QuitDriver();
        }
    }
}

