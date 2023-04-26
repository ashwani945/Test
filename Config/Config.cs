using System.Collections.Generic;
using System.Drawing;
using System.Security.Policy;

namespace FiservAssignment.Config
{
    public class BrowserSearchInfo
    {
        public BrowserType BrowserType { get; set; }
        public SearchEngine SearchEngine { get; set; }
    }

    public class BrowserType
    {
        public string ChromeBrowser { get; set; }
    }

    public class SearchEngine
    {
        public string Google { get; set; }
        public string Bing { get; set; }
        public string Yahoo { get; set; }
    }

}
