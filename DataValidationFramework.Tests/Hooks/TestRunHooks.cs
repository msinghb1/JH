using Reqnroll;
using DataValidationFramework.Config;
using System.IO;
using System;
using DataValidationFramework.Helper;
using DataValidationFramework.Services;
using DataValidationFramework.Interfaces;

namespace DataValidationFramework.Tests.Hooks
{
    [Binding]
    public sealed class TestRunHooks
    {
        private DateTime _scenarioStartTime;

        [BeforeTestRun(Order = 0)]
        public static void RegisterServices(Reqnroll.BoDi.ObjectContainer container)
        {
            container.RegisterTypeAs<CsvReaderService, ICsvReader>();
            container.RegisterTypeAs<ExcelFieldReader, IExcelReader>();
            container.RegisterTypeAs<HtmlReportGenerator, IHtmlReportGenerator>();
        }

        [BeforeTestRun]
        public static void InitReport()
        {
            var textLogPath = AppConfig.Get("ReportTextLog");
            Directory.CreateDirectory(Path.GetDirectoryName(textLogPath)!);
            File.WriteAllText(textLogPath, $"Test execution started at {DateTime.Now}\n");
        }

        [BeforeScenario]
        public void LogScenarioStart(ScenarioContext scenarioContext)
        {
            _scenarioStartTime = DateTime.Now;
            TestLogger.Log($"START Scenario: {scenarioContext.ScenarioInfo.Title}");
        }

        [AfterScenario]
        public void LogScenarioEnd(ScenarioContext scenarioContext)
        {
            var duration = DateTime.Now - _scenarioStartTime;
            var status = scenarioContext.TestError == null ? "PASSED" : "FAILED";

            TestLogger.Log($"{status}: {scenarioContext.ScenarioInfo.Title} [{duration.TotalSeconds:F2} sec]");

            if (scenarioContext.TestError != null)
            {
                TestLogger.Log($"Error: {scenarioContext.TestError.Message}");
            }

            TestLogger.Log($"\n");
        }

        [AfterTestRun]
        public static void GenerateReport()
        {
            var textLogPath = AppConfig.Get("ReportTextLog");
            var htmlOutputPath = AppConfig.Get("ReportHtmlLog");

            var generator = new HtmlReportGenerator(); // manual instantiation
            generator.GenerateFromTextLog(textLogPath, htmlOutputPath);
        }
    }
}
