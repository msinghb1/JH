using System;
using System.IO;
using System.Linq;
using DataValidationFramework.Config;
using DataValidationFramework.Helper;
using FluentAssertions;
using Reqnroll;

namespace DataValidationFramework.Tests.StepDefinitions
{
    [Binding]
    public class DuplicateColumnSteps
    {
        private string[] reportFiles;
        private string duplicateInfo = "";
        private readonly string testFilesPath;

        public DuplicateColumnSteps()
        {
            testFilesPath = AppConfig.Get("TestDataDirectory");
        }

        [Given("the report files are loaded")]
        public void GivenTheReportFilesAreLoaded()
        {
            reportFiles = Directory.GetFiles(testFilesPath, "y_*.*");
            reportFiles.Should().NotBeEmpty("report files should be present in the 'files' folder");
        }

        [When("I run the duplicate column validation")]
        public void WhenIRunTheDuplicateColumnValidation()
        {
            duplicateInfo = "";
            foreach (var file in reportFiles)
            {
                var header = File.ReadLines(file).First();
                var columns = header.Split(',').Select(h => h.Trim()).ToList();
                var duplicates = columns
                    .GroupBy(c => c, StringComparer.OrdinalIgnoreCase)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                var filename = Path.GetFileName(file);

                if (duplicates.Any())
                {
                    duplicateInfo += $"duplicate columns found:\n File: {filename} - Duplicate Columns: {string.Join(", ", duplicates)}\n";
                    TestLogger.Log(duplicateInfo);
                }
            }
        }

        [Then("there should be no duplicate columns")]
        public void ThenThereShouldBeNoDuplicateColumns()
        {
            duplicateInfo.Should().BeNullOrEmpty("duplicate columns found:\n" + duplicateInfo);
        }
    }
}