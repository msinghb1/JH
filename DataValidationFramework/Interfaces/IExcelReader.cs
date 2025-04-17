using DataValidationFramework.Models;
using System.Collections.Generic;

namespace DataValidationFramework.Interfaces
{
    public interface IExcelReader
    {
        Dictionary<string, List<FieldDefinition>> GetYesNoFieldsMappedByFile(string excelPath);
    }
}