using System.Collections.Generic;

namespace DataValidationFramework.Interfaces
{
    public interface ICsvReader
    {
        List<dynamic> ReadCsv(string path);
    }
}