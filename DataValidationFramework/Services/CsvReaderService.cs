using CsvHelper;
using CsvHelper.Configuration;
using DataValidationFramework.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace DataValidationFramework.Services
{
    public class CsvReaderService : ICsvReader
    {
        public List<dynamic> ReadCsv(string path)
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            return new List<dynamic>(csv.GetRecords<dynamic>());
        }
    }
}