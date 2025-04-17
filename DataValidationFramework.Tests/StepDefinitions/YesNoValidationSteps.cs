using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataValidationFramework.Interfaces;
using DataValidationFramework.Models;
using FluentAssertions;
using Reqnroll;
using DataValidationFramework.Config;
using DataValidationFramework.Helper;

namespace DataValidationFramework.Tests.StepDefinitions
{
    [Binding]
    public class YesNoValidationSteps
    {
        private readonly ICsvReader _csvReader;
        private readonly IExcelReader _excelReader;

        private Dictionary<string, List<FieldDefinition>> yesNoFieldMap;
        private string validationErrors = "";
        private static string testFilesPath = AppConfig.Get("TestDataDirectory");
        private static string requirementsExcelPath = AppConfig.Get("RequirementsFile");

        public YesNoValidationSteps(ICsvReader csvReader, IExcelReader excelReader)
        {
            _csvReader = csvReader;
            _excelReader = excelReader;
        }

        [Given("the report files and Excel definitions are available")]
        public void GivenTheReportFilesAndExcelDefinitionsAreAvailable()
        {
            yesNoFieldMap = _excelReader.GetYesNoFieldsMappedByFile(requirementsExcelPath);
            yesNoFieldMap.Should().NotBeEmpty("Yes/No fields must be defined in Excel");

            foreach (var file in yesNoFieldMap.Keys)
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, testFilesPath, file);
                File.Exists(filePath).Should().BeTrue($"Expected test file '{file}' to exist in TestData/Files");
            }
        }

        [When("I run the Yes No field validation")]
        public void WhenIRunTheYesNoFieldValidation()
        {
            foreach (var kvp in yesNoFieldMap)
            {
                string fileName = kvp.Key;
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, testFilesPath, fileName);
                var records = _csvReader.ReadCsv(filePath);

                foreach (var record in records)
                {
                    var dict = (IDictionary<string, object>)record;

                    foreach (var field in kvp.Value)
                    {
                        if (dict.TryGetValue(field.ColumnCode, out var valueObj))
                        {
                            string value = valueObj?.ToString().Trim() ?? "";
                            TestLogger.Log($"Checking value for column {field.ColumnCode} in file {fileName}: {value}");

                            if (!value.Equals("Yes", StringComparison.OrdinalIgnoreCase) &&
                                !value.Equals("No", StringComparison.OrdinalIgnoreCase))
                            {
                                validationErrors += $"File: {fileName} - Column: '{field.ColumnCode}' has invalid value: '{value}'\n";
                                TestLogger.Log($"X Invalid value '{value}' in column {field.ColumnCode} of file {fileName}");
                            }
                        }
                        else
                        {
                            validationErrors += $"File: {fileName} is missing expected column '{field.ColumnCode}'\n";
                            TestLogger.Log($"X Missing expected column: {field.ColumnCode} in file {fileName}");
                        }
                    }
                }
            }
        }

        [Then("there should be no invalid Yes No values")]
        public void ThenThereShouldBeNoInvalidYesNoValues()
        {
            validationErrors.Should().BeNullOrEmpty();
        }
    }
}