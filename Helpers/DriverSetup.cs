using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace TestFramework
{
    public class DriverSetup
    {
        private static IWebDriver driver;

        public static IWebDriver GetDriver()
        {
            if (driver == null)
            {
                throw new NullReferenceException("Driver has not been initialized");
            }

            return driver;
        }

        public static void InitDriver(string browser)
        {
            if (driver != null)
            {
                throw new InvalidOperationException("Driver has already been initialized");
            }
            String downloadFilepath = "E:\\Downloads";
            switch (browser)
            {
                case "chrome":
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddUserProfilePreference("download.default_directory", downloadFilepath);
                    chromeOptions.AddArgument("--start-maximized");
                    chromeOptions.AddArgument("no-sandbox");
                    driver = new RemoteWebDriver(new Uri("http://localhost:4444"), chromeOptions);
                    break;
                case "firefox":
                    FirefoxOptions firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArgument("--start-maximized");
                    firefoxOptions.AddArgument("no-sandbox");
                    firefoxOptions.BrowserExecutableLocation = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
                    driver = new RemoteWebDriver(new Uri("http://localhost:4444"), firefoxOptions);
                    break;
                case "edge":
                    EdgeOptions edgeOptions = new EdgeOptions();
                    edgeOptions.AddUserProfilePreference("download.default_directory", downloadFilepath);
                    edgeOptions.AddArgument("--start-maximized");
                    edgeOptions.AddArgument("no-sandbox");
                    driver = new RemoteWebDriver(new Uri("http://localhost:4444"), edgeOptions);
                    break;
                default:
                    throw new ArgumentException("Invalid browser name");
            }
        }
        public static void QuitDriver()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }
    }
}
