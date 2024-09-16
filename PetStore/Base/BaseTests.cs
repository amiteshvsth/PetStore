using OpenQA.Selenium;
using PetStore.utilities;

namespace PetStore.Base
{
    [TestClass]
    public class BaseTests
    {
        public IWebDriver Driver;
        protected WebDriverFactory DriverFactory = new();

        protected string PetStoreUrl;
        public string Browser { get; set; }
        protected Logger Log { get; set; }
        public TestContext TestContext { get; set; }


        [TestInitialize]
        public void Setup()
        {
            Log = new Logger($"C:\\Users\\amitesh\\source\\repos\\PetStore\\PetStore\\Resources\\Logs\\{SetFileName("Log")}.log");
            Log.Info($"Starting test {TestContext.TestName}");

            Browser = TestContext.Properties["Browser"].ToString().ToLower();

            PetStoreUrl = TestContext.Properties["PetStoreUrl"].ToString();

            Driver = DriverFactory.InitBrowser(Browser);
        }

        [TestCleanup]
        public void TearDown()
        {
            Log.Info($"Result - {TestContext.TestName} {TestContext.CurrentTestOutcome}");

            if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
            {
                try
                {
                    //screenshot                    
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
            try
            {
                TestContext.AddResultFile(Log.LogPath);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            DriverFactory.CloseBrowser();
        }

        private string SetFileName(string type)
        {
            var fullyQualifiedTestClassName = TestContext.FullyQualifiedTestClassName.Split('.');
            var className = fullyQualifiedTestClassName[^1];
            var filename = $"[Test]_[{type}]_{new CSharpHelpers().GenerateRandomNumber()}_{className}_{TestContext.TestName}_{DateTime.Now:yy-MM-dd HH.mm.ss}";
            if (filename.Length > 70)
            {
                filename = filename[..70];
            }
            return filename;
        }
    }
}
