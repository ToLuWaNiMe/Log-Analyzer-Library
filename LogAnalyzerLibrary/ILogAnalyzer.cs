using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalyzerLibrary
{
    public interface ILogAnalyzerService
    {
        Task<IEnumerable<string>> SearchLogsInDirectoriesAsync(IEnumerable<string> directories);
        Task<Dictionary<string, int>> CountUniqueErrorsPerLogAsync(string logFilePath);
        Task<Dictionary<string, int>> CountDuplicatedErrorsPerLogAsync(string logFilePath);
        Task DeleteArchivedLogsFromPeriodAsync(string directory, DateTime startDate, DateTime endDate);
        Task ArchiveLogsFromPeriodAsync(string directory, DateTime startDate, DateTime endDate);
        Task UploadLogsToRemoteServerAsync(string logFilePath, string apiEndpoint);
        Task DeleteLogsFromPeriodAsync(string directory, DateTime startDate, DateTime endDate);
        Task<int> CountTotalLogsInPeriodAsync(string directory, DateTime startDate, DateTime endDate);
        Task<IEnumerable<string>> SearchLogsBySizeAsync(string directory, long minSizeKb, long maxSizeKb);
        Task<IEnumerable<string>> SearchLogsByDirectoryAsync(string directory);
    }
}
