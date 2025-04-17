using DataValidationFramework.Interfaces;
using System.Text;
using System;
using System.IO;

public class HtmlReportGenerator : IHtmlReportGenerator
{
    public void GenerateFromTextLog(string textLogPath, string htmlOutputPath)
    {
        if (!File.Exists(textLogPath)) return;

        var lines = File.ReadAllLines(textLogPath);
        var sb = new StringBuilder();
        sb.AppendLine("<html><head><title>Test Report</title>");
        sb.AppendLine("<style>");
        sb.AppendLine("body { font-family: Arial; }");
        sb.AppendLine("table { width: 100%; border-collapse: collapse; }");
        sb.AppendLine("th, td { border: 1px solid #ccc; padding: 8px; text-align: left; }");
        sb.AppendLine(".passed { background-color: #d4edda; }");
        sb.AppendLine(".failed { background-color: #f8d7da; }");
        sb.AppendLine("</style></head><body>");
        sb.AppendLine("<h2>Data Validation Test Report</h2>");
        sb.AppendLine("<table><tr><th>Time</th><th>Status</th><th>Details</th></tr>");

        foreach (var line in lines)
        {
            var cssClass = line.Contains("FAILED") ? "failed" :
                           line.Contains("PASSED") ? "passed" : "";
            var time = DateTime.Now.ToString("HH:mm:ss");
            sb.AppendLine($"<tr class='{cssClass}'><td>{time}</td><td>{(line.Contains("PASSED") ? "Passed" : line.Contains("FAILED") ? "Failed" : "-")}</td><td>{line}</td></tr>");
        }

        sb.AppendLine("</table></body></html>");
        File.WriteAllText(htmlOutputPath, sb.ToString());
    }
}
