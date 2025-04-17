using DataValidationFramework.Models;
using DataValidationFramework.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace DataValidationFramework.Services
{
    public class ExcelFieldReader : IExcelReader
    {
        public Dictionary<string, List<FieldDefinition>> GetYesNoFieldsMappedByFile(string excelPath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage(new FileInfo(excelPath));
            var sheet = package.Workbook.Worksheets["Field Definitions"];

            var yesNoMap = new Dictionary<string, List<FieldDefinition>>(StringComparer.OrdinalIgnoreCase);

            for (int row = 2; row <= sheet.Dimension.End.Row; row++)
            {
                string columnCodeRaw = sheet.Cells[row, 1].Text?.Trim();
                string columnName = sheet.Cells[row, 2].Text?.Trim();
                string type = sheet.Cells[row, 3].Text?.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(columnCodeRaw) || string.IsNullOrWhiteSpace(type))
                    continue;

                if (!type.Contains("yes") || !type.Contains("no") || !columnCodeRaw.StartsWith("y_"))
                    continue;

                var parts = columnCodeRaw.Split('.');
                if (parts.Length == 3)
                {
                    string fileName = $"{parts[0]}.{parts[1]}.csv";
                    string columnCode = $"c{parts[2]}";

                    if (!yesNoMap.ContainsKey(fileName))
                        yesNoMap[fileName] = new List<FieldDefinition>();

                    yesNoMap[fileName].Add(new FieldDefinition
                    {
                        ColumnCode = columnCode,
                        ColumnName = columnName,
                        Type = type
                    });
                }
            }

            return yesNoMap;
        }
    }
}