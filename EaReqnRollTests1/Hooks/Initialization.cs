using System.Reflection;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Feature = AventStack.ExtentReports.Gherkin.Model.Feature;
using Scenario = AventStack.ExtentReports.Gherkin.Model.Scenario;

namespace EaReqnRollTests1.Hooks
{
    [Binding]
    public class Initialization
    {
        private static ExtentReports _extentReports;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private ExtentTest _scenario;

        public Initialization(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [BeforeTestRun]
        public static void InitializeExtendReport()
        {
            var extentReport = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "extentreport.html";
            _extentReports = new ExtentReports();
            var spark = new ExtentSparkReporter(extentReport);
            _extentReports.AttachReporter(spark);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var feature = _extentReports.CreateTest<Feature>(_featureContext.FeatureInfo.Title);
            _scenario = feature.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }
    }
}
