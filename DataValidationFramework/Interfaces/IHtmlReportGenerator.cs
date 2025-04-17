namespace DataValidationFramework.Interfaces
{
    public interface IHtmlReportGenerator
    {
        void GenerateFromTextLog(string textLogPath, string htmlOutputPath);
    }
}