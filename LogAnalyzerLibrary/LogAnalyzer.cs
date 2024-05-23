using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalyzerLibrary
{
    public class LogAnalyzerService : ILogAnalyzerService
    {
        public async Task<IEnumerable<string>> SearchLogsInDirectoriesAsync(IEnumerable<string> directories)
        {
            var logFiles = new List<string>();
            foreach (var dir in directories)
            {
                if (Directory.Exists(dir))
                {
                    logFiles.AddRange(Directory.GetFiles(dir, "*.log", SearchOption.AllDirectories));
                }
            }
            return await Task.FromResult(logFiles);
        }

        public async Task<Dictionary<string, int>> CountUniqueErrorsPerLogAsync(string logFilePath)
        {
            var errorCounts = new Dictionary<string, int>();
            var lines = await File.ReadAllLinesAsync(logFilePath);
            foreach (var line in lines.Distinct())
            {
                if (!errorCounts.ContainsKey(line))
                {
                    errorCounts[line] = 1;
                }
            }
            return errorCounts;
        }

        public async Task<Dictionary<string, int>> CountDuplicatedErrorsPerLogAsync(string logFilePath)
        {
            var errorCounts = new Dictionary<string, int>();
            var lines = await File.ReadAllLinesAsync(logFilePath);
            foreach (var line in lines)
            {
                if (errorCounts.ContainsKey(line))
                {
                    errorCounts[line]++;
                }
                else
                {
                    errorCounts[line] = 1;
                }
            }
            return errorCounts.Where(kvp => kvp.Value > 1).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public async Task DeleteArchivedLogsFromPeriodAsync(string directory, DateTime startDate, DateTime endDate)
        {
            var zipFileName = Path.Combine(directory, $"{startDate:yyyyMMdd}-{endDate:yyyyMMdd}.zip");
            if (File.Exists(zipFileName))
            {
                File.Delete(zipFileName);
            }
            await Task.CompletedTask;
        }

        public async Task ArchiveLogsFromPeriodAsync(string directory, DateTime startDate, DateTime endDate)
        {
            var logFiles = Directory.GetFiles(directory, "*.log", SearchOption.AllDirectories)
                .Where(f => File.GetCreationTime(f) >= startDate && File.GetCreationTime(f) <= endDate).ToList();
            var zipFileName = Path.Combine(directory, $"{startDate:yyyyMMdd}-{endDate:yyyyMMdd}.zip");

            using (var zip = ZipFile.Open(zipFileName, ZipArchiveMode.Create))
            {
                foreach (var logFile in logFiles)
                {
                    zip.CreateEntryFromFile(logFile, Path.GetFileName(logFile));
                    File.Delete(logFile);
                }
            }

            await Task.CompletedTask;
        }

        public async Task UploadLogsToRemoteServerAsync(string logFilePath, string apiEndpoint)
        {
            using var client = new HttpClient();
            using var content = new MultipartFormDataContent();
            content.Add(new StreamContent(File.OpenRead(logFilePath)), "file", Path.GetFileName(logFilePath));
            var response = await client.PostAsync(apiEndpoint, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteLogsFromPeriodAsync(string directory, DateTime startDate, DateTime endDate)
        {
            var logFiles = Directory.GetFiles(directory, "*.log", SearchOption.AllDirectories)
                .Where(f => File.GetCreationTime(f) >= startDate && File.GetCreationTime(f) <= endDate).ToList();

            foreach (var logFile in logFiles)
            {
                File.Delete(logFile);
            }

            await Task.CompletedTask;
        }

        public async Task<int> CountTotalLogsInPeriodAsync(string directory, DateTime startDate, DateTime endDate)
        {
            var logFiles = Directory.GetFiles(directory, "*.log", SearchOption.AllDirectories)
                .Where(f => File.GetCreationTime(f) >= startDate && File.GetCreationTime(f) <= endDate).ToList();

            return await Task.FromResult(logFiles.Count);
        }

        public async Task<IEnumerable<string>> SearchLogsBySizeAsync(string directory, long minSizeKb, long maxSizeKb)
        {
            var logFiles = Directory.GetFiles(directory, "*.log", SearchOption.AllDirectories)
                .Where(f => new FileInfo(f).Length >= minSizeKb * 1024 && new FileInfo(f).Length <= maxSizeKb * 1024);

            return await Task.FromResult(logFiles);
        }

        public async Task<IEnumerable<string>> SearchLogsByDirectoryAsync(string directory)
        {
            var logFiles = Directory.GetFiles(directory, "*.log", SearchOption.AllDirectories);
            return await Task.FromResult(logFiles);
        }

    }
}
