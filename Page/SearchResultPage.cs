using OpenQA.Selenium;
using TestFramework;

namespace FiservAssignment.Page
{
    
    public class SearchResultPage
    {
        public static readonly By SearchBox = By.Name("q");
        public static readonly By FirstResult = By.CssSelector("#search .g:nth-of-type(1) a");
    }
}